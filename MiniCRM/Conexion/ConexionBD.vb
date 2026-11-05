Imports System.Configuration
Imports System.Data.SqlClient

Public Class ConexionBD
    Public Shared Function CrearConexion() As SqlClient.SqlConnection
        Dim cadena As String = ConfigurationManager.ConnectionStrings("MiniCRMConnection").ConnectionString
        Return New SqlClient.SqlConnection(cadena)
    End Function
End Class
