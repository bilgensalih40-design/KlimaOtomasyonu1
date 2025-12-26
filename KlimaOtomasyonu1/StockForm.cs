using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Drawing; // Renklendirme için lazım olacak
using System.Linq;
using System.Windows.Forms;
using FireSharp.Response;
using KlimaOtomasyonu1.Models;

namespace KlimaOtomasyonu1
{
    public partial class StockForm : Form
    {
        public StockForm()
        {
            InitializeComponent();
        }

        private void StockForm_Load(object sender, EventArgs e)
        {
            RefreshStockList();
        }

        // --- 1. LİSTELEME VE RENKLENDİRME ---
        private void RefreshStockList()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                FirebaseResponse response = client.Get("Stocks");

                if (response.Body != "null")
                {
                    var data = response.ResultAs<Dictionary<string, StockItem>>();
                    var list = data.Values.ToList();
                    dgvStocks.DataSource = list;

                    // ID sütununu gizle
                    if (dgvStocks.Columns["Id"] != null) dgvStocks.Columns["Id"].Visible = false;

                    // --- KRİTİK STOK UYARISI (Renklendirme) ---
                    foreach (DataGridViewRow row in dgvStocks.Rows)
                    {
                        int adet = Convert.ToInt32(row.Cells["Quantity"].Value);
                        int kritik = Convert.ToInt32(row.Cells["CriticalLevel"].Value);

                        if (adet <= kritik)
                        {
                            // Stok azaldıysa satırı Kırmızı yap!
                            row.DefaultCellStyle.BackColor = Color.MistyRose;
                            row.DefaultCellStyle.ForeColor = Color.Red;
                        }
                    }
                }
            }
            catch { }
        }

        // --- 3. SİLME ---
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvStocks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek ürünü seçin.");
                return;
            }

            if (MessageBox.Show("Bu stok kaydını silmek istediğinize emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // Seçili satırdaki ID'yi al
                    string id = dgvStocks.SelectedRows[0].Cells["Id"].Value.ToString();

                    var client = ConnectionProvider.GetClient();
                    client.Delete("Stocks/" + id);

                    MessageBox.Show("Ürün silindi.");
                    RefreshStockList();
                    Temizle();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Silme hatası: " + ex.Message);
                }
            }
        }
        

        // --- 4. SEÇİLENİ KUTULARA DOLDURMA ---
        private void dgvStocks_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvStocks.Rows[e.RowIndex];
                cmbProductName.Text = row.Cells["ProductName"].Value.ToString();
                numQuantity.Value = Convert.ToInt32(row.Cells["Quantity"].Value);
                txtPurchasePrice.Text = row.Cells["PurchasePrice"].Value.ToString();
                txtSalePrice.Text = row.Cells["SalePrice"].Value.ToString();
                cmbUnit.Text = row.Cells["Unit"].Value?.ToString();
                numCritical.Value = Convert.ToInt32(row.Cells["CriticalLevel"].Value);
            }
        }

        private void Temizle()
        {

            cmbProductName.Text = "";
            numQuantity.Value = 0;
            txtPurchasePrice.Text = "0";
            txtSalePrice.Text = "0";
            cmbUnit.SelectedIndex = -1;
            numCritical.Value = 0;
        }
        private void btnDelete_Click_1(object sender, EventArgs e)
        {

        }


        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cmbProductName.Text))
            {
                MessageBox.Show("Ürün adı boş olamaz!");
                return;
            }

            try
            {
                StockItem newItem = new StockItem()
                {
                    Id = Guid.NewGuid(),
                    ProductName = cmbProductName.Text, // ComboBox'tan alıyoruz
                    Quantity = (int)numQuantity.Value,
                    Unit = cmbUnit.Text,
                    PurchasePrice = decimal.Parse(txtPurchasePrice.Text),
                    SalePrice = decimal.Parse(txtSalePrice.Text),
                    CriticalLevel = (int)numCritical.Value
                };

                var client = ConnectionProvider.GetClient();
                client.Set("Stocks/" + newItem.Id, newItem);

                MessageBox.Show("Ürün stoğa eklendi!");
                RefreshStockList();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 1. Seçili satır var mı?
            if (dgvStocks.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen güncellenecek ürünü listeden seçin.");
                return;
            }

            try
            {
                // 2. Seçili satırdaki ID'yi al (Değişmeyecek olan şey bu)
                string id = dgvStocks.SelectedRows[0].Cells["Id"].Value.ToString();

                // 3. Güncel nesneyi oluştur (ID aynı kalmalı!)
                StockItem updatedItem = new StockItem()
                {
                    Id = Guid.Parse(id), // ID'yi listeden alıp tekrar kullanıyoruz
                    ProductName = cmbProductName.Text,
                    Quantity = (int)numQuantity.Value,
                    Unit = cmbUnit.Text,
                    PurchasePrice = decimal.Parse(txtPurchasePrice.Text),
                    SalePrice = decimal.Parse(txtSalePrice.Text),
                    CriticalLevel = (int)numCritical.Value
                };

                // 4. Firebase'de üzerine yaz (Update veya Set aynı ID ile çalışır)
                var client = ConnectionProvider.GetClient();
                client.Set("Stocks/" + id, updatedItem);

                MessageBox.Show("Ürün bilgileri güncellendi!");
                RefreshStockList();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
        }
    }
    }
    
