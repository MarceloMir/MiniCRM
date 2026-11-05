
Imports System.Data
Imports System.Data.SqlClient

Partial Class Segmentos
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
        End If

    End Sub

    Private Sub CargarSegmentos()

        Using conexion As SqlConnection = ConexionBD.CrearConexion()
            Using comando As New SqlCommand("sp_ObtenerSegmentos", conexion)

                comando.CommandType = CommandType.StoredProcedure

                Using adaptador As New SqlDataAdapter(comando)
                    Dim tabla As New DataTable()
                    adaptador.Fill(tabla)
                    gvSegmentos.DataSource = tabla
                    gvSegmentos.DataBind()
                End Using

            End Using
        End Using

    End Sub

    Protected Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click

        Dim nombre As String = txtNombre.Text.Trim()
        Dim descripcion As String = txtDescripcion.Text.Trim()

        If nombre = "" Then
            lblMensaje.Text = "Ingrese el nombre del segmento."
            Return
        End If

        Try
            Using conexion As SqlConnection = ConexionBD.CrearConexion()
                Using comando As New SqlCommand("sp_InsertarSegmento", conexion)

                    comando.CommandType = CommandType.StoredProcedure
                    comando.Parameters.AddWithValue("@Nombre", nombre)
                    comando.Parameters.AddWithValue("@Descripcion", descripcion)

                    conexion.Open()
                    comando.ExecuteNonQuery()

                End Using
            End Using

            lblMensaje.Text = "Segmento registrado correctamente."
            txtNombre.Text = ""
            txtDescripcion.Text = ""
            CargarSegmentos()

        Catch ex As SqlException
            lblMensaje.Text = "No se pudo registrar el segmento. Puede que ya exista."
        Catch ex As Exception
            lblMensaje.Text = "Ocurrió un error al registrar el segmento."
        End Try

    End Sub

End Class
