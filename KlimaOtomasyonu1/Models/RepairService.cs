using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class RepairService : ServiceTransaction
    {
        public override string GetSummary()
        {
            return $"[TAMİR] {Date.ToShortDateString()} - Parça Değişimi: {PartCost} TL, İşçilik: {LaborCost} TL. Toplam: {TotalPrice} TL";
        }
        public decimal PartCost { get; set; } // Parça maliyeti
        public decimal LaborCost { get; set; } // İşçilik

        public override void CalculatePricing()
        {
            // Tamir = Parça + İşçilik
            this.TotalPrice = PartCost + LaborCost;
        }
    }
}
