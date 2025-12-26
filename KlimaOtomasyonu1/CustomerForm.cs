using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FireSharp.Response;
using KlimaOtomasyonu1.Models;

namespace KlimaOtomasyonu1
{
    public partial class CustomerForm : Form
    {
        // Seçili müşterinin ID'sini hafızada tutmak için değişken
        private string selectedCustomerId = null;

        public CustomerForm()
        {
            InitializeComponent();
        }

        private void CustomerForm_Load(object sender, EventArgs e)
        {
            RefreshCustomerList();
        }

        // --- SOL TARAF: MÜŞTERİ İŞLEMLERİ ---

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCusName.Text) || string.IsNullOrEmpty(txtCusPhone.Text))
            {
                MessageBox.Show("Ad Soyad ve Telefon zorunludur.");
                return;
            }

            try
            {
                var client = ConnectionProvider.GetClient();
                Customer newCus = new Customer()
                {
                    FullName = txtCusName.Text,
                    PhoneNumber = txtCusPhone.Text,
                    Address = txtCusAddress.Text
                };

                // Müşteriyi Firebase'e kaydet
                SetResponse response = client.Set("Customers/" + newCus.Id, newCus);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Müşteri eklendi.");
                    RefreshCustomerList();
                    TemizleMusteri();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void RefreshCustomerList()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                FirebaseResponse response = client.Get("Customers");

                if (response.Body != "null")
                {
                    var data = response.ResultAs<Dictionary<string, Customer>>();
                    dgvCustomers.DataSource = data.Values.ToList();

                    // Gereksiz kolonları gizle
                    if (dgvCustomers.Columns["Id"] != null) dgvCustomers.Columns["Id"].Visible = false;
                }
            }
            catch { }
        }

        private void TemizleMusteri()
        {
            txtCusName.Clear();
            txtCusPhone.Clear();
            txtCusAddress.Clear();
        }

        // --- ORTA NOKTA: SEÇİM İŞLEMİ ---

        private void dgvCustomers_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Seçilen satırdaki veriyi al
                string id = dgvCustomers.Rows[e.RowIndex].Cells["Id"].Value.ToString();
                string name = dgvCustomers.Rows[e.RowIndex].Cells["FullName"].Value.ToString();

                // Değişkeni güncelle
                selectedCustomerId = id;

                // Etiketi güncelle (Kullanıcı kime işlem yaptığını görsün)
                lblSelectedCustomer.Text = "Seçili Müşteri: " + name;
                lblSelectedCustomer.ForeColor = System.Drawing.Color.Green;

                // O müşteriye ait cihazları getir
                RefreshDeviceList(id);
            }
        }

        // --- SAĞ TARAF: CİHAZ (KLİMA) İŞLEMLERİ ---

        private void btnAddDevice_Click(object sender, EventArgs e)
        {
            if (selectedCustomerId == null)
            {
                MessageBox.Show("Lütfen önce soldaki listeden bir müşteri seçin!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var client = ConnectionProvider.GetClient();
                AirConditioner newAC = new AirConditioner()
                {
                    Brand = txtBrand.Text,
                    Model = txtModel.Text,
                    BTU = cmbBTU.Text,
                    CustomerId = Guid.Parse(selectedCustomerId) // İLİŞKİ BURADA KURULUYOR
                };

                // Cihazı "AirConditioners" tablosuna kaydet
                client.Set("AirConditioners/" + newAC.Id, newAC);

                MessageBox.Show("Cihaz müşteriye tanımlandı.");
                RefreshDeviceList(selectedCustomerId); // Listeyi yenile
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cihaz ekleme hatası: " + ex.Message);
            }
        }

        private void RefreshDeviceList(string customerId)
        {
            // Sadece seçili müşteriye ait cihazları getirmeliyiz
            try
            {
                var client = ConnectionProvider.GetClient();
                FirebaseResponse response = client.Get("AirConditioners");

                if (response.Body != "null")
                {
                    var allDevices = response.ResultAs<Dictionary<string, AirConditioner>>().Values.ToList();

                    // LINQ ile Filtreleme: Sadece CustomerId'si eşleşenleri getir
                    var customerDevices = allDevices.Where(d => d.CustomerId.ToString() == customerId).ToList();

                    dgvDevices.DataSource = customerDevices;

                    if (dgvDevices.Columns["Id"] != null) dgvDevices.Columns["Id"].Visible = false;
                    if (dgvDevices.Columns["CustomerId"] != null) dgvDevices.Columns["CustomerId"].Visible = false;
                }
                else
                {
                    dgvDevices.DataSource = null;
                }
            }
            catch
            {
                dgvDevices.DataSource = null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            this.Close(); // Bu formu kapatır ve seni otomatik olarak Ana Menüye geri atar.
        }

        private void grpDevice_Enter(object sender, EventArgs e)
        {

        }
    }
}