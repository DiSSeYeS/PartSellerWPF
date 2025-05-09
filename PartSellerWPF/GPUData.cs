using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class GPUData : IDeletableComponent
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Voltage { get; set; }
        public int VideoMemory { get; set; }
        public int MemoryFreq { get; set; }
        public int CoreFreq { get; set; }
        public int MaxRAMCount { get; set; }
        public int MaxRAMFreq { get; set; }
        public int Height { get; set; }
        public int Length { get; set; }
        public int Width { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }

        public string ComponentType => "GPU";
    }
}
