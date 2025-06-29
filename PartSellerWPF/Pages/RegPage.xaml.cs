﻿using System;
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
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        public RegPage()
        {
            InitializeComponent();
            Loaded += AuthPage_Loaded;
        }

        private void AuthPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoginTextBox.Focus();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            Entities db = Entities.GetContext();

            string name = NameTextBox.Text.Trim();
            string lastname = LastNameTextBox.Text.Trim();
            string login = LoginTextBox.Text.Trim().ToLower();
            string password = PasswordBox.Password;

            if (ConfirmPasswordBox.Password.Trim() != password)
            {
                ShowError("Пароли не совпадают");
                return;
            }

            if (string.IsNullOrEmpty(name))
            {
                ShowError("Введите имя");
                return;
            }

            if (string.IsNullOrEmpty(lastname))
            {
                ShowError("Введите фамилию");
                return;
            }

            if (string.IsNullOrEmpty(login))
            {
                ShowError("Введите электронную почту");
                return;
            }

            if (!login.Contains("@"))
            {
                MessageBox.Show("Некорректный email пользователя. Должен содержать @",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return;
            }

            if (!(login.Length > 5)) 
            {
                MessageBox.Show("Некорректный email пользователя.",
                              "Ошибка",
                              MessageBoxButton.OK,
                              MessageBoxImage.Error);
                return;
            }


            if (string.IsNullOrEmpty(password))
            {
                ShowError("Введите пароль");
                return;
            }

            if (password.Length < 10)
            {
                MessageBox.Show("Ненадежный пароль.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
                return;
            }

            if (db.User.Select(u => u).Where(u => u.Email == login).ToList().Count != 0)
            {
                MessageBox.Show("Пользователь с таким email уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                using (var context = new Entities())
                {
                    User newUser = new User
                    {
                        Name = name + " " + lastname,
                        Email = login,
                        Password = password,
                        RoleID = 1
                    };

                    context.User.Add(newUser);
                    context.SaveChanges();
                }

                NavigationService.Navigate(new AuthPage());
                NavigationService.RemoveBackEntry();
            }
            catch (System.Exception ex)
            {
                ShowError($"Ошибка регистрации: {ex.Message}");
            }
        }

        private void ShowError(string message)
        {
            ErrorTextBlock.Text = message;
            ErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
}
