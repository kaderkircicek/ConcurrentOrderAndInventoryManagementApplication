namespace siparisyonetimuyg
{
    partial class YoneticiSayfasi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoneticiSayfasi));
            pictureBox1 = new PictureBox();
            btnsiparisekranı = new Button();
            btnurunekranı = new Button();
            btnurunekle = new Button();
            btnYntciLog = new Button();
            btnUrunStokGoruntuleme = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(8, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(780, 417);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // btnsiparisekranı
            // 
            btnsiparisekranı.BackColor = Color.Linen;
            btnsiparisekranı.Location = new Point(265, 52);
            btnsiparisekranı.Name = "btnsiparisekranı";
            btnsiparisekranı.Size = new Size(295, 30);
            btnsiparisekranı.TabIndex = 1;
            btnsiparisekranı.Text = "Siparişleri Görüntüle Ve Onayla";
            btnsiparisekranı.UseVisualStyleBackColor = false;
            btnsiparisekranı.Click += btnsiparisekranı_Click;
            // 
            // btnurunekranı
            // 
            btnurunekranı.BackColor = Color.Linen;
            btnurunekranı.Location = new Point(265, 132);
            btnurunekranı.Name = "btnurunekranı";
            btnurunekranı.Size = new Size(295, 30);
            btnurunekranı.TabIndex = 2;
            btnurunekranı.Text = "Ürünleri Güncelle Veya Sil";
            btnurunekranı.UseVisualStyleBackColor = false;
            btnurunekranı.Click += btnurunekranı_Click;
            // 
            // btnurunekle
            // 
            btnurunekle.BackColor = Color.Linen;
            btnurunekle.Location = new Point(265, 272);
            btnurunekle.Name = "btnurunekle";
            btnurunekle.Size = new Size(295, 30);
            btnurunekle.TabIndex = 3;
            btnurunekle.Text = "Ürün Ekle";
            btnurunekle.UseVisualStyleBackColor = false;
            btnurunekle.Click += btnurunekle_Click;
            // 
            // btnYntciLog
            // 
            btnYntciLog.BackColor = Color.Linen;
            btnYntciLog.Location = new Point(265, 347);
            btnYntciLog.Name = "btnYntciLog";
            btnYntciLog.Size = new Size(295, 30);
            btnYntciLog.TabIndex = 5;
            btnYntciLog.Text = "Sistem Mesajlarını Görüntüle";
            btnYntciLog.UseVisualStyleBackColor = false;
            btnYntciLog.Click += btnYntciLog_Click;
            // 
            // btnUrunStokGoruntuleme
            // 
            btnUrunStokGoruntuleme.BackColor = Color.Linen;
            btnUrunStokGoruntuleme.Location = new Point(265, 209);
            btnUrunStokGoruntuleme.Name = "btnUrunStokGoruntuleme";
            btnUrunStokGoruntuleme.Size = new Size(295, 30);
            btnUrunStokGoruntuleme.TabIndex = 6;
            btnUrunStokGoruntuleme.Text = "Ürün Stoklarını Görüntüleme";
            btnUrunStokGoruntuleme.UseVisualStyleBackColor = false;
            btnUrunStokGoruntuleme.Click += btnUrunStokGoruntuleme_Click;
            // 
            // YoneticiSayfasi
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(800, 450);
            Controls.Add(btnUrunStokGoruntuleme);
            Controls.Add(btnYntciLog);
            Controls.Add(btnurunekle);
            Controls.Add(btnurunekranı);
            Controls.Add(btnsiparisekranı);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "YoneticiSayfasi";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "YÖNETİCİ ANA SAYFASI";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private PictureBox pictureBox1;
        private Button btnsiparisekranı;
        private Button btnurunekranı;
        private Button btnurunekle;
        private Button btnYntciLog;
        private Button btnUrunStokGoruntuleme;
    }
}