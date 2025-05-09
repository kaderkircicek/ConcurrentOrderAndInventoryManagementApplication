namespace siparisyonetimuyg
{
    partial class MusteriSayfasi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusteriSayfasi));
            btnsiparisdurum = new Button();
            btnSepetim = new Button();
            pictureBox1 = new PictureBox();
            btnsiparisolustur = new Button();
            txtMusteriAdi = new TextBox();
            txtMusteriID = new TextBox();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnsiparisdurum
            // 
            btnsiparisdurum.BackColor = Color.LightPink;
            btnsiparisdurum.Location = new Point(295, 218);
            btnsiparisdurum.Name = "btnsiparisdurum";
            btnsiparisdurum.Size = new Size(181, 29);
            btnsiparisdurum.TabIndex = 2;
            btnsiparisdurum.Text = "Siparişlerimin Durumu";
            btnsiparisdurum.UseVisualStyleBackColor = false;
            btnsiparisdurum.Click += btnsiparisdurum_Click;
            // 
            // btnSepetim
            // 
            btnSepetim.BackColor = Color.LightPink;
            btnSepetim.Location = new Point(347, 159);
            btnSepetim.Name = "btnSepetim";
            btnSepetim.Size = new Size(94, 29);
            btnSepetim.TabIndex = 3;
            btnSepetim.Text = "Sepetim";
            btnSepetim.UseVisualStyleBackColor = false;
            btnSepetim.Click += btnSepetim_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(12, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(769, 619);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // btnsiparisolustur
            // 
            btnsiparisolustur.BackColor = Color.LightPink;
            btnsiparisolustur.Location = new Point(295, 294);
            btnsiparisolustur.Name = "btnsiparisolustur";
            btnsiparisolustur.Size = new Size(181, 29);
            btnsiparisolustur.TabIndex = 5;
            btnsiparisolustur.Text = "Sipariş Oluştur";
            btnsiparisolustur.UseVisualStyleBackColor = false;
            btnsiparisolustur.Click += btnsiparisolustur_Click;
            // 
            // txtMusteriAdi
            // 
            txtMusteriAdi.BackColor = Color.LightPink;
            txtMusteriAdi.Location = new Point(570, 52);
            txtMusteriAdi.Name = "txtMusteriAdi";
            txtMusteriAdi.Size = new Size(125, 27);
            txtMusteriAdi.TabIndex = 6;
            // 
            // txtMusteriID
            // 
            txtMusteriID.BackColor = Color.LightPink;
            txtMusteriID.Location = new Point(166, 49);
            txtMusteriID.Name = "txtMusteriID";
            txtMusteriID.Size = new Size(125, 27);
            txtMusteriID.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(464, 56);
            label1.Name = "label1";
            label1.Size = new Size(85, 20);
            label1.TabIndex = 8;
            label1.Text = "Müşteri Adı";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(63, 52);
            label2.Name = "label2";
            label2.Size = new Size(77, 20);
            label2.TabIndex = 9;
            label2.Text = "Müşteri ID";
            // 
            // MusteriSayfasi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(793, 643);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtMusteriID);
            Controls.Add(txtMusteriAdi);
            Controls.Add(btnsiparisolustur);
            Controls.Add(btnSepetim);
            Controls.Add(btnsiparisdurum);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "MusteriSayfasi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Müşteri Ana Sayfası";
            Load += MusteriSayfasi_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button btnsiparisdurum;
        private Button btnSepetim;
        private PictureBox pictureBox1;
        private Button btnsiparisolustur;
        private TextBox txtMusteriAdi;
        private TextBox txtMusteriID;
        private Label label1;
        private Label label2;
    }
}