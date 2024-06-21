
Partial Class frmWaiting
    Inherits System.Web.UI.Page

    Dim VisitReportCtrl As New clsVisitReport()

    Protected Sub GetVisitReports()
        grdWaitingList.DataSource = VisitReportCtrl.fnGetWaitingApprovalVisits(Session("EmployeeID"))
        grdWaitingList.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("EmployeeID") = "" Then
                Response.Redirect("~\frmLogin.aspx")
            End If
            GetVisitReports()
        End If
    End Sub

End Class
