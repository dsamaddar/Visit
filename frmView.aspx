<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmView.aspx.vb" Inherits="frmView"
    Title="Periodic Visit: View Report" MaintainScrollPositionOnPostback="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Visit Report</title>
    <!-- Scripts -->

    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js" integrity="sha384-KJ3o2DKtIkvYIK3UENzmM7KCkRr/rE9/Qpg6aAZGJwFDMVNA/GpGFF93hXpG5KkN"
        crossorigin="anonymous"></script>

    <script src="https://cdn.jsdelivr.net/npm/popper.js@1.12.9/dist/umd/popper.min.js"
        integrity="sha384-ApNbgh9B+Y1QKtv3Rn7W3mgPxhU9K/ScQsAP7hUibX39j7fakFPskvXusvfa0b4Q"
        crossorigin="anonymous"></script>

    <script src="https://cdn.jsdelivr.net/npm/bootstrap@4.0.0/dist/js/bootstrap.min.js"
        integrity="sha384-JZR6Spejh4U02d8jOt6vLEHfe/JQGiRRSQQxSfFWpi1MquVdAyjUar5+76PVCmYl"
        crossorigin="anonymous"></script>

    <!-- Fonts -->
    <link rel="dns-prefetch" href="//fonts.gstatic.com">
    <link href="https://fonts.googleapis.com/css?family=Nunito" rel="stylesheet">
    <!-- Styles -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css"
        rel="stylesheet" integrity="sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T"
        crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />
    <style>
        @media print
        {
            body
            {
                -webkit-print-color-adjust: exact;
                margin: none;
                transform: scale(.95);
            }
            page
            {
                size: portrait;
                margin: 10mm 10mm 20mm 10mm;
                mso-header-margin: 20mm;
                mso-footer-margin: 20mm;
                mso-paper-source: 0;
            }
            table
            {
                page-break-inside: avoid;
            }
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="container container-sm container-md container-lg text-left">
        <div class="row">
            <div class="col-md-12">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 30%">
                            <h4>
                                PERIODIC VISIT REPORT
                            </h4>
                        </td>
                        <td style="width: 5%">
                        </td>
                        <td align="right" style="width: 65%">
                            <img src="Sources/images/meridian_logo.png" height="60px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblFinalScore" runat="server" Text="" ForeColor="#009900"></asp:Label>--%>
                        </td>
                        <td align="right">
                        </td>
                        <td align="right" colspan="3">
                            <h6>
                                Visit Date :
                                <asp:Label ID="lblVisitDate" runat="server" Text=""></asp:Label>
                            </h6>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <h6 class="card-header bg-primary text-white">
                                Key Information of the facility</h6>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Name of the borrower
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblBorrowerName" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Registered Office Address
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblRegOfficeAddress" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Factory Address
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblFactoryAddress" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            AgreementNo
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblAgreementNo" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Product Type
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblProductType" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Purpose of Loan
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblPurposeOfLoan" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Security (Cash/FDR/LD)
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblSecurity" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Business Nature
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:Label ID="lblBusinessNature" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-12">
                <br />
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        All Related Agreements</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdAgrSummary" CssClass="table table-sm" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="AgreementID">
                                                <ItemTemplate>
                                                    <asp:Label ID="slblAgreementID" runat="server" Text='<%# Bind("AgreementID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DisbursementDt">
                                                <ItemTemplate>
                                                    <asp:Label ID="slblDisbursementDt" runat="server" Text='<%# Eval("DisbursementDt", "{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DisbursementAmt">
                                                <ItemTemplate>
                                                    <asp:Label ID="slblDisbursementAmt" runat="server" Text='<%# Eval("DisbursementAmt","{0:N2}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DisbursementAmt" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="sxlblDisbursementAmt" runat="server" Text='<%# Bind("DisbursementAmt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Tenor">
                                                <ItemTemplate>
                                                    <asp:Label ID="slblTenor" runat="server" Text='<%# Bind("Tenor") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <br />
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Co-Applicant/Guarantor Details</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grdRelatedParty" runat="server" AutoGenerateColumns="False" EmptyDataText="No Co-Applicant/Guarantor Available"
                                    class="table table-sm">
                                    <Columns>
                                        <asp:TemplateField HeaderText="CustomerID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCustomerID" runat="server" Text='<%# Bind("CustomerID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Role">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRole" runat="server" Text='<%# Bind("Role") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Related Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRelatedPerson" runat="server" Text='<%# Bind("RelatedPerson") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Relation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRelatedRelation" runat="server" Text='<%# Bind("Relation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <br />
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        All Related Payment Status As on
                        <asp:Label ID="lblAsOnDate" runat="server" Text=""></asp:Label>
                    </h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdPaymentStatus" CssClass="table table-sm" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="AgreementID">
                                                <ItemTemplate>
                                                    <asp:Label ID="plblAgreementID" runat="server" Text='<%# Bind("AgreementID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="InstallmentDue">
                                                <ItemTemplate>
                                                    <asp:Label ID="plblInstallmentDue" runat="server" Text='<%# Bind("InstallmentDue") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="InstallmentPaid">
                                                <ItemTemplate>
                                                    <asp:Label ID="plblInstallmentPaid" runat="server" Text='<%# Bind("InstallmentPaid") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OutstandingAmnt">
                                                <ItemTemplate>
                                                    <asp:Label ID="plblOutstandingAmnt" runat="server" Text='<%# Eval("OutstandingAmnt","{0:N2}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OutstandingAmnt" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="pxlblOutstandingAmnt" runat="server" Text='<%# Bind("OutstandingAmnt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OverdueNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="plblOverdueNo" runat="server" Text='<%# Bind("OverdueNo") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OverdueAmnt">
                                                <ItemTemplate>
                                                    <asp:Label ID="plblOverdueAmnt" runat="server" Text='<%# Bind("OverdueAmnt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Visit Observation</h6>
                    <div class="card-body">
                        <h6 class="card-title">
                        </h6>
                        <div class="row border">
                            <div class="col-md-3 border border-secondary">
                                <label for="lblPaymentBehavior" class="form-label">
                                    Payment Behavior :
                                </label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <asp:Label ID="lblPaymentBehavior" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblTurnoverGrowth" class="form-label">
                                    Turnover Growth :
                                </label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <asp:Label ID="lblTurnoverGrowth" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblCapacityUtilization" class="form-label">
                                    Capacity Utilization :
                                </label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <asp:Label ID="lblCapacityUtilization" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblBusinessExpansion" class="form-label">
                                    Business Expansion :
                                </label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <asp:Label ID="lblBusinessExpansion" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblCurrentStock" class="form-label">
                                    Current Stock :
                                </label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <asp:Label ID="lblCurrentStock" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblLiabilityPosition" class="form-label">
                                    Liability Position :
                                </label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <asp:Label ID="lblLiabilityPosition" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblWarehouse" class="form-label">
                                    Warehouse Address:
                                </label>
                            </div>
                            <div class="col-md-9 border border-secondary">
                                <asp:Label ID="lblWarehouse" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-3 border border-secondary">
                                <label for="lblRemarks" class="form-label">
                                    Remarks:
                                </label>
                            </div>
                            <div class="col-md-9 border border-secondary">
                                <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Personnel during visit</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:GridView ID="grdContacts" CssClass="table table-sm" runat="server" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Contact Person">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactPerson" runat="server" Text='<%# Bind("ContactPerson") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Relation">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRelation" runat="server" Text='<%# Bind("Relation") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Contact No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblContactNo" runat="server" Text='<%# Bind("ContactNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Visiting Official">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVisitingOfficial" runat="server" Text='<%# Bind("VisitingOfficial") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="VisitingDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblVisitingDate" runat="server" Text='<%# Bind("VisitingDate") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Visit Pictures</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <asp:Panel ID="pnlParameters" runat="server" SkinID="pnlInner">
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Attachments</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdDocuments" runat="server" AutoGenerateColumns="False" CssClass="table table-sm">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Title">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocumentName" runat="server" Text='<%# Bind("DocumentName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Type">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDocumentType" runat="server" Text='<%# Bind("DocumentType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Attachment">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" Text='<%# Bind("Attachment") %>'
                                                        NavigateUrl='<%# Eval("Attachment","~/Attachments/{0}") %>'></asp:HyperLink>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Approval Section</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="height: 60px; width: 15%">
                                            <b>Visit By</b>
                                        </td>
                                        <td style="width: 35%">
                                        </td>
                                        <td style="width: 15%">
                                            <b>
                                                <asp:Label ID="lblApprovedBy" runat="server" Text=""></asp:Label></b>
                                        </td>
                                        <td style="width: 35%">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 60px; width: 50%" colspan="2">
                                            <asp:Image ID="imgVisitBy" runat="server" Height="50px" />
                                        </td>
                                        <td style="height: 60px; width: 50%" colspan="2">
                                            <asp:Image ID="imgApprovedBy" runat="server" Height="50px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Name
                                        </td>
                                        <td>
                                            <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            Name
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSupervisor" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Designation
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            Designation
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSupervisorDesignation" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Department
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            Department
                                        </td>
                                        <td>
                                            <asp:Label ID="lblSupervisorDepartment" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Visit Date
                                        </td>
                                        <td>
                                            <asp:Label ID="lblVisitDateFooter" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblApprovalDate" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblApprovalDateFooter" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Reviewer Remarks
                                        </td>
                                        <td colspan="3">
                                            <asp:Label ID="lblReviewerRemarks" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
