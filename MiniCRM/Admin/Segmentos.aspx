
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Segmentos.aspx.vb" Inherits="MiniCRM.Segmentos" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Administrar segmentos</title>
    <meta charset="utf-8" />
</head>
<body>
<form id="form1" runat="server">
    <h1>Administración de segmentos</h1>
    <p>Clasificación de clientes según su relación comercial.</p>

    <p>Nombre:</p>
    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" />

    <p>Descripción:</p>
    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />

    <br /><br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar segmento" />
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" />

    <h2>Segmentos registrados</h2>
    <asp:GridView ID="gvSegmentos" runat="server"
        AutoGenerateColumns="True"
        EmptyDataText="No existen segmentos registrados." />

    <br />
    <asp:HyperLink runat="server" NavigateUrl="~/Admin/Panel.aspx">
        Volver a administración
    </asp:HyperLink>
</form>
</body>
</html>
