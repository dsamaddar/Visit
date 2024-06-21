Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class clsVisitReport

    Dim con As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

    Dim _ReportID, _AgreementID, _CustomerID, _RegisteredOffice, _ProductType, _Tenure, _BorrowerName, _
    _DisbursementDt, _DisbursementAmnt, _Security, _FactoryAddress, _PurposeOfLoan, _BusinessNature, _Warehouse, _ElaboratePurpose, _
 _EntryBy, _AgrSummaryList, _PaymentStatusList, _RelatedPartyList, _ContactList, _DocumentList, _EmployeeName, _
 _Designation, _Department, _SubmissionDate, _ApprovalDate, _PaymentBehavior, _TurnoverGrowth, _ApproverRemarks, _ApproverID, _EmployeeID, _
 _Supervisor, _SupervisorDesignation, _SupervisorDepartment As String

    Public Property ReportID() As String
        Get
            Return _ReportID
        End Get
        Set(ByVal value As String)
            _ReportID = value
        End Set
    End Property

    Public Property AgreementID() As String
        Get
            Return _AgreementID
        End Get
        Set(ByVal value As String)
            _AgreementID = value
        End Set
    End Property

    Public Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
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

    Public Property ProductType() As String
        Get
            Return _ProductType
        End Get
        Set(ByVal value As String)
            _ProductType = value
        End Set
    End Property

    Public Property BorrowerName() As String
        Get
            Return _BorrowerName
        End Get
        Set(ByVal value As String)
            _BorrowerName = value
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

    Public Property FactoryAddress() As String
        Get
            Return _FactoryAddress
        End Get
        Set(ByVal value As String)
            _FactoryAddress = value
        End Set
    End Property

    Public Property PurposeOfLoan() As String
        Get
            Return _PurposeOfLoan
        End Get
        Set(ByVal value As String)
            _PurposeOfLoan = value
        End Set
    End Property

    Public Property BusinessNature() As String
        Get
            Return _BusinessNature
        End Get
        Set(ByVal value As String)
            _BusinessNature = value
        End Set
    End Property

    Public Property Warehouse() As String
        Get
            Return _Warehouse
        End Get
        Set(ByVal value As String)
            _Warehouse = value
        End Set
    End Property

    Public Property ElaboratePurpose() As String
        Get
            Return _ElaboratePurpose
        End Get
        Set(ByVal value As String)
            _ElaboratePurpose = value
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

    Public Property AgrSummaryList() As String
        Get
            Return _AgrSummaryList
        End Get
        Set(ByVal value As String)
            _AgrSummaryList = value
        End Set
    End Property

    Public Property PaymentStatusList() As String
        Get
            Return _PaymentStatusList
        End Get
        Set(ByVal value As String)
            _PaymentStatusList = value
        End Set
    End Property

    Public Property RelatedPartyList() As String
        Get
            Return _RelatedPartyList
        End Get
        Set(ByVal value As String)
            _RelatedPartyList = value
        End Set
    End Property

    Public Property ContactList() As String
        Get
            Return _ContactList
        End Get
        Set(ByVal value As String)
            _ContactList = value
        End Set
    End Property

    Public Property DocumentList() As String
        Get
            Return _DocumentList
        End Get
        Set(ByVal value As String)
            _DocumentList = value
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

    Public Property SubmissionDate() As String
        Get
            Return _SubmissionDate
        End Get
        Set(ByVal value As String)
            _SubmissionDate = value
        End Set
    End Property

    Public Property ApprovalDate() As String
        Get
            Return _ApprovalDate
        End Get
        Set(ByVal value As String)
            _ApprovalDate = value
        End Set
    End Property

    Public Property PaymentBehavior() As String
        Get
            Return _PaymentBehavior
        End Get
        Set(ByVal value As String)
            _PaymentBehavior = value
        End Set
    End Property

    Public Property TurnoverGrowth() As String
        Get
            Return _TurnoverGrowth
        End Get
        Set(ByVal value As String)
            _TurnoverGrowth = value
        End Set
    End Property

    Public Property ApproverRemarks() As String
        Get
            Return _ApproverRemarks
        End Get
        Set(ByVal value As String)
            _ApproverRemarks = value
        End Set
    End Property

    Public Property ApproverID() As String
        Get
            Return _ApproverID
        End Get
        Set(ByVal value As String)
            _ApproverID = value
        End Set
    End Property

    Public Property EmployeeID() As String
        Get
            Return _EmployeeID
        End Get
        Set(ByVal value As String)
            _EmployeeID = value
        End Set
    End Property

    Public Property Supervisor() As String
        Get
            Return _Supervisor
        End Get
        Set(ByVal value As String)
            _Supervisor = value
        End Set
    End Property

    Public Property SupervisorDesignation() As String
        Get
            Return _SupervisorDesignation
        End Get
        Set(ByVal value As String)
            _SupervisorDesignation = value
        End Set
    End Property

    Public Property SupervisorDepartment() As String
        Get
            Return _SupervisorDepartment
        End Get
        Set(ByVal value As String)
            _SupervisorDepartment = value
        End Set
    End Property

    Dim _EntryDate As Date

    Public Property EntryDate() As Date
        Get
            Return _EntryDate
        End Get
        Set(ByVal value As Date)
            _EntryDate = value
        End Set
    End Property

    Dim _StockAmnt, _CurrentStock, _LiabilityPosition, _CapacityUtilization, _BusinessExpansion, _ReviewRiskFactors, _BusinessOpChange As String

    Public Property StockAmnt() As String
        Get
            Return _StockAmnt
        End Get
        Set(ByVal value As String)
            _StockAmnt = value
        End Set
    End Property

    Public Property CurrentStock() As String
        Get
            Return _CurrentStock
        End Get
        Set(ByVal value As String)
            _CurrentStock = value
        End Set
    End Property

    Public Property LiabilityPosition() As String
        Get
            Return _LiabilityPosition
        End Get
        Set(ByVal value As String)
            _LiabilityPosition = value
        End Set
    End Property

    Public Property CapacityUtilization() As String
        Get
            Return _CapacityUtilization
        End Get
        Set(ByVal value As String)
            _CapacityUtilization = value
        End Set
    End Property

    Public Property BusinessExpansion() As String
        Get
            Return _BusinessExpansion
        End Get
        Set(ByVal value As String)
            _BusinessExpansion = value
        End Set
    End Property

    Public Property ReviewRiskFactors() As String
        Get
            Return _ReviewRiskFactors
        End Get
        Set(ByVal value As String)
            _ReviewRiskFactors = value
        End Set
    End Property

    Public Property BusinessOpChange() As String
        Get
            Return _BusinessOpChange
        End Get
        Set(ByVal value As String)
            _BusinessOpChange = value
        End Set
    End Property

    Dim _InstallmentDue, _InstallmentPaid, _OutstandingAmnt, _OverdueAmnt, _Remarks, _AsOnDate As String

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

    Public Property Remarks() As String
        Get
            Return _Remarks
        End Get
        Set(ByVal value As String)
            _Remarks = value
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

    Dim _IsSubmitted, _EvidenceAttached, _IsApproved, _IsRejected As Boolean

    Public Property IsSubmitted() As Boolean
        Get
            Return _IsSubmitted
        End Get
        Set(ByVal value As Boolean)
            _IsSubmitted = value
        End Set
    End Property

    Public Property EvidenceAttached() As Boolean
        Get
            Return _EvidenceAttached
        End Get
        Set(ByVal value As Boolean)
            _EvidenceAttached = value
        End Set
    End Property

    Public Property IsApproved() As Boolean
        Get
            Return _IsApproved
        End Get
        Set(ByVal value As Boolean)
            _IsApproved = value
        End Set
    End Property

    Public Property IsRejected() As Boolean
        Get
            Return _IsRejected
        End Get
        Set(ByVal value As Boolean)
            _IsRejected = value
        End Set
    End Property

    Dim _PaymentBehaviorID, _CapacityUtilizationID, _BusinessExpansionID, _TurnoverGrowthID As Integer

    Public Property PaymentBehaviorID() As Integer
        Get
            Return _PaymentBehaviorID
        End Get
        Set(ByVal value As Integer)
            _PaymentBehaviorID = value
        End Set
    End Property

    Public Property CapacityUtilizationID() As Integer
        Get
            Return _CapacityUtilizationID
        End Get
        Set(ByVal value As Integer)
            _CapacityUtilizationID = value
        End Set
    End Property

    Public Property BusinessExpansionID() As Integer
        Get
            Return _BusinessExpansionID
        End Get
        Set(ByVal value As Integer)
            _BusinessExpansionID = value
        End Set
    End Property

    Public Property TurnoverGrowthID() As Integer
        Get
            Return _TurnoverGrowthID
        End Get
        Set(ByVal value As Integer)
            _TurnoverGrowthID = value
        End Set
    End Property

    Dim _PaymentBehaviorScore, _CapacityUtilizationScore, _BusinessExpansionScore, _TurnoverGrowthScore, _FinalScore As Double

    Public Property PaymentBehaviorScore() As Double
        Get
            Return _PaymentBehaviorScore
        End Get
        Set(ByVal value As Double)
            _PaymentBehaviorScore = value
        End Set
    End Property

    Public Property CapacityUtilizationScore() As Double
        Get
            Return _CapacityUtilizationScore
        End Get
        Set(ByVal value As Double)
            _CapacityUtilizationScore = value
        End Set
    End Property

    Public Property BusinessExpansionScore() As Double
        Get
            Return _BusinessExpansionScore
        End Get
        Set(ByVal value As Double)
            _BusinessExpansionScore = value
        End Set
    End Property

    Public Property TurnoverGrowthScore() As Double
        Get
            Return _TurnoverGrowthScore
        End Get
        Set(ByVal value As Double)
            _TurnoverGrowthScore = value
        End Set
    End Property

    Public Property FinalScore() As Double
        Get
            Return _FinalScore
        End Get
        Set(ByVal value As Double)
            _FinalScore = value
        End Set
    End Property

    Dim _imgVisitBy, _imgApprovedBy As String

    Public Property imgVisitBy() As String
        Get
            Return _imgVisitBy
        End Get
        Set(ByVal value As String)
            _imgVisitBy = value
        End Set
    End Property

    Public Property imgApprovedBy() As String
        Get
            Return _imgApprovedBy
        End Get
        Set(ByVal value As String)
            _imgApprovedBy = value
        End Set
    End Property



