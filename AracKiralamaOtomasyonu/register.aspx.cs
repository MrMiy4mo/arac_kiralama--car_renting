using System;
using System.Globalization;

namespace AracKiralamaOtomasyonu
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ortak_fonksiyonlar.DisablePageCaching();

            for (int i = 1; i < 32; i++)
            {
                dplDay.Items.Add(Convert.ToString(i));
            }
            for (int i = 1930; i < 1 + DateTime.Now.Year; i++)
            {
                dplYear.Items.Add(Convert.ToString(i));
            }   //Doğum tarihi girişi için günler ve yılları listeye ekler

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string kimlik = txtTCNO.Text;
            string ad = txtAd.Text;
            string soyad = txtSoyad.Text;
            string tel = txtTel.Text;
            string eposta = txtMail.Text;
            string kullaniciAdi = txtLogin.Text;
            string rawsifre = txtPass.Text;
            string sifre = ortak_fonksiyonlar.sha256Hesapla(txtPass.Text);
            string sifreKontrol = txtPassCheck.Text;
            string dogumYeri = txtAdres.Text;
            string cinsiyet = rbCinsiyet.SelectedValue;
            if (
                uplAvatar.PostedFile.ContentLength == 0 ||
                string.IsNullOrEmpty(kimlik) == true ||
                string.IsNullOrEmpty(cinsiyet) == true ||
                string.IsNullOrEmpty(ad) == true ||
                string.IsNullOrEmpty(soyad) == true ||
                string.IsNullOrEmpty(tel) == true ||
                string.IsNullOrEmpty(eposta) == true ||
                string.IsNullOrEmpty(kullaniciAdi) == true ||
                string.IsNullOrEmpty(rawsifre) == true ||
                string.IsNullOrEmpty(sifreKontrol) == true ||
                string.IsNullOrEmpty(dogumYeri) == true )
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen alanları boş bırakmayınız!');", true);
                    /*Boş alan varsa mesaj gösterir*/
            }

            else if (kimlik.Length != 11) {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Kimlik numaranızı hatalı girdiniz!');", true);
                    /*girilen kimlik numarası 11 haneli değilse mesaj gösterir*/ }

            else if (tel.Length != 11) {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Telefon numaranızı hatalı girdiniz');", true);
                    /*girilen telefon numarası 11 haneli değilse mesaj gösterir*/ }

            else if (rawsifre != sifreKontrol) {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Girilen şifreler eşleşmiyor!');", true);
                    /*girilen şifreler eşleşmiyorsa mesaj gösterir*/ }

            else
            {
                try {
                    AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
                    userList eklenen = new userList();
                    eklenen.userTC = kimlik;
                    eklenen.userLogin = kullaniciAdi;
                    eklenen.userPass = sifre;
                    eklenen.userAd = ad;
                    eklenen.userSoyad = soyad;
                    eklenen.userCinsiyet = cinsiyet;
                    eklenen.userEPosta = eposta;
                    eklenen.userTel = tel;
                    eklenen.userDyeri = dogumYeri;
                    eklenen.rolID = 2;
                    eklenen.userAktif = true;

                    string path = "./assets/ortak/pictures/profiles/" + kimlik + ".jpg";
                    ortak_fonksiyonlar.resimBoyutlandir(uplAvatar.PostedFile.InputStream).Save(Server.MapPath(path));
                    eklenen.userAvatar = path;

                    eklenen.userDtarihi = Convert.ToDateTime(
                        dplDay.SelectedValue + "/" +
                        dplMonth.SelectedValue + "/" +
                        dplYear.SelectedValue, new CultureInfo("tr-TR"));

                    aracEntity.userList.Add(eklenen);
                    aracEntity.SaveChanges();
                }   //veritabanına kayıt eklemeye çalışır


                catch (Exception Ex)
                {
                    if (Ex.InnerException.InnerException.Message.Contains("Violation of PRIMARY KEY"))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "alert('Bu kimlik numarası ile daha önce kayıt oldunuz!');", true);
                        return;
                    }
                }   //tanımlı hata meydana gelirse mesaj gösterir


                ClientScript.RegisterStartupScript(GetType(), "", "alert('Kayıt başarılı, şimdi giriş sayfasına yönlendirileceksiniz.');location.replace('./login.aspx');", true);
                    //kayıt başarılı olduğunda mesaj gösterir ve giriş sayfasına yönlendirir
            }

        }
    }
}