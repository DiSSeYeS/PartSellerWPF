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
    /// Логика взаимодействия для SupplyPage.xaml
    /// </summary>
    public partial class SupplyPage : Page
    {
        public SupplyPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadSupplyData(filterParams);
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

        private void LoadSupplyData(object filterParams)
        {

            try
            {
                var context = Entities.GetContext();

                var query = from c in context.Supply
                            join p in context.Part on c.ID equals p.SupplyID
                            join prod in context.Product on p.ID equals prod.PartID
                            select new
                            {
                                Supply = c,
                                Part = p,
                                Product = prod
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Supply.BrandID == brandId);
                    }

                    if (filters.MaxHeight.HasValue)
                        query = query.Where(x => x.Supply.Height <= filters.MaxHeight.Value);

                    if (filters.MaxWidth.HasValue)
                        query = query.Where(x => x.Supply.Width <= filters.MaxWidth.Value);

                    if (filters.MaxLength.HasValue)
                        query = query.Where(x => x.Supply.Length <= filters.MaxLength.Value);

                    if (filters.MaxWattage.HasValue)
                        query = query.Where(x => x.Supply.Wattage <= filters.MaxWattage.Value);

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

                var result = query.AsEnumerable().Select(x => new
                {
                    Brand = x.Supply.Brand.Name,
                    x.Supply.Model,
                    x.Supply.Wattage,
                    x.Supply.Height,
                    x.Supply.Length,
                    x.Supply.Width,
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
    }
}

