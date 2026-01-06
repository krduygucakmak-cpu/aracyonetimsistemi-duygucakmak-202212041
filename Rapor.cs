using System;
using System.Collections.Generic; // Listeler için gerekli
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using Newtonsoft.Json; // Veriyi okumak için gerekli (Hata verirse ampulden ekleyeceğiz)

namespace AracKiralamaOtomasyonu
{
    public partial class Rapor : Form
    {
        // Bağlantı Ayarları
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "b8mHUSSkoH6LyRNuHd9W7PdoRrGZur1hebB4DbpwY",
            BasePath = "https://arackiralamaotomasyonu-db24f-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public Rapor()
        {
            InitializeComponent();
            try { client = new FireSharp.FirebaseClient(config); }
            catch { }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Firebase'den "Araclar" klasörünü çekiyoruz
                FirebaseResponse response = await client.GetAsync("Araclar");

                // 2. Gelen veriyi bir Sözlük (Dictionary) yapısına çeviriyoruz
                // Çünkü Firebase verileri { "34ABC": {..}, "06XYZ": {..} } şeklinde tutar.
                var data = JsonConvert.DeserializeObject<Dictionary<string, Arac>>(response.Body.ToString());

                // 3. Veriyi tabloya (DataGridView) uygun hale getiriyoruz
                List<Arac> aracListesi = new List<Arac>();
                foreach (var item in data)
                {
                    aracListesi.Add(item.Value); // Her bir aracı listeye ekle
                }

                // 4. Tabloya basıyoruz
                dataGridView1.DataSource = aracListesi;
                MessageBox.Show("Araçlar listelendi!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Henüz hiç araç yok veya bir hata oluştu.\n" + ex.Message);
            }
        }
    }
}