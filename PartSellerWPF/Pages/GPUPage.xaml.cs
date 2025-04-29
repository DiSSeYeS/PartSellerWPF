using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Логика взаимодействия для GPUPage.xaml
    /// </summary>
    public partial class GPUPage : Page
    {
        public GPUPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadGPUData(filterParams);

        }

        private void LoadGPUData(object filterParams)
        {

            try
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

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.GPU.BrandID == brandId);
                    }

                    if (filters.MaxVoltage.HasValue)
                    {
                        int maxVoltage = filters.MaxVoltage.Value;
                        query = query.Where(x => x.GPU.Voltage <= maxVoltage);
                    }

                    if (filters.MaxVideoMemory.HasValue)
                    {
                        int maxVideoMemory = filters.MaxVideoMemory.Value;
                        query = query.Where(x => x.GPU.VideoMemoryGB <= maxVideoMemory);
                    }

                    if (filters.MaxMemoryFreq.HasValue)
                    {
                        int maxMemoryFreq = filters.MaxMemoryFreq.Value;
                        query = query.Where(x => x.GPU.MemoryFrequencyMHz <= maxMemoryFreq);
                    }

                    if (filters.MaxCoreFreq.HasValue)
                    {
                        int maxCoreFreq = filters.MaxCoreFreq.Value;
                        query = query.Where(x => x.GPU.CoreFrequencyMHz <= maxCoreFreq);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

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

