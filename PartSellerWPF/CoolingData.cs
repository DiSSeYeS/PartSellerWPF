using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class CoolingData : IDeletableComponent
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public int RPM { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public int ProductID { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }

        public string ComponentType => "Cooling";
    }
}
