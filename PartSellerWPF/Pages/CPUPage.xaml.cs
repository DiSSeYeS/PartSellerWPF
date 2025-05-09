using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для CPUPage.xaml
    /// </summary>
    public partial class CPUPage : Page
    {
        private static FilterParams filterParams;
        public CPUPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadCPUData(filterParams);
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
                        LoadCPUData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }
        private void LoadCPUData(object filterParams)
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

                var query = from c in context.CPU
                            join p in context.Part on c.ID equals p.CPUID
                            join prod in context.Product on p.ID equals prod.PartID
                            join s in context.Socket on c.SocketID equals s.ID
                            select new
                            {
                                CPU = c,
                                Part = p,
                                Product = prod,
                                Socket = s
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.CPU.BrandID == brandId);
                    }

                    if (filters.SocketId.HasValue && filters.SocketId != -1)
                    {
                        int socketId = filters.SocketId.Value;
                        query = query.Where(x => x.CPU.SocketID == socketId);
                    }

                    if (filters.MaxVoltage.HasValue)
                    {
                        int maxVoltage = filters.MaxVoltage.Value;
                        query = query.Where(x => x.CPU.Voltage <= maxVoltage);
                    }

                    if (filters.MaxCores.HasValue)
                    {
                        int maxCores = filters.MaxCores.Value;
                        query = query.Where(x => x.CPU.Cores <= maxCores);
                    }

                    if (filters.MaxThreads.HasValue)
                    {
                        int maxThreads = filters.MaxThreads.Value;
                        query = query.Where(x => x.CPU.Threads <= maxThreads);
                    }

                    if (filters.MaxFreq.HasValue)
                    {
                        decimal maxFreq = filters.MaxFreq.Value;
                        query = query.Where(x => x.CPU.FrequencyGHz <= maxFreq);
                    }

                    if (filters.MaxL1.HasValue)
                    {
                        int maxL1 = filters.MaxL1.Value;
                        query = query.Where(x => x.CPU.L1 <= maxL1);
                    }

                    if (filters.MaxL2.HasValue)
                    {
                        int maxL2 = filters.MaxL2.Value;
                        query = query.Where(x => x.CPU.L2 <= maxL2);
                    }

                    if (filters.MaxMaxFreq.HasValue)
                    {
                        decimal maxMaxFreq = filters.MaxMaxFreq.Value;
                        query = query.Where(x => x.CPU.MaxFrequency <= maxMaxFreq);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new CPUData
                {
                    Brand = x.CPU.Brand.Name,
                    Model = x.CPU.Model,
                    Voltage = x.CPU.Voltage,
                    Socket = x.Socket.Name,
                    Cores = x.CPU.Cores,
                    Threads = x.CPU.Threads,
                    CoreFreq = (int)x.CPU.FrequencyGHz,
                    L1 = x.CPU.L1,
                    L2 = x.CPU.L2,
                    MaxFreq = (int)x.CPU.MaxFrequency,
                    HasTurboBoost = x.CPU.HasTurboBoost,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new CPUData());
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
                var editedItem = e.Row.Item as CPUData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newCPU = new CPU();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newCPU.Model = editedItem.Model;
                            newCPU.Voltage = editedItem.Voltage;
                            newCPU.Cores = editedItem.Cores;
                            newCPU.FrequencyGHz = editedItem.CoreFreq;
                            newCPU.Threads = editedItem.Threads;
                            newCPU.L1 = editedItem.L1;
                            newCPU.L2 = editedItem.L2;
                            newCPU.HasTurboBoost = editedItem.HasTurboBoost;
                            newCPU.MaxFrequency = editedItem.MaxFreq;

                            var newsocket = context.Socket.FirstOrDefault(ct => ct.Name == editedItem.Socket);
                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);

                            if (newbrand != null) newCPU.BrandID = newbrand.ID;
                            if (newsocket != null) newCPU.SocketID = newsocket.ID;

                            context.CPU.Add(newCPU);
                            context.SaveChanges();

                            newPart.CPUID = newCPU.ID;
                            newPart.Image = editedItem.Image;
                            if (editedItem.Image != null) newPart.Image = editedItem.Image;

                            context.Part.Add(newPart);
                            context.SaveChanges();

                            newProduct.PartID = newPart.ID;
                            newProduct.Price = editedItem.Price;

                            context.Product.Add(newProduct);
                            context.SaveChanges();

                            editedItem.ID = newPart.ID;
                            editedItem.PartID = newProduct.PartID;

                            var items = dataGrid.ItemsSource as List<CPUData>;
                            if (items != null)
                            {
                                items.Add(new CPUData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новый процессор успешно добавлен",
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

                        var product = context.Product.FirstOrDefault(p => p.PartID == editedItem.ID);
                        if (product == null)
                        {
                            MessageBox.Show("Продукт не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var part = context.Part.FirstOrDefault(p => p.ID == editedItem.PartID);
                        var cpu = context.CPU.FirstOrDefault(c => c.ID == part.CPUID);

                        if (editedItem.Voltage <= 0)
                        {
                            MessageBox.Show("Некорректное значение напряжения",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.Cores <= 0 || editedItem.Threads <= 0 || editedItem.CoreFreq <= 0)
                        {
                            MessageBox.Show("Количество ядер, потоков и частота должны быть положительными числами",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.L1 <= 0 || editedItem.L2 <= 0)
                        {
                            MessageBox.Show("Объем кэш-памяти должен быть положительным числом",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (editedItem.MaxFreq < editedItem.CoreFreq)
                        {
                            MessageBox.Show("Максимальная частота не может быть меньше базовой",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        var oldPrice = product.Price;
                        product.Price = editedItem.Price;

                        cpu.BrandID = brand.ID;
                        cpu.Model = editedItem.Model;
                        cpu.Voltage = editedItem.Voltage;
                        cpu.SocketID = socket.ID;
                        cpu.Cores = editedItem.Cores;
                        cpu.Threads = editedItem.Threads;
                        cpu.FrequencyGHz = editedItem.CoreFreq;
                        cpu.L1 = editedItem.L1;
                        cpu.L2 = editedItem.L2;
                        cpu.MaxFrequency = editedItem.MaxFreq;
                        cpu.HasTurboBoost = editedItem.HasTurboBoost;

                        if (editedItem.Image != null)
                        {
                            part.Image = editedItem.Image;
                        }

                        var priceDifference = editedItem.Price - oldPrice;
                        if (priceDifference != 0)
                        {
                            var ordersToUpdate = context.Order
                                .Where(o => o.OrderItem.Any(oi => oi.ProductID == editedItem.ID))
                                .ToList();

                            foreach (var order in ordersToUpdate)
                            {
                                order.TotalPrice += priceDifference;
                            }
                        }

                        context.SaveChanges();
                        MessageBox.Show("Данные процессора успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

    

