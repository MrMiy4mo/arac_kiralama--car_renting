using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AracKiralamaOtomasyonu
{
    public partial class profile : System.Web.UI.Page
    {

        private string tcNo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ortak_fonksiyonlar.DisablePageCaching();
            string aktifOturum = Request.Cookies["SessionID"]?.Value;
            if (!string.IsNullOrEmpty(aktifOturum))
            {
                try
                {
                    AracKiralamaOtomasyonuEntities vt = new AracKiralamaOtomasyonuEntities();
                    userSession oturumCheck = vt.userSession.FirstOrDefault(p => p.sessionKey == aktifOturum);
                    userList kullaniciListesi = vt.userList.FirstOrDefault(p => p.userTC == oturumCheck.userTC);
                    lbProfile.Text = kullaniciListesi.userAd + " " + kullaniciListesi.userSoyad;
                    imgAvatar.ImageUrl = kullaniciListesi.userAvatar;
                    tcNo = kullaniciListesi.userTC;
                        //profil bilgilerini navigasyon cubuğunda gösterir


                    for (int i = 1; i < 32; i++)
                    {
                        dplDay.Items.Add(Convert.ToString(i));
                    }
                    for (int i = 1930; i < 1 + DateTime.Now.Year; i++)
                    {
                        dplYear.Items.Add(Convert.ToString(i));
                    }   //Doğum tarihi girişi için günler ve yılları listeye ekler

                    ibAvatar.ImageUrl = kullaniciListesi.userAvatar;
                    rbCinsiyet.SelectedValue = kullaniciListesi.userCinsiyet;
                    txtTCNO.Text = kullaniciListesi.userTC;
                    txtAd.Text = kullaniciListesi.userAd;
                    txtSoyad.Text = kullaniciListesi.userSoyad;
                    txtTel.Text = kullaniciListesi.userTel;
                    txtMail.Text = kullaniciListesi.userEPosta;
                    txtLogin.Text = kullaniciListesi.userLogin;
                    dplDay.SelectedValue = Convert.ToString(kullaniciListesi.userDtarihi.Day);
                    dplMonth.SelectedValue = Convert.ToString(kullaniciListesi.userDtarihi.Month);
                    dplYear.SelectedValue = Convert.ToString(kullaniciListesi.userDtarihi.Year);
                    txtAdres.Text = kullaniciListesi.userDyeri;
                        //kullanıcının düzenlemesi için formu doldurur
                }

                catch (Exception)
                {
                    return;
                }       //hata durumunda işlemi iptal eder
            }

            else
            {
                Response.Redirect("./login.aspx");
            }   //aktif oturum yoksa giriş sayfasına yönlendirir
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
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
                try
                {
                    AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
                    userList guncellenen = aracEntity.userList.FirstOrDefault(p => p.userTC ==kimlik);
                    guncellenen.userLogin = kullaniciAdi;
                    guncellenen.userPass = sifre;
                    guncellenen.userAd = ad;
                    guncellenen.userSoyad = soyad;
                    guncellenen.userCinsiyet = cinsiyet;
                    guncellenen.userEPosta = eposta;
                    guncellenen.userTel = tel;
                    guncellenen.userDyeri = dogumYeri;
                    guncellenen.userAktif = true;

                    if (uplAvatar.PostedFile.ContentLength != 0)
                    {
                        string path = "./assets/ortak/pictures/profiles/" + kimlik + ".jpg";
                        ortak_fonksiyonlar.resimBoyutlandir(uplAvatar.PostedFile.InputStream)
                            .Save(Server.MapPath(path));
                        guncellenen.userAvatar = path;
                    }

                    guncellenen.userDtarihi = Convert.ToDateTime(
                        dplDay.SelectedValue + "/" +
                        dplMonth.SelectedValue + "/" +
                        dplYear.SelectedValue, new CultureInfo("tr-TR"));

                    aracEntity.SaveChanges();
                }   //veritabanında kayıt güncellemeye çalışır
                catch(Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "alert('Bilinmeyen hata!');", true);
                    return;
                }   //hata oluşursa mesaj gösterir
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Kayıt başarı ile güncellendi');", true);
                    //güncelleme başarılı olursa mesaj gösterir
            }
        }


        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("./index.aspx");
        }   //sayfa logosono tıklanınca anasayfaya yönlendirir


        protected void lbProfile_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["SessionID"] != null)
            {
                HttpCookie oturumID = new HttpCookie("SessionID");
                oturumID.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(oturumID);
                ortak_fonksiyonlar.eskiOturumSonlandır(tcNo);
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Hesabınızdan çıkış yapıldı.');location.replace('./login.aspx');", true);
                return;
            }   //giriş satfasında kaydedilen çerezi siler, oturumu sonlandırır ve giriş sayfasına yönlendirir

            ClientScript.RegisterStartupScript(GetType(), "", "location.replace('./login.aspx');", true);
            //giriş sayfasına yönlendirir
        }

        protected void lblProfile_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(tcNo))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Önce giriş yapmalısınız!');", true);
            }
            else
            {
                Response.Redirect("./profile.aspx");
            }   //aktif oturum varsa, profil bilgileri düzenleme ekranına yönlendirir, yoksa mesaj gösterir
        }

        protected void lbAraclar_Click(object sender, EventArgs e)
        {
            Response.Redirect("./araclar.aspx");
        }   //kiralanabilen araç sayfasına yönlendirir

        protected void lbGecmis_Click(object sender, EventArgs e)
        {
            Response.Redirect("./gecmis.aspx");
        }   //kiralanmış araç sayfasına yönlendirir
    }
}