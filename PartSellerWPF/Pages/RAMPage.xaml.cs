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
    /// Логика взаимодействия для RAMPage.xaml
    /// </summary>
    public partial class RAMPage : Page
    {
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

        private void LoadRAMData(object filterParams)
        {

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

                var result = query.AsEnumerable().Select(x => new
                {
                    Brand = x.RAM.Brand.Name,
                    x.RAM.Model,
                    x.RAM.MemoryCountGB,
                    x.RAM.MemoryFrequencyMHz,
                    x.RAM.Count,
                    RamType = x.RAMType.Type,
                    x.Part.ID,
                    x.Part.Image,
                    x.Product.Price,
                }).ToList();

                dataGrid.ItemsSource = result;
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

        }
    }

}
