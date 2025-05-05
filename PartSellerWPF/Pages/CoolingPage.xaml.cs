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
    /// Логика взаимодействия для CoolingPage.xaml
    /// </summary>
    public partial class CoolingPage : Page
    {
        public CoolingPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadCoolingData(filterParams);
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

        private void LoadCoolingData(object filterParams)
        {

            try
            {
                var context = Entities.GetContext();

                var query = from c in context.Cooling
                            join p in context.Part on c.ID equals p.CoolingID
                            join prod in context.Product on p.ID equals prod.PartID
                            join ct in context.CoolerType on c.CoolerTypeID equals ct.ID
                            from ss in context.SupportedSockets.Where(ss => ss.CoolerID == c.ID).DefaultIfEmpty() 
                            join s in context.Socket on ss.SocketID equals s.ID into socketJoin
                            from s in socketJoin.DefaultIfEmpty()
                            select new
                            {
                                Cooling = c,
                                Part = p,
                                Product = prod,
                                CoolerType = ct,
                                SupportedSockets = ss,
                                Socket = s
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Cooling.BrandID == brandId);
                    }

                    if (filters.SocketId.HasValue && filters.SocketId != -1)
                    {
                        int socketId = filters.SocketId.Value;
                        query = query.Where(x => x.SupportedSockets.SocketID == socketId);
                    }

                    if (filters.CoolerTypeId.HasValue && filters.CoolerTypeId != -1)
                    {
                        int coolertypeId = filters.CoolerTypeId.Value;
                        query = query.Where(x => x.CoolerType.ID == coolertypeId);
                    }

                    if (filters.MaxHeight.HasValue)
                        query = query.Where(x => x.Cooling.Height <= filters.MaxHeight.Value);

                    if (filters.MaxWidth.HasValue)
                        query = query.Where(x => x.Cooling.Width <= filters.MaxWidth.Value);

                    if (filters.MaxLength.HasValue)
                        query = query.Where(x => x.Cooling.Length <= filters.MaxLength.Value);



                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new
                {
                    Brand = x.Cooling.Brand.Name,
                    x.Cooling.Model,
                    x.Cooling.RPM,
                    x.Cooling.Height,
                    x.Cooling.Length,
                    x.Cooling.Width,
                    x.CoolerType.Type,
                    x.Part.ID,
                    x.Part.Image,
                    x.Product.Price,
                }).Distinct().ToList();

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
    }
}
