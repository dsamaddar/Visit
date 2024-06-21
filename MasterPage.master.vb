
Partial Class MasterPage
    Inherits System.Web.UI.MasterPage

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Try
                lblLoggedInUser.Text = "Welcome !" + Session("EmployeeName")
            Catch ex As Exception

            End Try
        End If
    End Sub


    Protected Sub lgStatus_LoggedOut(ByVal sender As Object, ByVal e As System.EventArgs) Handles lgStatus.LoggedOut
        Session("EmployeeID") = ""
        Session("EmployeeName") = ""
        Session("UserID") = ""
        Session("Designation") = ""
        Session("Department") = ""

        Response.Redirect("~\frmLogin.aspx")
    End Sub
End Class

