using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class MaintenanceService : ServiceTransaction
    {
        public override string GetSummary()
        {
            return $"[BAKIM] {Date.ToShortDateString()} tarihinde periyodik bakım yapıldı. Tutar: {TotalPrice} TL";
        }
        public decimal LaborCost { get; set; }
        public override void CalculatePricing()
        {
            // Sabit 1500'ü kaldırdık. Artık kutuya ne yazarsan fiyat o olacak.
            this.TotalPrice = LaborCost;

        }
    }
}
