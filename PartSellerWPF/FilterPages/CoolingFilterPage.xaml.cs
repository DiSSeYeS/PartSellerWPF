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

namespace PartSellerWPF.FilterPages
{
    /// <summary>
    /// Логика взаимодействия для CoolingFilterPage.xaml
    /// </summary>
    public partial class CoolingFilterPage : Page
    {
        public CoolingFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            var context = Entities.GetContext();

            var query = from c in context.Cooling
                        join p in context.Part on c.ID equals p.CoolingID
                        join prod in context.Product on p.ID equals prod.PartID
                        join ct in context.CoolerType on c.CoolerTypeID equals ct.ID
                        select new
                        {
                            Cooling = c,
                            Part = p,
                            Product = prod,
                            CoolerType = ct
                        };

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
            }).ToList();

            BrandComboBox.ItemsSource = query.Select(x => x.Cooling.Brand).Distinct().ToList();
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
            HeightSlider.Maximum = result.Max(x => (double)x.Height);
            WidthSlider.Maximum = result.Max(x => (double)x.Width);
            LengthSlider.Maximum = result.Max(x => (double)x.Length);

        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            BrandComboBox.SelectedIndex = -1;
            HeightSlider.Value = HeightSlider.Maximum;
            WidthSlider.Value = WidthSlider.Maximum;
            LengthSlider.Value = LengthSlider.Maximum;
            PriceSlider.Value = PriceSlider.Maximum;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                MaxHeight = (int)HeightSlider.Value,
                MaxWidth = (int)WidthSlider.Value,
                MaxLength = (int)LengthSlider.Value,
                MaxPrice = (int)PriceSlider.Value
            };

            var coolingPage = new CoolingPage(filterParams);
            NavigationService.Navigate(coolingPage);
        }
    }
}
