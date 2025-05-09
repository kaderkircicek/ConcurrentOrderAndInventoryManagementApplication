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
    public partial class MusteriSiparisVerme : Form
    {
        private int _musteriID;
        private string _baglantiDizgesi = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True; MultipleActiveResultSets=True;";
        private static SemaphoreSlim semaphore = YoneticiUrunGuncelleme.semaphore;
        private static SemaphoreSlim semaphore2 = YoneticiUrunEkleme.semaphore;

        public static int MaxBekleyenThreadSayisi = 3;


        public MusteriSiparisVerme(int musteriID)
        {
            InitializeComponent();
            _musteriID = musteriID;
            Task.Run(() => DinamikGuncellemeleriBekle());
            Task.Run(() => DinamikEklemeleriBekle());

        }



        private void MusteriSiparisVerme_Load(object sender, EventArgs e)
        {
            UrunleriYukle();
         }

        private void UrunleriYukle()
        {
            if (InvokeRequired)
            {
                Invoke(new MethodInvoker(() => Controls.Clear()));
            }
            else
            {
                Controls.Clear();
            }

            VeriTabaniBaglantisi.BaglantiKontrolu();
            using (SqlCommand komut = new SqlCommand("SELECT ProductID, ProductName, Stock, Price, PicturePath FROM Products", VeriTabaniBaglantisi.baglanti))
            {
                SqlDataReader reader = komut.ExecuteReader();

                
                GroupBox urunGroupBox = new GroupBox
                {
                    Text = "Ürünler", // GroupBox başlığı
                    Dock = DockStyle.Fill,
                    Padding = new Padding(10),
                    AutoSize = true
                };

                FlowLayoutPanel urunPaneli = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    Padding = new Padding(20),
                    FlowDirection = FlowDirection.LeftToRight,
                    WrapContents = true
                };

                while (reader.Read())
                {
                    Panel urunKart = new Panel
                    {
                        Width = 220,
                        Height = 320,
                        BorderStyle = BorderStyle.FixedSingle,
                        Margin = new Padding(20)
                    };

                    string urunAdi = reader["ProductName"].ToString();
                    int urunID = (int)reader["ProductID"];
                    int fiyat = (int)reader["Price"];
                    string resimYolu = reader["PicturePath"].ToString();

                    PictureBox urunResmi = new PictureBox
                    {
                        Width = 200,
                        Height = 200,
                        SizeMode = PictureBoxSizeMode.StretchImage,
                        Dock = DockStyle.Top
                    };

                    try
                    {
                        if (File.Exists(resimYolu))
                        {
                            using (var stream = new FileStream(resimYolu, FileMode.Open, FileAccess.Read))
                            {
                                urunResmi.Image = Image.FromStream(stream);
                            }
                        }
                        else
                        {
                            urunResmi.Image = null;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Resim yüklenirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        urunResmi.Image = null;
                    }

                    Label isimEtiketi = new Label
                    {
                        Text = urunAdi,
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Top,
                        Font = new Font("Arial", 10, FontStyle.Bold)
                    };

                    Label fiyatEtiketi = new Label
                    {
                        Text = $"Fiyat: {fiyat} TL",
                        AutoSize = false,
                        TextAlign = ContentAlignment.MiddleCenter,
                        Dock = DockStyle.Top,
                        Font = new Font("Arial", 9, FontStyle.Regular)
                    };

                    NumericUpDown adetSecici = new NumericUpDown
                    {
                        Minimum = 1, 
                        Maximum = 5,
                        Value = 1,
                        Width = 60,
                        Margin = new Padding(5),
                        Dock = DockStyle.Bottom,
                        ReadOnly = true,
                        TextAlign = HorizontalAlignment.Center
                    };

                    Button sepeteEkleButonu = new Button
                    {
                        Text = "Sepete Ekle",
                        Dock = DockStyle.Bottom,
                        BackColor = Color.CornflowerBlue,
                        ForeColor = Color.White,
                        FlatStyle = FlatStyle.Flat,
                        Height = 40,
                        Font = new Font("Arial", 9, FontStyle.Bold)
                    };

                    sepeteEkleButonu.Click += (s, e) => SepeteEkle(urunID, (int)adetSecici.Value, fiyat);

                    urunKart.Controls.Add(urunResmi);
                    urunKart.Controls.Add(isimEtiketi);
                    urunKart.Controls.Add(fiyatEtiketi);
                    urunKart.Controls.Add(adetSecici);
                    urunKart.Controls.Add(sepeteEkleButonu);

                    urunPaneli.Controls.Add(urunKart);
                }

                reader.Close();

               
                urunGroupBox.Controls.Add(urunPaneli);

                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(() => Controls.Add(urunGroupBox)));
                }
                else
                {
                    Controls.Add(urunGroupBox);
                }
            }
        }

        private void SepeteEkle(int urunID, int adet, int fiyat)
        {
            if (adet > 5)
            {
                MessageBox.Show("En fazla 5 adet sipariş verebilirsiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int toplamFiyat = fiyat * adet;

            VeriTabaniBaglantisi.BaglantiKontrolu();

            using (SqlCommand komut = new SqlCommand(@"INSERT INTO Orders (CustomerID, ProductID, Quantity, TotalPrice, OrderValue, OrderDate, OrderStatus) 
                            VALUES (@MusteriID, @UrunID, @Adet, @ToplamFiyat, @OrderValue, @SiparisTarihi, @SiparisDurumu); 
                            SELECT SCOPE_IDENTITY();", VeriTabaniBaglantisi.baglanti))
            {
                komut.Parameters.AddWithValue("@MusteriID", _musteriID);
                komut.Parameters.AddWithValue("@UrunID", urunID);
                komut.Parameters.AddWithValue("@Adet", adet);
                komut.Parameters.AddWithValue("@ToplamFiyat", toplamFiyat);
                komut.Parameters.AddWithValue("@OrderValue", 0);
                komut.Parameters.AddWithValue("@SiparisTarihi", DateTime.Now);
                komut.Parameters.AddWithValue("@SiparisDurumu", "Müşteri sepetine ekledi henüz onay vermedi");

              
                int orderID = Convert.ToInt32(komut.ExecuteScalar());

               
                LogKaydiEkle(orderID, urunID, adet);

                MessageBox.Show("Ürün başarıyla sepete eklendi!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void LogKaydiEkle(int orderID, int urunID, int adet)
        {
            
            string customerType;
            using (SqlCommand customerCommand = new SqlCommand("SELECT CustomerType FROM Customer WHERE CustomerID = @CustomerID", VeriTabaniBaglantisi.baglanti))
            {
                customerCommand.Parameters.AddWithValue("@CustomerID", _musteriID);
                customerType = customerCommand.ExecuteScalar()?.ToString();
            }

            if (string.IsNullOrEmpty(customerType))
            {
                MessageBox.Show("Müşteri tipi alınamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

           
            string logDetails = $"{customerType} Müşteri {_musteriID} 'ün {orderID} idli siparişi müşteri tarafından sepetine eklendi.";

           
            using (SqlCommand logCommand = new SqlCommand(@"INSERT INTO Logs (CustomerID, OrderID, LogDate, CustomerType, Quantity, LogType, LogDetails) 
                            VALUES (@CustomerID, @OrderID, @LogDate, @CustomerType, @Quantity, @LogType, @LogDetails)", VeriTabaniBaglantisi.baglanti))
            {
                logCommand.Parameters.AddWithValue("@CustomerID", _musteriID);
                logCommand.Parameters.AddWithValue("@OrderID", orderID);
                logCommand.Parameters.AddWithValue("@LogDate", DateTime.Now);
                logCommand.Parameters.AddWithValue("@CustomerType", customerType);
                logCommand.Parameters.AddWithValue("@Quantity", adet);
                logCommand.Parameters.AddWithValue("@LogType", "BİLGİLENDİRME");
                logCommand.Parameters.AddWithValue("@LogDetails", logDetails);

                logCommand.ExecuteNonQuery();
            }
        }

        private async void DinamikGuncellemeleriBekle()
        {
            while (true)
            {
                await MusteriSiparisVerme.semaphore.WaitAsync();
                if (InvokeRequired)
                {
                    Invoke(new MethodInvoker(UrunleriYukle));
                }
                else
                {
                    UrunleriYukle();
                }
            }
        }

        private async void DinamikEklemeleriBekle()
        {
            while (true)
            {

                await semaphore2.WaitAsync();
                UrunleriYukle();
            }
        }

     
    }
}