#Region " Generate Visit Report ID "

    Public Function fnGenVisitReportID() As String
        Dim sp As String = "spGenVisitReportID"
        Dim dr As SqlDataReader
        Dim ReportID As String = ""
        Try
            con.Open()

            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                dr = cmd.ExecuteReader()
                While dr.Read()
                    ReportID = dr.Item("ReportID")
                End While
                con.Close()

                Return ReportID
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return ""
        End Try
    End Function

#End Region

#Region " Get Report Details "

    Public Function fnGetReportDetails(ByVal ReportID As String) As clsVisitReport
        Dim sp As String = "spGetReportDetails"
        Dim dr As SqlDataReader
        Dim VisitReport As New clsVisitReport()
        Try
            con.Open()

            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    VisitReport.AgreementID = dr.Item("AgreementID")
                    VisitReport.CustomerID = dr.Item("CustomerID")
                    VisitReport.BorrowerName = dr.Item("BorrowerName")
                    VisitReport.RegisteredOffice = dr.Item("RegisteredOffice")
                    VisitReport.FactoryAddress = dr.Item("FactoryAddress")
                    VisitReport.ProductType = dr.Item("ProductType")
                    VisitReport.PurposeOfLoan = dr.Item("PurposeOfLoan")
                    VisitReport.Security = dr.Item("Security")
                    VisitReport.AsOnDate = dr.Item("AsOnDate")
                    VisitReport.BusinessNature = dr.Item("BusinessNature")
                    VisitReport.PaymentBehaviorID = dr.Item("PaymentBehaviorID")
                    VisitReport.PaymentBehavior = dr.Item("PaymentBehavior")
                    VisitReport.PaymentBehaviorScore = dr.Item("PaymentBehaviorScore")
                    VisitReport.CurrentStock = dr.Item("CurrentStock")
                    VisitReport.LiabilityPosition = dr.Item("LiabilityPosition")
                    VisitReport.Warehouse = dr.Item("Warehouse")
                    VisitReport.Remarks = dr.Item("Remarks")
                    VisitReport.CapacityUtilizationID = dr.Item("CapacityUtilizationID")
                    VisitReport.CapacityUtilization = dr.Item("CapacityUtilization")
                    VisitReport.CapacityUtilizationScore = dr.Item("CapacityUtilizationScore")
                    VisitReport.BusinessExpansionID = dr.Item("BusinessExpansionID")
                    VisitReport.BusinessExpansion = dr.Item("BusinessExpansion")
                    VisitReport.BusinessExpansionScore = dr.Item("BusinessExpansionScore")
                    VisitReport.TurnoverGrowthID = dr.Item("TurnoverGrowthID")
                    VisitReport.TurnoverGrowth = dr.Item("TurnoverGrowth")
                    VisitReport.TurnoverGrowthScore = dr.Item("TurnoverGrowthScore")
                    VisitReport.EmployeeName = dr.Item("EmployeeName")
                    VisitReport.Designation = dr.Item("Designation")
                    VisitReport.Department = dr.Item("Department")
                    VisitReport.SubmissionDate = dr.Item("SubmissionDate")
                    VisitReport.FinalScore = dr.Item("FinalScore")
                    VisitReport.IsApproved = dr.Item("IsApproved")
                    VisitReport.IsRejected = dr.Item("IsRejected")
                    VisitReport.ApproverRemarks = dr.Item("ApproverRemarks")
                    VisitReport.Supervisor = dr.Item("Supervisor")
                    VisitReport.SupervisorDesignation = dr.Item("SupervisorDesignation")
                    VisitReport.SupervisorDepartment = dr.Item("SupervisorDepartment")
                    VisitReport.ApprovalDate = dr.Item("ApprovalDate")
                    VisitReport.imgVisitBy = dr.Item("imgVisitBy")
                    VisitReport.imgApprovedBy = dr.Item("imgApprovedBy")
                End While
                con.Close()

                Return VisitReport
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

