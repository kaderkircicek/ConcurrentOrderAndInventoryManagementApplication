using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace siparisyonetimuyg
{
    internal class VeriTabaniBaglantisi
    {

        public static SqlConnection baglanti = new SqlConnection("Data Source=LAPTOP-KH2PNG3N\\SQLEXPRESS02;Initial Catalog=SiparisUygulamasi;Integrated Security=True; MultipleActiveResultSets=True");

        public static void BaglantiKontrolu()
        {

            if (baglanti.State == System.Data.ConnectionState.Closed)
            {
                baglanti.Open();
            }

            else
            {

            }


        }

    }
}
