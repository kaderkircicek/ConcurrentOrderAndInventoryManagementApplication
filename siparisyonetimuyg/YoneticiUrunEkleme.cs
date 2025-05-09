using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siparisyonetimuyg
{
    public partial class YoneticiUrunEkleme : Form
    {
        public static SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);
        public YoneticiUrunEkleme()
        {
            InitializeComponent();
        }

        private void btnurunekle_Click(object sender, EventArgs e)
        {
            string urunAdi = txtUrunAdi.Text.Trim();
            string stokMiktariStr = txtStok.Text.Trim();
            string fiyatStr = txtFiyat.Text.Trim();

          
            if (string.IsNullOrEmpty(urunAdi) || string.IsNullOrEmpty(stokMiktariStr) || string.IsNullOrEmpty(fiyatStr))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.");
                return;
            }

            if (!int.TryParse(stokMiktariStr, out int stokMiktari) || !int.TryParse(fiyatStr, out int fiyat))
            {
                MessageBox.Show("Stok ve fiyat bilgileri sayısal olmalıdır.");
                return;
            }

            try
            {
              
                VeriTabaniBaglantisi.BaglantiKontrolu();

               
                string insertQuery = "INSERT INTO Products (ProductName, Stock, Price) VALUES (@ProductName, @Stock, @Price)";
                using (SqlCommand command = new SqlCommand(insertQuery, VeriTabaniBaglantisi.baglanti))
                {
                   
                    command.Parameters.AddWithValue("@ProductName", urunAdi);
                    command.Parameters.AddWithValue("@Stock", stokMiktari);
                    command.Parameters.AddWithValue("@Price", fiyat);

                  
                    command.ExecuteNonQuery();
                }

                MessageBox.Show("Ürün başarıyla eklendi. ");
                for (int i = 0; i < MusteriSiparisVerme.MaxBekleyenThreadSayisi; i++)
                {
                    semaphore.Release();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}