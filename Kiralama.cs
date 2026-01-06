using System;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;

namespace AracKiralamaOtomasyonu
{
    public partial class Kiralama : Form
    {
        // Bağlantı Ayarları
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "b8mHUSSkoH6LyRNuHd9W7PdoRrGZur1hebB4DbpwY",
            BasePath = "https://arackiralamaotomasyonu-db24f-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public Kiralama()
        {
            InitializeComponent();
            try { client = new FireSharp.FirebaseClient(config); }
            catch { }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. Plakaya göre aracı bul
                FirebaseResponse response = await client.GetAsync("Araclar/" + txtPlaka.Text);
                Arac gelenArac = response.ResultAs<Arac>();

                if (gelenArac != null)
                {
                    // 2. KONTROL: Araç zaten kirada mı?
                    if (gelenArac.MusaitMi == false)
                    {
                        MessageBox.Show("Bu araç şu an KİRADA! Lütfen başka araç seçiniz.");
                        return; // İşlemi burada durdur
                    }

                    // 3. Fiyat Hesapla
                    int gunSayisi = Convert.ToInt32(txtGun.Text);
                    int toplamTutar = gelenArac.Fiyat * gunSayisi;

                    // 4. Durumu Güncelle (Artık Müsait Değil) ve Kaydet
                    gelenArac.MusaitMi = false;
                    await client.SetAsync("Araclar/" + txtPlaka.Text, gelenArac);

                    MessageBox.Show("Araç Kiralandı!\nToplam Tutar: " + toplamTutar + " TL");
                }
                else
                {
                    MessageBox.Show("Bu plakaya ait araç bulunamadı!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
        }
    }
}