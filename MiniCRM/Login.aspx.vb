
Imports System.Data
Imports System.Data.SqlClient

Partial Class Login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object,
                            ByVal e As EventArgs) Handles Me.Load

        If Session("UsuarioId") IsNot Nothing Then
            Response.Redirect("~/Default.aspx")
        End If

    End Sub

    Protected Sub btnIngresar_Click(ByVal sender As Object,
                                    ByVal e As EventArgs) Handles btnIngresar.Click

        Dim usuario As String = txtUsuario.Text.Trim()
        Dim password As String = txtPassword.Text

        If usuario = "" OrElse password = "" Then
            lblMensaje.Text = "Ingrese usuario y contraseña."
            Return
        End If

        Try

            Using conexion As SqlConnection = ConexionBD.CrearConexion()

                Using comando As New SqlCommand("sp_ObtenerUsuarioLogin", conexion)

                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.Add("@Usuario", SqlDbType.NVarChar, 100).Value = usuario

                    conexion.Open()

                    Dim usuarioId As Integer = 0
                    Dim nombres As String = ""
                    Dim roleId As Integer = 0
                    Dim rol As String = ""
                    Dim salt As Byte() = Nothing
                    Dim hash As Byte() = Nothing

                    Using lector As SqlDataReader = comando.ExecuteReader()

                        If lector.Read() Then

                            usuarioId = Convert.ToInt32(lector("UsuarioId"))
                            nombres = lector("Nombres").ToString()
                            roleId = Convert.ToInt32(lector("RoleId"))
                            rol = lector("Rol").ToString()

                            salt = DirectCast(lector("PasswordSalt"), Byte())
                            hash = DirectCast(lector("PasswordHash"), Byte())

                        End If

                    End Using

                    If salt Is Nothing OrElse hash Is Nothing Then
                        lblMensaje.Text = "Usuario o contraseña incorrectos."
                        Return
                    End If

                    Dim correcta As Boolean =
                        Seguridad.VerificarPassword(password, salt, hash)

                    Dim anterior As Boolean = False

                    If Not correcta Then
                        anterior = Seguridad.VerificarPasswordAnterior(
                            password, salt, hash)
                    End If

                    If Not correcta AndAlso Not anterior Then
                        lblMensaje.Text = "Usuario o contraseña incorrectos."
                        Return
                    End If

                    ' Actualizar automáticamente contraseñas antiguas.
                    If anterior Then

                        Dim nuevoSalt As Byte() = Seguridad.GenerarSalt()
                        Dim nuevoHash As Byte() =
                            Seguridad.GenerarHash(password, nuevoSalt)

                        Using actualizar As New SqlCommand(
                            "sp_ActualizarPassword", conexion)

                            actualizar.CommandType = CommandType.StoredProcedure

                            actualizar.Parameters.Add("@UsuarioId", SqlDbType.Int).Value = usuarioId
                            actualizar.Parameters.Add("@PasswordHash", SqlDbType.VarBinary, 64).Value = nuevoHash
                            actualizar.Parameters.Add("@PasswordSalt", SqlDbType.VarBinary, 32).Value = nuevoSalt

                            actualizar.ExecuteNonQuery()

                        End Using

                    End If

                    Session("UsuarioId") = usuarioId
                    Session("NombreUsuario") = nombres
                    Session("RoleId") = roleId
                    Session("Rol") = rol

                    Response.Redirect("~/Default.aspx", False)
                    Context.ApplicationInstance.CompleteRequest()
                    Return

                End Using
            End Using

        Catch ex As Exception

            lblMensaje.Text = "Error al iniciar sesión: " & ex.Message

        End Try

    End Sub

End Class
