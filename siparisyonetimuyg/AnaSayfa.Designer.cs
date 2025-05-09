namespace siparisyonetimuyg
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            pictureBox1 = new PictureBox();
            buttonyonetici = new Button();
            buttonmusteri = new Button();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.RoyalBlue;
            label1.Location = new Point(346, 41);
            label1.Name = "label1";
            label1.Size = new Size(107, 20);
            label1.TabIndex = 3;
            label1.Text = "HOŞ GELDİNİZ";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(-5, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(810, 451);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // buttonyonetici
            // 
            buttonyonetici.BackColor = Color.RoyalBlue;
            buttonyonetici.Location = new Point(77, 195);
            buttonyonetici.Name = "buttonyonetici";
            buttonyonetici.Size = new Size(145, 29);
            buttonyonetici.TabIndex = 4;
            buttonyonetici.Text = "Yönetici Girişi";
            buttonyonetici.UseVisualStyleBackColor = false;
            buttonyonetici.Click += buttonyonetici_Click;
            // 
            // buttonmusteri
            // 
            buttonmusteri.BackColor = Color.RoyalBlue;
            buttonmusteri.Location = new Point(606, 195);
            buttonmusteri.Name = "buttonmusteri";
            buttonmusteri.Size = new Size(160, 29);
            buttonmusteri.TabIndex = 5;
            buttonmusteri.Text = "Müşteri Girişi";
            buttonmusteri.UseVisualStyleBackColor = false;
            buttonmusteri.Click += buttonmusteri_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.AliceBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(buttonmusteri);
            Controls.Add(buttonyonetici);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            MaximizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ANA SAYFA";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox1;
        private Button buttonyonetici;
        private Button buttonmusteri;
    }
}
