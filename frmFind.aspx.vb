
Partial Class frmFind
    Inherits System.Web.UI.Page

    Dim VisitReportCtrl As New clsVisitReport()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session("EmployeeID") = "" Then
            Response.Redirect("~\frmLogin.aspx")
        End If

        If Not IsPostBack Then
            txtAgreementNo.Text = ""

            txtStartDate.Text = Convert.ToDateTime(Now.Month.ToString() & "/01/" & Now.Year.ToString()).ToString("MM/dd/yyyy")
            txtEndDate.Text = Now.Date.ToString("MM/dd/yyyy")
        End If
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btnFetchInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFetchInfo.Click
        'If txtAgreementNo.Text <> "" Then

        'Else
        '    MessageBox("Agreement No Required.")
        '    Exit Sub
        'End If
        FindVisitReports()
    End Sub

    Protected Sub FindVisitReports()
        Dim StartDate, EndDate As DateTime

        If txtStartDate.Text = "" Then
            StartDate = Convert.ToDateTime(Now.Month.ToString() & "/01/" & Now.Year.ToString())
        Else
            StartDate = Convert.ToDateTime(txtStartDate.Text)
        End If

        If txtEndDate.Text = "" Then
            EndDate = Now.Date.ToString()
        Else
            EndDate = Convert.ToDateTime(txtEndDate.Text)
        End If

        grdMyVisitReports.DataSource = VisitReportCtrl.fnFindVisitReports(txtAgreementNo.Text, StartDate, EndDate)
        grdMyVisitReports.DataBind()
    End Sub

End Class
