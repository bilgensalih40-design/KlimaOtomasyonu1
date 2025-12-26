using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class Technician : User
    {
        public string CurrentStatus { get; set; } = "Musait"; // Müsait, Meşgul

        public Technician()
        {
            Role = "Technician";
        }
    }
}
