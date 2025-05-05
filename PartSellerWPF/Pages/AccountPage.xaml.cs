using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        private Entities dbContext;

        public AccountPage()
        {
            InitializeComponent();
            dbContext = Entities.GetContext();
            LoadUserData();
        }

        private void LoadUserData()
        {
            try
            {
                var user = dbContext.User.Find(AuthManager.CurrentUser?.ID);

                if (user != null)
                {
                    var nameParts = user.Name.Split(' ');
                    if (nameParts.Length >= 2)
                    {
                        FirstNameTextBox.Text = nameParts[0];
                        LastNameTextBox.Text = nameParts[1];
                    }
                    else
                    {
                        FirstNameTextBox.Text = user.Name;
                        LastNameTextBox.Text = string.Empty;
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки данных: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            string currentPassword = CurrentPasswordBox.Password;
            string newPassword = NewPasswordBox.Password;
            string confirmPassword = ConfirmPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(newPassword) ||
                string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Заполните поля паролей.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword != confirmPassword)
            {
                MessageBox.Show("Пароли не совпадают.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (newPassword.Length < 6)
            {
                MessageBox.Show("Пароль должен содержать минимум 6 символов.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                var user = dbContext.User.Find(AuthManager.CurrentUser?.ID);

                if (user.Password != currentPassword)
                {
                    MessageBox.Show("Неправильный текущий пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                user.SetPassword(newPassword);
                dbContext.SaveChanges();

                AuthManager.Login(user);

                MessageBox.Show("Пароль успешно изменен!", "Успешно", MessageBoxButton.OK, MessageBoxImage.Information);
                NewPasswordBox.Password = string.Empty;
                ConfirmPasswordBox.Password = string.Empty;
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"Ошибка изменения пароля: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
