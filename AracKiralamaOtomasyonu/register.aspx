<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="AracKiralamaOtomasyonu.register" %>

<!DOCTYPE html>

<html lang="tr" xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
		<meta charset='utf-8'/>
		<meta name='viewport' content='width=device-width, initial-scale=1'/>
		<title>Kayıt Ol</title>
		<link rel="icon" type="image/x-icon" href="./assets/ortak/login.ico"/>
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
				max-width: 700px;
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

			.wrapper .form-field input{
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
				padding-left: 10px;
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
				color: #03A9F4;
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
		</style>
	</head>
<body class='snippet-body'>
    <form id="form1" runat="server">
			<div class="wrapper" style="min-width:700px;">
				<table>
					<tr><td>
						<div class="p-3 mt-3">
							<div style="width: 50%;float:left;">
								<asp:FileUpload ID="uplAvatar" runat="server" style="width:120px;height:120px;opacity:0;position:absolute;"/>
								<asp:ImageButton class="form-field d-flex align-items-center fixed-height" ID="imgAvatar" runat="server" style="width:120px;height:120px;padding-left:0px;"  ImageUrl="~/assets/ortak/pictures/avatar.png"/>
							</div>
							<div style="margin-left:50%;height:140px;">
								<p style="font-size:24px;line-height:26px;">Profil Fotoğrafı</p>
								<asp:RadioButtonList ID="rbCinsiyet" runat="server">
                                    <asp:ListItem Value="E">Erkek</asp:ListItem>
                                    <asp:ListItem Value="K">Kadın</asp:ListItem>
                                </asp:RadioButtonList>
							</div>
							<div class="form-field d-flex align-items-center fixed-height">
								<asp:TextBox ID="txtTCNO" runat="server" placeholder="TC Kimlik Numarası" MaxLength="11"></asp:TextBox>
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
				<asp:Button ID="btnRegister" runat="server" Text="Kayıt Ol" class="btn mt-3" OnClick="btnRegister_Click"/>
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
    </form>
</body>
</html>
