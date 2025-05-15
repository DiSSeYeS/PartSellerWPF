using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

        private static Dictionary<string, int> status = new Dictionary<string, int>()
        {
            {"Подтвержден",1 },
            {"Передан в доставку",2},
            {"В пути",3},
            {"Ожидает получения",4},
            {"Получен",5},
        };
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
                            OrderStatus = o.Status,
                            Date = o.Date 
                        };

            dataGrid.ItemsSource = query.ToList();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
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

                if (payment.Status.Equals("Подтверждение"))
                {
                    payment.Status = "Оплачен";
                    order.Status = "Подтвержден";
                    order.Date = DateTime.Now;

                    context.SaveChanges();
                    LoadData();

                    MessageBox.Show("Статус успешно изменен на 'Оплачен'");
                    return;
                }

                MessageBox.Show("Невозможно изменить статус неоплаченного заказа.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }

        private void ButtonGet_Click(object sender, RoutedEventArgs e)
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

                if (status.ContainsKey(order.Status))
                {
                    switch (status[order.Status])
                    {
                        case 4:

                            order.Status = "Получен";
                            order.Date = DateTime.Now;

                            break;

                        default:

                            MessageBox.Show("Невозможно установить этот статус");

                            return;
                    }
                }

                context.SaveChanges();
                LoadData();

                MessageBox.Show("Статус успешно изменен на 'Оплачен'");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
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

                if (order.Status != "Получен")
                {
                    order.Status = "Отменён";
                    order.Date = DateTime.Now;

                    context.SaveChanges();
                    LoadData();

                    MessageBox.Show("Статус успешно изменен на 'Отменён'");
                    return;
                }

                MessageBox.Show("Невозможно отменить завершённый заказ.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }

        private void ButtonDelivery_Click(object sender, RoutedEventArgs e)
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

                if (status.ContainsKey(order.Status))
                {
                    switch (status[order.Status])
                    {
                        case 1:

                            order.Status = "Передан в доставку";
                            order.Date = DateTime.Now;

                            break;
                        case 2:

                            order.Status = "В пути";
                            order.Date = DateTime.Now;

                            break;
                        case 3:

                            order.Status = "Ожидает получения";
                            order.Date = DateTime.Now;

                            break;
                        default:

                            MessageBox.Show("Заказ уже доставлен");

                            return;
                    }
                }

                context.SaveChanges();
                LoadData();

                MessageBox.Show("Статус успешно изменен");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении статуса: {ex.Message}");
            }
        }
    }
}
