Imports Microsoft.VisualBasic


Public Class GlobalVars
    Public Shared cl As New CommonLib
    Public Shared UserID As String = "None"
    Public Shared BranchCode As String = "10"
    Public Shared BranchName As String = "Corporate Head Office"
    Public Shared DepartmentName As String = "NA"
    Public Shared MainBranchCode As String = "10"
    Public Shared MainBranchName As String = "Corporate Head Office"

    Public Shared Version As String = "1.1.0"
    Public Shared RoleName As String = "Super"
    'Public Shared sql As String = "Select * from ActivityEntry"
    Public Shared gldsMain As SqlDataSource
    Public Shared gblSessionID As String = ""   'ahsan
    Public Shared gblIPAddress As String = ""   'ahsan
End Class
