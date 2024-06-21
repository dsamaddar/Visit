Imports System
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Runtime.InteropServices
Imports System.Data.SqlClient
Imports System.Net.Dns
Imports System.Security.Principal
Imports System.Security.Cryptography

Partial Class frmLogin
    Inherits System.Web.UI.Page

    Dim VisitUserCtrl As New clsVisitUser()
    'Dim DashBoardData As New clsDashBoard()

    Private Sub MessageBox(ByVal strMsg As String)
        Dim lbl As New System.Web.UI.WebControls.Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine _
                   & "window.alert(" & "'" & strMsg & "'" & ")</script>"
        Page.Controls.Add(lbl)
    End Sub

    <DllImport("ADVAPI32.dll", EntryPoint:="LogonUserW", SetLastError:=True, CharSet:=CharSet.Auto)> _
    Public Shared Function LogonUser(ByVal lpszUsername As String, ByVal lpszDomain As String, ByVal lpszPassword As String, ByVal dwLogonType As Integer, ByVal dwLogonProvider As Integer, ByRef phToken As IntPtr) As Boolean
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim UserID As String = ""
        Dim Password As String = ""

        UserID = Request.QueryString("UserID")
        Password = Request.QueryString("Password")

        Session("LoginUserID") = UserID
        Session("Password") = Password

        If Not IsPostBack Then
            Session("EmployeeID") = ""
            Session("UserID") = ""
            Session("EmployeeName") = ""
            Session("Designation") = ""
            Session("Department") = ""
        End If
    End Sub

    Protected Sub LoginButton_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LoginButton.Click
        Try
            Dim UniqueUserID As String = ""
            Dim VisitUser As New clsVisitUser()

            VisitUser.UserID = UserName.Text
            VisitUser.Password = Password.Text

            VisitUser = VisitUserCtrl.fnCheckUserLogin(VisitUser)

            If VisitUser.EmployeeID = "" Or VisitUser.EmployeeID = Nothing Then
                MessageBox("Provide Correct Credentials")
            Else
                FormsAuthentication.RedirectFromLoginPage(VisitUser.UserID, False)
                UniqueUserID = VisitUser.EmployeeID
                Session("EmployeeID") = VisitUser.EmployeeID
                Session("UserID") = VisitUser.UserID
                Session("EmployeeName") = VisitUser.EmployeeName
                Session("Designation") = VisitUser.Designation
                Session("Department") = VisitUser.Department
                Response.Redirect("frmDashBoard.aspx")
            End If
        Catch ex As Exception
            MessageBox(ex.Message)
        End Try
    End Sub

End Class
