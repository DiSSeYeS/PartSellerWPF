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

namespace PartSellerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page)
            {
                this.Title = $"ComponentSeller - {page.Title}";
                // btnBack.Visibility = page is Pages.PartnersPage ? Visibility.Hidden : Visibility.Visible;
            }
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            if (AuthManager.IsLoggedIn)
            {
                MainFrame.Navigate(new Pages.AccountPage());
            }
            else
            {
                MainFrame.Navigate(new Pages.AuthPage());
            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCatalog_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void btnFilters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
