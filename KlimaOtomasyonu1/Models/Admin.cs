using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    public class Admin : User
    {
        public Admin()
        {
            Role = "Admin";
        }
        // İleride admine özel property gerekirse buraya eklenir
    }
}
