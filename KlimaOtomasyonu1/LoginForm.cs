using System;
using System.Linq; // Veri sorgulama (LINQ) için gerekli
using System.Windows.Forms;
using FireSharp.Response;
using KlimaOtomasyonu1.Models; // Modelleri kullanmak için
using Newtonsoft.Json; // JSON verisini işlemek için
using System.Collections.Generic;

namespace KlimaOtomasyonu1
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        // GİRİŞ YAP BUTONU
        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifre giriniz.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var client = ConnectionProvider.GetClient();
                if (client == null)
                {
                    MessageBox.Show("Veritabanı bağlantısı kurulamadı! İnternet bağlantınızı kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 1. Firebase'den tüm kullanıcıları çekiyoruz
                FirebaseResponse response = client.Get("Users");

                if (response.Body == "null")
                {
                    MessageBox.Show("Sistemde kayıtlı kullanıcı yok. Önce Demo Admin oluşturun.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 2. Gelen veriyi Listeye çeviriyoruz (Dictionary olarak gelir çünkü Firebase ID'leri key olarak tutar)
                var usersDict = response.ResultAs<Dictionary<string, User>>();

                // 3. Kullanıcı adı ve şifre eşleşiyor mu kontrol ediyoruz (LINQ Kullanımı)
                var foundUser = usersDict.Values.FirstOrDefault(u => u.Username == txtUsername.Text && u.Password == txtPassword.Text);

                if (foundUser != null)
                {
                    MessageBox.Show($"Hoşgeldiniz, {foundUser.FullName} ({foundUser.Role})", "Giriş Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    //BURADA DASHBOARD FORMUNA YÖNLENDİRECEĞİZ (Henüz yapmadık)
                    DashboardForm dashboard = new DashboardForm(foundUser);
                     dashboard.Show();
                     this.Hide();
                }
                else
                {
                    MessageBox.Show("Kullanıcı adı veya şifre hatalı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Hata Yönetimi 
                MessageBox.Show("Bir hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}