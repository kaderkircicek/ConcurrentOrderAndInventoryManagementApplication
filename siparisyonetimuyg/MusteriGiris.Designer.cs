namespace siparisyonetimuyg
{
    partial class MusteriGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MusteriGiris));
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            textBoxisim = new TextBox();
            textBoxsifre = new TextBox();
            buttongirisyap = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.FromArgb(255, 192, 192);
            label1.Location = new Point(135, 125);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 1;
            label1.Text = "İsim     ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(255, 192, 192);
            label2.Location = new Point(135, 221);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 2;
            label2.Text = "Şifre   ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.RoyalBlue;
            label3.Location = new Point(205, 20);
            label3.Name = "label3";
            label3.Size = new Size(231, 20);
            label3.TabIndex = 3;
            label3.Text = "    Müşteri Girişine Hoş Geldiniz    ";
            // 
            // textBoxisim
            // 
            textBoxisim.Location = new Point(452, 118);
            textBoxisim.Name = "textBoxisim";
            textBoxisim.Size = new Size(125, 27);
            textBoxisim.TabIndex = 4;
            // 
            // textBoxsifre
            // 
            textBoxsifre.Location = new Point(452, 214);
            textBoxsifre.Name = "textBoxsifre";
            textBoxsifre.Size = new Size(125, 27);
            textBoxsifre.TabIndex = 5;
            // 
            // buttongirisyap
            // 
            buttongirisyap.BackColor = Color.RoyalBlue;
            buttongirisyap.Location = new Point(274, 338);
            buttongirisyap.Name = "buttongirisyap";
            buttongirisyap.Size = new Size(94, 29);
            buttongirisyap.TabIndex = 6;
            buttongirisyap.Text = "Giriş Yap";
            buttongirisyap.UseVisualStyleBackColor = false;
            buttongirisyap.Click += buttongirisyap_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-1, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(680, 450);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // MusteriGiris
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(677, 450);
            Controls.Add(buttongirisyap);
            Controls.Add(textBoxsifre);
            Controls.Add(textBoxisim);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "MusteriGiris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Müşteri Girişi";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox textBoxisim;
        private TextBox textBoxsifre;
        private Button buttongirisyap;
        private PictureBox pictureBox1;
    }
}