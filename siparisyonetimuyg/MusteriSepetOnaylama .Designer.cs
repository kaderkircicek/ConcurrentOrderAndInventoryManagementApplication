namespace siparisyonetimuyg
{
    partial class MusteriSepetOnaylama
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
            dataGridView1 = new DataGridView();
            btnOnayla = new Button();
            label1 = new Label();
            txtMusteriID = new TextBox();
            label2 = new Label();
            txtMusteriAdi = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // dataGridView1
            // 
            dataGridView1.BackgroundColor = SystemColors.ButtonHighlight;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(59, 81);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(790, 365);
            dataGridView1.TabIndex = 0;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            // 
            // btnOnayla
            // 
            btnOnayla.BackColor = SystemColors.ButtonHighlight;
            btnOnayla.Location = new Point(377, 452);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(141, 29);
            btnOnayla.TabIndex = 1;
            btnOnayla.Text = "Sepetimi Onayla";
            btnOnayla.UseVisualStyleBackColor = false;
            btnOnayla.Click += btnOnayla_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(108, 48);
            label1.Name = "label1";
            label1.Size = new Size(77, 20);
            label1.TabIndex = 2;
            label1.Text = "Müşteri ID";
            // 
            // txtMusteriID
            // 
            txtMusteriID.Location = new Point(209, 48);
            txtMusteriID.Name = "txtMusteriID";
            txtMusteriID.Size = new Size(125, 27);
            txtMusteriID.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(477, 44);
            label2.Name = "label2";
            label2.Size = new Size(85, 20);
            label2.TabIndex = 4;
            label2.Text = "Müşteri Adı";
            // 
            // txtMusteriAdi
            // 
            txtMusteriAdi.Location = new Point(587, 41);
            txtMusteriAdi.Name = "txtMusteriAdi";
            txtMusteriAdi.Size = new Size(125, 27);
            txtMusteriAdi.TabIndex = 5;
            // 
            // MusteriSepetOnaylama
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightPink;
            ClientSize = new Size(883, 504);
            Controls.Add(txtMusteriAdi);
            Controls.Add(label2);
            Controls.Add(txtMusteriID);
            Controls.Add(label1);
            Controls.Add(btnOnayla);
            Controls.Add(dataGridView1);
            Name = "MusteriSepetOnaylama";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sepet Onaylama";
            Load += MusteriSepetOnaylama_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView1;
        private Button btnOnayla;
        private Label label1;
        private TextBox txtMusteriID;
        private Label label2;
        private TextBox txtMusteriAdi;
    }
}