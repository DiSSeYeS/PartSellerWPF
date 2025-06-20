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
    /// Логика взаимодействия для DiskFilterPage.xaml
    /// </summary>
    public partial class DiskFilterPage : Page
    {
        public DiskFilterPage()
        {
            InitializeComponent();
            LoadFilters();
        }

        private void LoadFilters()
        {
            var context = Entities.GetContext();

            var query = from c in context.Disk
                        join p in context.Part on c.ID equals p.DiskID
                        join prod in context.Product on p.ID equals prod.PartID
                        join dt in context.DiskType on c.DiskTypeID equals dt.ID
                        select new
                        {
                            Disk = c,
                            Part = p,
                            Product = prod,
                            DiskType = dt
                        };

            var result = query.AsEnumerable().Select(x => new
            {
                Brand = x.Disk.Brand.Name,
                x.Disk.Model,
                x.Disk.Space,
                DiskType = x.DiskType.Type,
                x.Part.ID,
                x.Part.Image,
                x.Product.Price,
            }).ToList();

            BrandComboBox.ItemsSource = query.Select(x => x.Disk.Brand).Distinct().ToList();
            DiskTypeComboBox.ItemsSource = query.Select(x => x.Disk.DiskType).Distinct().ToList();
            PriceSlider.Maximum = result.Max(x => (double)x.Price);
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
            DiskTypeComboBox.SelectedIndex = -1;
            PriceSlider.Value = PriceSlider.Maximum;
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            var filterParams = new FilterParams
            {
                BrandId = BrandComboBox.SelectedValue == null ? -1 : BrandComboBox.SelectedValue as int?,
                DiskTypeId = DiskTypeComboBox.SelectedValue == null ? -1 : DiskTypeComboBox.SelectedValue as int?,
                MaxPrice = (int)PriceSlider.Value
            };

            var diskPage = new DiskPage(filterParams);
            NavigationService.Navigate(diskPage);
        }
    }
}
