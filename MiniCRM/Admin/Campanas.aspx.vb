
Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization

Partial Class Campanas
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UsuarioId") Is Nothing OrElse
           Session("RoleId") Is Nothing OrElse
           Convert.ToInt32(Session("RoleId")) <> 1 Then

            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarProductos()
            CargarCampanas()
        End If

    End Sub

    Private Sub CargarProductos()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()
            Using comando As New SqlCommand("sp_ObtenerProductos", conexion)

                comando.CommandType = CommandType.StoredProcedure

                Using adaptador As New SqlDataAdapter(comando)
                    Dim tabla As New DataTable()
                    adaptador.Fill(tabla)

                    ddlProducto.DataSource = tabla
                    ddlProducto.DataTextField = "Nombre"
                    ddlProducto.DataValueField = "ProductoId"
                    ddlProducto.DataBind()

                    ddlProducto.Items.Insert(0,
                        New ListItem("-- Sin producto asociado --", "0"))
                End Using

            End Using
        End Using

    End Sub

    Private Sub CargarCampanas()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()
            Using comando As New SqlCommand("sp_ObtenerCampanas", conexion)

                comando.CommandType = CommandType.StoredProcedure

                Using adaptador As New SqlDataAdapter(comando)
                    Dim tabla As New DataTable()
                    adaptador.Fill(tabla)

                    gvCampanas.DataSource = tabla
                    gvCampanas.DataBind()
                End Using

            End Using
        End Using

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim nombre As String = txtNombre.Text.Trim()
        Dim descripcion As String = txtDescripcion.Text.Trim()
        Dim fechaInicio As DateTime
        Dim fechaFin As DateTime

        If nombre = "" Then
            lblMensaje.Text = "Ingrese el nombre de la campaña."
            Return
        End If

        If Not DateTime.TryParseExact(txtFechaInicio.Text,
            "yyyy-MM-dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, fechaInicio) Then

            lblMensaje.Text = "Ingrese una fecha de inicio válida."
            Return
        End If

        If Not DateTime.TryParseExact(txtFechaFin.Text,
            "yyyy-MM-dd", CultureInfo.InvariantCulture,
            DateTimeStyles.None, fechaFin) Then

            lblMensaje.Text = "Ingrese una fecha de finalización válida."
            Return
        End If

        If fechaFin < fechaInicio Then
            lblMensaje.Text = "La fecha final no puede ser anterior a la fecha inicial."
            Return
        End If

        Dim productoId As Integer

        If Not Integer.TryParse(ddlProducto.SelectedValue, productoId) Then
            lblMensaje.Text = "Seleccione un producto válido."
            Return
        End If

        Try

            Using conexion As SqlConnection = ConexionBD.CrearConexion()
                Using comando As New SqlCommand("sp_InsertarCampana", conexion)

                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@ProductoId", SqlDbType.Int).Value =
                        If(productoId = 0, CType(DBNull.Value, Object), productoId)

                    comando.Parameters.Add("@Nombre", SqlDbType.NVarChar, 100).Value = nombre

                    comando.Parameters.Add("@Descripcion", SqlDbType.NVarChar, -1).Value =
                        If(descripcion = "", CType(DBNull.Value, Object), descripcion)

                    comando.Parameters.Add("@FechaInicio", SqlDbType.Date).Value = fechaInicio.Date
                    comando.Parameters.Add("@FechaFin", SqlDbType.Date).Value = fechaFin.Date

                    conexion.Open()
                    comando.ExecuteNonQuery()

                End Using
            End Using

            lblMensaje.Text = "Campaña registrada correctamente."

            txtNombre.Text = ""
            txtDescripcion.Text = ""
            txtFechaInicio.Text = ""
            txtFechaFin.Text = ""
            ddlProducto.SelectedIndex = 0

            CargarCampanas()

        Catch ex As SqlException
            lblMensaje.Text = "No se pudo registrar la campaña. Verifique los datos y el producto."
        Catch ex As Exception
            lblMensaje.Text = "Ocurrió un error al guardar la campaña."
        End Try

    End Sub

End Class
