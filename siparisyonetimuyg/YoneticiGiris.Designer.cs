namespace siparisyonetimuyg
{
    partial class YoneticiGiris
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(YoneticiGiris));
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
            label1.BackColor = Color.Red;
            label1.Location = new Point(161, 139);
            label1.Name = "label1";
            label1.Size = new Size(56, 20);
            label1.TabIndex = 0;
            label1.Text = "İsim     ";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Red;
            label2.Location = new Point(161, 220);
            label2.Name = "label2";
            label2.Size = new Size(51, 20);
            label2.TabIndex = 1;
            label2.Text = "Şifre   ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Red;
            label3.Location = new Point(255, 85);
            label3.Name = "label3";
            label3.Size = new Size(202, 20);
            label3.TabIndex = 2;
            label3.Text = "Yönetici Girişine Hoş Geldiniz";
            // 
            // textBoxisim
            // 
            textBoxisim.Location = new Point(394, 139);
            textBoxisim.Name = "textBoxisim";
            textBoxisim.Size = new Size(125, 27);
            textBoxisim.TabIndex = 3;
            // 
            // textBoxsifre
            // 
            textBoxsifre.Location = new Point(394, 213);
            textBoxsifre.Name = "textBoxsifre";
            textBoxsifre.Size = new Size(125, 27);
            textBoxsifre.TabIndex = 4;
            // 
            // buttongirisyap
            // 
            buttongirisyap.BackColor = Color.Red;
            buttongirisyap.Location = new Point(275, 280);
            buttongirisyap.Name = "buttongirisyap";
            buttongirisyap.Size = new Size(122, 29);
            buttongirisyap.TabIndex = 5;
            buttongirisyap.Text = "Giriş Yap";
            buttongirisyap.UseVisualStyleBackColor = false;
            buttongirisyap.Click += buttongirisyap_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(0, 1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(678, 448);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 6;
            pictureBox1.TabStop = false;
            // 
            // YoneticiGiris
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(676, 450);
            Controls.Add(buttongirisyap);
            Controls.Add(textBoxsifre);
            Controls.Add(textBoxisim);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "YoneticiGiris";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Yönetici Girişi";
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