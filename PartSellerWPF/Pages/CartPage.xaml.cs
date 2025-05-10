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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        private static int staticOrderId;
        public CartPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
            {
                dataGrid.CanUserAddRows = true;
                dataGrid.CanUserDeleteRows = true;
            }

            var context = Entities.GetContext();
            var components = new List<CartItemsDto>();

            var currentOrder = context.Order
                            .FirstOrDefault(o => o.UserId == AuthManager.CurrentUser.ID &&
                            o.Status != "Завершен");

            if (currentOrder == null)
            {
                currentOrder = new Order
                {
                    UserId = AuthManager.CurrentUser.ID,
                    Date = DateTime.Now,
                    Status = "Корзина",
                    TotalPrice = 0
                };
                context.Order.Add(currentOrder);
                context.SaveChanges();
            }

            var cpuComponents = from cp in context.CPU
                                join b in context.Brand on cp.BrandID equals b.ID
                                join p in context.Part on cp.ID equals p.CPUID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = cp.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(cpuComponents);

            var gpuComponents = from gp in context.GPU
                                join b in context.Brand on gp.BrandID equals b.ID
                                join p in context.Part on gp.ID equals p.GPUID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = gp.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(gpuComponents);

            var supplyComponents = from s in context.Supply
                                join b in context.Brand on s.BrandID equals b.ID
                                join p in context.Part on s.ID equals p.SupplyID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = s.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(supplyComponents);

            var diskComponents = from d in context.Disk
                                join b in context.Brand on d.BrandID equals b.ID
                                join p in context.Part on d.ID equals p.DiskID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = d.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(diskComponents);

            var coolingComponents = from c in context.Cooling
                                join b in context.Brand on c.BrandID equals b.ID
                                join p in context.Part on c.ID equals p.CoolingID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = c.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(coolingComponents);

            var motherboardComponents = from m in context.Motherboard
                                join b in context.Brand on m.BrandID equals b.ID
                                join p in context.Part on m.ID equals p.MotherboardID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = m.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(motherboardComponents);

            var ramComponents = from r in context.RAM
                                join b in context.Brand on r.BrandID equals b.ID
                                join p in context.Part on r.ID equals p.RAMID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = r.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(ramComponents);

            var caseСomponents = from c in context.Case
                                join b in context.Brand on c.BrandID equals b.ID
                                join p in context.Part on c.ID equals p.CaseID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                where o.Status != "Завершен"
                                select new CartItemsDto
                                {
                                    OrderId = o.ID,
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = c.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(caseСomponents);


            staticOrderId = context.Order.Where(x => x.Status != "Завершен" && x.UserId == AuthManager.CurrentUser.ID).FirstOrDefault().ID;

            dataGrid.ItemsSource = components;
            totalPriceText.Text = context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().TotalPrice.ToString();
            orderIdText.Text = staticOrderId.ToString();
            // btnCheckout.Visibility = context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().Status != "Завершен" ? Visibility.Visible : Visibility.Collapsed;
            //btnDelete.Visibility = context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().StatusVisibility.Visible : Visibility.Collapsed;

        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            var context = Entities.GetContext();

            var order = from o in context.Order
                        join oi in context.OrderItem on o.ID equals oi.OrderID
                        join pr in context.Product on oi.ProductID equals pr.ID
                        join p in context.Part on pr.PartID equals p.ID
                        where o.UserId == AuthManager.CurrentUser.ID
                        where o.Status != "Завершен"
                        select new
                        {
                            Order = o,
                            OrderItem = oi,
                            Product = pr,
                            Part = p
                        };

            NavigationService.Navigate(new PaymentPage(order.FirstOrDefault().Order.ID));
        }

        private void chkIsAssembly_Checked(object sender, RoutedEventArgs e)
        {
            CheckCompatibility();
        }
        private void chkIsAssembly_Unchecked(object sender, RoutedEventArgs e)
        {
            compatibilityResult.Text = "";
        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            Funcs.ExtendCell(dataGrid);
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var context = Entities.GetContext();

            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите удаляемый компонент");
                return;
            }

            if (dataGrid.SelectedItem is CartItemsDto selectedItem)
            {
                int orderItemId = selectedItem.OrderItemId;

                var order = (from oi in context.OrderItem
                            join o in context.Order on oi.OrderID equals o.ID
                            where oi.ID == orderItemId
                            select new
                            {
                                Order = o,
                                OrderItem = oi
                            });

                var item = context.OrderItem.FirstOrDefault(x => x.ID == orderItemId);

                decimal price = selectedItem.Price * selectedItem.Quantity;

                order.Where(x => x.Order.ID == staticOrderId).FirstOrDefault().Order.TotalPrice -= price;
                context.OrderItem.Remove(item);

                context.SaveChanges();
                LoadData();
            }
        }

        private void CheckCompatibility()
        {
            var context = Entities.GetContext();
            var compatibilityIssues = new List<string>();

            try
            {
                var order = from o in context.Order
                            join oi in context.OrderItem on o.ID equals oi.OrderID
                            join pr in context.Product on oi.ProductID equals pr.ID
                            join p in context.Part on pr.PartID equals p.ID
                            where o.UserId == AuthManager.CurrentUser.ID
                            where o.ID == staticOrderId
                            select new
                            {
                                Order = o,
                                OrderItem = oi,
                                Product = pr,
                                Part = p
                            };

                var motherboard = from o in order
                                  join m in context.Motherboard on o.Part.MotherboardID equals m.ID
                                  join s in context.Socket on m.SocketID equals s.ID
                                  join rt in context.RAMType on m.RAMTypeID equals rt.ID
                                  join c in context.Chipset on m.ChipsetID equals c.ID
                                  select new
                                  {
                                     Brand = m.Brand.Name,
                                     m.Model,
                                     m.SocketID,
                                     m.MaxRAMCountGB,
                                     m.MaxRAMFrequencyMHz,
                                     m.RAMSlots,
                                     m.RAMTypeID,
                                     m.M2Slots,
                                     m.SATASlots,
                                     m.FormFactorID,
                                     m.NVMe
                                  };

                var cpu = from o in order
                          join c in context.CPU on o.Part.CPUID equals c.ID
                          select new
                          {
                              Brand = c.Brand.Name,
                              c.Model,
                              c.SocketID,
                              c.Voltage
                          };

                var gpu = from o in order
                          join g in context.GPU on o.Part.GPUID equals g.ID
                          select new
                          {
                              Brand = g.Brand.Name,
                              g.Model,
                              g.Length,
                              g.Voltage,
                              g.Width,
                              g.Height
                          };

                var cases = from o in order
                            join c in context.Case on o.Part.CaseID equals c.ID

                            select new
                            {
                                Brand = c.Brand.Name,
                                c.Model,
                                c.CoolerLength,
                                c.GPULength,
                                c.SupplyLength,
                                SupportedMotherboardFormFactorIds = context.SupportedFormFactorMotherboard
                                    .Where(sffm => sffm.CaseID == c.ID)
                                    .Select(sffm => sffm.FormFactorID)
                                    .ToList(),
                                SupportedSupplyFormFactorIds = context.SupportedFormFactorSupply
                                    .Where(sffs => sffs.CaseID == c.ID)
                                    .Select(sffs => sffs.FormFactorID)
                                    .ToList()
                            };

                var disk = from o in order
                           join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                           join d in context.Disk on o.Part.DiskID equals d.ID

                           select new
                           {
                               Brand = d.Brand.Name,
                               d.Model,
                               oi.Quantity,
                               d.DiskTypeID,
                           };

                var ram = from o in order
                          join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                          join r in context.RAM on o.Part.RAMID equals r.ID

                          select new
                          {
                              Brand = r.Brand.Name,
                              oi.Quantity,
                              r.Model,
                              r.Count,
                              r.MemoryCountGB,
                              r.MemoryFrequencyMHz,
                              r.RAMTypeID,
                          };

                var cooling = from o in order

                              join c in context.Cooling on o.Part.CoolingID equals c.ID
                              join ss in context.SupportedSockets on c.ID equals ss.CoolerID
                              select new
                              {
                                  Brand = c.Brand.Name,
                                  c.Model,
                                  c.CoolerTypeID,
                                  c.Width,
                                  c.Length,
                                  c.Height,
                                  ss.SocketID
                              };

                var supply = from o in order
                             join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                             join s in context.Supply on o.Part.SupplyID equals s.ID

                             select new
                             {
                                 Brand = s.Brand.Name,
                                 s.Model,
                                 s.FormFactorID,
                                 s.Height,
                                 s.Length,
                                 s.Width,
                                 s.Wattage,
                             };


                foreach (var item in cpu)
                {
                    if (motherboard.Count() != 0 && motherboard.FirstOrDefault().SocketID != item.SocketID)
                    {
                        compatibilityIssues.Add($"❌ Сокет процессора отличается \nот сокета материнской платы");
                    }
                }

                
                foreach (var item in gpu)
                {
                    if (cases.Count() != 0 && cases.FirstOrDefault().GPULength < item.Length)
                    {
                        compatibilityIssues.Add($"❌ Видеокарта не поместится в корпус");
                    }
                }

                foreach (var item in ram)
                {
                    if (motherboard.Count() != 0 && motherboard.FirstOrDefault().MaxRAMFrequencyMHz < item.MemoryFrequencyMHz)
                    {
                        compatibilityIssues.Add($"❌ Частота оперативной памяти больше \nмаксимальной частоты материнской платы");
                    }
                    if (motherboard.Count() != 0 && motherboard.FirstOrDefault().MaxRAMCountGB < (item.MemoryCountGB * item.Quantity))
                    {
                        compatibilityIssues.Add($"❌ Количество оперативной памяти \nпревышает максиамльное количество \nоперативной памяти мат. платы");
                    }
                    if (motherboard.Count() != 0 && motherboard.FirstOrDefault().RAMSlots < (item.Count * item.Quantity))
                    {
                        compatibilityIssues.Add($"❌ Количество плашек оперативной памяти\nпревышает количество слотов на мат. плате");
                    }
                    if (motherboard.Count() != 0 && motherboard.FirstOrDefault().RAMTypeID != item.RAMTypeID)
                    {
                        compatibilityIssues.Add($"❌ Тип оперативной памяти отличается от\nтипа оперативной памяти на мат.плате");
                    }
                }

                foreach (var item in motherboard)
                {
                    if (cases.Count() != 0 && !cases.FirstOrDefault().SupportedMotherboardFormFactorIds.Contains((int)item.FormFactorID))
                    {
                        compatibilityIssues.Add($"❌ Корпус не поддерживает форм-фактор\nматеринской платы");
                    }
                }

                foreach (var item in supply)
                {
                    if (gpu.Count() != 0 && cpu.Count() != 0)
                    {
                        if (item.Wattage < (gpu.FirstOrDefault().Voltage + cpu.FirstOrDefault().Voltage + 150))
                        {
                            compatibilityIssues.Add($"❌ Недостаточно напряжения в блоке питания");
                        }
                    }
                    else if (gpu.Count() != 0)
                    {
                        if (item.Wattage < (gpu.FirstOrDefault().Voltage + 150))
                        {
                            compatibilityIssues.Add($"❌ Недостаточно напряжения в блоке питания");
                        } 
                    }
                    else if (cpu.Count() != 0)
                    {
                        if (item.Wattage < (cpu.FirstOrDefault().Voltage + 150))
                        {
                            compatibilityIssues.Add($"❌ Недостаточно напряжения в блоке питания");
                        }
                    }

                    if (cases.Count() != 0 && !cases.FirstOrDefault().SupportedSupplyFormFactorIds.Contains((int)item.FormFactorID))
                    {
                        compatibilityIssues.Add($"❌ Корпус не поддерживает форм-фактор\nблока питания");
                    }
                }

                foreach (var item in disk)
                {
                    if (motherboard.Count() != 0 && item.DiskTypeID == 2 && motherboard.FirstOrDefault().SATASlots < item.Quantity)
                    {
                        compatibilityIssues.Add($"❌ Кол-во SATA-дисков превышает кол-во\nSATA-слотов в материнской плате");
                    }
                    if (motherboard.Count() != 0 && item.DiskTypeID == 1 && motherboard.FirstOrDefault().M2Slots < item.Quantity)
                    {
                        compatibilityIssues.Add($"❌ Кол-во M2-дисков превышает кол-во\nM2-слотов в материнской плате");
                    }
                }

                foreach (var item in cooling)
                {
                    if (cases.Count() != 0 && item.CoolerTypeID == 1 && item.Height > cases.FirstOrDefault().CoolerLength)
                    {
                        compatibilityIssues.Add($"❌ Башня не поместится в корпус");
                    }

                    if (item.CoolerTypeID == 3 && item.Width > 140)
                    {
                        compatibilityIssues.Add($"❌ Корпусные вентиляторы слишком большие");
                    }
                }

                if (compatibilityIssues.Any())
                {
                    compatibilityResult.Text = "Проблемы совместимости:\n" + string.Join("\n", compatibilityIssues);
                    compatibilityResult.Foreground = Brushes.Red;
                }
                else
                {
                    compatibilityResult.Text = "✅ Все компоненты совместимы";
                    compatibilityResult.Foreground = Brushes.Green;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка проверки совместимости: {ex.Message}");
            }
        }

        private void btnPlusOne_Click(object sender, RoutedEventArgs e)
        {
            var context = Entities.GetContext();

            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите удаляемый компонент");
                return;
            }

            if (dataGrid.SelectedItem is CartItemsDto selectedItem)
            {
                int orderItemId = selectedItem.OrderItemId;

                var order = (from oi in context.OrderItem
                             join o in context.Order on oi.OrderID equals o.ID
                             where oi.ID == orderItemId
                             select new
                             {
                                 Order = o,
                                 OrderItem = oi
                             });

                var item = context.OrderItem.FirstOrDefault(x => x.ID == orderItemId);

                decimal price = selectedItem.Price;

                order.FirstOrDefault().Order.TotalPrice += price;
                order.FirstOrDefault().OrderItem.Quantity++;

                context.SaveChanges();
                LoadData();
            }
        }

        private void btnMinusOne_Click(object sender, RoutedEventArgs e)
        {
            var context = Entities.GetContext();

            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите удаляемый компонент");
                return;
            }

            if (dataGrid.SelectedItem is CartItemsDto selectedItem)
            {
                int orderItemId = selectedItem.OrderItemId;

                var order = (from oi in context.OrderItem
                             join o in context.Order on oi.OrderID equals o.ID
                             where oi.ID == orderItemId
                             select new
                             {
                                 Order = o,
                                 OrderItem = oi
                             });

                var item = context.OrderItem.FirstOrDefault(x => x.ID == orderItemId);

                decimal price = selectedItem.Price;

                order.FirstOrDefault().Order.TotalPrice -= price;
                order.FirstOrDefault().OrderItem.Quantity--;

                if (order.FirstOrDefault().OrderItem.Quantity == 0)
                {
                    context.OrderItem.Remove(item);
                }

                context.SaveChanges();
                LoadData();
            }
        }
    }
}
