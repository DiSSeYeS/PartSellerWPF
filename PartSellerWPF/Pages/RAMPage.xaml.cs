using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// Логика взаимодействия для RAMPage.xaml
    /// </summary>
    public partial class RAMPage : Page
    {
        private static FilterParams filterParams;
        public RAMPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadRAMData(filterParams);
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
                        LoadRAMData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }

        private void LoadRAMData(object filterParams)
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

                var query = from c in context.RAM
                            join p in context.Part on c.ID equals p.RAMID
                            join prod in context.Product on p.ID equals prod.PartID
                            join rt in context.RAMType on c.RAMTypeID equals rt.ID
                            select new
                            {
                                RAM = c,
                                Part = p,
                                Product = prod,
                                RAMType = rt
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.RAM.BrandID == brandId);
                    }

                    if (filters.RamTypeId.HasValue && filters.RamTypeId != -1)
                    {
                        int ramTypeId = filters.RamTypeId.Value;
                        query = query.Where(x => x.RAM.RAMTypeID == ramTypeId);
                    }

                    if (filters.MaxRAMGB.HasValue)
                        query = query.Where(x => x.RAM.MemoryCountGB <= filters.MaxRAMGB.Value);

                    if (filters.MaxFreq.HasValue)
                        query = query.Where(x => x.RAM.MemoryFrequencyMHz <= filters.MaxFreq.Value);

                    if (filters.MaxCount.HasValue)
                        query = query.Where(x => x.RAM.Count <= filters.MaxCount.Value);

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new RAMData
                {
                    Brand = x.RAM.Brand.Name,
                    Model = x.RAM.Model,
                    RAMCount = x.RAM.MemoryCountGB,
                    RAMFreq = x.RAM.MemoryFrequencyMHz,
                    RAMQuantity = x.RAM.Count,
                    RAMType = x.RAMType.Type,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                    QuantityInStock = x.Part.QuantityInStock
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new RAMData());
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
                var editedItem = e.Row.Item as RAMData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newRAM = new RAM();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newRAM.Model = editedItem.Model;
                            newRAM.MemoryCountGB = int.Parse(editedItem.RAMCount.ToString().Split().First());
                            newRAM.MemoryFrequencyMHz = int.Parse(editedItem.RAMFreq.ToString().Split().First());
                            newRAM.Count = int.Parse(editedItem.RAMQuantity.ToString().Split().First());

                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);
                            var newramtype = context.RAMType.FirstOrDefault(rt => rt.Type == editedItem.RAMType);

                            if (newbrand != null) newRAM.BrandID = newbrand.ID;
                            if (newramtype != null) newRAM.RAMTypeID = newramtype.ID;

                            context.RAM.Add(newRAM);
                            context.SaveChanges();

                            newPart.RAMID = newRAM.ID;
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

                            var items = dataGrid.ItemsSource as List<RAMData>;
                            if (items != null)
                            {
                                items.Add(new RAMData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новая оперативная память успешно добавлена",
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

                        var ramType = context.RAMType.FirstOrDefault(rt => rt.Type == editedItem.RAMType);
                        if (ramType == null)
                        {
                            MessageBox.Show($"Тип памяти '{editedItem.RAMType}' не найден в базе данных",
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
                        var ram = context.RAM.FirstOrDefault(r => r.ID == part.RAMID);

                        var oldPrice = int.Parse(product.Price.ToString().Split().First());
                        product.Price = int.Parse(editedItem.Price.ToString().Split().First());

                        ram.BrandID = brand.ID;
                        ram.RAMTypeID = ramType.ID;
                        ram.Model = editedItem.Model;
                        ram.MemoryCountGB = int.Parse(editedItem.RAMCount.ToString().Split().First());
                        ram.MemoryFrequencyMHz = int.Parse(editedItem.RAMFreq.ToString().Split().First());
                        ram.Count = int.Parse(editedItem.RAMQuantity.ToString().Split().First());

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
