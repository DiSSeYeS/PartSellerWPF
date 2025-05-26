using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class CPUData : IDeletableComponent
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Socket { get; set; }
        public int Voltage { get; set; }
        public int CoreFreq { get; set; }
        public int Cores { get; set; }
        public int Threads { get; set; }
        public int L1 { get; set; }
        public int L2 { get; set; }
        public int HasTurboBoost { get; set; }
        public int MaxFreq { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public int ProductID { get; set; }
        public string Image { get; set; }
        public int QuantityInStock { get; set; }
        public decimal Price { get; set; }

        public string ComponentType => "CPU";
    }
}
