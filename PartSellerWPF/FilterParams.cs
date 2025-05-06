using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public class FilterParams
    {
        public int? BrandId { get; set; }
        public int? FormFactorId { get; set; }
        public decimal? MaxHeight { get; set; }
        public decimal? MaxWidth { get; set; }
        public decimal? MaxLength { get; set; }
        public decimal? MaxGpuLength { get; set; }
        public decimal? MaxCoolerHeight { get; set; }
        public decimal? MaxPsuLength { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MaxRAMGB { get; set; }
        public decimal? MaxFreq { get; set; }
        public int? MaxCount { get; set; }
        public int? SocketId { get; set; }
        public int? CoolerTypeId { get; set; }
        public int? MaxCores { get; set; }
        public int? MaxThreads { get; set; }
        public int? MaxL1 { get; set; }
        public int? MaxL2 { get; set; }
        public decimal? MaxMaxFreq { get; set; }
        public int? MaxVoltage { get; set; }
        public int? DiskTypeId { get; set; }
        public int? MaxVideoMemory { get; set; }
        public int? MaxMemoryFreq { get; set; }
        public int? MaxCoreFreq { get; set; }
        public int? RamTypeId { get; set; }
        public int? ChipsetId { get; set; }
        public int? MaxRamSlots { get; set; }
        public int? MaxSataSlots { get; set; }
        public int? MaxM2Slots { get; set; }
        public int? MaxWattage { get; set; }

    }
}
