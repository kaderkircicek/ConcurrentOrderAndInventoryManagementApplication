namespace siparisyonetimuyg
{
    partial class YoneticiUrunEkleme
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoneticiUrunEkleme));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            btnurunekle = new Button();
            txtUrunAdi = new TextBox();
            txtStok = new TextBox();
            txtFiyat = new TextBox();
            label4 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Red;
            label1.Location = new Point(216, 135);
            label1.Name = "label1";
            label1.Size = new Size(67, 20);
            label1.TabIndex = 0;
            label1.Text = "Ürün Adı";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Red;
            label2.Location = new Point(216, 195);
            label2.Name = "label2";
            label2.Size = new Size(88, 20);
            label2.TabIndex = 1;
            label2.Text = "Stok Miktarı";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Red;
            label3.Location = new Point(216, 271);
            label3.Name = "label3";
            label3.Size = new Size(40, 20);
            label3.TabIndex = 2;
            label3.Text = "Fiyat";
            // 
            // btnurunekle
            // 
            btnurunekle.BackColor = Color.Red;
            btnurunekle.Location = new Point(395, 359);
            btnurunekle.Name = "btnurunekle";
            btnurunekle.Size = new Size(94, 29);
            btnurunekle.TabIndex = 3;
            btnurunekle.Text = "Ürün Ekle";
            btnurunekle.UseVisualStyleBackColor = false;
            btnurunekle.Click += btnurunekle_Click;
            // 
            // txtUrunAdi
            // 
            txtUrunAdi.Location = new Point(460, 128);
            txtUrunAdi.Name = "txtUrunAdi";
            txtUrunAdi.Size = new Size(125, 27);
            txtUrunAdi.TabIndex = 4;
            // 
            // txtStok
            // 
            txtStok.Location = new Point(460, 188);
            txtStok.Name = "txtStok";
            txtStok.Size = new Size(125, 27);
            txtStok.TabIndex = 5;
            // 
            // txtFiyat
            // 
            txtFiyat.Location = new Point(460, 264);
            txtFiyat.Name = "txtFiyat";
            txtFiyat.Size = new Size(125, 27);
            txtFiyat.TabIndex = 6;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Red;
            label4.Location = new Point(321, 47);
            label4.Name = "label4";
            label4.Size = new Size(168, 20);
            label4.TabIndex = 7;
            label4.Text = "Ürün Özelliklerini Giriniz";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(1, -4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(796, 442);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 8;
            pictureBox1.TabStop = false;
            // 
            // YoneticiUrunEkleme
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label4);
            Controls.Add(txtFiyat);
            Controls.Add(txtStok);
            Controls.Add(txtUrunAdi);
            Controls.Add(btnurunekle);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "YoneticiUrunEkleme";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Ürün Ekleme";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Button btnurunekle;
        private TextBox txtUrunAdi;
        private TextBox txtStok;
        private TextBox txtFiyat;
        private Label label4;
        private PictureBox pictureBox1;
    }
}