
Imports System.Data
Imports System.Data.SqlClient

Partial Class Clientes
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load

        If Session("UsuarioId") Is Nothing OrElse
           Session("RoleId") Is Nothing OrElse
           Convert.ToInt32(Session("RoleId")) <> 1 Then

            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarSegmentos()
            CargarClientes()
        End If

    End Sub

    Private Sub CargarSegmentos()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()

            Using comando As New SqlCommand("sp_ObtenerSegmentos", conexion)
                comando.CommandType = CommandType.StoredProcedure

                Using adaptador As New SqlDataAdapter(comando)

                    Dim tabla As New DataTable()
                    adaptador.Fill(tabla)

                    ddlSegmento.DataSource = tabla
                    ddlSegmento.DataTextField = "Nombre"
                    ddlSegmento.DataValueField = "SegmentoId"
                    ddlSegmento.DataBind()

                    ddlSegmento.Items.Insert(0,
                        New ListItem("-- Seleccione un segmento --", ""))

                End Using
            End Using
        End Using

    End Sub

    Private Sub CargarClientes()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()

            Using comando As New SqlCommand("sp_ObtenerClientes", conexion)
                comando.CommandType = CommandType.StoredProcedure

                Using adaptador As New SqlDataAdapter(comando)

                    Dim tabla As New DataTable()
                    adaptador.Fill(tabla)

                    gvClientes.DataSource = tabla
                    gvClientes.DataBind()

                End Using
            End Using
        End Using

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim nombres As String = txtNombres.Text.Trim()
        Dim apellidos As String = txtApellidos.Text.Trim()
        Dim cedula As String = txtCedula.Text.Trim()
        Dim correo As String = txtCorreo.Text.Trim()
        Dim telefono As String = txtTelefono.Text.Trim()

        If nombres = "" OrElse apellidos = "" OrElse
           cedula = "" OrElse correo = "" OrElse telefono = "" Then

            lblMensaje.Text = "Complete todos los campos."
            Return
        End If

        ' Validación sensible ejecutada en el servidor.
        If Not ValidarCedula(cedula) Then
            lblMensaje.Text = "La cédula ingresada no es válida."
            Return
        End If

        Dim segmentoId As Integer

        If Not Integer.TryParse(ddlSegmento.SelectedValue, segmentoId) Then
            lblMensaje.Text = "Seleccione un segmento válido."
            Return
        End If

        Try

            Using conexion As SqlConnection = ConexionBD.CrearConexion()

                Using comando As New SqlCommand("sp_InsertarCliente", conexion)

                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@SegmentoId", SqlDbType.Int).Value = segmentoId
                    comando.Parameters.Add("@Nombres", SqlDbType.NVarChar, 100).Value = nombres
                    comando.Parameters.Add("@Apellidos", SqlDbType.NVarChar, 100).Value = apellidos
                    comando.Parameters.Add("@Cedula", SqlDbType.NVarChar, 10).Value = cedula
                    comando.Parameters.Add("@Correo", SqlDbType.NVarChar, 150).Value = correo
                    comando.Parameters.Add("@Telefono", SqlDbType.NVarChar, 20).Value = telefono

                    conexion.Open()
                    comando.ExecuteNonQuery()

                End Using
            End Using

            lblMensaje.Text = "Cliente registrado correctamente."

            txtNombres.Text = ""
            txtApellidos.Text = ""
            txtCedula.Text = ""
            txtCorreo.Text = ""
            txtTelefono.Text = ""
            ddlSegmento.SelectedIndex = 0

            CargarClientes()

        Catch ex As SqlException

            If ex.Number = 2601 OrElse ex.Number = 2627 Then
                lblMensaje.Text = "Ya existe un cliente con esa cédula."
            Else
                lblMensaje.Text = "No se pudo registrar el cliente. Revise los datos."
            End If

        Catch ex As Exception

            lblMensaje.Text = "Ocurrió un error al guardar el cliente."

        End Try

    End Sub

    Private Function ValidarCedula(cedula As String) As Boolean

        If cedula.Length <> 10 OrElse Not IsNumeric(cedula) Then
            Return False
        End If

        Dim provincia As Integer = Convert.ToInt32(cedula.Substring(0, 2))
        Dim tercerDigito As Integer = Convert.ToInt32(cedula.Substring(2, 1))

        If Not ((provincia >= 1 AndAlso provincia <= 24) OrElse provincia = 30) Then
            Return False
        End If

        If tercerDigito >= 6 Then
            Return False
        End If

        Dim suma As Integer = 0

        For i As Integer = 0 To 8

            Dim digito As Integer = Convert.ToInt32(cedula(i).ToString())

            If i Mod 2 = 0 Then
                digito *= 2

                If digito > 9 Then
                    digito -= 9
                End If
            End If

            suma += digito

        Next

        Dim verificador As Integer = (10 - (suma Mod 10)) Mod 10

        Return verificador = Convert.ToInt32(cedula(9).ToString())

    End Function

End Class
