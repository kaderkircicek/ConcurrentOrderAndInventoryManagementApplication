namespace siparisyonetimuyg
{
    partial class MusteriSiparisDurumu
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
            txtKalanButce = new TextBox();
            label1 = new Label();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label2 = new Label();
            txtMusteriID = new TextBox();
            label3 = new Label();
            txtMusteriAdi = new TextBox();
            SuspendLayout();
            // 
            // txtKalanButce
            // 
            txtKalanButce.Location = new Point(776, 16);
            txtKalanButce.Name = "txtKalanButce";
            txtKalanButce.Size = new Size(125, 27);
            txtKalanButce.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(683, 20);
            label1.Name = "label1";
            label1.Size = new Size(87, 20);
            label1.TabIndex = 1;
            label1.Text = "Kalan Bütçe";
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.Location = new Point(26, 64);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(917, 455);
            flowLayoutPanel1.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(61, 23);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 9;
            label2.Text = "Müşteri Adı";
            // 
            // txtMusteriID
            // 
            txtMusteriID.Location = new Point(483, 16);
            txtMusteriID.Name = "txtMusteriID";
            txtMusteriID.Size = new Size(125, 27);
            txtMusteriID.TabIndex = 10;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(392, 23);
            label3.Name = "label3";
            label3.Size = new Size(77, 20);
            label3.TabIndex = 11;
            label3.Text = "Müşteri ID";
            // 
            // txtMusteriAdi
            // 
            txtMusteriAdi.Location = new Point(163, 16);
            txtMusteriAdi.Name = "txtMusteriAdi";
            txtMusteriAdi.Size = new Size(125, 27);
            txtMusteriAdi.TabIndex = 12;
            // 
            // MusteriSiparisDurumu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(968, 565);
            Controls.Add(txtMusteriID);
            Controls.Add(txtMusteriAdi);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(label1);
            Controls.Add(txtKalanButce);
            Name = "MusteriSiparisDurumu";
            Text = "Sipariş Durumum";
            Load += MusteriSiparisDurumu_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox txtKalanButce;
        private Label label1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label2;
        private TextBox txtMusteriID;
        private Label label3;
        private TextBox txtMusteriAdi;
    }
}