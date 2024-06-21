Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
<System.Web.Script.Services.ScriptService()> _
Public Class CommonService
    Inherits System.Web.Services.WebService

    <WebMethod()> _
Public Function GetBankulatorDate() As String
        Dim comDB As New CommonDBSvc
        Dim rcptdate As String
        'test comment by Abdullah
        'another comment
        rcptdate = ""
        Try
            rcptdate = comDB.GetBankulatorDate()
        Catch ex As Exception
            rcptdate = -1

        End Try
        Return rcptdate

    End Function

    <WebMethod()> _
    Public Function GetAutoCompleteList(ByVal key As String) As String

        Dim mySql As String
        Dim cnstring As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim objConn As New SqlConnection(cnstring)
        Dim result As String = ""
        Dim keyfields(3) As String
        keyfields = key.Split("|")
        Dim myds As New DataSet("ACdata")
        If keyfields(2).Trim() = "" Then
            mySql = "SELECT DISTINCT " & keyfields(1) & " FROM " & keyfields(0)
            '& " WHERE " & keyfields(1) & " like '" + term + "%'"
        Else
            mySql = "SELECT DISTINCT " & keyfields(1) & " FROM " & keyfields(0) & " WHERE " & keyfields(2) & ""
            ' and " & keyfields(1) & " like '" + term + "%'"
        End If

        objConn.Open()
        Dim adapter As New SqlClient.SqlDataAdapter(mySql, objConn)
        adapter.Fill(myds, "ACdata")
        For Each dr As DataRow In myds.Tables(0).Rows
            result = result & dr.Item(0).ToString() & "|"
        Next
        objConn.Close()
        If (Len(result) < 2) Then
            Return ""
        End If
        Return Left(result, Len(result) - 1)
    End Function

    <WebMethod()> _
    Public Function GetPrimaryKey(ByVal source As String) As String
        Dim format As String
        format = "NA"
        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        myConnection = New SqlConnection(cnStr)

        myConnection.Open()

        myCommand = New SqlCommand("select dbo.fgetCode1('" & source & "','Main','" & format & " ')", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    <WebMethod()> _
    Public Function GetChequeReceiptNo(ByVal userid As String) As String
        Dim comDB As New CommonDBSvc
        Dim rcptno As String
        rcptno = ""
        Try
            rcptno = comDB.getChequeReceiptNo()
        Catch ex As Exception
            rcptno = -1

        End Try
        Return rcptno

    End Function

    <WebMethod()> _
    Public Function GetChildData(ByVal tableName As String, ByVal functionName As String, ByVal InputString As String) As String
        Dim comDB As New CommonDBSvc
        Dim OutputString As String
        OutputString = ""
        Try
            'All the functions used Need to get a block
            If functionName = "ExecuteQueryAndReturn" Then
                Dim retArr(2) As String
                OutputString = comDB.ExecuteQueryAndReturn(InputString)
                retArr = OutputString.Split("|")
                OutputString = retArr(0)
            ElseIf functionName = "ExecuteQueryAndReturnAll" Then
                OutputString = comDB.ExecuteQueryAndReturnAll(InputString)
            ElseIf functionName = "ExecuteQuery" Then
                OutputString = comDB.ExecuteQuery(InputString)
            ElseIf functionName = "GetValueFromTable" Then
                Dim cond(3) As String
                cond = InputString.Split("|")
                OutputString = comDB.GetValueFromTable(cond(0), cond(1), cond(2))

            End If

        Catch ex As Exception

        End Try

        If (OutputString <> "NOVALUE") Then

            Dim pos As Integer
            Dim epos As Integer
            Dim substr As String
            Dim collist As String
            Dim col As String
            collist = ""
            'tableName = "afl_director"
            'OutputString = "<afl_director><DirectorID>1</DirectorID><AFL_ID>jhjh</AFL_ID><DirectorName>dir</DirectorName><DirectorAddress>HJJ</DirectorAddress><DOB>2000-01-01T00:00:00</DOB><Nationality>BD</Nationality><Share_Percent>12.00</Share_Percent><Position>jkjkj</Position><PersonalNetWorth>78.00</PersonalNetWorth><LastUpdate>2010-12-04T07:11:34.083</LastUpdate></afl_director><afl_director><DirectorID>10</DirectorID>  <AFL_ID>43242</AFL_ID><DirectorName>43434</DirectorName><DirectorAddress>e2e</DirectorAddress><DOB>2010-02-02T00:00:00</DOB><Nationality>23</Nationality><Share_Percent>12.00</Share_Percent><Position>fdf</Position>  <PersonalNetWorth>323.00</PersonalNetWorth><LastUpdate>2010-12-04T08:53:17.367</LastUpdate></afl_director>"
            OutputString = OutputString.Replace(tableName, "r")
            pos = OutputString.IndexOf("</r>")
            substr = OutputString.Substring(3, pos - 3)
            While (substr.Length > 0)
                pos = substr.IndexOf("<")
                epos = substr.IndexOf(">")
                col = substr.Substring(pos + 1, epos - 1)
                OutputString = OutputString.Replace(col, "c")
                collist = collist + "<c>" + col + "</c>"
                col = "</" + col + ">"
                substr = substr.Substring(substr.IndexOf(col) + col.Length)

            End While
            OutputString = "<r>" + collist + "</r>" + OutputString
            OutputString = OutputString.Replace("<c>", "")
            OutputString = OutputString.Replace("<r>", "")
            OutputString = OutputString.Replace("</c>", "|")
            'OutputString = OutputString.Replace("</r>", "|")
        End If
        Return OutputString

    End Function

    <WebMethod()> _
    Public Function GetAutoNumber(ByVal source As String, ByVal format As String) As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        myConnection = New SqlConnection(cnStr)

        myConnection.Open()

        myCommand = New SqlCommand("select dbo.fgetCode1('" & source & "','Main','" & format & "')", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    <WebMethod()> _
    Public Function GetAutoNumberV2(ByVal source As String, ByVal BranchName As String, ByVal format As String, ByVal AddParams As String) As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        myConnection = New SqlConnection(cnStr)

        myConnection.Open()

        'myCommand = New SqlCommand("select dbo.fgetCode1('" & source & "','Main','" & format & "')", myConnection)
        'myCommand = New SqlCommand("select dbo.fgetCode2('" & source & "','" & BranchName & "','" & format & "','" & AddParams & "')", myConnection)    'ahsan
        myCommand = New SqlCommand("select dbo.fgetCode2('" & AddParams & "','" & BranchName & "','" & format & "','" & AddParams & "')", myConnection)    'ahsan

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    <WebMethod()> _
    Public Function GetAutoNumberAndCheckAgreement(ByVal source As String, ByVal format As String, ByVal AgreementID As String) As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        myConnection = New SqlConnection(cnStr)

        myConnection.Open()

        myCommand = New SqlCommand("select dbo.fgetCode1('" & source & "','Main','" & format & " ')+'!'+dbo.CheckAgreementIDInUse('" + AgreementID + "') info", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    <WebMethod()> _
    Public Function GetAutoNumberAndCheckCustomer(ByVal source As String, ByVal format As String, ByVal CustomerID As String) As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        myConnection = New SqlConnection(cnStr)

        myConnection.Open()

        myCommand = New SqlCommand("select dbo.fgetCode1('" & source & "','Main','" & format & " ')+'!'+dbo.CheckCustomerIDInUse('" + CustomerID + "') info", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    <WebMethod()> _
    Public Function GetAutoNumberAndCheckDuplicate(ByVal source As String, ByVal format As String, ByVal ID As String) As String

        Dim myConnection As SqlConnection
        Dim myCommand As SqlCommand
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        myConnection = New SqlConnection(cnStr)

        myConnection.Open()

        myCommand = New SqlCommand("exec GetIDAndCheckIDInUse '" & ID & "','" & source & "','" & format & "'", myConnection)

        Dim dr As SqlDataReader
        dr = myCommand.ExecuteReader()

        dr.Read()
        Return dr.GetString(0)

    End Function

    <WebMethod()> _
    Public Function GetColumnWiseData(ByVal Query As String) As String
        Dim Str, ColName, ColType, ColValue As String
        Dim tmpdatevalue As DateTime
        Dim tmpID, tmpvalue As String
        Dim i, r As Integer
        Dim val As Decimal
        Dim rd As SqlDataReader
        Try

            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            Str = ""
            tmpID = ""
            con.Open()
            cmd.ExecuteNonQuery()
            rd = cmd.ExecuteReader()
            r = 0
            ColName = ""
            ColType = ""
            ColValue = ""
            If rd.HasRows Then
                Do While (rd.Read())
                    If r > 0 Then
                        Str = Str & "@"
                    End If
                    For i = 0 To rd.FieldCount - 1
                        ColName = rd.GetName(i).ToString()
                        ColType = rd.Item(i).GetType().ToString()

                        If ColType = "System.Decimal" Then
                            val = rd.GetDecimal(i)
                            If (ColName = "ScheduleIntRate") Then
                                ColValue = String.Format("{0:#,##0.00000000}", val)
                            Else
                                ColValue = String.Format("{0:#,##0.00}", val)
                            End If

                        ElseIf ColType = "System.Int32" Then
                            val = rd.GetInt32(i)
                            ColValue = String.Format("{0:#,##0}", val)
                        ElseIf ColType = "System.DateTime" Then
                            tmpdatevalue = rd.GetDateTime(i).ToString()
                            ColValue = Format(tmpdatevalue, "dd-MMM-yyyy")
                        Else
                            tmpvalue = rd.GetValue(i).ToString()
                            ColValue = tmpvalue.Replace("&", "and")
                        End If

                        Str = Str & ColName & "|" & ColType & "|" & ColValue & "|"
                    Next i
                    r = r + 1
                Loop

            Else
                Str = "No Record Exists!!!"
            End If
            con.Close()
            Return Str
        Catch ex As Exception
            Str = "No Record Exists!!!"
            Return Str
        End Try

    End Function

    <WebMethod()> _
    Public Function GetDynamicTable(ByVal Query As String, ByVal GridHeaders As String, ByVal opt As String, ByVal index As String, ByVal LoadDataIndex As Integer) As String
        Dim str1, filterboxstr, str2, HaveRowIndex As String
        Dim DataLength(150) As Integer
        Dim GrdHeader(150) As String
        Dim type, tmpID, tmpvalue As String
        Dim tmpdatevalue As DateTime
        Dim i, r As Integer
        Dim val As Decimal
        Dim rd As SqlDataReader
        Try
            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            tmpID = ""
            con.Open()
            cmd.ExecuteNonQuery()
            str1 = ""
            str2 = ""
            filterboxstr = ""
            HaveRowIndex = "No"
            rd = cmd.ExecuteReader()
            If GridHeaders <> "" Then
                GrdHeader = GridHeaders.Split("|")
            End If
            If rd.HasRows Then
                r = 0
                Do While (rd.Read())
                    If (r = 0 And LoadDataIndex = 1) Then
                        If opt = "5" Then
                            str1 += "<tr visible=""false"" id=""HeaderRow" & index & """>"
                        ElseIf opt = "3" Then
                            str1 += "<tr visible=""false"" id=""" & index & "HeaderRow" & index & """>"
                        Else
                            str1 += "<tr visible=""false"" id=""HeaderRow"">"
                        End If
                        For i = 0 To rd.FieldCount - 1
                            DataLength(i) = 0
                            tmpvalue = rd.GetName(i).ToString()
                            If tmpvalue = "Row Index" Then
                                HaveRowIndex = "yes"
                                str1 += "<th id=""RowIndexHeader"" title=""" & tmpvalue & """><a href='#' title=""Click Header to Sort"">"
                            Else
                                str1 += "<th  title=""" & tmpvalue & """><a href='#' title=""Click Header to Sort"">"
                            End If
                            If GridHeaders <> "" Then
                                str1 += GrdHeader(i)
                            Else
                                str1 += tmpvalue
                            End If
                            str1 += "</a></th>"
                        Next i
                        If opt = "2" Or opt = "5" Then
                            str1 += "<th id=""DelHeader"">Delete</th>"
                        End If
                        str1 += "</tr>"
                    End If
                    r = r + 1
                    If (LoadDataIndex = 1) Then
                        If opt = "1" Then
                            str2 += "<tr id=""row" & r & """>"
                        ElseIf opt = "2" Then
                            str2 += "<tr id=""row" & r & """ onmouseover=""this.style.cursor='hand'"" onclick=""RowSelected(" & r & ",0);"">"
                        ElseIf opt = "3" Then
                            str2 += "<tr id=""" + index + "row" & r & """ onmouseover=""this.style.cursor='hand'"" onclick=""RowSelected(" & r & ",'" & index & "');"">"
                        ElseIf opt = "4" Then
                            str2 += "<tr id=""row" & r & """ onmouseover=""this.style.cursor='hand'"" onclick=""RowSelected('Main'," & r & ");"">"
                        ElseIf opt = "5" Then
                            str2 += "<tr id=""row" & index & "-" & r & """ onmouseover=""this.style.cursor='hand'"" onclick=""RowSelected(" & r & "," & index & ");"">"
                        ElseIf opt = "6" Then
                            str2 += "<tr id=""row" & index & "-" & r & """ onmouseover=""this.style.cursor='hand'"" >"
                        End If
                    Else
                        str2 += "<tr>"
                    End If

                    For i = 0 To rd.FieldCount - 1
                        type = rd.Item(i).GetType().ToString()
                        If (LoadDataIndex = 1) Then
                            If i = 0 And HaveRowIndex = "yes" Then
                                tmpID = " id=""RowIndex" & r & """ "
                            Else
                                tmpID = ""
                            End If
                        End If

                        If type = "System.Decimal" Then
                            If (LoadDataIndex = 1) Then
                                str2 += "<td " & tmpID & " align=""right"">"
                            Else
                                str2 += "<td>"
                            End If
                            val = rd.GetDecimal(i)
                            str2 += String.Format("{0:#,##0.00}", val)
                            If DataLength(i) < Len(val.ToString()) Then
                                DataLength(i) = Len(val.ToString())
                            End If
                        ElseIf type = "System.Int32" Then
                            If (LoadDataIndex = 1) Then
                                str2 += "<td " & tmpID & " align=""right"">"
                            Else
                                str2 += "<td>"
                            End If

                            val = rd.GetInt32(i)
                            str2 += String.Format("{0:#,##0}", val)
                            If DataLength(i) < Len(val.ToString()) Then
                                DataLength(i) = Len(val.ToString())
                            End If
                        ElseIf type = "System.DateTime" Then
                            If (LoadDataIndex = 1) Then
                                str2 += "<td " & tmpID & " align=""left"">"
                            Else
                                str2 += "<td>"
                            End If
                            tmpdatevalue = rd.GetDateTime(i).ToString()
                            tmpvalue = Format(tmpdatevalue, "dd-MMM-yyyy")
                            str2 += tmpvalue
                            If DataLength(i) < Len(tmpvalue) Then
                                DataLength(i) = Len(tmpvalue)
                            End If
                        Else
                            tmpvalue = rd.GetValue(i).ToString()

                            If (LoadDataIndex = 1) Then
                                str2 += "<td " & tmpID & " align=""left"">"
                            Else
                                str2 += "<td>"
                            End If
                            str2 += tmpvalue.Replace("&", "and")

                            If DataLength(i) < Len(tmpvalue) Then
                                DataLength(i) = Len(tmpvalue)
                            End If
                        End If
                        If (LoadDataIndex = 1) Then
                            str2 += "</td>"
                        End If
                    Next i
                    If (LoadDataIndex = 1) Then
                        If opt = "2" Then
                            str2 += "<td id=""DelCol" & r & """><img src=""Images/Delicon.jpg"" onclick=""DeleteData(" & r & ",0);""/></td>"
                        ElseIf opt = "5" Then
                            str2 += "<td id=""DelCol" & r & """><img src=""Images/Delicon.jpg"" onclick=""DeleteData(" & r & "," & index & ");""/></td>"
                        ElseIf opt = "6" Then
                            str2 += "<td id=""DelCol" & r & """><img src=""Images/Delicon.jpg"" onclick=""DeleteData(" & r & ");""/></td>"
                        End If
                    Else
                        If opt = "2" Then
                            str2 += "<td><img src=""Images/Delicon.jpg"" onclick=""DeleteData(" & ((LoadDataIndex - 1) * 100 + r) & ",0);""/>"
                        ElseIf opt = "5" Then
                            str2 += "<td><img src=""Images/Delicon.jpg"" onclick=""DeleteData(" & ((LoadDataIndex - 1) * 100 + r) & "," & index & ");""/>"
                        ElseIf opt = "6" Then
                            str2 += "<td><img src=""Images/Delicon.jpg"" onclick=""DeleteData(" & r & ");""/>"
                        End If
                    End If
                    If (LoadDataIndex = 1) Then
                        str2 += "</tr>"
                    End If

                Loop
                If LoadDataIndex = 1 Then
                    str2 += "</tbody>"
                End If
                If opt <> "6" And opt <> "5" And opt <> "3" And opt <> "4" And LoadDataIndex = 1 Then
                    filterboxstr += "^<tr id=""FilterBox"">"
                    For i = 0 To rd.FieldCount - 1
                        filterboxstr += "<th><input id=""filterBox" & i & """ value="""" maxlength=""30"" size=""" & DataLength(i) & """ type=""text"" /></th>"
                    Next i
                    filterboxstr += "</tr>^"
                End If
                If LoadDataIndex = 1 Then
                    str1 = str1 & filterboxstr & "</thead><tbody id=""tbody1"">"
                End If
            Else
                str1 = ""
                str2 = "No Record Exists!!!"
            End If
            con.Close()
            str1 = str1 & str2
            Return str1
        Catch ex As Exception
            str2 = "No Record Exists!!!"
            Return str2
        End Try

    End Function

    <WebMethod()> _
    Public Function GetDynamicTable2(ByVal Query As String, ByVal GridHeaders As String) As String
        Dim str1, str2 As String
        Dim DataLength(50) As Integer
        Dim GrdHeader(50) As String
        Dim type, tmpID, tmpvalue, colname As String
        Dim tmpdatevalue As DateTime
        Dim i, r As Integer
        Dim val As Decimal
        Dim rd As SqlDataReader
        Try
            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            tmpID = ""
            con.Open()
            cmd.ExecuteNonQuery()
            str1 = ""
            str2 = ""
            rd = cmd.ExecuteReader()
            If GridHeaders <> "" Then
                GrdHeader = GridHeaders.Split("|")
            End If
            str1 += "<table id=""tbldynamictable2"" border=""1"" class=""yui"">"
            If rd.HasRows Then
                r = 0
                Do While (rd.Read())
                    If (r = 0) Then
                        str1 += "<thead>"
                        For i = 0 To rd.FieldCount - 1
                            DataLength(i) = 0
                            tmpvalue = rd.GetName(i).ToString()
                            str1 += "<th><b>"
                            If GridHeaders <> "" Then
                                str1 += GrdHeader(i)
                            Else
                                str1 += tmpvalue
                            End If
                            str1 += "</b></th>"
                        Next i
                        str1 += "</thead>"
                    End If
                    str2 += "<tr>"
                    r = r + 1

                    For i = 0 To rd.FieldCount - 1
                        type = rd.Item(i).GetType().ToString()
                        colname = rd.GetName(i)
                        tmpID = " id=""row" & (r - 1) & "-" & colname & """ "
                        If type = "System.Decimal" Then
                            str2 += "<td " & tmpID & " align=""right"">"
                            val = rd.GetDecimal(i)
                            str2 += String.Format("{0:#,##0.00}", val)
                            If DataLength(i) < Len(val.ToString()) Then
                                DataLength(i) = Len(val.ToString())
                            End If
                        ElseIf type = "System.Int32" Then
                            str2 += "<td " & tmpID & " align=""right"">"

                            val = rd.GetInt32(i)
                            str2 += String.Format("{0:#,##0}", val)
                            If DataLength(i) < Len(val.ToString()) Then
                                DataLength(i) = Len(val.ToString())
                            End If
                        ElseIf type = "System.DateTime" Then
                            str2 += "<td " & tmpID & " align=""left"">"
                            tmpdatevalue = rd.GetDateTime(i).ToString()
                            tmpvalue = Format(tmpdatevalue, "dd-MMM-yyyy")
                            str2 += tmpvalue
                            If DataLength(i) < Len(tmpvalue) Then
                                DataLength(i) = Len(tmpvalue)
                            End If
                        Else
                            tmpvalue = rd.GetValue(i).ToString()

                            str2 += "<td " & tmpID & " align=""left"">"
                            str2 += tmpvalue.Replace("&", "and")

                            If DataLength(i) < Len(tmpvalue) Then
                                DataLength(i) = Len(tmpvalue)
                            End If
                        End If
                        str2 += "</td>"
                    Next i
                    str2 += "</tr>"

                Loop
                str2 += "</table>"

            Else
                str1 = ""
                str2 = "No Record Exists!!!"
            End If
            con.Close()
            str1 = str1 & str2
            Return str1
        Catch ex As Exception
            str2 = "No Record Exists!!!"
            Return str2
        End Try

    End Function

    <WebMethod()> _
    Public Function GetDynamicTableHeader_V1(ByVal Query As String, ByVal isCheckBox As Boolean, ByVal index As String) As String
        Dim str1 As String
        Dim FilterBoxData(50) As String
        Dim tmpID, tmpvalue As String
        Dim i, r As Integer

        Dim rd As SqlDataReader
        Dim TotalCol As Integer
        Try
            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            tmpID = ""
            con.Open()
            cmd.ExecuteNonQuery()
            str1 = ""
            rd = cmd.ExecuteReader()

            If rd.HasRows Then
                r = 0
                TotalCol = rd.FieldCount
                str1 += "<table id=""" + index + "dynamictable"" border=""1"" class=""yui"" title=""" & TotalCol & """>"
                rd.Read()

                str1 += "<thead id=""tableheaderrow"">"
                If (isCheckBox = True) Then
                    str1 += "<th><b>Select</b></th>"
                End If
                For i = 0 To rd.FieldCount - 1
                    tmpvalue = rd.GetName(i).ToString()
                    str1 += "<th id=""" + Replace(tmpvalue, " ", "") + """ onclick=""SortData(this.id);"" title=""" + tmpvalue + """><b>"
                    str1 += tmpvalue
                    str1 += "</b></th>"
                Next i

                If (isCheckBox = True) Then
                    str1 += "<tr id=""" + index + "FilterBox""><th></th><th></th>"
                Else
                    str1 += "<tr id=""" + index + "FilterBox""><th></th>"
                End If
                For i = 1 To rd.FieldCount - 1
                    str1 += "<th><input id=""" + index + "filterBox" & i & """ value="""" maxlength=""30"" size=""20"" type=""text"" onkeyup=""GetFilteredData(" + index + ");"" title=""" & rd.GetName(i).ToString() & """ /></th>"
                Next i
                str1 += "</tr>"
                str1 += "</thead>"
                If (isCheckBox = True) Then
                    str1 += "<tbody id=""" + index + "dynamictablebody""></tbody><tfoot><tr id=""" + index + "pagerOne""><td colspan=""" & CStr(TotalCol + 1) & """><img src=""_assets/img/first.png"" class=""first"" onclick=""GetFirstPage(" + index + ");""/><img src=""_assets/img/prev.png"" class=""prev""  onclick=""GetPrevPage(" + index + ");""/><input type=""text"" readonly=""readonly"" id=""" + index + "pagedisplaytext"" class=""pagedisplay"" value=""0""/><img src=""_assets/img/next.png"" class=""next""  onclick=""GetNextPage(" + index + ");""/><img src=""_assets/img/last.png"" class=""last""  onclick=""GetLastPage(" + index + ");""/></td></tr></tfoot></table>"
                Else
                    str1 += "<tbody id=""" + index + "dynamictablebody""></tbody><tfoot><tr id=""" + index + "pagerOne""><td colspan=""" & CStr(TotalCol) & """><img src=""_assets/img/first.png"" class=""first"" onclick=""GetFirstPage(" + index + ");""/><img src=""_assets/img/prev.png"" class=""prev""  onclick=""GetPrevPage(" + index + ");""/><input type=""text"" readonly=""readonly"" id=""" + index + "pagedisplaytext"" class=""pagedisplay"" value=""0""/><img src=""_assets/img/next.png"" class=""next""  onclick=""GetNextPage(" + index + ");""/><img src=""_assets/img/last.png"" class=""last""  onclick=""GetLastPage(" + index + ");""/></td></tr></tfoot></table>"
                End If
            Else
                str1 = "No Record Exists!!!"
            End If
            con.Close()
            Return str1
        Catch ex As Exception
            str1 = "No Record Exists!!!"
            Return str1
        End Try

    End Function


    <WebMethod()> _
    Public Function GetDynamicTableHeader(ByVal Query As String, ByVal isCheckBox As Boolean, ByVal index As String) As String
        Dim str1 As String
        Dim FilterBoxData(50) As String
        Dim tmpID, tmpvalue, tmptype As String
        Dim i, r As Integer

        Dim rd As SqlDataReader
        Dim TotalCol As Integer
        Try
            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            tmpID = ""
            con.Open()
            cmd.ExecuteNonQuery()
            str1 = ""
            rd = cmd.ExecuteReader()

            If rd.HasRows Then
                r = 0
                TotalCol = rd.FieldCount
                str1 += "<table id=""" + index + "dynamictable"" border=""1"" class=""yui"" title=""" & TotalCol & """>"
                rd.Read()

                str1 += "<thead id=""tableheaderrow"">"
                If (isCheckBox = True) Then
                    str1 += "<th><b>Select</b></th>"
                End If
                For i = 0 To rd.FieldCount - 1
                    tmpvalue = rd.GetName(i).ToString()
                    tmptype = rd.Item(i).GetType().ToString()

                    str1 += "<th id=""" + i.ToString() + Replace(tmpvalue, " ", "") + """ title=""" + tmpvalue + """ onmouseover=""ShowHeaderOptions(this.title)"" onmouseout=""HideHeaderOptions(this.title)"">"
                    str1 += "<span id=""Span" + i.ToString() + Replace(tmpvalue, " ", "") + """ onclick=""SortData('" + i.ToString() + Replace(tmpvalue, " ", "") + "');"" ><b>" + tmpvalue + "</b></span>"
                    str1 += "<input type=""hidden"" value=""" + tmptype + """ id=""HiddenType" + i.ToString() + Replace(tmpvalue, " ", "") + """/>"
                    If (i > 0) Then
                        str1 += "&nbsp;&nbsp;<img id=""" + i.ToString() + "FilterDownArrow" + Replace(tmpvalue, " ", "") + """ src=""Images/DownArrow.png"" onclick=""ShowAdvancedFilter(this.id,'" + tmpvalue + "');""/></br><span id=""imgspan" + Replace(tmpvalue, " ", "") + """ style=""display:none;"">"
                        str1 += "<img  title=""" + i.ToString() + Replace(tmpvalue, " ", "") + """ id=""" + i.ToString() + "imgLeftShift" + Replace(tmpvalue, " ", "") + """ src=""Images/LeftShiftColumn.png"" onclick=""LeftShiftColumn(this.id);""/>&nbsp;<img title=""" + i.ToString() + Replace(tmpvalue, " ", "") + """ id=""" + i.ToString() + "imgHideColumn" + Replace(tmpvalue, " ", "") + """ src=""Images/HideColumn.png"" onclick=""HideColumn(this.id);""/>&nbsp;<img title=""" + i.ToString() + Replace(tmpvalue, " ", "") + """ id=""" + i.ToString() + "imgRightShift" + Replace(tmpvalue, " ", "") + """ src=""Images/RightShiftColumn.png"" onclick=""RightShiftColumn(this.id);""/></span>"
                    End If
                    str1 += "</th>"

                Next i

                If (isCheckBox = True) Then
                    str1 += "<tr id=""" + index + "FilterBox""><th></th><th></th>"
                Else
                    str1 += "<tr id=""" + index + "FilterBox""><th></th>"
                End If
                For i = 1 To rd.FieldCount - 1
                    str1 += "<th id=""FilterHeader" + i.ToString() + """><input id=""" + index + "filterBox" & i & """ value="""" maxlength=""25"" type=""text"" onkeyup=""GetFilteredData(" + index + ");"" title=""" & rd.GetName(i).ToString() & """ /></br><div id=""AdvancedFilterBoxDiv" + i.ToString() + """ style=""position:absolute;background-color:silver;border:solid 1px grey;display:none;""></div></th>"
                Next i
                str1 += "</tr>"
                str1 += "</thead>"
                If (isCheckBox = True) Then
                    str1 += "<tbody id=""" + index + "dynamictablebody""></tbody><tfoot><tr id=""" + index + "pagerOne""><td colspan=""" & CStr(TotalCol + 1) & """><img src=""_assets/img/first.png"" class=""first"" onclick=""GetFirstPage(" + index + ");""/><img src=""_assets/img/prev.png"" class=""prev""  onclick=""GetPrevPage(" + index + ");""/><input type=""text"" readonly=""readonly"" id=""" + index + "pagedisplaytext"" class=""pagedisplay"" value=""0""/><img src=""_assets/img/next.png"" class=""next""  onclick=""GetNextPage(" + index + ");""/><img src=""_assets/img/last.png"" class=""last""  onclick=""GetLastPage(" + index + ");""/></td></tr></tfoot></table>"
                Else
                    str1 += "<tbody id=""" + index + "dynamictablebody""></tbody><tfoot><tr id=""" + index + "pagerOne""><td colspan=""" & CStr(TotalCol) & """><img src=""_assets/img/first.png"" class=""first"" onclick=""GetFirstPage(" + index + ");""/><img src=""_assets/img/prev.png"" class=""prev""  onclick=""GetPrevPage(" + index + ");""/><input type=""text"" readonly=""readonly"" id=""" + index + "pagedisplaytext"" class=""pagedisplay"" value=""0""/><img src=""_assets/img/next.png"" class=""next""  onclick=""GetNextPage(" + index + ");""/><img src=""_assets/img/last.png"" class=""last""  onclick=""GetLastPage(" + index + ");""/></td></tr></tfoot></table>"
                End If
            Else
                str1 = "No Record Exists!!!"
            End If
            con.Close()
            Return str1
        Catch ex As Exception
            str1 = "No Record Exists!!!"
            Return str1
        End Try

    End Function

    <WebMethod()> _
    Public Function GetDynamicTable3(ByVal Query As String) As String
        Dim str2 As String
        Dim type, tmpID, tmpvalue As String
        Dim tmpdatevalue As DateTime
        Dim i, r As Integer
        Dim val As Decimal
        Dim rd As SqlDataReader
        Dim TotalCol As Integer
        Try
            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            tmpID = ""
            con.Open()
            cmd.ExecuteNonQuery()
            str2 = ""
            rd = cmd.ExecuteReader()

            If rd.HasRows Then
                r = 0
                TotalCol = rd.FieldCount
                Do While (rd.Read())
                    r = r + 1
                    'str2 += "<tr id=""row" & r & """ onmouseover=""RowMouseOver(" & r & ");"" onclick=""RowSelected(" & r & ");"">"
                    str2 += "~@~"
                    For i = 0 To rd.FieldCount - 1
                        type = rd.Item(i).GetType().ToString()

                        If type = "System.Decimal" Then
                            str2 += "^|^align:right:"
                            val = rd.GetDecimal(i)
                            str2 += String.Format("{0:#,##0.00}", val)

                        ElseIf type = "System.Int32" Then
                            str2 += "^|^align:right:"
                            val = rd.GetInt32(i)
                            str2 += String.Format("{0:#,##0}", val)
                        ElseIf type = "System.DateTime" Then
                            str2 += "^|^align:left:"
                            tmpdatevalue = rd.GetDateTime(i).ToString()
                            tmpvalue = Format(tmpdatevalue, "dd-MMM-yyyy")
                            str2 += tmpvalue
                        Else
                            tmpvalue = rd.GetValue(i).ToString()
                            str2 += "^|^align:left:"
                            str2 += tmpvalue.Replace("&", "and")

                        End If

                    Next i

                Loop
            Else
                str2 = "No Record Exists!!!"
            End If
            con.Close()
            Return str2
        Catch ex As Exception
            str2 = "No Record Exists!!!"
            Return str2
        End Try

    End Function

    <WebMethod()> _
    Public Function GetRefInfo(ByVal Query As String) As String
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim con As New SqlConnection(cnStr)
        Dim cmd As New SqlCommand(Query, con)
        Dim str As String
        Dim i As Integer
        Dim TotalRef As Integer
        con.Open()
        cmd.ExecuteNonQuery()

        str = ""
        Dim rd As SqlDataReader
        rd = cmd.ExecuteReader()

        If rd.HasRows Then
            If (rd.Read()) Then
                TotalRef = rd.GetValue(0).ToString()
                str = TotalRef & ">"
                For i = 0 To CInt(TotalRef) - 1
                    str += rd.GetValue(i + 1).ToString() + "|"
                Next i
            Else
                str = "No Record Exists!!!"
            End If
        Else
            str = "No Record Exists!!!"
        End If
        con.Close()
        Return str
    End Function

    <WebMethod()> _
    Public Function GetCompletionList(ByVal prefixText As String, ByVal count As Integer, ByVal contextKey As String) As String()
        Dim item As New List(Of String)
        Dim count1 As Integer
        Dim FieldArray As String()

        Dim TableName, Column, WhereCond, Query As String

        FieldArray = contextKey.Split("|")

        TableName = FieldArray(0)
        Column = FieldArray(1)
        WhereCond = FieldArray(2)

        If Trim(WhereCond) = "" Then
            Query = "select distinct top " & count.ToString() & " " & Column & " from " & TableName & " where " & Column & " like '" & prefixText & "%'"
        Else
            Query = "select distinct top " & count.ToString() & " " & Column & " from " & TableName & " where " & WhereCond & " and " & Column & " like '" & prefixText & "%'"
        End If

        Dim cnString As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim con As New SqlConnection(cnString)
        Dim cmd As New SqlCommand(Query, con)

        Try
            con.Open()
            cmd.ExecuteNonQuery()
            Dim rd As SqlDataReader
            rd = cmd.ExecuteReader()

            If (rd.HasRows) Then
                count1 = 0
                While rd.Read
                    item.Add(rd.GetString(0))
                    count1 = count1 + 1
                End While
            End If
            con.Close()
        Catch e1 As Exception
            item.Add(e1.Message())
            con.Close()
        End Try

        count = count1
        Return item.ToArray()
    End Function
    <WebMethod()> _
    Public Function GetUserName(ByVal userid As String) As String
        Dim comDB As New CommonDBSvc
        Dim username As String
        username = ""
        Try
            username = comDB.getConcernStuffDept(userid)
        Catch ex As Exception

        End Try
        Return username

    End Function

    <WebMethod()> _
    Public Function SetSession(ByVal SessionName As String, ByVal SessionValue As Integer) As String
        Session(SessionName) = SessionValue
        Return "ok"
    End Function

    'This Web method is used for function call and the functions used are included in the CommonDBSvc classs
    'This method accepts A Single function name with a single input 
    'And Only those functions those returns a single output is used here
    <WebMethod()> _
        Public Function GetSingleOutputFunction(ByVal functionName As String, ByVal InputString As String) As String
        Dim comDB As New CommonDBSvc
        Dim OutputString As String
        OutputString = ""
        Try
            'All the functions used Need to get a block
            If functionName = "ExecuteQueryAndReturn" Then
                OutputString = comDB.ExecuteQueryAndReturn(InputString)
            ElseIf functionName = "ExecuteQueryAndReturnFirstString" Then
                OutputString = comDB.ExecuteQueryAndReturnFirstString(InputString)
            ElseIf functionName = "ExecuteQueryAndReturnAll" Then
                OutputString = comDB.ExecuteQueryAndReturnAll(InputString)
            ElseIf functionName = "ExecuteQuery" Then
                OutputString = comDB.ExecuteQuery(InputString)
            ElseIf functionName = "GetValueFromTable" Then
                Dim cond(3) As String
                cond = InputString.Split("|")
                OutputString = comDB.GetValueFromTable(cond(0), cond(1), cond(2))
            End If

        Catch ex As Exception

        End Try
        Return OutputString

    End Function

    <WebMethod()> _
    Public Function GetQueryResult(ByVal query As String) As String
        Dim comDB As New CommonDBSvc
        Dim output As String

        Try
            output = comDB.ExecuteQueryAndReturnAll(query)
            If output.StartsWith("-ERR") Then
                output = "NOVALUE"
            End If
        Catch ex As Exception
            output = "NOVALUE"
        End Try
        Return output

    End Function

    <WebMethod()> _
    Public Function ExecuteQuery(ByVal Query As String) As String
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
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

    <WebMethod()> _
    Public Function GetCustomerName(ByVal CustID As String) As String
        Dim comDB As New CommonDBSvc
        Dim output As String

        Try
            output = comDB.getCutomerName(CustID)
        Catch ex As Exception
            output = "-ERR"
        End Try
        Return output

    End Function

    <WebMethod()> _
    Public Function GetDepositIntRate(ByVal GrossPrincipal As Double, ByVal Term As Integer, ByVal CapitalizePeriod As Integer, ByVal MaturityValue As Double) As String
        Dim IntRate As String
        IntRate = GetQueryResult("select dbo.GetDepositIntRate(" & GrossPrincipal & "," & Term & "," & CapitalizePeriod & "," & MaturityValue & ") IntRate")
        IntRate = IntRate.Replace("!@", "")
        Return IntRate
    End Function

    <WebMethod()> _
    Public Function GetSLV(ByVal SLVRate As Double, ByVal InstallmentSize As Double, ByVal Remaining As Integer, ByVal FinanceMode As String) As String
        Try
            Dim SLVR As Double, SLVValue As Double
            SLVR = SLVRate / 1200
            SLVValue = -PV(SLVR, Remaining, InstallmentSize, 0, IIf(FinanceMode = "BOM", 1, 0))
            SLVValue = Math.Round(SLVValue, 0)
            Return SLVValue
        Catch ex As Exception
            Return 0
        End Try
    End Function

    <WebMethod()> _
    Public Function GetScheduleIntRate(ByVal IntFrequency As Integer, ByVal ResidualValue As Double, ByVal FinanceAmount As Double, ByVal Period As Integer, ByVal Installment As Double, ByVal InterestRest As Integer, ByVal FinanceMode As String) As String
        Try
            Dim RN, finAmt, M, ResVal, i, n, j, X, term As Double
            Dim PayInterval, iRest As Integer
            Dim m_Period As Double

            PayInterval = IntFrequency
            ResVal = ResidualValue
            finAmt = FinanceAmount
            term = Period
            M = PayInterval * 12
            RN = Installment

            iRest = InterestRest

            n = iRest / PayInterval
            m_Period = (term / PayInterval) / n

            X = Rate(m_Period, RN * n, -finAmt, ResVal, IIf(FinanceMode = "BOM", 1, 0))
            i = X * (12 / PayInterval) / n
            j = Math.Round(i * 100, 8)
            Dim ScheduleIntRate = Math.Round(j, 6)
            Return ScheduleIntRate
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    <WebMethod()> _
    Public Function GetInstallment(ByVal IntFrequency As Integer, ByVal ResidualValue As Double, ByVal FinanceAmount As Double, ByVal Period As Integer, ByVal InterestRate As Double, ByVal InterestRest As Integer, ByVal FinanceMode As String) As String
        Try
            Dim finAmt, ResVal, n, X, iRate, term As Double
            Dim PayInterval, iRest As Integer

            Dim m_iRate, m_Period As Double
            iRest = InterestRest
            iRate = (InterestRate) / 100
            term = Period
            finAmt = FinanceAmount
            ResVal = ResidualValue
            PayInterval = IntFrequency

            n = (iRest / PayInterval)
            m_iRate = n * iRate / (12 / PayInterval)
            m_Period = (term / PayInterval) / n
            X = Pmt(m_iRate, m_Period, -finAmt, ResVal, IIf(FinanceMode = "BOM", 1, 0))
            Dim Installment = Math.Round(X / n, 0)
            Return Installment
        Catch ex As Exception
            Return "0"
        End Try
    End Function

    <WebMethod()> _
Public Function GetPeriod(ByVal IntFrequency As Integer, ByVal ResidualValue As Double, ByVal FinanceAmount As Double, ByVal Installment As Integer, ByVal InterestRate As Double, ByVal InterestRest As Integer, ByVal FinanceMode As String) As String

        Try
            Dim RN, finAmt, M, ResVal, i, n, j As Double

            finAmt = FinanceAmount
            j = (InterestRate) / 100
            M = IntFrequency * 12

            i = j / M
            ResVal = ResidualValue
            RN = Installment

            n = NPer(i, -RN, finAmt, ResVal, IIf(FinanceMode = "BOM", 1, 0))
            n = n * 12 / M
            Dim Period = Math.Round(n, 0)
            Return Period
        Catch ex As Exception
            Return "0"
        End Try

    End Function


    Private Function GetFinAmt(ByVal txtNominalRate As Double, ByVal txtOffSetRate As Double, ByVal txtInstallmentPerYear As Double, ByVal txtGrossPrincipal As Double, ByVal txtConDownPayment As Boolean, ByVal txtDownPayment As Double) As Double
        Try
            Dim M, i, j, amt As Double
            j = (CDbl(IIf(Trim(txtNominalRate) = "", 0, CDbl(txtNominalRate))) - CDbl(IIf(Trim(txtOffSetRate) = "", 0, CDbl(txtOffSetRate)))) / 100
            M = CDbl(txtInstallmentPerYear) 'CDbl(drpInstallment.Items(drpInstallment.SelectedIndex).Value)
            i = j / M
            amt = CDbl(txtGrossPrincipal)
            'amt = CDbl(IIf(txtGrossPrincipal = "", 0, CDbl(txtGrossPrincipal)))
            If txtConDownPayment = True Then amt = amt - CDbl(txtDownPayment)

            'If chkPrincipalizeGracePeriodInterest.Checked = True Then
            'GRPInterest = CInt(txtGracePeriod.Text) * amt * i
            'amt = amt + GRPInterest
            'End If
            'If chkFields(PDP).Value = 1 Then amt = amt + SmartVal(txtFields(DF).Text)

            GetFinAmt = amt
        Catch e As Exception
            GetFinAmt = 0
        End Try
    End Function

    <WebMethod()> _
    Public Function GetNominalRateAgreement(ByVal txtNominalRate As Double, ByVal txtOffSetRate As Double, ByVal txtGrossPrincipal As Double, ByVal txtConDownPayment As Boolean, ByVal txtDownPayment As Double, ByVal txtInstallmentPerYear As Double, ByVal txtPeriod As Integer, ByVal txtResidualValue As Double, ByVal txtRental As Double, ByVal txtFinanceMode As String) As String
        Try
            Dim RN, finAmt, M, ResVal, i, n, j As Double

            finAmt = GetFinAmt(txtNominalRate, txtOffSetRate, txtInstallmentPerYear, txtGrossPrincipal, txtConDownPayment, txtDownPayment)
            M = CDbl(txtInstallmentPerYear) 'drpInstallment.Items(drpInstallment.SelectedIndex).Value


            'change for quarterly reduce principle
            'If chkQtrRduc.Value = 1 Then
            '    M = M / 3
            'End If

            n = CInt(txtPeriod)
            '------------ period calculation for period with IDCP
            'If (LoadProperty("PeriodWithIDCP", False) = "1") Then
            'n = n - SmartVal(txtFields(17).Text)
            'End If
            '---------end-------------
            n = n * M / 12
            ResVal = CDbl(txtResidualValue)
            RN = CDbl(txtRental)

            'change for quarterly reduce principle
            'If chkQtrRduc.Value = 1 Then
            '    RN = RN * 3
            'End If

            i = Rate(n, -RN, finAmt, ResVal, IIf(txtFinanceMode = "BOM", 1, 0))
            j = i * M * 100

            'If CommonDBSvc.LoadProperty("PmtWithActualYear") = "1" Then
            'j = j * 360 / 365
            'End If
            txtNominalRate = Math.Round(j, 6)
            Return txtNominalRate
        Catch ex As Exception
            Return "0"
        End Try

    End Function

    <WebMethod()> _
    Public Function GetRentalAgreement(ByVal txtNominalRate As Double, ByVal txtOffSetRate As Double, ByVal txtGrossPrincipal As Double, ByVal txtConDownPayment As Boolean, ByVal txtDownPayment As Double, ByVal txtInstallmentPerYear As Double, ByVal txtPeriod As Integer, ByVal txtResidualValue As Double, ByVal txtFinanceMode As String) As String
        Try
            Dim finAmt, i, j, M, ResVal, n As Double

            finAmt = GetFinAmt(txtNominalRate, txtOffSetRate, txtInstallmentPerYear, txtGrossPrincipal, txtConDownPayment, txtDownPayment)
            j = (CDbl(txtNominalRate) - CDbl(txtOffSetRate)) / 100

            'calculate the interest considering 365 days
            'If CommonDBSvc.LoadProperty("PmtWithActualYear") = "1" Then
            'j = j * 365 / 360
            'End If

            M = CDbl(txtInstallmentPerYear)

            'change for quarterly reduce principle
            'If chkQtrRduc.Value = 1 Then
            '    M = M / 3
            'End If

            i = j / M
            n = CInt(txtPeriod)

            '------------ period calculation for period with IDCP
            'If (LoadProperty("PeriodWithIDCP", False) = "1") Then

            'n = n - SmartVal(txtFields(17).Text)
            'End If
            '---------end-------------

            n = n * M / 12
            ResVal = CDbl(txtResidualValue)

            Dim txtRental = Math.Round(Pmt(i, n, -finAmt, ResVal, IIf(txtFinanceMode = "BOM", 1, 0)), 2)
            Return txtRental
            'change for quarterly reduce principle
            'If chkQtrRduc.Value = 1 Then
            '    txtFields(R).Text = Format(txtFields(R).Text, txtFields(R).DataFormat.Format) / 3
            'Else
            '    txtFields(R).Text = Format(txtFields(R).Text, txtFields(R).DataFormat.Format)
            'End If

        Catch ex As Exception
            Return "0"
        End Try



    End Function


    '<WebMethod()> _
    'Public Function GetNominalRateAgreement(ByVal txtNominalRate As Double, ByVal txtOffSetRate As Double, ByVal txtGrossPrincipal As Double, ByVal txtConDownPayment As Boolean, ByVal txtDownPayment As Double, ByVal txtInstallmentPerYear As Double, ByVal txtPeriod As Integer, ByVal txtResidualValue As Double, ByVal txtRental As Double, ByVal txtFinanceMode As String) As String
    '    Try
    '        Dim RN, finAmt, M, ResVal, i, n, j As Double

    '        finAmt = GetFinAmt(txtNominalRate, txtOffSetRate, txtInstallmentPerYear, txtGrossPrincipal, txtConDownPayment, txtDownPayment)
    '        M = CDbl(txtInstallmentPerYear) 'drpInstallment.Items(drpInstallment.SelectedIndex).Value


    '        'change for quarterly reduce principle
    '        'If chkQtrRduc.Value = 1 Then
    '        '    M = M / 3
    '        'End If

    '        n = CInt(txtPeriod)
    '        '------------ period calculation for period with IDCP
    '        'If (LoadProperty("PeriodWithIDCP", False) = "1") Then
    '        'n = n - SmartVal(txtFields(17).Text)
    '        'End If
    '        '---------end-------------
    '        n = n * M / 12
    '        ResVal = CDbl(txtResidualValue)
    '        RN = CDbl(txtRental)

    '        'change for quarterly reduce principle
    '        'If chkQtrRduc.Value = 1 Then
    '        '    RN = RN * 3
    '        'End If

    '        i = Rate(n, -RN, finAmt, ResVal, IIf(txtFinanceMode = "BOM", 1, 0))
    '        j = i * M * 100

    '        'If CommonDBSvc.LoadProperty("PmtWithActualYear") = "1" Then
    '        'j = j * 360 / 365
    '        'End If
    '        txtNominalRate = Math.Round(j, 6)
    '        Return txtNominalRate
    '    Catch ex As Exception
    '        Return "0"
    '    End Try



    'End Function



    <WebMethod()> _
    Public Function GetPeriodAgreement(ByVal txtNominalRate As Double, ByVal txtOffSetRate As Double, ByVal txtGrossPrincipal As Double, ByVal txtConDownPayment As Boolean, ByVal txtDownPayment As Double, ByVal txtInstallmentPerYear As Double, ByVal txtResidualValue As Double, ByVal txtRental As Double, ByVal txtFinanceMode As String) As String
        Try
            Dim RN, finAmt, M, ResVal, i, n, j As Double

            finAmt = GetFinAmt(txtNominalRate, txtOffSetRate, txtInstallmentPerYear, txtGrossPrincipal, txtConDownPayment, txtDownPayment)
            j = (CDbl(txtNominalRate) - CDbl(txtOffSetRate)) / 100
            M = CDbl(txtInstallmentPerYear) 'drpInstallment.Items(drpInstallment.SelectedIndex).Value

            'change for quarterly reduce principle
            'If chkQtrRduc.Value = 1 Then
            '    M = M / 3
            'End If


            i = j / M
            ResVal = CDbl(txtResidualValue)
            RN = CDbl(txtRental)

            'change for quarterly reduce principle
            'If chkQtrRduc.Value = 1 Then
            '    RN = RN * 3
            'End If

            n = NPer(i, -RN, finAmt, ResVal, IIf(txtFinanceMode = "BOM", 1, 0))
            n = n * 12 / M
            '------------ period calculation for period with IDCP
            'If (CommonDBSvc.LoadProperty("PeriodWithIDCP") = "1") Then
            ' n = n + SmartVal(txtFields(17).Text)
            'End If
            '---------end-------------
            Dim txtPeriod = Math.Round(n, 0)
            Return txtPeriod

        Catch ex As Exception
            Return "0"
        End Try
    End Function
    <WebMethod()> _
  Public Function GetRM(ByVal CustID As String) As String
        Dim comDB As New CommonDBSvc
        Dim output As String

        Try
            output = comDB.getRM(CustID)
        Catch ex As Exception
            output = "-ERR"
        End Try
        Return output

    End Function
    '<WebMethod()> _
    'Public Function GetEmployeeInfo(ByVal empid As String, ByVal chkparam As String) As String
    '    Dim comDB As New CommonDBSvc
    '    Dim username As String
    '    Dim companyProf As String
    '    Dim finalhtml As String = ""
    '    Dim Query As String
    '    Dim employeeid(100) As String
    '    Dim html As String
    '    Dim i As Integer = 0
    '    Dim j As Integer = 0
    '    Dim cnt As Integer = 0
    '    Dim FieldArray As String()
    '    Dim chkArray(23, 1) As String
    '    Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
    '    Dim con As New SqlConnection(cnStr)
    '    Dim qr As String
    '    qr = "select distinct(EmployeeID) from Employee where EmployeeID like '" & empid & "'"
    '    Dim cmd1 As New SqlCommand(qr, con)
    '    con.Open()
    '    cmd1.ExecuteNonQuery()
    '    Dim rd1 As SqlDataReader
    '    rd1 = cmd1.ExecuteReader()
    '    If (rd1.HasRows) Then

    '        'For i = 0 To rd1. - 1
    '        While rd1.Read
    '            employeeid(cnt) = rd1.GetValue(0).ToString()
    '            cnt = cnt + 1
    '        End While

    '        'Next
    '        con.Close()
    '    End If
    '    FieldArray = chkparam.Split("|")
    '    For i = 0 To FieldArray.Count / 2 - 1
    '        chkArray(i, 0) = FieldArray(j)
    '        chkArray(i, 1) = FieldArray(j + 1)
    '        j = j + 2
    '    Next

    '    html = "<table border=""0"" width=""600"" cellspacing=""0"" cellpadding=""0""  style=""border-collapse:collapse""><tr style=""display:#chkEmployeeID#""><td style=""width: 177px"">Employee ID</td><td style=""width: 423px"">#EmployeeID#</td></tr><tr style=""display:#chkName#""><td style=""width: 177px"">Name</td><td style=""width: 423px"">#Name#</td></tr><tr style=""display:#chkSex#""><td style=""width: 177px"">Sex</td><td>#Sex#</td></tr><tr style=""display:#chkFathersName#""><td style=""width: 177px"">Fathers Name</td><td>#FathersName#</td></tr><tr style=""display:#chkMaritalStatus#""><td style=""width: 177px"">Marital Status</td><td>#MaritalStatus#</td></tr><tr style=""display:#chkDateOfBirth#""><td style=""width: 177px"">Date Of Birth</td><td>#DateOfBirth#</td></tr><tr style=""display:#chkBloodGroup#""><td style=""width: 177px"">Blood Group</td><td>#BloodGroup#</td></tr><tr style=""display:#chkNationalID#""><td style=""width: 177px"">NationalID</td><td>#NationalID#</td></tr><tr style=""display:#chkPassportNo#""><td style=""width: 177px"">PassportNo</td><td>#PassportNo#</td></tr><tr style=""display:#chkTIN#""><td style=""width: 177px"">TIN</td><td>#TIN#</td></tr><tr style=""display:#chkAppoinmentDate#""><td style=""width: 177px"">Appoinment Date</td><td>#AppoinmentDate#</td></tr><tr style=""display:#chkJoiningDate#""><td style=""width: 177px"">Joining Date</td><td >#JoiningDate#</td></tr><tr style=""display:#chkConfirmationDate#""><td style=""width: 177px"">ConfirmationDate</td><td > #ConfirmationDate#</td></tr><tr style=""display:#chkDesignation#""><td style=""width: 177px"">Designation</td><td>#Designation#</td></tr><tr style=""display:#chkSalaryScheme#""><td style=""width: 177px"">Salary Scheme</td><td>#SalaryScheme#</td></tr><tr style=""display:#chkGrade#""><td style=""width: 177px"">Grade</td><td>#Grade#</td></tr><tr style=""display:#chkDepartment#""><td style=""width: 177px"">Department</td><td>#Department#</td></tr><tr style=""display:#chkProvitionPeriodInMonth#""><td style=""width: 177px"">ProvitionPeriodInMonth</td><td>#ProvitionPeriodInMonth#</td></tr><tr style=""display:#chkHighestEducation#""><td style=""width: 177px"">Highest Education</td><td>#HighestEducation#</td></tr><tr style=""display:#chkContactAddress#""><td style=""width: 177px"">ContactAddress</td><td>#ContactAddress#</td></tr><tr style=""display:#chkPhoneNo#""><td style=""width: 177px"">PhoneNo</td><td>#PhoneNo#</td></tr><tr style=""display:#chkMobileNo#""><td style=""width: 177px"">MobileNo</td><td>#MobileNo#</td></tr><tr style=""display:#chkPhotographPath#""><td style=""width: 177px"">Photo</td><td><img src=""http://localhost:1335/FIntelligent_Plus/#PhotographPath#"" width=""40px"" height=""50px"" /></td></tr><tr style=""display:#chkSignaturePath#""><td style=""width: 177px"">Signature</td><td><img src=""http://localhost:1335/FIntelligent_Plus/#SignaturePath#"" width=""40px"" height=""50px"" /></td></tr></table>"
    '    username = ""
    '    Dim retVal As String = ""

    '    Try
    '        For j = 0 To cnt - 1
    '            username = html
    '            'username = Replace(username, "#display#", "block")
    '            Query = "select * from Employee where EmployeeID ='" & employeeid(j) & "'"
    '            Dim cmd As New SqlCommand(Query, con)
    '            con.Open()
    '            cmd.ExecuteNonQuery()
    '            Dim rd As SqlDataReader
    '            rd = cmd.ExecuteReader()
    '            If (rd.HasRows) Then

    '                rd.Read()
    '                For i = 0 To rd.FieldCount - 1
    '                    Dim retVal1 As String = rd.GetName(i)
    '                    retVal = rd.GetValue(i).ToString()
    '                    username = Replace(username, "#" + retVal1 + "#", retVal)
    '                    'username = Replace(username, "#display#", "#chk" + retVal1 + "#")
    '                Next
    '                con.Close()
    '            End If
    '            finalhtml = finalhtml + username + "<hr>" + "<br/>"
    '        Next

    '        For i = 0 To 23
    '            If chkArray(i, 1) = "true" Then
    '                finalhtml = Replace(finalhtml, "#" + chkArray(i, 0) + "#", "block")
    '            Else
    '                finalhtml = Replace(finalhtml, "#" + chkArray(i, 0) + "#", "none")
    '            End If
    '        Next

    '        'username = "<table border=""1""><tr><td>Value 1</td></tr><tr><td>Value 1</td></tr></table>"
    '    Catch ex As Exception

    '    End Try
    '    companyProf = "<table border=""0"" width=""600"" cellspacing=""0""><tr align=""center"" style=""font-size: 16px; font-weight: bold;""><td>" & comDB.getCompanyName() & "</td></tr><tr align=""center"" style=""font-size: 16px;""><td>" & comDB.getCompanyAddress() & "</td></tr></table><br/><hr><br/>"

    '    Return companyProf + finalhtml

    'End Function

    <WebMethod()> _
    Public Function GetVerifiedTellerTransaction(ByVal TellerID As String, ByVal chkparam As String) As String
        Dim comDB As New CommonDBSvc
        Dim TransactionInfo As String = ""
        Dim Query As String
        Dim i As Integer = 0
        Dim cnt As Integer = 0
        Dim rowsExist = 0
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim con As New SqlConnection(cnStr)
        TransactionInfo = "<table border=1><tr><td><b>Transaction ID</b></td><td><b>Agreement ID</b></td><td><b>Customer Name</b></td><td><b>Transaction Status</b></td><td><b>Amount</b></td><td><b>Transaction Type</b></td><td></td></tr>"

        Dim html As String = "<tr><td >#_TransactionID_#</td><td >#_AgreementNo_#</td><td >#_CustomerName_#</td><td >Verified</td><td >#_Amount_#</td><td>#_TransactionType_# </td><td><a href=""#"" OnClick=""ApproveTransaction('#_TransactionID_#');return false"">[Approve]</a> <a  href=""#"" OnClick=""CancelTransaction('#_TransactionID_#');return false"">[Cancel]</a></td></tr>"
        Dim rowsFormat As String, retVal As String
        Query = "select TransactionID,AgreementNo, dbo.getCustomerNamefromAgID(AgreementNo,'Agreement') AS CustomerName,Amount,TransactionType from TellerTransaction where TellerID = '" & TellerID & "' and Status='Verified'"
        Dim cmd1 As New SqlCommand(Query, con)
        con.Open()
        cmd1.ExecuteNonQuery()
        Dim rd As SqlDataReader
        rd = cmd1.ExecuteReader()
        While rd.Read()
            rowsFormat = html
            rowsExist = 1

            For i = 0 To rd.FieldCount - 1
                Dim retVal1 As String = rd.GetName(i)
                retVal = rd.GetValue(i).ToString()
                rowsFormat = Replace(rowsFormat, "#_" & retVal1 & "_#", retVal)

            Next
            TransactionInfo = TransactionInfo & rowsFormat

        End While

        con.Close()


        TransactionInfo = TransactionInfo & "</table>"
        If rowsExist = 0 Then
            TransactionInfo = ""
        End If
        Return TransactionInfo

    End Function

    <WebMethod()> _
    Public Function GetDenominationTable() As String
        Dim comDB As New CommonDBSvc
        Dim DenominationTableHTML As String = ""
        Dim Query As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim cnt As Integer = 0
        Dim rowsExist = 0
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim con As New SqlConnection(cnStr)



        DenominationTableHTML = "<table border=1 style=""height: 138px; width: 291px""><tr ><td style=""width: 37px""><b>Currency</b></td><td style=""width: 77px""><b>Denomination</b></td><td style=""width: 74px""><b>Count</b></td></tr>"

        Dim html As String = "<tr><td style=""width: 37px"">#_Currency_#</td><td style=""width: 77px"">#_denomination_#</td><td style=""width: 74px""><input type=text name=""ctl00$Body$Denomination#_Index_#"" id=""ctl00_Body_Denomination#_Index_#"" value=0></td></tr>"
        Dim rowsFormat As String, retVal As String


        Query = "select Currency,denomination from DenominationDef"
        Dim cmd1 As New SqlCommand(Query, con)
        con.Open()
        cmd1.ExecuteNonQuery()
        Dim rd As SqlDataReader
        rd = cmd1.ExecuteReader()
        While rd.Read()
            rowsFormat = html
            rowsExist = rowsExist + 1

            For i = 0 To rd.FieldCount - 1
                Dim retVal1 As String = rd.GetName(i)
                retVal = rd.GetValue(i).ToString()
                rowsFormat = Replace(rowsFormat, "#_" & retVal1 & "_#", retVal)

            Next
            rowsFormat = Replace(rowsFormat, "#_Index_#", rowsExist)
            DenominationTableHTML = DenominationTableHTML & rowsFormat

        End While

        con.Close()
        DenominationTableHTML = DenominationTableHTML & "</table><input type=hidden id=total_denomination value=" & rowsExist & ">"
        If rowsExist = 0 Then
            DenominationTableHTML = ""
        End If
        Return DenominationTableHTML

    End Function

    <WebMethod()> _
    Public Function GetAgreementInformation(ByVal AgreementID As String, ByVal Number As Integer) As String
        Dim comDB As New CommonDBSvc
        Dim AgreementDetailsHTML As String = ""
        Dim OperatorDetailsHTML As String = ""
        Dim Query As String
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim cnt As Integer = 0
        Dim rowsExist As Integer = 0
        Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        Dim con As New SqlConnection(cnStr)



        AgreementDetailsHTML = "<table border=1><tr><td colspan=3 align=center>Agreement Information</td></tr><tr><td><b>Current Balance</b></td><td colspan=2><b>#_Balance_#</b></td></tr>"
        AgreementDetailsHTML = AgreementDetailsHTML & "<tr><td><b>Minimum Balance</b></td><td colspan=2><b>#_MinimumBalance_#</b></td></tr>"
        AgreementDetailsHTML = AgreementDetailsHTML & "<tr><td><b>Balance in Transit</b></td><td colspan=2><b>#_BalanceTransit_#</b></td></tr>"

        OperatorDetailsHTML = "<tr><td><img id=photo_id Height=""125"" Width= ""125"" src=""#_PhotographPath_#""><canvas id=""Canvas4""></canvas><br/><input onClick=""zoomin('photo_id')"" type=button name=zoomin1 id=zoomin1 value=""Zoom In""><input type=button name=zoomout1  onClick=""zoomout('photo_id')""  id=zoomout1 value=""Zoom Out""><br/><input type=button name=rotateleft1 id=rotateleft1 Onclick=""RotateLeft1()"" value=""Rotate Left""><input type=button name=rotateright1 id=rotateright1  Onclick=""RotateRight1()""  value=""Rotate Right""></td><td colspan=2><img  id=signature_id Height=""125"" Width= ""125""  src=""#_SignaturePath_#""><canvas id=""Canvas3""></canvas><br/><input type=button name=zoomin2  onclick=""zoomin('signature_id')"" id=zoomin2 value=""Zoom In""><input type=button name=zoomout2 id=zoomout2   onclick=""zoomout('signature_id')""  value=""Zoom Out""><br/><input type=button name=rotateleft2 id=rotateleft2 value=""Rotate Left"" Onclick=""RotateLeft2()""><input type=button name=rotateright2 id=rotateright2 value=""Rotate Right"" Onclick=""RotateRight2()""></td></tr>"
        OperatorDetailsHTML = OperatorDetailsHTML & "<tr><td><b>Operator</b></td><td><b>Photograph</b></td><td><b>Signature</b></td></tr>"

        Dim html As String = "<tr><td><a href="""" Onclick=""ChangeImageSource('#_PhotographPath_#','#_SignaturePath_#');return false;"" style=""Cursor:hand;Cursor:pointer"" >#_Operator_#</a></td><td><img  Height=""125"" Width= ""125""  src=""#_PhotographPath_#""></td><td><img  Height=""125"" Width= ""125""  src=""#_SignaturePath_#""></td>"

        Dim rowsFormat As String, retVal As String



        Query = "select dbo.CheckAgreementBalanceForTeller('" & AgreementID & "') AS Balance, dbo.getResidualValueFromAgreement('" & AgreementID & "') AS MinimumBalance"
        Dim cmd1 As New SqlCommand(Query, con)
        con.Open()
        cmd1.ExecuteNonQuery()
        Dim rd As SqlDataReader

        rd = cmd1.ExecuteReader()
        While rd.Read()
            AgreementDetailsHTML = Replace(AgreementDetailsHTML, "#_Balance_#", rd.GetValue(0).ToString())
            AgreementDetailsHTML = Replace(AgreementDetailsHTML, "#_MinimumBalance_#", rd.GetValue(1).ToString())
            AgreementDetailsHTML = Replace(AgreementDetailsHTML, "#_BalanceTransit_#", CDbl(rd.GetValue(0)) - CDbl(rd.GetValue(1)))
        End While
        con.Close()

        Query = "select OperatorID,Operator,PhotographPath,SignaturePath from DepositOperator where AgreemenID='" & AgreementID & "'"
        con.Open()
        Dim cmd2 As New SqlCommand(Query, con)
        cmd1.ExecuteNonQuery()
        rd = cmd2.ExecuteReader()
        While rd.Read()
            rowsFormat = html
            rowsExist = rowsExist + 1

            For i = 0 To rd.FieldCount - 1
                Dim retVal1 As String = rd.GetName(i)
                retVal = rd.GetValue(i).ToString()
                rowsFormat = Replace(rowsFormat, "#_" & retVal1 & "_#", retVal)
                rowsFormat = Replace(rowsFormat, "~/", "")

                OperatorDetailsHTML = Replace(OperatorDetailsHTML, "#_" & retVal1 & "_#", retVal)

            Next

            OperatorDetailsHTML = OperatorDetailsHTML & rowsFormat

        End While
        con.Close()
        If rowsExist = 0 Then
            OperatorDetailsHTML = ""
        End If
        AgreementDetailsHTML = AgreementDetailsHTML & OperatorDetailsHTML



        rowsExist = 0
        Dim Balance As Double = 0
        Dim TransactionDetailsHTML As String = "<tr><td colspan=7>Transaction History</td></tr><tr><td><b>Date</b></td><td><b>VoucherNo</b></td><td><b>Particulars</b></td><td><b>Debit</b></td><td><b>Credit</b></td><td><b>Balance</b></td><td><b>Ref</b></td></td>"
        Dim TransactionHistoryFormat As String = "<tr><td>#_Date_#</td><td>#_VoucherNo_#</td><td>#_FinalVoucher_#</td><td>#_Debit_#</td><td>#_Credit_#</td><td>#_Balance_#</td><td>#_reference_#</td></tr>"
        Query = "select TOP " & Number & " dbo.getDateFormat(Date) As Date,VoucherNo,FinalVoucher.AccountName FinalVoucher,Debit,Credit,reference from FinalVoucher,ChartofAccounts where   Agreementno='" & AgreementID & "' and FinalVoucher.AccountName=ChartofAccounts.AccountName and ChartofAccounts.ExcludeInAgreementStatement='False' order by [Date]"
        con.Open()
        Dim cmd3 As New SqlCommand(Query, con)
        cmd1.ExecuteNonQuery()
        rd = cmd3.ExecuteReader()
        While rd.Read()
            rowsFormat = TransactionHistoryFormat
            rowsExist = rowsExist + 1

            For i = 0 To rd.FieldCount - 1
                Dim retVal1 As String = rd.GetName(i)
                retVal = rd.GetValue(i).ToString()
                rowsFormat = Replace(rowsFormat, "#_" & retVal1 & "_#", retVal)

            Next
            Balance = Balance + (CDbl(rd.GetValue(3)) - CDbl(rd.GetValue(4)))
            rowsFormat = Replace(rowsFormat, "#_Balance_#", Balance)

            TransactionDetailsHTML = TransactionDetailsHTML & rowsFormat

        End While
        con.Close()
        If rowsExist = 0 Then
            TransactionDetailsHTML = ""
        End If
        AgreementDetailsHTML = AgreementDetailsHTML & TransactionDetailsHTML

        AgreementDetailsHTML = AgreementDetailsHTML & "</table>"

        Return AgreementDetailsHTML

    End Function

    <WebMethod()> _
    Public Function GetCustomerautonumber(ByVal custype As String, ByVal bname As String) As String
        Dim comDB As New CommonDBSvc
        Dim output As String

        Try
            output = comDB.ExecuteQueryAndReturnFirstString("select Autonumberdefinition from CustomerType where CustomerType = '" & custype & "'")
        Catch ex As Exception
            Return "-ERR"
        End Try
        Try
            output = comDB.getPrimaryKey("CustomerGeneral", bname, output)
        Catch ex As Exception
            output = "-ERR"
        End Try
        Return output

    End Function

    <WebMethod()> _
    Public Function TableColumnList(ByVal TableName As String) As String

        Dim cnt As Integer
        Dim output As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.Text
        dsmain.SelectCommand = "select ColName,ColType,ColLength,IsMandatory,ColDesc,PopupQuery,SelCol,AutoCompleteContext,FixedAutoComplete,Precision,scale,DefaultValue,DependentValueExpression,ReferenceQuery,SpecialValidationExpression,PopupFilterType,SortingColumn,URLLink from Tablecolumninfo where TableName='" & TableName & "' order by column_id"

        output = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.SelectParameters.Count
            For i = 0 To rc - 1
                If i = rc - 1 Then
                    output = output & dataview.Item(i).Row.Item("ColName").ToString() & "~" & dataview.Item(i).Row.Item("ColType").ToString() & "~" & dataview.Item(i).Row.Item("ColLength").ToString() & "~" & dataview.Item(i).Row.Item("IsMandatory").ToString() & "~" & dataview.Item(i).Row.Item("ColDesc").ToString() & "~" & dataview.Item(i).Row.Item("PopupQuery").ToString() & "~" & dataview.Item(i).Row.Item("SelCol").ToString() & "~" & dataview.Item(i).Row.Item("AutoCompleteContext").ToString() & "~" & dataview.Item(i).Row.Item("FixedAutoComplete").ToString() & "~" & dataview.Item(i).Row.Item("Precision").ToString() & "~" & dataview.Item(i).Row.Item("scale").ToString() & "~" & dataview.Item(i).Row.Item("DefaultValue").ToString() & "~" & dataview.Item(i).Row.Item("DependentValueExpression").ToString() & "~" & dataview.Item(i).Row.Item("ReferenceQuery").ToString() & "~" & dataview.Item(i).Row.Item("SpecialValidationExpression").ToString() & "~" & dataview.Item(i).Row.Item("PopupFilterType").ToString() & "~" & dataview.Item(i).Row.Item("SortingColumn").ToString() & "~" & dataview.Item(i).Row.Item("URLLink").ToString()
                Else
                    output = output & dataview.Item(i).Row.Item("ColName").ToString() & "~" & dataview.Item(i).Row.Item("ColType").ToString() & "~" & dataview.Item(i).Row.Item("ColLength").ToString() & "~" & dataview.Item(i).Row.Item("IsMandatory").ToString() & "~" & dataview.Item(i).Row.Item("ColDesc").ToString() & "~" & dataview.Item(i).Row.Item("PopupQuery").ToString() & "~" & dataview.Item(i).Row.Item("SelCol").ToString() & "~" & dataview.Item(i).Row.Item("AutoCompleteContext").ToString() & "~" & dataview.Item(i).Row.Item("FixedAutoComplete").ToString() & "~" & dataview.Item(i).Row.Item("Precision").ToString() & "~" & dataview.Item(i).Row.Item("scale").ToString() & "~" & dataview.Item(i).Row.Item("DefaultValue").ToString() & "~" & dataview.Item(i).Row.Item("DependentValueExpression").ToString() & "~" & dataview.Item(i).Row.Item("ReferenceQuery").ToString() & "~" & dataview.Item(i).Row.Item("SpecialValidationExpression").ToString() & "~" & dataview.Item(i).Row.Item("PopupFilterType").ToString() & "~" & dataview.Item(i).Row.Item("SortingColumn").ToString() & "~" & dataview.Item(i).Row.Item("URLLink").ToString() & "@"
                End If
            Next i
        Catch ex As Exception
            output = "-ERR"
        End Try
        Return output
    End Function

    <WebMethod()> _
    Public Function CustomTableColumnList(ByVal TableName As String) As String

        Dim cnt As Integer
        Dim output As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.Text
        dsmain.SelectCommand = "select FieldName,IsPrimaryKey,DefaultValue,FieldType,FieldLength,IsMandatory,DecimalPoint,Rowspan,Columnspan,HorizontalSize,VerticalSize,dbo.getCustomPopupQuery(WindowPopupReference) WindowPopupReference,dbo.getCustomPopupQuerySelectCol(WindowPopupReference,WindowPopupSelectColumnIndex) WindowPopupSelectColumnIndex,dbo.getCustomPopupQuerySortCol(WindowPopupReference,WindowPopupSortColumnIndex) WindowPopupSortColumn,dbo.getCustomAutoCompleteContext(DownPopupInfo,DownPopupColumnIndex) DownPopupInfo,DownPopupColumnIndex,IsDataRestrictedtoDownPopup,dbo.getCustomAdditionalRefQuery(AdditionalInfoReference) AdditionalRefQuery from CustomTableFieldDef where TableName='" & TableName & "' order by FieldIndex"

        output = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.SelectParameters.Count
            For i = 0 To rc - 1
                If i = rc - 1 Then
                    output = output & dataview.Item(i).Row.Item("FieldName").ToString() & "~" & dataview.Item(i).Row.Item("IsPrimaryKey").ToString() & "~" & dataview.Item(i).Row.Item("DefaultValue").ToString() & "~" & dataview.Item(i).Row.Item("FieldType").ToString() & "~" & dataview.Item(i).Row.Item("FieldLength").ToString() & "~" & dataview.Item(i).Row.Item("IsMandatory").ToString() & "~" & dataview.Item(i).Row.Item("DecimalPoint").ToString() & "~" & dataview.Item(i).Row.Item("Rowspan").ToString() & "~" & dataview.Item(i).Row.Item("Columnspan").ToString() & "~" & dataview.Item(i).Row.Item("HorizontalSize").ToString() & "~" & dataview.Item(i).Row.Item("VerticalSize").ToString() & "~" & dataview.Item(i).Row.Item("WindowPopupReference").ToString() & "~" & dataview.Item(i).Row.Item("WindowPopupSelectColumnIndex").ToString() & "~" & dataview.Item(i).Row.Item("WindowPopupSortColumn").ToString() & "~" & dataview.Item(i).Row.Item("DownPopupInfo").ToString() & "~" & dataview.Item(i).Row.Item("DownPopupColumnIndex").ToString() & "~" & dataview.Item(i).Row.Item("IsDataRestrictedtoDownPopup").ToString() & "~" & dataview.Item(i).Row.Item("AdditionalRefQuery").ToString()
                Else
                    output = output & dataview.Item(i).Row.Item("FieldName").ToString() & "~" & dataview.Item(i).Row.Item("IsPrimaryKey").ToString() & "~" & dataview.Item(i).Row.Item("DefaultValue").ToString() & "~" & dataview.Item(i).Row.Item("FieldType").ToString() & "~" & dataview.Item(i).Row.Item("FieldLength").ToString() & "~" & dataview.Item(i).Row.Item("IsMandatory").ToString() & "~" & dataview.Item(i).Row.Item("DecimalPoint").ToString() & "~" & dataview.Item(i).Row.Item("Rowspan").ToString() & "~" & dataview.Item(i).Row.Item("Columnspan").ToString() & "~" & dataview.Item(i).Row.Item("HorizontalSize").ToString() & "~" & dataview.Item(i).Row.Item("VerticalSize").ToString() & "~" & dataview.Item(i).Row.Item("WindowPopupReference").ToString() & "~" & dataview.Item(i).Row.Item("WindowPopupSelectColumnIndex").ToString() & "~" & dataview.Item(i).Row.Item("WindowPopupSortColumn").ToString() & "~" & dataview.Item(i).Row.Item("DownPopupInfo").ToString() & "~" & dataview.Item(i).Row.Item("DownPopupColumnIndex").ToString() & "~" & dataview.Item(i).Row.Item("IsDataRestrictedtoDownPopup").ToString() & "~" & dataview.Item(i).Row.Item("AdditionalRefQuery").ToString() & "@"
                End If
            Next i
        Catch ex As Exception
            output = "-ERR"
        End Try
        Return output
    End Function

    <WebMethod()> _
    Public Function GetParameterList(ByVal objectname As String) As String

        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prGetParameterList"
        dsmain.SelectParameters.Add("object", objectname)

        dsmain.InsertCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.InsertCommand = "prGetParameterList"

        ''Dim param As New System.Web.UI.WebControls.Parameter("test")

        fieldName = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.InsertParameters.Count
            For i = 0 To rc - 1
                fieldName = fieldName & dataview.Item(i).Row.Item("ParameterName").ToString() & "|" & dataview.Item(i).Row.Item("ParameterDataType").ToString() & "|"
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        fieldName = fieldName.Replace("@", "")
        Return fieldName

    End Function

    <WebMethod()> _
    Public Function GetStoredProcCode(ByVal SPName As String) As String

        Dim cnt As Integer
        Dim SPText As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString

        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "sp_helptext"

        Dim param As New System.Web.UI.WebControls.Parameter("objname")
        param.DefaultValue = SPName
        dsmain.SelectParameters.Add(param)

        SPText = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.SelectParameters.Count
            For i = 0 To rc - 1
                SPText = SPText & dataview.Item(i).Row.Item("Text").ToString()
            Next i
        Catch ex As Exception
            SPText = "-ERR"
        End Try
        Return SPText

    End Function

    <WebMethod()> _
      Public Function AjaxInsert(ByVal objectname As String, ByVal parametervalue As String) As String
        Dim comdb As New CommonDBSvc
        Dim dsmain As New SqlDataSource
        Dim i As Integer
        Dim param As System.Web.UI.WebControls.Parameter
        Dim val As String
        Dim dbsvc As New CommonDBSvc

        'parametervalue = parametervalue.Replace("<o:p>", "")
        'parametervalue = parametervalue.Replace("</o:p>", "")
        'parametervalue = parametervalue.Replace("&nbsp;", "")


        Dim rc As Integer = 0
        Dim parameterarray As String()
        Try
            parameterarray = parametervalue.Split("|")

            dsmain.ConnectionString = comdb.GetConnectionString()
            dsmain.InsertCommandType = SqlDataSourceCommandType.StoredProcedure
            dsmain.InsertCommand = objectname
            For i = 0 To parameterarray.Count - 2
                param = New System.Web.UI.WebControls.Parameter(parameterarray(i))
                If parameterarray(i + 1) <> "" Then
                    val = parameterarray(i + 1)
                    If val.Length > 5 Then
                        If (val.Substring(0, 5) = "<xml>") Then
                            val = val.Replace("<xml>", "")
                            val = val.Replace("</xml>", "")
                            val = val.Replace("<", "#%#")
                            val = val.Replace(">", "@%#")
                            val = val.Replace(" & ", " and ")
                            val = "<xml>" + val + "</xml>"
                            param.DefaultValue = val
                        Else
                            param.DefaultValue = parameterarray(i + 1)
                        End If
                    Else
                        param.DefaultValue = parameterarray(i + 1)
                    End If


                End If
                If param.Name <> "" Then
                    dsmain.InsertParameters.Add(param)
                End If
                i = i + 1
            Next i

            dsmain.Insert()
            ''Dim param As New System.Web.UI.WebControls.Parameter("test")        
            Return "ok"
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    <WebMethod()> _
      Public Function AjaxExecute(ByVal objectname As String, ByVal parametervalue As String) As String

        Dim i As Integer
        Dim ReturnValue As String
        'Dim param As System.Web.UI.WebControls.Parameter
        Dim parameterarray As String()
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
        Try
            parameterarray = parametervalue.Split("|")

            conn.Open()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandText = objectname
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.CommandTimeout = 0

            'For i = 0 To parameterarray.Count - 2
            For i = 0 To parameterarray.Length - 2
                'param = New System.Web.UI.WebControls.Parameter(parameterarray(i))

                If parameterarray(i) <> "" Then
                    If parameterarray(i + 1) <> "" Then
                        cmd.Parameters.AddWithValue(parameterarray(i), parameterarray(i + 1).Replace(" & ", " and "))
                    Else
                        cmd.Parameters.AddWithValue(parameterarray(i), DBNull.Value)
                    End If
                End If
                i = i + 1
            Next i
            ReturnValue = cmd.ExecuteScalar()
            If ReturnValue Is Nothing Then
                ReturnValue = "Updated Successfully"
            End If
            conn.Close()
            Return ReturnValue
        Catch ex As Exception
            conn.Close()
            Return ex.Message
        End Try
    End Function

    <WebMethod()> _
      Public Function AjaxExecute2(ByVal objectname As String, ByVal parametervalue As String) As String

        Dim i As Integer
        Dim ReturnValue As String
        'Dim param As System.Web.UI.WebControls.Parameter
        Dim parameterarray As String()
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
        Try
            parameterarray = parametervalue.Split("|")

            conn.Open()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandText = objectname
            cmd.CommandType = CommandType.StoredProcedure
            cmd.CommandTimeout = 0
            cmd.CommandTimeout = 0

            'For i = 0 To parameterarray.Count - 2
            For i = 0 To parameterarray.Length - 2
                'param = New System.Web.UI.WebControls.Parameter(parameterarray(i))

                If parameterarray(i) <> "" Then
                    If parameterarray(i + 1) <> "" Then
                        cmd.Parameters.AddWithValue(parameterarray(i), parameterarray(i + 1).Replace(" & ", " and "))
                    Else
                        cmd.Parameters.AddWithValue(parameterarray(i), DBNull.Value)
                    End If
                End If
                i = i + 1
            Next i
            ReturnValue = cmd.ExecuteScalar()
            If ReturnValue Is Nothing Then
                ReturnValue = "Updated Successfully"
            End If
            conn.Close()
            Return ReturnValue
        Catch ex As Exception
            conn.Close()
            Return ex.Message
        End Try
    End Function

    <WebMethod()> _
    Public Function AjaxExecuteMultiRow(ByVal objectname As String, ByVal parametervalue As String) As String

        Dim i As Integer
        Dim k As Integer
        Dim ReturnValue As String
        'Dim param As System.Web.UI.WebControls.Parameter
        Dim multiarray As String()
        Dim parameterarray As String()
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
        conn.Open()
        multiarray = parametervalue.Split("!")
        For k = 0 To multiarray.Length - 2
            Try
                'parameterarray = parametervalue.Split("|")
                parameterarray = multiarray(k).Split("|")


                Dim cmd As New SqlCommand
                cmd.Connection = conn
                cmd.CommandText = objectname
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                'For i = 0 To parameterarray.Count - 2
                For i = 0 To parameterarray.Length - 2
                    'param = New System.Web.UI.WebControls.Parameter(parameterarray(i))

                    If parameterarray(i) <> "" Then
                        If parameterarray(i + 1) <> "" Then
                            cmd.Parameters.AddWithValue(parameterarray(i), parameterarray(i + 1).Replace(" & ", " and "))
                        Else
                            cmd.Parameters.AddWithValue(parameterarray(i), DBNull.Value)
                        End If
                    End If
                    i = i + 1
                Next i
                ReturnValue = cmd.ExecuteScalar()
                If ReturnValue Is Nothing Then
                    ReturnValue = "Updated Successfully"
                End If
                'Return ReturnValue
            Catch ex As Exception
                conn.Close()
                Return ex.Message
            End Try
        Next k
        conn.Close()
        Return "Updated Successfully"
    End Function

    <WebMethod()> _
    Public Function AjaxExecuteMultiRow2(ByVal objectname As String, ByVal parametervalue As String, ByVal RowList As String) As String

        Dim i As Integer
        Dim k As Integer
        Dim ReturnValue As String
        Dim ReturnString As String
        'Dim param As System.Web.UI.WebControls.Parameter
        Dim multiarray As String()
        Dim parameterarray As String()
        Dim Rows As String()
        Dim conn As New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
        conn.Open()
        ReturnString = ""
        multiarray = parametervalue.Split("!")
        Rows = RowList.Split("!")
        For k = 0 To multiarray.Length - 2
            Try
                'parameterarray = parametervalue.Split("|")
                parameterarray = multiarray(k).Split("|")
                Dim cmd As New SqlCommand
                cmd.Connection = conn
                cmd.CommandText = objectname
                cmd.CommandType = CommandType.StoredProcedure
                cmd.CommandTimeout = 0
                'For i = 0 To parameterarray.Count - 2
                For i = 0 To parameterarray.Length - 2
                    'param = New System.Web.UI.WebControls.Parameter(parameterarray(i))

                    If parameterarray(i) <> "" Then
                        If parameterarray(i + 1) <> "" Then
                            cmd.Parameters.AddWithValue(parameterarray(i), parameterarray(i + 1).Replace(" & ", " and "))
                        Else
                            cmd.Parameters.AddWithValue(parameterarray(i), DBNull.Value)
                        End If
                    End If
                    i = i + 1
                Next i
                ReturnValue = cmd.ExecuteScalar()
                If ReturnValue Is Nothing Then
                    ReturnValue = "Updated Successfully"
                End If
                ReturnString = ReturnString & "Row " & Rows(k) & ": " & ReturnValue & "!"
                'Return ReturnValue
            Catch ex As Exception
                ReturnString = ReturnString & "Row " & Rows(k) & ": " & ex.Message & "!"
                'conn.Close()
                'Return ex.Message
            End Try
        Next k
        conn.Close()
        Return ReturnString
    End Function

    <WebMethod()> _
      Public Function AjaxSelect(ByVal objectname As String, ByVal parametervalue As String) As String
        Dim comdb As New CommonDBSvc
        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim i As Integer
        Dim param As System.Web.UI.WebControls.Parameter
        Dim val As String

        Dim retval As String

        Dim rc As Integer
        Dim parameterarray As String()
        retval = ""
        Try
            parameterarray = parametervalue.Split("|")
            dsmain.ConnectionString = comdb.GetConnectionString()
            dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
            dsmain.SelectCommand = objectname
            For i = 0 To parameterarray.Count - 2
                param = New System.Web.UI.WebControls.Parameter(parameterarray(i))
                If parameterarray(i + 1) <> "" Then
                    param.DefaultValue = parameterarray(i + 1)
                End If
                If param.Name <> "" Then
                    dsmain.SelectParameters.Add(param)
                End If
                i = i + 1
            Next i
            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
            ''Dim param As New System.Web.UI.WebControls.Parameter("test")    
            rc = dataview.Count
            cnt = dataview.Table.Columns.Count
            For i = 0 To cnt - 1
                fieldName = dataview.Table.Columns(i).ColumnName
                val = dataview.Item(0).Row.Item(fieldName).ToString()
                retval = retval + fieldName + "|" + val + "|"
            Next i
            retval = retval.Substring(0, retval.Length - 1)
            retval = retval.Replace("<xml>", "")
            retval = retval.Replace("</xml>", "")
            retval = retval.Replace("#%#", "<")
            retval = retval.Replace("@%#", ">")

            Return retval
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    <WebMethod()> _
  Public Function AjaxSelect2(ByVal objectname As String, ByVal parametervalue As String) As String

        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim i As Integer
        Dim param As System.Web.UI.WebControls.Parameter
        Dim val As Decimal
        Dim tmpvalue As String
        Dim tmpdatevalue As Date
        Dim type As String
        Dim retval As String

        Dim rc As Integer
        Dim parameterarray As String()
        retval = ""
        Try
            parameterarray = parametervalue.Split("|")

            dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
            dsmain.SelectCommand = objectname
            'For i = 0 To parameterarray.Count - 2
            For i = 0 To parameterarray.Length - 2
                param = New System.Web.UI.WebControls.Parameter(parameterarray(i))
                If parameterarray(i + 1) <> "" Then
                    param.DefaultValue = parameterarray(i + 1)
                End If
                If param.Name <> "" Then
                    dsmain.SelectParameters.Add(param)
                End If
                i = i + 1
            Next i
            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)

            rc = dataview.Count
            cnt = dataview.Table.Columns.Count
            For r = 0 To rc - 1
                If (r = 0) Then
                    retval = retval + "<tr>"
                    For i = 0 To cnt - 1
                        fieldName = dataview.Table.Columns(i).ColumnName
                        retval = retval + "<th>" + fieldName
                    Next i
                End If
                retval = retval + "<tr>"
                For i = 0 To cnt - 1
                    fieldName = dataview.Table.Columns(i).ColumnName
                    type = dataview.Item(r).Row.Item(fieldName).GetType().ToString()
                    If type = "System.Decimal" Then
                        val = dataview.Item(r).Row.Item(fieldName).ToString()
                        tmpvalue = String.Format("{0:#,##0.00}", val)
                    ElseIf type = "System.DateTime" Then
                        tmpdatevalue = dataview.Item(r).Row.Item(fieldName).ToString()
                        tmpvalue = Format(tmpdatevalue, "dd-MMM-yyyy")
                    Else
                        tmpvalue = dataview.Item(r).Row.Item(fieldName).ToString()
                        tmpvalue = tmpvalue.Replace("&", "and")
                    End If
                    retval = retval + "<td>" + tmpvalue
                Next i
                'retval = retval.Substring(0, retval.Length - 1)
            Next r
            Return retval
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    <WebMethod()> _
  Public Function AjaxSelect3(ByVal objectname As String, ByVal parametervalue As String, ByVal colperrow As String) As String
        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim i As Integer
        Dim param As System.Web.UI.WebControls.Parameter
        Dim val As Decimal
        Dim tmpvalue As String
        Dim tmpdatevalue As Date
        Dim type As String
        Dim retval As String
        Dim rc As Integer
        Dim parameterarray As String()
        retval = "<table id=""tableOne"" class=""yui"" border=""1""><tbody id=""tbody1""></tbody>"
        Try
            parameterarray = parametervalue.Split("|")

            dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
            dsmain.SelectCommand = objectname
            'For i = 0 To parameterarray.Count - 2
            For i = 0 To parameterarray.Length - 2
                param = New System.Web.UI.WebControls.Parameter(parameterarray(i))
                If parameterarray(i + 1) <> "" Then
                    param.DefaultValue = parameterarray(i + 1)
                End If
                If param.Name <> "" Then
                    dsmain.SelectParameters.Add(param)
                End If
                i = i + 1
            Next i
            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)

            rc = dataview.Count
            cnt = dataview.Table.Columns.Count
            For r = 0 To rc - 1

                For i = 0 To cnt - 1
                    If (i Mod colperrow = 0 And i > 0) Then
                        If i > 0 Then
                            retval = retval + "</tr><tr>"
                        Else
                            retval = retval + "<tr>"
                        End If
                    End If
                    fieldName = dataview.Table.Columns(i).ColumnName
                    type = dataview.Item(r).Row.Item(fieldName).GetType().ToString()
                    If type = "System.Decimal" Then
                        val = dataview.Item(r).Row.Item(fieldName).ToString()
                        tmpvalue = String.Format("{0:#,##0.00}", val)
                    ElseIf type = "System.DateTime" Then
                        tmpdatevalue = dataview.Item(r).Row.Item(fieldName).ToString()
                        tmpvalue = Format(tmpdatevalue, "dd-MMM-yyyy")
                    Else
                        tmpvalue = dataview.Item(r).Row.Item(fieldName).ToString()
                        tmpvalue = tmpvalue.Replace("&", "and")
                    End If
                    retval = retval & "<th id=""col" & CStr(i) & """>" & fieldName & "</th><td id=""" & Replace(fieldName, " ", "") & "-1"">" & tmpvalue & "</td>"
                Next i
                retval = retval + "</tr>"
            Next r
            If IsNumeric(cnt) Then
                retval = retval & "</tbody></table>" & "~~" & CStr(cnt)
            Else
                retval = retval & "</tbody></table>" & "~~" & "0"
            End If
            Return retval
        Catch ex As Exception
            Return ex.Message
        End Try

    End Function

    <WebMethod()> _
    Public Function GetMessages(ByVal strFromUserID As String, ByVal strToUserID As String) As String
        Dim blnSucess As Boolean = False
        Dim strMessage As String
        Dim dDateSent As Date

        strMessage = ""

        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strFromUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strFromUserID))
            'strFromUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strToUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strToUserID))
            'strToUserGUID = MemUser.ProviderUserKey.ToString

            cmd = New SqlCommand("SELECT TOP(1) Message, DateSent FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID) ORDER BY DateSent DESC", conn)
            'cmd.Parameters.AddWithValue("@SenderUserID", strToUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strFromUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strToUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strFromUserID)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                dDateSent = reader("DateSent")
                'If dDateSent > DateAdd(DateInterval.Minute, -5, DateTime.Now) Then
                strMessage = reader("Message")
                blnSucess = True
                'End If
            End While
            reader.Close()
            cmd.Dispose()

            If blnSucess Then
                cmd = conn.CreateCommand()
                cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
                'cmd.Parameters.AddWithValue("@SenderUserID", strToUserGUID)
                'cmd.Parameters.AddWithValue("@RecipientUserID", strFromUserGUID)

                cmd.Parameters.AddWithValue("@SenderUserID", strToUserID)
                cmd.Parameters.AddWithValue("@RecipientUserID", strFromUserID)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
            conn.Close()

        End Using
        Return strMessage
    End Function
    <WebMethod()> _
    Public Function SendMessage(ByVal strSenderUserID As String, ByVal strRecipientUserID As String, ByVal strMessage As String) As String
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            cmd = conn.CreateCommand()
            cmd.CommandText = "INSERT INTO IMChats(RecipientUserID, SenderUserID, Message, DateSent) VALUES (@RecipientUserID, @SenderUserID, @Message, @DateSent)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)

            cmd.Parameters.AddWithValue("@Message", strMessage)
            cmd.Parameters.AddWithValue("@DateSent", DateTime.Now)

            cmd.ExecuteNonQuery()
            blnSucess = True
            cmd.Dispose()
            conn.Close()
        End Using
        Return blnSucess
    End Function

    'ENQ = chr(5)                            # Request to send
    'EOT = chr(4)                            # Ready to receive
    'ACK = chr(6)                            # Correct reception
    'NAK = chr(21)                           # Incorrect reception
    '////////////////////////////////
    '// Sender init chat
    '////////////////////////////////
    '// Sender ENQ     Wait 4 ACK
    '// Recip  ACK ENQ Wait 4 ACK ACK
    '// Sender ACK ACK Wait 4 EOT
    '// Recip  EOT
    '////////////////////////////////
    '// Recip wait 4 chat
    '////////////////////////////////
    '// Recip  ACK ENQ Wait 4 ACK
    '// Sender ACK ACK Wait 4 EOT
    '// Recip  EOT

    <WebMethod()> _
    Public Function CheckChatReq(ByVal strMyUserID As String) As String
        Dim strSenderUserID, strMessage As String
        Dim dDateSent As Date
        strSenderUserID = ""
        Dim strRetValue = ""

        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim strMyUserGUID As String = ""
            'Dim strSenderUserGUID As String = ""
            'Dim MemUser As MembershipUser = Membership.GetUser(HttpUtility.UrlDecode(strMyUserID))
            'strMyUserGUID = MemUser.ProviderUserKey.ToString

            cmd = New SqlCommand("SELECT TOP(1) SenderUserID, Message, DateSent FROM [IMChats] WHERE ([RecipientUserID] = @RecipientUserID) ORDER BY DateSent DESC", conn)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserID)
            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                dDateSent = reader("DateSent")
                'If dDateSent > DateAdd(DateInterval.Minute, -5, DateTime.Now) Then
                strMessage = reader("Message")
                ' ENQ
                If strMessage = "~::::=[(HANDSHAKE)]=::::~[ENQ]" Then
                    'strSenderUserGUID = reader("SenderUserID").ToString
                    'strSenderUserID = Membership.GetUser(New Guid(strSenderUserGUID)).UserName
                    strSenderUserID = reader("SenderUserID").ToString
                    strRetValue = strSenderUserID
                    'End If
                ElseIf strMessage.StartsWith("~::Instant Message:") Then
                    strSenderUserID = reader("SenderUserID").ToString
                    strRetValue = "Message from " & strSenderUserID & " : " & strMessage.Replace("~::Instant Message:", "")
                End If
            End While
            reader.Close()
            cmd.Dispose()

            If Not String.IsNullOrEmpty(strMyUserID) Then
                cmd = conn.CreateCommand()
                cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
                'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
                'cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserGUID)

                cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
                cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserID)

                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
            conn.Close()

        End Using
        Return strRetValue
    End Function

    <WebMethod()> _
    Public Function SendChatReq(ByVal strSenderUserID As String, ByVal strRecipientUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()
            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @RecipientUserID AND [RecipientUserID] = @SenderUserID)"
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            cmd = conn.CreateCommand()
            cmd.CommandText = "INSERT INTO IMChats(RecipientUserID, SenderUserID, Message, DateSent) VALUES (@RecipientUserID, @SenderUserID, @Message, @DateSent)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)

            ' ENQ
            cmd.Parameters.AddWithValue("@Message", "~::::=[(HANDSHAKE)]=::::~[ENQ]")
            cmd.Parameters.AddWithValue("@DateSent", DateTime.Now)

            cmd.ExecuteNonQuery()
            blnSucess = True
            cmd.Dispose()
            conn.Close()
        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
    Public Function ChatCleanUpAll(ByVal strMyUserID As String) As String
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim strMyUserGUID As String
            'Dim MemUser As MembershipUser = Membership.GetUser(HttpUtility.UrlDecode(strMyUserID))
            'strMyUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([RecipientUserID] = @RecipientUserID)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserID)
            cmd.ExecuteNonQuery()
            cmd.Dispose()
            conn.Close()
            blnSucess = True
        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
    Public Function CleanUp(ByVal strMyUserID As String, ByVal strSenderUserID As String, ByVal blnSendEOT As Boolean) As Boolean
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim strMyUserGUID As String
            'Dim MemUser As MembershipUser = Membership.GetUser(HttpUtility.UrlDecode(strMyUserID))
            'strMyUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserGUID)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strMyUserID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            If blnSendEOT Then
                cmd = conn.CreateCommand()
                cmd.CommandText = "INSERT INTO IMChats(RecipientUserID, SenderUserID, Message, DateSent) VALUES (@RecipientUserID, @SenderUserID, @Message, @DateSent)"
                'cmd.Parameters.AddWithValue("@RecipientUserID", strSenderUserGUID)
                'cmd.Parameters.AddWithValue("@SenderUserID", strMyUserGUID)

                cmd.Parameters.AddWithValue("@RecipientUserID", strSenderUserID)
                cmd.Parameters.AddWithValue("@SenderUserID", strMyUserID)

                ' EOT
                cmd.Parameters.AddWithValue("@Message", "~::::=[(HANDSHAKE)]=::::~[EOT]")
                cmd.Parameters.AddWithValue("@DateSent", DateTime.Now)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
            conn.Close()
            blnSucess = True

        End Using
        Return blnSucess

    End Function
    <WebMethod()> _
    Public Function SendNak(ByVal strSenderUserID As String, ByVal strRecipientUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
            cmd.ExecuteNonQuery()
            cmd.Dispose()

            cmd = conn.CreateCommand()
            cmd.CommandText = "INSERT INTO IMChats(RecipientUserID, SenderUserID, Message, DateSent) VALUES (@RecipientUserID, @SenderUserID, @Message, @DateSent)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)

            ' NAK
            cmd.Parameters.AddWithValue("@Message", "~::::=[(HANDSHAKE)]=::::~[NAK]")
            cmd.Parameters.AddWithValue("@DateSent", DateTime.Now)

            cmd.ExecuteNonQuery()
            blnSucess = True
            cmd.Dispose()
            conn.Close()
        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
    Public Function SendAck(ByVal strSenderUserID As String, ByVal strRecipientUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            cmd = conn.CreateCommand()
            cmd.CommandText = "INSERT INTO IMChats(RecipientUserID, SenderUserID, Message, DateSent) VALUES (@RecipientUserID, @SenderUserID, @Message, @DateSent)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)

            ' ACK
            cmd.Parameters.AddWithValue("@Message", "~::::=[(HANDSHAKE)]=::::~[ACK]")
            cmd.Parameters.AddWithValue("@DateSent", DateTime.Now)

            cmd.ExecuteNonQuery()
            blnSucess = True
            cmd.Dispose()
            conn.Close()
        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
    Public Function GetAck(ByVal strRecipientUserID As String, ByVal strSenderUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Dim strMessage As String
        Dim dDateSent As Date

        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = New SqlCommand("SELECT TOP(1) Message, DateSent FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID) ORDER BY DateSent DESC", conn)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                dDateSent = reader("DateSent")
                'If dDateSent > DateAdd(DateInterval.Minute, -5, DateTime.Now) Then
                strMessage = reader("Message")
                ' ACK
                If strMessage = "~::::=[(HANDSHAKE)]=::::~[ACK]" Then
                    blnSucess = True
                    'End If
                End If
            End While
            reader.Close()
            cmd.Dispose()

            If blnSucess Then
                cmd = conn.CreateCommand()
                cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
                'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
                'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

                cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
                cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
            conn.Close()

        End Using
        Return blnSucess
    End Function

    <WebMethod()> _
    Public Function GetNak(ByVal strRecipientUserID As String, ByVal strSenderUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Dim strMessage As String
        Dim dDateSent As Date

        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = New SqlCommand("SELECT TOP(1) Message, DateSent FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID) ORDER BY DateSent DESC", conn)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                dDateSent = reader("DateSent")
                'If dDateSent > DateAdd(DateInterval.Minute, -5, DateTime.Now) Then
                strMessage = reader("Message")
                ' NAK
                If strMessage = "~::::=[(HANDSHAKE)]=::::~[NAK]" Then
                    blnSucess = True
                    'End If
                End If
            End While
            reader.Close()
            cmd.Dispose()

            If blnSucess Then
                cmd = conn.CreateCommand()
                cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
                'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
                'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

                cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
                cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
            conn.Close()

        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
    Public Function SendEot(ByVal strSenderUserID As String, ByVal strRecipientUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)

            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = conn.CreateCommand()
            cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @RecipientUserID AND [RecipientUserID] = @SenderUserID)"
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            cmd.ExecuteNonQuery()
            cmd.Dispose()

            cmd = conn.CreateCommand()
            cmd.CommandText = "INSERT INTO IMChats(RecipientUserID, SenderUserID, Message, DateSent) VALUES (@RecipientUserID, @SenderUserID, @Message, @DateSent)"
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)

            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)
            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)

            ' EOT
            cmd.Parameters.AddWithValue("@Message", "~::::=[(HANDSHAKE)]=::::~[EOT]")
            cmd.Parameters.AddWithValue("@DateSent", DateTime.Now)

            cmd.ExecuteNonQuery()
            blnSucess = True
            cmd.Dispose()
            conn.Close()
        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
    Public Function GetEot(ByVal strRecipientUserID As String, ByVal strSenderUserID As String) As Boolean
        Dim blnSucess As Boolean = False
        Dim strMessage As String
        Dim dDateSent As Date

        Using conn As SqlConnection = New SqlConnection(ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString)
            conn.Open()

            Dim cmd As SqlCommand

            'Dim MemUser As MembershipUser
            'Dim strRecipientUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strRecipientUserID))
            'strRecipientUserGUID = MemUser.ProviderUserKey.ToString
            'Dim strSenderUserGUID As String
            'MemUser = Membership.GetUser(HttpUtility.UrlDecode(strSenderUserID))
            'strSenderUserGUID = MemUser.ProviderUserKey.ToString

            cmd = New SqlCommand("SELECT TOP(1) Message, DateSent FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID) ORDER BY DateSent DESC", conn)
            'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
            'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

            cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
            cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

            Dim reader As SqlDataReader = cmd.ExecuteReader
            While reader.Read()
                dDateSent = reader("DateSent")
                'If dDateSent > DateAdd(DateInterval.Minute, -5, DateTime.Now) Then
                strMessage = reader("Message")
                ' EOT
                If strMessage = "~::::=[(HANDSHAKE)]=::::~[EOT]" Then
                    blnSucess = True
                    'End If
                End If
            End While
            reader.Close()
            cmd.Dispose()

            If blnSucess Then
                cmd = conn.CreateCommand()
                cmd.CommandText = "DELETE FROM [IMChats] WHERE ([SenderUserID] = @SenderUserID AND [RecipientUserID] = @RecipientUserID)"
                'cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserGUID)
                'cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserGUID)

                cmd.Parameters.AddWithValue("@SenderUserID", strSenderUserID)
                cmd.Parameters.AddWithValue("@RecipientUserID", strRecipientUserID)

                cmd.ExecuteNonQuery()
                cmd.Dispose()
            End If
            conn.Close()

        End Using
        Return blnSucess
    End Function
    <WebMethod()> _
        Public Function GetVisibleList(ByVal objectname As String) As String


        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prGetVisibleList"
        dsmain.SelectParameters.Add("type", objectname)

        dsmain.InsertCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.InsertCommand = "prGetVisibleList"

        ''Dim param As New System.Web.UI.WebControls.Parameter("test")

        fieldName = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            For i = 0 To rc - 1
                fieldName = fieldName & dataview.Item(i).Row.Item("FieldName").ToString() & "|" & dataview.Item(i).Row.Item("IsVisible").ToString() & "|" & dataview.Item(i).Row.Item("accordion").ToString() & "|"
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        fieldName = fieldName.Replace("@", "")
        Return fieldName

    End Function
    <WebMethod()> _
        Public Function GetChequeRefConfig(ByVal objectname As String) As String

        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prChequeRefConfig"
        dsmain.SelectParameters.Add("RefName", objectname)

        dsmain.InsertCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.InsertCommand = "prChequeRefConfig"

        ''Dim param As New System.Web.UI.WebControls.Parameter("test")

        fieldName = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        'CntrlName,ActionName,AdditionalInfo
        Try
            For i = 0 To rc - 1
                fieldName = fieldName & dataview.Item(i).Row.Item("CntrlName").ToString() & "|" & dataview.Item(i).Row.Item("ActionName").ToString() & "|" & dataview.Item(i).Row.Item("AdditionalInfo").ToString() & "|"
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        fieldName = fieldName.Replace("@", "")
        Return fieldName

    End Function

    <WebMethod()> _
    Public Function FICLHTML(ByVal type As String, ByVal cutoff As String, ByVal BranchCode As String) As String
        Dim TBody_1 As String
        Dim TBody_2 As String
        Dim table As String
        table = ""
        Dim linkpage As String
        linkpage = ""
        Dim linkname As String

        Dim subtotal As Double()
        ReDim subtotal(33)
        Dim total As Double()
        ReDim total(33)

        TBody_1 = ""
        TBody_2 = ""

        Dim ColIndex As Integer
        Dim RowIndex As Integer
        Dim cds As New CommonDBSvc
        Dim row_per_sheet As Integer
        'Dim type As String
        Dim i As Integer
        ColIndex = 3
        RowIndex = 1

        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim sheetcount As Integer

        Dim rc As Integer

        For rc = 0 To 33 - 1
            total(rc) = 0
            subtotal(rc) = 0
        Next rc

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "psFICL"
        'type = "2A"
        dsmain.SelectParameters.Add("Class", type)
        dsmain.SelectParameters.Add("cutoff", cutoff)
        dsmain.SelectParameters.Add("BranchCode", BranchCode)


        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        row_per_sheet = 0
        Try

            sheetcount = 0
            For i = 0 To rc - 1
                If row_per_sheet <= 0 Then
                    sheetcount = sheetcount + 1
                    row_per_sheet = 15
                    'newsheet.Name = "FICL - " + type + "-" + sheetcount.ToString()


                    If TBody_1 = "" Then
                        TBody_1 = "<tbody style=""display:block"" id='FICL-" + type + "-" + sheetcount.ToString() + "-1'>"
                        TBody_2 = "<tbody style=""display:block"" id='FICL-" + type + "-" + sheetcount.ToString() + "-2'>"
                        linkname = "FICL-" + type + "-" + sheetcount.ToString()
                        linkpage = linkpage + "<a href=""javascript:gopage('" + linkname + "',totalsheet,'" + type + "');"">FICL - " + type + "-" + sheetcount.ToString() + "</a>&nbsp;"
                    Else
                        TBody_1 = TBody_1 + "<tr style='font-weight: bold'><td></td><td></td><td></td><td>" + String.Format("{0:#,##0.00}", subtotal(3)) + "</td><td></td>" + "<td></td>" + "<td></td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(7)) + "</td>" + "<td></td>" + "<td></td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(10)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(11)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(12)) + "</td>" + "<td></td>" + "<td></td><td></td><td></td><td></td></tr>"
                        TBody_2 = TBody_2 + "<tr style='font-weight: bold;text-align:right'><td>" + String.Format("{0:#,##0.00}", subtotal(18)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(19)) + "</td><td>" + String.Format("{0:#,##0.00}", subtotal(20)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(21)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(22)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(23)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(24)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(25)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(26)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(27)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(28)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(29)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(30)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(31)) + "</td>" + "<td></td></tr>"

                        linkname = "FICL-" + type + "-" + sheetcount.ToString()
                        TBody_1 = TBody_1 + "</tbody><tbody style=""display:none"" id='FICL-" + type + "-" + sheetcount.ToString() + "-1'>"
                        TBody_2 = TBody_2 + "</tbody><tbody style=""display:none"" id='FICL-" + type + "-" + sheetcount.ToString() + "-2'>"
                        linkpage = linkpage + "<a href=""javascript:gopage('" + linkname + "',totalsheet,'" + type + "');"">FICL - " + type + "-" + sheetcount.ToString() + "</a>&nbsp;"
                    End If
                    subtotal(3) = 0 'loanamount
                    subtotal(7) = 0 'outstanding amount
                    subtotal(10) = 0 'amountdue
                    subtotal(11) = 0 'amountpaid
                    subtotal(12) = 0 'amountarrear

                    subtotal(18) = 0 'amount standard
                    subtotal(19) = 0 'amount sma
                    subtotal(20) = 0 'amount ss
                    subtotal(21) = 0 'amount df
                    subtotal(22) = 0 'amount bl

                    subtotal(23) = 0 'suspence interest standard
                    subtotal(24) = 0 'suspence interest sma
                    subtotal(25) = 0 'suspence interest classified
                    subtotal(26) = 0 'suspence interest subtotal


                    subtotal(27) = 0 'security
                    subtotal(28) = 0 'provision sma
                    subtotal(29) = 0 'provision ss
                    subtotal(30) = 0 'provision dl
                    subtotal(31) = 0 'provision bl
                End If

                subtotal(3) = subtotal(3) + dataview.Item(i).Row.Item("Amount") 'loanamount
                subtotal(7) = subtotal(7) + dataview.Item(i).Row.Item("OutstandingAmount") 'outstanding amount
                subtotal(10) = subtotal(10) + dataview.Item(i).Row.Item("AmountDue") 'amountdue
                subtotal(11) = subtotal(11) + dataview.Item(i).Row.Item("AmountPaid") 'amountpaid
                subtotal(12) = subtotal(12) + dataview.Item(i).Row.Item("AmountInArrear") 'amountarrear
                subtotal(18) = subtotal(18) + dataview.Item(i).Row.Item("AmountStandard") 'amount standard
                subtotal(19) = subtotal(19) + dataview.Item(i).Row.Item("AmountSMA") 'amount sma
                subtotal(20) = subtotal(20) + dataview.Item(i).Row.Item("AmountSS") 'amount ss
                subtotal(21) = subtotal(21) + dataview.Item(i).Row.Item("AmountDF") 'amount df
                subtotal(22) = subtotal(22) + dataview.Item(i).Row.Item("AmountBL") 'amount bl
                subtotal(23) = subtotal(23) + dataview.Item(i).Row.Item("InterestSuspenceStandard") 'suspence interest standard
                subtotal(24) = subtotal(24) + dataview.Item(i).Row.Item("InterestSuspenceSMA") 'suspence interest sma
                subtotal(25) = subtotal(25) + dataview.Item(i).Row.Item("ClassifiedSuspence") 'suspence interest classified
                subtotal(26) = subtotal(26) + dataview.Item(i).Row.Item("TotalSuspence") 'suspence interest subtotal
                subtotal(27) = subtotal(27) + dataview.Item(i).Row.Item("EligibleSecurities") 'security
                subtotal(28) = subtotal(28) + dataview.Item(i).Row.Item("ProvisionBaseSMA") 'provision sma
                subtotal(29) = subtotal(29) + dataview.Item(i).Row.Item("ProvisionBaseSS") 'provision ss
                subtotal(30) = subtotal(30) + dataview.Item(i).Row.Item("ProvisionBaseDF") 'provision dl
                subtotal(31) = subtotal(31) + dataview.Item(i).Row.Item("ProvisionBaseBL") 'provision bl

                total(3) = total(3) + dataview.Item(i).Row.Item("Amount") 'loanamount
                total(7) = total(7) + dataview.Item(i).Row.Item("OutstandingAmount") 'outstanding amount
                total(10) = total(10) + dataview.Item(i).Row.Item("AmountDue") 'amountdue
                total(11) = total(11) + dataview.Item(i).Row.Item("AmountPaid") 'amountpaid
                total(12) = total(12) + dataview.Item(i).Row.Item("AmountInArrear") 'amountarrear
                total(18) = total(18) + dataview.Item(i).Row.Item("AmountStandard") 'amount standard
                total(19) = total(19) + dataview.Item(i).Row.Item("AmountSMA") 'amount sma
                total(20) = total(20) + dataview.Item(i).Row.Item("AmountSS") 'amount ss
                total(21) = total(21) + dataview.Item(i).Row.Item("AmountDF") 'amount df
                total(22) = total(22) + dataview.Item(i).Row.Item("AmountBL") 'amount bl
                total(23) = total(23) + dataview.Item(i).Row.Item("InterestSuspenceStandard") 'suspence interest standard
                total(24) = total(24) + dataview.Item(i).Row.Item("InterestSuspenceSMA") 'suspence interest sma
                total(25) = total(25) + dataview.Item(i).Row.Item("ClassifiedSuspence") 'suspence interest classified
                total(26) = total(26) + dataview.Item(i).Row.Item("TotalSuspence") 'suspence interest total
                total(27) = total(27) + dataview.Item(i).Row.Item("EligibleSecurities") 'security
                total(28) = total(28) + dataview.Item(i).Row.Item("ProvisionBaseSMA") 'provision sma
                total(29) = total(29) + dataview.Item(i).Row.Item("ProvisionBaseSS") 'provision ss
                total(30) = total(30) + dataview.Item(i).Row.Item("ProvisionBaseDF") 'provision dl
                total(31) = total(31) + dataview.Item(i).Row.Item("ProvisionBaseBL") 'provision bl


                TBody_1 = TBody_1 + "<tr><td>" + dataview.Item(i).Row.Item("SLNo").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("CustomerName").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AgreementNo").ToString() + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Amount")) + "</td>" + "<td>" + dataview.Item(i).Row.Item("ExecutionDate").ToString() + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("RescheduleAmount")) + "</td>" + "<td>" + dataview.Item(i).Row.Item("RescheduleDate").ToString() + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("OutstandingAmount")) + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InstallmentSize")) + "</td>" + "<td style='text-align:right'>" + dataview.Item(i).Row.Item("InstallmentFrequency").ToString() + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountDue")) + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountPaid")) + "</td>" + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountInArrear")) + "</td>" + "<td style='text-align:right'>" + dataview.Item(i).Row.Item("TimeEquivalentofArrear").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ClassificationObjective").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ClassificationQualitative").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("FinalClassification").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("BasisofClassification").ToString() + "</td></tr>"
                'TBody_2 = TBody_2 + "<tr><td>" + dataview.Item(i).Row.Item("AmountStandard").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountSMA").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountSS").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountDF").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountBL").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("InterestSuspenceStandard").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("InterestSuspenceSMA").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ClassifiedSuspence").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("TotalSuspence").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("EligibleSecurities").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ProvisionBaseSMA").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ProvisionBaseSS").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ProvisionBaseDF").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ProvisionBaseBL").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("Remarks").ToString() + "</td></tr>"

                'TBody_1 = TBody_1 + "<tr><td>" + dataview.Item(i).Row.Item("SLNo").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("CustomerName").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AgreementNo").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("Amount") + "</td>" + "<td>" + dataview.Item(i).Row.Item("ExecutionDate").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("RescheduleAmount") + "</td>" + "<td>" + dataview.Item(i).Row.Item("RescheduleDate").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("OutstandingAmount") + "</td>" + "<td>" + dataview.Item(i).Row.Item("InstallmentSize") + "</td>" + "<td>" + dataview.Item(i).Row.Item("InstallmentFrequency").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountDue") + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountPaid") + "</td>" + "<td>" + dataview.Item(i).Row.Item("AmountInArrear") + "</td>" + "<td>" + dataview.Item(i).Row.Item("TimeEquivalentofArrear") + "</td>" + "<td>" + dataview.Item(i).Row.Item("ClassificationObjective").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("ClassificationQualitative").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("FinalClassification").ToString() + "</td>" + "<td>" + dataview.Item(i).Row.Item("BasisofClassification").ToString() + "</td></tr>"
                TBody_2 = TBody_2 + "<tr style='text-align:right'><td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountStandard")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountSMA")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountSS")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountDF")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("AmountBL")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InterestSuspenceStandard")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InterestSuspenceSMA")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("ClassifiedSuspence")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("TotalSuspence")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("EligibleSecurities")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("ProvisionBaseSMA")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("ProvisionBaseSS")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("ProvisionBaseDF")) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("ProvisionBaseBL")) + "</td>" + "<td>" + dataview.Item(i).Row.Item("Remarks").ToString() + "</td></tr>"
                row_per_sheet = row_per_sheet - 1
            Next i
            If sheetcount > 1 Then
                TBody_1 = TBody_1 + "<tr style='font-weight: bold;'><td></td><td></td><td></td><td>" + String.Format("{0:#,##0.00}", subtotal(3)) + "</td><td></td>" + "<td></td>" + "<td></td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(7)) + "</td>" + "<td></td>" + "<td></td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(10)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(11)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(12)) + "</td>" + "<td></td>" + "<td></td><td></td><td></td><td></td></tr>"
                TBody_2 = TBody_2 + "<tr style='font-weight: bold;text-align:right'><td>" + String.Format("{0:#,##0.00}", subtotal(18)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(19)) + "</td><td>" + String.Format("{0:#,##0.00}", subtotal(20)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(21)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(22)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(23)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(24)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(25)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(26)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(27)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(28)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(29)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(30)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", subtotal(31)) + "</td>" + "<td></td></tr>"
            End If
            If sheetcount > 0 Then
                TBody_1 = TBody_1 + "<tr style='font-weight: bold;text-align:right'><td></td><td></td><td></td><td>" + String.Format("{0:#,##0.00}", total(3)) + "</td><td></td>" + "<td></td>" + "<td></td>" + "<td>" + String.Format("{0:#,##0.00}", total(7)) + "</td>" + "<td></td>" + "<td></td>" + "<td>" + String.Format("{0:#,##0.00}", total(10)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(11)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(12)) + "</td>" + "<td></td>" + "<td></td><td></td><td></td><td></td></tr>"
                TBody_2 = TBody_2 + "<tr style='font-weight: bold;text-align:right'><td>" + String.Format("{0:#,##0.00}", total(18)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(19)) + "</td><td>" + String.Format("{0:#,##0.00}", total(20)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(21)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(22)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(23)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(24)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(25)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(26)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(27)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(28)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(29)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(30)) + "</td>" + "<td>" + String.Format("{0:#,##0.00}", total(31)) + "</td>" + "<td></td></tr>"

                TBody_1 = TBody_1 + "</tbody>"
                TBody_2 = TBody_2 + "</tbody>"
            End If
            linkpage = linkpage.Replace("totalsheet", sheetcount.ToString())

            'table = "<table border=""1"">" + "<thead><tr><td>SL. No</td><td>Name of the Lessee</td><td>Lease No./Lease A/C No.</td><td>Lease Amount</td><td>Date of lease Execution</td><td>Rescheduled Amount</td><td>ate of rescheduling</td><td>Amount of Outstanding</td><td>Installment Size</td><td>Installment Frequency</td><td>Amount Due</td><td>Amount Paid</td><td>Amount in Arrears</td><td>Time Equivalent of Amount in Arrears</td><td>Primary Status of Classification(Objective Criteria)</td><td>Primary Status of Classification(Qualitative Judgement)</td><td>Final Classification Status</td><td>Basis for Classification</td></tr><tr><td>1</td><td>2</td><td>3</td><td>4</td><td>5</td><td>6</td><td>7</td><td>8</td><td>9</td><td>10</td><td>11</td><td>12</td><td>13</td><td>14</td><td>15</td><td>16</td><td>17</td><td>18</td></tr></thead>"
            table = "<table border=""1"" class='yui'>" + "<tr><th>SL. No.</th><th>Name of the Lessee</th><th>Lease No./</th><th>Lease</th><th>Date of lease</th><th>Rescheduled</th><th>Date of</th><th>Amount of</th><th colspan='2' rowspan='2'>Installment</th><th>Amount Due</th><th>Amount Paid</th><th>Amount in</th><th>Time Equivalent</th><th colspan='2'>Primary Status of Classification</th><th colspan='2'>Final Classification</th></tr><tr><th></th><th></th><th>Lease A/C</th><th>Amount</th><th>Execution</th><th>Amount</th><th>rescheduling</th><th>Outstanding</th><th>(As on</th><th>(As on</th><th>Arrears</th><th>of Amount</th><th>Objective</th><th>Qualitative</th><th rowspan='2'>Classification Status</th><th rowspan='2'>Basis for Classification</th></tr><tr><th></th><th></th><th>No.</th><th></th><th>DD MM YY</th><th></th><th>DD MM YY</th><th>(As on Reference Date)</th><th>Size</th><th>Frequency</th><th>Reference Date)</th><th>Reference Date)</th><th></th><th>in Arrears*</th><th>Criteria</th><th>Judgement</th></tr><tr><th>1</th><th>2</th><th>3</th><th>4</th><th>5</th><th>6</th><th>7</th><th>8</th><th>9</th><th>10</th><th>11</th><th>12</th><th>13</th><th>14</th><th>15</th><th>16</th><th>17</th><th>18</th></tr>"
            table = table + TBody_1
            'table = table + "<thead><tr><td>Amount Standard</td><td>Amount SMA</td><td>Amount Sub-standard(SS)</td><td>Amount Doubtful(DF)</td><td>Amount Bad/Loss(BL)</td><td>Interest Suspense Standard</td><td>Interest Suspense SMA</td><td>Classified A/Cs</td><td>Total Interest Suspense/td><td>Elligible securities</td><td>Provision Base for SMA(col. 20-25)</td><td>Provision Base for Sub-standard(SS) (col.21-26-28)</td><td>Provision Base for Doubtful(DF)(col.22-26-28)</td><td>Provision Base for Bad/loss(BL)(col.23-26-28)</td><td>Remarks</td></tr><tr><td>19</td><td>20</td><td>21</td><td>22</td><td>23</td><td>24</td><td>25</td><td>26</td><td>27</td><td>28</td><td>29</td><td>30</td><td>31</td><td>32</td><td>33</td></tr></thead>"
            table = table + "<tr><th colspan='2'>Amount Unclassified (UC)</th><th colspan='3'>Amount Classified</th><th colspan='4'>Cumulative interest suspense as of reference date</th><th></th><th colspan='4'>Base for Provision for</th><th rowspan='3'>Remarks</th></tr><tr><th rowspan='2'>Standard</th><th rowspan='2'>SMA</th><th rowspan='2'>Sub-standard(SS)</th><th rowspan='2'>Doubtful (DF)</th><th rowspan='2'>Bad/Loss (BL)</th><th rowspan='2'>Standard( if any)</th><th rowspan='2'>SMA</th><th rowspan='2'>Classified A/Cs</th><th rowspan='2'>Total</th><th>Value of elligible</th><th rowspan='2'>SMA (col. 20-25)</th><th rowspan='2'>Sub-standard(SS)(col.21-26-28)</th><th rowspan='2'>Doubtful (DF) (col.22-26-28)</th><th rowspan='2'>Bad/ loss (BL) (col.23-26-28)</th></tr><tr><th>securities (in nearest taka)</th></tr><tr><th>19</th><th>20</th><th>21</th><th>22</th><th>23</th><th>24</th><th>25</th><th>26</th><th>27</th><th>28</th><th>29</th><th>30</th><th>31</th><th>32</th><th>33</th></tr>"
            table = table + TBody_2
            table = table + "</table>" + "<div>" + linkpage + "</div>"

            'RenameExcelSheetsName(type)


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally

            GC.Collect()
            GC.WaitForPendingFinalizers()
        End Try
        Return table
    End Function

    Public Function getExcelColumnChar(ByVal i As Integer) As String
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
    Public Function getExcelColumnIndex(ByVal i As Integer) As String
        Dim index As String
        index = ""
        If i <= 26 Then
            index = getExcelColumnChar(i)
        ElseIf i <= 52 Then
            index = "A" + getExcelColumnChar(i - 26)
        ElseIf i <= 78 Then
            index = "B" + getExcelColumnChar(i - 52)
        End If
        Return index
    End Function

    <WebMethod()> _
    Public Function QuaterlyBusinessReport(ByVal pageid As String, ByVal StartDate As String, ByVal EndDate As String) As String
        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "psQuarterlyBusiness"
        dsmain.SelectParameters.Add("pageid", pageid)
        dsmain.SelectParameters.Add("StartDate", StartDate)
        dsmain.SelectParameters.Add("EndDate", EndDate)

        fieldName = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.InsertParameters.Count
            For i = 0 To rc - 1
                'String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Total"))
                If dataview.Item(i).Row.Item("datatype").ToString() = "NUMERIC" Then
                    fieldName = fieldName & dataview.Item(i).Row.Item("Property").ToString() & "|" & String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Value")) & "|"
                Else
                    fieldName = fieldName & dataview.Item(i).Row.Item("Property").ToString() & "|" & dataview.Item(i).Row.Item("Value").ToString() & "|"
                End If

                'fieldName = fieldName & dataview.Item(i).Row.Item("Property").ToString() & "|" & dataview.Item(i).Row.Item("Value").ToString() & "|"
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        Return fieldName

    End Function

    <WebMethod()> _
    Public Function IndustrialCreditReport(ByVal parametervalue As String) As String
        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim parameterarray As String()

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "pSIndustrialCredit"


        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
                'fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
            End If
            i = i + 1
        Next i

        fieldName = ""

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.InsertParameters.Count
            For i = 0 To rc - 1
                fieldName = fieldName + dataview.Item(i).Row.Item("Particulars") + "|" + String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Balance")) & "|"
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        Return fieldName
    End Function
    <WebMethod()> _
        Public Function NBDC_A(ByVal parametervalue As String) As String
        Dim cnt As Integer
        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(8, 19) As String

        For i = 0 To 7
            For j = 0 To 18
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 1
        valuearray(rindex, cindex - 1) = "1"
        valuearray(rindex, cindex) = "Fixed Deposits"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = "2"
        valuearray(rindex, cindex) = "Short Term Deposits"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = "3"
        valuearray(rindex, cindex) = "Long Term Deposits"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = "4"
        valuearray(rindex, cindex) = "NCD & Promissory Notes"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = "5"
        valuearray(rindex, cindex) = "Restricted (Blocked) Deposits"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = "6"
        valuearray(rindex, cindex) = "Other Deposits, Lease Deposit"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = "7"
        valuearray(rindex, cindex) = "Accrued Interest"

        rindex = rindex + 1
        valuearray(rindex, cindex - 1) = ""
        valuearray(rindex, cindex) = "Total"




        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prNBDCDCM_A"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i

        fieldName = ""

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.InsertParameters.Count
            For i = 0 To rc - 1
                If dataview.Item(i).Row.Item("AccountType").ToString() = "Fixed Deposits" Then
                    rindex = 0
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Short Term Deposits" Then
                    rindex = 1
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Long Term Deposits" Then
                    rindex = 2
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "NCD & Promissory Notes" Then
                    rindex = 3
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Restricted (Blocked) Deposits" Then
                    rindex = 4
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Other Deposits, Lease Deposit" Then
                    rindex = 5
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Accrued Interest" Then
                    rindex = 6
                End If

                If dataview.Item(i).Row.Item("NBDCSector").ToString() = "Presisency, PM's Office, Ministries & Judiciary" Then
                    cindex = 2
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Autonomous & semi autonomous bodies" Then
                    cindex = 3
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Scheduled Banks" Then
                    cindex = 4
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Local Authorities" Then
                    cindex = 5
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Public Non Financial Corporations" Then
                    cindex = 6
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Non Bank Depository Corporations- Public" Then
                    cindex = 7
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Other Financial Intermediaries- Public" Then
                    cindex = 8
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Insurance Companies and Pension funds- Public" Then
                    cindex = 9
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Non bank depository corporations-Private" Then
                    cindex = 10
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Other Financial Intermediaries- Private" Then
                    cindex = 11
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Insurance Companies and Pension funds-Private" Then
                    cindex = 12
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Financial Auxiliaries- Private" Then
                    cindex = 13
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Other Non Financial Corporation- Private" Then
                    cindex = 14
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Non Profit Institutions Serving Household" Then
                    cindex = 15
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Household residents" Then
                    cindex = 16
                End If
                valuearray(rindex, cindex + 1) = dataview.Item(i).Row.Item("Balance").ToString()
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        Dim tv As Double

        For i = 0 To 7
            tv = 0
            For j = 3 To 17
                If valuearray(i, j) = "" Then
                    tv = tv + 0
                Else
                    tv = tv + CDbl(valuearray(i, j))
                End If
            Next j
            If tv > 0 Then
                valuearray(i, 18) = tv.ToString()
            End If
        Next i



        For j = 3 To 18
            tv = 0
            For i = 0 To 6
                If valuearray(i, j) = "" Then
                    tv = tv + 0
                Else
                    tv = tv + CDbl(valuearray(i, j))
                End If
            Next
            If tv > 0 Then
                valuearray(7, j) = tv.ToString()
            End If
        Next j




        fieldName = ""
        For i = 0 To 7
            If i = 7 Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If

            For j = 0 To 18
                If j <= 2 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td style='text-align:right'>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If

            Next j
            fieldName = fieldName + "</tr>"
        Next i

        Return fieldName

    End Function
    <WebMethod()> _
        Public Function NBDC_D(ByVal parametervalue As String) As String
        Dim cnt As Integer
        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(6, 3) As String
        Dim total As Double
        total = 0

        For i = 0 To 5
            For j = 0 To 2
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 0

        valuearray(rindex, cindex) = "Un-encumbered"
        valuearray(rindex, cindex + 1) = "210"
        rindex = rindex + 1

        valuearray(rindex, cindex) = "Encumbered"
        valuearray(rindex, cindex + 1) = ""
        rindex = rindex + 1

        valuearray(rindex, cindex) = "Against REPO"
        valuearray(rindex, cindex + 1) = "215"
        rindex = rindex + 1

        valuearray(rindex, cindex) = "Others"
        valuearray(rindex, cindex + 1) = "219"
        rindex = rindex + 1

        valuearray(rindex, cindex) = "Other Investment (Un approved Securities)"
        valuearray(rindex, cindex + 1) = "220"
        rindex = rindex + 1

        valuearray(rindex, cindex) = "Total Investment as reported in Item 5-Assets side"
        valuearray(rindex, cindex + 1) = "999"

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prNBDCDCM_D"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i

        fieldName = ""

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.InsertParameters.Count
            For i = 0 To rc - 1
                If dataview.Item(i).Row.Item("AccountType").ToString() = "Un-encumbered" Then
                    rindex = 0
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Encumbered Against REPO" Then
                    rindex = 2
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Encumbered Others" Then
                    rindex = 3
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Other Investment Un approved Securities" Then
                    rindex = 4
                End If
                cindex = 2
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("Balance").ToString()
                total = total + CDbl(dataview.Item(i).Row.Item("Balance").ToString())
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        If total > 0 Then
            valuearray(5, 2) = total.ToString()
        End If

        fieldName = ""
        For i = 0 To 5
            If i = 5 Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If


            For j = 0 To 2
                If j <= 1 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td style='text-align:right'>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If
            Next j
            fieldName = fieldName + "</tr>"
        Next i
        Return fieldName

    End Function

    <WebMethod()> _
        Public Function NBDC_C(ByVal parametervalue As String, ByVal opt As String) As String
        Dim dsmain As New SqlDataSource
        Dim parameterarray As String()
        Dim dataview As Data.DataView
        Dim fieldName As String
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim rindex As Integer
        Dim cindex As Integer
        Dim valuearray(5, 6) As String
        i = 0
        j = 0
        For i = 0 To 4
            For j = 0 To 5
                valuearray(i, j) = ""
            Next j
        Next i
        rindex = 0
        cindex = 0
        If opt = "outstanding" Then

            valuearray(rindex, cindex) = "Unclassified"
            valuearray(rindex, cindex + 1) = "50100"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Substandard"
            valuearray(rindex, cindex + 1) = "50200"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Doubtful"
            valuearray(rindex, cindex + 1) = "50300"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Bad"
            valuearray(rindex, cindex + 1) = "50400"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Total"
            valuearray(rindex, cindex + 1) = "99990"
            rindex = rindex + 1
        ElseIf opt = "Provision" Then
            valuearray(rindex, cindex) = "Unclassified"
            valuearray(rindex, cindex + 1) = "50110"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Substandard"
            valuearray(rindex, cindex + 1) = "50210"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Doubtful"
            valuearray(rindex, cindex + 1) = "50310"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Bad"
            valuearray(rindex, cindex + 1) = "50410"
            rindex = rindex + 1

            valuearray(rindex, cindex) = "Total"
            valuearray(rindex, cindex + 1) = "99990"
            rindex = rindex + 1
        End If
        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "pSNBDCTable_c"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i

        fieldName = ""

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        rindex = 0
        cindex = 0
        Try
            For i = 0 To rc - 1
                If dataview.Item(i).Row.Item("ClassificationStatus").ToString() = "Unclassified" Then
                    rindex = 0
                ElseIf dataview.Item(i).Row.Item("ClassificationStatus").ToString() = "Substandard" Then
                    rindex = 1
                ElseIf dataview.Item(i).Row.Item("ClassificationStatus").ToString() = "Doubtful" Then
                    rindex = 2
                ElseIf dataview.Item(i).Row.Item("ClassificationStatus").ToString() = "Bad" Then
                    rindex = 3
                End If

                If dataview.Item(i).Row.Item("NBDCSectorType").ToString() = "Government" Then
                    cindex = 2
                ElseIf dataview.Item(i).Row.Item("NBDCSectorType").ToString() = "Other Public" Then
                    cindex = 3
                ElseIf dataview.Item(i).Row.Item("NBDCSectorType").ToString() = "Private" Then
                    cindex = 4
                End If
                If opt = "outstanding" Then
                    valuearray(rindex, cindex) = dataview.Item(i).Row.Item("Outstanding").ToString()
                ElseIf opt = "Provision" Then
                    valuearray(rindex, cindex) = dataview.Item(i).Row.Item("Provision").ToString()
                End If
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try

        Dim tv As Double

        For i = 0 To 3
            tv = 0
            For j = 2 To 4
                If valuearray(i, j) = "" Then
                    tv = tv + 0
                Else
                    tv = tv + CDbl(valuearray(i, j))
                End If
            Next j
            If tv > 0 Then
                valuearray(i, 5) = tv.ToString()
            End If
        Next i



        For j = 2 To 5
            tv = 0
            For i = 0 To 3
                If valuearray(i, j) = "" Then
                    tv = tv + 0
                Else
                    tv = tv + CDbl(valuearray(i, j))
                End If
            Next
            If tv > 0 Then
                valuearray(4, j) = tv.ToString()
            End If
        Next j

        fieldName = ""
        For i = 0 To 4
            If i = 4 Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If

            For j = 0 To 5
                If j <= 1 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td style='text-align:right'>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If

            Next j
            fieldName = fieldName + "</tr>"
        Next i

        Return fieldName

    End Function
    <WebMethod()> _
        Public Function NBDC_Top(ByVal StartDate As String, ByVal EndDate As String) As String
        Dim cnt As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer


        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prGenerateNBDCTopPage"
        dsmain.SelectParameters.Add("StartDate", StartDate)
        dsmain.SelectParameters.Add("EndDate", EndDate)

        fieldName = ""
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            cnt = dsmain.InsertParameters.Count
            For i = 0 To rc - 1
                'String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Total"))
                fieldName = fieldName & dataview.Item(i).Row.Item("Particulars").ToString() & "|" & String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Amount")) & "|"
                'fieldName = fieldName & dataview.Item(i).Row.Item("Property").ToString() & "|" & dataview.Item(i).Row.Item("Value").ToString() & "|"
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        Return fieldName

    End Function
    <WebMethod()> _
        Public Function NBDC_OtherAssetsLiabilities(ByVal parametervalue As String) As String

        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim totalasset As Double
        Dim totalliability As Double
        Dim parameterarray As String()
        Dim valuearray(15, 4) As String
        totalasset = 0
        totalliability = 0
        For i = 0 To 14
            For j = 0 To 3
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 0

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prNBDCDCM_OtherAssetsLiabilities"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i
        dsmain.SelectParameters.Add("option", "Other Liabilities")
        cindex = 0
        rindex = 0

        fieldName = ""

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            For i = 0 To rc - 1
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("AccountType").ToString()
                valuearray(rindex, cindex + 1) = dataview.Item(i).Row.Item("Balance").ToString()
                totalliability = totalliability + CDbl(dataview.Item(i).Row.Item("Balance").ToString())
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        dsmain.SelectParameters.Clear()
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i
        dsmain.SelectParameters.Add("option", "Other Assets")
        cindex = 2
        rindex = 0

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            For i = 0 To rc - 1
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("AccountType").ToString()
                valuearray(rindex, cindex + 1) = dataview.Item(i).Row.Item("Balance").ToString()
                totalasset = totalasset + CDbl(dataview.Item(i).Row.Item("Balance").ToString())
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        valuearray(rindex, 0) = "Total"
        valuearray(rindex, 1) = totalliability.ToString()
        valuearray(rindex, 2) = "Total"
        valuearray(rindex, 3) = totalasset.ToString()

        fieldName = ""
        For i = 0 To 14
            If i = 14 Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If

            For j = 0 To 3
                If j = 0 Or j = 2 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If

            Next j
            fieldName = fieldName + "</tr>"
        Next i
        Return fieldName
    End Function
    <WebMethod()> _
        Public Function NBDC_OtherBorrowings(ByVal parametervalue As String) As String

        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView

        Dim subtotalbb As Double
        Dim subtotalcb As Double

        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(15, 4) As String

        For i = 0 To 14
            For j = 0 To 3
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 0

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prNBDCDCM_OtherBorrowings"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i
        dsmain.SelectParameters.Add("option", "other borrowing from BB")
        cindex = 0
        fieldName = ""

        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            For i = 0 To rc - 1
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("CustomerName").ToString()
                If i = 0 Then
                    valuearray(rindex, cindex + 1) = "Code No. 33401"
                End If
                If dataview.Item(i).Row.Item("CustomerName").ToString() = "<b>Sub Total</b>" Then
                    subtotalbb = CDbl(dataview.Item(i).Row.Item("Balance").ToString())
                End If
                valuearray(rindex, cindex + 2) = dataview.Item(i).Row.Item("Balance").ToString()
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try

        dsmain.SelectParameters.Clear()
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i

        dsmain.SelectParameters.Add("option", "other borrowing from CB")
        cindex = 0
        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            For i = 0 To rc - 1
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("CustomerName").ToString()
                If i = 0 Then
                    valuearray(rindex, cindex + 1) = "Code No. 33509"
                End If
                If dataview.Item(i).Row.Item("CustomerName").ToString() = "<b>Sub Total</b>" Then
                    subtotalcb = CDbl(dataview.Item(i).Row.Item("Balance").ToString())
                End If
                valuearray(rindex, cindex + 2) = dataview.Item(i).Row.Item("Balance").ToString()
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try

        valuearray(rindex, 0) = "<b>Total</b>"
        valuearray(rindex, 2) = (subtotalbb + subtotalcb).ToString()

        fieldName = ""
        For i = 0 To rindex
            If i = rindex Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If

            For j = 0 To 3
                If j <= 1 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If
            Next j
            fieldName = fieldName + "</tr>"
        Next i
        Return fieldName
    End Function
    <WebMethod()> _
        Public Function NBDC_OtherBorrowingsCallLoan(ByVal parametervalue As String) As String

        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(30, 4) As String

        For i = 0 To 30
            For j = 0 To 2
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 0

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prNBDCDCM_OtherBorrowings"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i
        dsmain.SelectParameters.Add("option", "Call Loan Borrowing")
        cindex = 0
        fieldName = ""
        Try
            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
            rc = dataview.Count

            For i = 0 To rc - 1
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("CustomerName").ToString()
                If i = 0 Then
                    valuearray(rindex, cindex + 1) = "Code No. 33501"
                End If
                valuearray(rindex, cindex + 2) = dataview.Item(i).Row.Item("Balance").ToString()
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try


        fieldName = ""
        For i = 0 To rindex - 1
            If i = rindex - 1 Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If

            For j = 0 To 2
                If j <= 1 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If

            Next j
            fieldName = fieldName + "</tr>"
        Next i
        Return fieldName
    End Function
    <WebMethod()> _
        Public Function NBDC_AccruedInterest(ByVal parametervalue As String) As String

        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(4, 6) As String

        For i = 0 To 3
            For j = 0 To 5
                valuearray(i, j) = "0"
            Next j
        Next i

        rindex = 0
        cindex = 0

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "prNBDCDCM_E"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i

        cindex = 0
        fieldName = ""

        valuearray(0, 0) = "Investment(Securities)"
        valuearray(1, 0) = "Advances"
        valuearray(2, 0) = "Total"
        valuearray(0, 1) = "310"
        valuearray(1, 1) = "315"
        valuearray(2, 1) = "999"


        dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
        rc = dataview.Count
        Try
            For i = 0 To rc - 1
                If dataview.Item(i).Row.Item("AccountType").ToString() = "Investment(Securities)" Then
                    rindex = 0
                ElseIf dataview.Item(i).Row.Item("AccountType").ToString() = "Advances" Then
                    rindex = 1
                End If

                If dataview.Item(i).Row.Item("NBDCSector").ToString() = "Government" Then
                    cindex = 2
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Other Public" Then
                    cindex = 3
                ElseIf dataview.Item(i).Row.Item("NBDCSector").ToString() = "Private" Then
                    cindex = 4
                End If
                valuearray(rindex, cindex) = dataview.Item(i).Row.Item("Balance").ToString()
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        valuearray(0, 5) = (CDbl(valuearray(0, 2)) + CDbl(valuearray(0, 3)) + CDbl(valuearray(0, 4))).ToString()
        valuearray(1, 5) = (CDbl(valuearray(1, 2)) + CDbl(valuearray(1, 3)) + CDbl(valuearray(1, 4))).ToString()

        valuearray(2, 2) = (CDbl(valuearray(0, 2)) + CDbl(valuearray(1, 2))).ToString()
        valuearray(2, 3) = (CDbl(valuearray(0, 3)) + CDbl(valuearray(1, 3))).ToString()
        valuearray(2, 4) = (CDbl(valuearray(0, 4)) + CDbl(valuearray(1, 4))).ToString()
        valuearray(2, 5) = (CDbl(valuearray(0, 5)) + CDbl(valuearray(1, 5))).ToString()


        fieldName = ""
        For i = 0 To 2
            If i = 2 Then
                fieldName = fieldName + "<tr style='font-weight:bold'>"
            Else
                fieldName = fieldName + "<tr>"
            End If

            For j = 0 To 5
                If j <= 1 Then
                    fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                Else
                    If valuearray(i, j) = "" Then
                        fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
                    Else
                        fieldName = fieldName + "<td style='text-align:right'>" + String.Format("{0:#,##0.00}", CDbl(valuearray(i, j))) + "</td>"
                    End If
                End If
            Next j
            fieldName = fieldName + "</tr>"
        Next i
        Return fieldName
    End Function
    <WebMethod()> _
        Public Function FICL_Top(ByVal parametervalue As String) As String

        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(10, 17) As String

        For i = 0 To 9
            For j = 0 To 16
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 0

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "pSFICLTopPage"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i
        cindex = 0
        fieldName = ""
        Try
            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
            rc = dataview.Count

            For i = 0 To rc - 1
                valuearray(rindex, 0) = dataview.Item(i).Row.Item("Serial").ToString()
                valuearray(rindex, 1) = dataview.Item(i).Row.Item("Sectors").ToString()
                valuearray(rindex, 2) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Total"))
                valuearray(rindex, 3) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Outstanding_Standard"))
                valuearray(rindex, 4) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Outstanding_SMA"))
                valuearray(rindex, 5) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Outstanding_SS"))
                valuearray(rindex, 6) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Outstanding_DF"))
                valuearray(rindex, 7) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("Outstanding_BL"))
                valuearray(rindex, 8) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("BaseProvision_SMA"))
                valuearray(rindex, 9) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("BaseProvision_SS"))
                valuearray(rindex, 10) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("BaseProvision_DF"))
                valuearray(rindex, 11) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("BaseProvision_BL"))
                valuearray(rindex, 12) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("ProvisionAmount"))
                valuearray(rindex, 13) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InterestSuspence_Standard"))
                valuearray(rindex, 14) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InterestSuspence_SMA"))
                valuearray(rindex, 15) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InterestSuspence_Classified"))
                valuearray(rindex, 16) = String.Format("{0:#,##0.00}", dataview.Item(i).Row.Item("InterestSuspence_Total"))
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try
        fieldName = ""
        For i = 0 To rindex - 1
            fieldName = fieldName + "<tr>"
            For j = 0 To 16
                fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
            Next j
            fieldName = fieldName + "</tr>"
        Next i
        Return fieldName
    End Function
    <WebMethod()> _
        Public Function FICL_PageRefer(ByVal parametervalue As String) As String

        Dim rindex As Integer
        Dim cindex As Integer
        Dim fieldName As String
        Dim dsmain As New SqlDataSource
        Dim dataview As Data.DataView
        Dim rc As Integer
        Dim i As Integer
        Dim j As Integer
        Dim parameterarray As String()
        Dim valuearray(10, 2) As String

        For i = 0 To 9
            For j = 0 To 1
                valuearray(i, j) = ""
            Next j
        Next i

        rindex = 0
        cindex = 0

        dsmain.ConnectionString = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
        dsmain.SelectCommandType = SqlDataSourceCommandType.StoredProcedure
        dsmain.SelectCommand = "pSFICLPageNo"

        parameterarray = parametervalue.Split("|")
        For i = 0 To parameterarray.Length - 1
            If parameterarray(i) <> "" Then
                dsmain.SelectParameters.Add(parameterarray(i), parameterarray(i + 1))
            End If
            i = i + 1
        Next i
        cindex = 0
        fieldName = ""
        Try
            dataview = dsmain.Select(System.Web.UI.DataSourceSelectArguments.Empty)
            rc = dataview.Count

            For i = 0 To rc - 1
                valuearray(rindex, 0) = dataview.Item(i).Row.Item("FICLProductClass").ToString()
                valuearray(rindex, 1) = dataview.Item(i).Row.Item("pageno").ToString()
                rindex = rindex + 1
            Next i
        Catch ex As Exception
            fieldName = "-ERR"
        End Try


        fieldName = "<table class='yui'><tr><th colspan='2' style='text-align:center'>CHECK LIST</th></tr><tr><th>FORM NO</th><th>No. of Pages</th></tr>"
        For i = 0 To rindex - 1
            fieldName = fieldName + "<tr>"
            For j = 0 To 1
                fieldName = fieldName + "<td>" + valuearray(i, j) + "</td>"
            Next j
            fieldName = fieldName + "</tr>"
        Next i
        fieldName = fieldName + "</table>"
        Return fieldName
    End Function

    <WebMethod()> _
    Public Function ShowMaturityReport(ByVal endDate As String) As String
        Dim filename As String
        Dim comdb As New CommonDBSvc
        filename = comdb.AssetLiabilityMaturityData(endDate)
        Return filename
    End Function
    <WebMethod()> _
    Public Function ExportTaxReport(ByVal empid As String, ByVal EffectiveStatus As String, ByVal startdate As String, ByVal enddate As String) As String

        Dim Str, ColName, ColType, ColValue As String
        Dim Query As String
        Dim tmpID As String
        Dim i, r As Integer
        Dim comDB As New CommonDBSvc

        Dim rd As SqlDataReader
        Try
            Query = "SELECT EmployeeID FROM Employee WHERE EmployeeID LIKE '" + empid + "' and ApplicationStatus not in('New','Sent for Approval','Terminated')"
            Dim cnStr As String = ConfigurationManager.ConnectionStrings("cnStringMain").ConnectionString
            Dim con As New SqlConnection(cnStr)
            Dim cmd As New SqlCommand(Query, con)
            Str = ""
            tmpID = ""
            con.Open()
            cmd.ExecuteScalar()
            rd = cmd.ExecuteReader()
            r = 0
            ColName = ""
            ColType = ""
            ColValue = ""
            If rd.HasRows Then
                Do While (rd.Read())
                    empid = rd.GetValue(i).ToString()
                    comDB.ExportTaxReport(empid, EffectiveStatus, startdate, enddate)
                Loop
            End If
            con.Close()

        Catch ex As Exception
            Str = "No Record Exists!!!"
            Return Str
        End Try
        Return Str

    End Function

End Class


