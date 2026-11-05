
Partial Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object,
                            ByVal e As EventArgs) Handles Me.Load

        If Session("UsuarioId") Is Nothing Then
            Response.Redirect("~/Login.aspx")
            Return
        End If

        If Not IsPostBack Then

            lblBienvenida.Text =
                "Bienvenido, " & Session("NombreUsuario").ToString() &
                " | Rol: " & Session("Rol").ToString()

            lnkAdmin.Visible = (Convert.ToInt32(Session("RoleId")) = 1)

            lnkOportunidades.Visible =
                (Convert.ToInt32(Session("RoleId")) = 2 OrElse
                 Convert.ToInt32(Session("RoleId")) = 3)

            lnkPracticas.Visible =
                (Convert.ToInt32(Session("RoleId")) = 2 OrElse
                 Convert.ToInt32(Session("RoleId")) = 3)

            lnkIndicadores.Visible =
                (Convert.ToInt32(Session("RoleId")) = 2)

        End If

    End Sub

End Class
