
Partial Class frmMyVisit
    Inherits System.Web.UI.Page

    Dim VisitReportCtrl As New clsVisitReport()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            GetVisitReports()
        End If
    End Sub

    Protected Sub GetVisitReports()
        grdMyVisitReports.DataSource = VisitReportCtrl.fnGetVisitReports(Session("EmployeeID"))
        grdMyVisitReports.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

End Class
