<%@ Page Title="Home Page" Language="VB" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.vb" Inherits="MiniCRM._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">


    <h1>Mini-CRM</h1>

    <h2>Panel principal</h2>

    <asp:Label ID="lblBienvenida" runat="server" />

    <br />
    <br />

    <asp:HyperLink ID="lnkAdmin" runat="server"
        NavigateUrl="~/Admin/Panel.aspx">
    Administración del sistema
    </asp:HyperLink>

    <br />
    <br />

    <asp:HyperLink ID="lnkOportunidades" runat="server"
        NavigateUrl="~/Core/Oportunidades.aspx">
    Gestión de oportunidades
    </asp:HyperLink>

    <br />
    <br />

    <asp:HyperLink ID="lnkPracticas" runat="server"
        NavigateUrl="~/Core/MejoresPracticas.aspx">
    Mejores prácticas comerciales
    </asp:HyperLink>

    <br />
    <br />

    <asp:HyperLink ID="lnkIndicadores" runat="server"
        NavigateUrl="~/Core/Indicadores.aspx">
    Indicadores comerciales
    </asp:HyperLink>

    <br />
    <br />

    <asp:HyperLink ID="lnkCerrar" runat="server"
        NavigateUrl="~/CerrarSesion.aspx">
    Cerrar sesión
    </asp:HyperLink>


</asp:Content>
