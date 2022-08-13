<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="admin.aspx.cs" Inherits="AracKiralamaOtomasyonu.admin" %>

<!DOCTYPE html>

<html lang="tr" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<meta name="viewport" content="width=device-width" charset="utf-8"/>
		<title>Admin Paneli</title>
		<link rel="stylesheet" href="./assets/anasayfa/reset.min.css"/>
		<link rel="stylesheet" href="./assets/anasayfa/style.css"/>
		<link rel="stylesheet" href="./assets/anasayfa/header-7.css"/>
		<link rel="icon" type="image/x-icon" href="./assets/ortak/anasayfa.ico"/>
		<link href='./assets/ortak/bootstrap.min.css' rel='stylesheet'/>
		<link href='./assets/ortak/all.css' rel='stylesheet'/>
		<script type='text/javascript' src='./assets/ortak/jquery.min.js'></script>
		<style>
			::-webkit-scrollbar {
			width: 8px;
			}
			/* Track */
			
			::-webkit-scrollbar-track {
				background: #f1f1f1; 
			}

			/* Handle */
			::-webkit-scrollbar-thumb {
				background: #888; 
			}

			/* Handle on hover */
			::-webkit-scrollbar-thumb:hover {
				background: #555; 
			}

			/* Reseting */
			* {
				margin: 0;
				padding: 0;
				box-sizing: border-box;
				font-family: 'Poppins', sans-serif;
			}

			body {
				background: #ecf0f3;
			}

			.wrapper {
				max-width: 350px;
				min-height: 500px;
				margin: 30px auto;
				padding: 40px 30px 30px 30px;
				background-color: #ecf0f3;
				border-radius: 15px;
				box-shadow: 13px 13px 20px #cbced1, -13px -13px 20px #fff;
			}

			.logo {
				width: 150px;
				margin: auto;
			}

			.logo img {
				width: 100%;
				height: 150px;
				object-fit: cover;
				border-radius: 50%;
				box-shadow: 0px 0px 3px #5f5f5f,
					0px 0px 0px 5px #ecf0f3,
					8px 8px 15px #a7aaa7,
					-8px -8px 15px #fff;
			}

			.wrapper .name {
				font-weight: 600;
				font-size: 1.4rem;
				letter-spacing: 1.3px;
				padding-left: 10px;
				color: #555;
			}

			.wrapper .form-field input {
				width: 100%;
				display: block;
				border: none;
				outline: none;
				background: none;
				font-size: 1.2rem;
				color: #666;
				padding: 10px 15px 10px 10px;
				/* border: 1px solid red; */
			}

			.wrapper .form-field select{
				width: 100%;
				display: block;
				border: none;
				outline: none;
				background: none;
				font-size: 1.2rem;
				color: #666;
				padding: 10px 15px 10px 10px;
				/* border: 1px solid red; */
			}

			.wrapper .form-field {
				padding-left: 0px;
				margin-bottom: 20px;
				border-radius: 20px;
				box-shadow: inset 8px 8px 8px #cbced1, inset -8px -8px 8px #fff;
			}

			.wrapper .form-field .fas {
				color: #555;
			}

			.wrapper .btn {
				box-shadow: none;
				width: 100%;
				height: 40px;
				background-color: #03A9F4;
				color: #fff;
				border-radius: 25px;
				box-shadow: 3px 3px 3px #b1b1b1, -3px -3px 3px #fff;
				letter-spacing: 1.3px;
			}

			.wrapper .btn:hover {
				background-color: #039BE5;
			}

			.wrapper a {
				text-decoration: none;
				font-size: 0.8rem;
				color: black;
			}

			.wrapper a:hover {
				color: #039BE5;
			}

			.fixed-height {
				height: 50px;
			}

			@media(max-width: 380px) {
				.wrapper {
					margin: 30px 20px;
					padding: 40px 15px 15px 15px;
				}
			}

			.header_link {
				font-size:20px;
				text-decoration:none;
				color:black;
				font-family:sans-serif;
			}

			.content_container {
				max-width: 90%;
				min-height: auto;
				padding: 0px;
			}
			.thumbnail {
				width:min-content;
				float:left;
				margin: 10px;
				width:136px;
				height:136px;
				padding-left:0px;
			}
		</style>
	</head>
