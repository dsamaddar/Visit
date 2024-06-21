Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsHolidays

    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("HRMConnectionString").ConnectionString)

    Dim _HolidayID, _Purpose, _EntryBy As String

    Public Property HolidayID() As String
        Get
            Return _HolidayID
        End Get
        Set(ByVal value As String)
            _HolidayID = value
        End Set
    End Property

    Public Property Purpose() As String
        Get
            Return _Purpose
        End Get
        Set(ByVal value As String)
            _Purpose = value
        End Set
    End Property

    Public Property EntryBy() As String
        Get
            Return _EntryBy
        End Get
        Set(ByVal value As String)
            _EntryBy = value
        End Set
    End Property

    Dim _HolidayDate, _DateTo As DateTime

    Public Property HolidayDate() As DateTime
        Get
            Return _HolidayDate
        End Get
        Set(ByVal value As DateTime)
            _HolidayDate = value
        End Set
    End Property

    Public Property DateTo() As DateTime
        Get
            Return _DateTo
        End Get
        Set(ByVal value As DateTime)
            _DateTo = value
        End Set
    End Property

    Public Function fnInsertHolidays(ByVal Holidays As clsHolidays) As Integer
        Try
            Dim cmd As SqlCommand = New SqlCommand("spInsertHolidays", con)
            con.Open()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@DateFrom", Holidays.HolidayDate)
            cmd.Parameters.AddWithValue("@DateTo", Holidays.DateTo)
            cmd.Parameters.AddWithValue("@Purpose", Holidays.Purpose)
            cmd.Parameters.AddWithValue("@EntryBy", Holidays.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Return 1
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return 0
        End Try
    End Function

    Public Function fnUpdateHoliday(ByVal Holidays As clsHolidays) As Integer
        Try
            Dim cmd As SqlCommand = New SqlCommand("spUpdateHoliday", con)
            con.Open()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@HolidayID", Holidays.HolidayID)
            cmd.Parameters.AddWithValue("@HolidayDate", Holidays.HolidayDate)
            cmd.Parameters.AddWithValue("@Purpose", Holidays.Purpose)
            cmd.Parameters.AddWithValue("@EntryBy", Holidays.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            Return 1
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return 0
        End Try
    End Function

    Public Function fnDeleteHoliday(ByVal HolidayID As String) As Integer
        Try
            Dim cmd As SqlCommand = New SqlCommand("spDeleteHoliday", con)
            con.Open()

            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@HolidayID", HolidayID)
            cmd.ExecuteNonQuery()
            con.Close()
            Return 1
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return 0
        End Try
    End Function

    Public Function fnGetHolidayByID(ByVal HolidayID As String) As clsHolidays
        Dim Holidays As New clsHolidays()
        Dim sp As String = "spGetHolidayInfoByID"
        Dim dr As SqlDataReader
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@HolidayID", HolidayID)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    Holidays.HolidayDate = dr.Item("HolidayDate")
                    Holidays.Purpose = dr.Item("Purpose")
                End While
                con.Close()
                Return Holidays
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

    Public Function fnGetHolidayList() As DataSet
        Dim sp As String = "spGetHolidayList"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                da.SelectCommand = cmd
                da.Fill(ds)
                con.Close()
                Return ds
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

End Class
