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

namespace PartSellerWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для MotherboardPage.xaml
    /// </summary>
    public partial class MotherboardPage : Page
    {
        public MotherboardPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadMotherboardData(filterParams);

        }

        private void LoadMotherboardData(object filterParams)
        {

            try
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

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Motherboard.BrandID == brandId);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

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
    }
}

