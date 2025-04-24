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

namespace PartSellerWPF.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        public CatalogPage()
        {
            InitializeComponent();
        }

        private void btnCPU_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CPUPage());
        }

        private void btnGPU_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new GPUPage());
        }

        private void btnRAM_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new RAMPage());
        }

        private void btnMotherboard_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new MotherboardPage());
        }

        private void btnSupply_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SupplyPage());
        }

        private void btnCase_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CasePage());
        }

        private void btnDisk_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new DiskPage());
        }

        private void btnCooling_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new CoolingPage());
        }
    }
}
