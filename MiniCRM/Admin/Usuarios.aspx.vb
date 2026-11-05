
Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography

Partial Class Usuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If Session("UsuarioId") Is Nothing Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Convert.ToInt32(Session("RoleId")) <> 1 Then
            Response.Redirect("~/Default.aspx")
            Return
        End If

        If Not IsPostBack Then
            CargarRoles()
            CargarUsuarios()
        End If

    End Sub

    Private Sub CargarRoles()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()

            Using comando As New SqlCommand(
                "SELECT RoleId, Nombre FROM Roles ORDER BY Nombre", conexion)

                conexion.Open()

                ddlRol.DataSource = comando.ExecuteReader()
                ddlRol.DataTextField = "Nombre"
                ddlRol.DataValueField = "RoleId"
                ddlRol.DataBind()

            End Using
        End Using

        ddlRol.Items.Insert(0, New ListItem("Seleccione un rol", ""))

    End Sub

    Private Sub CargarUsuarios()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()

            Using comando As New SqlCommand("sp_ObtenerUsuarios", conexion)

                comando.CommandType = CommandType.StoredProcedure

                Using adaptador As New SqlDataAdapter(comando)

                    Dim tabla As New DataTable()
                    adaptador.Fill(tabla)

                    gvUsuarios.DataSource = tabla
                    gvUsuarios.DataBind()

                End Using
            End Using
        End Using

    End Sub

    Protected Sub btnGuardar_Click(ByVal sender As Object,
                                   ByVal e As EventArgs) Handles btnGuardar.Click

        Dim cedula As String = txtCedula.Text.Trim()
        Dim nombres As String = txtNombres.Text.Trim()
        Dim apellidos As String = txtApellidos.Text.Trim()
        Dim usuario As String = txtNombreUsuario.Text.Trim()
        Dim correo As String = txtCorreo.Text.Trim()
        Dim password As String = txtPassword.Text

        If nombres = "" OrElse apellidos = "" OrElse
           usuario = "" OrElse correo = "" OrElse password = "" Then

            lblMensaje.Text = "Complete todos los campos."
            Return
        End If

        If Not ValidarCedula(cedula) Then
            lblMensaje.Text = "La cédula ecuatoriana no es válida."
            Return
        End If

        If ddlRol.SelectedValue = "" Then
            lblMensaje.Text = "Seleccione un rol."
            Return
        End If

        If password.Length < 8 Then
            lblMensaje.Text = "La contraseña debe tener al menos 8 caracteres."
            Return
        End If

        Try

            ' Obtener el rol seleccionado
            Dim roleId As Integer = Convert.ToInt32(ddlRol.SelectedValue)

            ' Comprobar que el rol exista en la base de datos
            Using conexion As SqlConnection = ConexionBD.CrearConexion()

                Using comando As New SqlCommand("SELECT COUNT(*) FROM Roles WHERE RoleId = @RoleId", conexion)

                    comando.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId

                    conexion.Open()

                    Dim existeRol As Integer = Convert.ToInt32(comando.ExecuteScalar())

                    If existeRol = 0 Then
                        lblMensaje.Text = "El rol seleccionado no existe."
                        Return
                    End If

                End Using

            End Using

            ' Generar contraseña segura
            Dim salt As Byte() = Seguridad.GenerarSalt()
            Dim hash As Byte() = Seguridad.GenerarHash(password, salt)

            Using conexion As SqlConnection = ConexionBD.CrearConexion()

                Using comando As New SqlCommand("sp_InsertarUsuario", conexion)

                    comando.CommandType = CommandType.StoredProcedure

                    comando.Parameters.Add("@RoleId", SqlDbType.Int).Value = roleId
                    comando.Parameters.Add("@Nombres", SqlDbType.NVarChar, 80).Value = nombres
                    comando.Parameters.Add("@Apellidos", SqlDbType.NVarChar, 80).Value = apellidos
                    comando.Parameters.Add("@Cedula", SqlDbType.NVarChar, 10).Value = cedula
                    comando.Parameters.Add("@NombreUsuario", SqlDbType.NVarChar, 40).Value = usuario
                    comando.Parameters.Add("@Correo", SqlDbType.NVarChar, 100).Value = correo
                    comando.Parameters.Add("@PasswordHash", SqlDbType.VarBinary, 64).Value = hash
                    comando.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary, 32).Value = salt

                    conexion.Open()
                    comando.ExecuteNonQuery()

                End Using
            End Using

            lblMensaje.Text = "Usuario registrado correctamente."

            LimpiarFormulario()
            CargarRoles()
            CargarUsuarios()

        Catch ex As SqlException

            If ex.Number = 2601 OrElse ex.Number = 2627 Then
                lblMensaje.Text = "La cédula, el usuario o el correo ya están registrados."
            Else
                lblMensaje.Text = "Error al guardar: " & ex.Message
            End If

        Catch ex As Exception

            lblMensaje.Text = "Error: " & ex.Message

        End Try

    End Sub

    Private Function ValidarCedula(ByVal cedula As String) As Boolean

        If cedula.Length <> 10 OrElse Not IsNumeric(cedula) Then
            Return False
        End If

        Dim provincia As Integer = Convert.ToInt32(cedula.Substring(0, 2))
        Dim tercerDigito As Integer = Convert.ToInt32(cedula.Substring(2, 1))

        If (provincia < 1 OrElse provincia > 24) AndAlso provincia <> 30 Then
            Return False
        End If

        If tercerDigito >= 6 Then
            Return False
        End If

        Dim suma As Integer = 0

        For i As Integer = 0 To 8

            Dim numero As Integer = Convert.ToInt32(cedula(i).ToString())

            If i Mod 2 = 0 Then
                numero *= 2

                If numero > 9 Then
                    numero -= 9
                End If
            End If

            suma += numero

        Next

        Dim decena As Integer = ((suma + 9) \ 10) * 10
        Dim verificador As Integer = decena - suma

        If verificador = 10 Then
            verificador = 0
        End If

        Return verificador = Convert.ToInt32(cedula(9).ToString())

    End Function

    Private Sub LimpiarFormulario()

        txtNombres.Text = ""
        txtApellidos.Text = ""
        txtCedula.Text = ""
        txtNombreUsuario.Text = ""
        txtCorreo.Text = ""
        txtPassword.Text = ""
        ddlRol.SelectedIndex = 0

    End Sub

End Class
