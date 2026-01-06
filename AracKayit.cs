using System;
using System.Windows.Forms;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response; // Bu kütüphane şart

namespace AracKiralamaOtomasyonu
{
    public partial class AracKayit : Form
    {
        // 1. Firebase Bağlantı Ayarları
        IFirebaseConfig config = new FirebaseConfig
        {
            AuthSecret = "b8mHUSSkoH6LyRNuHd9W7PdoRrGZur1hebB4DbpwY",
            BasePath = "https://arackiralamaotomasyonu-db24f-default-rtdb.firebaseio.com/"
        };
        IFirebaseClient client;

        public AracKayit()
        {
            InitializeComponent();
            BaglantiyiKur();
        }

        private void BaglantiyiKur()
        {
            try
            {
                client = new FireSharp.FirebaseClient(config);
            }
            catch
            {
                MessageBox.Show("İnternet bağlantını kontrol et!");
            }
        }

        // 2. Kaydet Butonuna Basınca Çalışacak Kod
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // NTP Kriteri: Verileri 'Arac' sınıfında paketliyoruz
                Arac yeniArac = new Arac
                {
                    Plaka = txtPlaka.Text,
                    Marka = txtMarka.Text,
                    Model = txtModel.Text,
                    Fiyat = Convert.ToInt32(txtFiyat.Text),
                    MusaitMi = true // YENİ EKLENEN SATIR: Araç varsayılan olarak müsaittir.
                };

                // Firebase'e gönderiyoruz ("SetResponse" yerine "FirebaseResponse" kullanıyoruz)
                FirebaseResponse response = await client.SetAsync("Araclar/" + txtPlaka.Text, yeniArac);

                MessageBox.Show("Araç Başarıyla Kaydedildi!");

                // Kayıttan sonra kutuları temizleyelim
                txtPlaka.Clear();
                txtMarka.Clear();
                txtModel.Clear();
                txtFiyat.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message);
            }
        }
    }
}