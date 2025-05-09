using System;
using System.Collections.Concurrent;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siparisyonetimuyg
{
    public partial class MusteriSepetOnaylama : Form
    {
        private int customerID;
        private string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True; MultipleActiveResultSets=True;";
        private static readonly object logLock = new object();


        public MusteriSepetOnaylama(int customerID)
        {
            InitializeComponent();
            this.customerID = customerID;


        }

        private void MusteriSepetOnaylama_Load(object sender, EventArgs e)
        {
            ÜrünleriYukle();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT CustomerName FROM Customer WHERE CustomerID = @CustomerID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CustomerID", customerID);

                try
                {
                    connection.Open();
                    object result = command.ExecuteScalar(); 

                    if (result != null)
                    {
                        txtMusteriAdi.Text = result.ToString();
                        txtMusteriID.Text = customerID.ToString(); 
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

        private void ÜrünleriYukle()
        {
            if (dataGridView1.Columns.Count == 0)
            {

                dataGridView1.Columns.Add("OrderID", "Sipariş ID");
                dataGridView1.Columns["OrderID"].Visible = false;


                DataGridViewImageColumn imageColumn = new DataGridViewImageColumn();
                imageColumn.Name = "ProductImage";
                imageColumn.HeaderText = "Resim";
                dataGridView1.Columns.Add(imageColumn);


                dataGridView1.Columns.Add("ProductName", "İsim");
                dataGridView1.Columns.Add("Quantity", "Adet");
                dataGridView1.Columns.Add("Price", "Fiyat");
                dataGridView1.Columns.Add("TotalPrice", "Toplam Tutar");


                DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                btnColumn.HeaderText = "Sepetimden Çıkar";
                btnColumn.Text = "Sepetimden Çıkar";
                btnColumn.UseColumnTextForButtonValue = true;
                dataGridView1.Columns.Add(btnColumn);


                imageColumn.Width = 100;
                dataGridView1.RowTemplate.Height = 100;


                imageColumn.ImageLayout = DataGridViewImageCellLayout.Zoom;
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        SELECT o.OrderID, o.ProductID, p.ProductName, p.PicturePath, p.Price, o.Quantity, o.TotalPrice
        FROM Orders o
        INNER JOIN Products p ON o.ProductID = p.ProductID
        WHERE o.CustomerID = @CustomerID AND o.OrderValue = 0";

                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.SelectCommand.Parameters.AddWithValue("@CustomerID", customerID);

                DataTable dt = new DataTable();
                adapter.Fill(dt);

                dataGridView1.Rows.Clear();

                foreach (DataRow row in dt.Rows)
                {

                    string imagePath = row["PicturePath"] as string;
                    Image productImage = null;

                    if (!string.IsNullOrEmpty(imagePath) && System.IO.File.Exists(imagePath))
                    {
                        productImage = Image.FromFile(imagePath);
                    }


                    dataGridView1.Rows.Add(
                        row["OrderID"],
                        productImage,
                        row["ProductName"],
                        row["Quantity"],
                        row["Price"],
                        row["TotalPrice"],
                        "Sepetimden Çıkar"
                    );
                }
            }

        }


        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            if (e.ColumnIndex == 6 && e.RowIndex >= 0)
            {
                int orderID = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
                IptalEtOrder(orderID);
            }
        }



        private void IptalEtOrder(int orderID)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
        UPDATE Orders 
        SET 
            OrderStatus = 'Müşteri sepetinden siparişi çıkardı, sipariş iptal edildi', 
            OrderValue = -1
        WHERE 
            OrderID = @OrderID";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@OrderID", orderID);

                connection.Open();
                command.ExecuteNonQuery();

               
                string orderDetailsQuery = @"
        SELECT o.Quantity, c.CustomerType 
        FROM Orders o
        INNER JOIN Customer c ON o.CustomerID = c.CustomerID
        WHERE o.OrderID = @OrderID";

                SqlCommand detailsCommand = new SqlCommand(orderDetailsQuery, connection);
                detailsCommand.Parameters.AddWithValue("@OrderID", orderID);

                SqlDataReader reader = detailsCommand.ExecuteReader();

                if (reader.Read())
                {
                    int quantity = reader.GetInt32(0);
                    string customerType = reader.GetString(1);

                    LogKaydet(orderID, customerType, "BİLGİLENDİRME",
                        $"{customerType} Müşteri {customerID} 'ün {orderID} idli siparişi sepetinden çıkarıldı ve iptal edildi.",
                        quantity, customerID);  
                }
                ÜrünleriYukle();
            }
        }

        private async void btnOnayla_Click(object sender, EventArgs e)
        {
            GuncelleOrderValue();
            ÜrünleriYukle();

         
            var yoneticiForm = Application.OpenForms.OfType<YoneticiSiparisOnaylama>().FirstOrDefault();
            if (yoneticiForm != null)
            {
                await yoneticiForm.SiparisleriYukle();
            }

            
            var musteriFormlar = Application.OpenForms.OfType<MusteriSiparisDurumu>();
            foreach (var musteriForm in musteriFormlar)
            {
                SemaphoreSlim semaphore = MusteriSiparisDurumu.GetSemaphoreForCustomer(musteriForm.CustomerID);
                await semaphore.WaitAsync();
                try
                {
                    musteriForm.SiparisleriGoster();
                    musteriForm.KalanButceyiGoster();
                }
                finally
                {
                    semaphore.Release();
                }
            }
        }

        private async void GuncelleOrderValue()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string budgetQuery = @"
        SELECT 
            ISNULL(SUM(o.TotalPrice), 0) AS TotalOrderPrice,
            c.Budget,
            c.TotalSpent,
            c.CustomerType
        FROM Orders o
        INNER JOIN Customer c ON o.CustomerID = c.CustomerID
        WHERE o.CustomerID = @CustomerID AND o.OrderValue = 0
        GROUP BY c.Budget, c.TotalSpent, c.CustomerType";

                SqlCommand budgetCommand = new SqlCommand(budgetQuery, connection);
                budgetCommand.Parameters.AddWithValue("@CustomerID", customerID);

                connection.Open();
                SqlDataReader reader = budgetCommand.ExecuteReader();

                int totalOrderPrice = 0;
                int customerBudget = 0;
                int totalSpent = 0;
                string customerType = "";

                if (reader.Read())
                {
                    totalOrderPrice = reader.GetInt32(0);
                    customerBudget = reader.GetInt32(1);
                    totalSpent = reader.GetInt32(2);
                    customerType = reader.GetString(3);

                    int remainingBudget = customerBudget - totalSpent;

                    if (totalOrderPrice > remainingBudget)
                    {
                        string orderQuery = @"
                SELECT OrderID, Quantity 
                FROM Orders 
                WHERE CustomerID = @CustomerID AND OrderValue = 0";

                        SqlCommand orderCommand = new SqlCommand(orderQuery, connection);
                        orderCommand.Parameters.AddWithValue("@CustomerID", customerID);
                        SqlDataAdapter adapter = new SqlDataAdapter(orderCommand);

                        DataTable orderTable = new DataTable();
                        adapter.Fill(orderTable);

                        foreach (DataRow row in orderTable.Rows)
                        {
                            int orderID = (int)row["OrderID"];
                            int quantity = (int)row["Quantity"];
                            LogKaydet(orderID, customerType, "HATA",
                                $"{customerType} Müşteri {customerID} 'ün {orderID} idli siparişi bütçe yetersizliğinden iptal edildi.",
                                quantity, customerID);  
                        }


                        string updateFailedOrdersQuery = @"
                UPDATE Orders 
                SET 
                    OrderStatus = 'Bütçe yetersiz sipariş iptal edildi',
                    OrderValue = -2
                WHERE 
                    CustomerID = @CustomerID AND OrderValue = 0";

                        SqlCommand updateFailedOrdersCommand = new SqlCommand(updateFailedOrdersQuery, connection);
                        updateFailedOrdersCommand.Parameters.AddWithValue("@CustomerID", customerID);
                        updateFailedOrdersCommand.ExecuteNonQuery();

                        MessageBox.Show("Sepetinizdeki ürünlerin toplam fiyatı, kalan bütçenizi aşmaktadır.");
                        reader.Close();
                        connection.Close();
                        return;
                    }

                }

                reader.Close();


                string updateApprovedOrdersQuery = @"
        UPDATE Orders 
        SET 
            OrderValue = 1, 
            OrderDate = @OrderDate,
            OrderStatus = 'Müşteri onayladı, yönetici onayı bekleniyor'
        WHERE 
            CustomerID = @CustomerID AND OrderValue = 0";

                SqlCommand updateApprovedOrdersCommand = new SqlCommand(updateApprovedOrdersQuery, connection);
                updateApprovedOrdersCommand.Parameters.AddWithValue("@CustomerID", customerID);
                updateApprovedOrdersCommand.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                updateApprovedOrdersCommand.ExecuteNonQuery();


                string updateCustomerQuery = @"
        UPDATE Customer 
        SET 
            TotalSpent = @NewTotalSpent
        WHERE 
            CustomerID = @CustomerID";

                SqlCommand updateCustomerCommand = new SqlCommand(updateCustomerQuery, connection);
                updateCustomerCommand.Parameters.AddWithValue("@NewTotalSpent", totalSpent + totalOrderPrice);
                updateCustomerCommand.Parameters.AddWithValue("@CustomerID", customerID);
                updateCustomerCommand.ExecuteNonQuery();

                string orderDetailsQuery = @"
        SELECT OrderID, Quantity 
        FROM Orders 
        WHERE CustomerID = @CustomerID AND OrderValue = 1";

                SqlCommand orderDetailsCommand = new SqlCommand(orderDetailsQuery, connection);
                orderDetailsCommand.Parameters.AddWithValue("@CustomerID", customerID);
                SqlDataAdapter detailsAdapter = new SqlDataAdapter(orderDetailsCommand);

                DataTable detailsTable = new DataTable();
                detailsAdapter.Fill(detailsTable);

                foreach (DataRow row in detailsTable.Rows)
                {
                    int orderID = (int)row["OrderID"];
                    int quantity = (int)row["Quantity"];
                    LogKaydet(orderID, customerType, "BİLGİLENDİRME",
                        $"{customerType} Müşteri {customerID}, {orderID} idli verdiği {quantity} adet siparişi işleme alındı.",
                        quantity, customerID);  
                }

                connection.Close();
            } 

            MessageBox.Show("Sepetiniz onaylandı!");


            ÜrünleriYukle();
        }

        private void LogKaydet(int orderID, string customerType, string logType, string logDetails, int quantity, int customerID)
        {
            lock (logLock)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
    INSERT INTO Logs (CustomerID, OrderID, CustomerType, LogType, LogDetails, Quantity, LogDate)
    VALUES (@CustomerID, @OrderID, @CustomerType, @LogType, @LogDetails, @Quantity, @LogDate)";  

                    SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerID);  
                    command.Parameters.AddWithValue("@OrderID", orderID);
                    command.Parameters.AddWithValue("@CustomerType", customerType);
                    command.Parameters.AddWithValue("@LogType", logType);
                    command.Parameters.AddWithValue("@LogDetails", logDetails);
                    command.Parameters.AddWithValue("@Quantity", quantity);
                    command.Parameters.AddWithValue("@LogDate", DateTime.Now);

                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }




       
    }
}