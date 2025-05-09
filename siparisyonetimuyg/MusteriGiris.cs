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
    public partial class MusteriGiris : Form
    {
        public MusteriGiris()
        {
            InitializeComponent();
        }

        private void buttongirisyap_Click(object sender, EventArgs e)
        {
            string isim = textBoxisim.Text.Trim();
            string sifre = textBoxsifre.Text.Trim();


            if (string.IsNullOrEmpty(isim) || string.IsNullOrEmpty(sifre))
            {
                MessageBox.Show("Lütfen kullanıcı adı ve şifrenizi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {

                VeriTabaniBaglantisi.BaglantiKontrolu();


                string query = "SELECT CustomerID FROM Customer WHERE CustomerName = @CustomerName AND Password = @Password";

                using (SqlCommand command = new SqlCommand(query, VeriTabaniBaglantisi.baglanti))
                {

                    command.Parameters.AddWithValue("@CustomerName", isim);
                    command.Parameters.AddWithValue("@Password", sifre);


                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        int customerID = Convert.ToInt32(result);


                        MusteriSayfasi musteriSayfasi = new MusteriSayfasi(customerID);
                        musteriSayfasi.Show();

                    }
                    else
                    {
                        MessageBox.Show("Kullanıcı adı veya şifre hatalı.", "Giriş Başarısız", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Veri tabanı hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                if (VeriTabaniBaglantisi.baglanti.State == System.Data.ConnectionState.Open)
                {
                    VeriTabaniBaglantisi.baglanti.Close();
                }
            }
        }
    }
}