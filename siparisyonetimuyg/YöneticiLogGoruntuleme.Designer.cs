namespace siparisyonetimuyg
{
    partial class YöneticiLogGoruntuleme
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
            dataGridViewLogs = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLogs).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewLogs
            // 
            dataGridViewLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLogs.Location = new Point(12, 32);
            dataGridViewLogs.Name = "dataGridViewLogs";
            dataGridViewLogs.RowHeadersWidth = 51;
            dataGridViewLogs.RowTemplate.Height = 29;
            dataGridViewLogs.Size = new Size(766, 391);
            dataGridViewLogs.TabIndex = 0;
            // 
            // YöneticiLogGoruntuleme
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Linen;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridViewLogs);
            MaximizeBox = false;
            Name = "YöneticiLogGoruntuleme";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sistem Mesajları";
            Load += YöneticiLogGoruntuleme_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewLogs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewLogs;
    }
}