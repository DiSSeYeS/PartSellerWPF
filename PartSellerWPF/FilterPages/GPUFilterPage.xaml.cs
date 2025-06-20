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
    /// Логика взаимодействия для GPUFilterPage.xaml
    /// </summary>
    public partial class GPUFilterPage : Page
    {
        public GPUFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            var context = Entities.GetContext();

            var query = from c in context.GPU
                        join p in context.Part on c.ID equals p.GPUID
                        join prod in context.Product on p.ID equals prod.PartID
                        select new
                        {
                            GPU = c,
                            Part = p,
                            Product = prod
                        };

            var result = query.AsEnumerable().Select(x => new
            {
                Brand = x.GPU.Brand.Name,
                x.GPU.Model,
                x.GPU.Height,
                x.GPU.Length,
                x.GPU.Width,
                x.GPU.Voltage,
                x.GPU.VideoMemoryGB,
                x.GPU.MemoryFrequencyMHz,
                x.GPU.CoreFrequencyMHz,
                x.Part.ID,
                x.Part.Image,
                x.Product.Price,
            }).ToList();

            BrandComboBox.ItemsSource = query.Select(x => x.GPU.Brand).Distinct().ToList();
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
            HeightSlider.Maximum = result.Max(x => (double)x.Height);
            WidthSlider.Maximum = result.Max(x => (double)x.Width);
            LengthSlider.Maximum = result.Max(x => (double)x.Length);
            VoltageSlider.Maximum = result.Max(x => x.Voltage);
            MemorySlider.Maximum = result.Max(x => x.VideoMemoryGB);
            MemoryFreqSlider.Maximum = result.Max(x => (double)x.MemoryFrequencyMHz);
            CoreFreqSlider.Maximum = result.Max(x => (double)x.CoreFrequencyMHz);
            PriceSlider.Minimum = result.Min(x => (double)x.Price);
            HeightSlider.Minimum = result.Min(x => (double)x.Height);
            WidthSlider.Minimum = result.Min(x => (double)x.Width);
            LengthSlider.Minimum = result.Min(x => (double)x.Length);
            VoltageSlider.Minimum = result.Min(x => x.Voltage);
            MemorySlider.Minimum = result.Min(x => x.VideoMemoryGB);
            MemoryFreqSlider.Minimum = result.Min(x => (double)x.MemoryFrequencyMHz);
            CoreFreqSlider.Minimum = result.Min(x => (double)x.CoreFrequencyMHz);

            ResetValues();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            BrandComboBox.SelectedIndex = -1;
            HeightSlider.Value = HeightSlider.Maximum;
            WidthSlider.Value = WidthSlider.Maximum;
            LengthSlider.Value = LengthSlider.Maximum;
            PriceSlider.Value = PriceSlider.Maximum;
            VoltageSlider.Value = VoltageSlider.Maximum;
            MemorySlider.Value = MemorySlider.Maximum;
            MemoryFreqSlider.Value = MemoryFreqSlider.Maximum;
            CoreFreqSlider.Value = CoreFreqSlider.Maximum;
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
                MaxVoltage = (int)VoltageSlider.Value,
                MaxVideoMemory = (int)MemorySlider.Value,
                MaxMemoryFreq = (int)MemoryFreqSlider.Value,
                MaxCoreFreq = (int)CoreFreqSlider.Value
            };

            var gpuPage = new GPUPage(filterParams);
            NavigationService.Navigate(gpuPage);
        }
    }
}
