using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid(); // Benzersiz ID otomatik oluşur
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string Role { get; set; } // "Admin" veya "Technician"
    }
}
