using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PartSellerWPF
{
    class Funcs
    {
        public static int GetProductIdByComponentDto(ComponentDto component)
        {
            var context = Entities.GetContext();
            Part part = null;

            switch (component.Type)
            {
                case "CPU":
                    part = context.Part.FirstOrDefault(p => p.CPUID == component.Id);
                    break;
                case "GPU":
                    part = context.Part.FirstOrDefault(p => p.GPUID == component.Id);
                    break;
                case "Motherboard":
                    part = context.Part.FirstOrDefault(p => p.MotherboardID == component.Id);
                    break;
                case "RAM":
                    part = context.Part.FirstOrDefault(p => p.RAMID == component.Id);
                    break;
                case "Disk":
                    part = context.Part.FirstOrDefault(p => p.DiskID == component.Id);
                    break;
                case "Power Supply":
                    part = context.Part.FirstOrDefault(p => p.SupplyID == component.Id);
                    break;
                case "Cooling":
                    part = context.Part.FirstOrDefault(p => p.CoolingID == component.Id);
                    break;
                case "Case":
                    part = context.Part.FirstOrDefault(p => p.CaseID == component.Id);
                    break;
                default:
                    throw new Exception($"Неизвестный тип компонента: {component.Type}");
            }

            if (part == null)
            {
                throw new Exception($"Компонент не найден в таблице Part (Type: {component.Type}, ID: {component.Id})");
            }

            var product = context.Product.FirstOrDefault(p => p.PartID == part.ID);

            if (product == null)
            {
                throw new Exception($"Продукт не найден для PartID: {part.ID}");
            }

            return product.ID;

        }

        public static void AddComponentToOrder(dynamic selectedComponent)
        {
            if (!AuthManager.IsLoggedIn)
            {
                MessageBox.Show("Необходимо авторизоваться");
                return;
            }

            var context = Entities.GetContext();

            var currentOrder = context.Order
                .Where(o => o.UserId == AuthManager.CurrentUser.ID)
                .OrderByDescending(o => o.Date)
                .FirstOrDefault(or => or.Status == "Корзина");

            if (currentOrder == null)
            {
				currentOrder = new Order
				{
					UserId = AuthManager.CurrentUser.ID,
					Date = DateTime.Now,
					Status = "Корзина",
					TotalPrice = 0
				};
				context.Order.Add(currentOrder);
				context.SaveChanges();
			}

            int selectedPartID = selectedComponent.ID;
            var existingItem = currentOrder.OrderItem?
                    .FirstOrDefault(oi => oi.ProductID == context.Product.FirstOrDefault(x => x.PartID == selectedPartID).ID);

            int selectedID = selectedComponent.PartID;
            var partInStock = context.Part
                .FirstOrDefault(p => p.ID == selectedID);

            if (currentOrder != null)
            {
                currentOrder.TotalPrice += selectedComponent.Price;

                if (existingItem != null)
                {
                    if (partInStock != null && existingItem.Quantity + 1 > partInStock.QuantityInStock)
                    {
                        MessageBox.Show($"Недостаточно товара на складе. Доступно: {partInStock.QuantityInStock}");
                        return;
                    }

                    existingItem.Quantity++;
                }
                else
                {
                    var orderItem = new OrderItem
                    {
                        OrderID = currentOrder.ID,
                        ProductID = context.Product.Where(x => x.PartID == selectedID).FirstOrDefault().ID,
                        Quantity = 1
                    };

                    context.OrderItem.Add(orderItem);
                }
            }
            else
            {
                if (partInStock.QuantityInStock < 1)
                {
                    MessageBox.Show("Недостаточно товара на складе");
                    return;
                }

                currentOrder = new Order
                {
                    UserId = AuthManager.CurrentUser.ID,
                    TotalPrice = selectedComponent.Price,
                    Status = "Cart",
                    Date = DateTime.Now
                };

                context.Order.Add(currentOrder);
                context.SaveChanges();

                var orderItem = new OrderItem
                {
                    OrderID = currentOrder.ID,
                    ProductID = selectedComponent.ID,
                    Quantity = 1
                };

                context.OrderItem.Add(orderItem);
            }

            context.SaveChanges();
            MessageBox.Show($"Компонент успешно добавлен в заказ.");

        }

        public static void ExtendCell(DataGrid dataGrid)
        {
            if (dataGrid.CurrentColumn != null)
            {
                foreach (var column in dataGrid.Columns)
                {
                    if (column.Header.ToString() != "Изображение")
                        column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                }

                dataGrid.CurrentColumn.Width = new DataGridLength(3, DataGridLengthUnitType.Star);
                dataGrid.ScrollIntoView(dataGrid.CurrentItem, dataGrid.CurrentColumn);
            }
        }

        public static void DeleteComponent(IDeletableComponent component)
        {

            if (AuthManager.CurrentUser.RoleID != 2)
            {
                return;
            }

            try
            {
                var context = Entities.GetContext();

                var part = context.Part.FirstOrDefault(p => p.ID == component.ID);
                if (part == null) return;

                var product = context.Product.FirstOrDefault(p => p.PartID == part.ID);
                if (product != null && context.OrderItem.Any(oi => oi.ProductID == product.ID))
                {
                    MessageBox.Show($"Невозможно удалить {component.ComponentType}, так как он есть в заказах",
                                    "Ошибка",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                    return;
                }

                switch (component.ComponentType)
                {
                    case "Case":
                        var computerCase = context.Case.FirstOrDefault(c => c.ID == part.CaseID);
                        if (computerCase != null) context.Case.Remove(computerCase);
                        break;

                    case "RAM":
                        var ram = context.RAM.FirstOrDefault(r => r.ID == part.RAMID);
                        if (ram != null) context.RAM.Remove(ram);
                        break;

                    case "Motherboard":
                        var motherboard = context.Motherboard.FirstOrDefault(g => g.ID == part.MotherboardID);
                        if (motherboard != null) context.Motherboard.Remove(motherboard);
                        break;
                    case "GPU":
                        var gpu = context.GPU.FirstOrDefault(g => g.ID == part.GPUID);
                        if (gpu != null) context.GPU.Remove(gpu);
                        break;
                    case "Supply":
                        var supply = context.Supply.FirstOrDefault(g => g.ID == part.SupplyID);
                        if (supply != null) context.Supply.Remove(supply);
                        break;
                    case "CPU":
                        var cpu = context.CPU.FirstOrDefault(g => g.ID == part.CPUID);
                        if (cpu != null) context.CPU.Remove(cpu);
                        break;
                    case "Disk":
                        var disk = context.Disk.FirstOrDefault(g => g.ID == part.DiskID);
                        if (disk != null) context.Disk.Remove(disk);
                        break;
                    case "Cooling":
                        var cooling = context.Cooling.FirstOrDefault(g => g.ID == part.CoolingID);
                        if (cooling != null) context.Cooling.Remove(cooling);
                        break;


                    default:
                        throw new InvalidOperationException($"Неизвестный тип компонента: {component.ComponentType}");
                }

                if (product != null) context.Product.Remove(product);
                context.Part.Remove(part);

                context.SaveChanges();

                MessageBox.Show($"{component.ComponentType} успешно удален",
                                "Успех",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении {component.ComponentType}: {ex.Message}",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }
        
    }
}
