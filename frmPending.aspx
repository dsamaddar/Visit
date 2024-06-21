<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="frmPending.aspx.vb" Inherits="frmPending" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <div class="container text-left">
        <div class="row">
            <div class="col-md-12">
                <div class="card">
                    <h6 class="card-header">
                        Pending Visits <a href="frmDashboard.aspx"><i class="fa-solid fa-house-user fa-2x"></i>
                        </a>
                    </h6>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-12">
                                <div style="width: 100%; overflow: auto">
                                    <asp:GridView ID="grdMyVisitReports" CssClass="table" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="No Pending Visit">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Edit">
                                                <ItemTemplate>
                                                    <a href='<%# Eval("ReportID","frmVisit.aspx?ReportID={0}") %>'><i class="fa-solid fa-edit">
                                                    </i></a>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delete">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgbtnDelete" ImageUrl="~/Sources/icons/erase.png" OnClientClick="if (!confirm('Are you Sure to Delete The Report ?')) return false"
                                                        CommandName="Delete" runat="server" />
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
    </div>
</asp:Content>
