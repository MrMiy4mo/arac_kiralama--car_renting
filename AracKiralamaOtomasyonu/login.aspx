<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="AracKiralamaOtomasyonu.login" %>

<!DOCTYPE html>
<html lang="tr" xmlns="http://www.w3.org/1999/xhtml">
	<head runat="server">
		<meta charset='utf-8'/>
		<meta name='viewport' content='width=device-width, initial-scale=1'/>
		<title>Giriş Ekranı</title>
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
			<div class="wrapper">
				<div class="logo">
					<img src="./assets/ortak/pictures/car_logo.png"/>
				</div>
				<div class="text-center mt-4 name">
					Hoş Geldiniz! <br/> Giriş Yapınız
				</div>
                <div class="p-3 mt-3">
                    <div class="form-field d-flex align-items-center">
                        <span class="far fa-user"></span>
                        <asp:TextBox ID="txtTCNO" runat="server" placeholder="Kimlik Numarası" MaxLength="11"></asp:TextBox>
                    </div>
                    <div class="form-field d-flex align-items-center">
                        <span class="fas fa-key"></span>
                        <asp:TextBox ID="txtPass" runat="server" placeholder="Şifre" TextMode="Password"></asp:TextBox>
                    </div>
					<asp:Button ID="btnLogin" runat="server" Text="Giriş Yap" class="btn mt-3" OnClick="btnLogin_Click"/>
                </div>
				<div class="text-center fs-6">
					<asp:LinkButton ID="lbRegister" runat="server" OnClick="lbRegister_Click"> Kayıt Ol</asp:LinkButton>
				</div>
			</div>
			<script type='text/javascript' src='./assets/ortak/bootstrap.bundle.min.js'></script>
		</form>
	</body>
</html>
