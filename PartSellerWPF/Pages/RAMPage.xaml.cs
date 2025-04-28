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
    /// Логика взаимодействия для RAMPage.xaml
    /// </summary>
    public partial class RAMPage : Page
    {
        public RAMPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadRAMData(filterParams);

        }

        private void LoadRAMData(object filterParams)
        {

            try
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

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.RAM.BrandID == brandId);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

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
