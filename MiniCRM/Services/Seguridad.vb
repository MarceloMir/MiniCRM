
Imports System.Security.Cryptography

Public Class Seguridad

    Public Shared Function GenerarSalt() As Byte()
        Dim salt(31) As Byte

        Using generador As RandomNumberGenerator = RandomNumberGenerator.Create()
            generador.GetBytes(salt)
        End Using

        Return salt
    End Function

    Public Shared Function GenerarHash(
        ByVal password As String,
        ByVal salt As Byte()
    ) As Byte()

        Using derivador As New Rfc2898DeriveBytes(
            password, salt, 100000, HashAlgorithmName.SHA256)

            Return derivador.GetBytes(64)

        End Using
    End Function

    Public Shared Function VerificarPassword(
        ByVal password As String,
        ByVal salt As Byte(),
        ByVal hashGuardado As Byte()
    ) As Boolean

        Dim hashIngresado As Byte() = GenerarHash(password, salt)

        If hashIngresado.Length <> hashGuardado.Length Then
            Return False
        End If

        Dim diferencia As Integer = 0

        For i As Integer = 0 To hashGuardado.Length - 1
            diferencia = diferencia Or (hashIngresado(i) Xor hashGuardado(i))
        Next

        Return diferencia = 0

    End Function

    Public Shared Function VerificarPasswordAnterior(
    ByVal password As String,
    ByVal salt As Byte(),
    ByVal hashGuardado As Byte()
) As Boolean

        Using derivador As New Rfc2898DeriveBytes(password, salt, 100000)

            Dim hashAnterior As Byte() = derivador.GetBytes(64)

            If hashAnterior.Length <> hashGuardado.Length Then
                Return False
            End If

            Dim diferencia As Integer = 0

            For i As Integer = 0 To hashGuardado.Length - 1
                diferencia = diferencia Or (hashAnterior(i) Xor hashGuardado(i))
            Next

            Return diferencia = 0

        End Using

    End Function
End Class
