
Partial Class frmPending
    Inherits System.Web.UI.Page

    Dim VisitReportCtrl As New clsVisitReport()

    Protected Sub GetVisitReports()
        grdMyVisitReports.DataSource = VisitReportCtrl.fnGetPendingVisits(Session("EmployeeID"))
        grdMyVisitReports.DataBind()
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

    Protected Sub grdMyVisitReports_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdMyVisitReports.RowDeleting
        Try
            Dim lblReportID As New Label
            Dim result As New clsResult()
            lblReportID = grdMyVisitReports.Rows(e.RowIndex).FindControl("lblReportID")

            result = VisitReportCtrl.fnDeleteVisitReport(lblReportID.Text)
            GetVisitReports()
            MessageBox(result.Message)
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

End Class
