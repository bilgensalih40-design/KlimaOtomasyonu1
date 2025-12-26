using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public abstract class ServiceTransaction : ISummary
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid CustomerId { get; set; }
        public Guid TechnicianId { get; set; }
        public string CustomerName { get; set; } // Müşteri Adı Soyadı
        public string DeviceName { get; set; }   // Klima Marka Model
        public DateTime Date { get; set; } = DateTime.Now;
        public string Description { get; set; }
        public decimal TotalPrice { get; set; }
        public abstract string GetSummary();

        // Abstract Metod: Gövdesi yok. Alt sınıflar bunu EZMEK (Override) zorunda.
        public abstract void CalculatePricing();
    }
}
