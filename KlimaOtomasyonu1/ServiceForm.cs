using FireSharp.Response;
using KlimaOtomasyonu1.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.IO; // Dosya işlemleri için en tepeye ekle
using System.Text; // Türkçe karakterler için (Encoding)

namespace KlimaOtomasyonu1
{
   
    public partial class ServiceForm : Form
    {
        // Geçici olarak müşteri ve cihaz listelerini tutalım
        List<Customer> customerList = new List<Customer>();
        List<AirConditioner> deviceList = new List<AirConditioner>();
        List<User> technicianList = new List<User>();
        // EKSİK OLAN SATIR BU:
        List<StockItem> stockList = new List<StockItem>();
        // Hesaplanan servis nesnesini burada tutacağız
        ServiceTransaction currentService = null;
        List<StockItem> sepet = new List<StockItem>();
        public ServiceForm()
        {
            InitializeComponent();
        }
        private void LoadStocks()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                var response = client.Get("Stocks");
                if (response.Body != "null")
                {
                    var data = response.ResultAs<Dictionary<string, StockItem>>();
                    stockList = data.Values.ToList();

                    // ComboBox'a doldur
                    cmbStocks.DataSource = stockList;
                    cmbStocks.DisplayMember = "ProductName"; // Ürün adı görünsün
                    cmbStocks.ValueMember = "Id";            // Arkada ID tutsun
                    cmbStocks.SelectedIndex = -1;            // Başlangıçta boş olsun
                }
            }
            catch { }
        }
        private void ServiceForm_Load(object sender, EventArgs e)
        {
            LoadCustomers();
            LoadTechnicians();
            LoadStocks();
            RefreshServiceHistory();
        }

        // --- VERİ DOLDURMA İŞLEMLERİ ---
        private void LoadCustomers()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                var response = client.Get("Customers");
                if (response.Body != "null")
                {
                    var data = response.ResultAs<Dictionary<string, Customer>>();
                    customerList = data.Values.ToList();

                    cmbCustomers.DataSource = customerList;
                    cmbCustomers.DisplayMember = "FullName"; // Görünen isim
                    cmbCustomers.ValueMember = "Id";         // Arkadaki değer (ID)
                    cmbCustomers.SelectedIndex = -1;
                }
            }
            catch { }
        }

        private void LoadTechnicians()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                var response = client.Get("Users"); // Teknisyenler de User tablosunda
                if (response.Body != "null")
                {
                    var data = response.ResultAs<Dictionary<string, User>>();
                    // Sadece rolü Technician olanları filtrele
                    technicianList = data.Values.Where(u => u.Role == "Technician").ToList();

                    cmbTechnicians.DataSource = technicianList;
                    cmbTechnicians.DisplayMember = "FullName";
                    cmbTechnicians.ValueMember = "Id";
                }
            }
            catch { }
        }

        // Müşteri değişince cihazları getir
        private void cmbCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbCustomers.SelectedIndex == -1) return;

            try
            {
                Guid selectedCusId = (Guid)cmbCustomers.SelectedValue;
                var client = ConnectionProvider.GetClient();
                var response = client.Get("AirConditioners");

                if (response.Body != "null")
                {
                    var allDevices = response.ResultAs<Dictionary<string, AirConditioner>>().Values.ToList();
                    // Sadece bu müşteriye ait olanları süz
                    deviceList = allDevices.Where(d => d.CustomerId == selectedCusId).ToList();

                    cmbDevices.DataSource = deviceList;
                    cmbDevices.DisplayMember = "Model"; // Listede Model görünsün
                    cmbDevices.ValueMember = "Id";
                }
                else
                {
                    cmbDevices.DataSource = null;
                }
            }
            catch { }
        }

        // Tamir seçilirse "Parça Ücreti" kutusunu aç
        private void rbRepair_CheckedChanged(object sender, EventArgs e)
        {
            txtPartCost.Enabled = rbRepair.Checked;
            if (!rbRepair.Checked) txtPartCost.Text = "0";
        }

        // --- POLİMORFİZM İLE HESAPLAMA ---
        private void btnCalculate_Click(object sender, EventArgs e)
        {
            if (cmbDevices.SelectedIndex == -1)
            {
                MessageBox.Show("Lütfen bir cihaz seçin.");
                return;
            }

            // Seçilen cihazın BTU bilgisini al (Montaj hesaplaması için lazım olabilir)
            AirConditioner selectedDevice = (AirConditioner)cmbDevices.SelectedItem;
            int btu = 0;
            int.TryParse(selectedDevice.BTU, out btu);

            decimal partCost = 0;
            decimal.TryParse(txtPartCost.Text, out partCost);

            decimal laborCost = 0;
            decimal.TryParse(txtLaborCost.Text, out laborCost);

            // 1. ADIM: Hangi servis tipi seçildiyse O SINIFTAN nesne üret (Polimorfizm)
            if (rbMaintenance.Checked)
            {
                // Artık sabit fiyat yok, "İşçilik Ücreti" kutusuna ne yazdıysan o geçerli.
                currentService = new MaintenanceService()
                {
                    LaborCost = laborCost // Formdaki değeri olduğu gibi aktarıyoruz
                };
            }
            else if (rbRepair.Checked)
            {
                currentService = new RepairService()
                {
                    PartCost = partCost,
                    LaborCost = laborCost
                };
            }
            else if (rbInstallation.Checked)
            {
                currentService = new InstallationService()
                {
                    BTUSize = btu
                };
            }
            else
            {
                MessageBox.Show("Lütfen bir işlem türü seçin.");
                return;
            }

            // 2. ADIM: Soyut Metodu Çağır (Her sınıf kendi fiyatını hesaplar)
            currentService.CalculatePricing();

            // Sonucu ekrana yaz
            lblTotalPrice.Text = currentService.TotalPrice.ToString() + " TL";
        }

        // --- KAYDETME ---
        private void btnSaveService_Click(object sender, EventArgs e)
        {
            // 1. ADIM: Güvenlik Kontrolleri
            if (currentService == null)
            {
                MessageBox.Show("Lütfen önce 'Fiyat Hesapla' butonuna basarak işlemleri tamamlayın.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCustomers.SelectedValue == null || cmbTechnicians.SelectedValue == null)
            {
                MessageBox.Show("Müşteri ve Teknisyen seçimi zorunludur!", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // 2. ADIM: Temel Verileri Guid Olarak Hazırla (Daha önce aldığın CS0029 hatası burada çözüldü)
                currentService.CustomerId = Guid.Parse(cmbCustomers.SelectedValue.ToString());
                currentService.TechnicianId = Guid.Parse(cmbTechnicians.SelectedValue.ToString());

                // Açıklamayı ve tarihi set et
                currentService.Description = txtDescription.Text;
                currentService.Date = DateTime.Now;

                // 3. ADIM: Malzemeleri Açıklamaya Ekle (Fişte düzgün görünmesi için)
                if (sepet.Count > 0)
                {
                    string malzemeListesi = "\nKullanılan Malzemeler: " + string.Join(", ", sepet.Select(s => s.ProductName));
                    currentService.Description += malzemeListesi;
                }

                // 4. ADIM: Servis Kaydını Firebase'e Gönder
                var client = ConnectionProvider.GetClient();
                client.Set("ServiceTransactions/" + currentService.Id, currentService);

                // 5. ADIM: Sepetteki Tüm Malzemeler İçin Stoktan Düş (Döngü ile)
                if (sepet.Count > 0)
                {
                    foreach (var urun in sepet)
                    {
                        try
                        {
                            // Stok miktarını 1 düşür
                            urun.Quantity -= 1;

                            // Veritabanındaki ilgili ürünü güncelle
                            client.Set("Stocks/" + urun.Id, urun);
                        }
                        catch (Exception exStok)
                        {
                            // Stokta hata olsa bile servis kaydı yapıldığı için kullanıcıyı bilgilendirip devam ediyoruz
                            MessageBox.Show($"{urun.ProductName} stoktan düşülürken hata: {exStok.Message}");
                        }
                    }
                }

                // 6. ADIM: Başarı ve Formu Kapatma
                MessageBox.Show("Servis kaydı başarıyla oluşturuldu ve stoklar güncellendi.", "İşlem Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sepeti ve listeyi temizle (Bir sonraki kayıt için)
                sepet.Clear();
                if (lstSelectedStocks != null) lstSelectedStocks.Items.Clear();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kayıt sırasında kritik bir hata oluştu: " + ex.Message, "Sistem Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private void RefreshServiceHistory()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                var response = client.Get("ServiceTransactions");

                if (response.Body != "null")
                {
                    // Veriyi "Görüntüleme Modeli" formatında çekiyoruz
                    var data = response.ResultAs<Dictionary<string, ServiceDisplayModel>>();

                    // Tarihe göre yeniden eskiye sıralayıp listeye atalım
                    var list = data.Values.OrderByDescending(x => x.Date).ToList();

                    dgvHistory.DataSource = list;

                    // ID gibi teknik sütunları gizleyelim, kafa karıştırmasın
                    if (dgvHistory.Columns["Id"] != null) dgvHistory.Columns["Id"].Visible = false;
                    if (dgvHistory.Columns["CustomerId"] != null) dgvHistory.Columns["CustomerId"].Visible = false;
                }
                dgvHistory.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // 2. Özel genişlik ayarları (Bazılarını daralt, bazılarını genişlet)
                if (dgvHistory.Columns["Date"] != null) dgvHistory.Columns["Date"].Width = 80;
                if (dgvHistory.Columns["TotalPrice"] != null) dgvHistory.Columns["TotalPrice"].Width = 70;
                if (dgvHistory.Columns["Description"] != null) dgvHistory.Columns["Description"].FillWeight = 200; // Açıklama en genişi olsun

             

                // 4. Gereksiz sütunları gizle (Kullanıcının görmesine gerek yok)
                if (dgvHistory.Columns["PartCost"] != null) dgvHistory.Columns["PartCost"].Visible = false;
                if (dgvHistory.Columns["LaborCost"] != null) dgvHistory.Columns["LaborCost"].Visible = false;
                if (dgvHistory.Columns["BTUSize"] != null) dgvHistory.Columns["BTUSize"].Visible = false;
                // 4. ÖZEL GENİŞLİK AYARI (Açıklama kısmı daha geniş olsun, tarih dar kalsın)
                dgvHistory.Columns["Date"].Width = 100;
                dgvHistory.Columns["Description"].FillWeight = 200; // Diğer sütunların 2 katı yer kaplar
                dgvHistory.Columns["TotalPrice"].Width = 120;
            }
            catch (Exception ex)
            {
                // İlk başta veri yoksa hata verebilir, sessizce geçebiliriz veya loglayabiliriz
                // MessageBox.Show("Liste yüklenirken hata: " + ex.Message);
            }
        }

        private void btnDeleteService_Click(object sender, EventArgs e)
        {
            // 1. Güvenlik Kontrolü: Bir satır seçili mi?
            if (dgvHistory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek kaydı listeden seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Kullanıcıdan son bir onay alalım (Yanlışlıkla basarsa diye)
            DialogResult onay = MessageBox.Show("Bu servis kaydını kalıcı olarak silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (onay == DialogResult.Yes)
            {
                try
                {
                    // Seçili satırdaki gizli ID'yi al
                    string id = dgvHistory.SelectedRows[0].Cells["Id"].Value.ToString();

                    // Firebase'den sil
                    var client = ConnectionProvider.GetClient();
                    client.Delete("ServiceTransactions/" + id);

                    // Kullanıcıya bilgi ver ve listeyi yenile
                    MessageBox.Show("Kayıt silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshServiceHistory();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme işlemi sırasında hata: " + ex.Message);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close(); // Bu formu kapatır ve seni otomatik olarak Ana Menüye geri atar.
        }

        private void dgvHistory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Seçili satır var mı?
            if (e.RowIndex >= 0)
            {
                // DataGridView'deki satır aslında bizim ServiceDisplayModel'dir.
                // Ancak biz burada Interface mantığını göstermek için veritabanındaki 
                // gerçek nesnenin özetini simüle eden bir mesaj gösterelim.

                string musteriAdi = dgvHistory.Rows[e.RowIndex].Cells["CustomerName"].Value.ToString();
                string cihazAdi = dgvHistory.Rows[e.RowIndex].Cells["DeviceName"].Value.ToString();
                string tutar = dgvHistory.Rows[e.RowIndex].Cells["TotalPrice"].Value.ToString();
                string aciklama = dgvHistory.Rows[e.RowIndex].Cells["Description"].Value.ToString();

                // Normalde Interface'i şöyle çağırırız (Polimorfizm):
                // ISummary ozet = (ISummary)secilenNesne;
                // MessageBox.Show(ozet.GetSummary());

                // Bizim listemiz DTO olduğu için el ile bir format yapıp Interface mantığını sunumda anlatabilirsin.
                MessageBox.Show($"DETAY (Interface): \n\nİşlem: {aciklama}\nMüşteri: {musteriAdi}\nCihaz: {cihazAdi}\nTutar: {tutar} TL", "Özet Bilgi");
            }
        }

        private void dgvHistory_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        
        {
            // --- 1. AYARLAR (Okunabilirlik İçin Fontlar Büyütüldü) ---
            Color anaRenk = Color.FromArgb(0, 45, 90);    // Lacivert
            Color arkaPlanGri = Color.FromArgb(240, 240, 240); // Açık Gri
            Color vurguRenk = Color.DarkRed;              // Kırmızı

            // FONT BOYUTLARI ARTIRILDI (+2/3 Puan)
            Font baslikFont = new Font("Segoe UI", 30, FontStyle.Bold);      // Ana Başlık
            Font altBaslikFont = new Font("Segoe UI", 14, FontStyle.Bold);   // Kutu Başlıkları
            Font etiketFont = new Font("Segoe UI", 12, FontStyle.Bold);      // "Müşteri Adı:" (Artık büyük)
            Font degerFont = new Font("Segoe UI", 12, FontStyle.Regular);    // "Ahmet Yılmaz" (Artık büyük)
            Font icerikFont = new Font("Segoe UI", 12, FontStyle.Regular);   // Açıklama Metni
            Font tutarFont = new Font("Segoe UI", 36, FontStyle.Bold);       // TUTAR (Devasa)
            Font footerFont = new Font("Segoe UI", 10, FontStyle.Italic);    // Alt bilgi

            // Kalemler
            Pen cerceveKalemi = new Pen(Color.Gray, 2); // Çerçeveler kalınlaştı
            Brush anaFirca = new SolidBrush(anaRenk);
            Brush siyahFirca = Brushes.Black;

            // Sayfa Düzeni
            int solBosluk = 40;
            int sagBosluk = 40;
            int y = 50;
            int genislik = e.PageBounds.Width - (solBosluk + sagBosluk);
            int ortaNokta = solBosluk + (genislik / 2);

            // --- 2. VERİLERİ GÜVENLİ AL ---
            if (dgvHistory.SelectedRows.Count > 0)
            {
                var row = dgvHistory.SelectedRows[0];

                // Veri Çekme (Hata Önleyicili)
                string tarih = Convert.ToDateTime(row.Cells["Date"].Value).ToShortDateString();
                string musteri = row.Cells["CustomerName"].Value?.ToString() ?? "-";
                string cihaz = row.Cells["DeviceName"].Value?.ToString() ?? "-";
                string aciklama = row.Cells["Description"].Value?.ToString() ?? "-";
                string tutar = row.Cells["TotalPrice"].Value?.ToString() ?? "0";

                // TEKNİSYEN ADI KONTROLÜ (HATA ÇÖZÜMÜ)
                string teknisyen = "Belirtilmemiş";
                // Eğer sütun varsa ve içi boş değilse al, yoksa varsayılan kalsın.
                if (dgvHistory.Columns.Contains("TechnicianName") && row.Cells["TechnicianName"].Value != null)
                {
                    teknisyen = row.Cells["TechnicianName"].Value.ToString();
                }

                // İşlem Türü Belirleme
                decimal parcaUcreti = Convert.ToDecimal(row.Cells["PartCost"].Value ?? 0);
                int btu = Convert.ToInt32(row.Cells["BTUSize"].Value ?? 0);

                string islemBasligi = "PERİYODİK BAKIM İŞLEMİ";
                if (btu > 0) islemBasligi = "KLİMA MONTAJ VE KURULUM";
                else if (parcaUcreti > 0) islemBasligi = "ARIZA ONARIM VE PARÇA DEĞİŞİMİ";


                // --- 3. ÇİZİM ---

                // A. HEADER
                // Logo Kutusu
                Rectangle logoRect = new Rectangle(solBosluk, y, 70, 70);
                e.Graphics.FillRectangle(anaFirca, logoRect);
                e.Graphics.DrawString("BS", new Font("Arial", 30, FontStyle.Bold), Brushes.White, solBosluk + 5, y + 15);

                // Başlıklar
                e.Graphics.DrawString("BS KLİMA SİSTEMLERİ", baslikFont, anaFirca, solBosluk + 80, y);
                e.Graphics.DrawString("TEKNİK SERVİS HİZMET FORMU", new Font("Segoe UI", 14, FontStyle.Regular), Brushes.Gray, solBosluk + 85, y + 50);

                // Tarih (Header içine)
                string tarihMetni = $"Tarih: {tarih}";
                SizeF tarihBoyut = e.Graphics.MeasureString(tarihMetni, etiketFont);
                e.Graphics.DrawString(tarihMetni, etiketFont, Brushes.Black, (solBosluk + genislik) - tarihBoyut.Width, y + 20);

                y += 90;
                e.Graphics.DrawLine(new Pen(anaRenk, 4), solBosluk, y, solBosluk + genislik, y); // Çok kalın çizgi
                y += 25;


                // B. BİLGİ KUTUSU (YÜKSEKLİK ARTTI)
                int kutuYuksekligi = 140; // Yazılar büyüdüğü için kutuyu büyüttük
                Rectangle bilgiKutusu = new Rectangle(solBosluk, y, genislik, kutuYuksekligi);
                e.Graphics.FillRectangle(new SolidBrush(arkaPlanGri), bilgiKutusu);
                e.Graphics.DrawRectangle(cerceveKalemi, bilgiKutusu);

                // Sol Sütun
                int solX = solBosluk + 20;
                int solY = y + 20;
                e.Graphics.DrawString("MÜŞTERİ BİLGİLERİ", altBaslikFont, anaFirca, solX, solY);
                solY += 35;
                e.Graphics.DrawString("Adı Soyadı:", etiketFont, Brushes.Gray, solX, solY);
                e.Graphics.DrawString(musteri.ToUpper(), degerFont, siyahFirca, solX + 110, solY);

                // Sağ Sütun
                int sagX = ortaNokta + 20;
                int sagY = y + 20;
                e.Graphics.DrawString("SERVİS DETAYLARI", altBaslikFont, anaFirca, sagX, sagY);
                sagY += 35;
                e.Graphics.DrawString("Cihaz:", etiketFont, Brushes.Gray, sagX, sagY);
                e.Graphics.DrawString(cihaz, degerFont, siyahFirca, sagX + 100, sagY);
                sagY += 30;

                // Teknisyen İsmi (Vurgulu)
                e.Graphics.DrawString("Teknisyen:", etiketFont, Brushes.Gray, sagX, sagY);
                e.Graphics.DrawString(teknisyen.ToUpper(), new Font("Segoe UI", 12, FontStyle.Bold), anaFirca, sagX + 100, sagY);

                e.Graphics.DrawLine(new Pen(Color.LightGray, 2), ortaNokta, y + 10, ortaNokta, y + kutuYuksekligi - 10);
                y += kutuYuksekligi + 40;


                // C. İŞLEM DETAYI
                // Başlık Şeridi (Yükseklik arttı)
                Rectangle islemBaslikRect = new Rectangle(solBosluk, y, genislik, 45);
                e.Graphics.FillRectangle(anaFirca, islemBaslikRect);
                e.Graphics.DrawString("YAPILAN İŞLEM VE MALZEME DETAYI", new Font("Segoe UI", 13, FontStyle.Bold), Brushes.White, solBosluk + 15, y + 10);

                y += 60;
                e.Graphics.DrawString($"İşlem Türü: {islemBasligi}", new Font("Segoe UI", 14, FontStyle.Bold), Brushes.Black, solBosluk, y);
                y += 35;

                // Açıklama Kutusu
                Rectangle aciklamaKutusu = new Rectangle(solBosluk, y, genislik, 150);
                e.Graphics.DrawRectangle(cerceveKalemi, aciklamaKutusu);

                StringFormat format = new StringFormat { Trimming = StringTrimming.Word };
                RectangleF yaziAlani = new RectangleF(solBosluk + 10, y + 10, genislik - 20, 130);
                e.Graphics.DrawString(aciklama, icerikFont, siyahFirca, yaziAlani, format);

                y += 180;


                // D. TUTAR
                e.Graphics.DrawLine(new Pen(Color.Silver, 2), solBosluk, y, solBosluk + genislik, y);
                y += 30;

                string tutarYazisi = $"TOPLAM TUTAR: {tutar} TL";
                SizeF tutarBoyut = e.Graphics.MeasureString(tutarYazisi, tutarFont);
                e.Graphics.DrawString(tutarYazisi, tutarFont, new SolidBrush(vurguRenk), (solBosluk + genislik) - tutarBoyut.Width, y);

                // E. İMZA VE FOOTER
                y = e.PageBounds.Height - 120; // Alt kısma sabitle

                int imzaY = y;
                e.Graphics.DrawString("Teslim Alan (Müşteri)", etiketFont, Brushes.Gray, solBosluk + 50, imzaY);
                e.Graphics.DrawString("Teslim Eden (Teknisyen)", etiketFont, Brushes.Gray, solBosluk + genislik - 250, imzaY);

                e.Graphics.DrawLine(cerceveKalemi, solBosluk + 30, imzaY + 40, solBosluk + 250, imzaY + 40);
                e.Graphics.DrawLine(cerceveKalemi, solBosluk + genislik - 270, imzaY + 40, solBosluk + genislik - 50, imzaY + 40);

                y += 60;
                e.Graphics.DrawString("BS Klima Sistemleri - Ankara | Müşteri Hizmetleri: 0312 123 45 67 | Web: www.bsklimaankara.com.tr", footerFont, anaFirca, solBosluk, y);
            }
        }
        
        
        private void btnPrint_Click(object sender, EventArgs e)
        {
            // Seçili satır var mı kontrol et
            if (dgvHistory.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen yazdırmak için listeden bir kayıt seçin.");
                return;
            }

            // Önizleme ekranını aç
            printPreviewDialog1.ShowDialog();
        }

        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            // Listede veri var mı?
            if (dgvHistory.Rows.Count == 0)
            {
                MessageBox.Show("Listede aktarılacak veri yok.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dosya Kaydetme Penceresini Aç
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Excel Dosyası (CSV)|*.csv";
            sfd.FileName = "Servis_Raporu_" + DateTime.Now.ToShortDateString().Replace(".", "_");
            sfd.Title = "Raporu Kaydet";

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // CSV oluşturmak için String Builder kullanıyoruz (Daha hızlı)
                    StringBuilder sb = new StringBuilder();

                    // 1. Başlıkları Ekle (Sütun İsimleri)
                    // Görünen sütunları alalım
                    List<string> basliklar = new List<string>();
                    foreach (DataGridViewColumn col in dgvHistory.Columns)
                    {
                        if (col.Visible) // Sadece gizli olmayanları al
                        {
                            basliklar.Add(col.HeaderText);
                        }
                    }
                    sb.AppendLine(string.Join(";", basliklar)); // Türkiye Excel'i için ayraç ; dir.

                    // 2. Satırları Ekle
                    foreach (DataGridViewRow row in dgvHistory.Rows)
                    {
                        List<string> hucreler = new List<string>();
                        foreach (DataGridViewColumn col in dgvHistory.Columns)
                        {
                            if (col.Visible)
                            {
                                // Hücre boşsa hata vermesin
                                string deger = row.Cells[col.Name].Value?.ToString() ?? "";

                                // Eğer metnin içinde noktalı virgül varsa karışmasın diye temizleyelim
                                deger = deger.Replace(";", " - ");

                                // Satır sonlarını temizle (açıklama alanı bozulmasın)
                                deger = deger.Replace("\n", " ").Replace("\r", "");

                                hucreler.Add(deger);
                            }
                        }
                        sb.AppendLine(string.Join(";", hucreler));
                    }

                    // 3. Dosyayı Yaz (Türkçe Karakter Sorunu Olmaması İçin UTF8-BOM Kullanıyoruz)
                    File.WriteAllText(sfd.FileName, sb.ToString(), Encoding.UTF8);

                    MessageBox.Show("Rapor başarıyla kaydedildi!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // İstersen dosyayı otomatik açtırabilirsin (Opsiyonel)
                    // System.Diagnostics.Process.Start(sfd.FileName); 
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cmbStocks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStocks.SelectedIndex != -1)
            {
                // Seçilen parçayı bul
                StockItem secilenParca = (StockItem)cmbStocks.SelectedItem;

                // Fiyatı kutuya yaz (Satış fiyatını)
                txtPartCost.Text = secilenParca.SalePrice.ToString();
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            if (cmbStocks.SelectedIndex != -1)
            {
                StockItem secilen = (StockItem)cmbStocks.SelectedItem;

                // Sepete ekle
                sepet.Add(secilen);

                // ListBox'ta göster
                lstSelectedStocks.Items.Add($"{secilen.ProductName} - {secilen.SalePrice} TL");

                // Toplam fiyatı güncelle (Opsiyonel: mevcut parça maliyetine ekle)
                decimal mevcutMaliyet = string.IsNullOrEmpty(txtPartCost.Text) ? 0 : decimal.Parse(txtPartCost.Text);
                txtPartCost.Text = (mevcutMaliyet + secilen.SalePrice).ToString();
            }
        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (lstSelectedStocks.SelectedIndex != -1)
            {
                int index = lstSelectedStocks.SelectedIndex;

                // Fiyatı düş
                decimal mevcutMaliyet = decimal.Parse(txtPartCost.Text);
                txtPartCost.Text = (mevcutMaliyet - sepet[index].SalePrice).ToString();

                // Listelerden temizle
                sepet.RemoveAt(index);
                lstSelectedStocks.Items.RemoveAt(index);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
    }
    
    
    
    

