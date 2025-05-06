using System;
using System.Data.Entity;
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
    /// Логика взаимодействия для CasePage.xaml
    /// </summary>
    public partial class CasePage : Page
    {
        public CasePage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadCaseData(filterParams);
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

        private void LoadCaseData(object filterParams)
        {

            try
            {
                var context = Entities.GetContext();

                var query = from c in context.Case
                            join p in context.Part on c.ID equals p.CaseID
                            join prod in context.Product on p.ID equals prod.PartID
                            select new
                            {
                                Case = c,
                                Part = p,
                                Product = prod
                            };

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Case.BrandID == brandId);
                    }

                    if (filters.FormFactorId.HasValue && filters.FormFactorId != -1)
                    {
                        int formFactorId = filters.FormFactorId.Value;
                        query = query.Where(x => x.Case.FormFactorTypeID == formFactorId);
                    }

                    if (filters.MaxHeight.HasValue)
                        query = query.Where(x => x.Case.Height <= filters.MaxHeight.Value);

                    if (filters.MaxWidth.HasValue)
                        query = query.Where(x => x.Case.Width <= filters.MaxWidth.Value);

                    if (filters.MaxLength.HasValue)
                        query = query.Where(x => x.Case.Length <= filters.MaxLength.Value);

                    if (filters.MaxGpuLength.HasValue)
                        query = query.Where(x => x.Case.GPULength <= filters.MaxGpuLength.Value);

                    if (filters.MaxCoolerHeight.HasValue)
                        query = query.Where(x => x.Case.CoolerLength <= filters.MaxCoolerHeight.Value);

                    if (filters.MaxPsuLength.HasValue)
                        query = query.Where(x => x.Case.SupplyLength <= filters.MaxPsuLength.Value);

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }
                    
                var result = query.AsEnumerable().Select(x => new
                {
                    Brand = x.Case.Brand.Name,
                    x.Case.Model,
                    x.Case.Height,
                    x.Case.Length,
                    x.Case.Width,
                    x.Case.SupplyLength,
                    x.Case.CoolerLength,
                    x.Case.GPULength,
                    FormFactor = x.Case.FormFactor.Type,
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
            if (AuthManager.CurrentUser != null)
            {

            }
        }
    }
}