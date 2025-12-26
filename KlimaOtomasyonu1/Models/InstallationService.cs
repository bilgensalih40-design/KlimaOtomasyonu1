using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class InstallationService : ServiceTransaction
    {
        public override string GetSummary()
        {
            return $"[MONTAJ] {BTUSize} BTU kapasiteli cihaz montajı yapıldı. Tutar: {TotalPrice} TL";
        }
        public int BTUSize { get; set; } // Klimanın büyüklüğü

        public override void CalculatePricing()
        {
            // Örnek: Büyük klimaların montajı daha pahalı
            if (BTUSize > 18000)
                this.TotalPrice = 3000;
            else
                this.TotalPrice = 2000;
        }
    }
}
