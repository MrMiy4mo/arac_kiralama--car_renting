using System;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace AracKiralamaOtomasyonu
{
    public class ortak_fonksiyonlar
    {
        //string olarak girilen parametrenin sha256 karşılığını döndürür
        public static String sha256Hesapla(String value)
        {
            StringBuilder sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }

        //Seçilen Resmi Yeniden Boyutlandır
        public static System.Drawing.Image resimBoyutlandir(Stream stream)
        {
            Bitmap resim = new Bitmap(stream);
            Bitmap yeniResim = new Bitmap(256, 256);
            Graphics graphic = Graphics.FromImage(yeniResim);
            graphic.DrawImage(resim, 0, 0, 256, 256);
            return yeniResim;
        }

        //Rastgele string veri oluşturur, Oturum ID için kullanılacak.
        private static Random random = new Random();
        public static string RandomString()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, 64)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        //Bir kullanıcı giriş yaptığında kullanıcının önceki oturumlarını sonlandır
        private static string baglantiParametre = 
            @"server=DESKTOP-DM5D20S\SQLEXPRESS;database=AracKiralamaOtomasyonu;integrated security=true;";
        public static void eskiOturumSonlandır(string userTC)
        {
            string sorgu =  @"delete from userSession WHERE userTC = @userTC";
            SqlConnection baglanti = new SqlConnection(baglantiParametre);
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            string TCNO  = userTC;
            komut.Parameters.AddWithValue("@userTC", TCNO);
            baglanti.Open();
            int i = komut.ExecuteNonQuery();
            baglanti.Close();
        }

        //Bir kullanıcı giriş yaptığında anasayfada araçları listeler
        public static string aracListele(int i)
        {
            string sorgu = @"select * from vw_aracList where rowNo=@i";
            SqlConnection baglanti = new SqlConnection(baglantiParametre);
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@i", i);
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();
            string gorselUrl = "~/assets/ortak/pictures/car_logo_blank.png";
            while (okuyucu.Read())
            {
                gorselUrl = okuyucu[2].ToString();
            }
            baglanti.Close();

            return gorselUrl;
        }
        
        public static string GetAracPlaka(int i)
        {
            string sorgu = @"select * from vw_aracList where rowNo=@i";
            SqlConnection baglanti = new SqlConnection(baglantiParametre);
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            komut.Parameters.AddWithValue("@i", i);
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();
            string plakaID = null;
            while (okuyucu.Read())
            {
                plakaID = okuyucu[1].ToString();
            }
            baglanti.Close();

            return plakaID;
        }

        //kayıtlı araç sayısını döndürür
        public static int aracSayisi()
        {
            string sorgu = @"select count(*) from vw_aracList";
            SqlConnection baglanti = new SqlConnection(baglantiParametre);
            SqlCommand komut = new SqlCommand(sorgu, baglanti);
            baglanti.Open();
            SqlDataReader okuyucu = komut.ExecuteReader();
            int f = 0;
            while (okuyucu.Read())
            {
                f = Convert.ToInt32(okuyucu[0]);
            }
            baglanti.Close();

            return f;
        }

        //Tarayıcının önbellek oluşturmasını durdurur, değiştirilen görsellerin tarayıcıda görüntülenmemesi sebebi ile geciçi çözüm olarak kullanılacak
        public static void DisablePageCaching()
        {
            HttpContext.Current.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
            HttpContext.Current.Response.Cache.SetValidUntilExpires(false);
            HttpContext.Current.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.Cache.SetNoStore();
        }
    }
}