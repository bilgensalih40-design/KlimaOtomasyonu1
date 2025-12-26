using FireSharp.Config;
using FireSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlimaOtomasyonu1
{
    public static class ConnectionProvider
    {
        // Buraya kendi Firebase bilgilerini gireceksin
        public static IFirebaseConfig config = new FirebaseConfig
        {
            // Örnek: "https://klimaotomasyonu-default-rtdb.firebaseio.com/"
            BasePath = "https://klimaotomasyonudb-default-rtdb.europe-west1.firebasedatabase.app/",

            // Örnek: "A1b2C3d4E5..." (Uzun şifre)
            AuthSecret = "AsdndoQESPUoZ42UNDbQgcNrlNltgYn0NSGpSwQ8"
        };

        public static IFirebaseClient client;

        // Bağlantıyı başlatan metod
        public static IFirebaseClient GetClient()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch (System.Exception)
            {
                // Bağlantı hatası olursa null döner, kontrol ederiz.
                return null;
            }
            return client;
        }
    }
}
