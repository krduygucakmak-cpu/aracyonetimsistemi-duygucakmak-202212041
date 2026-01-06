using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AracKiralamaOtomasyonu
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AracKayit kayitSayfasi = new AracKayit();
            kayitSayfasi.Show();
            this.Hide(); // Menüyü gizler, kayıt ekranını açar
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Kiralama kiralamaSayfasi = new Kiralama();
            kiralamaSayfasi.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Rapor raporSayfasi = new Rapor();
            raporSayfasi.Show();
            this.Hide();
        }
    }
}
