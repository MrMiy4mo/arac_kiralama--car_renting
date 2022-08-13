using System;
using System.Linq;
using System.Web;

namespace AracKiralamaOtomasyonu
{
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            ortak_fonksiyonlar.DisablePageCaching();
            string aktifOturum = Request.Cookies["SessionID"]?.Value;
            if (!string.IsNullOrEmpty(aktifOturum))
            {
                Response.Redirect("./index.aspx");
            }   //daha önce giriş yapılıp yapılmadığını kontrol eder, yapılmışsa anasayfaya yönlendirir
        }

        protected void lbRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("./register.aspx");
                //Kayıt olma sayfasına yönlendirir
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {

            string girisAd = txtTCNO.Text;
            string rawSifre = txtPass.Text;
            string girisSifre = ortak_fonksiyonlar.sha256Hesapla(rawSifre);

            if (string.IsNullOrEmpty(girisAd) || string.IsNullOrEmpty(rawSifre))
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen alanları boş bırakmayınız!');", true);
                return;
            }   //giriş yaparken bilgiler girilmediyse mesaj gösterir


            AracKiralamaOtomasyonuEntities vt = new AracKiralamaOtomasyonuEntities();
            userList girisCheck = vt.userList.FirstOrDefault( p => p.userTC == girisAd && p.userPass == girisSifre && p.userAktif == true);
                //Veritabanından kayıt sorgular


            if (girisCheck == null)
            {
                 ClientScript.RegisterStartupScript(GetType(), "", "alert('Kayıt bulunamadı, Kullanıcı adı ya da Şifreniz hatalı olabilir.');", true);
            }   //eşleşen kayıt yoksa mesaj gösterir


            else
            {
                string randomSessionID = ortak_fonksiyonlar.RandomString();
                string userID = girisCheck.userTC;

                ortak_fonksiyonlar.eskiOturumSonlandır(girisCheck.userTC);
                    //üyenin önceki oturumlarını siler


                AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
                userSession girisYapan = new userSession();
                girisYapan.sessionKey = randomSessionID;
                girisYapan.userTC = girisCheck.userTC;
                girisYapan.sessionTarih = DateTime.Now;
                girisYapan.sessionAktif = true;
                aracEntity.userSession.Add(girisYapan);
                aracEntity.SaveChanges(); 
                    //üyenin oturumunu kaydeder


                HttpCookie oturumID = new HttpCookie("SessionID"); 
                oturumID.Expires = DateTime.Now.AddHours(1);
                oturumID.Value = randomSessionID;
                Response.Cookies.Add(oturumID);
                    //kaydedilen oturumun daha sonra kullanılması için tarayıcıya çerez kaydeder


                if (girisCheck.rolID == 2)
                {
                    Response.Redirect("./index.aspx");
                }
                else if (girisCheck.rolID == 1)
                {
                    Response.Redirect("./admin.aspx");
                }   //Giriş yapan kullanıcının rolöne göre yönlendirme yapar

            }
        }
    }
}