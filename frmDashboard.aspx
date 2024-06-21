<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="frmDashboard.aspx.vb" Inherits="frmDashboard" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="Server">
    <p>
    </p>
    <div class="container text-center">
        <div class="row">
            <div class="col-sm-6">
                <div class="card text-white bg-success mb-3">
                    <div class="card-body">
                        <h5 class="card-title">
                            New Visit
                        </h5>
                        <p class="card-text">
                        </p>
                        <a href="frmVisit.aspx"><i class="fa-solid fa-newspaper fa-2xl" style="color: #70b5ba;">
                        </i></a>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="card text-white bg-warning mb-3">
                    <div class="card-body">
                        <h5 class="card-title">
                            My Visit Reports
                        </h5>
                        <p class="card-text">
                        </p>
                        <a href="frmMyVisit.aspx"><i class="fa-solid fa-database fa-2xl"></i></a>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="card text-white bg-dark mb-3">
                    <div class="card-body">
                        <h5 class="card-title">
                            Approval
                        </h5>
                        <p class="card-text">
                        </p>
                        <a href="frmWaiting.aspx"><i class="fa-solid fa-school-circle-check fa-2xl"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="card text-white bg-secondary mb-3">
                    <div class="card-body">
                        <h5 class="card-title">
                            Find Visits
                        </h5>
                        <p class="card-text">
                        </p>
                        <a href="frmFind.aspx"><i class="fa-solid fa-magnifying-glass-location fa-2xl" style="color: #70b5ba;"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="card text-white bg-danger mb-3">
                    <div class="card-body">
                        <h5 class="card-title">
                            Pending Visits
                        </h5>
                        <p class="card-text">
                        </p>
                        <a href="frmPending.aspx"><i class="fa-solid fa-clock fa-2xl"  style="color: #70b5ba;">
                        </i></a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
