<%@ Page Language="VB" AutoEventWireup="false" CodeFile="frmLogin.aspx.vb" Inherits="frmLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>.::m-Periodic Visit App:Login::.</title>
    <link href="./Sources/css/Loginstyle.css" rel="stylesheet" type="text/css" />
 </head>
<body>
    <!--WRAPPER-->
    <div id="wrapper">
        <!--SLIDE-IN ICONS-->
        <div class="user-icon">
        </div>
        <div class="pass-icon">
        </div>
        <!--END SLIDE-IN ICONS-->
        <!--LOGIN FORM-->
        <form id="Form1" name="login-form" runat="server" class="login-form" action="" method="post">
        <%--<asp:Login ID="logInCtrl" runat="server" FailureText="Login failed. Please try again."
            OnAuthenticate="logInCtrl_Authenticate">
            <LayoutTemplate>--%>
        <!--HEADER-->
        <div class="header">
            <!--TITLE-->
            <h1>
                Periodic Visit App &nbsp;&nbsp;<img src="Sources/images/meridian_logo.png" height="30px" /></h1>
            <!--END TITLE-->
            <!--DESCRIPTION-->
            <span>Provide Login Credentials</span><!--END DESCRIPTION-->
        </div>
        <!--END HEADER-->
        <!--CONTENT-->
        <div class="content">
            <!--USERNAME-->
            <asp:TextBox ID="UserName" runat="server" class="input username"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
            <!--PASSWORD-->
            <asp:TextBox ID="Password" runat="server" class="input password" Font-Size="0.8em"
                TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1">*</asp:RequiredFieldValidator>
        </div>
        <!--END CONTENT-->
        <!--FOOTER-->
        <div class="footer">
            <asp:Button ID="LoginButton" class="button" runat="server" CommandName="Login" Text="Log In"
                ValidationGroup="Login1" />
            <asp:Label ID="FailureText" runat="server" Text="3 Bad attemps will Lock your windows ID."
                CssClass="label"></asp:Label>
        </div>
        <!--END FOOTER-->
        <%--</LayoutTemplate>
        </asp:Login>--%>
        <asp:Label ID="lblIPAddress" runat="server" Text="" Visible="false"></asp:Label>
        </form>
        <!--END LOGIN FORM-->
    </div>
    <!--END WRAPPER-->
    <!--GRADIENT-->
    <div class="gradient">
    </div>
    <!--END GRADIENT-->
</body>
</html>
