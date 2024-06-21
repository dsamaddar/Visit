Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsAgreement

    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

    Dim _AgreementID, _AgreementNo, _BranchName, _CustomerName, _RegisteredOffice, _FactoryAddress, _ProductName, _LoanCategory, _
    _DisbursementDt, _DisbursementAmnt, _Tenure, _Security As String

    Public Property AgreementID() As String
        Get
            Return _AgreementID
        End Get
        Set(ByVal value As String)
            _AgreementID = value
        End Set
    End Property

    Public Property AgreementNo() As String
        Get
            Return _AgreementNo
        End Get
        Set(ByVal value As String)
            _AgreementNo = value
        End Set
    End Property

    Public Property BranchName() As String
        Get
            Return _BranchName
        End Get
        Set(ByVal value As String)
            _BranchName = value
        End Set
    End Property

    Public Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property

    Public Property RegisteredOffice() As String
        Get
            Return _RegisteredOffice
        End Get
        Set(ByVal value As String)
            _RegisteredOffice = value
        End Set
    End Property

    Public Property FactoryAddress() As String
        Get
            Return _FactoryAddress
        End Get
        Set(ByVal value As String)
            _FactoryAddress = value
        End Set
    End Property

    Public Property ProductName() As String
        Get
            Return _ProductName
        End Get
        Set(ByVal value As String)
            _ProductName = value
        End Set
    End Property

    Public Property LoanCategory() As String
        Get
            Return _LoanCategory
        End Get
        Set(ByVal value As String)
            _LoanCategory = value
        End Set
    End Property

    Public Property DisbursementDt() As String
        Get
            Return _DisbursementDt
        End Get
        Set(ByVal value As String)
            _DisbursementDt = value
        End Set
    End Property

    Public Property DisbursementAmnt() As String
        Get
            Return _DisbursementAmnt
        End Get
        Set(ByVal value As String)
            _DisbursementAmnt = value
        End Set
    End Property

    Public Property Tenure() As String
        Get
            Return _Tenure
        End Get
        Set(ByVal value As String)
            _Tenure = value
        End Set
    End Property

    Public Property Security() As String
        Get
            Return _Security
        End Get
        Set(ByVal value As String)
            _Security = value
        End Set
    End Property

    Dim _FinanceAmount As Double

    Public Property FinanceAmount() As Double
        Get
            Return _FinanceAmount
        End Get
        Set(ByVal value As Double)
            _FinanceAmount = value
        End Set
    End Property

    Dim _InstallmentDue, _InstallmentPaid, _OutstandingAmnt, _OverdueAmnt, _AsOnDate As String

    Public Property InstallmentDue() As String
        Get
            Return _InstallmentDue
        End Get
        Set(ByVal value As String)
            _InstallmentDue = value
        End Set
    End Property

    Public Property InstallmentPaid() As String
        Get
            Return _InstallmentPaid
        End Get
        Set(ByVal value As String)
            _InstallmentPaid = value
        End Set
    End Property

    Public Property OutstandingAmnt() As String
        Get
            Return _OutstandingAmnt
        End Get
        Set(ByVal value As String)
            _OutstandingAmnt = value
        End Set
    End Property

    Public Property OverdueAmnt() As String
        Get
            Return _OverdueAmnt
        End Get
        Set(ByVal value As String)
            _OverdueAmnt = value
        End Set
    End Property

    Public Property AsOnDate() As String
        Get
            Return _AsOnDate
        End Get
        Set(ByVal value As String)
            _AsOnDate = value
        End Set
    End Property

#Region " Get Agreement Info By ID "

    Public Function fnGetAgreementInfoByID(ByVal AgreementID As String) As clsAgreement
        Dim agr As New clsAgreement()
        Dim sp As String = "spGetAgreementInfoByID"
        Dim dr As SqlDataReader
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@AgreementID", AgreementID)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    agr.AgreementID = dr.Item("AgreementID")
                    agr.AgreementNo = dr.Item("AgreementNo")
                    agr.BranchName = dr.Item("BranchName")
                    agr.CustomerName = dr.Item("CustomerName")
                    agr.RegisteredOffice = dr.Item("RegisteredOffice")
                    agr.FactoryAddress = dr.Item("FactoryAddress")
                    agr.ProductName = dr.Item("ProductName")
                    agr.LoanCategory = dr.Item("LoanCategory")
                    agr.DisbursementDt = dr.Item("DisbursementDt")
                    agr.DisbursementAmnt = dr.Item("DisbursementAmnt")
                    agr.Tenure = dr.Item("Tenure")
                    agr.Security = dr.Item("Security")
                    agr.AsOnDate = dr.Item("AsOnDate")
                    agr.InstallmentDue = dr.Item("InstallmentDue")
                    agr.InstallmentPaid = dr.Item("InstallmentPaid")
                    agr.OutstandingAmnt = dr.Item("OutstandingAmnt")
                    agr.OverdueAmnt = dr.Item("OverdueAmnt")
                End While
                con.Close()
                Return agr
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

#End Region
   
#Region " Get Related Party "

    Public Function fnGetRelatedParty(ByVal AgreementID As String) As DataSet

        Dim sp As String = "spGetRelatedParty"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@AgreementID", AgreementID)
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

#End Region

#Region " Get Agr Summary By AgrNo "

    Public Function fnGetAgrSummaryByAgrNo(ByVal AgreementNo As String) As DataSet

        Dim sp As String = "spGetAgrSummaryByAgrNo"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@AgreementNo", AgreementNo)
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

#End Region

#Region " Get Agr Payment Status By AgrNo "

    Public Function fnGetAgrPaymentStatusByAgrNo(ByVal AgreementNo As String) As DataSet

        Dim sp As String = "spGetAgrPaymentStatusByAgrNo"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@AgreementNo", AgreementNo)
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

#End Region

End Class
