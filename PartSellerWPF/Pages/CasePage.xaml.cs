using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для CasePage.xaml
    /// </summary>
    public partial class CasePage : Page
    {
        private static FilterParams filterParams;
        public CasePage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadCaseData(filterParams);
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
                        LoadCaseData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }

        private void LoadCaseData(object filterParams)
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

                var query = from c in context.Case
                            join p in context.Part on c.ID equals p.CaseID
                            join prod in context.Product on p.ID equals prod.PartID
                            select new
                            {
                                Case = c,
                                Part = p,
                                Product = prod
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Case.BrandID == brandId);
                    }

                    if (filters.FormFactorId.HasValue && filters.FormFactorId != -1)
                    {
                        int formFactorId = filters.FormFactorId.Value;
                        query = query.Where(x => x.Case.FormFactorID == formFactorId);
                    }

                    if (filters.MaxHeight.HasValue)
                        query = query.Where(x => x.Case.Height <= filters.MaxHeight.Value);

                    if (filters.MaxWidth.HasValue)
                        query = query.Where(x => x.Case.Width <= filters.MaxWidth.Value);

                    if (filters.MaxLength.HasValue)
                        query = query.Where(x => x.Case.Length <= filters.MaxLength.Value);

                    if (filters.MaxGpuLength.HasValue)
                        query = query.Where(x => x.Case.GPULength <= filters.MaxGpuLength.Value);

                    if (filters.MaxCoolerHeight.HasValue)
                        query = query.Where(x => x.Case.CoolerLength <= filters.MaxCoolerHeight.Value);

                    if (filters.MaxPsuLength.HasValue)
                        query = query.Where(x => x.Case.SupplyLength <= filters.MaxPsuLength.Value);

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }
                    
                var result = query.AsEnumerable().Select(x => new CaseData
                {
                    Brand = x.Case.Brand.Name,
                    Model = x.Case.Model,
                    Height = x.Case.Height,
                    Length = x.Case.Length,
                    Width = x.Case.Width,
                    MaxSupplyLength = x.Case.SupplyLength,
                    MaxCoolerHeight = x.Case.CoolerLength,
                    MaxGPULength = x.Case.GPULength,
                    FormFactor = x.Case.FormFactor.Type,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                    QuantityInStock = x.Part.QuantityInStock
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new CaseData());
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
            if (AuthManager.CurrentUser != null)
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
        }
        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedItem = e.Row.Item as CaseData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newCase = new Case();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newCase.Model = editedItem.Model;
                            newCase.Height = editedItem.Height;
                            newCase.Length = editedItem.Length;
                            newCase.Width = editedItem.Width;
                            newCase.SupplyLength = editedItem.MaxSupplyLength;
                            newCase.CoolerLength = editedItem.MaxCoolerHeight;
                            newCase.GPULength = editedItem.MaxGPULength;

                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);
                            var newformFactor = context.FormFactor.FirstOrDefault(ff => ff.Type == editedItem.FormFactor);

                            if (newbrand != null) newCase.BrandID = newbrand.ID;
                            if (newformFactor != null) newCase.FormFactorID = newformFactor.ID;

                            context.Case.Add(newCase);
                            context.SaveChanges(); 

                            newPart.CaseID = newCase.ID;
                            newPart.Image = editedItem.Image;
                            if (editedItem.Image != null) newPart.Image = editedItem.Image;
                            if (editedItem.QuantityInStock > 0) newPart.QuantityInStock = editedItem.QuantityInStock;
                            else { newPart.QuantityInStock = 0; }

                            context.Part.Add(newPart);
                            context.SaveChanges();

                            newProduct.PartID = newPart.ID;
                            newProduct.Price = editedItem.Price;

                            context.Product.Add(newProduct);
                            context.SaveChanges();

                            editedItem.ID = newPart.ID;
                            editedItem.PartID = newProduct.PartID;

                            var items = dataGrid.ItemsSource as List<CaseData>;
                            if (items != null)
                            {
                                items.Add(new CaseData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новый корпус успешно добавлен",
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

                        var part = context.Part.FirstOrDefault(p => p.ID == editedItem.PartID);
                        var computerCase = context.Case.FirstOrDefault(c => c.ID == part.CaseID);

                        if (editedItem.Height <= 0 || editedItem.Length <= 0 || editedItem.Width <= 0)
                        {
                            MessageBox.Show("Габариты должны быть положительными числами",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.MaxSupplyLength <= 0 || editedItem.MaxCoolerHeight <= 0 || editedItem.MaxGPULength <= 0)
                        {
                            MessageBox.Show("Максимальные размеры компонентов должны быть положительными числами",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.MaxCoolerHeight >= editedItem.Height)
                        {
                            MessageBox.Show("Максимальная высота кулера не может быть больше высоты корпуса",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.MaxGPULength >= editedItem.Length)
                        {
                            MessageBox.Show("Максимальная длина видеокарты не может быть больше длины корпуса",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
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

                        var product = context.Product.FirstOrDefault(p => p.PartID == editedItem.ID);
                        if (product == null)
                        {
                            computerCase.BrandID = brand.ID;
                            computerCase.Model = editedItem.Model;
                            computerCase.Height = editedItem.Height;
                            computerCase.Length = editedItem.Length;
                            computerCase.Width = editedItem.Width;
                            computerCase.SupplyLength = editedItem.MaxSupplyLength;
                            computerCase.CoolerLength = editedItem.MaxCoolerHeight;
                            computerCase.GPULength = editedItem.MaxGPULength;
                            computerCase.FormFactorID = formFactor.ID;

                            context.Case.Add(computerCase);
                            context.SaveChanges();
                            return;
                        }

                        var oldPrice = product.Price;
                        product.Price = editedItem.Price;

                        computerCase.BrandID = brand.ID;
                        computerCase.Model = editedItem.Model;
                        computerCase.Height = editedItem.Height;
                        computerCase.Length = editedItem.Length;
                        computerCase.Width = editedItem.Width;
                        computerCase.SupplyLength = editedItem.MaxSupplyLength;
                        computerCase.CoolerLength = editedItem.MaxCoolerHeight;
                        computerCase.GPULength = editedItem.MaxGPULength;
                        computerCase.FormFactorID = formFactor.ID;

                        if (editedItem.Image != null)
                        {
                            part.Image = editedItem.Image;
                        }
                        part.QuantityInStock = editedItem.QuantityInStock;

                        var priceDifference = editedItem.Price - oldPrice;
                        if (priceDifference != 0)
                        {
                            var ordersToUpdate = context.Order
                                .Where(o => o.OrderItem.Any(oi => oi.ProductID == editedItem.ProductID && 
                                new List<string>{"Корзина", "Оформление"}.Contains(o.Status)))
                                .ToList();

                            foreach (var order in ordersToUpdate)
                            {
                                order.TotalPrice += priceDifference;
                            }
                        }

                        context.SaveChanges();
                        MessageBox.Show("Данные корпуса успешно обновлены",
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