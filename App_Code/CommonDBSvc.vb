Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports Microsoft.VisualBasic
Imports System
Imports System.Xml
Imports System.Configuration

Public Class CommonDBSvc
    Public passwordMsg As Boolean
    Public ds As New DataSet
    Public comLib As New CommonLib
    Public Function getColumnChar(ByVal i As Integer) As String
        Dim index As String
        index = ""
        Select Case i
            Case 1
                index = "A"
            Case 2
                index = "B"
            Case 3
                index = "C"
            Case 4
                index = "D"
            Case 5
                index = "E"
            Case 6
                index = "F"
            Case 7
                index = "G"
            Case 8
                index = "H"
            Case 9
                index = "I"
            Case 10
                index = "J"
            Case 11
                index = "K"
            Case 12
                index = "L"
            Case 13
                index = "M"
            Case 14
                index = "N"
            Case 15
                index = "O"
            Case 16
                index = "P"
            Case 17
                index = "Q"
            Case 18
                index = "R"
            Case 19
                index = "S"
            Case 20
                index = "T"
            Case 21
                index = "U"
            Case 22
                index = "V"
            Case 23
                index = "W"
            Case 24
                index = "X"
            Case 25
                index = "Y"
            Case 26
                index = "Z"
        End Select
        Return index

    End Function
    Public Function getColumnIndex(ByVal i As Integer) As String
        Dim index As String
        index = ""
        If i <= 26 Then
            index = getColumnChar(i)
        ElseIf i <= 52 Then
            index = "A" + getColumnChar(i - 26)
        ElseIf i <= 78 Then
            index = "B" + getColumnChar(i - 52)
        End If
        Return index
    End Function
    Public Function AddUpdateTable(ByVal mode As String, ByVal tableName As String, ByVal UpdateStr As String) As String
        Dim QueryStr As String
        QueryStr = "p" & mode & tableName & " " & UpdateStr

        Try
            ExecuteQuery(QueryStr)
            Return QueryStr
        Catch e As Exception
            Return QueryStr & " " & e.Message()
        End Try

    End Function

    Public Function getBranchCode(ByVal BName As String) As String
        Return GetValueFromTable("Branch", "BranchCode", "BranchName='" & BName & "'")
    End Function
    Public Function getCutomerFirstName(ByVal CID As String) As String
        Return GetValueFromTable("CustomerGeneral", "Firstname", "Customerid='" & CID & "'")
    End Function
    Public Function getCutomerLastName(ByVal CID As String) As String
        Return GetValueFromTable("CustomerGeneral", "Lastname", "Customerid='" & CID & "'")
    End Function

    Public Function getCutomerName(ByVal CID As String) As String
        Return GetValueFromTable("CustomerGeneral", "isnull(FirstName,'')+' '+isnull(Lastname,'')", "Customerid='" & CID & "'")
    End Function

    Public Function getBranchName(ByVal BCode As String) As String
        Return GetValueFromTable("Branch", "BranchName", "BranchCode=" & BCode & "")
    End Function

    Public Function getUserBranchName(ByVal UserID As String) As String
        Return GetValueFromTable("UserInfo", "BranchName", "ApplicationUserID='" & UserID & "'")
    End Function

    Public Function getAnnouncementDesc(ByVal aid As String) As String
        Return GetValueFromTable("tblAnnouncement", "Description", "aid='" & aid & "'")
    End Function

    Public Function getAnnouncementTitle(ByVal aid As String) As String
        Return GetValueFromTable("tblAnnouncement", "Title", "aid='" & aid & "'")
    End Function
    Public Function getProductName(ByVal aid As String) As String
        Return GetValueFromTable("Agreement", "ProductName", "agreementid='" & aid & "'")
    End Function
    Public Function getProductCategory(ByVal pid As String) As String
        Return GetValueFromTable("Product", "productcategory", "productname='" & pid & "'")
    End Function
    Public Function getRM(ByVal CID As String) As String
        Return GetValueFromTable("CustomerGeneral", "isnull(relationshipmanager,'')", "Customerid='" & CID & "'")
    End Function
    Public Function getWorkFlowPermission(ByVal Status As String, ByVal DocTypeID As String, ByVal FieldName As String) As String
        Dim i As Integer
        Dim Query As String
        Query = "select " + FieldName + " from statusdef where statusname='" + Status + "' and documenttypeid='" + DocTypeID + "'"
        Dim ds As New DataSet
        ds = GetDataSet(Query)
        Dim retVal As String
        retVal = ""
        Try
            If ds.Tables.Count >= 1 Then
                If ds.Tables(0).Columns.Count >= 1 Then
                    For i = 0 To ds.Tables(0).Columns.Count - 1
                        retVal = retVal & Trim(ds.Tables(0).Rows(0).Item(i)) & "|"

                    Next i
                Else
                    Return "NoData"
                End If
            Else
                Return "NoData"
            End If
        Catch e As Exception
            Return e.Message()
        End Try
        Return retVal


    End Function

    Public Function LoadTable(ByVal tableName As String, ByVal wherePart As String) As String
        Dim Query As String
        Query = "select * from " & tableName & " where " & wherePart
        Dim i As Integer

        Dim ds As New DataSet
        ds = GetDataSet(Query)

        Dim retVal As String
        retVal = ""
        Try
            If ds.Tables.Count >= 1 Then
                If ds.Tables(0).Columns.Count >= 1 Then
                    For i = 0 To ds.Tables(0).Columns.Count - 1
                        retVal = retVal & Trim(ds.Tables(0).Rows(0).Item(i)) & "|"

                    Next i
                Else
                    Return "NoData"
                End If
            Else
                Return "NoData"
            End If
        Catch e As Exception
            Return e.Message()
        End Try
        Return retVal
    End Function

    Public Function GetTranID() As String
        Dim Query As String
        Query = " select dbo.GetTransactionID() TranID"

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String
        Try
            Dim cmd As New SqlCommand(Query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then
                rd.Read()
                retVal = rd.GetString(0)
                con.Close()
                Return retVal
            Else
                con.Close()
                Return "NOVALUE"
            End If
        Catch e As Exception
            con.Close()
            Return "-ERR" & e.Message()
        End Try

    End Function

    Public Function getLastNode(ByVal workid As String, ByVal worktaskid As String, ByVal user As String) As String
        Dim Query As String
        Query = " select dbo.fGetChangeStatus ('" & worktaskid & "', '" & workid & "', '" & user & "')"

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String
        Try
            Dim cmd As New SqlCommand(Query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then
                rd.Read()
                retVal = rd.GetString(0)
                con.Close()
                Return retVal
            Else
                con.Close()
                Return "NOVALUE"
            End If
        Catch e As Exception
            con.Close()
            Return "-ERR" & e.Message()
        End Try
    End Function

    Public Function getMoneyReceiptNo() As String
        Dim query As String = "select cast(count(SplitNo)+1 as nvarchar(50)) from MoneyReceipt"
        Dim aid() As String = ExecuteQueryAndReturn(query).Split("|")
        Return "Mr-" & aid(0)
    End Function

    Public Function getChequeReceiptNo() As String
        Dim query As String = "select cast(isnull(max(cast(ChequeRecNo as bigint))+1,1) as nvarchar(50)) from ChequeMaster"
        Dim aid() As String = ExecuteQueryAndReturn(query).Split("|")
        Return aid(0)
    End Function

    Public Function getChequeReceiptNoOutbound() As String
        Dim query As String = "select cast(isnull(max(cast(ChqRcptNo as bigint))+1,1) as nvarchar(50)) from Cheque"
        Dim aid() As String = ExecuteQueryAndReturn(query).Split("|")
        Return aid(0)
    End Function

    Public Function GetConnectionString() As String
        Return GlobalVars.cl.cnString
    End Function

    Public Function GetDataSet(ByVal Query As String) As DataSet
        Try
            Dim cnString As String
            cnString = GetConnectionString()
            Dim ds As New DataSet("DataSet1")
            Dim con As New SqlConnection(cnString)
            con.Open()
            Dim da As New SqlDataAdapter(Query, con)
            da.Fill(ds)
            con.Close()
            Return ds
        Catch e As Exception
            Return ds
        End Try
    End Function

    Public Function CheckExistInTable(ByVal TableName As String, ByVal Cond As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim cmd As New SqlCommand("select * from " & TableName & " where " & Cond, con)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()

            If (rd.HasRows) Then
                con.Close()
                Return "YES"
            Else
                con.Close()
                Return "NO"
            End If
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function getAccountName(ByVal feename As String, ByVal prname As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim cmd As New SqlCommand("select AC_Name from AutoVoucherDef where product= '" & prname & "' and event= dbo.GetAutoVoucherEventforcheque('" & feename & "')", con)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()

            If (rd.HasRows) Then
                con.Close()
                Return rd.GetString(0)
            Else
                con.Close()
                Return "No"
            End If
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function prevnextstatus(ByVal TableName As String, ByVal status As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim ret As String = ""
        Dim cmd As New SqlCommand("select ApprovedStatus, RejectStatus from DocumentWorkFlow where DocumentTypeID = '" & TableName & "' and Status='" & status & "'", con)
        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()

            If (rd.HasRows) Then
                While rd.Read
                    ret = ret & rd.GetString(0) & "~" & rd.GetString(1) & "~"
                End While
                con.Close()

            End If
        Catch
            con.Close()
            Return "-ERR"
        End Try
        con.Close()
        Return ret
    End Function

    Public Function GetValueFromTable(ByVal TableName As String, ByVal ColName As String, ByVal Cond As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String
        Dim query As String
        If Trim(Cond) = "" Then
            query = "select cast(" & ColName & " as nvarchar(500)) from " & TableName
        Else
            query = "select cast(" & ColName & " as nvarchar(500)) from " & TableName & " where " & Cond
        End If
        Try
            Dim cmd As New SqlCommand(query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then
                rd.Read()
                retVal = rd.GetString(0)
                con.Close()
                Return retVal
            Else
                con.Close()
                Return "NOVALUE"
            End If
        Catch e As Exception
            con.Close()
            Return "-ERR" & e.Message() & " " & query
        End Try
    End Function

    Public Function IsExist(ByVal TableName As String, ByVal ColName As String, ByVal Cond As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String
        Dim query As String
        If Trim(Cond) = "" Then
            query = "select cast(" & ColName & " as nvarchar(50)) from " & TableName
        Else
            query = "select cast(" & ColName & " as nvarchar(50)) from " & TableName & " where " & Cond
        End If
        Try
            Dim cmd As New SqlCommand(query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then
                rd.Read()
                retVal = rd.GetString(0)
                con.Close()
                Return "yes"
            Else
                con.Close()
                Return "NOVALUE"
            End If
        Catch e As Exception
            con.Close()
            Return "-ERR" & e.Message() & " " & query
        End Try
    End Function

    Public Function ExecuteQueryAndReturnAll(ByVal Query As String) As String
        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String = ""
        Dim i As Integer = 0
        Try
            Dim cmd As New SqlCommand(Query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then
                While (rd.Read())
                    For i = 0 To rd.FieldCount - 1
                        If rd.Item(i).GetType().Name = "Decimal" Then
                            retVal = retVal & rd.GetDecimal(i) & "!"
                        ElseIf rd.Item(i).GetType().Name = "Boolean" Then
                            retVal = retVal & rd.GetBoolean(i) & "!"
                        ElseIf rd.Item(i).GetType().Name = "Int32" Then
                            retVal = retVal & rd.GetInt32(i) & "!"
                        ElseIf rd.Item(i).GetType().Name = "Int64" Then
                            retVal = retVal & rd.GetInt64(i) & "!"
                        ElseIf rd.Item(i).GetType().Name = "DateTime" Then
                            retVal = retVal & rd.GetDateTime(i) & "!"
                        Else
                            retVal = retVal & rd.GetString(i) & "!"
                        End If

                    Next
                    retVal = retVal & "@"
                End While
            Else
                con.Close()
                Return "NOVALUE"
            End If
            con.Close()
            Return retVal

        Catch
            con.Close()
            Return "-ERR" & Err.Description
        End Try
    End Function

    Public Function ExecuteQueryAndReturnFirstString(ByVal Query As String) As String
        Dim OutputString As String
        Dim retArr(2) As String
        OutputString = ""
        OutputString = ExecuteQueryAndReturn(Query)
        retArr = OutputString.Split("|")
        OutputString = retArr(0)
        Return OutputString
    End Function
    Public Function ExecuteQueryAndReturn(ByVal Query As String) As String
        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String = ""
        Dim i As Integer = 0
        Try
            Dim cmd As New SqlCommand(Query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then

                rd.Read()
                For i = 0 To rd.FieldCount - 1

                    If rd.Item(i).GetType().Name = "Decimal" Then
                        retVal = retVal & rd.GetDecimal(i) & "|"
                    ElseIf rd.Item(i).GetType().Name = "Int32" Then
                        retVal = retVal & rd.GetInt32(i) & "|"
                    Else
                        retVal = retVal & rd.GetString(i) & "|"
                    End If
                Next i
                con.Close()
                Return retVal
            Else
                con.Close()
                Return "NOVALUE"
            End If
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function GetValueFromFunction(ByVal FunctionName As String, ByVal Param As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim retVal As String
        Try
            Dim cmd As New SqlCommand("select  dbo." & FunctionName & " (" & Param & ") ", con)
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()
            If (rd.HasRows) Then
                rd.Read()
                retVal = rd.GetString(0)
                con.Close()
                Return retVal
            Else
                con.Close()
                Return "NOVALUE"
            End If
        Catch e As Exception
            con.Close()
            Return "-ERR" & e.Message()
        End Try
    End Function


    Public Function GetValueListFromTable(ByVal Query As String) As String
        Dim ds As New DataSet
        ds = GetDataSet(Query)
        Dim i As Integer
        Dim retVal As String
        Try
            retVal = ""
            For i = 0 To ds.Tables(0).Columns.Count - 1
                retVal = retVal & ds.Tables(0).Rows(0).Item(i) & "|"
            Next i

        Catch e As Exception
            Return "-ERR"
        End Try
        Return retVal
    End Function
    Public Function updateMoneyReceipt(ByVal moneyreceipt As String, ByVal entryno As String, ByVal status As String) As String
        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Try
            Dim cmd As New SqlCommand("update MoneyReceipt set Status='" & status & "' where ReceiptNo='" & moneyreceipt & "' and EntryNo=" & entryno, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Return "+OK"
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function updatecheque(ByVal mode As String, ByVal chqreceiptno As String, ByVal entryno As String, ByVal status As String, ByVal ac1 As String, ByVal ac2 As String) As String
        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim query As String = ""

        If mode = "1" Then
            query = "Update cheque set ChequeStatus = '" & status & "' and DepositBank = '" & ac1 & "' and DepositTo = '" & ac2 & "' where ChqRcptNo=" & chqreceiptno & " and EntryNo=" & entryno
        End If

        If mode = "0" Then
            query = "Update cheque set ChequeStatus = '" & status & "' where ChqRcptNo=" & chqreceiptno & " and EntryNo=" & entryno

        End If
        Try
            Dim cmd As New SqlCommand(query, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Return "+OK"
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function SetValueOfTable(ByVal TableName As String, ByVal ColName As String, ByVal UpdateValue As String, ByVal Cond As String) As String
        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Try
            Dim cmd As New SqlCommand("update " & TableName & " set " & ColName & " = " & UpdateValue & " where " & Cond, con)
            con.Open()
            cmd.ExecuteNonQuery()
            con.Close()
            Return "+OK"
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function ExecuteQuery(ByVal Query As String) As String
        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Try
            Dim cmd As New SqlCommand(Query, con)
            con.Open()
            cmd.CommandTimeout = 0
            cmd.ExecuteNonQuery()
            con.Close()
            Return "+OK"
        Catch
            con.Close()
            Return "-ERR"
        End Try
    End Function

    Public Function LoadProperty(ByVal pName As String) As String
        Dim pValue As String
        pValue = GetValueFromTable("ApplicationSetting", "pValue", " PropertyName='" & pName & "'")
        Return pValue
    End Function

    Public Function ReturnAutoComplete(ByVal tablename As String, ByVal colname As String, ByVal cond As String) As String

        Dim ret As String
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnString As String
        cnString = GetConnectionString()
        myConnection = New SqlConnection(cnString)

        myConnection.Open()
        If Trim(cond) = "" Then
            myCommand = New SqlCommand("Select distinct cast(" & colname & " as nvarchar(50)) from " & tablename, myConnection)
        Else
            myCommand = New SqlCommand("Select distinct cast(" & colname & " as nvarchar(50)) from " & tablename & " where " & cond, myConnection)
        End If
        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        ret = ""
        If (dr.HasRows()) Then
            While dr.Read()
                ret += dr(0).ToString & "|"

            End While
            'Else
            '    count = 0
        End If
        myConnection.Close()
        Return ret


    End Function

    Public Function Test(ByVal EntryID As String, ByVal Operation As String, ByVal OpString As String, ByVal Cond As String) As String
        Dim retStr As String
        retStr = ""
        If EntryID = "LeaseAsset" Then
            If (Operation = "Insert") Then
                retStr = "exec pILeaseAsset " & OpString
            ElseIf (Operation = "Update") Then
                retStr = "exec pULeaseAsset " & OpString
            ElseIf (Operation = "Delete") Then
                retStr = "exec pDLeaseAsset " & OpString
            End If
            'ds = GetDataSet("select AgreementNo [Agreement No],SplitNo [Split No],AssetID [Asset ID],AssetName [Asset Name],AssetType [Asset Type],Price [Price],[Vendor/Supplier] [Vendor/Supplier],WarrantyCoverage [Warranty Coverage],PlaceofInst [Place Of Installation],Unit [Unit],Qty [Qty],OfferNo [Offer No],OfferSplit [Offer Split],LastUpdate [Last Update], UserID [Update By] from LeaseAsset where " & Cond)
        End If
        Return retStr
    End Function

    Public Function MakeDate(ByVal fromd As Integer, ByVal tod As Integer, ByVal fromdate As String, ByVal todate As String, ByVal colname As String) As String
        Dim ret As String
        ret = ""
        If fromd = 1 And tod = 1 Then
            ret = " " & colname & ">='" & fromdate & "' and " & colname & "<='" & todate & "'"
            Return ret
        ElseIf fromd = 1 Then
            ret = " " & colname & ">='" & fromdate & "'"
            Return ret
        ElseIf tod = 1 Then
            ret = " " & colname & "<='" & todate & "'"
            Return ret
        End If

        Return "no"
    End Function

    Public Function GetFormatDate(ByVal dateValue As String) As String
        Return GetValueFromFunction("getDateFormat", "'" & dateValue & "'")
    End Function

    Public Function GetFintDate() As String
        Return GetValueFromFunction("GetFintDate", "")
    End Function

    Public Function GetBankulatorDate() As String
        Return GetValueFromFunction("GetBankulatorDate", "")
    End Function

    Public Function OpeningVoucherDate() As String
        Return GetValueFromFunction("GetOpeningVoucherDate", "")
    End Function


    Public Sub SetFintDate(ByVal d As String, ByVal m As String, ByVal y As String)
        ExecuteQuery("update ApplicationSetting set pValue='" & d & "' where PropertyName='CurrentDay'")
        ExecuteQuery("update ApplicationSetting set pValue='" & m & "' where PropertyName='CurrentMonth'")
        ExecuteQuery("update ApplicationSetting set pValue='" & y & "' where PropertyName='CurrentYear'")
    End Sub

    Public Function nextstatus(ByVal TableName As String, ByVal status As String) As String

        Dim cnStr As String = GetConnectionString()
        Dim con As New SqlConnection(cnStr)
        Dim ret As String = ""
        Dim cmd As New SqlCommand("select ApprovedStatus from DocumentWorkFlow where DocumentTypeID = '" & TableName & "' and Status='" & status & "'", con)

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()

            If (rd.HasRows) Then
                rd.Read()
                ret = rd.GetString(0)
                con.Close()

            End If
        Catch
            con.Close()
            Return "-ERR"
        End Try
        con.Close()
        Return ret
    End Function


    Public Function getTransactionID() As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnString As String
        cnString = GetConnectionString()
        myConnection = New SqlConnection(cnString)

        myConnection.Open()

        myCommand = New SqlCommand("select str(DATEPART(yy,getDate()),4)+''+ltrim(str(DATEPART(mm,getDate()),2))+''+ltrim(str(DATEPART(dd,getDate()),2))+''+ltrim(str(DATEPART(hh,getDate()),1))+''+ltrim(str(DATEPART(mi,getDate()),1))+''+ltrim(str(DATEPART(ss,getDate()),2))+''+ltrim(str(DATEPART(ms,getDate()),4))", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function


    Public Function getLedgerRecon() As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnString As String
        cnString = GetConnectionString()
        myConnection = New SqlConnection(cnString)

        myConnection.Open()

        myCommand = New SqlCommand("select PropertyValue from SettingDate where PropertyName='Reconciliation'", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    Public Function getPrimaryKey(ByVal table As String, ByVal branch As String, ByVal format As String) As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand

        myConnection = New SqlConnection(comLib.cnString)

        myConnection.Open()

        myCommand = New SqlCommand("select dbo.fgetCode1('" & table & "','" & branch & "','" & format & " ')", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function
    Public Function getConcernStuffDept(ByVal user As String) As String
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Dim rd As SqlDataReader
        Dim dept As String
        dept = ""
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "Select Department from [Employee] where Name='" & user & "'"
            rd = cmd.ExecuteReader()

            While rd.Read()
                dept = rd.GetValue(0)
            End While
            cn.Close()
        Catch ex As SqlException
            cn.Close()
        End Try
        Return dept
    End Function
    Public Function getCompanyName() As String
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Dim rd As SqlDataReader
        Dim dept As String
        dept = ""
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "exec getCompanyName"
            rd = cmd.ExecuteReader()

            While rd.Read()
                dept = rd.GetValue(0)
            End While
            cn.Close()
        Catch ex As SqlException
            cn.Close()
        End Try
        Return dept
    End Function
    Public Function getCompanyAddress() As String
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Dim rd As SqlDataReader
        Dim dept As String
        dept = ""
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "exec getCompanyAddress"
            rd = cmd.ExecuteReader()

            While rd.Read()
                dept = rd.GetValue(0)
            End While
            cn.Close()
        Catch ex As SqlException
            cn.Close()
        End Try
        Return dept
    End Function

    Public Function RemoveUserFromRole(ByVal userid As String, ByVal roleid As String) As Boolean
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "exec Pr_DelUserFromRole '" & userid & "','" & roleid & "'"
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            cn.Close()
            Return False
        End Try
    End Function
    Public Function RemoveProductFromRole(ByVal roleid As String, ByVal pname As String, ByVal userid As String) As Boolean
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "exec pDProductInRole '" & roleid & "','" & pname & "','" & userid & "'"
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            cn.Close()
            Return False
        End Try
    End Function
    Public Function RemoveUserFromUserList(ByVal userid As String) As Boolean
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Try
            cn.Open()
            cmd.Connection = cn
            cmd.CommandText = "exec prDeleteUser '" & userid & "'"
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            cn.Close()
            Return False
        End Try
    End Function
    Public Function InsertLoginLog(ByVal userid As String, ByVal action As String, ByVal termination As String, ByVal SessionID As String) As Boolean
        Dim cn As New SqlConnection(comLib.cnString)
        Dim cmd As New SqlCommand()
        Dim CurrentIPAddress As String
        CurrentIPAddress = GlobalVars.gblIPAddress
        Try
            cn.Open()
            cmd.Connection = cn
            'cmd.CommandText = "exec prInsertLoginLog '" & userid & "','" & action & "','" & termination & "'"
            cmd.CommandText = "exec prInsertLoginLog '" & userid & "','" & action & "','" & termination & "','" & SessionID & "','" & CurrentIPAddress & "','Active'"
            cmd.ExecuteNonQuery()
            Return True
        Catch ex As Exception
            cn.Close()
            Return False
        End Try
    End Function

    Public Function AssetLiabilityMaturityData(ByVal EndDate As String) As String
        Dim i As Integer
        Dim j As Integer
        Dim comDB As New CommonDBSvc
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim tablsestring As String
        tablsestring = ""

        Dim valuearray(50, 7) As String

        i = 0
        j = 0
        For i = 0 To 49
            For j = 0 To 6
                valuearray(i, j) = ""
            Next j
        Next i

        Try
            dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure

            dsmain.SelectParameters.Add("Cutoff", EndDate)
            dsmain.SelectCommand = "psAssetLiabilityMaturity"

            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
            rc = dataview.Count
            j = 0
            For i = 0 To rc - 1
                'newsheet.Range(getColumnIndex(ColIndex + 1) + RowIndex.ToString()).Value = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Cost of Borrowing"))
                valuearray(i, j + 0) = dataview.Item(i).Row.Item("Particulars").ToString()
                valuearray(i, j + 1) = dataview.Item(i).Row.Item("Balance_1Month").ToString()
                valuearray(i, j + 2) = dataview.Item(i).Row.Item("Balance_1_3Month").ToString()
                valuearray(i, j + 3) = dataview.Item(i).Row.Item("Balance_3_12Month").ToString()
                valuearray(i, j + 4) = dataview.Item(i).Row.Item("Balance_1_5Year").ToString()
                valuearray(i, j + 5) = dataview.Item(i).Row.Item("Balance_5MoreYear").ToString()
                valuearray(i, j + 6) = CDbl(valuearray(i, 1)) + CDbl(valuearray(i, 2)) + CDbl(valuearray(i, 3)) + CDbl(valuearray(i, 4)) + CDbl(valuearray(i, 5))

            Next i
            rc = i - 1

            tablsestring = "<tr><th>Particulars</th><th>Up to 1 Month Maturity</th><th>1-3 Month Maturity</th><th>3-12 Month Maturity</th><th>1-5 Month Maturity</th><th>More than 5 Year Maturity</th><th>Total</th></tr>"
            For i = 0 To rc
                If valuearray(i, 0) = "Total Assets" Then
                    tablsestring = tablsestring + "<tr style='font-weight:bold'>"
                ElseIf valuearray(i, 0) = "Total Liabilitis" Then
                    tablsestring = tablsestring + "<tr style='font-weight:bold'>"
                ElseIf valuearray(i, 0) = "Net Liquidity Excess/Shortage" Then
                    tablsestring = tablsestring + "<tr style='font-weight:bold'>"
                ElseIf valuearray(i, 0) = "Percentage of Net Liquidity Difference" Then
                    tablsestring = tablsestring + "<tr style='font-weight:bold'>"
                Else
                    tablsestring = tablsestring + "<tr>"
                End If
                tablsestring = tablsestring + "<td>" + valuearray(i, 0) + "</td>"
                tablsestring = tablsestring + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, 1))) + "</td>"
                tablsestring = tablsestring + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, 2))) + "</td>"
                tablsestring = tablsestring + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, 3))) + "</td>"
                tablsestring = tablsestring + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, 4))) + "</td>"
                tablsestring = tablsestring + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, 5))) + "</td>"
                tablsestring = tablsestring + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, 6))) + "</td>"
                tablsestring = tablsestring + "</tr>"
                'String.Format("{0:#,##0.00}", CDbl(valuearray(i, 5)))
            Next i
        Catch ex As Exception


        End Try

        Return tablsestring
    End Function

    Public Sub ExportTaxReport(ByVal empid As String, ByVal EffectiveStatus As String, ByVal startdate As String, ByVal enddate As String)
        Dim row As TableRow, cell As TableCell, header As TableHeaderCell

        Dim EmployeeName As String
        Dim filename As String
        Dim streamWriter As StreamWriter = Nothing
        Dim ComputationYear_1 As String
        Dim ComputationYear_2 As String

        Dim comdb As New CommonService
        Dim r, i As Integer
        Dim Query As String
        'Dim empid As String

        Dim Table1 As Table
        Dim Table2 As Table
        Dim Table3 As Table
        Dim Table4 As Table
        Dim TableMain As Table


        Table1 = New Table()
        Table2 = New Table()
        Table3 = New Table()
        Table4 = New Table()
        TableMain = New Table()


        EmployeeName = comdb.GetSingleOutputFunction("ExecuteQueryAndReturnFirstString", "select dbo.getEmployeeName('" + empid + "')")


        ComputationYear_1 = "Computation of Total Income & Tax Liability"
        ComputationYear_2 = "for the Income Year " + startdate.Substring(startdate.LastIndexOf("-") + 1) + "-" + enddate.Substring(enddate.LastIndexOf("-") + 3)


        Query = "exec pSTaxSalaryFinal '" + empid + "','" + startdate + "','" + enddate + "','" + EffectiveStatus + "'"


        row = New TableRow
        cell = New TableCell
        cell.Text = ""
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        Table1.Rows.Add(row)




        row = New TableRow

        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim con As New SqlConnection(cnStr)
        Dim cmd As New SqlCommand(Query, con)
        Dim type, tmpvalue As String

        con.Open()
        cmd.ExecuteNonQuery()

        Dim rd As SqlDataReader
        rd = cmd.ExecuteReader()
        If rd.HasRows Then
            r = 0
            Do While (rd.Read())
                If (r = 0) Then
                    For i = 0 To rd.FieldCount - 1
                        tmpvalue = rd.GetName(i).ToString()
                        header = New TableHeaderCell
                        header.Text = tmpvalue
                        header.BorderWidth = "1"
                        row.Cells.Add(header)
                    Next i
                    Table1.Rows.Add(row)
                End If
                r = r + 1

                row = New TableRow

                For i = 0 To rd.FieldCount - 1
                    type = rd.Item(i).GetType().ToString()
                    tmpvalue = rd.GetValue(i).ToString()

                    cell = New TableCell
                    cell.Text = tmpvalue
                    cell.BorderWidth = "1"
                    row.Cells.Add(cell)
                Next i
                Table1.Rows.Add(row)
            Loop
        End If
        rd.Close()
        '#############################
        row = New TableRow
        cell = New TableCell
        cell.Text = ""
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        Table2.Rows.Add(row)

        row = New TableRow
        Query = "exec pSTaxExceslPaid '" + empid + "','" + startdate + "','" + enddate + "','" + EffectiveStatus + "'"
        cmd = New SqlCommand(Query, con)
        cmd.ExecuteNonQuery()


        rd = cmd.ExecuteReader()
        If rd.HasRows Then
            r = 0
            Do While (rd.Read())
                If (r = 0) Then
                    For i = 0 To rd.FieldCount - 1
                        tmpvalue = rd.GetName(i).ToString()
                        header = New TableHeaderCell
                        header.Text = tmpvalue
                        header.BorderWidth = "1"
                        row.Cells.Add(header)
                    Next i
                    Table2.Rows.Add(row)
                End If
                r = r + 1

                row = New TableRow

                For i = 0 To rd.FieldCount - 1
                    type = rd.Item(i).GetType().ToString()
                    tmpvalue = rd.GetValue(i).ToString()

                    cell = New TableCell
                    cell.Text = tmpvalue
                    cell.BorderWidth = "1"
                    row.Cells.Add(cell)
                Next i
                Table2.Rows.Add(row)
            Loop
        End If
        rd.Close()
        '########

        '#############################
        row = New TableRow
        cell = New TableCell
        cell.Text = ""
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        Table3.Rows.Add(row)

        row = New TableRow
        Query = "exec pSTaxRule '" + empid + "','" + startdate + "','" + enddate + "','" + EffectiveStatus + "'"
        cmd = New SqlCommand(Query, con)
        cmd.ExecuteNonQuery()


        rd = cmd.ExecuteReader()
        If rd.HasRows Then
            r = 0
            Do While (rd.Read())
                If (r = 0) Then
                    For i = 0 To rd.FieldCount - 1
                        tmpvalue = rd.GetName(i).ToString()
                        header = New TableHeaderCell
                        header.Text = tmpvalue
                        header.BorderWidth = "1"
                        row.Cells.Add(header)
                    Next i
                    Table3.Rows.Add(row)
                End If
                r = r + 1

                row = New TableRow

                For i = 0 To rd.FieldCount - 1
                    type = rd.Item(i).GetType().ToString()
                    tmpvalue = rd.GetValue(i).ToString()

                    cell = New TableCell
                    cell.Text = tmpvalue
                    cell.BorderWidth = "1"
                    row.Cells.Add(cell)
                Next i
                Table3.Rows.Add(row)
            Loop
        End If
        rd.Close()
        '########
        '#############################
        row = New TableRow
        cell = New TableCell
        cell.Text = ""
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        Table4.Rows.Add(row)

        row = New TableRow
        Query = "exec pSTAXInvestment '" + empid + "','" + startdate + "','" + enddate + "','" + EffectiveStatus + "'"
        cmd = New SqlCommand(Query, con)
        cmd.ExecuteNonQuery()


        rd = cmd.ExecuteReader()
        If rd.HasRows Then
            r = 0
            Do While (rd.Read())
                If (r = 0) Then
                    For i = 0 To rd.FieldCount - 1
                        tmpvalue = rd.GetName(i).ToString()
                        header = New TableHeaderCell
                        header.Text = tmpvalue
                        header.BorderWidth = "1"
                        row.Cells.Add(header)
                    Next i
                    Table4.Rows.Add(row)
                End If
                r = r + 1

                row = New TableRow

                For i = 0 To rd.FieldCount - 1
                    type = rd.Item(i).GetType().ToString()
                    tmpvalue = rd.GetValue(i).ToString()

                    cell = New TableCell

                    cell.Text = tmpvalue
                    cell.BorderWidth = "1"
                    row.Cells.Add(cell)
                Next i
                Table4.Rows.Add(row)
            Loop
        End If
        rd.Close()
        '########

        row = New TableRow
        cell = New TableCell
        cell.Text = EmployeeName
        cell.Font.Bold = True
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)

        row = New TableRow
        cell = New TableCell
        cell.Text = ComputationYear_1
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)

        row = New TableRow
        cell = New TableCell
        cell.Text = ComputationYear_2
        cell.HorizontalAlign = HorizontalAlign.Center
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)



        row = New TableRow
        cell = New TableCell
        cell.Text = ""
        cell.BorderWidth = "0"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)

        row = New TableRow
        cell = New TableCell
        cell.Controls.Add(Table1)
        cell.BorderWidth = "1"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)

        row = New TableRow
        cell = New TableCell
        cell.Controls.Add(Table2)
        cell.BorderWidth = "1"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)

        row = New TableRow
        cell = New TableCell
        cell.Controls.Add(Table3)
        cell.BorderWidth = "1"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)

        row = New TableRow
        cell = New TableCell
        cell.Controls.Add(Table4)
        cell.BorderWidth = "1"
        row.Cells.Add(cell)
        TableMain.Rows.Add(row)


        con.Close()

        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.Charset = ""

        Dim rootdirectory As String
        Dim commmonDB As New CommonDBSvc
        rootdirectory = commmonDB.LoadProperty("ExcelReportDirectory") 'F:\APSIS Project Code Database\.Net Products\BankulatorProject\"
        'rootdirectory = "F:\APSIS Project Code Database\.Net Products\BankulatorProject\CIBFiles\"

        Dim sb As System.Text.StringBuilder
        sb = New System.Text.StringBuilder()

        Dim sw As StringWriter = New StringWriter(sb)
        Dim htmltw As HtmlTextWriter = New HtmlTextWriter(sw)
        TableMain.RenderControl(htmltw)

        Dim w As System.IO.TextWriter
        Dim FILE1 As String


        FILE1 = "Tax_" + EffectiveStatus + EmployeeName + "_" + startdate.Substring(startdate.LastIndexOf("-") + 1) + "-" + enddate.Substring(enddate.LastIndexOf("-") + 3) + ".xls"
        filename = rootdirectory + "Tax_" + EffectiveStatus + EmployeeName + "_" + startdate.Substring(startdate.LastIndexOf("-") + 1) + "-" + enddate.Substring(enddate.LastIndexOf("-") + 3) + ".xls"

        w = New System.IO.StreamWriter(filename, False)
        w.Write(sb.ToString())
        w.Flush()
        w.Close()
        Dim qry As String
        qry = "EXEC pITaxFileList " + empid + ",'" + startdate + "','" + enddate + "','" + EffectiveStatus + "','" + FILE1 + "','" + GlobalVars.UserID + "'"
        comdb.ExecuteQuery(qry)

    End Sub

End Class

