using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KlimaOtomasyonu1
{

    public partial class SplashForm : Form
    {

        public SplashForm()
        {
            InitializeComponent();
            timerFade.Start(); // Form açılınca "görünürlük" animasyonu başlasın
          
        }

        private void timerFade_Tick(object sender, EventArgs e)
        {
            if (this.Opacity < 1)
            {
                this.Opacity += 0.05; // Görünürlüğü artır
            }
            else
            {
                timerFade.Stop();
                timerProgress.Start(); // Tamamen görünür olunca yükleme barı başlasın
            }
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            if (progressBar1.Value < 100)
            {
                progressBar1.Value += 2; // Barı doldur

                // Profesyonel görünüm için durum metnini değiştir
                if (progressBar1.Value == 20) lblStatus.Text = "Veritabanına bağlanılıyor...";
                if (progressBar1.Value == 50) lblStatus.Text = "Stok verileri çekiliyor...";
                if (progressBar1.Value == 80) lblStatus.Text = "Arayüz hazırlanıyor...";
            }
            else
            {
                timerProgress.Stop();
                // Yükleme bittiğinde Login ekranına veya Dashboard'a geç
                LoginForm login = new LoginForm();
                login.Show();
                this.Hide(); // Splash ekranını gizle
            }
        }
    }
    }

