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
            dataGrid.Loaded += DataGrid_Loaded;
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            dataGrid.CurrentCellChanged += DataGrid_CurrentCellChanged;
        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            if (dataGrid.CurrentColumn != null)
            {
                foreach (var column in dataGrid.Columns)
                {
                    if (column.Header.ToString() != "Изображение")
                        column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }

                dataGrid.CurrentColumn.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
                dataGrid.ScrollIntoView(dataGrid.CurrentItem, dataGrid.CurrentColumn);
            }
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

                    if (filters.SocketId.HasValue && filters.SocketId != -1)
                    {
                        int socketId = filters.SocketId.Value;
                        query = query.Where(x => x.Motherboard.SocketID == socketId);
                    }
                    if (filters.ChipsetId.HasValue && filters.ChipsetId != -1)
                    {
                        int chipsetId = filters.ChipsetId.Value;
                        query = query.Where(x => x.Motherboard.ChipsetID == chipsetId);
                    }
                    if (filters.RamTypeId.HasValue && filters.RamTypeId != -1)
                    {
                        int ramTypeId = filters.RamTypeId.Value;
                        query = query.Where(x => x.Motherboard.RAMTypeID == ramTypeId);
                    }
                    if (filters.MaxRamSlots.HasValue && filters.MaxRamSlots != -1)
                    {
                        int ramSlots = filters.MaxRamSlots.Value;
                        query = query.Where(x => x.Motherboard.RAMSlots <= ramSlots);
                    }
                    if (filters.MaxRAMGB.HasValue && filters.MaxRAMGB != -1)
                    {
                        int maxRAMGB = filters.MaxRAMGB.Value;
                        query = query.Where(x => x.Motherboard.MaxRAMCountGB <= maxRAMGB);
                    }
                    if (filters.MaxMemoryFreq.HasValue && filters.MaxMemoryFreq != -1)
                    {
                        int maxMemoryFreq = filters.MaxMemoryFreq.Value;
                        query = query.Where(x => x.Motherboard.MaxRAMFrequencyMHz <= maxMemoryFreq);
                    }
                    if (filters.MaxWidth.HasValue && filters.MaxWidth != -1)
                    {
                        decimal width = filters.MaxWidth.Value;
                        query = query.Where(x => x.Motherboard.Width <= width);
                    }
                    if (filters.MaxHeight.HasValue && filters.MaxHeight != -1)
                    {
                        decimal height = filters.MaxHeight.Value;
                        query = query.Where(x => x.Motherboard.Height <= height);
                    }
                    if (filters.MaxSataSlots.HasValue && filters.MaxSataSlots != -1)
                    {
                        int maxSataSlots = filters.MaxSataSlots.Value;
                        query = query.Where(x => x.Motherboard.SATASlots <= maxSataSlots);
                    }
                    if (filters.MaxM2Slots.HasValue && filters.MaxM2Slots != -1)
                    {
                        int m2Slots = filters.MaxM2Slots.Value;
                        query = query.Where(x => x.Motherboard.M2Slots <= m2Slots);
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
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

