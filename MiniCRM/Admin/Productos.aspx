
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Productos.aspx.vb" Inherits="MiniCRM.Productos" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Administración de Productos</title>
    <meta charset="utf-8" />
    <style>
        body { font-family: Arial; background: #f4f6f9; padding: 30px; }
        .contenedor { max-width: 1100px; margin: auto; background: white; padding: 25px; border-radius: 10px; }
        .grupo { margin-bottom: 14px; }
        .grupo label { display: block; margin-bottom: 6px; font-weight: bold; }
        .grupo input, .grupo select { width: 100%; padding: 10px; box-sizing: border-box; border: 1px solid #ccc; border-radius: 5px; }
        .boton { background: #1769aa; color: white; border: 0; padding: 11px 20px; border-radius: 5px; cursor: pointer; }
        .mensaje { display: block; margin: 15px 0; font-weight: bold; }
        .tabla { width: 100%; margin-top: 25px; border-collapse: collapse; }
        .tabla th, .tabla td { padding: 10px; border: 1px solid #ddd; text-align: left; }
        .tabla th { background: #24364b; color: white; }
    </style>
</head>
<body>
<form id="form1" runat="server">
    <div class="contenedor">
        <h2>Administración de Productos</h2>
        <p>Registro de productos y asociación con sus categorías.</p>

        <div class="grupo">
            <label>Nombre del producto:</label>
            <asp:TextBox ID="txtNombre" runat="server" MaxLength="100" />
        </div>

        <div class="grupo">
            <label>Descripción:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Rows="3" />
        </div>

        <div class="grupo">
            <label>Precio:</label>
            <asp:TextBox ID="txtPrecio" runat="server" TextMode="SingleLine" />
        </div>

        <div class="grupo">
            <label>Categoría:</label>
            <asp:DropDownList ID="ddlCategoria" runat="server" />
        </div>

        <asp:Button ID="btnGuardar" runat="server"
            Text="Guardar producto" CssClass="boton" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />

        <h2>Productos registrados</h2>
        <asp:GridView ID="gvProductos" runat="server"
            AutoGenerateColumns="True" CssClass="tabla"
            EmptyDataText="No hay productos registrados." />
    </div>
</form>
</body>
</html>
