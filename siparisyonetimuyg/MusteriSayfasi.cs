using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace siparisyonetimuyg
{
    public partial class MusteriSayfasi : Form
    {
        private readonly int _musteriID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True; MultipleActiveResultSets=True;";

        public MusteriSayfasi(int musteriID)
        {
            InitializeComponent();
            _musteriID = musteriID;


        }

        private void btnSepetim_Click(object sender, EventArgs e)
        {
            var sepetOnaylamaFormu = new MusteriSepetOnaylama(_musteriID);
            sepetOnaylamaFormu.Show();
        }


        private void btnsiparisolustur_Click(object sender, EventArgs e)
        {
            var siparisvermeformu = new MusteriSiparisVerme(_musteriID);
            siparisvermeformu.Show();
        }

        private void btnsiparisdurum_Click(object sender, EventArgs e)
        {
            var musteriSiparisDurumuGoruntuleme = new MusteriSiparisDurumu(_musteriID);
            musteriSiparisDurumuGoruntuleme.Show();
        }

        private void MusteriSayfasi_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerName FROM Customer WHERE CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", _musteriID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); // Tek bir değer alıyoruz

                    if (result != null)
                    {
                        txtMusteriAdi.Text = result.ToString();
                        txtMusteriID.Text = _musteriID.ToString(); // ID'yi direkt yazdırıyoruz
                    }
                    else
                    {
                        MessageBox.Show("Müşteri bilgisi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }

}