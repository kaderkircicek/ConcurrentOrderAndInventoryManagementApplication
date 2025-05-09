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
using System.Threading;

namespace siparisyonetimuyg
{
    public partial class YoneticiUrunGuncelleme : Form
    {

        public static SemaphoreSlim semaphore = new SemaphoreSlim(0, 1);

        public YoneticiUrunGuncelleme()
        {
            InitializeComponent();
        }

        private async void YoneticiUrunGuncelleme_Load(object sender, EventArgs e)
        {
            UrunleriYukle();

        }


        public void UrunleriYukle()
        {

            try
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                string query = "SELECT ProductID, ProductName, Stock, Price FROM Products";
                SqlDataAdapter adapter = new SqlDataAdapter(query, VeriTabaniBaglantisi.baglanti);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;


                dataGridView1.Columns["ProductID"].Visible = false;
                dataGridView1.Columns["ProductName"].HeaderText = "Ürün Adı";
                dataGridView1.Columns["Stock"].HeaderText = "Stok";
                dataGridView1.Columns["Price"].HeaderText = "Fiyat";


                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.ReadOnly = !(column.Name == "Stock" || column.Name == "Price");
                }


                if (!dataGridView1.Columns.Contains("Sil"))
                {
                    DataGridViewButtonColumn silButton = new DataGridViewButtonColumn
                    {
                        HeaderText = "Sil",
                        Name = "Sil",
                        Text = "Sil",
                        UseColumnTextForButtonValue = true
                    };
                    dataGridView1.Columns.Add(silButton);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }

        }




        private async void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue;

                    int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                    int stock = Convert.ToInt32(row.Cells["Stock"].Value);
                    int price = Convert.ToInt32(row.Cells["Price"].Value);

                    string updateQuery = "UPDATE Products SET Stock = @Stock, Price = @Price WHERE ProductID = @ProductID";
                    using (SqlCommand command = new SqlCommand(updateQuery, VeriTabaniBaglantisi.baglanti))
                    {
                        command.Parameters.AddWithValue("@Stock", stock);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@ProductID", productId);


                        command.ExecuteNonQuery();

                    }
                }

                MessageBox.Show("Ürünlerin stok ve fiyat bilgileri güncellendi.");
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


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Sil"].Index)
            {
                int productId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ProductID"].Value);
                string productName = dataGridView1.Rows[e.RowIndex].Cells["ProductName"].Value.ToString();

                DialogResult result = MessageBox.Show(
                    $"{productName} ürününü silmek istediğinize emin misiniz?",
                    "Ürün Silme Onayı",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    SilUrun(productId);
                    UrunleriYukle();
                }
            }
        }



        private void SilUrun(int productId)
        {
            try
            {
                VeriTabaniBaglantisi.BaglantiKontrolu();
                string deleteLogsQuery = "DELETE FROM Logs WHERE OrderID IN (SELECT OrderID FROM Orders WHERE ProductID = @ProductID)";
                using (SqlCommand command = new SqlCommand(deleteLogsQuery, VeriTabaniBaglantisi.baglanti))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.ExecuteNonQuery();
                }

                
                string deleteOrdersQuery = "DELETE FROM Orders WHERE ProductID = @ProductID";
                using (SqlCommand command = new SqlCommand(deleteOrdersQuery, VeriTabaniBaglantisi.baglanti))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.ExecuteNonQuery();
                }

               
                string deleteProductQuery = "DELETE FROM Products WHERE ProductID = @ProductID";
                using (SqlCommand command = new SqlCommand(deleteProductQuery, VeriTabaniBaglantisi.baglanti))
                {
                    command.Parameters.AddWithValue("@ProductID", productId);
                    command.ExecuteNonQuery();
                }

               
                MessageBox.Show("Ürün başarıyla silindi.");

              
                for (int i = 0; i < MusteriSiparisVerme.MaxBekleyenThreadSayisi; i++)
                {
                    semaphore.Release();
                }
            }
            catch (Exception ex)
            {
                // Hata mesajı
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}







