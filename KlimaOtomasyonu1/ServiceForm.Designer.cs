namespace KlimaOtomasyonu1
{
    partial class ServiceForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServiceForm));
            this.label1 = new System.Windows.Forms.Label();
            this.cmbCustomers = new System.Windows.Forms.ComboBox();
            this.cmbDevices = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbTechnicians = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rbMaintenance = new System.Windows.Forms.RadioButton();
            this.rbRepair = new System.Windows.Forms.RadioButton();
            this.rbInstallation = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtPartCost = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLaborCost = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnCalculate = new System.Windows.Forms.Button();
            this.lblTotalPrice = new System.Windows.Forms.Label();
            this.btnSaveService = new System.Windows.Forms.Button();
            this.dgvHistory = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.btnAddItem = new System.Windows.Forms.Button();
            this.lstSelectedStocks = new System.Windows.Forms.ListBox();
            this.cmbStocks = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnExportExcel = new System.Windows.Forms.Button();
            this.btnPrint = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printPreviewDialog1 = new System.Windows.Forms.PrintPreviewDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(43, 67);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 23);
            this.label1.TabIndex = 0;
            this.label1.Text = "Müşteri Seç:";
            // 
            // cmbCustomers
            // 
            this.cmbCustomers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbCustomers.FormattingEnabled = true;
            this.cmbCustomers.Location = new System.Drawing.Point(168, 69);
            this.cmbCustomers.Name = "cmbCustomers";
            this.cmbCustomers.Size = new System.Drawing.Size(121, 24);
            this.cmbCustomers.TabIndex = 1;
            this.cmbCustomers.SelectedIndexChanged += new System.EventHandler(this.cmbCustomers_SelectedIndexChanged);
            // 
            // cmbDevices
            // 
            this.cmbDevices.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDevices.FormattingEnabled = true;
            this.cmbDevices.Location = new System.Drawing.Point(168, 99);
            this.cmbDevices.Name = "cmbDevices";
            this.cmbDevices.Size = new System.Drawing.Size(121, 24);
            this.cmbDevices.TabIndex = 3;
            this.cmbDevices.SelectedIndexChanged += new System.EventHandler(this.rbRepair_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.Location = new System.Drawing.Point(61, 99);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cihaz Seç:";
            // 
            // cmbTechnicians
            // 
            this.cmbTechnicians.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTechnicians.FormattingEnabled = true;
            this.cmbTechnicians.Location = new System.Drawing.Point(168, 135);
            this.cmbTechnicians.Name = "cmbTechnicians";
            this.cmbTechnicians.Size = new System.Drawing.Size(121, 24);
            this.cmbTechnicians.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.Location = new System.Drawing.Point(30, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "Teknisyen Ata:";
            // 
            // rbMaintenance
            // 
            this.rbMaintenance.AutoSize = true;
            this.rbMaintenance.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rbMaintenance.Location = new System.Drawing.Point(14, 67);
            this.rbMaintenance.Name = "rbMaintenance";
            this.rbMaintenance.Size = new System.Drawing.Size(165, 24);
            this.rbMaintenance.TabIndex = 6;
            this.rbMaintenance.TabStop = true;
            this.rbMaintenance.Text = "Periyodik Bakım";
            this.rbMaintenance.UseVisualStyleBackColor = true;
            // 
            // rbRepair
            // 
            this.rbRepair.AutoSize = true;
            this.rbRepair.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rbRepair.Location = new System.Drawing.Point(14, 33);
            this.rbRepair.Name = "rbRepair";
            this.rbRepair.Size = new System.Drawing.Size(140, 24);
            this.rbRepair.TabIndex = 7;
            this.rbRepair.TabStop = true;
            this.rbRepair.Text = "Arıza / Tamir";
            this.rbRepair.UseVisualStyleBackColor = true;
            this.rbRepair.CheckedChanged += new System.EventHandler(this.rbRepair_CheckedChanged);
            // 
            // rbInstallation
            // 
            this.rbInstallation.AutoSize = true;
            this.rbInstallation.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.rbInstallation.Location = new System.Drawing.Point(14, 3);
            this.rbInstallation.Name = "rbInstallation";
            this.rbInstallation.Size = new System.Drawing.Size(86, 24);
            this.rbInstallation.TabIndex = 8;
            this.rbInstallation.TabStop = true;
            this.rbInstallation.Text = "Montaj";
            this.rbInstallation.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.Location = new System.Drawing.Point(3, 297);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 23);
            this.label4.TabIndex = 9;
            this.label4.Text = "Açıklama / Şikayet:";
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(182, 297);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(113, 22);
            this.txtDescription.TabIndex = 10;
            // 
            // txtPartCost
            // 
            this.txtPartCost.Enabled = false;
            this.txtPartCost.Location = new System.Drawing.Point(168, 377);
            this.txtPartCost.Name = "txtPartCost";
            this.txtPartCost.Size = new System.Drawing.Size(113, 22);
            this.txtPartCost.TabIndex = 12;
            this.txtPartCost.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.Location = new System.Drawing.Point(45, 375);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 69);
            this.label5.TabIndex = 11;
            this.label5.Text = "Parça Ücreti\r\n\r\n\r\n";
            // 
            // txtLaborCost
            // 
            this.txtLaborCost.Location = new System.Drawing.Point(149, 486);
            this.txtLaborCost.Name = "txtLaborCost";
            this.txtLaborCost.Size = new System.Drawing.Size(113, 22);
            this.txtLaborCost.TabIndex = 14;
            this.txtLaborCost.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label6.Location = new System.Drawing.Point(30, 486);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 23);
            this.label6.TabIndex = 13;
            this.label6.Text = "İşçilik Ücreti:";
            // 
            // btnCalculate
            // 
            this.btnCalculate.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnCalculate.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnCalculate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCalculate.Location = new System.Drawing.Point(112, 524);
            this.btnCalculate.Name = "btnCalculate";
            this.btnCalculate.Size = new System.Drawing.Size(212, 33);
            this.btnCalculate.TabIndex = 15;
            this.btnCalculate.Text = "FİYAT HESAPLA";
            this.btnCalculate.UseVisualStyleBackColor = false;
            this.btnCalculate.Click += new System.EventHandler(this.btnCalculate_Click);
            // 
            // lblTotalPrice
            // 
            this.lblTotalPrice.AutoSize = true;
            this.lblTotalPrice.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.lblTotalPrice.Location = new System.Drawing.Point(222, 560);
            this.lblTotalPrice.Name = "lblTotalPrice";
            this.lblTotalPrice.Size = new System.Drawing.Size(50, 20);
            this.lblTotalPrice.TabIndex = 16;
            this.lblTotalPrice.Text = "0 TL";
            // 
            // btnSaveService
            // 
            this.btnSaveService.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnSaveService.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnSaveService.Location = new System.Drawing.Point(15, 586);
            this.btnSaveService.Name = "btnSaveService";
            this.btnSaveService.Size = new System.Drawing.Size(209, 33);
            this.btnSaveService.TabIndex = 17;
            this.btnSaveService.Text = "SERVİS KAYDI OLUŞTUR";
            this.btnSaveService.UseVisualStyleBackColor = false;
            this.btnSaveService.Click += new System.EventHandler(this.btnSaveService_Click);
            // 
            // dgvHistory
            // 
            this.dgvHistory.BackgroundColor = System.Drawing.Color.Navy;
            this.dgvHistory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dgvHistory.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHistory.Location = new System.Drawing.Point(0, 0);
            this.dgvHistory.Name = "dgvHistory";
            this.dgvHistory.ReadOnly = true;
            this.dgvHistory.RowHeadersWidth = 51;
            this.dgvHistory.RowTemplate.Height = 24;
            this.dgvHistory.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHistory.Size = new System.Drawing.Size(1204, 740);
            this.dgvHistory.TabIndex = 19;
            this.dgvHistory.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistory_CellContentClick);
            this.dgvHistory.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvHistory_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.btnRemoveItem);
            this.panel1.Controls.Add(this.btnAddItem);
            this.panel1.Controls.Add(this.lstSelectedStocks);
            this.panel1.Controls.Add(this.cmbStocks);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.btnExportExcel);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.cmbCustomers);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.btnSaveService);
            this.panel1.Controls.Add(this.lblTotalPrice);
            this.panel1.Controls.Add(this.cmbDevices);
            this.panel1.Controls.Add(this.btnCalculate);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.txtLaborCost);
            this.panel1.Controls.Add(this.cmbTechnicians);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.txtPartCost);
            this.panel1.Controls.Add(this.txtDescription);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(439, 740);
            this.panel1.TabIndex = 21;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label10.Location = new System.Drawing.Point(61, 560);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(140, 20);
            this.label10.TabIndex = 33;
            this.label10.Text = "Toplam Fiyat:";
            // 
            // btnRemoveItem
            // 
            this.btnRemoveItem.BackColor = System.Drawing.Color.Red;
            this.btnRemoveItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnRemoveItem.Location = new System.Drawing.Point(339, 421);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(53, 35);
            this.btnRemoveItem.TabIndex = 32;
            this.btnRemoveItem.Text = "-";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.btnRemoveItem_Click);
            // 
            // btnAddItem
            // 
            this.btnAddItem.BackColor = System.Drawing.Color.Green;
            this.btnAddItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnAddItem.Location = new System.Drawing.Point(290, 421);
            this.btnAddItem.Name = "btnAddItem";
            this.btnAddItem.Size = new System.Drawing.Size(43, 34);
            this.btnAddItem.TabIndex = 31;
            this.btnAddItem.Text = "+";
            this.btnAddItem.UseVisualStyleBackColor = false;
            this.btnAddItem.Click += new System.EventHandler(this.btnAddItem_Click);
            // 
            // lstSelectedStocks
            // 
            this.lstSelectedStocks.FormattingEnabled = true;
            this.lstSelectedStocks.ItemHeight = 16;
            this.lstSelectedStocks.Location = new System.Drawing.Point(168, 405);
            this.lstSelectedStocks.Name = "lstSelectedStocks";
            this.lstSelectedStocks.Size = new System.Drawing.Size(103, 68);
            this.lstSelectedStocks.TabIndex = 30;
            // 
            // cmbStocks
            // 
            this.cmbStocks.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStocks.FormattingEnabled = true;
            this.cmbStocks.Location = new System.Drawing.Point(174, 341);
            this.cmbStocks.Name = "cmbStocks";
            this.cmbStocks.Size = new System.Drawing.Size(121, 24);
            this.cmbStocks.TabIndex = 29;
            this.cmbStocks.SelectedIndexChanged += new System.EventHandler(this.cmbStocks_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label9.Location = new System.Drawing.Point(38, 342);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(137, 23);
            this.label9.TabIndex = 28;
            this.label9.Text = "Kullanılan Parça";
            // 
            // btnExportExcel
            // 
            this.btnExportExcel.BackColor = System.Drawing.Color.Green;
            this.btnExportExcel.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnExportExcel.Location = new System.Drawing.Point(240, 586);
            this.btnExportExcel.Name = "btnExportExcel";
            this.btnExportExcel.Size = new System.Drawing.Size(199, 33);
            this.btnExportExcel.TabIndex = 27;
            this.btnExportExcel.Text = "Excele Aktar";
            this.btnExportExcel.UseVisualStyleBackColor = false;
            this.btnExportExcel.Click += new System.EventHandler(this.btnExportExcel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.btnPrint.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.btnPrint.Location = new System.Drawing.Point(15, 625);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(209, 33);
            this.btnPrint.TabIndex = 26;
            this.btnPrint.Text = "Seçili Kaydı Yazdır";
            this.btnPrint.UseVisualStyleBackColor = false;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Crimson;
            this.button2.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button2.Location = new System.Drawing.Point(237, 625);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 33);
            this.button2.TabIndex = 25;
            this.button2.Text = "Seçili Kaydı Sil";
            this.button2.UseVisualStyleBackColor = false;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Verdana", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.button1.Location = new System.Drawing.Point(96, 664);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(212, 33);
            this.button1.TabIndex = 24;
            this.button1.Text = "Ana Menü";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label8.Location = new System.Drawing.Point(11, 210);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 23);
            this.label8.TabIndex = 23;
            this.label8.Text = "İşlem:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbInstallation);
            this.panel2.Controls.Add(this.rbMaintenance);
            this.panel2.Controls.Add(this.rbRepair);
            this.panel2.Location = new System.Drawing.Point(89, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 22;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label7.ForeColor = System.Drawing.Color.Navy;
            this.label7.Location = new System.Drawing.Point(89, 10);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(204, 38);
            this.label7.TabIndex = 21;
            this.label7.Text = "Servis Bilgileri";
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printPreviewDialog1
            // 
            this.printPreviewDialog1.AutoScrollMargin = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
            this.printPreviewDialog1.ClientSize = new System.Drawing.Size(400, 300);
            this.printPreviewDialog1.Document = this.printDocument1;
            this.printPreviewDialog1.Enabled = true;
            this.printPreviewDialog1.Icon = ((System.Drawing.Icon)(resources.GetObject("printPreviewDialog1.Icon")));
            this.printPreviewDialog1.Name = "printPreviewDialog1";
            this.printPreviewDialog1.Visible = false;
            // 
            // ServiceForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1204, 740);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgvHistory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ServiceForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Servis Kayıt İşlemleri";
            this.Load += new System.EventHandler(this.ServiceForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistory)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cmbCustomers;
        private System.Windows.Forms.ComboBox cmbDevices;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbTechnicians;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton rbMaintenance;
        private System.Windows.Forms.RadioButton rbRepair;
        private System.Windows.Forms.RadioButton rbInstallation;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtPartCost;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLaborCost;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnCalculate;
        private System.Windows.Forms.Label lblTotalPrice;
        private System.Windows.Forms.Button btnSaveService;
        private System.Windows.Forms.DataGridView dgvHistory;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintPreviewDialog printPreviewDialog1;
        private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Button btnExportExcel;
        private System.Windows.Forms.ComboBox cmbStocks;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Button btnAddItem;
        private System.Windows.Forms.ListBox lstSelectedStocks;
        private System.Windows.Forms.Label label10;
    }
}