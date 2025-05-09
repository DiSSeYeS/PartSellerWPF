using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class RAMData : IDeletableComponent
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string RAMType { get; set; }
        public int RAMCount { get; set; }
        public int RAMFreq { get; set; }
        public int RAMQuantity { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public string ComponentType => "RAM";
    }
}
