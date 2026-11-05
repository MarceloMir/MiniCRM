
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Categorias.aspx.vb" Inherits="MiniCRM.Categorias" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>Administrar categorías</title>
    <meta charset="utf-8" />
</head>
<body>
<form id="form1" runat="server">
    <h1>Administración de categorías</h1>
    <p>Clasificación de los productos del sistema.</p>

    <p>Nombre:</p>
    <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" />

    <p>Descripción:</p>
    <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />

    <br /><br />
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar categoría" />
    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" />

    <h2>Categorías registradas</h2>
    <asp:GridView ID="gvCategorias" runat="server"
        AutoGenerateColumns="True"
        EmptyDataText="No existen categorías registradas." />

    <br />
    <asp:HyperLink runat="server" NavigateUrl="~/Admin/Panel.aspx">
        Volver a administración
    </asp:HyperLink>
</form>
</body>
</html>
