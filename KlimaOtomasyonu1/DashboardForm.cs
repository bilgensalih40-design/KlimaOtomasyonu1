using FireSharp.Response;
using KlimaOtomasyonu1.Models; // Modelleri tanıması için
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
namespace KlimaOtomasyonu1
{
    public partial class DashboardForm : Form
    {
        
        private void AdjustLayout()
        {
            // 1. Sol taraftaki butonları bul (Tag veya Name ile kontrol edebiliriz ama basitçe listeye alalım)
            List<Button> solButonlar = new List<Button>() { btnPersonnelOps, btnServiceOps, btnCustomerOps, button1, btnExit }; // btnExit senin Çıkış butonun

            // 2. Sadece görünür olanları (Visible = true) filtrele
            var gorunurButonlar = solButonlar.Where(b => b.Visible).ToList();

            // 3. Formun kullanılabilir yüksekliğini al (Üst çubuk vs hariç)
            // Eğer butonlar bir Panel içindeyse panel.Height kullan, direkt Form üzerindeyse ClientSize.Height
            int toplamYukseklik = this.ClientSize.Height;

            // 4. Her butona eşit yükseklik ver
            int yeniYukseklik = toplamYukseklik / gorunurButonlar.Count;

            foreach (var btn in gorunurButonlar)
            {
                btn.Height = yeniYukseklik;
                // Dock = Top kullanıyorsan zaten otomatik sıralanır, sadece yükseklik yeterli.
                // Eğer Dock kullanmıyorsan Top (Y) pozisyonunu da ayarlamak gerekir.
            }
        }
        private void LoadChartData()
        {
            try
            {
                // 1. Önce grafiği temizle (Eski veriler kalmasın)
                chartServices.Series[0].Points.Clear();
                chartServices.Series[0].ChartType = SeriesChartType.Pie; // Kodla da garantiye alalım

                // 2. Firebase'den verileri çek
                var client = ConnectionProvider.GetClient();
                FirebaseResponse response = client.Get("ServiceTransactions");

                if (response.Body != "null")
                {
                    // Verileri sözlük olarak al
                    var data = response.ResultAs<Dictionary<string, ServiceDisplayModel>>(); // DisplayModel kullanmak güvenli
                    var servisListesi = data.Values.ToList();

                    // 3. Verileri Analiz Et (Sayma İşlemi)
                    // Not: Açıklamada veya özette geçen kelimelere göre sayacağız.
                    // Veya daha kolayı: Eğer PartCost (Parça Ücreti) > 0 ise Tamirdir, yoksa Bakımdır diyelim.
                    // Veya Description içinde "Bakım" geçiyor mu diye bakabiliriz.

                    int bakimSayisi = 0;
                    int tamirSayisi = 0;
                    int montajSayisi = 0;

                    foreach (var item in servisListesi)
                    {
                        // Basit bir mantık kuralım:
                        if (item.Description.ToLower().Contains("bakım"))
                        {
                            bakimSayisi++;
                        }
                        else if (item.Description.ToLower().Contains("tamir") || item.PartCost > 0)
                        {
                            tamirSayisi++;
                        }
                        else if (item.Description.ToLower().Contains("montaj"))
                        {
                            montajSayisi++;
                        }
                        else
                        {
                            // Hiçbiri değilse "Diğer" grubuna veya Bakıma ekleyebilirsin
                            bakimSayisi++;
                        }
                    }

                    // 4. Grafiğe Ekle
                    // Eğer sayı 0 ise eklemeyelim ki grafik bozulmasın
                    if (bakimSayisi > 0) chartServices.Series[0].Points.AddXY("Bakım", bakimSayisi);
                    if (tamirSayisi > 0) chartServices.Series[0].Points.AddXY("Tamir", tamirSayisi);
                    if (montajSayisi > 0) chartServices.Series[0].Points.AddXY("Montaj", montajSayisi);

                    // 5. Görsellik (İsteğe bağlı: Değerleri dilimlerin üzerinde göster)
                    chartServices.Series[0].IsValueShownAsLabel = true;
                    // Grafiğin dış arka planını şeffaf yap
                    chartServices.BackColor = Color.Transparent;

                    // Grafiğin iç çizim alanını da şeffaf yap
                    chartServices.ChartAreas[0].BackColor = Color.Transparent;

                    // Yazı renklerini Beyaz yap (Lacivert üstünde görünsün diye)
                    chartServices.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
                    chartServices.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
                    chartServices.Legends[0].ForeColor = Color.White;
                    chartServices.Legends[0].BackColor = Color.Transparent; // Lejant arkası da şeffaf olsun
                                                                            // --- GRAFİK GÖRSELLİK AYARLARI ---

                    // 1. Dış Çerçeve Rengi (En önemlisi bu, şu an açık mavi duran yer)
                    chartServices.BackColor = Color.Transparent;

                    // 2. İç Grafik Alanı Rengi
                    chartServices.ChartAreas[0].BackColor = Color.Transparent;

                    // 3. Yazı Renklerini Beyaz Yap (Koyu zemin üstünde görünsün)
                    chartServices.ChartAreas[0].AxisX.LabelStyle.ForeColor = Color.White;
                    chartServices.ChartAreas[0].AxisY.LabelStyle.ForeColor = Color.White;
                    chartServices.Legends[0].ForeColor = Color.White;

                    // 4. Lejant (Sağdaki yazıların) Arka Planı
                    chartServices.Legends[0].BackColor = Color.Transparent;

                    // 5. Kenarlık Çizgilerini Kaldır (Daha modern durur)
                    chartServices.BorderlineColor = Color.Transparent;
                }
            }
            catch
            {
                // Hata olursa grafik boş kalsın, program çökmesin.
            }
        }
        // Giriş yapan kullanıcıyı hafızada tutmak için değişken
        private User currentUser;

