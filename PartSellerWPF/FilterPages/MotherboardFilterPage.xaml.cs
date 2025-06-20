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
    /// Логика взаимодействия для MotherboardFilterPage.xaml
    /// </summary>
    public partial class MotherboardFilterPage : Page
    {
        public MotherboardFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            var context = Entities.GetContext();

            var query = from c in context.Motherboard
                        join p in context.Part on c.ID equals p.MotherboardID
                        join prod in context.Product on p.ID equals prod.PartID
                        join ch in context.Chipset on c.ChipsetID equals ch.ID
                        join s in context.Socket on c.SocketID equals s.ID
                        join r in context.RAMType on c.RAMTypeID equals r.ID
                        select new
                        {
                            Motherboard = c,
                            Part = p,
                            Product = prod,
                            Chipset = ch,
                            Socket = s,
                            RAMType = r
                        };

            var result = query.AsEnumerable().Select(x => new
            {
                Brand = x.Motherboard.Brand.Name,
                x.Motherboard.Model,
                x.Motherboard.Height,
                x.Motherboard.Width,
                Socket = x.Socket.Name,
                Chipset = x.Chipset.Name,
                RAMType = x.RAMType.Type,
                x.Motherboard.RAMSlots,
                x.Motherboard.MaxRAMCountGB,
                x.Motherboard.MaxRAMFrequencyMHz,
                x.Motherboard.SATASlots,
                x.Motherboard.M2Slots,
                x.Motherboard.NVMe,
                x.Part.ID,
                x.Part.Image,
                x.Product.Price,
            }).ToList();

            BrandComboBox.ItemsSource = query.Select(x => x.Motherboard.Brand).Distinct().ToList();
            RamTypeComboBox.ItemsSource = query.Select(x => x.RAMType).Distinct().ToList();
            ChipsetComboBox.ItemsSource = query.Select(x => x.Chipset).Distinct().ToList();
            SocketComboBox.ItemsSource = query.Select(x => x.Socket).Distinct().ToList();
            NVMeComboBox.ItemsSource = query.Select(x => x.Motherboard.NVMe).Distinct().ToList(); // КОСТЫЛЬ
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
            HeightSlider.Maximum = result.Max(x => (double)x.Height);
            WidthSlider.Maximum = result.Max(x => (double)x.Width);
            RamSlotsSlider.Maximum = result.Max(x => x.RAMSlots);
            RamFreqSlider.Maximum = result.Max(x => x.MaxRAMFrequencyMHz);
            RamCountSlider.Maximum = result.Max(x => x.MaxRAMCountGB);
            SataSlotsSlider.Maximum = result.Max(x => x.SATASlots);
            M2SlotsSlider.Maximum = result.Max(x => x.M2Slots);
            PriceSlider.Minimum = result.Min(x => (double)x.Price);
            HeightSlider.Minimum = result.Min(x => (double)x.Height);
            WidthSlider.Minimum = result.Min(x => (double)x.Width);
            RamSlotsSlider.Minimum = result.Min(x => x.RAMSlots);
            RamFreqSlider.Minimum = result.Min(x => x.MaxRAMFrequencyMHz);
            RamCountSlider.Minimum = result.Min(x => x.MaxRAMCountGB);
            SataSlotsSlider.Minimum = result.Min(x => x.SATASlots);
            M2SlotsSlider.Minimum = result.Min(x => x.M2Slots);

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
            ChipsetComboBox.SelectedIndex = -1;
            SocketComboBox.SelectedIndex = -1;
            NVMeComboBox.SelectedIndex = -1;
            HeightSlider.Value = HeightSlider.Maximum;
            WidthSlider.Value = WidthSlider.Maximum;
            PriceSlider.Value = PriceSlider.Maximum;
            RamSlotsSlider.Value = RamSlotsSlider.Maximum;
            RamFreqSlider.Value = RamFreqSlider.Maximum;
            RamCountSlider.Value = RamCountSlider.Maximum;
            SataSlotsSlider.Value = SataSlotsSlider.Maximum;
            M2SlotsSlider.Value = M2SlotsSlider.Maximum;
        }
        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                RamTypeId = RamTypeComboBox.SelectedValue == null ? -1 : RamTypeComboBox.SelectedValue as int?,
                ChipsetId = ChipsetComboBox.SelectedValue == null ? -1 : ChipsetComboBox.SelectedValue as int?,
                SocketId = SocketComboBox.SelectedValue == null ? -1 : SocketComboBox.SelectedValue as int?,
                MaxHeight = (int)HeightSlider.Value,
                MaxWidth = (int)WidthSlider.Value,
                MaxPrice = (int)PriceSlider.Value,
                MaxRamSlots = (int)RamSlotsSlider.Value,
                MaxMemoryFreq = (int)RamFreqSlider.Value,
                MaxRAMGB = (int)RamCountSlider.Value,
                MaxSataSlots = (int)SataSlotsSlider.Value,
                MaxM2Slots = (int)M2SlotsSlider.Value
            };

            var motherboardPage = new MotherboardPage(filterParams);
            NavigationService.Navigate(motherboardPage);
        }
    }
}
