using PartSellerWPF.Pages;
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
using static PartSellerWPF.Pages.CasePage;

namespace PartSellerWPF.FilterPages
{
    /// <summary>
    /// Логика взаимодействия для CaseFilterPage.xaml
    /// </summary>
    public partial class CaseFilterPage : Page
    {
        public CaseFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            using (var context = new Entities())
            {
                var query = from c in context.Case
                            join p in context.Part on c.ID equals p.CaseID
                            join prod in context.Product on p.ID equals prod.PartID
                            select new
                            {
                                Case = c,
                                Part = p,
                                Product = prod
                            };

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
                });

                BrandComboBox.ItemsSource = query.Select(x => x.Case.Brand).Distinct().ToList();
                FormFactorComboBox.ItemsSource = query.Select(x => x.Case.FormFactor).Distinct().ToList();
                PriceSlider.Maximum = result.Max(x => (double)x.Price);
                HeightSlider.Maximum = result.Max(x => (double)x.Height);
                WidthSlider.Maximum = result.Max(x => (double)x.Width);
                LengthSlider.Maximum = result.Max(x => (double)x.Length);
                GpuLengthSlider.Maximum = result.Max(x => (double)x.GPULength);
                CoolerHeightSlider.Maximum = result.Max(x => (double)x.CoolerLength);
                PsuLengthSlider.Maximum = result.Max(x => (double)x.SupplyLength);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            BrandComboBox.SelectedIndex = -1;
            FormFactorComboBox.SelectedIndex = -1;
            HeightSlider.Value = HeightSlider.Maximum;
            WidthSlider.Value = WidthSlider.Maximum;
            LengthSlider.Value = LengthSlider.Maximum;
            GpuLengthSlider.Value = GpuLengthSlider.Maximum;
            CoolerHeightSlider.Value = CoolerHeightSlider.Maximum;
            PsuLengthSlider.Value = PsuLengthSlider.Maximum;
            PriceSlider.Value = PriceSlider.Maximum;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                FormFactorId = FormFactorComboBox.SelectedValue == null ? -1 : FormFactorComboBox.SelectedValue as int?,
                MaxHeight = (int)HeightSlider.Value,
                MaxWidth = (int)WidthSlider.Value,
                MaxLength = (int)LengthSlider.Value,
                MaxGpuLength = (int)GpuLengthSlider.Value,
                MaxCoolerHeight = (int)CoolerHeightSlider.Value,
                MaxPsuLength = (int)PsuLengthSlider.Value,
                MaxPrice = (int)PriceSlider.Value
            };

            var casePage = new CasePage(filterParams);
            NavigationService.Navigate(casePage);
        }

    }
}
