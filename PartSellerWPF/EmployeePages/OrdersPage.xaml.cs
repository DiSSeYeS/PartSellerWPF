using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PartSellerWPF.EmployeePages
{
    /// <summary>
    /// Логика взаимодействия для OrdersPage.xaml
    /// </summary>
    public partial class OrdersPage : Page
    {
        public OrdersPage()
        {
            InitializeComponent();
            LoadData();

        }

        private void LoadData()
        {
            var context = Entities.GetContext();

            var query = from o in context.Order
                        join p in context.Payment on o.ID equals p.OrderID
                        select new
                        {
                            OrderId = o.ID,
                            PaymentId = p.ID,
                            Amount = p.Amount,
                            Status = p.Status,
                            Date = o.Date 
                        };

            dataGrid.ItemsSource = query.ToList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Пожалуйста, выберите заказ для подтверждения");
                return;
            }

            dynamic selectedItem = dataGrid.SelectedItem;
            int paymentId = selectedItem.PaymentId;
            int orderId = selectedItem.OrderId;

            try
            {
                var context = Entities.GetContext();
                var payment = context.Payment.FirstOrDefault(p => p.ID == paymentId);
                var order = context.Order.FirstOrDefault(o => o.ID == orderId);

                if (payment == null)
                {
                    MessageBox.Show("Платеж не найден");
                    return;
                }

                payment.Status = "Оплачен";
                order.Status = "Завершен";
                order.Date = DateTime.Now;

                context.SaveChanges();
                LoadData();

                MessageBox.Show("Статус успешно изменен на 'Оплачен'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }
    }
}
