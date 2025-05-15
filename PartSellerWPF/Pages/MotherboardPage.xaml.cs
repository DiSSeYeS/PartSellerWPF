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
    /// Логика взаимодействия для MotherboardPage.xaml
    /// </summary>
    public partial class MotherboardPage : Page
    {
        private static FilterParams filterParams;
        public MotherboardPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadMotherboardData(filterParams);
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
                        LoadMotherboardData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }

        private void LoadMotherboardData(object filterParams)
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

                var query = from c in context.Motherboard
                            join p in context.Part on c.ID equals p.MotherboardID
                            join prod in context.Product on p.ID equals prod.PartID
                            join ch in context.Chipset on c.ChipsetID equals ch.ID
                            join s in context.Socket on c.SocketID equals s.ID
                            join r in context.RAMType on c.RAMTypeID equals r.ID
                            join ff in context.FormFactor on c.FormFactorID equals ff.ID
                            select new
                            {
                                Motherboard = c,
                                Part = p,
                                Product = prod,
                                Chipset = ch,
                                Socket = s,
                                RAMType = r,
                                FormFactor = ff
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Motherboard.BrandID == brandId);
                    }

                    if (filters.SocketId.HasValue && filters.SocketId != -1)
                    {
                        int socketId = filters.SocketId.Value;
                        query = query.Where(x => x.Motherboard.SocketID == socketId);
                    }
                    if (filters.ChipsetId.HasValue && filters.ChipsetId != -1)
                    {
                        int chipsetId = filters.ChipsetId.Value;
                        query = query.Where(x => x.Motherboard.ChipsetID == chipsetId);
                    }
                    if (filters.RamTypeId.HasValue && filters.RamTypeId != -1)
                    {
                        int ramTypeId = filters.RamTypeId.Value;
                        query = query.Where(x => x.Motherboard.RAMTypeID == ramTypeId);
                    }
                    if (filters.MaxRamSlots.HasValue && filters.MaxRamSlots != -1)
                    {
                        int ramSlots = filters.MaxRamSlots.Value;
                        query = query.Where(x => x.Motherboard.RAMSlots <= ramSlots);
                    }
                    if (filters.MaxRAMGB.HasValue && filters.MaxRAMGB != -1)
                    {
                        int maxRAMGB = filters.MaxRAMGB.Value;
                        query = query.Where(x => x.Motherboard.MaxRAMCountGB <= maxRAMGB);
                    }
                    if (filters.MaxMemoryFreq.HasValue && filters.MaxMemoryFreq != -1)
                    {
                        int maxMemoryFreq = filters.MaxMemoryFreq.Value;
                        query = query.Where(x => x.Motherboard.MaxRAMFrequencyMHz <= maxMemoryFreq);
                    }
                    if (filters.MaxWidth.HasValue && filters.MaxWidth != -1)
                    {
                        decimal width = filters.MaxWidth.Value;
                        query = query.Where(x => x.Motherboard.Width <= width);
                    }
                    if (filters.MaxHeight.HasValue && filters.MaxHeight != -1)
                    {
                        decimal height = filters.MaxHeight.Value;
                        query = query.Where(x => x.Motherboard.Height <= height);
                    }
                    if (filters.MaxSataSlots.HasValue && filters.MaxSataSlots != -1)
                    {
                        int maxSataSlots = filters.MaxSataSlots.Value;
                        query = query.Where(x => x.Motherboard.SATASlots <= maxSataSlots);
                    }
                    if (filters.MaxM2Slots.HasValue && filters.MaxM2Slots != -1)
                    {
                        int m2Slots = filters.MaxM2Slots.Value;
                        query = query.Where(x => x.Motherboard.M2Slots <= m2Slots);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new MotherboardData
                {
                    Brand = x.Motherboard.Brand.Name,
                    Model = x.Motherboard.Model,
                    Height = x.Motherboard.Height,
                    Width = x.Motherboard.Width,
                    Socket = x.Socket.Name,
                    Chipset = x.Chipset.Name,
                    RAMType = x.RAMType.Type,
                    RAMSlots = x.Motherboard.RAMSlots,
                    MaxRAMCount = x.Motherboard.MaxRAMCountGB,
                    MaxRAMFreq = x.Motherboard.MaxRAMFrequencyMHz,
                    SATASlots = x.Motherboard.SATASlots,
                    M2Slots = x.Motherboard.M2Slots,
                    NVMe = x.Motherboard.NVMe,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                    FormFactor = x.FormFactor.Type
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new MotherboardData());
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
                var editedItem = e.Row.Item as MotherboardData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newMotherboard = new Motherboard();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newMotherboard.Model = editedItem.Model;
                            newMotherboard.RAMSlots = int.Parse(editedItem.RAMSlots.ToString().Split().First());
                            newMotherboard.MaxRAMCountGB = int.Parse(editedItem.MaxRAMCount.ToString().Split().First());
                            newMotherboard.MaxRAMFrequencyMHz = int.Parse(editedItem.MaxRAMFreq.ToString().Split().First());
                            newMotherboard.Width = int.Parse(editedItem.Width.ToString().Split().First());
                            newMotherboard.Height = (int)int.Parse(editedItem.Height.ToString().Split().First());
                            newMotherboard.SATASlots = int.Parse(editedItem.SATASlots.ToString().Split().First());
                            newMotherboard.M2Slots = int.Parse(editedItem.M2Slots.ToString().Split().First());
                            newMotherboard.NVMe = editedItem.NVMe;

                            var newsocket = context.Socket.FirstOrDefault(ct => ct.Name == editedItem.Socket);
                            var newchipset = context.Chipset.FirstOrDefault(c => c.Name == editedItem.Chipset);
                            var newramtype = context.RAMType.FirstOrDefault(rt => rt.Type == editedItem.RAMType);
                            var newformfactor = context.FormFactor.FirstOrDefault(ff => ff.Type == editedItem.FormFactor);
                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);

                            if (newbrand != null) newMotherboard.BrandID = newbrand.ID;
                            if (newformfactor != null) newMotherboard.FormFactorID = newformfactor.ID;
                            if (newchipset != null) newMotherboard.ChipsetID = newchipset.ID;
                            if (newramtype != null) newMotherboard.RAMTypeID = newramtype.ID;
                            if (newsocket != null) newMotherboard.SocketID = newsocket.ID;

                            context.Motherboard.Add(newMotherboard);
                            context.SaveChanges();

                            newPart.MotherboardID = newMotherboard.ID;
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

                            var items = dataGrid.ItemsSource as List<MotherboardData>;
                            if (items != null)
                            {
                                items.Add(new MotherboardData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новая мат. плата успешно добавлена",
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

                        var socket = context.Socket.FirstOrDefault(s => s.Name == editedItem.Socket);
                        if (socket == null)
                        {
                            MessageBox.Show($"Сокет '{editedItem.Socket}' не найден в базе данных",
                                          "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
                            return;
                        }

                        var chipset = context.Chipset.FirstOrDefault(c => c.Name == editedItem.Chipset);
                        if (chipset == null)
                        {
                            MessageBox.Show($"Чипсет '{editedItem.Chipset}' не найден в базе данных",
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

                        var part = context.Part.FirstOrDefault(p => p.ID == editedItem.PartID);
                        var motherboard = context.Motherboard.FirstOrDefault(m => m.ID == part.MotherboardID);

                        var oldPrice = product.Price;
                        product.Price = int.Parse(editedItem.Price.ToString().Split().First());

                        motherboard.BrandID = brand.ID;
                        motherboard.Model = editedItem.Model;
                        motherboard.Height = int.Parse(editedItem.Height.ToString().Split().First());
                        motherboard.Width = int.Parse(editedItem.Width.ToString().Split().First());
                        motherboard.SocketID = socket.ID;
                        motherboard.ChipsetID = chipset.ID;
                        motherboard.RAMTypeID = ramType.ID;
                        motherboard.RAMSlots = int.Parse(editedItem.RAMSlots.ToString().Split().First());
                        motherboard.MaxRAMCountGB = int.Parse(editedItem.MaxRAMCount.ToString().Split().First());
                        motherboard.MaxRAMFrequencyMHz = int.Parse(editedItem.MaxRAMFreq.ToString().Split().First());
                        motherboard.SATASlots = int.Parse(editedItem.SATASlots.ToString().Split().First());
                        motherboard.M2Slots = int.Parse(editedItem.M2Slots.ToString().Split().First());
                        motherboard.NVMe = int.Parse(editedItem.NVMe.ToString().Split().First());
                        motherboard.FormFactorID = formFactor.ID;

                        if (editedItem.Image != null)
                        {
                            part.Image = editedItem.Image;
                        }

                        var priceDifference = int.Parse(editedItem.Price.ToString().Split().First()) - oldPrice;
                        if (priceDifference != 0)
                        {
                            var ordersToUpdate = context.Order
                                .Where(o => o.OrderItem.Any(oi => oi.ProductID == int.Parse(editedItem.ProductID.ToString().Split().First()) &&
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

