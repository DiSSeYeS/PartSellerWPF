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
    /// Логика взаимодействия для DiskPage.xaml
    /// </summary>
    public partial class DiskPage : Page
    {
        public DiskPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadDiskData(filterParams);

        }

        private void LoadDiskData(object filterParams)
        {

            try
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

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.Disk.BrandID == brandId);
                    }


                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

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

