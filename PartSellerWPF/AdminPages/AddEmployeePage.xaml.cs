using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace PartSellerWPF.AdminPages
{
    /// <summary>
    /// Логика взаимодействия для AddEmployeePage.xaml
    /// </summary>
    public partial class AddEmployeePage : Page
    {
        public AddEmployeePage()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var context = Entities.GetContext();

            var query = from u in context.User
                        select new
                        {
                            User = u
                        };

            var result = query.AsEnumerable().Select(x => new UserData
            {
                Role = x.User.RoleID,
                Name = x.User.Name,
                Username = x.User.Email,
            }).Where(x => x.Username != AuthManager.CurrentUser.Email).ToList();

            dataGrid.ItemsSource = result;
            dataGrid.IsReadOnly = !(AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 3);
            dataGrid.CanUserAddRows = !(AuthManager.IsLoggedIn && AuthManager.CurrentUser.RoleID == 3);
        }

        private void dataGrid_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedItem = e.Row.Item as UserData;

                if (editedItem != null)
                {
                    try
                    {
                        var context = Entities.GetContext();

                        if (string.IsNullOrWhiteSpace(editedItem.Username))
                        {
                            MessageBox.Show("Email пользователя не может быть пустым",
                                          "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
                            e.Cancel = true;
                            return;
                        }

                        if (!editedItem.Username.Contains("@"))
                        {
                            MessageBox.Show("Некорректный email пользователя. Должен содержать @",
                                          "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
                            e.Cancel = true;
                            return;
                        }

                        if (editedItem.Role < 1 || editedItem.Role > 2)
                        {
                            MessageBox.Show("Некорректная роль пользователя (допустимо 1-2)",
                                          "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
                            e.Cancel = true;
                            return;
                        }

                        var user = context.User.FirstOrDefault(u => u.Email == editedItem.Username);
                        if (user == null)
                        {
                            MessageBox.Show("Пользователь не найден",
                                          "Ошибка",
                                          MessageBoxButton.OK,
                                          MessageBoxImage.Error);
                            e.Cancel = true;
                            return;
                        }

                        user.Name = editedItem.Name;
                        user.Email = editedItem.Username;
                        user.RoleID = editedItem.Role;

                        context.SaveChanges();

                        MessageBox.Show("Данные пользователя успешно обновлены",
                                      "Успех",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Information);

                        LoadData();
                    }
                    catch (DbEntityValidationException ex)
                    {
                        var errorMessages = ex.EntityValidationErrors
                            .SelectMany(x => x.ValidationErrors)
                            .Select(x => x.ErrorMessage);

                        MessageBox.Show($"Ошибки валидации:\n{string.Join("\n", errorMessages)}",
                                      "Ошибка валидации",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка сохранения: {ex.Message}\n\n{(ex.InnerException?.Message ?? "Нет дополнительной информации")}",
                                      "Ошибка",
                                      MessageBoxButton.OK,
                                      MessageBoxImage.Error);
                    }
                }
            }
        }

    }
}
