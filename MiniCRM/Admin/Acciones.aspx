
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Acciones.aspx.vb" Inherits="MiniCRM.Acciones" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Administrar acciones</title>
    <meta charset="utf-8" />
</head>
<body>
<form id="form1" runat="server">
    <h1>Administración de acciones comerciales</h1>
    <p>Acciones que los vendedores pueden realizar ante situaciones comerciales.</p>

    <p>Nombre:</p>
    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" />

    <p>Descripción:</p>
    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />

    <br /><br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar acción" />
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" />

    <h2>Acciones registradas</h2>
    <asp:GridView ID="gvAcciones" runat="server"
        AutoGenerateColumns="True"
        EmptyDataText="No existen acciones registradas." />

    <br />
    <asp:HyperLink runat="server" NavigateUrl="~/Admin/Panel.aspx">
        Volver a administración
    </asp:HyperLink>
</form>
</body>
</html>
