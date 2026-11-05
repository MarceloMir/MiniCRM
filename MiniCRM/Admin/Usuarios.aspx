<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Usuarios.aspx.vb" Inherits="MiniCRM.Usuarios" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <h2>Administración de usuarios</h2>
        <p>Registrar y consultar los usuarios del sistema.</p>

        <h3>Registrar nuevo usuario</h3>

        <div>
            <label>Nombres:</label>
            <asp:TextBox ID="txtNombres" runat="server" />
            <br />
            <br />

            <label>Apellidos:</label>
            <asp:TextBox ID="txtApellidos" runat="server" />
            <br />
            <br />

            <label>Cédula:</label>
            <asp:TextBox ID="txtCedula" runat="server" MaxLength="10" />
            <br />
            <br />

            <label>Nombre de usuario:</label>
            <asp:TextBox ID="txtNombreUsuario" runat="server" />
            <br />
            <br />

            <label>Correo:</label>
            <asp:TextBox ID="txtCorreo" runat="server" TextMode="Email" />
            <br />
            <br />

            <label>Contraseña:</label>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
            <br />
            <br />

            <label>Rol:</label>
            <asp:DropDownList ID="ddlRol" runat="server" />
            <br />
            <br />

            <asp:Button
                ID="btnGuardar"
                runat="server"
                Text="Guardar usuario" />

            <br />
            <br />

            <asp:Label ID="lblMensaje" runat="server" />
        </div>

        <hr />

        <h3>Usuarios registrados</h3>

        <asp:GridView
            ID="gvUsuarios"
            runat="server"
            AutoGenerateColumns="True"
            EmptyDataText="No hay usuarios registrados.">
        </asp:GridView>

    </form>
</body>
</html>
