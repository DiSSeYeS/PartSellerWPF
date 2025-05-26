using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PartSellerWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для SupplyPage.xaml
    /// </summary>
    public partial class SupplyPage : Page
    {
        private static FilterParams filterParams;
        public SupplyPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadSupplyData(filterParams);
            dataGrid.Loaded += DataGrid_Loaded;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.CurrentCellChanged += DataGrid_CurrentCellChanged;
        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            Funcs.ExtendCell(dataGrid);
        }
        private void dataGrid_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && dataGrid.SelectedItem != null && AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
            {
                var itemToDelete = dataGrid.SelectedItem as IDeletableComponent;
                if (itemToDelete != null && itemToDelete.ID != 0)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить этот {itemToDelete.ComponentType}?",
                                              "Подтверждение удаления",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Funcs.DeleteComponent(itemToDelete);
                        LoadSupplyData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }
        private void LoadSupplyData(object filterParams)
        {
            if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
            {
                dataGrid.CanUserAddRows = true;
                dataGrid.CanUserDeleteRows = true;
                imageLinkColumn.Visibility = Visibility.Visible;
                quantityInStockColumn.Visibility = Visibility.Visible;
                partIdColumn.Visibility = Visibility.Visible;
            }
            else
            {
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
                imageLinkColumn.Visibility = Visibility.Hidden;
                quantityInStockColumn.Visibility = Visibility.Hidden;
                partIdColumn.Visibility = Visibility.Hidden;
            }

            try
            {
                var context = Entities.GetContext();

                var query = from c in context.Supply
                            join p in context.Part on c.ID equals p.SupplyID
                            join prod in context.Product on p.ID equals prod.PartID
                            join ff in context.FormFactor on c.FormFactorID equals ff.ID
                            select new
                            {
                                Supply = c,
                                Part = p,
                                Product = prod,
                                FormFactor = ff,
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Supply.BrandID == brandId);
                    }

                    if (filters.MaxHeight.HasValue)
                        query = query.Where(x => x.Supply.Height <= filters.MaxHeight.Value);

                    if (filters.MaxWidth.HasValue)
                        query = query.Where(x => x.Supply.Width <= filters.MaxWidth.Value);

                    if (filters.MaxLength.HasValue)
                        query = query.Where(x => x.Supply.Length <= filters.MaxLength.Value);

                    if (filters.MaxWattage.HasValue)
                        query = query.Where(x => x.Supply.Wattage <= filters.MaxWattage.Value);

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new SupplyData
                {
                    Brand = x.Supply.Brand.Name,
                    Model = x.Supply.Model,
                    Wattage = x.Supply.Wattage,
                    Height = x.Supply.Height,
                    Length = x.Supply.Length,
                    Width = x.Supply.Width,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                    FormFactor = x.FormFactor.Type,
                    QuantityInStock = x.Part.QuantityInStock
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new SupplyData());
                }

                dataGrid.ItemsSource = result;
                dataGrid.IsReadOnly = !(AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2);
                dataGrid.CanUserAddRows = !(AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!AuthManager.IsLoggedIn)
            {
                MessageBox.Show("Необходимо авторизоваться");
                return;
            }

            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите компонент из таблицы");
                return;
            }

            dynamic selectedComponent = dataGrid.SelectedItem;

            Funcs.AddComponentToOrder(selectedComponent);
        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedItem = e.Row.Item as SupplyData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newSupply = new Supply();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newSupply.Model = editedItem.Model;
                            newSupply.Wattage = int.Parse(editedItem.Wattage.ToString().Split().First());
                            newSupply.Height = int.Parse(editedItem.Height.ToString().Split().First());
                            newSupply.Width = int.Parse(editedItem.Width.ToString().Split().First());
                            newSupply.Height = int.Parse(editedItem.Height.ToString().Split().First());

                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);
                            var newformfactor = context.FormFactor.FirstOrDefault(ff => ff.Type == editedItem.FormFactor);

                            if (newbrand != null) newSupply.BrandID = newbrand.ID;
                            if (newformfactor != null) newSupply.FormFactorID = newformfactor.ID;

                            context.Supply.Add(newSupply);
                            context.SaveChanges();

                            newPart.SupplyID = newSupply.ID;
                            newPart.Image = editedItem.Image;
                            if (editedItem.Image != null) newPart.Image = editedItem.Image;
                            if (editedItem.QuantityInStock > 0) newPart.QuantityInStock = editedItem.QuantityInStock;
                            else { newPart.QuantityInStock = 0; }

                            context.Part.Add(newPart);
                            context.SaveChanges();

                            newProduct.PartID = newPart.ID;
                            newProduct.Price = int.Parse(editedItem.Price.ToString().Split().First());

                            context.Product.Add(newProduct);
                            context.SaveChanges();

                            editedItem.ID = newPart.ID;
                            editedItem.PartID = newProduct.PartID;

                            var items = dataGrid.ItemsSource as List<SupplyData>;
                            if (items != null)
                            {
                                items.Add(new SupplyData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новый блок питания успешно добавлен",
                                          "Успех",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Information);
                            return;
                        }

                        var brand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);
                        if (brand == null)
                        {
                            MessageBox.Show($"Бренд '{editedItem.Brand}' не найден в базе данных",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        var formFactor = context.FormFactor.FirstOrDefault(ff => ff.Type == editedItem.FormFactor);
                        if (formFactor == null)
                        {
                            MessageBox.Show($"Форм-фактор '{editedItem.FormFactor}' не найден в базе данных",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        var product = context.Product.FirstOrDefault(p => p.PartID == editedItem.ID);
                        if (product == null)
                        {
                            MessageBox.Show("Продукт не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.QuantityInStock < 0)
                        {
                            MessageBox.Show("Количество компонентов на складе не может быть отрицательным числом",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        var part = context.Part.FirstOrDefault(p => p.ID == editedItem.PartID);
                        var supply = context.Supply.FirstOrDefault(s => s.ID == part.SupplyID);

                        var oldPrice = product.Price;

                        product.Price = int.Parse(editedItem.Price.ToString().Split().First());
                        supply.BrandID = brand.ID;
                        supply.FormFactorID = formFactor.ID;
                        supply.Model = editedItem.Model;
                        supply.Wattage = int.Parse(editedItem.Wattage.ToString().Split().First());
                        supply.Height = int.Parse(editedItem.Height.ToString().Split().First());
                        supply.Length = int.Parse(editedItem.Length.ToString().Split().First());
                        supply.Width = int.Parse(editedItem.Width.ToString().Split().First());

                        if (editedItem.Image != null)
                        {
                            part.Image = editedItem.Image;
                        }
                        part.QuantityInStock = editedItem.QuantityInStock;

                        var priceDifference = int.Parse(editedItem.Price.ToString().Split().First()) - oldPrice;
                        if (priceDifference != 0)
                        {
                            var ordersToUpdate = context.Order
                                .Where(o => o.OrderItem.Any(oi => oi.ProductID == editedItem.ProductID &&
                                new List<string> { "Корзина", "Оформление" }.Contains(o.Status)))
                                .ToList();

                            foreach (var order in ordersToUpdate)
                            {
                                order.TotalPrice += priceDifference;
                            }
                        }

                        context.SaveChanges();
                        MessageBox.Show("Изменения успешно сохранены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}\n\n{ex.InnerException?.Message}",
                                      "Ошибка",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                }
            }
        }
    }
}

