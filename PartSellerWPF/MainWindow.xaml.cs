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
        private static string currentPage;
        public MainWindow()
        {
            InitializeComponent();   
        }

        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page && currentPage != page.Name)
            {
                this.Title = $"ComponentSeller - {page.Title}";
                // btnBack.Visibility = page is Pages.(?) ? Visibility.Hidden : Visibility.Visible;
                currentPage = page.Name;

                btnCatalog.Visibility = e.Content == new Pages.CatalogPage() ? Visibility.Hidden : Visibility.Visible;
                btnCart.Visibility = e.Content == new Pages.CartPage() ? Visibility.Hidden : Visibility.Visible;
                btnBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;
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
            MainFrame.Navigate(new Pages.CartPage());
        }

        private void btnCatalog_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new Pages.CatalogPage());
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
            switch (currentPage)
            {
                case "RAMPage":

                    MainFrame.Navigate(new FilterPages.RAMFilterPage());

                    break;
                case "CasePage":

                    MainFrame.Navigate(new FilterPages.CaseFilterPage());

                    break;
                case "SupplyPage":

                    MainFrame.Navigate(new FilterPages.SupplyFilterPage());

                    break;
                case "CoolingPage":

                    MainFrame.Navigate(new FilterPages.CoolingFilterPage());

                    break;
                case "CPUPage":

                    MainFrame.Navigate(new FilterPages.CPUFilterPage());

                    break;
                case "DiskPage":

                    MainFrame.Navigate(new FilterPages.DiskFilterPage());

                    break;
                case "GPUPage":

                    MainFrame.Navigate(new FilterPages.GPUFilterPage());

                    break;
                case "MotherboardPage":

                    MainFrame.Navigate(new FilterPages.MotherboardFilterPage());

                    break;
                default:

                    MessageBox.Show("Пожалуйста, выберите страницу.");

                    break;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
