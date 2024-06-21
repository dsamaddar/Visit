<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="frmFind.aspx.vb" Inherits="frmFind" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container text-left">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header">
                        Find Visit Reports <a href="frmDashboard.aspx"><i class="fa-solid fa-house-user fa-2x">
                        </i></a>
                    </h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-4">
                                <label for="txtAgreementNo" class="form-label">
                                    Agreement/Borrower/Product/Business Nature</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtAgreementNo" class="form-control" type="text" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                            </div>
                            <div class="col-md-2">
                                <asp:Button ID="btnFetchInfo" class="btn btn-primary btn-block" runat="server" Text="Fetch" />
                            </div>
                        </div>
                        <div class="row py-2">
                            <div class="col-md-4">
                                <label for="txtStartDate" class="form-label">
                                    Start Date</label>
                            </div>
                            <div class="col-md-3">
                                <asp:TextBox ID="txtStartDate" class="form-control" type="date" runat="server"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <label for="txtEndDate" class="form-label">
                                    End Date</label>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtEndDate" class="form-control" type="date" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header">
                        Available Reports
                    </h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdMyVisitReports" CssClass="table table-sm sortable-table" runat="server" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:TemplateField HeaderText="View">
                                                <ItemTemplate>
                                                    <a href='<%# Eval("ReportID","frmView.aspx?ReportID={0}") %>' target="_blank"><i
                                                        class="fa-solid fa-eye"></i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ReportID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblReportID" runat="server" Text='<%# Bind("ReportID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="AgreementNo">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("AgreementID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Product">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("ProductType") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Borrower">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("BorrowerName") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Visited By">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitedBy" runat="server" Text='<%# Bind("VisitedBy") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VisitDate">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("VisitDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVisitStatus" runat="server" Text='<%# Bind("VisitStatus") %>'></asp:Label>
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
        </div>

        <script src="https://cdn.jsdelivr.net/npm/@riversun/sortable-table@1.0.0/lib/sortable-table.js"></script>
</asp:Content>
