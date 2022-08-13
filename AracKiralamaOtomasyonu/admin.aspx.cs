using System;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace AracKiralamaOtomasyonu
{
    public partial class admin : System.Web.UI.Page
    {
        private string tcNo = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            ortak_fonksiyonlar.DisablePageCaching();
            string aktifOturum = Request.Cookies["SessionID"]?.Value;

            for (int i = 1; i < 32; i++)
            {
                dplDay.Items.Add(Convert.ToString(i));
            }
            for (int i = 1930; i < 1 + DateTime.Now.Year; i++)
            {
                dplYear.Items.Add(Convert.ToString(i));
            }   //Doğum tarihi girişi için günler ve yılları listeye ekler

            if (!string.IsNullOrEmpty(aktifOturum))
                try
                {
                    AracKiralamaOtomasyonuEntities vt = new AracKiralamaOtomasyonuEntities();
                    userSession oturumCheck = vt.userSession.FirstOrDefault(
                    p => p.sessionKey == aktifOturum);

                    userList kullaniciListesi = vt.userList.FirstOrDefault(
                        p => p.userTC == oturumCheck.userTC);
                    if(kullaniciListesi.rolID == 1)
                    {
                        lbProfile.Text = kullaniciListesi.userAd + " " + kullaniciListesi.userSoyad;
                        imgAvatar.ImageUrl = kullaniciListesi.userAvatar;
                        tcNo = kullaniciListesi.userTC;
                        return;
                    }
                    else
                    {
                        Response.Redirect("./login.aspx");
                    }
                }
                catch (Exception)
                {
                    return;
                }
            else
            {
                Response.Redirect("./login.aspx");
            }

        }   //daha önce giriş yapılıp yapılmadığını ve giriş yapanın admin olup olmadığını kontrol eder, şartlar sağlanmıyorsa giriş sayfasına yönlendirir.

        protected void imgLogo_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("./admin.aspx");
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
            }
            ClientScript.RegisterStartupScript(GetType(), "", "location.replace('./login.aspx');", true);
        }

        protected void lbKullaniciYönetimi_Click(object sender, EventArgs e)
        {
            div1.Visible = false;
            div2.Visible = true;
            div3.Visible = false;

        }

        private void aracYonetimi()
        {
            div1.Visible = false;
            div2.Visible = false;
            div3.Visible = true;
        }

        protected void lbAracYonetimi_Click(object sender, EventArgs e)
        {
            aracYonetimi();
        }

        protected void dplKayitYontemi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(Convert.ToInt32(dplKayitYontemi.SelectedValue) == 0)
            {
                txtPlaka.Visible = true;
                dplKayitlar.Visible = false;
                txtAracMarka.Visible = false;
                dplAracMarka.Visible = true;
                btnDelete.Visible = false;
                aracYonetimi();
            }
            else if (Convert.ToInt32(dplKayitYontemi.SelectedValue) == 1)
            {
                txtPlaka.Visible = true;
                dplKayitlar.Visible = false;
                txtAracMarka.Visible = true;
                dplAracMarka.Visible = false;
                btnDelete.Visible = false;
                aracYonetimi();
            }
            else if (Convert.ToInt32(dplKayitYontemi.SelectedValue) == 2)
            {
                txtPlaka.Visible = false;
                dplKayitlar.Visible = true;
                txtAracMarka.Visible = false;
                dplAracMarka.Visible = true;
                btnDelete.Visible = true;
                aracYonetimi();
            }
        }

        protected void btnAddCar_Click(object sender, EventArgs e)
        {
            if
            (
                uplAracResim.PostedFile.ContentLength == 0 ||
                string.IsNullOrEmpty(txtAracAdi.Text) == true ||
                string.IsNullOrEmpty(txtRenk.Text) == true ||
                string.IsNullOrEmpty(txtBavulAded.Text) == true ||
                string.IsNullOrEmpty(txtKoltukAded.Text) == true
            )
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen alanları boş bırakmayınız!');", true);
            }

            else
            {
                try
                {
                    int kayitYontemi = Convert.ToInt32(dplKayitYontemi.SelectedValue);
                    string plaka = null;
                    AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
                    aracList kaydedilen = null;

                    if (kayitYontemi != 2)
                    {
                        if (string.IsNullOrEmpty(txtPlaka.Text) == true)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen Plaka giriniz!');", true);
                            return;
                        }
                        plaka = txtPlaka.Text;
                        kaydedilen = new aracList();
                        kaydedilen.aracPlaka = plaka;

                        if (uplAracResim.PostedFile.ContentLength == 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen araç fotoğrafı ekleyin!');", true);
                            return;
                        }   //kayit yöntemi güncelleme değilse fotoğraf seçilmesini hatırlatır
                        else
                        {
                            string path = "./assets/ortak/pictures/cars/" + plaka + ".jpg";
                            ortak_fonksiyonlar.resimBoyutlandir(uplAracResim.PostedFile.InputStream).Save(Server.MapPath(path));
                            kaydedilen.aracResim = path;
                        }

                    }

                    else if (kayitYontemi == 2)
                    {
                        plaka = dplKayitlar.SelectedItem.Text;
                        kaydedilen = aracEntity.aracList.FirstOrDefault(p => p.aracPlaka == plaka);
                        kaydedilen.aracPlaka = plaka;

                        if (uplAracResim.PostedFile.ContentLength != 0)
                        {
                            string path = "./assets/ortak/pictures/cars/" + plaka + ".jpg";
                            ortak_fonksiyonlar.resimBoyutlandir(uplAracResim.PostedFile.InputStream).Save(Server.MapPath(path));
                            kaydedilen.aracResim = path;
                        }   //fotoğraf seçilmiş ise veritabanına ve yerel depolamaya kaydet
                    }

                    if (kayitYontemi == 0)
                    {
                        kaydedilen.aracMarkaID = Convert.ToInt32(dplAracMarka.SelectedIndex) + 1;
                    }   //markası önceden kaydedilmiş araç

                    if (kayitYontemi == 1)
                    {
                        aracMarka eklenenMarka = new aracMarka();
                        if (!string.IsNullOrEmpty(txtAracMarka.Text))
                        {
                            eklenenMarka.aracMarkaAdi = txtAracMarka.Text;
                            aracEntity.aracMarka.Add(eklenenMarka);
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen arac markasını belirtiniz.');", true);
                            return;
                        }
                    }   //markası önceden kaydedilmemiş araç, veri tabanına yeni marka ekler

                    kaydedilen.aracAdi = txtAracAdi.Text;
                    kaydedilen.aracRenk = txtRenk.Text;
                    kaydedilen.aracYakitTipi = dplYakitTipi.SelectedItem.Text;
                    kaydedilen.aracVitesTipi = dplVitesTipi.SelectedValue;
                    kaydedilen.aracBavulSayisi = txtBavulAded.Text;
                    kaydedilen.aracKoltukSayisi = txtKoltukAded.Text;
                    kaydedilen.aracKlimali = Convert.ToBoolean(dplKlimaDurumu.SelectedIndex);
                    kaydedilen.aracAktif = true;


                    if (kayitYontemi != 2)
                    {
                        aracEntity.aracList.Add(kaydedilen);
                    }

                    aracEntity.SaveChanges();
                }


                catch (Exception Ex)
                {
                    if (Ex.InnerException.InnerException.Message.Contains("Violation of PRIMARY KEY"))
                    {
                        ClientScript.RegisterStartupScript(GetType(), "", "alert('Bu Plaka numarası ile daha önce kayıt oldunuz!');", true);
                        return;
                    }
                }
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Kayıt başarılı.');", true);
            }

        } //araç kaydetme komutları sonu


        private void formDoldur()
        {
            AracKiralamaOtomasyonuEntities vt = new AracKiralamaOtomasyonuEntities();
            aracList aracListesi = vt.aracList.FirstOrDefault(p => p.aracPlaka == dplKayitlar.Text);
            ibAracResim.ImageUrl = aracListesi.aracResim;
            dplAracMarka.SelectedIndex = aracListesi.aracMarkaID - 1;
            txtAracAdi.Text = aracListesi.aracAdi;
            txtRenk.Text = aracListesi.aracRenk;
            dplYakitTipi.SelectedItem.Text = aracListesi.aracYakitTipi;
            dplVitesTipi.SelectedValue = aracListesi.aracVitesTipi;
            txtBavulAded.Text = aracListesi.aracBavulSayisi;
            txtKoltukAded.Text = aracListesi.aracKoltukSayisi;
            dplKlimaDurumu.SelectedValue = Convert.ToString(aracListesi.aracKlimali);
        }
        protected void dplKayitlar_SelectedIndexChanged(object sender, EventArgs e)
        {
            formDoldur();
        }   //seçilen araçın bilgilerini forma doldurur

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
            aracList silinen = aracEntity.aracList.FirstOrDefault(p => p.aracPlaka == dplKayitlar.SelectedItem.Text);
            aracEntity.aracList.Remove(silinen);
            aracEntity.SaveChanges();

        }

        protected void dplUserList_SelectedIndexChanged(object sender, EventArgs e)
        {
            AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
            userList secilen = aracEntity.userList.FirstOrDefault(p => p.userTC == dplUserList.SelectedItem.Text);
            ibAvatar.ImageUrl = secilen.userAvatar;
            rbCinsiyet.SelectedValue = secilen.userCinsiyet;
            txtAd.Text = secilen.userAd;
            txtSoyad.Text = secilen.userSoyad;
            txtTel.Text = secilen.userTel;
            txtMail.Text = secilen.userEPosta;
            txtLogin.Text = secilen.userLogin;
            dplDay.SelectedValue = Convert.ToString(secilen.userDtarihi.Day);
            dplMonth.SelectedValue = Convert.ToString(secilen.userDtarihi.Month);
            dplYear.SelectedValue = Convert.ToString(secilen.userDtarihi.Year);
            txtAdres.Text = secilen.userDyeri;
            if(secilen.userAktif == true)
            {
                btnDisable.Visible = true;
                btnEnable.Visible = false;
            }
            else if (secilen.userAktif == false)
            {
                btnDisable.Visible = false;
                btnEnable.Visible = true;
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string kimlik = dplUserList.SelectedItem.Text;
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
                string.IsNullOrEmpty(dogumYeri) == true)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Lütfen alanları boş bırakmayınız!');", true);
                /*Boş alan varsa mesaj gösterir*/
            }

            else if (kimlik.Length != 11)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Kimlik numaranızı hatalı girdiniz!');", true);
                /*girilen kimlik numarası 11 haneli değilse mesaj gösterir*/
            }

            else if (tel.Length != 11)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Telefon numaranızı hatalı girdiniz');", true);
                /*girilen telefon numarası 11 haneli değilse mesaj gösterir*/
            }

            else if (rawsifre != sifreKontrol)
            {
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Girilen şifreler eşleşmiyor!');", true);
                /*girilen şifreler eşleşmiyorsa mesaj gösterir*/
            }

            else
            {
                try
                {
                    AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
                    userList guncellenen = aracEntity.userList.FirstOrDefault(p => p.userTC == kimlik);
                    guncellenen.userLogin = kullaniciAdi;
                    guncellenen.userPass = sifre;
                    guncellenen.userAd = ad;
                    guncellenen.userSoyad = soyad;
                    guncellenen.userCinsiyet = cinsiyet;
                    guncellenen.userEPosta = eposta;
                    guncellenen.userTel = tel;
                    guncellenen.userDyeri = dogumYeri;
                    guncellenen.userAktif = true;
                    guncellenen.userTC = kimlik;

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
                catch (Exception)
                {
                    ClientScript.RegisterStartupScript(GetType(), "", "alert('Bilinmeyen hata!');", true);
                    return;
                }   //hata oluşursa mesaj gösterir
                ClientScript.RegisterStartupScript(GetType(), "", "alert('Kayıt başarı ile güncellendi');", true);
                //güncelleme başarılı olursa mesaj gösterir
            }
        }

        protected void btnDisable_Click(object sender, EventArgs e)
        {
            string kimlik = dplUserList.SelectedItem.Text;
            AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
            userList guncellenen = aracEntity.userList.FirstOrDefault(p => p.userTC == kimlik);
            guncellenen.userAktif = false;
            aracEntity.SaveChanges();

            btnEnable.Visible = true;
            btnDisable.Visible = false;
        }

        protected void btnEnable_Click(object sender, EventArgs e)
        {
            string kimlik = dplUserList.SelectedItem.Text;
            AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
            userList guncellenen = aracEntity.userList.FirstOrDefault(p => p.userTC == kimlik);
            guncellenen.userAktif = true;
            aracEntity.SaveChanges();

            btnEnable.Visible = false;
            btnDisable.Visible = true;
        }

        protected void btnDeleteUser_Click(object sender, EventArgs e)
        {
            string kimlik = dplUserList.SelectedItem.Text;
            AracKiralamaOtomasyonuEntities aracEntity = new AracKiralamaOtomasyonuEntities();
            userList silinen = aracEntity.userList.FirstOrDefault(p => p.userTC == kimlik);
            aracEntity.userList.Remove(silinen);
            aracEntity.SaveChanges();

            btnEnable.Visible = false;
            btnDisable.Visible = false;
        }
    }
}