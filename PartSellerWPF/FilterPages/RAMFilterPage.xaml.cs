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
    /// Логика взаимодействия для RAMFilterPage.xaml
    /// </summary>
    public partial class RAMFilterPage : Page
    {
        public RAMFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
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

            BrandComboBox.ItemsSource = query.Select(x => x.RAM.Brand).Distinct().ToList();
            RamTypeComboBox.ItemsSource = query.Select(x => x.RAMType).Distinct().ToList();
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
            RAMGBSlider.Maximum = result.Max(x => (double)x.MemoryCountGB);
            FreqSlider.Maximum = result.Max(x => (double)x.MemoryFrequencyMHz);
            CountSlider.Maximum = result.Max(x => (double)x.Count);
            PriceSlider.Minimum = result.Min(x => (double)x.Price);
            RAMGBSlider.Minimum = result.Min(x => (double)x.MemoryCountGB);
            FreqSlider.Minimum = result.Min(x => (double)x.MemoryFrequencyMHz);
            CountSlider.Minimum = result.Min(x => (double)x.Count);

            ResetValues();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            BrandComboBox.SelectedIndex = -1;
            RamTypeComboBox.SelectedIndex = -1;
            RAMGBSlider.Value = RAMGBSlider.Maximum;
            FreqSlider.Value = FreqSlider.Maximum;
            CountSlider.Value = CountSlider.Maximum;
            PriceSlider.Value = PriceSlider.Maximum;
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                RamTypeId = RamTypeComboBox.SelectedValue == null ? -1 : RamTypeComboBox.SelectedValue as int?,
                MaxRAMGB = (int)RAMGBSlider.Value,
                MaxFreq = (int)FreqSlider.Value,
                MaxCount = (int)CountSlider.Value,
                MaxPrice = (int)PriceSlider.Value
            };

            var ramPage = new RAMPage(filterParams);
            NavigationService.Navigate(ramPage);
        }
    }
}
