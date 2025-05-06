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
            var context = Entities.GetContext();

                var currentOrder = context.Order
                    .Where(o => o.UserId == AuthManager.CurrentUser.ID)
                    .OrderByDescending(o => o.Date)
                    .FirstOrDefault();

                var existingItem = currentOrder.OrderItem?
                        .FirstOrDefault(oi => oi.ProductID == selectedComponent.ID);

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
                            ProductID = selectedComponent.ID,
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
                MessageBox.Show("Компонент успешно добавлен в заказ");

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
    }
}