#End Region

#Region " Get Pending Visits "

    Public Function fnGetPendingVisits(ByVal EmployeeID As String) As DataSet

        Dim sp As String = "spPendingVisits"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
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

#Region " Get Waiting Approval List "

    Public Function fnGetWaitingApprovalVisits(ByVal EmployeeID As String) As DataSet

        Dim sp As String = "spGetWaitingApprovalVisits"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
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

#Region " Get Visit Reports "

    Public Function fnGetVisitReports(ByVal EmployeeID As String) As DataSet

        Dim sp As String = "spGetVisitReports"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID)
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

#Region " Find Visit Reports "

    Public Function fnFindVisitReports(ByVal AgreementID As String, ByVal StartDate As Date, ByVal EndDate As Date) As DataSet

        Dim sp As String = "spFindVisitReports"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@AgreementID", AgreementID)
                cmd.Parameters.AddWithValue("@StartDate", StartDate)
                cmd.Parameters.AddWithValue("@EndDate", EndDate)
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

#Region " Get Visit Related Party By ReportID "

    Public Function fnGetVisitRelatedPartyByReportID(ByVal ReportID As String) As DataSet

        Dim sp As String = "spGetVisitRelatedPartyByReportID"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
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

#Region " Get Contacts By ReportID "

    Public Function fnGetContactsByReportID(ByVal ReportID As String) As DataSet

        Dim sp As String = "spGetContactsByReportID"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
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

