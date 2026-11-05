<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Login.aspx.vb" Inherits="MiniCRM.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <h1>Mini-CRM</h1>
        <h2>Iniciar sesión</h2>

        <p>Ingresa tus credenciales para continuar.</p>

        <p>Usuario o correo electrónico:</p>
        <asp:TextBox ID="txtUsuario" runat="server" />

        <p>Contraseña:</p>
        <asp:TextBox ID="txtPassword" runat="server"
            TextMode="Password" />

        <br />
        <br />

        <asp:Button ID="btnIngresar" runat="server"
            Text="Ingresar" />

        <br />
        <br />

        <asp:Label ID="lblMensaje" runat="server" />

    </form>
</body>
</html>
