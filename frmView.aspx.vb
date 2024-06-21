Imports System.Data

Partial Class frmView
    Inherits System.Web.UI.Page

    Dim VisitReportCtrl As New clsVisitReport()

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim ReportID As String = Request.QueryString("ReportID")
            GetReportDetails(ReportID)
            GetRelatedPartyByReportID(ReportID)
            GetDocumentsByReportID(ReportID)
            GetContactsByReportID(ReportID)
            GetVisitPhotos(ReportID)
            GetVisitAgrSummaryByReportID(ReportID)
            GetVisitPaymentStatusByReportID(ReportID)
        End If
    End Sub

    Protected Sub GetVisitPhotos(ByVal ReportID As String)


        Dim dtDocuments As DataTable = New DataTable()
        Dim i As Integer = 0
        dtDocuments = VisitReportCtrl.fnGetDocumentsByReportID(ReportID).Tables(0)

        For Each row As DataRow In dtDocuments.Rows
            If row.Item("DocumentType") = "Visit Photo" Then
                AddControl("img_" & i.ToString(), "~/Attachments/" & row.Item("Attachment"))
                i = i + 1
            End If
        Next row
    End Sub

    Protected Sub AddControl(ByVal ID As String, ByVal Link As String)
        Dim img As New Image()
        img.ID = ID
        img.Height = "300"
        img.BorderStyle = BorderStyle.Outset
        img.ImageUrl = Link
        pnlParameters.Controls.Add(img)

        Dim lbl As New Label()
        lbl.Text = " "
        pnlParameters.Controls.Add(lbl)

    End Sub

    Protected Sub GetRelatedPartyByReportID(ByVal ReportID As String)
        Dim dtRelatedParty As DataTable = New DataTable()
        dtRelatedParty = VisitReportCtrl.fnGetVisitRelatedPartyByReportID(ReportID).Tables(0)
        grdRelatedParty.DataSource = dtRelatedParty
        grdRelatedParty.DataBind()
    End Sub

    Protected Sub GetDocumentsByReportID(ByVal ReportID As String)
        Dim dtDocuments As DataTable = New DataTable()
        dtDocuments = VisitReportCtrl.fnGetDocumentsByReportID(ReportID).Tables(0)
        grdDocuments.DataSource = dtDocuments
        grdDocuments.DataBind()
    End Sub

    Protected Sub GetContactsByReportID(ByVal ReportID As String)
        Dim dtContacts As DataTable = New DataTable()
        dtContacts = VisitReportCtrl.fnGetContactsByReportID(ReportID).Tables(0)
        grdContacts.DataSource = dtContacts
        grdContacts.DataBind()
    End Sub

    Protected Sub GetReportDetails(ByVal ReportID As String)
        Try
            Dim VisitReport As clsVisitReport = VisitReportCtrl.fnGetReportDetails(ReportID)

            lblBorrowerName.Text = VisitReport.BorrowerName
            lblRegOfficeAddress.Text = VisitReport.RegisteredOffice
            lblFactoryAddress.Text = VisitReport.FactoryAddress
            lblAgreementNo.Text = VisitReport.AgreementID
            lblProductType.Text = VisitReport.ProductType
            lblPurposeOfLoan.Text = VisitReport.PurposeOfLoan
            lblSecurity.Text = VisitReport.Security
            lblBusinessNature.Text = VisitReport.BusinessNature
            lblAsOnDate.Text = " [ " & VisitReport.AsOnDate & " ]"
            lblPaymentBehavior.Text = VisitReport.PaymentBehavior
            lblCurrentStock.Text = Format(VisitReport.CurrentStock, "Standard")
            lblLiabilityPosition.Text = Format(VisitReport.LiabilityPosition, "Standard")
            lblWarehouse.Text = VisitReport.Warehouse
            lblRemarks.Text = VisitReport.Remarks

            lblCapacityUtilization.Text = VisitReport.CapacityUtilization
            lblBusinessExpansion.Text = VisitReport.BusinessExpansion
            lblTurnoverGrowth.Text = VisitReport.TurnoverGrowth
            lblEmployeeName.Text = VisitReport.EmployeeName
            lblDesignation.Text = VisitReport.Designation
            lblDepartment.Text = VisitReport.Department
            lblVisitDate.Text = VisitReport.SubmissionDate
            'lblFinalScore.Text = Format(VisitReport.FinalScore, "Standard")
            lblSupervisor.Text = VisitReport.Supervisor
            lblSupervisorDesignation.Text = VisitReport.SupervisorDesignation
            lblSupervisorDepartment.Text = VisitReport.SupervisorDepartment
            lblVisitDateFooter.Text = VisitReport.SubmissionDate
            lblReviewerRemarks.Text = VisitReport.ApproverRemarks
            imgVisitBy.ImageUrl = "~/Sources/signature/" & VisitReport.imgVisitBy

            If VisitReport.IsApproved = True Then
                lblApprovedBy.Text = "Approved By"
                lblApprovalDate.Text = "Approval Date"
                lblApprovalDateFooter.Text = VisitReport.ApprovalDate
                imgApprovedBy.ImageUrl = "~/Sources/signature/" & VisitReport.imgApprovedBy

            End If

            If VisitReport.IsRejected = True Then
                lblApprovedBy.Text = "Rejected By"
                lblApprovalDate.Text = "Rejection Date"
                lblApprovalDateFooter.Text = VisitReport.ApprovalDate
            End If

            If VisitReport.IsApproved = False And VisitReport.IsRejected = False Then
                lblApprovedBy.Text = "Yet to Approved By"
                lblApprovalDate.Text = ""
            End If

        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub GetVisitAgrSummaryByReportID(ByVal ReportID As String)
        grdAgrSummary.DataSource = VisitReportCtrl.fnGetVisitAgrSummaryByReportID(ReportID)
        grdAgrSummary.DataBind()
    End Sub

    Protected Sub GetVisitPaymentStatusByReportID(ByVal ReportID As String)
        grdPaymentStatus.DataSource = VisitReportCtrl.fnGetVisitPaymentStatusByReportID(ReportID)
        grdPaymentStatus.DataBind()
    End Sub

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub
End Class