#Region " Get Documents By ReportID "

    Public Function fnGetDocumentsByReportID(ByVal ReportID As String) As DataSet

        Dim sp As String = "spGetDocumentsByReportID"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
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

#Region " Get Visit Agr Summary By ReportID "

    Public Function fnGetVisitAgrSummaryByReportID(ByVal ReportID As String) As DataSet

        Dim sp As String = "spGetVisitAgrSummaryByReportID"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
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

#Region " Get Visit Payment Status By ReportID "

    Public Function fnGetVisitPaymentStatusByReportID(ByVal ReportID As String) As DataSet

        Dim sp As String = "spGetVisitPaymentStatusByReportID"
        Dim da As SqlDataAdapter = New SqlDataAdapter()
        Dim ds As DataSet = New DataSet()
        Try
            con.Open()
            Using cmd = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
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

#Region " Insert Visit Report "

    Public Function fnInsertVisitReport(ByVal visitReport As clsVisitReport) As clsResult
        Dim result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spInsertVisitReport", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ReportID", visitReport.ReportID)
            cmd.Parameters.AddWithValue("@AgreementID", visitReport.AgreementID)
            cmd.Parameters.AddWithValue("@BusinessNature", visitReport.BusinessNature)
            cmd.Parameters.AddWithValue("@PaymentBehaviorID", visitReport.PaymentBehaviorID)
            cmd.Parameters.AddWithValue("@CurrentStock", visitReport.CurrentStock)
            cmd.Parameters.AddWithValue("@LiabilityPosition", visitReport.LiabilityPosition)
            cmd.Parameters.AddWithValue("@Warehouse", visitReport.Warehouse)
            cmd.Parameters.AddWithValue("@Remarks", visitReport.Remarks)
            cmd.Parameters.AddWithValue("@CapacityUtilizationID", visitReport.CapacityUtilizationID)
            cmd.Parameters.AddWithValue("@BusinessExpansionID", visitReport.BusinessExpansionID)
            cmd.Parameters.AddWithValue("@TurnoverGrowthID", visitReport.TurnoverGrowthID)
            cmd.Parameters.AddWithValue("@AgrSummaryList", visitReport.AgrSummaryList)
            cmd.Parameters.AddWithValue("@PaymentStatusList", visitReport.PaymentStatusList)
            cmd.Parameters.AddWithValue("@RelatedPartyList", visitReport.RelatedPartyList)
            cmd.Parameters.AddWithValue("@ContactList", visitReport.ContactList)
            cmd.Parameters.AddWithValue("@DocumentList", visitReport.DocumentList)
            cmd.Parameters.AddWithValue("@IsSubmitted", visitReport.IsSubmitted)
            cmd.Parameters.AddWithValue("@EntryBy", visitReport.EntryBy)
            cmd.ExecuteNonQuery()
            con.Close()
            result.Success = True
            result.Message = "Visit Report Submitted."
            Return result
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            result.Success = False
            result.Message = ex.Message
            Return result
        End Try
    End Function

