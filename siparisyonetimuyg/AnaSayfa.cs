namespace siparisyonetimuyg
{
    public partial class Form1 : Form
    {
        private readonly int _customerID;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonyonetici_Click(object sender, EventArgs e)
        {

            YoneticiGiris yoneticiGiris = new YoneticiGiris();
            yoneticiGiris.Show();

        }

        private void buttonmusteri_Click(object sender, EventArgs e)
        {
            MusteriGiris musteriGiris = new MusteriGiris();
            musteriGiris.Show();
        }


    }
}