<body>
    <form id="form1" runat="server">
        <header class="site-header" style="height:64px;box-shadow: 13px 13px 20px #cbced1, -13px -13px 20px #fff;border-radius: 50px;">
			<div class="wrapper_header site-header__wrapper" style="height:inherit;">
				<div>
					<asp:ImageButton ID="imgLogo" runat="server" ImageUrl="./assets/ortak/pictures/car_logo_small.png" style="height:36px;" OnClick="imgLogo_Click"/>
				</div>
				<div class="site-header__middle">
					<nav class="nav">
						<button class="nav__toggle" aria-expanded="false" type="button">menu</button>
						<ul class="nav__wrapper">
							<li class="nav__item">
								<asp:LinkButton ID="lbKullaniciYönetimi" runat="server" class="header_link" OnClick="lbKullaniciYönetimi_Click">Kullanıcı Yönetimi</asp:LinkButton> 
							</li>
							<li class="nav__item">
								<asp:LinkButton ID="lbAracYonetimi" runat="server" class="header_link" OnClick="lbAracYonetimi_Click">Araç Yönetimi</asp:LinkButton> 
							</li>
						</ul>
					</nav>
				</div>
				<div class="site-header__end">
					<table>
						<tr>
						<td style="padding-right: 10px;">
							<asp:Image ID="imgAvatar" runat="server" ImageUrl="~/assets/ortak/pictures/avatar.png" style="height:36px;border-radius:100%;"/>
						</td><td>
							<asp:LinkButton ID="lbProfile" runat="server" Text="Giriş Yap" class="header_link" OnClick="lbProfile_Click"></asp:LinkButton>
						</td></tr>
					</table>
				</div>
			</div>
		</header>

		<div class="wrapper content_container" id="div1" runat="server">
			<div class="name">
				Yukarıdan bir işlem seçiniz
			</div>
		</div>
		<div id="div2" Visible="false" runat="server">
			<div class="wrapper" style="min-width:700px;">
				<table>
					<tr><td>
						<div class="p-3 mt-3">
							<div style="width: 50%;float:left;">
								<asp:FileUpload ID="uplAvatar" runat="server" style="width:120px;height:120px;opacity:0;position:absolute;"/>
								<asp:ImageButton class="form-field d-flex align-items-center fixed-height" ID="ibAvatar" runat="server" style="width:120px;height:120px;padding-left:0px;"  ImageUrl="~/assets/ortak/pictures/avatar.png"/>
							</div>
							<div style="margin-left:50%;height:140px;">
								<p style="font-size:24px;line-height:26px;">Profil Fotoğrafı</p>
								<asp:RadioButtonList ID="rbCinsiyet" runat="server">
                                    <asp:ListItem Value="E">Erkek</asp:ListItem>
                                    <asp:ListItem Value="K">Kadın</asp:ListItem>
                                </asp:RadioButtonList>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:DropDownList ID="dplUserList" runat="server" DataSourceID="SqlDataSource3" DataTextField="userTC" DataValueField="userTC" OnSelectedIndexChanged="dplUserList_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
							    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="Data Source=DESKTOP-DM5D20S\SQLEXPRESS;Initial Catalog=AracKiralamaOtomasyonu;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [userTC] FROM [userList]"></asp:SqlDataSource>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtAd" runat="server" placeholder="Ad" MaxLength="50"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtSoyad" runat="server" placeholder="Soyad" MaxLength="50"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtTel" runat="server" placeholder="Telefon" MaxLength="11" TextMode="Phone"></asp:TextBox>
							</div>
						</div>
					</td>
					<td>
						<div class="p-3 mt-3">
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtMail" runat="server" placeholder="E-posta" MaxLength="50" TextMode="Email"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtLogin" runat="server" placeholder="Kullanıcı Adı" MaxLength="50"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtPass" runat="server" placeholder="Şifre" TextMode="Password" MaxLength="50"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtPassCheck" runat="server" placeholder="Şifre Onayı" TextMode="Password" MaxLength="50"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height" style="padding-left:0px;">
								<asp:DropDownList ID="dplDay" runat="server" style="width:24%;font-size:18px;"></asp:DropDownList>
								<asp:DropDownList ID="dplMonth" runat="server" style="width:42%;font-size:18px;">
                                    <asp:ListItem Value="1">Ocak</asp:ListItem>
                                    <asp:ListItem Value="2">Şubat</asp:ListItem>
                                    <asp:ListItem Value="3">Mart</asp:ListItem>
                                    <asp:ListItem Value="4">Nisan</asp:ListItem>
                                    <asp:ListItem Value="5">Mayıs</asp:ListItem>
                                    <asp:ListItem Value="6">Haziran</asp:ListItem>
                                    <asp:ListItem Value="7">Temmuz</asp:ListItem>
                                    <asp:ListItem Value="8">Ağustoz</asp:ListItem>
                                    <asp:ListItem Value="9">Eylül</asp:ListItem>
                                    <asp:ListItem Value="10">Ekim</asp:ListItem>
                                    <asp:ListItem Value="11">Kasım</asp:ListItem>
                                    <asp:ListItem Value="12">Aralık</asp:ListItem>
                                </asp:DropDownList>
								<asp:DropDownList ID="dplYear" runat="server" style="width:32%;font-size:18px;"></asp:DropDownList>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtAdres" runat="server" placeholder="Doğum Yeri" MaxLength="50"></asp:TextBox>
							</div>
						</div>
					</td></tr>
				</table>
				<asp:Button ID="btnRegister" runat="server" Text="Güncelle" class="btn mt-3" OnClick="btnRegister_Click"/>
				<asp:Button ID="btnEnable" runat="server" Visible="false" Text="Aktif et" class="btn mt-3" OnClick="btnEnable_Click" style="background:green;"/>
				<asp:Button ID="btnDisable" runat="server" Visible="false" Text="Devredışı bırak" class="btn mt-3" OnClick="btnDisable_Click" style="background:orange;"/>
				<asp:Button ID="btnDeleteUser" runat="server" Text="Sil" class="btn mt-3" OnClick="btnDeleteUser_Click" style="background:red;"/>
			</div>
			<script type='text/javascript' src='./assets/ortak/bootstrap.bundle.min.js'></script>
			<script>
                function readURL(input) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $('#imgAvatar').attr('src', e.target.result);
                        }

                        reader.readAsDataURL(input.files[0]);
                    }
                }

                $("#uplAvatar").change(function () {
                    readURL(this);
                });

            </script>
		</div>
		<div id="div3" Visible="false" runat="server">
			<div class="wrapper" style="min-width:700px;">
				<table style="min-width: 630px;">
					<tr><td>
						<div class="p-3 mt-3">
							<div style="width: 50%;float:left;">
								<asp:FileUpload ID="uplAracResim" runat="server" style="width:120px;height:120px;opacity:0;position:absolute;"/>
								<asp:ImageButton class="form-field d-flex align-items-center fixed-height" ID="ibAracResim" runat="server" style="width:120px;height:120px;padding-left:0px;"  ImageUrl="~/assets/ortak/pictures/car_logo_blank.png"/>
							</div>
							<div style="margin-left:50%;height:100px;">
								<p style="font-size:24px;line-height:26px;margin-top:40px;">Araç Fotoğrafı</p>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:DropDownList ID="dplKayitYontemi" runat="server" OnSelectedIndexChanged="dplKayitYontemi_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="0">Yeni Kayıt</asp:ListItem>
                                    <asp:ListItem Value="1">Yeni Kayıt (Marka Girişi İle)</asp:ListItem>
                                    <asp:ListItem Value="2">Kayıt Güncelle</asp:ListItem>
                                </asp:DropDownList>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtPlaka" runat="server" placeholder="Plaka Kodu" MaxLength="50"></asp:TextBox>
								<asp:DropDownList ID="dplKayitlar" Visible="False" runat="server" DataSourceID="SqlDataSource1" DataTextField="aracPlaka" DataValueField="aracPlaka" AutoPostBack="True" OnSelectedIndexChanged="dplKayitlar_SelectedIndexChanged"></asp:DropDownList>
							    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="Data Source=DESKTOP-DM5D20S\SQLEXPRESS;Initial Catalog=AracKiralamaOtomasyonu;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [aracPlaka] FROM [aracList]"></asp:SqlDataSource>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtAracMarka" runat="server" Visible="False" placeholder="Arac Markası" MaxLength="50"></asp:TextBox>
								<asp:DropDownList ID="dplAracMarka" runat="server" DataSourceID="SqlDataSource2" DataTextField="aracMarkaAdi" DataValueField="aracMarkaAdi"></asp:DropDownList>
							    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="Data Source=DESKTOP-DM5D20S\SQLEXPRESS;Initial Catalog=AracKiralamaOtomasyonu;Integrated Security=True" ProviderName="System.Data.SqlClient" SelectCommand="SELECT [aracMarkaAdi] FROM [aracMarka]"></asp:SqlDataSource>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtAracAdi" runat="server" placeholder="Arac Adı" MaxLength="50"></asp:TextBox>
							</div>
						</div>
					</td>
					<td>
						<div class="p-3 mt-3">
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtRenk" runat="server" placeholder="Renk"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:DropDownList ID="dplYakitTipi" runat="server">
                                    <asp:ListItem Value="0">Dizel</asp:ListItem>
                                    <asp:ListItem Value="1">Benzin</asp:ListItem>
                                </asp:DropDownList>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:DropDownList ID="dplVitesTipi" runat="server">
                                    <asp:ListItem Value="Manuel">Manuel Vites</asp:ListItem>
                                    <asp:ListItem Value="Otomatik">Otomatik Vites</asp:ListItem>
                                </asp:DropDownList>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtBavulAded" runat="server" placeholder="Bavul Sayısı"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtKoltukAded" runat="server" placeholder="Koltuk Sayısı"></asp:TextBox>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:DropDownList ID="dplKlimaDurumu" runat="server">
                                    <asp:ListItem Value="False">Klimasız</asp:ListItem>
                                    <asp:ListItem Value="True">Klimalı</asp:ListItem>
                                </asp:DropDownList>
							</div>
						</div>
					</td></tr>
				</table>
				<asp:Button ID="btnAddCar" runat="server" Text="Kaydet" class="btn mt-3" OnClick="btnAddCar_Click"/>
				<asp:Button ID="btnDelete" runat="server" Visible="false" Text="Kaydı Sil" class="btn mt-3" OnClick="btnDelete_Click" style="background:red;"/>
			</div>
			<script type='text/javascript' src='./assets/ortak/bootstrap.bundle.min.js'></script>
			<script>
                function readURL(input) {
                    if (input.files && input.files[0]) {
                        var reader = new FileReader();

                        reader.onload = function (e) {
                            $('#ibAracResim').attr('src', e.target.result);
                        }

                        reader.readAsDataURL(input.files[0]);
                    }
                }

                $("#uplAracResim").change(function () {
                    readURL(this);
                });

            </script>
		</div>
    </form>
</body>
</html>
