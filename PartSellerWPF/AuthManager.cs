using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PartSellerWPF
{
    class AuthManager
    {
        public static bool IsLoggedIn { get; private set; }
        public static User CurrentUser { get; private set; }

        public static void Login(User user)
        {
            CurrentUser = user;
            IsLoggedIn = true;
        }

        public static void Logout()
        {
            CurrentUser = null;
            IsLoggedIn = false;
        }

        public static User AuthUser(string login, string password)
        {
            using (var db = Entities.GetContext())
            {
                var user = db.User.FirstOrDefault(u => u.Name == login);
                if (user != null && user.CheckPassword(password))
                {
                    MessageBox.Show($"Пользователь {login} авторизован");

                    return user;
                }
                MessageBox.Show($"Неверный логин или пароль");

                return null;
            }
        }

        public static User RegUser()
        {
            return new User();
        }

    }
}
