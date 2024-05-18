<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Prodavnica_Web.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="CSS/StyleSheetLogin.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="wrapper">
            <h2>Login</h2>
            <div class="input-box">
                <asp:TextBox ID="txtEmail" runat="server" TextMode="Email" placeholder="Unesite svoj e-mail" required="true"></asp:TextBox>
            </div>
            <div class="input-box">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" placeholder="Unesite svoju sifru" required="true"></asp:TextBox>
            </div>
            <div class="input-box button">
                <asp:Button ID="btnLogin" runat="server" Text="Uloguj se" OnClick="btnLogin_Click"/>
            </div>
        </div>
    </form>
</body>
</html>
