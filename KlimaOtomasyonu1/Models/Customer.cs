using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class Customer:ISummary
    {
        public string GetSummary()
        {
            return $"Müşteri: {FullName} - Tel: {PhoneNumber} - Adres: {Address}";
        }

        public Guid Id { get; set; } = Guid.NewGuid();
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
