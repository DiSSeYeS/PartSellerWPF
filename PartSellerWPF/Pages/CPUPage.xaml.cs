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
    /// Логика взаимодействия для CPUPage.xaml
    /// </summary>
    public partial class CPUPage : Page
    {
        public CPUPage(FilterParams filterParams = null)
        {
            InitializeComponent();
            LoadCPUData(filterParams);

        }

        private void LoadCPUData(object filterParams)
        {

            try
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

                if (filterParams is FilterParams filters)
                {
                    if (filters.BrandId.HasValue && filters.BrandId != -1)
                    {
                        int brandId = filters.BrandId.Value;
                        query = query.Where(x => x.CPU.BrandID == brandId);
                    }

                    if (filters.SocketId.HasValue && filters.SocketId != -1)
                    {
                        int socketId = filters.SocketId.Value;
                        query = query.Where(x => x.CPU.SocketID == socketId);
                    }

                    if (filters.MaxVoltage.HasValue)
                    {
                        int maxVoltage = filters.MaxVoltage.Value;
                        query = query.Where(x => x.CPU.Voltage <= maxVoltage);
                    }

                    if (filters.MaxCores.HasValue)
                    {
                        int maxCores = filters.MaxCores.Value;
                        query = query.Where(x => x.CPU.Cores <= maxCores);
                    }

                    if (filters.MaxThreads.HasValue)
                    {
                        int maxThreads = filters.MaxThreads.Value;
                        query = query.Where(x => x.CPU.Threads <= maxThreads);
                    }

                    if (filters.MaxFreq.HasValue)
                    {
                        decimal maxFreq = filters.MaxFreq.Value;
                        query = query.Where(x => x.CPU.FrequencyGHz <= maxFreq);
                    }

                    if (filters.MaxL1.HasValue)
                    {
                        int maxL1 = filters.MaxL1.Value;
                        query = query.Where(x => x.CPU.L1 <= maxL1);
                    }

                    if (filters.MaxL2.HasValue)
                    {
                        int maxL2 = filters.MaxL2.Value;
                        query = query.Where(x => x.CPU.L2 <= maxL2);
                    }

                    if (filters.MaxMaxFreq.HasValue)
                    {
                        decimal maxMaxFreq = filters.MaxMaxFreq.Value;
                        query = query.Where(x => x.CPU.MaxFrequency <= maxMaxFreq);
                    }

                    if (filters.MaxPrice.HasValue)
                        query = query.Where(x => x.Product.Price <= filters.MaxPrice.Value);
                }

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

    

