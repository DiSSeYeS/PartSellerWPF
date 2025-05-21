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
    /// Логика взаимодействия для OrdersHistoryPage.xaml
    /// </summary>
    public partial class OrdersHistoryPage : Page
    {
        public OrdersHistoryPage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var context = Entities.GetContext();

            var query = from p in context.Payment
                        join o in context.Order on p.OrderID equals o.ID
                        where o.UserId == AuthManager.CurrentUser.ID
                        select new
                        {
                            OrderId = o.ID,
                            PaymentId = p.ID,
                            Amount = p.Amount,
                            Status = p.Status,
                            OrderStatus = o.Status,
                            Date = o.Date
                        };

            dataGrid.ItemsSource = query.ToList();
        }
    }
}
