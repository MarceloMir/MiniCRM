Imports System
Imports System.Runtime.CompilerServices

Public Class PruebaConexion
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ProbarConexion()
        End If
    End Sub

    Private Sub ProbarConexion()
        Try
            Using conexion As SqlClient.SqlConnection = ConexionBD.CrearConexion()
                conexion.Open()
                lblResultado.Text = "Conexión exitosa a la base de datos."
            End Using
        Catch ex As Exception
            lblResultado.Text = "Error al conectar a la base de datos: " & ex.Message
        End Try
    End Sub

End Class