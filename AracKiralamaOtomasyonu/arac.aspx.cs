using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AracKiralamaOtomasyonu
{
    public partial class arac : System.Web.UI.Page
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
                    aracList aracListesi = vt.aracList.FirstOrDefault(p => p.aracPlaka == dplKayitlar.Text);
                    lbProfile.Text = kullaniciListesi.userAd + " " + kullaniciListesi.userSoyad;
                    imgAvatar.ImageUrl = kullaniciListesi.userAvatar;
                    tcNo = kullaniciListesi.userTC;

                    string secilenArac = null;
                    if (String.IsNullOrEmpty(dplKayitlar.SelectedValue))
                    {
                        dplKayitlar.DataBind();
                        secilenArac = Request.QueryString["plakaID"];
                    }
                    if (secilenArac != null)
                    {
                        dplKayitlar.SelectedValue = secilenArac;
                        formDoldur();
                    }

                }   //daha önce giriş yapılıp yapılmadığını kontrol eder, yapılmışsa profil bilgilerini göstermeye çalışır


                catch (Exception)
                {
                    return;
                }   //profil bilgileri görüntülenemediğinde işlemi geri alır
            }
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

        private void formDoldur()
        {
            AracKiralamaOtomasyonuEntities vt = new AracKiralamaOtomasyonuEntities();
            aracList aracListesi = vt.aracList.FirstOrDefault(p => p.aracPlaka == dplKayitlar.Text);
            aracMarka aracMarka = vt.aracMarka.FirstOrDefault(q => q.aracMarkaID == aracListesi.aracMarkaID);
            ibAracResim.ImageUrl = aracListesi.aracResim;
            txtAracMarka.Text = aracMarka.aracMarkaAdi;
            txtAracAdi.Text = aracListesi.aracAdi;
            txtRenk.Text = aracListesi.aracRenk;
            txtYakit.Text = aracListesi.aracYakitTipi;
            txtVites.Text = aracListesi.aracVitesTipi + " Vites";
            txtBavulAded.Text = aracListesi.aracBavulSayisi + " Bavul";
            txtKoltukAded.Text = aracListesi.aracKoltukSayisi + "Koltuk";

            if (aracListesi.aracKlimali == true)
            {
                txtKlima.Text = "Klimalı"; 
            }
            else
            {
                txtKlima.Text = "Klimasız";
            }

            
        }

        protected void dplKayitlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            formDoldur();
        }   //seçilen araçın bilgilerini forma doldurur

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("./admin.aspx");
        }

        protected void btnKirala_Click(object sender, EventArgs e)
        {
            AracKiralamaOtomasyonuEntities vt = new AracKiralamaOtomasyonuEntities();
            aracKira kiralanan = new aracKira();
            aracList aracListesi = vt.aracList.FirstOrDefault(p => p.aracPlaka == dplKayitlar.Text);

            kiralanan.aracPlaka = dplKayitlar.SelectedValue;
            kiralanan.userTC = tcNo;
            kiralanan.kiraTarih = DateTime.Now;
            kiralanan.kiraAktif = true;
            aracListesi.aracAktif = false;
            vt.aracKira.Add(kiralanan);
            vt.SaveChanges();

            btnKirala.Visible = false;
            btnTeslimEt.Visible = true;
        }
    }
}