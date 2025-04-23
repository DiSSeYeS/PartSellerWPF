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
        public MotherboardPage()
        {
            InitializeComponent();
        }

        public List<Motherboard> GetMotherboardsBySocket(string socketName)
        {
            using (var db = Entities.GetContext())
            {
                return db.Motherboard
                    .Join(db.Socket,
                          m => m.SocketID,
                          s => s.ID,
                          (m, s) => new { Motherboard = m, Socket = s })
                    .Where(x => x.Socket.Name == socketName)
                    .Select(x => x.Motherboard)
                    .ToList();
            }
        }
    }
}
