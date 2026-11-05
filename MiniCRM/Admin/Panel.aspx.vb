Partial Public Class Panel
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

    End Sub

End Class