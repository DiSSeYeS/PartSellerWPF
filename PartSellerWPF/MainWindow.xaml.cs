using System;
using System.Collections.Generic;
using System.IO.Packaging;
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

namespace PartSellerWPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static string currentPage;
        private static List<string> PagesNames = new List<string>
        {
            "CasePage", "CoolingPage", "CPUPage", "DiskPage",
            "GPUPage", "MotherboardPage", "RAMPage", "SupplyPage"
        };
        public MainWindow()
        {
            InitializeComponent();
        }
        
        private void MainFrame_Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content is Page page && !page.Title.Equals(currentPage))
            {
                this.Title = $"ComponentSeller - {page.Title}";
                currentPage = page.Title;

                // btnCart.Visibility = currentPage == "CartPage" ? Visibility.Hidden : Visibility.Visible;
                btnBack.Visibility = MainFrame.CanGoBack ? Visibility.Visible : Visibility.Hidden;
                btnFilters.Visibility = PagesNames.Contains(currentPage) || currentPage.EndsWith("FilterPage") ? Visibility.Visible : Visibility.Hidden;
                btnLogout.Visibility = currentPage == "AccountPage" && AuthManager.CurrentUser != null ? Visibility.Visible : Visibility.Hidden;
                // btnApplyFilters.Visibility = currentPage.EndsWith("FilterPage") ? Visibility.Visible : Visibility.Hidden;
            }
        }

        private void btnAccount_Click(object sender, RoutedEventArgs e)
        {
            if (AuthManager.IsLoggedIn)
            {
                if (!currentPage.Equals("AccountPage"))
                {
                    MainFrame.Navigate(new Pages.AccountPage());
                }
            }
            else
            {
                if (!currentPage.Equals("AuthPage"))
                {
                    MainFrame.Navigate(new Pages.AuthPage());
                }
            }
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            if (!currentPage.Equals("CartPage"))
            {
                if (AuthManager.IsLoggedIn)
                {
                    MainFrame.Navigate(new Pages.CartPage());
                    return;
                }

                MessageBox.Show("Необходимо авторизоваться.");
            }
        }

        private void btnCatalog_Click(object sender, RoutedEventArgs e)
        {
            if (!currentPage.Equals("CatalogPage"))
            {
                MainFrame.Navigate(new Pages.CatalogPage());
            }
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

                    if (currentPage.EndsWith("FilterPage"))
                    {
                        MessageBox.Show("Вы уже на странице фильтров.");
                    }
                    else
                    {
                        MessageBox.Show("Пожалуйста, выберите страницу.");
                    }
                        
                    break;
            }
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            IEnumerable<string> text = SearchTextBox.Text.Split(' ');

            FilterParams filterParams = new FilterParams()
            {
                Search = text
            };

            MainFrame.Navigate(new Pages.SearchPage(filterParams));

        }

        private void btnApplyFilters_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            AuthManager.Logout();

            while (MainFrame.NavigationService.CanGoBack)
            {
                MainFrame.NavigationService.RemoveBackEntry();
            }

            MainFrame.Navigate(new Pages.CatalogPage());
        }

    }
}
