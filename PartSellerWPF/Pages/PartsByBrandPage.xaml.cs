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
    /// Логика взаимодействия для PartsByBrandPage.xaml
    /// </summary>
    public partial class PartsByBrandPage : Page
    {
        public PartsByBrandPage()
        {
            InitializeComponent();
        }

        public List<object> GetAllPartsByBrand(string brandName)
        {
            using (var db = Entities.GetContext())
            {
                var brandId = db.Brand
                              .Where(b => b.Name == brandName)
                              .Select(b => b.ID)
                              .FirstOrDefault();

                if (brandId == 0) return new List<object>();

                var result = new List<object>();

                result.AddRange(db.Motherboard
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.CPU
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.GPU
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.RAM
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.Supply
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.Disk
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.Cooling
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                result.AddRange(db.Case
                                .Where(s => s.BrandID == brandId)
                                .ToList());

                return result;
            }
        }
    }
}
