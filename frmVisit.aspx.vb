Imports System.Data
Imports System.Net.Mail

Partial Class frmVisit
    Inherits System.Web.UI.Page

    Dim agrCtrl As New clsAgreement()
    Dim VisitReportCtrl As New clsVisitReport()
    Dim CommonCtrl As New clsCommon()

    Protected Sub btnFetchInfo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFetchInfo.Click
        GetAgrInfo(txtAgreementNo.Text)
        btnFetchInfo.Enabled = False
    End Sub

    Protected Sub GetReportDetails(ByVal ReportID As String)
        Try
            Dim VisitReport As clsVisitReport = VisitReportCtrl.fnGetReportDetails(ReportID)

            lblBorrowerName.Text = VisitReport.BorrowerName
            lblRegOfficeAddress.Text = VisitReport.RegisteredOffice
            lblFactoryAddress.Text = VisitReport.FactoryAddress
            lblAgreementNo.Text = VisitReport.AgreementID
            txtAgreementNo.Text = VisitReport.AgreementID
            lblProductType.Text = VisitReport.ProductType
            lblPurposeOfLoan.Text = VisitReport.PurposeOfLoan
            lblSecurity.Text = VisitReport.Security
            lblAsOnDate.Text = VisitReport.AsOnDate
            drpBusinessNature.SelectedValue = VisitReport.BusinessNature
            CallBusinessNatureChange(VisitReport.BusinessNature)
            drpPaymentBehavior.SelectedValue = VisitReport.PaymentBehaviorID
            txtCurrentStock.Text = VisitReport.CurrentStock
            txtLiabilityPosition.Text = VisitReport.LiabilityPosition
            txtWarehouse.Text = VisitReport.Warehouse
            txtRemarks.Text = VisitReport.Remarks
            drpCapacityUtilization.SelectedValue = VisitReport.CapacityUtilizationID
            drpBusinessExpansion.SelectedValue = VisitReport.BusinessExpansionID
            drpTurnoverGrowth.SelectedValue = VisitReport.TurnoverGrowthID

            BlockAllInfo()
            
        Catch ex As Exception
            MessageBox(ex.Message)
            lblErrorMessage.Text = ex.Message
        End Try
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
        Session("dtDocuments") = dtDocuments
        grdDocuments.DataSource = dtDocuments
        grdDocuments.DataBind()
    End Sub

    Protected Sub GetContactsByReportID(ByVal ReportID As String)
        Dim dtContacts As DataTable = New DataTable()
        dtContacts = VisitReportCtrl.fnGetContactsByReportID(ReportID).Tables(0)
        Session("dtContacts") = dtContacts
        grdContacts.DataSource = dtContacts
        grdContacts.DataBind()
    End Sub

    Protected Sub GetVisitAgrSummaryByReportID(ByVal ReportID As String)
        grdAgrSummary.DataSource = VisitReportCtrl.fnGetVisitAgrSummaryByReportID(ReportID)
        grdAgrSummary.DataBind()
    End Sub

    Protected Sub GetVisitPaymentStatusByReportID(ByVal ReportID As String)
        grdPaymentStatus.DataSource = VisitReportCtrl.fnGetVisitPaymentStatusByReportID(ReportID)
        grdPaymentStatus.DataBind()
    End Sub

    Protected Sub BlockAllInfo()
        txtAgreementNo.Enabled = False
        btnFetchInfo.Enabled = False
        btnTemporarySave.Enabled = True
        btnSubmit.Enabled = True
    End Sub

    Protected Sub GetAgrSummary(ByVal AgreementNo As String)
        Try
            grdAgrSummary.DataSource = agrCtrl.fnGetAgrSummaryByAgrNo(AgreementNo)
            grdAgrSummary.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub GetAgrPaymentStatus(ByVal AgreementNo As String)
        Try
            grdPaymentStatus.DataSource = agrCtrl.fnGetAgrPaymentStatusByAgrNo(AgreementNo)
            grdPaymentStatus.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub GetAgrInfo(ByVal AgreementID As String)
        Try
            If txtAgreementNo.Text = "" Then
                MessageBox("Input Agreement No.")
                Exit Sub
            End If
            Dim agr As clsAgreement = agrCtrl.fnGetAgreementInfoByID(txtAgreementNo.Text)
            'lblAgrInfo.Text = agr.AgreementID & " " & agr.CustomerName & " "

            If agr.CustomerName <> "" Then

                lblBorrowerName.Text = agr.CustomerName
                lblRegOfficeAddress.Text = agr.RegisteredOffice
                lblFactoryAddress.Text = agr.FactoryAddress
                lblAgreementNo.Text = agr.AgreementID
                lblProductType.Text = agr.ProductName
                lblPurposeOfLoan.Text = agr.LoanCategory
                lblSecurity.Text = agr.Security
                lblAsOnDate.Text = agr.AsOnDate
                GetRelatedParty(agr.AgreementID)
                GetAgrSummary(agr.AgreementNo)
                GetAgrPaymentStatus(agr.AgreementNo)
                txtAgreementNo.Enabled = False
                btnTemporarySave.Enabled = True
                btnSubmit.Enabled = True
                lblErrorMessage.Text = ""
                lblAgrInfo.Text = ""
            Else
                MessageBox("Invalid AgreementNo/ID")
                lblErrorMessage.Text = "Invalid AgreementNo/ID"
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
            lblErrorMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub GetRelatedParty(ByVal AgreementID As String)
        grdRelatedParty.DataSource = agrCtrl.fnGetRelatedParty(AgreementID)
        grdRelatedParty.DataBind()
    End Sub

    Protected Sub Page_Error(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Error
        Dim objErr As Exception = Server.GetLastError().GetBaseException()
        Dim err As String = "<b>Error Caught in Page_Error event</b><hr><br>" & _
                            "<br><b>Error in: </b>" & Request.Url.ToString() & _
                            "<br><b>Error Message: </b>" & objErr.Message.ToString() & _
                            "<br><b>Stack Trace:</b><br>" & objErr.StackTrace.ToString()
        Response.Write(err.ToString())
        Server.ClearError()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not IsPostBack Then

            If Session("EmployeeID") = "" Then
                Response.Redirect("~\frmLogin.aspx")
            End If

            Dim ReportID As String = Request.QueryString("ReportID")

            If ReportID = "0" Or ReportID Is Nothing Then

                'Generate Report ID
                ReportID = VisitReportCtrl.fnGenVisitReportID()

                Dim dtRelatedParty As DataTable = New DataTable()
                dtRelatedParty = FormatRelatedPartyTable()
                Session("dtRelatedParty") = dtRelatedParty

                Dim dtContacts As DataTable = New DataTable()
                dtContacts = FormatContactTable()
                Session("dtContacts") = dtContacts

                Dim dtDocuments As DataTable = New DataTable()
                dtDocuments = FormatDocumentTable()
                Session("dtDocuments") = dtDocuments

                btnTemporarySave.Enabled = False
                btnSubmit.Enabled = False
                btnFetchInfo.Enabled = True
                CallBusinessNatureChange(drpBusinessNature.SelectedValue)

            Else
                GetReportDetails(ReportID)
                GetRelatedPartyByReportID(ReportID)
                GetDocumentsByReportID(ReportID)
                GetContactsByReportID(ReportID)
                GetVisitAgrSummaryByReportID(ReportID)
                GetVisitPaymentStatusByReportID(ReportID)

            End If
            ListEmployees()
            lblReportID.Text = ReportID.ToString()
        End If
    End Sub

    Protected Sub ListEmployees()
        drpEmployees.DataSource = CommonCtrl.fnListEmployees()
        drpEmployees.DataTextField = "EmployeeName"
        drpEmployees.DataValueField = "EmployeeID"
        drpEmployees.DataBind()
    End Sub

    Protected Function FormatRelatedPartyTable() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("RelatedPartyID", System.Type.GetType("System.String"))
        dt.Columns.Add("ReportID", System.Type.GetType("System.String"))
        dt.Columns.Add("CustomerID", System.Type.GetType("System.String"))
        dt.Columns.Add("Role", System.Type.GetType("System.String"))
        dt.Columns.Add("RelatedPerson", System.Type.GetType("System.String"))
        dt.Columns.Add("Relation", System.Type.GetType("System.String"))
        Return dt
    End Function

    Protected Function FormatContactTable() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("ContactPerson", System.Type.GetType("System.String"))
        dt.Columns.Add("Relation", System.Type.GetType("System.String"))
        dt.Columns.Add("ContactNo", System.Type.GetType("System.String"))
        dt.Columns.Add("VisitingOfficial", System.Type.GetType("System.String"))
        dt.Columns.Add("VisitingOfficialID", System.Type.GetType("System.String"))
        dt.Columns.Add("VisitingDate", System.Type.GetType("System.String"))
        Return dt
    End Function

    Protected Function FormatDocumentTable() As DataTable
        Dim dt As DataTable = New DataTable()
        dt.Columns.Add("DocumentName", System.Type.GetType("System.String"))
        dt.Columns.Add("DocumentType", System.Type.GetType("System.String"))
        dt.Columns.Add("Attachment", System.Type.GetType("System.String"))
        Return dt
    End Function

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    Protected Sub btnAddDocument_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddDocument.Click
        Try
            Dim folder As String = ""
            Dim Title As String = ""
            Dim DocExt As String = ""
            Dim DocFullName As String = ""
            Dim DocPrefix As String = ""
            Dim FileSize As Integer = 0
            Dim DocFileName As String = ""

            If txtAgreementNo.Text = "" Then
                MessageBox("No Agreement Is Selected.")
                Exit Sub
            End If

            If txtDocumentName.Text = "" Then
                MessageBox("Document Name Required")
                Exit Sub
            End If

            Dim doc As New clsDocument()

            doc.DocumentName = txtDocumentName.Text
            doc.DocumentType = drpDocType.SelectedValue

            If flupDocument.HasFile Then

                folder = ConfigurationManager.AppSettings("storage")
                FileSize = flupDocument.PostedFile.ContentLength()
                If FileSize > 20971529 Then
                    MessageBox("File size should be within 20MB : Current Size is -> " & FileSize.ToString())
                    Exit Sub
                End If
                DocExt = System.IO.Path.GetExtension(flupDocument.FileName)

                If drpDocType.SelectedValue = "Visit Photo" Then
                    If DocExt <> ".png" And DocExt <> ".PNG" And DocExt <> ".jpg" And DocExt <> ".JPG" And DocExt <> ".jpeg" And DocExt <> ".JPEG" And DocExt <> ".gif" And DocExt <> ".GIF" Then
                        MessageBox("for Visit picture you should attach image files.")
                        Exit Sub
                    End If
                End If

                DocFileName = "DOC_" & DateTime.Now.ToString("ddMMyyHHmmss") & DocExt
                flupDocument.SaveAs(Server.MapPath(folder) & DocFileName)
                doc.Attachment = DocFileName
            Else
                MessageBox("Attachment Missing")
                Exit Sub
            End If

            Dim dtDocuments As DataTable = New DataTable()
            dtDocuments = AddDocument(doc)
            Session("dtDocuments") = dtDocuments

            grdDocuments.DataSource = dtDocuments
            grdDocuments.DataBind()

            ClearDocumentSelection()

            If grdDocuments.Rows.Count > 0 Then
                btnSubmit.Enabled = True
            Else
                btnSubmit.Enabled = False
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub ClearDocumentSelection()
        txtDocumentName.Text = ""
        drpDocType.SelectedIndex = -1
    End Sub

    Protected Sub ClearContactSelection()
        txtContactPerson.Text = ""
        txtRelation.Text = ""
        txtContactNo.Text = ""
        drpEmployees.SelectedIndex = -1
        txtVisitDate.Text = ""
    End Sub

    Protected Sub btnAddContact_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddContact.Click
        Try
            If txtAgreementNo.Text = "" Then
                MessageBox("No Agreement Is Selected.")
                Exit Sub
            End If

            If txtContactPerson.Text = "" Or txtRelation.Text = "" Or txtContactNo.Text = "" Or txtVisitDate.Text = "" Then
                MessageBox("All input fields are mandatory")
                Exit Sub
            End If

            Dim contact As New clsContact()

            contact.ContactPerson = txtContactPerson.Text
            contact.Relation = txtRelation.Text
            contact.ContactNo = txtContactNo.Text
            contact.VisitingOfficial = drpEmployees.SelectedItem.Text
            contact.VisitingOfficialID = drpEmployees.SelectedValue
            contact.VisitingDate = txtVisitDate.Text

            Dim dtContacts As DataTable = New DataTable()
            dtContacts = AddContact(contact)
            Session("dtContacts") = dtContacts

            grdContacts.DataSource = dtContacts
            grdContacts.DataBind()

            ClearContactSelection()

            If grdContacts.Rows.Count > 0 Then
                btnSubmit.Enabled = True
            Else
                btnSubmit.Enabled = False
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Function AddDocument(ByVal Doc As clsDocument) As DataTable

        Dim dtDocuments As DataTable = New DataTable()
        dtDocuments = Session("dtDocuments")

        Dim dr As DataRow
        dr = dtDocuments.NewRow()
        dr("DocumentName") = Doc.DocumentName
        dr("DocumentType") = Doc.DocumentType
        dr("Attachment") = Doc.Attachment

        dtDocuments.Rows.Add(dr)
        dtDocuments.AcceptChanges()
        Return dtDocuments

    End Function

    Protected Function AddContact(ByVal contact As clsContact) As DataTable
        Try
            Dim dtContacts As DataTable = New DataTable()
            dtContacts = Session("dtContacts")

            Dim dr As DataRow
            dr = dtContacts.NewRow()
            dr("ContactPerson") = contact.ContactPerson
            dr("Relation") = contact.Relation
            dr("ContactNo") = contact.ContactNo
            dr("VisitingOfficial") = contact.VisitingOfficial
            dr("VisitingOfficialID") = contact.VisitingOfficialID
            dr("VisitingDate") = contact.VisitingDate

            dtContacts.Rows.Add(dr)
            dtContacts.AcceptChanges()
            Return dtContacts
        Catch ex As Exception
            MessageBox(ex.Message)
            Return Nothing
        End Try
    End Function

    Protected Sub grdDocuments_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdDocuments.RowDeleting
        Try
            Dim i As Integer
            Dim dtDocuments As DataTable = New DataTable()

            dtDocuments = Session("dtDocuments")

            i = e.RowIndex

            dtDocuments.Rows(i).Delete()
            dtDocuments.AcceptChanges()

            Session("dtDocuments") = dtDocuments

            grdDocuments.DataSource = dtDocuments
            grdDocuments.DataBind()

            If grdDocuments.Rows.Count > 0 Then
                btnSubmit.Enabled = True
            Else
                btnSubmit.Enabled = False
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim VisitReport As New clsVisitReport()
            Dim result As New clsResult()
            Dim AgrSummaryList As String = ""
            Dim PaymentStatusList As String = ""
            Dim RelatedPartyList As String = ""
            Dim ContactList As String = ""
            Dim DocumentList As String = ""
            Dim MailProp As New clsMailProperty()

            If grdContacts.Rows.Count = 0 Then
                MessageBox("No Contact Added.")
                Exit Sub
            End If

            If grdDocuments.Rows.Count = 0 Then
                MessageBox("No Document Added.")
                Exit Sub
            End If

            VisitReport.ReportID = lblReportID.Text
            VisitReport.AgreementID = txtAgreementNo.Text
            VisitReport.BusinessNature = drpBusinessNature.SelectedValue
            VisitReport.PaymentBehaviorID = drpPaymentBehavior.SelectedValue
            VisitReport.CurrentStock = txtCurrentStock.Text
            VisitReport.LiabilityPosition = txtLiabilityPosition.Text
            VisitReport.Warehouse = txtWarehouse.Text
            VisitReport.Remarks = txtRemarks.Text
            VisitReport.CapacityUtilizationID = drpCapacityUtilization.SelectedValue
            VisitReport.BusinessExpansionID = drpBusinessExpansion.SelectedValue
            VisitReport.TurnoverGrowthID = drpTurnoverGrowth.SelectedValue

            Dim slblAgreementID, slblDisbursementDt, sxlblDisbursementAmt, slblTenor As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdAgrSummary.Rows
                slblAgreementID = rw.FindControl("slblAgreementID")
                slblDisbursementDt = rw.FindControl("slblDisbursementDt")
                sxlblDisbursementAmt = rw.FindControl("sxlblDisbursementAmt")
                slblTenor = rw.FindControl("slblTenor")

                AgrSummaryList += slblAgreementID.Text & "~" & slblDisbursementDt.Text & "~" & sxlblDisbursementAmt.Text & "~" & slblTenor.Text & "~|"
            Next

            Dim plblAgreementID, plblInstallmentDue, plblInstallmentPaid, pxlblOutstandingAmnt, plblOverdueNo, plblOverdueAmnt As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdPaymentStatus.Rows
                plblAgreementID = rw.FindControl("plblAgreementID")
                plblInstallmentDue = rw.FindControl("plblInstallmentDue")
                plblInstallmentPaid = rw.FindControl("plblInstallmentPaid")
                pxlblOutstandingAmnt = rw.FindControl("pxlblOutstandingAmnt")
                plblOverdueNo = rw.FindControl("plblOverdueNo")
                plblOverdueAmnt = rw.FindControl("plblOverdueAmnt")

                PaymentStatusList += plblAgreementID.Text & "~" & plblInstallmentDue.Text & "~" & plblInstallmentPaid.Text & "~" & pxlblOutstandingAmnt.Text & "~" & plblOverdueNo.Text & "~" & plblOverdueAmnt.Text & "~|"
            Next

            Dim lblCustomerID, lblRole, lblRelatedPerson, lblRelatedRelation As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdRelatedParty.Rows
                lblCustomerID = rw.FindControl("lblCustomerID")
                lblRole = rw.FindControl("lblRole")
                lblRelatedPerson = rw.FindControl("lblRelatedPerson")
                lblRelatedRelation = rw.FindControl("lblRelatedRelation")

                RelatedPartyList += lblCustomerID.Text & "~" & lblRole.Text & "~" & lblRelatedPerson.Text & "~" & lblRelatedRelation.Text & "~|"
            Next

            Dim lblContactPerson, lblRelation, lblContactNo, lblVisitingOfficial, lblVisitingOfficialID, lblVisitingDate As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdContacts.Rows
                lblContactPerson = rw.FindControl("lblContactPerson")
                lblRelation = rw.FindControl("lblRelation")
                lblContactNo = rw.FindControl("lblContactNo")
                lblVisitingOfficial = rw.FindControl("lblVisitingOfficial")
                lblVisitingOfficialID = rw.FindControl("lblVisitingOfficialID")
                lblVisitingDate = rw.FindControl("lblVisitingDate")

                ContactList += lblContactPerson.Text & "~" & lblRelation.Text & "~" & lblContactNo.Text & "~" & lblVisitingOfficialID.Text & "~" & lblVisitingDate.Text & "~|"
            Next

            Dim lblDocumentName, lblDocumentType, lblAttachment As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdDocuments.Rows
                lblDocumentName = rw.FindControl("lblDocumentName")
                lblDocumentType = rw.FindControl("lblDocumentType")
                lblAttachment = rw.FindControl("lblAttachment")

                DocumentList += lblDocumentName.Text & "~" & lblDocumentType.Text & "~" & lblAttachment.Text & "~|"
            Next

            VisitReport.AgrSummaryList = AgrSummaryList
            VisitReport.PaymentStatusList = PaymentStatusList
            VisitReport.RelatedPartyList = RelatedPartyList
            VisitReport.ContactList = ContactList
            VisitReport.DocumentList = DocumentList
            VisitReport.IsSubmitted = True
            VisitReport.EntryBy = Session("EmployeeID")

            result = VisitReportCtrl.fnInsertVisitReport(VisitReport)

            MessageBox(result.Message)
            If result.Success = True Then
                MailProp = VisitReportCtrl.fnGetVisitMailInfo(lblReportID.Text, "Submitted")
                SendMail(MailProp)
                Clear()
            Else
                lblErrorMessage.Text = result.Message
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
            lblErrorMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub Clear()
        txtAgreementNo.Text = ""
        txtAgreementNo.Enabled = True
        lblAgrInfo.Text = ""
        lblBorrowerName.Text = ""
        lblAgreementNo.Text = ""
        lblPurposeOfLoan.Text = ""
        lblSecurity.Text = ""
        lblAsOnDate.Text = ""
        lblErrorMessage.Text = ""
        lblFactoryAddress.Text = ""
        lblProductType.Text = ""
        lblRegOfficeAddress.Text = ""
        drpPaymentBehavior.SelectedIndex = -1
        txtWarehouse.Text = ""

        Session("dtContacts") = ""
        Session("dtDocuments") = ""

        Dim dtContacts As DataTable = New DataTable()
        dtContacts = FormatContactTable()
        Session("dtContacts") = dtContacts

        Dim dtDocuments As DataTable = New DataTable()
        dtDocuments = FormatDocumentTable()
        Session("dtDocuments") = dtDocuments

        txtCurrentStock.Text = ""

        drpCapacityUtilization.SelectedIndex = -1
        drpBusinessExpansion.SelectedIndex = -1
        drpTurnoverGrowth.SelectedIndex = -1

        txtContactPerson.Text = ""
        txtRelation.Text = ""
        txtContactNo.Text = ""
        drpEmployees.SelectedIndex = -1
        txtVisitDate.Text = ""

        txtDocumentName.Text = ""
        drpDocType.SelectedIndex = -1

        grdRelatedParty.DataSource = Nothing
        grdRelatedParty.DataBind()

        grdContacts.DataSource = Nothing
        grdContacts.DataBind()

        grdDocuments.DataSource = Nothing
        grdDocuments.DataBind()

        grdAgrSummary.DataSource = Nothing
        grdAgrSummary.DataBind()

        grdPaymentStatus.DataSource = Nothing
        grdPaymentStatus.DataBind()

        'Generate Report ID
        lblReportID.Text = VisitReportCtrl.fnGenVisitReportID()

        btnFetchInfo.Enabled = True
        btnTemporarySave.Enabled = False
        btnSubmit.Enabled = False
    End Sub

    Protected Sub btnReload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnReload.Click
        Response.Redirect("~/frmVisit.aspx")
    End Sub

    Protected Sub grdContacts_RowDeleting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewDeleteEventArgs) Handles grdContacts.RowDeleting
        Try
            Dim i As Integer
            Dim dtContacts As DataTable = New DataTable()

            dtContacts = Session("dtContacts")

            i = e.RowIndex

            dtContacts.Rows(i).Delete()
            dtContacts.AcceptChanges()

            Session("dtContacts") = dtContacts

            grdContacts.DataSource = dtContacts
            grdContacts.DataBind()

            If grdContacts.Rows.Count > 0 Then
                btnSubmit.Enabled = True
            Else
                btnSubmit.Enabled = False
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub btnTemporarySave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTemporarySave.Click
        Try
            Dim VisitReport As New clsVisitReport()
            Dim result As New clsResult()
            Dim RelatedPartyList As String = ""
            Dim AgrSummaryList As String = ""
            Dim PaymentStatusList As String = ""
            Dim ContactList As String = ""
            Dim DocumentList As String = ""

            VisitReport.ReportID = lblReportID.Text
            VisitReport.AgreementID = txtAgreementNo.Text
            VisitReport.BusinessNature = drpBusinessNature.SelectedValue
            VisitReport.PaymentBehaviorID = drpPaymentBehavior.SelectedValue
            VisitReport.CurrentStock = txtCurrentStock.Text
            VisitReport.LiabilityPosition = txtLiabilityPosition.Text
            VisitReport.Warehouse = txtWarehouse.Text
            VisitReport.Remarks = txtRemarks.Text
            VisitReport.CapacityUtilizationID = drpCapacityUtilization.SelectedValue
            VisitReport.BusinessExpansionID = drpBusinessExpansion.SelectedValue
            VisitReport.TurnoverGrowthID = drpTurnoverGrowth.SelectedValue

            Dim slblAgreementID, slblDisbursementDt, sxlblDisbursementAmt, slblTenor As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdAgrSummary.Rows
                slblAgreementID = rw.FindControl("slblAgreementID")
                slblDisbursementDt = rw.FindControl("slblDisbursementDt")
                sxlblDisbursementAmt = rw.FindControl("sxlblDisbursementAmt")
                slblTenor = rw.FindControl("slblTenor")

                AgrSummaryList += slblAgreementID.Text & "~" & slblDisbursementDt.Text & "~" & sxlblDisbursementAmt.Text & "~" & slblTenor.Text & "~|"
            Next

            Dim plblAgreementID, plblInstallmentDue, plblInstallmentPaid, pxlblOutstandingAmnt, plblOverdueNo, plblOverdueAmnt As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdPaymentStatus.Rows
                plblAgreementID = rw.FindControl("plblAgreementID")
                plblInstallmentDue = rw.FindControl("plblInstallmentDue")
                plblInstallmentPaid = rw.FindControl("plblInstallmentPaid")
                pxlblOutstandingAmnt = rw.FindControl("pxlblOutstandingAmnt")
                plblOverdueNo = rw.FindControl("plblOverdueNo")
                plblOverdueAmnt = rw.FindControl("plblOverdueAmnt")

                PaymentStatusList += plblAgreementID.Text & "~" & plblInstallmentDue.Text & "~" & plblInstallmentPaid.Text & "~" & pxlblOutstandingAmnt.Text & "~" & plblOverdueNo.Text & "~" & plblOverdueAmnt.Text & "~|"
            Next

            Dim lblCustomerID, lblRole, lblRelatedPerson, lblRelatedRelation As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdRelatedParty.Rows
                lblCustomerID = rw.FindControl("lblCustomerID")
                lblRole = rw.FindControl("lblRole")
                lblRelatedPerson = rw.FindControl("lblRelatedPerson")
                lblRelatedRelation = rw.FindControl("lblRelatedRelation")

                RelatedPartyList += lblCustomerID.Text & "~" & lblRole.Text & "~" & lblRelatedPerson.Text & "~" & lblRelatedRelation.Text & "~|"
            Next

            Dim lblContactPerson, lblRelation, lblContactNo, lblVisitingOfficial, lblVisitingOfficialID, lblVisitingDate As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdContacts.Rows
                lblContactPerson = rw.FindControl("lblContactPerson")
                lblRelation = rw.FindControl("lblRelation")
                lblContactNo = rw.FindControl("lblContactNo")
                lblVisitingOfficialID = rw.FindControl("lblVisitingOfficialID")
                lblVisitingDate = rw.FindControl("lblVisitingDate")

                ContactList += lblContactPerson.Text & "~" & lblRelation.Text & "~" & lblContactNo.Text & "~" & lblVisitingOfficialID.Text & "~" & lblVisitingDate.Text & "~|"
            Next

            Dim lblDocumentName, lblDocumentType, lblAttachment As New System.Web.UI.WebControls.Label()
            For Each rw As GridViewRow In grdDocuments.Rows
                lblDocumentName = rw.FindControl("lblDocumentName")
                lblDocumentType = rw.FindControl("lblDocumentType")
                lblAttachment = rw.FindControl("lblAttachment")

                DocumentList += lblDocumentName.Text & "~" & lblDocumentType.Text & "~" & lblAttachment.Text & "~|"
            Next

            VisitReport.AgrSummaryList = AgrSummaryList
            VisitReport.PaymentStatusList = PaymentStatusList
            VisitReport.RelatedPartyList = RelatedPartyList
            VisitReport.ContactList = ContactList
            VisitReport.DocumentList = DocumentList
            VisitReport.IsSubmitted = False
            VisitReport.EntryBy = Session("EmployeeID")

            result = VisitReportCtrl.fnInsertVisitReport(VisitReport)

            If result.Success = True Then
                MessageBox("Visit Report Temporarily Saved.")
            Else
                MessageBox(result.Message)
                lblErrorMessage.Text = result.Message
            End If

        Catch ex As Exception
            MessageBox(ex.Message)
            lblErrorMessage.Text = ex.Message
        End Try
    End Sub

    Protected Sub drpBusinessNature_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles drpBusinessNature.SelectedIndexChanged
        Try
            CallBusinessNatureChange(drpBusinessNature.SelectedValue)
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub CallBusinessNatureChange(ByVal BusinessNature As String)
        LoadPaymentBehavior("PaymentBehavior", BusinessNature)
        LoadCapcityUtilizationList("CapacityUtilization", BusinessNature)
        LoadBusinessExpansionList("BusinessExpansion", BusinessNature)
        LoadTurnoverGrowthList("TurnoverGrowth", BusinessNature)
    End Sub

    Protected Sub LoadPaymentBehavior(ByVal BusinessArea As String, ByVal BusinessNature As String)
        Try
            drpPaymentBehavior.DataSource = CommonCtrl.fnListWeightValues(BusinessArea, BusinessNature)
            drpPaymentBehavior.DataTextField = "WeightItem"
            drpPaymentBehavior.DataValueField = "WeightMatrixID"
            drpPaymentBehavior.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub LoadCapcityUtilizationList(ByVal BusinessArea As String, ByVal BusinessNature As String)
        Try
            drpCapacityUtilization.DataSource = CommonCtrl.fnListWeightValues(BusinessArea, BusinessNature)
            drpCapacityUtilization.DataTextField = "WeightItem"
            drpCapacityUtilization.DataValueField = "WeightMatrixID"
            drpCapacityUtilization.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub LoadBusinessExpansionList(ByVal BusinessArea As String, ByVal BusinessNature As String)
        Try
            drpBusinessExpansion.DataSource = CommonCtrl.fnListWeightValues(BusinessArea, BusinessNature)
            drpBusinessExpansion.DataTextField = "WeightItem"
            drpBusinessExpansion.DataValueField = "WeightMatrixID"
            drpBusinessExpansion.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub LoadTurnoverGrowthList(ByVal BusinessArea As String, ByVal BusinessNature As String)
        Try
            drpTurnoverGrowth.DataSource = CommonCtrl.fnListWeightValues(BusinessArea, BusinessNature)
            drpTurnoverGrowth.DataTextField = "WeightItem"
            drpTurnoverGrowth.DataValueField = "WeightMatrixID"
            drpTurnoverGrowth.DataBind()
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

    Protected Sub rdBtnAddress_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdBtnAddress.SelectedIndexChanged
        If rdBtnAddress.SelectedValue = "Registered Address" Then
            txtWarehouse.Text = lblRegOfficeAddress.Text
            txtWarehouse.ReadOnly = True
        ElseIf rdBtnAddress.SelectedValue = "Factory Address" Then
            txtWarehouse.Text = lblFactoryAddress.Text
            txtWarehouse.ReadOnly = True
        Else
            txtWarehouse.Text = ""
            txtWarehouse.ReadOnly = False
        End If
    End Sub

    Protected Sub SendMail(ByVal MailProp As clsMailProperty)
        Dim mail As New Net.Mail.MailMessage()
        Dim TestArray() As String

        Try
            mail.From = New MailAddress(MailProp.MailFrom)

            TestArray = Split(MailProp.MailTo, ";")
            For i As Integer = 0 To TestArray.Length - 1
                If TestArray(i) <> "" Then
                    mail.To.Add(TestArray(i))
                End If
            Next
            TestArray = Nothing

            TestArray = Split(MailProp.MailCC, ";")
            For i As Integer = 0 To TestArray.Length - 1
                If TestArray(i) <> "" Then
                    mail.CC.Add(TestArray(i))
                End If
            Next
            TestArray = Nothing

            TestArray = Split(MailProp.MailBCC, ";")
            For i As Integer = 0 To TestArray.Length - 1
                If TestArray(i) <> "" Then
                    mail.Bcc.Add(TestArray(i))
                End If
            Next
            TestArray = Nothing

            mail.Subject = MailProp.MailSubject
            mail.Body = MailProp.MailBody
            mail.IsBodyHtml = True
            mail.Priority = MailPriority.Normal
            Dim smtp As New SmtpClient("192.168.1.15", 25)
            smtp.Send(mail)
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

End Class
