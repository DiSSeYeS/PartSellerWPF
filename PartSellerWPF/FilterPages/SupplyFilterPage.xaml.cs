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
    /// Логика взаимодействия для SupplyFilterPage.xaml
    /// </summary>
    public partial class SupplyFilterPage : Page
    {
        public SupplyFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
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

            BrandComboBox.ItemsSource = query.Select(x => x.Supply.Brand).Distinct().ToList();
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
            HeightSlider.Maximum = result.Max(x => (double)x.Height);
            WidthSlider.Maximum = result.Max(x => (double)x.Width);
            LengthSlider.Maximum = result.Max(x => (double)x.Length);
            WattageSlider.Maximum = result.Max(x => x.Wattage);
            PriceSlider.Minimum = result.Min(x => (double)x.Price);
            HeightSlider.Minimum = result.Min(x => (double)x.Height);
            WidthSlider.Minimum = result.Min(x => (double)x.Width);
            LengthSlider.Minimum = result.Min(x => (double)x.Length);
            WattageSlider.Minimum = result.Min(x => x.Wattage);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            BrandComboBox.SelectedIndex = -1;
            HeightSlider.Value = HeightSlider.Maximum;
            WidthSlider.Value = WidthSlider.Maximum;
            LengthSlider.Value = LengthSlider.Maximum;
            PriceSlider.Value = PriceSlider.Maximum;
            WattageSlider.Value = WattageSlider.Maximum;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                MaxHeight = (int)HeightSlider.Value,
                MaxWidth = (int)WidthSlider.Value,
                MaxLength = (int)LengthSlider.Value,
                MaxPrice = (int)PriceSlider.Value,
                MaxWattage = (int)WattageSlider.Value
            };

            var supplyPage = new SupplyPage(filterParams);
            NavigationService.Navigate(supplyPage);
        }
    }
}
