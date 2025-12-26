using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1.Models
{
    // Bu arayüzü uygulayan her sınıf, özet bilgi vermek ZORUNDADIR.
    public interface ISummary
    {
        string GetSummary();
    }
}