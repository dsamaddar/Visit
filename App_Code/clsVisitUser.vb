Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data
Imports System.Web.Services

Public Class clsVisitUser

    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

    Dim _EmployeeID, _EmployeeName, _UserID, _Password, _Designation, _Department As String

    Public Property EmployeeID() As String
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As String)
            _EmployeeID = value
        End Set
    End Property

    Public Property EmployeeName() As String
        Get
            Return _EmployeeName
        End Get
        Set(ByVal value As String)
            _EmployeeName = value
        End Set
    End Property

    Public Property UserID() As String
        Get
            Return _UserID
        End Get
        Set(ByVal value As String)
            _UserID = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    Public Property Designation() As String
        Get
            Return _Designation
        End Get
        Set(ByVal value As String)
            _Designation = value
        End Set
    End Property

    Public Property Department() As String
        Get
            Return _Department
        End Get
        Set(ByVal value As String)
            _Department = value
        End Set
    End Property

#Region " Check User Login "

    Public Function fnCheckUserLogin(ByVal VisitUser As clsVisitUser) As clsVisitUser
        Dim sp As String = "spCheckVisitUserLogin"
        Dim dr As SqlDataReader
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@UserID", VisitUser.UserID)
                cmd.Parameters.AddWithValue("@Password", VisitUser.Password)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    VisitUser.EmployeeID = dr.Item("EmployeeID")
                    VisitUser.UserID = dr.Item("UserID")
                    VisitUser.EmployeeName = dr.Item("EmployeeName")
                    VisitUser.Designation = dr.Item("Designation")
                    VisitUser.Department = dr.Item("Department")
                End While
                con.Close()
                Return VisitUser
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

#End Region


End Class
