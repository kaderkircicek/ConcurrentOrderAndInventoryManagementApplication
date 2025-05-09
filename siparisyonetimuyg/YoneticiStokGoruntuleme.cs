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
using System.Windows.Forms.DataVisualization.Charting;

namespace siparisyonetimuyg
{
    public partial class YoneticiStokGoruntuleme : Form
    {
        private Chart stokGrafigi;
        private DataGridView urunGrid;
        private string baglantiDizesi = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True; MultipleActiveResultSets=True"; // Gerçek bağlantı dizesi ile güncelle

        public YoneticiStokGoruntuleme()
        {
            InitializeComponent();
            OzelBileşenleriBaşlat();
            UrunVerileriniYukle();
        }

        public void OzelBileşenleriBaşlat()
        {
            this.Text = "Ürün Stok Görüntüleme";
            this.Size = new Size(800, 600);

            // Grafik Başlatma
            stokGrafigi = new Chart
            {
                Dock = DockStyle.Top,
                Height = 300
            };
            ChartArea grafikAlani = new ChartArea("StokDurumu");
            stokGrafigi.ChartAreas.Add(grafikAlani);
            stokGrafigi.Legends.Add(new Legend("Legend"));
            stokGrafigi.Titles.Add("Ürün Stok Durumu");

            this.Controls.Add(stokGrafigi);

            // DataGridView Başlatma
            urunGrid = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(urunGrid);
        }

        public void UrunVerileriniYukle()
        {
            try
            {
                using (SqlConnection baglanti = new SqlConnection(baglantiDizesi))
                {
                    string sorgu = "SELECT ProductID, ProductName, Stock, Price, PicturePath FROM Products";
                    SqlDataAdapter adaptör = new SqlDataAdapter(sorgu, baglanti);
                    DataTable veriTablosu = new DataTable();
                    adaptör.Fill(veriTablosu);

                    urunGrid.DataSource = veriTablosu;
                    urunGrid.SelectionChanged += UrunGrid_SecimDegisti;

                    GrafiğiGüncelle(veriTablosu);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Veri Yükleme Hatası", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GrafiğiGüncelle(DataTable veriTablosu)
        {
            stokGrafigi.Series.Clear();
            Series seri = new Series("Stok")
            {
                ChartType = SeriesChartType.Bar,
                IsValueShownAsLabel = true
            };

            foreach (DataRow satir in veriTablosu.Rows)
            {
                int stok = Convert.ToInt32(satir["Stock"]);
                string urunAdi = satir["ProductName"].ToString();

                DataPoint nokta = new DataPoint
                {
                    AxisLabel = urunAdi,
                    YValues = new[] { (double)stok }
                };

                
                if (stok < 10)
                    nokta.Color = Color.Red; 
                else if (stok < 50)
                    nokta.Color = Color.Orange; 
                else
                    nokta.Color = Color.Green; 

                seri.Points.Add(nokta);
            }

            stokGrafigi.Series.Add(seri);
        }

        private void UrunGrid_SecimDegisti(object sender, EventArgs e)
        {
            if (urunGrid.SelectedRows.Count > 0)
            {
                DataGridViewRow satir = urunGrid.SelectedRows[0];
                string resimYolu = satir.Cells["PicturePath"].Value.ToString();

                if (!string.IsNullOrEmpty(resimYolu) && System.IO.File.Exists(resimYolu))
                {
                    PictureBox resimKutusu = new PictureBox
                    {
                        Image = Image.FromFile(resimYolu),
                        SizeMode = PictureBoxSizeMode.Zoom,
                        Dock = DockStyle.Right,
                        Width = 200
                    };
                    this.Controls.Add(resimKutusu);
                }
            }
        }
    }
}

