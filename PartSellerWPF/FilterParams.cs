using System;
using System.Collections.Generic;
using System.Linq;
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
        public int? MaxFreq { get; set; }
        public int? MaxCount { get; set; }
    }
}
