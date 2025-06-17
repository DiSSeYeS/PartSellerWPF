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
                            o.Status == "Корзина");

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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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
                                where o.Status == "Корзина"
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


            staticOrderId = context.Order.Where(x => x.Status == "Корзина" && x.UserId == AuthManager.CurrentUser.ID).FirstOrDefault().ID;

            dataGrid.ItemsSource = components;
            totalPriceText.Text = context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().TotalPrice.ToString();
            orderIdText.Text = staticOrderId.ToString();
            btnCheckout.Visibility = context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().TotalPrice != 0 ? Visibility.Visible : Visibility.Collapsed;
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
                        where o.Status == "Корзина"
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
            var compatibilityIssues = new HashSet<string>();

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

                var motherboard = (from o in order
                                  join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                                  join m in context.Motherboard on oi.Product.Part.MotherboardID equals m.ID
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
                                  }).ToList();

                var cpu = (from o in order
                          join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                          join c in context.CPU on oi.Product.Part.CPUID equals c.ID
                          select new
                          {
                              Brand = c.Brand.Name,
                              c.Model,
                              c.SocketID,
                              c.Voltage
                          }).ToList();

                var gpu = (from o in order
                          join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                          join g in context.GPU on oi.Product.Part.GPUID equals g.ID
                          select new
                          {
                              Brand = g.Brand.Name,
                              g.Model,
                              g.Length,
                              g.Voltage,
                              g.Width,
                              g.Height
                          }).ToList();

                var cases = (from o in order
                            join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                            join c in context.Case on oi.Product.Part.CaseID equals c.ID
                            select new
                            {
                                Brand = c.Brand.Name,
                                Name = c.Model,
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
                            }).ToList();

                var disk = (from o in order
                           join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                           join d in context.Disk on oi.Product.Part.DiskID equals d.ID

                           select new
                           {
                               Brand = d.Brand.Name,
                               d.Model,
                               oi.Quantity,
                               d.DiskTypeID,
                           }).ToList();

                var ram = (from o in order
                          join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                          join r in context.RAM on oi.Product.Part.RAMID equals r.ID

                          select new
                          {
                              Brand = r.Brand.Name,
                              oi.Quantity,
                              r.Model,
                              r.Count,
                              r.MemoryCountGB,
                              r.MemoryFrequencyMHz,
                              r.RAMTypeID,
                          }).ToList();

                var cooling = (from o in order
                              join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                              join c in context.Cooling on oi.Product.Part.CoolingID equals c.ID
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
                              }).ToList();

                var supply = (from o in order
                             join oi in context.OrderItem on o.Order.ID equals oi.OrderID
                             join s in context.Supply on oi.Product.Part.SupplyID equals s.ID

                             select new
                             {
                                 Brand = s.Brand.Name,
                                 s.Model,
                                 s.FormFactorID,
                                 s.Height,
                                 s.Length,
                                 s.Width,
                                 s.Wattage,
                             }).ToList();


                foreach (var item in cpu)
                {
                    foreach (var mt in motherboard)
                    {
                        if (mt.SocketID != item.SocketID)
                        {
                            compatibilityIssues.Add($"❌ Сокет процессора \n{item.Model} - {context.Socket.FirstOrDefault(x => x.ID == item.SocketID).Name} \nотличается от сокета мат.платы \n{mt.Model} - {context.Socket.FirstOrDefault(x => x.ID == mt.SocketID).Name}");
                        }
                    }
                }
                
                foreach (var item in gpu)
                {
                    foreach (var c in cases)
                    {
                        if (c.GPULength < item.Length)
                        {
                            compatibilityIssues.Add($"❌ Видеокарта {item.Model}\n не поместится в корпус {c.Model}\nДлина видеокарты {item.Length} мм\nМакс.длина {c.GPULength} мм");
                        }
                    }
                }

                foreach (var item in ram)
                {
                    if (motherboard.Count() != 0)
                    {
                        foreach (var mt in motherboard)
                        {
                            if (mt.MaxRAMFrequencyMHz < item.MemoryFrequencyMHz)
                            {
                                compatibilityIssues.Add($"❌ Частота оперативной памяти \n{item.Model} - {item.MemoryFrequencyMHz} МГц больше \nмаксимальной частоты \nматеринской платы {mt.Model} - {mt.MaxRAMFrequencyMHz} МГц");
                            }
                            if (mt.MaxRAMCountGB < (item.MemoryCountGB * item.Quantity))
                            {
                                compatibilityIssues.Add($"❌ Объем оперативной памяти \n{item.Model} - {item.MemoryCountGB * item.Quantity} ГБ \nпревышает максиамльное количество \nоперативной памяти \nмат. платы {mt.Model} - {mt.MaxRAMCountGB} ГБ");
                            }
                            if (mt.RAMSlots < (item.Count * item.Quantity))
                            {
                                compatibilityIssues.Add($"❌ Количество плашек \nоперативной памяти \n{item.Model} - {item.Count * item.Quantity} шт.\nпревышает количество слотов \nна мат. плате {mt.Model} - {mt.RAMSlots} шт.");
                            }
                            if (mt.RAMTypeID != item.RAMTypeID)
                            {
                                compatibilityIssues.Add($"❌ Тип оперативной памяти \n{item.Model} - {context.RAMType.FirstOrDefault(x => x.ID == item.RAMTypeID).Type} отличается от\nтипа оперативной памяти на мат.плате \n{mt.Model} - {context.RAMType.FirstOrDefault(x => x.ID == mt.RAMTypeID).Type}");
                            }
                        }
                    }
                }

                foreach (var item in motherboard)
                {
                    foreach (var cs in cases)
                    {
                        if (!cs.SupportedMotherboardFormFactorIds.Contains((int)item.FormFactorID)) 
                        {
                            compatibilityIssues.Add($"❌ Корпус {cs.Name} \nне поддерживает форм-фактор \nматеринской платы\n{item.Model} - {context.FormFactor.FirstOrDefault(x => x.ID == item.FormFactorID).Type}");
                        }
                    }
                }

                foreach (var item in supply)
                {
                    if (gpu.Count() != 0 && cpu.Count() != 0)
                    {
                        foreach (var gp in gpu)
                        {
                            foreach (var cp in cpu)
                            {
                                if (item.Wattage < (gp.Voltage + cp.Voltage + 150))
                                {
                                    compatibilityIssues.Add($"❌ Недостаточно напряжения {item.Wattage} \nв блоке питания {item.Model} \nнеобходимо {gp.Voltage + cp.Voltage + 150}");
                                }
                            }
                        }
                        
                    }
                    else if (gpu.Count() != 0)
                    {
                        foreach (var gp in gpu)
                        {
                            if (item.Wattage < (gp.Voltage + 150))
                            {
                                compatibilityIssues.Add($"❌ Недостаточно напряжения {item.Wattage} в блоке питания {item.Model} \nнеобходимо {gp.Voltage + 150}");
                            }
                        }
                    }
                    else if (cpu.Count() != 0)
                    {
                        foreach (var cp in cpu)
                        {
                            if (item.Wattage < (cp.Voltage + 150))
                            {
                                compatibilityIssues.Add($"❌ Недостаточно напряжения {item.Wattage} в блоке питания {item.Model} \nнеобходимо {cp.Voltage + 150}");
                            }
                        }
                    }

                    if (cases.Count() != 0)
                    {
                        foreach (var cs in cases)
                        { 
                            if (!cs.SupportedSupplyFormFactorIds.Contains((int)item.FormFactorID))
                            {
                                compatibilityIssues.Add($"❌ Корпус {cs.Name} не поддерживает форм-фактор\nблока питания \n{item.Model} - {context.FormFactor.FirstOrDefault(x => x.ID == item.FormFactorID).Type}");
                            }
                        }    
                    }   
                }

                foreach (var item in disk)
                {
                    foreach (var mb in motherboard)
                    {
                        if (item.DiskTypeID == 1 && mb.SATASlots < item.Quantity)
                        {
                            compatibilityIssues.Add($"❌ Кол-во SATA-дисков {item.Quantity} шт. превышает \nкол-во SATA-слотов в материнской плате \n{mb.Model} - {mb.SATASlots} шт.");
                        }
                        if (item.DiskTypeID == 2 && mb.M2Slots < item.Quantity)
                        {
                            compatibilityIssues.Add($"❌ Кол-во M2-дисков {item.Quantity} шт. превышает \nкол-во M2-слотов в материнской плате \n{mb.Model} - {mb.SATASlots} шт.");
                        }
                    }
                    
                }

                foreach (var item in cooling)
                {
                    foreach (var cs in cases)
                    {
                        if (item.CoolerTypeID == 1 && item.Height > cs.CoolerLength)
                        {
                            compatibilityIssues.Add($"❌ Башня {item.Model} \nне поместится в корпус {cs.Model}\nвысота башни - {item.Height} мм.\nмакс.высота - {cs.CoolerLength} мм.");
                        }
                    }
                    
                    if (item.CoolerTypeID == 3 && item.Width > 140)
                    {
                        compatibilityIssues.Add($"❌ Корпусный вентилятор \n{item.Model} слишком большой, \nмаксимальная ширина - 140 мм.");
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

                var order = from oi in context.OrderItem
                             join o in context.Order on oi.OrderID equals o.ID
                             join p in context.Product on oi.ProductID equals p.ID
                             join pt in context.Part on p.PartID equals pt.ID
                             where oi.ID == orderItemId
                             select new
                             {
                                 Order = o,
                                 OrderItem = oi,
                                 Product = p,
                                 Part = pt
                             };

                var item = context.OrderItem.FirstOrDefault(x => x.ID == orderItemId);

                decimal price = selectedItem.Price;

                if (order.FirstOrDefault().Part.QuantityInStock >= order.FirstOrDefault().OrderItem.Quantity + 1)
                {
                    order.FirstOrDefault().Order.TotalPrice += price;
                    order.FirstOrDefault().OrderItem.Quantity++;

                    context.SaveChanges();
                    LoadData();
                }
                else
                {
                    MessageBox.Show("Недостаточно компонентов на складе.");
                    return;
                } 
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