#End Region

#Region " Delete Visit Report "

    Public Function fnDeleteVisitReport(ByVal ReportID As String) As clsResult
        Dim result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spDeleteVisitReport", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ReportID", ReportID)
            cmd.ExecuteNonQuery()
            con.Close()
            result.Success = True
            result.Message = "Visit Report Deleted."
            Return result
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            result.Success = False
            result.Message = ex.Message
            Return result
        End Try
    End Function

#End Region

#Region " Give Approval "

    Public Function fnGiveApproval(ByVal ReportID As String, ByVal ApprovalRemarks As String, ByVal ApprovalStatus As String) As clsResult
        Dim result As New clsResult()
        Try
            Dim cmd As SqlCommand = New SqlCommand("spGiveApproval", con)
            con.Open()
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.AddWithValue("@ReportID", ReportID)
            cmd.Parameters.AddWithValue("@ApproverRemarks", ApprovalRemarks)
            cmd.Parameters.AddWithValue("@ApprovalStatus", ApprovalStatus)
            cmd.ExecuteNonQuery()
            con.Close()
            result.Success = True
            result.Message = "Visit Report " & ApprovalStatus
            Return result
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            result.Success = False
            result.Message = ex.Message
            Return result
        End Try
    End Function

#End Region

    Public Function fnGetVisitMailInfo(ByVal ReportID As String, ByVal ApplicationStatus As String) As clsMailProperty
        Dim sp As String = "spGetVisitMailInfo"
        Dim dr As SqlDataReader
        Dim MailProp As New clsMailProperty()
        Try
            con.Open()
            Using cmd As SqlCommand = New SqlCommand(sp, con)
                cmd.CommandType = CommandType.StoredProcedure
                cmd.Parameters.AddWithValue("@ReportID", ReportID)
                cmd.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus)
                dr = cmd.ExecuteReader()
                While dr.Read()
                    MailProp.MailSubject = dr.Item("MailSubject")
                    MailProp.MailBody = dr.Item("MailBody")
                    MailProp.MailFrom = dr.Item("MailFrom")
                    MailProp.MailTo = dr.Item("MailTo")
                    MailProp.MailCC = dr.Item("MailCC")
                    MailProp.MailBCC = dr.Item("MailBCC")
                End While
                con.Close()
                Return MailProp
            End Using
        Catch ex As Exception
            If con.State = ConnectionState.Open Then
                con.Close()
            End If
            Return Nothing
        End Try
    End Function

End Class
