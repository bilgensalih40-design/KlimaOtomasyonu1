using System;

namespace KlimaOtomasyonu1.Models
{
    public class ServiceDisplayModel
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string CustomerName { get; set; }
        public string DeviceName { get; set; }
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }

        // Diğer detaylar
        public decimal PartCost { get; set; }
        public decimal LaborCost { get; set; }
        public int BTUSize { get; set; }
    }
}