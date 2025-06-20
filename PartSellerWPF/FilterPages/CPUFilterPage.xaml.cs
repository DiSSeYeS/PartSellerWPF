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
    /// Логика взаимодействия для CPUFilterPage.xaml
    /// </summary>
    public partial class CPUFilterPage : Page
    {
        public CPUFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            var context = Entities.GetContext();

            var query = from c in context.CPU
                        join p in context.Part on c.ID equals p.CPUID
                        join prod in context.Product on p.ID equals prod.PartID
                        join s in context.Socket on c.SocketID equals s.ID
                        select new
                        {
                            CPU = c,
                            Part = p,
                            Product = prod,
                            Socket = s
                        };

            var result = query.AsEnumerable().Select(x => new
            {
                Brand = x.CPU.Brand.Name,
                x.CPU.Model,
                x.CPU.Voltage,
                Socket = x.Socket.Name,
                x.CPU.Cores,
                x.CPU.Threads,
                x.CPU.FrequencyGHz,
                x.CPU.L1,
                x.CPU.L2,
                x.CPU.MaxFrequency,
                x.CPU.HasTurboBoost,
                x.Part.ID,
                x.Part.Image,
                x.Product.Price,
            }).ToList();

            BrandComboBox.ItemsSource = query.Select(x => x.CPU.Brand).Distinct().ToList();
            SocketComboBox.ItemsSource = query.Select(x => x.Socket).Where(x => x != null).Distinct().ToList();
            VoltageSlider.Maximum = result.Max(x => (double)x.Voltage);
            CoresSlider.Maximum = result.Max(x => x.Cores);
            ThreadsSlider.Maximum = result.Max(x => x.Threads);
            FreqSlider.Maximum = result.Max(x => (double)x.FrequencyGHz);
            L1Slider.Maximum = result.Max(x => x.L1);
            L2Slider.Maximum = result.Max(x => x.L2);
            MaxFreqSlider.Maximum = result.Max(x => (double)x.MaxFrequency);
            PriceSlider.Minimum = result.Min(x => (double)x.Price);
            VoltageSlider.Minimum = result.Min(x => (double)x.Voltage);
            CoresSlider.Minimum = result.Min(x => x.Cores);
            ThreadsSlider.Minimum = result.Min(x => x.Threads);
            FreqSlider.Minimum = result.Min(x => (double)x.FrequencyGHz);
            L1Slider.Minimum = result.Min(x => x.L1);
            L2Slider.Minimum = result.Min(x => x.L2);
            MaxFreqSlider.Minimum = result.Min(x => (double)x.MaxFrequency);
            PriceSlider.Minimum = result.Min(x => (double)x.Price);

            ResetValues();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            BrandComboBox.SelectedIndex = -1;
            SocketComboBox.SelectedIndex = -1;
            PriceSlider.Value = PriceSlider.Maximum;
            VoltageSlider.Value = VoltageSlider.Maximum;
            CoresSlider.Value = CoresSlider.Maximum;
            ThreadsSlider.Value = ThreadsSlider.Maximum;
            FreqSlider.Value = FreqSlider.Maximum;
            L1Slider.Value = L1Slider.Maximum;
            L2Slider.Value = L2Slider.Maximum;
            MaxFreqSlider.Value = MaxFreqSlider.Maximum;
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                SocketId = SocketComboBox.SelectedValue == null ? -1 : SocketComboBox.SelectedValue as int?,
                MaxVoltage = (int)VoltageSlider.Value,
                MaxCores = (int)CoresSlider.Value,
                MaxThreads = (int)ThreadsSlider.Value,
                MaxFreq = (decimal)FreqSlider.Value,
                MaxL1 = (int)L1Slider.Value,
                MaxL2 = (int)L2Slider.Value,
                MaxMaxFreq = (decimal)MaxFreqSlider.Value,
                MaxPrice = (int)PriceSlider.Value
            };

            var cpuPage = new CPUPage(filterParams);
            NavigationService.Navigate(cpuPage);
        }
    }
}
