using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class MotherboardData : IDeletableComponent
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Socket { get; set; }
        public string Chipset { get; set; }
        public string RAMType { get; set; }
        public int RAMSlots { get; set; }
        public int MaxRAMCount { get; set; }
        public int MaxRAMFreq { get; set; }
        public double Height { get; set; }
        public double Width { get; set; }
        public int SATASlots { get; set; }
        public int M2Slots { get; set; }
        public int NVMe {  get; set; }
        public string FormFactor { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public string ComponentType => "Motherboard";
    }
}
