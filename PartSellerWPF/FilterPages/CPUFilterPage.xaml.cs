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
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            BrandComboBox.SelectedIndex = -1;
            PriceSlider.Value = PriceSlider.Maximum;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                MaxPrice = (int)PriceSlider.Value
            };

            var cpuPage = new CPUPage(filterParams);
            NavigationService.Navigate(cpuPage);
        }
    }
}
