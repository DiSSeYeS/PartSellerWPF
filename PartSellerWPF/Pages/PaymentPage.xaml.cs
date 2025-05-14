using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для PaymentPage.xaml
    /// </summary>
    public partial class PaymentPage : Page
    {
        private static int staticOrderId;
        public PaymentPage(int orderId = -1)
        {
            staticOrderId = orderId;

            InitializeComponent();
            LoadData(orderId);
        }

        private void LoadData(int orderId)
        {
            if (AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 2)
            {
                dataGrid.CanUserAddRows = true;
                dataGrid.CanUserDeleteRows = true;
            }

            var context = Entities.GetContext();
            bool isNewPayment = context.Payment.Where(x => x.OrderID == orderId).ToList().Count > 0;

            if (!isNewPayment)
            {
                var newPayment = new Payment();

                newPayment.OrderID = orderId;
                newPayment.Amount = context.Order.Where(x => x.ID == orderId).FirstOrDefault().TotalPrice;
                newPayment.Status = "Не оплачен";

                context.Payment.Add(newPayment);
                context.SaveChanges();
            }

            var query = from o in context.Order
                        join oi in context.OrderItem on o.ID equals oi.OrderID
                        join p in context.Payment on o.ID equals p.OrderID
                        where o.UserId == AuthManager.CurrentUser.ID
                        select new
                        {
                            OrderId = o.ID,
                            Amount = p.Amount,
                            Status = p.Status,
                            OrderStatus = o.Status,
                            Date = o.Date
                        };

            dataGrid.ItemsSource = query.ToList().GroupBy(x => x.OrderId);
        }

        private void CardNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void CardNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;

            var text = textBox.Text.Replace(" ", "");
            if (text.Length > 16) text = text.Substring(0, 16);

            var formattedText = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (i > 0 && i % 4 == 0)
                    formattedText += " ";
                formattedText += text[i];
            }

            if (textBox.Text != formattedText)
            {
                textBox.Text = formattedText;
                textBox.CaretIndex = formattedText.Length;
            }
        }

        private void ExpiryDate_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;

            if (!char.IsDigit(e.Text, 0))
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length == 2 && textBox.SelectedText.Length == 0)
            {
                textBox.Text += "/";
                textBox.CaretIndex = 3;
            }
        }

        private void ExpiryDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox.Text.Length == 2 && !textBox.Text.Contains("/"))
            {
                textBox.Text += "/";
                textBox.CaretIndex = 3;
            }
        }

        private void Cvv_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !char.IsDigit(e.Text, 0);
        }

        private void CardHolder_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c) && c != ' ' && c != '\'')
                {
                    e.Handled = true;
                    return;
                }
            }
        }
        private void Pay(int orderId)
        {

        }
        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            var context = Entities.GetContext();

            if (!ValidatePaymentData())
                return;

            try
            {
                var paymentStatus = context.Payment.Where(x => x.OrderID == staticOrderId).FirstOrDefault().Status;
                var orderStatus = context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().Status;

                if (paymentStatus != "Подтверждение")
                {
                    context.Payment.Where(x => x.OrderID == staticOrderId).FirstOrDefault().Status = "Подтверждение";
                    context.Order.Where(x => x.ID == staticOrderId).FirstOrDefault().Status = "В обработке";

                    context.SaveChanges();
                    LoadData(staticOrderId);
                    return;
                }

                MessageBox.Show("Заказ уже оплачен, ожидается подтверждение менеджера", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при оплате: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool ValidatePaymentData()
        {
            var cardNumber = CardNumberTextBox.Text.Replace(" ", "");
            if (cardNumber.Length != 16 || !long.TryParse(cardNumber, out _))
            {
                MessageBox.Show("Введите корректный 16-значный номер карты", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Regex.IsMatch(ExpiryDateTextBox.Text, @"^(0[1-9]|1[0-2])\/?([0-9]{2})$"))
            {
                MessageBox.Show("Введите срок действия в формате MM/YY", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (CvvTextBox.Text.Length != 3 || !int.TryParse(CvvTextBox.Text, out _))
            {
                MessageBox.Show("Введите корректный CVV код (3 цифры)", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (string.IsNullOrWhiteSpace(CardHolderTextBox.Text))
            {
                MessageBox.Show("Введите имя владельца карты", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

    }
}
