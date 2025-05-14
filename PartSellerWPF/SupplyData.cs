using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class SupplyData : IDeletableComponent
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Wattage { get; set; }
        public double Height { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public int ProductID { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string FormFactor { get; set; }
        public bool Permission { get; set; }
        public bool CanEdit { get; set; }

        public string ComponentType => "Supply";
    }
}
