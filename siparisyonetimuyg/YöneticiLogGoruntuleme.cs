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
    public partial class YöneticiLogGoruntuleme : Form
    {
        private Thread logMonitorThread;
        private readonly string connectionString = "Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True;";
        private bool sutunlarAyarlandi = false;
        public YöneticiLogGoruntuleme()
        {
            InitializeComponent();
        }

        private void YöneticiLogGoruntuleme_Load(object sender, EventArgs e)
        {
            logMonitorThread = new Thread(ThreadEkrani);
            logMonitorThread.IsBackground = true;
            logMonitorThread.Start();
        }
        private void ThreadEkrani()
        {
            while (true)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT LogID, LogDetails FROM Logs ORDER BY LogDate DESC";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    if (dataGridViewLogs.InvokeRequired)
                    {
                        Invoke(new Action(() =>
                        {
                            dataGridViewLogs.DataSource = dt;

                          
                            if (!sutunlarAyarlandi)
                            {
                                dataGridViewLogs.Columns[0].HeaderText = "Log ID";
                                dataGridViewLogs.Columns[1].HeaderText = "Açıklama";
                                dataGridViewLogs.Columns[0].Width = 100;
                                dataGridViewLogs.Columns[1].Width = 900;
                                sutunlarAyarlandi = true; 
                            }
                        }));
                    }
                    else
                    {
                        dataGridViewLogs.DataSource = dt;

                      
                        if (!sutunlarAyarlandi)
                        {
                            dataGridViewLogs.Columns[0].HeaderText = "Log ID";
                            dataGridViewLogs.Columns[1].HeaderText = "Açıklama";
                            dataGridViewLogs.Columns[0].Width = 100;
                            dataGridViewLogs.Columns[1].Width = 900;
                            sutunlarAyarlandi = true; // Sütunlar bir kez ayarlandı
                        }
                    }
                }

                Thread.Sleep(2000); 
            }
        }
    }
}