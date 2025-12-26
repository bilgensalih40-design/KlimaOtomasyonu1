using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FireSharp.Response;
using KlimaOtomasyonu1.Models; // Modelleri unutma

namespace KlimaOtomasyonu1
{
    public partial class PersonnelForm : Form
    {
        public PersonnelForm()
        {
            InitializeComponent();
        }

        private void PersonnelForm_Load(object sender, EventArgs e)
        {
            // Form açılınca listeyi getir
            RefreshList();
            cmbRole.SelectedIndex = 1; // Varsayılan olarak "Technician" seçili gelsin
        }

        // LİSTELEME METODU (Firebase'den çeker)
        private void RefreshList()
        {
            try
            {
                var client = ConnectionProvider.GetClient();
                FirebaseResponse response = client.Get("Users");

                if (response.Body != "null")
                {
                    // Firebase verisini Dictionary olarak al
                    var usersDict = response.ResultAs<Dictionary<string, User>>();

                    // Datagridview'e bağlamak için Listeye çevir
                    dgvPersonnel.DataSource = usersDict.Values.ToList();

                    // Şifre sütununu gizleyelim (Güvenlik)
                    if (dgvPersonnel.Columns["Password"] != null)
                        dgvPersonnel.Columns["Password"].Visible = false;

                    if (dgvPersonnel.Columns["Id"] != null)
                        dgvPersonnel.Columns["Id"].Visible = false;
                }
                else
                {
                    dgvPersonnel.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Listeleme hatası: " + ex.Message);
            }
        }

        // EKLEME METODU
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Lütfen bilgileri eksiksiz girin.");
                return;
            }

            try
            {
                var client = ConnectionProvider.GetClient();
                User newUser;

                // Seçilen role göre uygun sınıfı üretiyoruz (Polimorfizm hazırlığı)
                if (cmbRole.SelectedItem.ToString() == "Admin")
                {
                    newUser = new Admin();
                }
                else
                {
                    newUser = new Technician();
                }

                // Ortak bilgileri doldur
                newUser.Username = txtUsername.Text;
                newUser.Password = txtPassword.Text;
                newUser.FullName = txtFullName.Text;
                // Role bilgisi constructor'da otomatik atandı ama garanti olsun:
                newUser.Role = cmbRole.SelectedItem.ToString();

                // Firebase'e kaydet (Id'si ile)
                SetResponse response = client.Set("Users/" + newUser.Id, newUser);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    MessageBox.Show("Personel başarıyla eklendi!");
                    RefreshList(); // Listeyi güncelle
                    Temizle();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ekleme hatası: " + ex.Message);
            }
        }

        // SİLME METODU
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silinecek kişiyi seçin.");
                return;
            }

            try
            {
                // Seçili satırdaki ID'yi al (Guid tipinde olduğu için string'e çeviriyoruz)
                string id = dgvPersonnel.SelectedRows[0].Cells["Id"].Value.ToString();

                var client = ConnectionProvider.GetClient();
                FirebaseResponse response = client.Delete("Users/" + id);

                MessageBox.Show("Kayıt silindi.");
                RefreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Silme hatası: " + ex.Message);
            }
        }

        // Formu temizleme yardımcısı
        private void Temizle()
        {
            txtFullName.Clear();
            txtUsername.Clear();
            txtPassword.Clear();
        }

        // GÜNCELLEME İÇİN SEÇİM (Opsiyonel ama kullanışlı)
        private void dgvPersonnel_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Başlık satırına tıklanmadıysa
            {
                DataGridViewRow row = dgvPersonnel.Rows[e.RowIndex];
                txtFullName.Text = row.Cells["FullName"].Value.ToString();
                txtUsername.Text = row.Cells["Username"].Value.ToString();
                txtPassword.Text = row.Cells["Password"].Value.ToString();
                cmbRole.SelectedItem = row.Cells["Role"].Value.ToString();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (dgvPersonnel.SelectedRows.Count == 0) return;

            try
            {
                string id = dgvPersonnel.SelectedRows[0].Cells["Id"].Value.ToString();
                var client = ConnectionProvider.GetClient();

                // Güncelleme için mevcut objeyi alıp değiştirebiliriz veya direkt set edebiliriz
                // Basitlik adına yeni verilerle üzerine yazıyoruz (Update mantığı Set ile aynıdır)
                User updatedUser = new User()
                {
                    Id = Guid.Parse(id),
                    FullName = txtFullName.Text,
                    Username = txtUsername.Text,
                    Password = txtPassword.Text,
                    Role = cmbRole.SelectedItem.ToString()
                };

                client.Update("Users/" + id, updatedUser);
                MessageBox.Show("Güncellendi.");
                RefreshList();
                Temizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Güncelleme hatası: " + ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close(); // Form kapanır, alttaki Ana Menü görünür.
        }
    }
}