using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siparisyonetimuyg
{
    public partial class YoneticiSiparisOnaylama : Form
    {
        private static readonly SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);
        private System.Windows.Forms.Timer oncelikTimer; 
        private readonly string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True;";
        public YoneticiSiparisOnaylama()
        {
            InitializeComponent();
            oncelikTimer = new System.Windows.Forms.Timer(); 
            oncelikTimer.Interval = 1000; 
            oncelikTimer.Tick += async (s, args) => { await SiparisleriYukle(); }; 
            oncelikTimer.Start();

        }

        private async void YoneticiSiparisOnaylama_Load(object sender, EventArgs e)
        {
            await SiparisleriYukle();
        }

        public async Task SiparisleriYukle()
        {
            await semaphore.WaitAsync();
            try
            {
                if (!IsHandleCreated || IsDisposed)
                {

                    return;

                }
                Invoke((MethodInvoker)(() => VerileriTemizleVeHazirla()));

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();
                    string query = @"
                SELECT 
                    Orders.OrderID,
                    Orders.CustomerID,
                    Customer.CustomerName,
                    Customer.CustomerType,
                    Orders.ProductID,
                    Products.ProductName,
                    Products.PicturePath,
                    Orders.Quantity,
                    Orders.TotalPrice,
                    Orders.OrderDate
                FROM Orders
                INNER JOIN Customer ON Orders.CustomerID = Customer.CustomerID
                INNER JOIN Products ON Orders.ProductID = Products.ProductID
                WHERE Orders.OrderValue = 1";

                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    var orderedRows = dataTable.AsEnumerable()
                        .OrderByDescending(row =>
                        {
                            string customerType = row["CustomerType"].ToString();
                            DateTime orderDate = Convert.ToDateTime(row["OrderDate"]);
                            return HesaplaOncelikSkoru(customerType, orderDate);
                        })
                        .ToList();

                    if (orderedRows.Any())
                    {
                        var resultTable = orderedRows.CopyToDataTable();
                        if (IsHandleCreated && !IsDisposed)
                        {
                            Invoke((MethodInvoker)(() => TabloOlustur(resultTable)));
                        }

                       
                        if (!oncelikTimer.Enabled)
                        {
                            oncelikTimer.Start();
                        }
                    }
                    else
                    {
                        if (IsHandleCreated && !IsDisposed)
                        {
                            Invoke((MethodInvoker)(() => MessageBox.Show("Listelenecek sipariş bulunamadı.")));
                        }

                       
                        if (oncelikTimer.Enabled)
                        {
                            oncelikTimer.Stop();
                        }
                    }
                }
            }
            finally
            {
                semaphore.Release();
            }
        }



        private void VerileriTemizleVeHazirla()
        {
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            if (!dataGridView1.Columns.Contains("ProductImage"))
            {
                DataGridViewImageColumn imgColumn = new DataGridViewImageColumn
                {
                    HeaderText = "Ürün Resmi",
                    Name = "ProductImage",
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                dataGridView1.Columns.Add(imgColumn);
            }

            if (!dataGridView1.Columns.Contains("WaitingTime"))
            {
                dataGridView1.Columns.Add("WaitingTime", "Bekleme Süresi (sn)");
            }

            if (!dataGridView1.Columns.Contains("PriorityScore"))
            {
                dataGridView1.Columns.Add("PriorityScore", "Öncelik Skoru");
            }
        }

        private void TabloOlustur(DataTable dataTable)
        {
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["PicturePath"].Visible = false;

            
            dataGridView1.Columns["OrderID"].HeaderText = "Sipariş ID";
            dataGridView1.Columns["CustomerID"].HeaderText = "Müşteri ID";
            dataGridView1.Columns["ProductID"].HeaderText = "Ürün ID";
            dataGridView1.Columns["CustomerType"].HeaderText = "Müşteri Türü";
            dataGridView1.Columns["CustomerName"].HeaderText = "Müşteri Adı";
            dataGridView1.Columns["ProductName"].HeaderText = "Ürün Adı";
            dataGridView1.Columns["Quantity"].HeaderText = "Adet";
            dataGridView1.Columns["TotalPrice"].HeaderText = "Toplam Fiyat";
            dataGridView1.Columns["OrderDate"].HeaderText = "Sipariş Tarihi";

         
            if (!dataGridView1.Columns.Contains("ProductImage"))
            {
                var productImageColumn = new DataGridViewImageColumn
                {
                    Name = "ProductImage",
                    HeaderText = "Ürün Görseli",
                    Width = 50,
                    ImageLayout = DataGridViewImageCellLayout.Zoom
                };
                dataGridView1.Columns.Add(productImageColumn);
            }

            if (!dataGridView1.Columns.Contains("WaitingTime"))
            {
                var waitingTimeColumn = new DataGridViewTextBoxColumn
                {
                    Name = "WaitingTime",
                    HeaderText = "Bekleme Süresi (sn)",
                    Width = 100
                };
                dataGridView1.Columns.Add(waitingTimeColumn);
            }

            if (!dataGridView1.Columns.Contains("PriorityScore"))
            {
                var priorityScoreColumn = new DataGridViewTextBoxColumn
                {
                    Name = "PriorityScore",
                    HeaderText = "Öncelik Skoru",
                    Width = 100
                };
                dataGridView1.Columns.Add(priorityScoreColumn);
            }

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells["PicturePath"].Value is string imagePath && System.IO.File.Exists(imagePath))
                {
                    row.Cells["ProductImage"].Value = Image.FromFile(imagePath);
                }

                if (row.Cells["OrderDate"].Value is DateTime orderDate)
                {
                    double beklemeSuresi = (DateTime.Now - orderDate).TotalSeconds;
                    row.Cells["WaitingTime"].Value = beklemeSuresi.ToString("F1");

                    string customerType = row.Cells["CustomerType"].Value.ToString();
                    int priorityScore = HesaplaOncelikSkoru(customerType, orderDate);
                    row.Cells["PriorityScore"].Value = priorityScore;
                }
            }

            
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
            dataGridView1.RowTemplate.Height = 100;
            dataGridView1.Columns["CustomerName"].Width = 50;
            dataGridView1.Columns["ProductName"].Width = 50;
            dataGridView1.Columns["Quantity"].Width = 50;
            dataGridView1.Columns["TotalPrice"].Width = 50;
            dataGridView1.Columns["OrderDate"].Width = 50;
            dataGridView1.Columns["WaitingTime"].Width = 50;
            dataGridView1.Columns["PriorityScore"].Width = 50;
        }



        private int HesaplaOncelikSkoru(string customerType, DateTime orderDate)
        {
            int temelSkor = customerType == "Premium" ? 15 : 10;
            double beklemeSuresi = (DateTime.Now - orderDate).TotalSeconds;
            double beklemeAgirligi = beklemeSuresi * 0.5;
            return (int)(temelSkor + beklemeAgirligi);
        }


        private async void btnOnayla_Click(object sender, EventArgs e)
        {

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Onaylanacak sipariş bulunmamaktadır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                MessageBox.Show("Semafor çalışıyor, işlem başlıyor.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);

                bool onaylananSiparisVar = false;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.IsNewRow) continue; // Yeni eklenmiş boş satırı atla
                    if (row.Cells["OrderID"].Value == null || row.Cells["CustomerID"].Value == null)
                    {
                        MessageBox.Show("Sipariş bilgileri eksik. Lütfen kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }
                    int orderId = Convert.ToInt32(row.Cells["OrderID"].Value);
                    int customerId = Convert.ToInt32(row.Cells["CustomerID"].Value);
                    int productId = Convert.ToInt32(row.Cells["ProductID"].Value);
                    int quantity = Convert.ToInt32(row.Cells["Quantity"].Value);

                    await semaphore.WaitAsync();
                    try
                    {
                        bool isOnaylandi = await StokKontrolVeGuncelle(productId, quantity, customerId, orderId);
                        if (isOnaylandi)
                        {
                            await GuncelleOrder(orderId);
                            onaylananSiparisVar = true;


                            var musteriForm = Application.OpenForms
                                .OfType<MusteriSiparisDurumu>()
                                .FirstOrDefault(form => form.CustomerID == customerId);

                            if (musteriForm != null)
                            {
                                musteriForm.Invoke((Action)(() =>
                                {
                                    musteriForm.SiparisleriGoster();
                                    musteriForm.KalanButceyiGoster();
                                }));
                            }


                            var yoneticiStokForm = Application.OpenForms
                                .OfType<YoneticiStokGoruntuleme>()
                                .FirstOrDefault();

                            if (yoneticiStokForm != null)
                            {
                                yoneticiStokForm.Invoke((Action)(() =>
                                {

                                    yoneticiStokForm.UrunVerileriniYukle();
                                }));
                            }



                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }

                if (onaylananSiparisVar)
                {
                    MessageBox.Show("Tüm uygun siparişler onaylandı.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Onaylanacak sipariş bulunmamaktadır.", "Bilgilendirme", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                await SiparisleriYukle();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Bir hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }


        private async Task GuncelleOrder(int orderId)
        {
            string query = @"
                UPDATE Orders 
                SET OrderValue = 2, 
                    OrderDate = @OrderDate, 
                    OrderStatus = 'Yönetici onayladı' 
                WHERE OrderID = @OrderID";

            using (SqlConnection connection = new SqlConnection(connectionString))
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@OrderID", orderId);
                command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                await connection.OpenAsync();
                await command.ExecuteNonQueryAsync();
            }
        }


        private async Task<bool> StokKontrolVeGuncelle(int productId, int quantity, int customerId, int orderId)
        {
            if (customerId == 0)
            {
                MessageBox.Show("Geçersiz müşteri ID.");
                return false; 
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string stokQuery = "SELECT Stock FROM Products WHERE ProductID = @ProductID";
                SqlCommand stokCommand = new SqlCommand(stokQuery, connection);
                stokCommand.Parameters.AddWithValue("@ProductID", productId);

                int currentStock = Convert.ToInt32(await stokCommand.ExecuteScalarAsync());

                if (currentStock >= quantity)
                {
                    
                    string stokGuncelleQuery = "UPDATE Products SET Stock = Stock - @Quantity WHERE ProductID = @ProductID";
                    SqlCommand stokGuncelleCommand = new SqlCommand(stokGuncelleQuery, connection);
                    stokGuncelleCommand.Parameters.AddWithValue("@Quantity", quantity);
                    stokGuncelleCommand.Parameters.AddWithValue("@ProductID", productId);
                    await stokGuncelleCommand.ExecuteNonQueryAsync();

                  
                    await LogKaydet(customerId, orderId, productId, quantity, "BİLGİLENDİRME", $"Müşteri {customerId} ürün {productId}'den {quantity} adet verdiği sipariş onaylandı.");
                    return true; 
                }
                else
                {
                    
                    string query = @"
                UPDATE Orders 
                SET OrderValue = -1, 
                    OrderDate = @OrderDate, 
                    OrderStatus = 'Yönetici stok yetersizliği nedeniyle siparişi iptal etti' 
                WHERE OrderID = @OrderID";

                    SqlCommand updateOrderCommand = new SqlCommand(query, connection);
                    updateOrderCommand.Parameters.AddWithValue("@OrderID", orderId);
                    updateOrderCommand.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                    await updateOrderCommand.ExecuteNonQueryAsync();


                    string totalPriceQuery = "SELECT TotalPrice FROM Orders WHERE OrderID = @OrderID";
                    SqlCommand totalPriceCommand = new SqlCommand(totalPriceQuery, connection);
                    totalPriceCommand.Parameters.AddWithValue("@OrderID", orderId);
                    decimal totalPrice = Convert.ToDecimal(await totalPriceCommand.ExecuteScalarAsync());

                   
                    string updateCustomerSpentQuery = @"
                UPDATE Customer
                SET TotalSpent = TotalSpent - @TotalPrice
                WHERE CustomerID = @CustomerID";

                    SqlCommand updateCustomerSpentCommand = new SqlCommand(updateCustomerSpentQuery, connection);
                    updateCustomerSpentCommand.Parameters.AddWithValue("@TotalPrice", totalPrice);
                    updateCustomerSpentCommand.Parameters.AddWithValue("@CustomerID", customerId);
                    await updateCustomerSpentCommand.ExecuteNonQueryAsync();


                   
                    await LogKaydet(customerId, orderId, productId, quantity, "HATA", $"Müşteri {customerId} ürün {productId}'den {quantity} adet istedi ancak stok yetersiz.");

                    return false;
                }
            }
        }

        private async Task LogKaydet(int customerId, int orderId, int productId, int quantity, string logType, string logDetails)
        {
           
            MessageBox.Show($"CustomerID: {customerId}", "Log Kaydı", MessageBoxButtons.OK, MessageBoxIcon.Information);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                string customerTypeQuery = "SELECT CustomerType FROM Customer WHERE CustomerID = @CustomerID";
                SqlCommand customerTypeCommand = new SqlCommand(customerTypeQuery, connection);
                customerTypeCommand.Parameters.AddWithValue("@CustomerID", customerId);

                object result = await customerTypeCommand.ExecuteScalarAsync();
                string customerType = result != null ? result.ToString() : "Unknown"; 

                string logInsertQuery = @"
                INSERT INTO Logs (CustomerID, OrderID, LogDate, CustomerType, Quantity, LogType, LogDetails) 
                VALUES (@CustomerID, @OrderID, @LogDate, @CustomerType, @Quantity, @LogType, @LogDetails)";
                SqlCommand logCommand = new SqlCommand(logInsertQuery, connection);
                logCommand.Parameters.AddWithValue("@CustomerID", customerId);
                logCommand.Parameters.AddWithValue("@OrderID", orderId);
                logCommand.Parameters.AddWithValue("@LogDate", DateTime.Now);
                logCommand.Parameters.AddWithValue("@CustomerType", customerType);
                logCommand.Parameters.AddWithValue("@Quantity", quantity);
                logCommand.Parameters.AddWithValue("@LogType", logType);
                logCommand.Parameters.AddWithValue("@LogDetails", logDetails);

                await logCommand.ExecuteNonQueryAsync();
            }
        }





    }
}

