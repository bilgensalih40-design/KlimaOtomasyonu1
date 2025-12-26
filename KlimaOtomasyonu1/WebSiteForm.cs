using System;
using System.Collections.Generic;
using System.ComponentModel;
using System;
using System.Windows.Forms;
 using Microsoft.Web.WebView2.Core;

namespace KlimaOtomasyonu1
{
    public partial class WebSiteForm : Form
    {
        public WebSiteForm()
        {
            InitializeComponent();
            Baslat();
        }

        private async void Baslat()
        {
            // WebView2'nin hazır olmasını bekle
            await webView.EnsureCoreWebView2Async(null);

            // Senin sitene git
            webView.Source = new Uri("https://bsklimaankara.com.tr/");
        }

        // Eğer geri butonu koyduysan:
        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
