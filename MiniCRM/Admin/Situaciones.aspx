
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Situaciones.aspx.vb" Inherits="MiniCRM.Situaciones" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Administrar situaciones</title>
    <meta charset="utf-8" />
</head>
<body>
<form id="form1" runat="server">
    <h1>Administración de situaciones comerciales</h1>
    <p>Situaciones que pueden presentarse durante una venta.</p>

    <p>Nombre:</p>
    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" />

    <p>Descripción:</p>
    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />

    <br /><br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar situación" />
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" />

    <h2>Situaciones registradas</h2>
    <asp:GridView ID="gvSituaciones" runat="server"
        AutoGenerateColumns="True"
        EmptyDataText="No existen situaciones registradas." />

    <br />
    <asp:HyperLink runat="server" NavigateUrl="~/Admin/Panel.aspx">
        Volver a administración
    </asp:HyperLink>
</form>
</body>
</html>
