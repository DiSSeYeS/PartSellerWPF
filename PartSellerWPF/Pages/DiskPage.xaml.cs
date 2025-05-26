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
    /// Логика взаимодействия для DiskPage.xaml
    /// </summary>
    public partial class DiskPage : Page
    {
        private static FilterParams filterParams;
        public DiskPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadDiskData(filterParams);
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
                        LoadDiskData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }
        private void LoadDiskData(object filterParams)
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

                var query = from c in context.Disk
                            join p in context.Part on c.ID equals p.DiskID
                            join prod in context.Product on p.ID equals prod.PartID
                            join dt in context.DiskType on c.DiskTypeID equals dt.ID
                            select new
                            {
                                Disk = c,
                                Part = p,
                                Product = prod,
                                DiskType = dt
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Disk.BrandID == brandId);
                    }

                    if (filters.DiskTypeId.HasValue && filters.DiskTypeId != -1)
                    {
                        int diskTypeId = filters.DiskTypeId.Value;
                        query = query.Where(x => x.Disk.DiskTypeID == diskTypeId);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new DiskData
                {
                    Brand = x.Disk.Brand.Name,
                    Model = x.Disk.Model,
                    MemoryCount = x.Disk.Space,
                    Type = x.DiskType.Type,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                    QuantityInStock = x.Part.QuantityInStock
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new DiskData());
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
                var editedItem = e.Row.Item as DiskData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newDisk = new Disk();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newDisk.Model = editedItem.Model;
                            newDisk.Space = int.Parse(editedItem.MemoryCount.ToString().Split().First());

                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);
                            var newtype = context.DiskType.FirstOrDefault(dt => dt.Type == editedItem.Type);

                            if (newbrand != null) newDisk.BrandID = newbrand.ID;
                            if (newtype != null) newDisk.DiskTypeID = newtype.ID;

                            context.SaveChanges();

                            newPart.DiskID = newDisk.ID;
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

                            var items = dataGrid.ItemsSource as List<DiskData>;
                            if (items != null)
                            {
                                items.Add(new DiskData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новый диск успешно добавлен",
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

                        var diskType = context.DiskType.FirstOrDefault(dt => dt.Type == editedItem.Type);
                        if (diskType == null)
                        {
                            MessageBox.Show($"Тип диска '{editedItem.Type}' не найден в базе данных",
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
                        var disk = context.Disk.FirstOrDefault(d => d.ID == part.DiskID);

                        if (editedItem.MemoryCount <= 0)
                        {
                            MessageBox.Show("Объем памяти должен быть положительным числом",
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

                        var oldPrice = product.Price;
                        product.Price = int.Parse(editedItem.Price.ToString().Split().First());

                        disk.BrandID = brand.ID;
                        disk.Model = editedItem.Model;
                        disk.Space = int.Parse(editedItem.MemoryCount.ToString().Split().First());
                        disk.DiskTypeID = diskType.ID;

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
                        MessageBox.Show("Данные диска успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

