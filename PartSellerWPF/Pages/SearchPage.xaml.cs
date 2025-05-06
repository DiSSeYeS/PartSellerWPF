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
    /// Логика взаимодействия для SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        public SearchPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            dataGrid.Loaded += DataGrid_Loaded;
            LoadComponents(filterParams);
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.CurrentCellChanged += DataGrid_CurrentCellChanged;
        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            if (dataGrid.CurrentColumn != null)
            {
                foreach (var column in dataGrid.Columns)
                {
                    if (column.Header.ToString() != "Изображение")
                        column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }

                dataGrid.CurrentColumn.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
                dataGrid.ScrollIntoView(dataGrid.CurrentItem, dataGrid.CurrentColumn);
            }
        }

        private void LoadComponents(object filterParams)
        {

            Entities context = Entities.GetContext();

            var components = new List<ComponentDto>();

            var cpuComponents = from cp in context.CPU
                                join p in context.Part on cp.ID equals p.CPUID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                select new ComponentDto
                                {
                                    Type = "CPU",
                                    Brand = cp.Brand.Name,
                                    Name = cp.Model,
                                    Price = (int)pr.Price,
                                    ImageUrl = p.Image,
                                    Id = cp.ID
                                };
            components.AddRange(cpuComponents);

            var gpuComponents = from gp in context.GPU
                                join p in context.Part on gp.ID equals p.GPUID
                                join pr in context.Product on p.ID equals pr.PartID
                                select new ComponentDto
                                {
                                    Type = "GPU",
                                    Brand = gp.Brand.Name,
                                    Name = gp.Model,
                                    Price = (int)pr.Price,
                                    ImageUrl = p.Image,
                                    Id = gp.ID
                                };
            components.AddRange(gpuComponents);

            var mbComponents = from m in context.Motherboard
                               join p in context.Part on m.ID equals p.MotherboardID
                               join pr in context.Product on p.ID equals pr.PartID
                               select new ComponentDto
                               {
                                   Type = "Motherboard",
                                   Brand = m.Brand.Name,
                                   Name = m.Model,
                                   Price = (int)pr.Price,
                                   ImageUrl = p.Image,
                                   Id = m.ID
                               };
            components.AddRange(mbComponents);

            var ramComponents = from r in context.RAM
                                join p in context.Part on r.ID equals p.RAMID
                                join pr in context.Product on p.ID equals pr.PartID
                                select new ComponentDto
                                {
                                    Type = "RAM",
                                    Brand = r.Brand.Name,
                                    Name = r.Model,
                                    Price = (int)pr.Price,
                                    ImageUrl = p.Image,
                                    Id = r.ID
                                };
            components.AddRange(ramComponents);

            var diskComponents = from d in context.Disk
                                 join p in context.Part on d.ID equals p.DiskID
                                 join pr in context.Product on p.ID equals pr.PartID
                                 select new ComponentDto
                                 {
                                     Type = "Disk",
                                     Brand = d.Brand.Name,
                                     Name = d.Model,
                                     Price = (int)pr.Price,
                                     ImageUrl = p.Image,
                                     Id = d.ID
                                 };
            components.AddRange(diskComponents);

            var supplyComponents = from s in context.Supply
                                   join p in context.Part on s.ID equals p.SupplyID
                                   join pr in context.Product on p.ID equals pr.PartID
                                   select new ComponentDto
                                   {
                                       Type = "Power Supply",
                                       Brand = s.Brand.Name,
                                       Name = s.Model,
                                       Price = (int)pr.Price,
                                       ImageUrl = p.Image,
                                       Id = s.ID
                                   };
            components.AddRange(supplyComponents);

            var coolingComponents = from c in context.Cooling
                                    join p in context.Part on c.ID equals p.CoolingID
                                    join pr in context.Product on p.ID equals pr.PartID
                                    select new ComponentDto
                                    {
                                        Type = "Cooling",
                                        Brand = c.Brand.Name,
                                        Name = c.Model,
                                        Price = (int)pr.Price,
                                        ImageUrl = p.Image,
                                        Id = c.ID
                                    };
            components.AddRange(coolingComponents);

            var caseComponents = from ca in context.Case
                                 join p in context.Part on ca.ID equals p.CaseID
                                 join pr in context.Product on p.ID equals pr.PartID
                                 select new ComponentDto
                                 {
                                     Type = "Case",
                                     Brand = ca.Brand.Name,
                                     Name = ca.Model,
                                     Price = (int)pr.Price,
                                     ImageUrl = p.Image,
                                     Id = ca.ID
                                 };
            components.AddRange(caseComponents);

            var distinctComponents = components
                .GroupBy(x => x.Id)
                .Select(g => g.First())
                .ToList();

            if (filterParams is FilterParams filters)
            {
                if (filters.Search != null && filters.Search.Any())
                {
                    var searchTerms = filters.Search
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(s => s.Trim().ToLower())
                        .ToList();

                    if (searchTerms.Any())
                    {
                        distinctComponents = distinctComponents
                            .Where(x => searchTerms.Any(term =>
                                (!string.IsNullOrEmpty(x.Name) && x.Name.ToLower().Contains(term)) ||
                                (!string.IsNullOrEmpty(x.Brand) && x.Brand.ToLower().Contains(term)) ||
                                (!string.IsNullOrEmpty(x.Type) && x.Type.ToLower().Contains(term))
                            )
                        ).ToList();
                            
                    }
                }
            }

            dataGrid.ItemsSource = distinctComponents;
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (AuthManager.IsLoggedIn)
            {
                if (!(dataGrid.SelectedItem is ComponentDto selectedComponent))
                {
                    MessageBox.Show("Пожалуйста, выберите компонент из таблицы");
                    return;
                }

                var context = Entities.GetContext();
                try
                {
                    var currentOrder = context.Order
                        .Where(o => o.UserId == AuthManager.CurrentUser.ID)
                        .OrderByDescending(o => o.Date)
                        .FirstOrDefault();

                    var existingItem = currentOrder.OrderItem?
                            .FirstOrDefault(oi => oi.ProductID == Funcs.GetProductIdByComponentDto(selectedComponent));

                    int selectedID = Funcs.GetProductIdByComponentDto(selectedComponent);
                    var partInStock = context.Part
                        .FirstOrDefault(p => p.ID == selectedID);

                    if (currentOrder != null)
                    {
                        currentOrder.TotalPrice += selectedComponent.Price;

                        if (existingItem != null)
                        {
                            if (partInStock != null && existingItem.Quantity + 1 > partInStock.QuantityInStock)
                            {
                                MessageBox.Show($"Недостаточно товара на складе. Доступно: {partInStock.QuantityInStock}");
                                return;
                            }

                            existingItem.Quantity++;
                        }
                        else
                        {
                            var orderItem = new OrderItem
                            {
                                OrderID = currentOrder.ID,
                                ProductID = Funcs.GetProductIdByComponentDto(selectedComponent),
                                Quantity = 1
                            };

                            context.OrderItem.Add(orderItem);
                        }                        
                    }
                    else
                    {
                        if (partInStock.QuantityInStock < 1)
                        {
                            MessageBox.Show("Недостаточно товара на складе");
                            return;
                        }

                        currentOrder = new Order
                        {
                            UserId = AuthManager.CurrentUser.ID,
                            TotalPrice = selectedComponent.Price,
                            Status = "Cart",
                            Date = DateTime.Now
                        };

                        context.Order.Add(currentOrder);
                        context.SaveChanges();

                        var orderItem = new OrderItem
                        {
                            OrderID = currentOrder.ID,
                            ProductID = Funcs.GetProductIdByComponentDto(selectedComponent),
                            Quantity = 1
                        };

                        context.OrderItem.Add(orderItem);
                    }

                    context.SaveChanges();
                    MessageBox.Show("Компонент успешно добавлен в заказ");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении в заказ: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Необходимо авторизоваться");
            }
        } 
    }
}
