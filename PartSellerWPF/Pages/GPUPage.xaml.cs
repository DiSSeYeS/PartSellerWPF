﻿using System;
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
    /// Логика взаимодействия для GPUPage.xaml
    /// </summary>
    public partial class GPUPage : Page
    {
        private static FilterParams filterParams;
        public GPUPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadGPUData(filterParams);
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
                        LoadGPUData(filterParams);
                    }
                    e.Handled = true;
                }
            }
        }

        private void LoadGPUData(object filterParams)
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

                var query = from c in context.GPU
                            join p in context.Part on c.ID equals p.GPUID
                            join prod in context.Product on p.ID equals prod.PartID
                            select new
                            {
                                GPU = c,
                                Part = p,
                                Product = prod
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.GPU.BrandID == brandId);
                    }

                    if (filters.MaxVoltage.HasValue)
                    {
                        int maxVoltage = filters.MaxVoltage.Value;
                        query = query.Where(x => x.GPU.Voltage <= maxVoltage);
                    }

                    if (filters.MaxVideoMemory.HasValue)
                    {
                        int maxVideoMemory = filters.MaxVideoMemory.Value;
                        query = query.Where(x => x.GPU.VideoMemoryGB <= maxVideoMemory);
                    }

                    if (filters.MaxMemoryFreq.HasValue)
                    {
                        int maxMemoryFreq = filters.MaxMemoryFreq.Value;
                        query = query.Where(x => x.GPU.MemoryFrequencyMHz <= maxMemoryFreq);
                    }

                    if (filters.MaxCoreFreq.HasValue)
                    {
                        int maxCoreFreq = filters.MaxCoreFreq.Value;
                        query = query.Where(x => x.GPU.CoreFrequencyMHz <= maxCoreFreq);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new GPUData
                {
                    Brand = x.GPU.Brand.Name,
                    Model = x.GPU.Model,
                    Height = (int)x.GPU.Height,
                    Length = (int)x.GPU.Length,
                    Width = (int)x.GPU.Width,
                    Voltage = x.GPU.Voltage,
                    VideoMemory = x.GPU.VideoMemoryGB,
                    MemoryFreq = (int)x.GPU.MemoryFrequencyMHz,
                    CoreFreq = (int)x.GPU.CoreFrequencyMHz,
                    ID = x.Part.ID,
                    PartID = x.Product.PartID,
                    ProductID = x.Product.ID,
                    Image = x.Part.Image,
                    Price = x.Product.Price,
                    QuantityInStock = x.Part.QuantityInStock
                }).ToList();

                if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
                {
                    result.Add(new GPUData());
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
                var editedItem = e.Row.Item as GPUData;
                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        bool isNewItem = false;

                        if (editedItem.ID == 0 && !string.IsNullOrWhiteSpace(editedItem.Model))
                        {
                            isNewItem = true;

                            var newGPU = new GPU();
                            var newPart = new Part();
                            var newProduct = new Product();

                            newGPU.Model = editedItem.Model;
                            newGPU.Voltage = int.Parse(editedItem.Voltage.ToString().Split().First());
                            newGPU.CoreFrequencyMHz = int.Parse(editedItem.CoreFreq.ToString().Split().First());
                            newGPU.VideoMemoryGB = int.Parse(editedItem.VideoMemory.ToString().Split().First());
                            newGPU.Height = int.Parse(editedItem.Height.ToString().Split().First());
                            newGPU.Width = int.Parse(editedItem.Width.ToString().Split().First());
                            newGPU.Length = int.Parse(editedItem.Length.ToString().Split().First());
                            newGPU.MemoryFrequencyMHz = int.Parse(editedItem.MemoryFreq.ToString().Split().First());

                            var newbrand = context.Brand.FirstOrDefault(b => b.Name == editedItem.Brand);

                            if (newbrand != null) newGPU.BrandID = newbrand.ID;

                            context.GPU.Add(newGPU);
                            context.SaveChanges();

                            newPart.GPUID = newGPU.ID;
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

                            var items = dataGrid.ItemsSource as List<GPUData>;
                            if (items != null)
                            {
                                items.Add(new GPUData());
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = items;
                            }

                            MessageBox.Show("Новая видеокарта успешно добавлена",
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

                        var product = context.Product.FirstOrDefault(p => p.PartID == editedItem.ID);
                        if (product == null)
                        {
                            MessageBox.Show("Продукт не найден", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        var part = context.Part.FirstOrDefault(p => p.ID == editedItem.PartID);
                        var gpu = context.GPU.FirstOrDefault(g => g.ID == part.GPUID);

                        if (int.Parse(editedItem.Height.ToString().Split().First()) <= 0 || int.Parse(editedItem.Length.ToString().Split().First()) <= 0 || int.Parse(editedItem.Width.ToString().Split().First()) <= 0)
                        {
                            MessageBox.Show("Габариты должны быть положительными числами",
                                         "Ошибка",
                                         MessageBoxButton.OK,
                                         MessageBoxImage.Error);
                            return;
                        }

                        if (int.Parse(editedItem.VideoMemory.ToString().Split().First()) <= 0 || int.Parse(editedItem.MemoryFreq.ToString().Split().First()) <= 0 || int.Parse(editedItem.CoreFreq.ToString().Split().First()) <= 0)
                        {
                            MessageBox.Show("Объем памяти и частоты должны быть положительными числами",
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

                        gpu.BrandID = brand.ID;
                        gpu.Model = editedItem.Model;
                        gpu.Height = int.Parse(editedItem.Height.ToString().Split().First());
                        gpu.Length = int.Parse(editedItem.Length.ToString().Split().First());
                        gpu.Width = int.Parse(editedItem.Width.ToString().Split().First());
                        gpu.Voltage = int.Parse(editedItem.Voltage.ToString().Split().First());
                        gpu.VideoMemoryGB = int.Parse(editedItem.VideoMemory.ToString().Split().First());
                        gpu.MemoryFrequencyMHz = int.Parse(editedItem.MemoryFreq.ToString().Split().First());
                        gpu.CoreFrequencyMHz = int.Parse(editedItem.CoreFreq.ToString().Split().First());

                        if (editedItem.Image != null)
                        {
                            part.Image = editedItem.Image;
                        }
                        part.QuantityInStock = editedItem.QuantityInStock;

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
                        MessageBox.Show("Данные видеокарты успешно обновлены", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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

