using System;

namespace KlimaOtomasyonu1.Models
{
    public class StockItem
    {
        public Guid Id { get; set; }           // Benzersiz Kimlik
        public string ProductName { get; set; } // Parça Adı (Örn: R410 Gaz, Bakır Boru)
        public int Quantity { get; set; }       // Stok Adedi (Depoda kaç tane var?)
        public decimal PurchasePrice { get; set; } // Alış Fiyatı (Maliyet hesabı için)
        public decimal SalePrice { get; set; }     // Satış Fiyatı (Müşteriye kaça satacağız?)
        public string Unit { get; set; }        // Birim (Adet, KG, Metre)
        public int CriticalLevel { get; set; }  // Kritik Seviye (Bunun altına düşerse uyar)
    }
}