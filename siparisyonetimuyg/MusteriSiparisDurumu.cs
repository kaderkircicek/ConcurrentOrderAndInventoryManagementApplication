using System;
using System.Collections.Concurrent;
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
    public partial class MusteriSiparisDurumu : Form
    {
        public int CustomerID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True; MultipleActiveResultSets=True;";
        public static SemaphoreSlim Semaphore = new SemaphoreSlim(1, 1);
        public static SemaphoreSlim Semaphore2 = new SemaphoreSlim(1, 1);
        public static ConcurrentDictionary<int, SemaphoreSlim> customerSemaphores = new ConcurrentDictionary<int, SemaphoreSlim>();
        public MusteriSiparisDurumu(int CustomerID)
        {
            InitializeComponent();
            this.CustomerID = CustomerID;

        }

        public static SemaphoreSlim GetSemaphoreForCustomer(int customerId)
        {
           
            return customerSemaphores.GetOrAdd(customerId, _ => new SemaphoreSlim(1, 1));
        }


            private void MusteriSiparisDurumu_Load(object sender, EventArgs e)
        {
            KalanButceyiGoster();
            SiparisleriGoster();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerName FROM Customer WHERE CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar();

                    if (result != null)
                    {
                        txtMusteriAdi.Text = result.ToString();
                        txtMusteriID.Text = CustomerID.ToString();
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


        public void KalanButceyiGoster()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT Budget - TotalSpent AS RemainingBudget 
            FROM Customer 
            WHERE CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                connection.Open();

                object result = command.ExecuteScalar();
                if (result != null)
                {
                    txtKalanButce.Text = result.ToString() + " TL";
                }
            }
        }


        public void SiparisleriGoster()
        {
            string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT o.OrderID, 
                   p.ProductName, 
                   o.Quantity, 
                   o.OrderDate,
                   CASE 
                       WHEN o.OrderValue = 1 THEN 'Alındı'
                       WHEN o.OrderValue = 2 THEN 'Onaylandı'
                       WHEN o.OrderValue = -1 THEN 'İptal Edildi'
                   END AS Status
            FROM Orders o
            INNER JOIN Products p ON o.ProductID = p.ProductID
            WHERE o.CustomerID = @CustomerID AND o.OrderValue IN(1, 2, -1)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", CustomerID);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable table = new DataTable();
                adapter.Fill(table);

                flowLayoutPanel1.Controls.Clear();

                if (table.Rows.Count == 0)
                {
                    Label noOrdersLabel = new Label
                    {
                        Text = "Henüz sipariş bulunmamaktadır.",
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Font = new Font("Arial", 12, FontStyle.Italic),
                        TextAlign = ContentAlignment.MiddleCenter
                    };
                    flowLayoutPanel1.Controls.Add(noOrdersLabel);
                    return;
                }

                foreach (DataRow row in table.Rows)
                {
                    Panel siparisPanel = PanelGosterimi(
                        row["ProductName"].ToString(),
                        row["Quantity"].ToString(),
                        Convert.ToDateTime(row["OrderDate"]).ToString("dd/MM/yyyy"),
                        row["Status"].ToString()
                    );
                    flowLayoutPanel1.Controls.Add(siparisPanel);
                }
            }
        }


        private Panel PanelGosterimi(string productName, string quantity, string orderDate, string status)
        {
            Panel panel = new Panel
            {
                Size = new Size(450, 100),
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(10),
                Margin = new Padding(5)
            };

            Label lblProductName = new Label
            {
                Text = $"Ürün Adı: {productName}",
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };
            panel.Controls.Add(lblProductName);

            Label lblQuantity = new Label
            {
                Text = $"Miktarı: {quantity}",
                AutoSize = true,
                Location = new Point(0, 25)
            };
            panel.Controls.Add(lblQuantity);

            Label lblOrderDate = new Label
            {
                Text = $"SON Güncellenme Tarihi: {orderDate}",
                AutoSize = true,
                Location = new Point(0, 50)
            };
            panel.Controls.Add(lblOrderDate);

            Label lblStatus = new Label
            {
                Text = $"Siparişin Durumu: {status}",
                AutoSize = true,
                Location = new Point(0, 75),
                ForeColor = status switch
                {
                    "Alındı" => Color.Orange,
                    "Onaylandı" => Color.Green,
                    "İptal Edildi" => Color.Red,
                    _ => Color.Gray
                }
            };
            panel.Controls.Add(lblStatus);

            return panel;
        }


    }
}
