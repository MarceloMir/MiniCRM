<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Panel.aspx.vb" Inherits="MiniCRM.Panel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">

        <h1>Administración del Mini-CRM</h1>

        <p>Configuración general del sistema.</p>

        <h3>Gestión de usuarios</h3>
        <asp:HyperLink ID="lnkUsuarios" runat="server"
            NavigateUrl="~/Admin/Usuarios.aspx">
    Administrar usuarios
        </asp:HyperLink>

        <h3>Información comercial</h3>

        <asp:HyperLink ID="lnkClientes" runat="server"
            NavigateUrl="~/Admin/Clientes.aspx">
    Administrar clientes
        </asp:HyperLink>

        <br />
        <br />

        <asp:HyperLink ID="lnkProductos" runat="server"
            NavigateUrl="~/Admin/Productos.aspx">
    Administrar productos
        </asp:HyperLink>

        <br />
        <br />

        <asp:HyperLink ID="lnkCampanas" runat="server"
            NavigateUrl="~/Admin/Campanas.aspx">
    Administrar campañas
        </asp:HyperLink>

        <br />
        <br />

        <asp:HyperLink ID="lnkSegmentos" runat="server"
            NavigateUrl="~/Admin/Segmentos.aspx">
    Administrar segmentos de clientes
        </asp:HyperLink>

        <br />
        <br />

        <asp:HyperLink ID="lnkSituaciones" runat="server"
            NavigateUrl="~/Admin/Situaciones.aspx">
    Administrar situaciones comerciales
        </asp:HyperLink>

        <br />
        <br />

        <asp:HyperLink ID="lnkAcciones" runat="server"
            NavigateUrl="~/Admin/Acciones.aspx">
    Administrar acciones comerciales
        </asp:HyperLink>

        <br />
        <br />

        <asp:HyperLink ID="lnkVolver" runat="server"
            NavigateUrl="~/Default.aspx">
    Volver al panel principal
        </asp:HyperLink>

    </form>
</body>
</html>
