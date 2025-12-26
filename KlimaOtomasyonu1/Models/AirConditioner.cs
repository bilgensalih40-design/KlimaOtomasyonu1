using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    internal class AirConditioner
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Brand { get; set; }    // Marka
        public string Model { get; set; }    // Model
        public string BTU { get; set; }      // Kapasite (9000, 12000 vs.)
        public Guid CustomerId { get; set; } // Hangi müşteriye ait
    }
}
