Imports Microsoft.VisualBasic

Public Class CommonLib : Inherits System.Web.UI.Page
    Public ThemeName As String
    Public cnString As String
    Public GridViewAlternateBGOdd As String
    Public GridViewAlternateBGEven As String
    Public SoftwareName As String
    Public OrganizationLogo As String
    Public tmpFilePath As String
    Public tmpURLPrefix As String

    Public Sub MessageBox(ByVal msg As String)
        ScriptManager.RegisterClientScriptBlock(Page, Page.GetType(), Guid.NewGuid().ToString(), "alert('" & msg & "');", True)
    End Sub

    Public Sub New()
        ThemeName = System.Configuration.ConfigurationManager.AppSettings("DefaultTheme")
        cnString = System.Configuration.ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        GridViewAlternateBGOdd = System.Configuration.ConfigurationManager.AppSettings("GridViewAlternateBGOdd")
        GridViewAlternateBGEven = System.Configuration.ConfigurationManager.AppSettings("GridViewAlternateBGEven")
        SoftwareName = System.Configuration.ConfigurationManager.AppSettings("SoftwareName")
        OrganizationLogo = System.Configuration.ConfigurationManager.AppSettings("OrganizationLogo")
        tmpFilePath = System.Configuration.ConfigurationManager.AppSettings("TempFilePath")
        tmpURLPrefix = System.Configuration.ConfigurationManager.AppSettings("TempURLPrefix")
    End Sub

    Public Sub InitPage(ByVal pg As System.Web.UI.Page)
        pg.Theme = ThemeName
    End Sub

    Public Sub GlobalExceptionHandler(ByVal ex As Exception, ByVal src As String)
'        MessageBox("Exception From: " + src + " Message: " + ex.Message)
    End Sub

End Class


