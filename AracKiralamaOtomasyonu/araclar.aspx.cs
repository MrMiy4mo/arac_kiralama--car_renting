using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AracKiralamaOtomasyonu
{
    public partial class araclar : System.Web.UI.Page
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
                    userSession oturumCheck = vt.userSession.FirstOrDefault(
                        p => p.sessionKey == aktifOturum);

                    userList kullaniciListesi = vt.userList.FirstOrDefault(
                        p => p.userTC == oturumCheck.userTC);
                    lbProfile.Text = kullaniciListesi.userAd + " " + kullaniciListesi.userSoyad;
                    imgAvatar.ImageUrl = kullaniciListesi.userAvatar;
                    tcNo = kullaniciListesi.userTC;
                }   //daha önce giriş yapılıp yapılmadığını kontrol eder, yapılmışsa profil bilgilerini göstermeye çalışır


                catch (Exception)
                {
                    return;
                }   //profil bilgileri görüntülenemediğinde işlemi geri alır
            }

            int f = ortak_fonksiyonlar.aracSayisi() + 1;
            ortak_fonksiyonlar.DisablePageCaching();
            for (int i = 1; i < f; i++)
            {
                Image resim = new Image();
                resim.Attributes["onclick"] = "location.replace('./arac.aspx?plakaID=" + ortak_fonksiyonlar.GetAracPlaka(i) + "');";
                resim.CssClass = "form-field d-flex align-items-center fixed-height thumbnail";
                resim.ID = "car" + i;
                resim.ImageUrl = ortak_fonksiyonlar.aracListele(i);
                slider1.Controls.Add(resim);
            }   //sayfa araç resimleri ile doldurur
        }

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

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("./index.aspx");
        }   //sayfa logosono tıklanınca anasayfaya yönlendirir
    }
}