        // Constructor (Yapıcı Metod) - Giriş yapan kullanıcıyı parametre olarak alır
        public DashboardForm(User user)
        {
            InitializeComponent();
            this.currentUser = user;
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {// --- GÜVENLİK KONTROLÜ (Bunu en başa ekle) ---
            if (currentUser == null)
            {
                // Eğer kullanıcı bilgisi yoksa (Test için açtıysan), sahte bir Admin oluştur.
                // Böylece program çökmez.
                currentUser = new User();
                currentUser.Role = "Admin";
            }
            LoadChartData();

            // String verisini temizleyip (Trim) kontrol edelim.
            // Veritabanında "Technician " gibi boşluklu kaydedilmiş olabilir.
            string userRole = currentUser.Role.Trim(); 
            
        }


        // BUTON YÖNLENDİRMELERİ (Henüz formları yapmadığımız için boş bırakıyoruz veya mesaj verdiriyoruz)

        private void btnCustomerOps_Click(object sender, EventArgs e)
        {
            CustomerForm customerForm = new CustomerForm();
            customerForm.ShowDialog();
            //MessageBox.Show("Müşteri ekranı hazırlanıyor...", "Bilgi");
        }

        private void btnServiceOps_Click(object sender, EventArgs e)
        {
             ServiceForm serviceForm = new ServiceForm();
            serviceForm.ShowDialog();
            //MessageBox.Show("Servis ekranı hazırlanıyor...", "Bilgi");
        }

        private void btnPersonnelOps_Click(object sender, EventArgs e)
        {
            // --- GÜVENLİK KONTROLÜ ---
            // Eğer kullanıcı Admin DEĞİLSE, içeri alma ve uyarı ver.
            if (currentUser.Role != "Admin")
            {
                MessageBox.Show("Bu alana giriş yetkiniz bulunmamaktadır.\nLütfen yönetici ile iletişime geçiniz.",
                                "Erişim Engellendi",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return; // Buradan geri dön, alt satırdaki formu açma kodunu çalıştırma!
            }
            // -------------------------

            // Admin ise normal şekilde formu aç
            PersonnelForm personelFormu = new PersonnelForm();
            personelFormu.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //private void button1_Click(object sender, EventArgs e)
        //{
            private void btnWebSite_Click(object sender, EventArgs e)
        {
            WebSiteForm webForm = new WebSiteForm();
            webForm.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebSiteForm webForm = new WebSiteForm();
            webForm.ShowDialog();
        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            StockForm stokFormu = new StockForm();
            stokFormu.ShowDialog();
        }
    }
    }
