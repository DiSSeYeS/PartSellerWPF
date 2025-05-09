using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartSellerWPF
{
    public interface IDeletableComponent
    {
        int ID { get; }
        int PartID { get; }
        string Image { get; }
        string Model { get; }
        string ComponentType { get; }
    }
}
