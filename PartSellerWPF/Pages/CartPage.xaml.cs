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
    /// Логика взаимодействия для CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        public CartPage()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            var context = Entities.GetContext();
            var components = new List<CartItemsDto>();

            var cpuComponents = from cp in context.CPU
                                join b in context.Brand on cp.BrandID equals b.ID
                                join p in context.Part on cp.ID equals p.CPUID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = cp.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(cpuComponents);

            var gpuComponents = from gp in context.GPU
                                join b in context.Brand on gp.BrandID equals b.ID
                                join p in context.Part on gp.ID equals p.GPUID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = gp.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(gpuComponents);

            var supplyComponents = from s in context.Supply
                                join b in context.Brand on s.BrandID equals b.ID
                                join p in context.Part on s.ID equals p.SupplyID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = s.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(supplyComponents);

            var diskComponents = from d in context.Disk
                                join b in context.Brand on d.BrandID equals b.ID
                                join p in context.Part on d.ID equals p.DiskID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = d.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(diskComponents);

            var coolingComponents = from c in context.Cooling
                                join b in context.Brand on c.BrandID equals b.ID
                                join p in context.Part on c.ID equals p.CoolingID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = c.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(coolingComponents);

            var motherboardComponents = from m in context.Motherboard
                                join b in context.Brand on m.BrandID equals b.ID
                                join p in context.Part on m.ID equals p.MotherboardID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = m.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(motherboardComponents);

            var ramComponents = from r in context.RAM
                                join b in context.Brand on r.BrandID equals b.ID
                                join p in context.Part on r.ID equals p.RAMID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = r.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(ramComponents);

            var caseСomponents = from c in context.Case
                                join b in context.Brand on c.BrandID equals b.ID
                                join p in context.Part on c.ID equals p.CaseID
                                join pr in context.Product on p.ID equals pr.PartID
                                join oi in context.OrderItem on pr.ID equals oi.ProductID
                                join o in context.Order on oi.OrderID equals o.ID
                                join u in context.User on o.UserId equals u.ID
                                where o.UserId == AuthManager.CurrentUser.ID
                                select new CartItemsDto
                                {
                                    OrderItemId = oi.ID,
                                    ProductId = p.ID,
                                    Brand = b.Name,
                                    Name = c.Model,
                                    Price = pr.Price,
                                    Quantity = oi.Quantity,
                                    TotalPrice = o.TotalPrice,
                                    ImageUrl = p.Image,
                                    UserId = u.ID
                                };
            components.AddRange(caseСomponents);

            dataGrid.ItemsSource = components;
            totalPriceText.Text = context.Order.Where(x => x.UserId == AuthManager.CurrentUser.ID).FirstOrDefault().TotalPrice.ToString();
        }

        private void btnCheckout_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PaymentPage());
        }

        private void chkIsAssembly_Checked(object sender, RoutedEventArgs e)
        {

        }
        private void chkIsAssembly_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void DataGrid_CurrentCellChanged(object sender, System.EventArgs e)
        {
            Funcs.ExtendCell(dataGrid);
        }

    }
}
