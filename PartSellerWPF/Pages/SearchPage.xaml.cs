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
        public SearchPage()
        {
            InitializeComponent();
            dataGrid.Loaded += DataGrid_Loaded;
            LoadComponents();
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

        private void LoadComponents()
        {

            Entities context = Entities.GetContext();

            var components = new List<ComponentDto>();

            var cpuComponents = from cp in context.CPU
                                join p in context.Part on cp.ID equals p.CPUID
                                join pr in context.Product on p.ID equals pr.PartID
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

            dataGrid.ItemsSource = components;
        }
    }
}
