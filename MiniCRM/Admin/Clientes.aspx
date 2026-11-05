
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Clientes.aspx.vb" Inherits="MiniCRM.Clientes" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title>Administración de Clientes - MiniCRM</title>
    <meta charset="utf-8" />
    <style>
        body {
            font-family: Arial, sans-serif;
            background: #f4f6f9;
            margin: 0;
            padding: 30px;
        }

        .contenedor {
            max-width: 1100px;
            margin: auto;
            background: white;
            padding: 25px;
            border-radius: 10px;
            box-shadow: 0 2px 10px #ddd;
        }

        h2 { color: #24364b; }

        .grupo {
            margin-bottom: 14px;
        }

        .grupo label {
            display: block;
            margin-bottom: 6px;
            font-weight: bold;
        }

        .grupo input, .grupo select {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
        }

        .boton {
            background: #1769aa;
            color: white;
            border: none;
            padding: 11px 20px;
            border-radius: 5px;
            cursor: pointer;
        }

        .mensaje {
            display: block;
            margin: 15px 0;
            font-weight: bold;
        }

        .tabla {
            width: 100%;
            margin-top: 25px;
            border-collapse: collapse;
        }

        .tabla th, .tabla td {
            padding: 10px;
            border: 1px solid #ddd;
            text-align: left;
        }

        .tabla th {
            background: #24364b;
            color: white;
        }
    </style>
</head>

<body>
<form id="form1" runat="server">

    <div class="contenedor">
        <h2>Administración de Clientes</h2>
        <p>Registro y consulta de clientes del Mini-CRM.</p>

        <div class="grupo">
            <label>Nombres:</label>
            <asp:TextBox ID="txtNombres" runat="server" />
        </div>

        <div class="grupo">
            <label>Apellidos:</label>
            <asp:TextBox ID="txtApellidos" runat="server" />
        </div>

        <div class="grupo">
            <label>Cédula:</label>
            <asp:TextBox ID="txtCedula" runat="server" MaxLength="10" />
        </div>

        <div class="grupo">
            <label>Correo:</label>
            <asp:TextBox ID="txtCorreo" runat="server" TextMode="Email" />
        </div>

        <div class="grupo">
            <label>Teléfono:</label>
            <asp:TextBox ID="txtTelefono" runat="server" />
        </div>

        <div class="grupo">
            <label>Segmento del cliente:</label>
            <asp:DropDownList ID="ddlSegmento" runat="server" />
        </div>

        <asp:Button ID="btnGuardar" runat="server"
            Text="Guardar cliente" CssClass="boton" />

        <asp:Label ID="lblMensaje" runat="server" CssClass="mensaje" />

        <h2>Clientes registrados</h2>

        <asp:GridView ID="gvClientes" runat="server"
            AutoGenerateColumns="True"
            CssClass="tabla"
            EmptyDataText="Todavía no hay clientes registrados." />

    </div>

</form>
</body>
</html>
