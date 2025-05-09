using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace siparisyonetimuyg
{
    public partial class YoneticiSayfasi : Form
    {
        private int adminID;

        public YoneticiSayfasi()
        {
            InitializeComponent();
        }

        public YoneticiSayfasi(int adminID)
        {
            InitializeComponent();
            this.adminID = adminID;
        }



        private void btnurunekranı_Click(object sender, EventArgs e)
        {
            YoneticiUrunGuncelleme yoneticiUrunGuncelleme = new YoneticiUrunGuncelleme();
            yoneticiUrunGuncelleme.Show();

        }

        private void btnurunekle_Click(object sender, EventArgs e)
        {
            YoneticiUrunEkleme yoneticiUrunEkleme = new YoneticiUrunEkleme();
            yoneticiUrunEkleme.Show();
        }

        private void btnsiparisekranı_Click(object sender, EventArgs e)
        {
            YoneticiSiparisOnaylama yoneticisiparisOnaylama = new YoneticiSiparisOnaylama();
            yoneticisiparisOnaylama.Show();
        }

        private void btnYntciLog_Click(object sender, EventArgs e)
        {
            YöneticiLogGoruntuleme yoneticiLogGoruntuleme = new YöneticiLogGoruntuleme();
            yoneticiLogGoruntuleme.Show();
        }

        private void btnUrunStokGoruntuleme_Click(object sender, EventArgs e)
        {
            YoneticiStokGoruntuleme yoneticiStokGoruntuleme = new YoneticiStokGoruntuleme();
            yoneticiStokGoruntuleme.Show();
        }
    }
}
