<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="frmVisit.aspx.vb" Inherits="frmVisit" Title="Periodic Visit: Visit" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">

    <script type="text/javascript">
    var ProductModule = "";

    function OpenURL(URL) {
        URL1 = URL.substr(0, URL.indexOf("?"));

        var myForm = document.createElement("form");
        myForm.method = "post";
        myForm.action = URL1//"CommonAuditTrail.aspx";
        myForm.target = "_self";

        if (URL.indexOf("?") > -1) {
            var ParamFullString = URL.substr(URL.indexOf("?") + 1);
            var ParamArray = ParamFullString.split("&");
            for (var iParam = 0; iParam < ParamArray.length; iParam++) {
                var info = ParamArray[iParam].split("=");
                var myInput = document.createElement("input");
                myInput.setAttribute("type", "hidden");
                myInput.setAttribute("name", info[0]);
                myInput.setAttribute("value", info[1]);
                myForm.appendChild(myInput);
            }
            document.body.appendChild(myForm);
            myForm.submit();
        }
        //window.open(URL1);
    }
    
        function PlaceAgreementID() {
           document.getElementById('body_txtAgreementNo').value = document.getElementById("txtReturn").value;
           document.getElementById("btn_open_agr").disabled = true;
        }
    

    function OnFailed(error, userContext) {
        alert("Failed:" + error);
    }

    function OpenAgreement() {
        var txtAgreementID = 'body_txtAgreementNo';
        option = "Agreement";
        var OpenQuery = "select+AgreementID,CustomerName,ProductName,FinanceAmount,BranchName,ApplicationStatus+Status+from+vwAllAgreement2+where+Department+IN('Corporate','SME','Consumer Finance','Staff personal Loan')";
        window.open("PopupServer.aspx?Query=" + OpenQuery + "&SelColumn=1&Placement=txtReturn&ColType=valueload&SortCol=CustomerName", "mywindow", "menubar=0,resizable=1,scrollbars=1,width=1000,height=400");
    }
    
    </script>

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="container text-left">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header">
                        New Visit [
                        <asp:Label ID="lblReportID" runat="server" Text="" ForeColor="#003399"></asp:Label>
                        ] <a href="frmDashboard.aspx"><i class="fa-solid fa-house-user fa-2x"></i></a>
                    </h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <label for="body_txtAgreementNo" class="form-label">
                                    Agreement No</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtAgreementNo" class="form-control" type="text" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <input id="btn_open_agr" type='button' value='....' onclick='OpenAgreement();' />
                                <input type="text" id="txtReturn" style="display: none;" onchange="PlaceAgreementID();" />
                            </div>
                            <div class="col-md-4">
                                <asp:Button ID="btnFetchInfo" class="btn btn-secondary btn-block" runat="server"
                                    Text="Fetch" />
                            </div>
                            <div>
                                <br />
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblErrorMessage" class="bg-danger btn-block" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <asp:Label ID="lblAgrInfo" runat="server" Text=""></asp:Label>
                            </div>
                            <div class="col-md-12">
                                <table style="width: 100%">
                                    <tr>
                                        <th>
                                        </th>
                                        <th>
                                        </th>
                                        <th>
                                        </th>
                                        <th>
                                        </th>
                                        <th>
                                        </th>
                                        <th>
                                        </th>
                                    </tr>
                                    <tr>
                                        <td>
                                            <h6>
                                                Key Information of the facility:</h6>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
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
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Security (Cash/FDR/LD)
                                        </td>
                                        <td>
                                            :
                                        </td>
                                        <td colspan="4">
                                            <asp:Label ID="lblSecurity" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Business Nature</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <table class="table">
                                    <tr>
                                        <td style="width: 20%">
                                            Select Business Nature
                                        </td>
                                        <td style="width: 80%">
                                            <asp:DropDownList ID="drpBusinessNature" class="form-control" runat="server" AutoPostBack="True">
                                                <asp:ListItem Text="Manufacturing" Value="Manufacturing"></asp:ListItem>
                                                <asp:ListItem Text="Trading" Value="Trading"></asp:ListItem>
                                                <asp:ListItem Text="Service" Value="Service"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        All Related Agreements</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdAgrSummary" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="AgreementID">
                                                <ItemTemplate>
                                                    <asp:Label ID="slblAgreementID" runat="server" Text='<%# Bind("AgreementID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DisbursementDt">
                                                <ItemTemplate>
                                                    <asp:Label ID="slblDisbursementDt" runat="server" Text='<%# Bind("DisbursementDt") %>'></asp:Label>
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
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Co-Applicant/Guarantor Details</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; height: 150px; overflow: auto">
                                    <asp:GridView ID="grdRelatedParty" runat="server" AutoGenerateColumns="False" class="table"
                                        EmptyDataText="No Co-Applicant/Guarantor Available">
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
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Payment Status As on [
                        <asp:Label ID="lblAsOnDate" runat="server" Text=""></asp:Label>
                        ]</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdPaymentStatus" CssClass="table" runat="server" AutoGenerateColumns="False">
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
                        Remarks on Payment Behavior
                    </h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <label for="drpPaymentBehavior" class="form-label">
                                    Payment Behavior</label>
                                <asp:DropDownList ID="drpPaymentBehavior" runat="server" class="form-control" ValidationGroup="AddDoc">
                                </asp:DropDownList>
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
                            Status of business operation, growth prospect, progression of risk mitigating factors</h6>
                        <div class="row">
                            <div class="col-md-6">
                                <label for="drpCapacityUtilization" class="form-label">
                                    Capacity Utilization</label>
                                <asp:DropDownList ID="drpCapacityUtilization" runat="server" class="form-control"
                                    ValidationGroup="AddDoc">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="drpBusinessExpansion" class="form-label">
                                    Business Expansion/Diversification</label>
                                <asp:DropDownList ID="drpBusinessExpansion" runat="server" class="form-control" ValidationGroup="AddDoc">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="drpTurnoverGrowth" class="form-label">
                                    Turnover Growth</label>
                                <asp:DropDownList ID="drpTurnoverGrowth" runat="server" class="form-control" ValidationGroup="AddDoc">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <label for="txtCurrentStock" class="form-label">
                                    Current Stock</label>
                                <asp:TextBox ID="txtCurrentStock" class="form-control" type="number" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldCurrentStock" runat="server" ControlToValidate="txtCurrentStock"
                                    ErrorMessage="Required: Current Stock" ValidationGroup="Submit" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="txtLiabilityPosition" class="form-label">
                                    Liability Position</label>
                                <asp:TextBox ID="txtLiabilityPosition" class="form-control" type="number" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldLiabilityPosition" runat="server" ControlToValidate="txtLiabilityPosition"
                                    ErrorMessage="Required: Liability Position" ValidationGroup="Submit" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="txtWarehouse" class="form-label">
                                    Warehouse</label>
                                <asp:RadioButtonList ID="rdBtnAddress" runat="server" AutoPostBack="True" RepeatDirection="Horizontal">
                                    <asp:ListItem>Registered Address</asp:ListItem>
                                    <asp:ListItem>Factory Address</asp:ListItem>
                                    <asp:ListItem>Others</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:TextBox ID="txtWarehouse" class="form-control" type="text" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldWarehouse" runat="server" ControlToValidate="txtWarehouse"
                                    ErrorMessage="Required: Warehouse" ValidationGroup="Submit" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-6">
                                <label for="txtRemarks" class="form-label">
                                    Remarks</label>
                                <asp:TextBox ID="txtRemarks" class="form-control" type="text" runat="server" TextMode="MultiLine"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldRemarks" runat="server" ControlToValidate="txtRemarks"
                                    ErrorMessage="Required: Remarks" ValidationGroup="Submit" ForeColor="#CC0000"></asp:RequiredFieldValidator>
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
                        Personnel During Visit</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <label for="body_txtContactPerson" class="form-label">
                                    Contact Person</label>
                                <asp:TextBox ID="txtContactPerson" class="form-control" type="text" runat="server"
                                    ValidationGroup="AddContact"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldContactPerson" runat="server" ControlToValidate="txtContactPerson"
                                    ErrorMessage="Required: Contact Person" ValidationGroup="AddContact" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label for="body_txtRelation" class="form-label">
                                    Relation/Designation</label>
                                <asp:TextBox ID="txtRelation" class="form-control" type="text" runat="server" ValidationGroup="AddContact"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldRelation" runat="server" ControlToValidate="txtRelation"
                                    ErrorMessage="Required: Relation/Designation" ValidationGroup="AddContact" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label for="body_txtContactNo" class="form-label">
                                    Contact No</label>
                                <asp:TextBox ID="txtContactNo" class="form-control" type="text" runat="server" ValidationGroup="AddContact"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldContactNo" runat="server" ControlToValidate="txtContactNo"
                                    ErrorMessage="Required: Contact No" ValidationGroup="AddContact" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label for="body_txtVisitOfficial" class="form-label">
                                    Visit Official</label>
                                <asp:DropDownList ID="drpEmployees" runat="server" class="form-control" ValidationGroup="AddDoc">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <label for="body_txtVisitDate" class="form-label">
                                    Visit Date</label>
                                <asp:TextBox ID="txtVisitDate" class="form-control" type="date" runat="server" ValidationGroup="AddContact"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldVisitDate" runat="server" ControlToValidate="txtVisitDate"
                                    ErrorMessage="Required: Visit Date" ValidationGroup="AddContact" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-4">
                                <label for="btnAddContact" class="form-label">
                                    &nbsp;
                                </label>
                                <asp:Button ID="btnAddContact" class="btn btn-success btn-block" runat="server" Text="Add Contact"
                                    ValidationGroup="AddContact" />
                            </div>
                            <br />
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdContacts" CssClass="table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Sources/icons/erase.png" OnClientClick="if (!confirm('Are you Sure to Delete The Item ?')) return false"
                                                        CommandName="Delete" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                            <asp:TemplateField HeaderText="Visiting Official ID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitingOfficialID" runat="server" Text='<%# Bind("VisitingOfficialID") %>'></asp:Label>
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
            </div>
            <div class="col-md-12">
                <br />
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header bg-primary text-white">
                        Attachments</h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-3">
                                <asp:TextBox ID="txtDocumentName" class="form-control" type="text" placeholder="Document Name"
                                    runat="server" ValidationGroup="AddDoc"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="reqFldDocumentName" runat="server" ControlToValidate="txtDocumentName"
                                    ErrorMessage="Required: Document Name" ValidationGroup="AddDoc" ForeColor="#CC0000"></asp:RequiredFieldValidator>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="drpDocType" runat="server" class="form-control" ValidationGroup="AddDoc">
                                    <asp:ListItem Text="Sales Ledger" Value="Sales Ledger"></asp:ListItem>
                                    <asp:ListItem Text="Receivable Report" Value="Receivable Report"></asp:ListItem>
                                    <asp:ListItem Text="Delivery Challan" Value="Delivery Challan"></asp:ListItem>
                                    <asp:ListItem Text="Inventory" Value="Inventory"></asp:ListItem>
                                    <asp:ListItem Text="Bill/Voucher etc" Value="Bill/Voucher etc"></asp:ListItem>
                                    <asp:ListItem Text="Updated License/Docs" Value="Updated License/Docs"></asp:ListItem>
                                    <asp:ListItem Text="Visit Photo" Value="Visit Photo"></asp:ListItem>
                                    <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:FileUpload ID="flupDocument" runat="server" />
                            </div>
                            <div class="col-md-3">
                                <asp:Button ID="btnAddDocument" class="btn btn-success btn-block" runat="server"
                                    Text="Add Document" ValidationGroup="AddDoc" />
                            </div>
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdDocuments" runat="server" AutoGenerateColumns="False" CssClass="table">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Delete" ShowHeader="False">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Sources/icons/erase.png" OnClientClick="if (!confirm('Are you Sure to Delete The Item ?')) return false"
                                                        CommandName="Delete" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
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
                                                    <asp:Label ID="lblAttachment" runat="server" Text='<%# Bind("Attachment") %>'></asp:Label>
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
            <div class="col-md-4">
                <asp:Button ID="btnTemporarySave" class="btn btn-info btn-block" OnClientClick="if (!confirm('Are you Sure to Save Temporarily [you can update again]?')) return false"
                    runat="server" Text="Save Temporarily" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnSubmit" ValidationGroup="Submit" class="btn btn-success btn-block"
                    runat="server" Text="Submit" OnClientClick="if (!confirm('Are you Sure to Submit [you will not be able to change further] ?')) return false" />
            </div>
            <div class="col-md-4">
                <asp:Button ID="btnReload" class="btn btn-warning btn-block" runat="server" Text="Reload"
                    OnClientClick="if (!confirm('Are you Sure to Reload the page [all data will be lost] ?')) return false" />
            </div>
        </div>
    </div>
</asp:Content>
