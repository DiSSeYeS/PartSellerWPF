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
    /// Логика взаимодействия для CoolingPage.xaml
    /// </summary>
    public partial class CoolingPage : Page
    {
        private static FilterParams filterParams;
        public CoolingPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadCoolingData(filterParams);
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
            if (e.Key == Key.Delete && dataGrid.SelectedItem != null)
            {
                var itemToDelete = dataGrid.SelectedItem as IDeletableComponent;
                if (itemToDelete != null && itemToDelete.ID != 0 && AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    var result = MessageBox.Show($"Вы уверены, что хотите удалить этот {itemToDelete.ComponentType}?",
                                              "Подтверждение удаления",
                                              MessageBoxButton.YesNo,
                                              MessageBoxImage.Question);

                    if (result == MessageBoxResult.Yes)
                    {
                        Funcs.DeleteComponent(itemToDelete);
                        LoadCoolingData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }
        private void LoadCoolingData(object filterParams)
        {
            if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
            {
                dataGrid.CanUserAddRows = true;
                dataGrid.CanUserDeleteRows = true;
                imageLinkColumn.Visibility = Visibility.Visible;
            }
            else
            {
                dataGrid.CanUserAddRows = false;
                dataGrid.CanUserDeleteRows = false;
                imageLinkColumn.Visibility = Visibility.Hidden;
            }

            try
            {
                var context = Entities.GetContext();

                var query = from c in context.Cooling
                            join p in context.Part on c.ID equals p.CoolingID
                            join prod in context.Product on p.ID equals prod.PartID
                            join ct in context.CoolerType on c.CoolerTypeID equals ct.ID
                            from ss in context.SupportedSockets.Where(ss => ss.CoolerID == c.ID).DefaultIfEmpty()
                            join s in context.Socket on ss.SocketID equals s.ID into socketJoin
                            from s in socketJoin.DefaultIfEmpty()
                            group new { c, p, prod, ct, ss, s } by c.ID into g
                            select new
                            {
                                Cooling = g.FirstOrDefault().c,
                                Part = g.FirstOrDefault().p,
                                Product = g.FirstOrDefault().prod,
                                CoolerType = g.FirstOrDefault().ct,
                                SupportedSockets = g.FirstOrDefault().ss,
                                Socket = g.FirstOrDefault().s
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Cooling.BrandID == brandId);
                    }

                    if (filters.SocketId.HasValue && filters.SocketId != -1)
                    {
                        int socketId = filters.SocketId.Value;
                        query = query.Where(x => x.SupportedSockets.SocketID == socketId);
                    }

                    if (filters.CoolerTypeId.HasValue && filters.CoolerTypeId != -1)
                    {
                        int coolertypeId = filters.CoolerTypeId.Value;
                        query = query.Where(x => x.CoolerType.ID == coolertypeId);
                    }

                    if (filters.MaxHeight.HasValue)
                        query = query.Where(x => x.Cooling.Height <= filters.MaxHeight.Value);

                    if (filters.MaxWidth.HasValue)
                        query = query.Where(x => x.Cooling.Width <= filters.MaxWidth.Value);

                    if (filters.MaxLength.HasValue)
                        query = query.Where(x => x.Cooling.Length <= filters.MaxLength.Value);

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new CoolingData
                {
                    Brand = x.Cooling.Brand.Name,
                    Model = x.Cooling.Model,
                    RPM = x.Cooling.RPM,
                    Height = x.Cooling.Height,
                    Length = x.Cooling.Length,
                    Width = x.Cooling.Width,
                    Type = x.CoolerType.Type,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new CoolingData());
                }

                dataGrid.ItemsSource = result.ToList();
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
                var editedItem = e.Row.Item as CoolingData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newCooling = new Cooling();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newCooling.Model = editedItem.Model;
                            newCooling.Height = int.Parse(editedItem.Height.ToString().Split().First());
                            newCooling.Length = int.Parse(editedItem.Length.ToString().Split().First());
                            newCooling.Width = int.Parse(editedItem.Width.ToString().Split().First());
                            newCooling.RPM = int.Parse(editedItem.RPM.ToString().Split().First());

                            var newCoolerType = context.CoolerType.FirstOrDefault(ct => ct.Type == editedItem.Type);
                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);

                            if (newbrand != null) newCooling.BrandID = newbrand.ID;
                            if (newCoolerType != null) newCooling.CoolerTypeID = newCoolerType.ID;

                            context.Cooling.Add(newCooling);
                            context.SaveChanges();

                            newPart.CoolingID = newCooling.ID;
                            newPart.Image = editedItem.Image;
                            if (editedItem.Image != null) newPart.Image = editedItem.Image;

                            context.Part.Add(newPart);
                            context.SaveChanges();

                            newProduct.PartID = newPart.ID;
                            newProduct.Price = int.Parse(editedItem.Price.ToString().Split().First());

                            context.Product.Add(newProduct);
                            context.SaveChanges();

                            editedItem.ID = newPart.ID;
                            editedItem.PartID = newProduct.PartID;

                            var items = dataGrid.ItemsSource as List<CoolingData>;
                            if (items != null)
                            {
                                items.Add(new CoolingData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новый кулер успешно добавлен",
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

                        var coolerType = context.CoolerType.FirstOrDefault(ct => ct.Type == editedItem.Type);
                        if (coolerType == null)
                        {
                            MessageBox.Show($"Тип охлаждения '{editedItem.Type}' не найден в базе данных",
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

                        var part = context.Part.FirstOrDefault(p => p.ID == editedItem.PartID);
                        var cooling = context.Cooling.FirstOrDefault(c => c.ID == part.CoolingID);

                        if (editedItem.RPM <= 0 || editedItem.RPM > 10000)
                        {
                            MessageBox.Show("Некорректное значение RPM (допустимый диапазон: 1-10000)",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.Height <= 0 || editedItem.Length <= 0 || editedItem.Width <= 0)
                        {
                            MessageBox.Show("Габариты должны быть положительными числами",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        var oldPrice = int.Parse(product.Price.ToString().Split().First());
                        product.Price = int.Parse(editedItem.Price.ToString().Split().First());

                        cooling.BrandID = brand.ID;
                        cooling.Model = editedItem.Model;
                        cooling.RPM = int.Parse(editedItem.RPM.ToString().Split().First());
                        cooling.Height = int.Parse(editedItem.Height.ToString().Split().First());
                        cooling.Length = int.Parse(editedItem.Length.ToString().Split().First());
                        cooling.Width = int.Parse(editedItem.Width.ToString().Split().First());
                        cooling.CoolerTypeID = coolerType.ID;

                        if (editedItem.Image != null)
                        {
                            part.Image = editedItem.Image;
                        }

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
                        MessageBox.Show("Данные системы охлаждения успешно обновлены",
                                      "Успех",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);
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
