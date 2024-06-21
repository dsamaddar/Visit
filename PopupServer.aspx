<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PopupServer.aspx.vb" Inherits="PopupServer" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="_assets/themes/yui/style.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server" EnablePartialRendering="true">
        <Services>
            <asp:ServiceReference Path="~/CommonService.asmx" />
        </Services>
    </asp:ScriptManager>
    <script type="text/jscript">

        var CurPage;
        var Query;
        var PageSize = 10;
        var TotalPages;
        var FilterString = "";
        var FilterBoxContent = "";
        var SortColumn = "";
        
        var ColumnNames = new Array();
        
        function OnFailed(error, userContext) {
            alert("Failed:" + userContext);
        }

        function getURLParam(strParamName) {
            var strReturn = "";
            var strHref = window.location.href;
            var exp = new RegExp("[+]", "g");
            if (strHref.indexOf("?") > -1) {
                var strQueryString = strHref.substr(strHref.indexOf("?"));
                var aQueryString = strQueryString.split("&");
                for (var iParam = 0; iParam < aQueryString.length; iParam++) {
                    if (aQueryString[iParam].toLowerCase().indexOf(strParamName.toLowerCase() + "=") > -1) {
                        var aParam = aQueryString[iParam].split("=");
                        strReturn = aParam[1].replace(exp, " ");
                        break;
                    }
                }
            }
            return unescape(strReturn);
        }

        function RowSelected(row) {
            var rowdata, headerdata;
            var colName;
            rowdata = document.getElementById("row" + row).getElementsByTagName("td");

            var placement = getURLParam("Placement");
            var selColumn = getURLParam("SelColumn");
            var ColType = getURLParam("ColType");
            if (ColType == "html")
                opener.document.getElementById(placement).innerHTML = rowdata[selColumn * 1].innerHTML;
            else if (ColType == "value") {
                opener.document.getElementById(placement).value = rowdata[selColumn * 1].innerHTML;
            }
            else if (ColType == "valueload") {
                opener.document.getElementById(placement).value = rowdata[selColumn * 1].innerHTML;
                opener.document.getElementById(placement).onchange();
            }
            window.close();

        }

        function LoadPage() {
            var CountQuery = "";
            var CurQuery = "";
            
            var Selectpart = Query.substring(Query.indexOf("select "), Query.indexOf(" from "));
            if (FilterString != "") {
                if (Query.indexOf(" where ") > -1) {
                    CurQuery = "with tempview as(select ROW_NUMBER() over(order by " + SortColumn + ") [Row Index]," + Query.replace("select", "") + " and (" + FilterString + ")) select * from tempview where ([Row Index] between " + ((CurPage - 1) * PageSize + 1) + " and " + (CurPage * PageSize) + ") order by [Row Index]";
                    CountQuery = "select count(*) " + Query.replace(Selectpart, "") + " and (" + FilterString + ")";
                }
                else {
                    CurQuery = "with tempview as(select ROW_NUMBER() over(order by " + SortColumn + ") [Row Index]," + Query.replace("select", "") + " where (" + FilterString + ")) select * from tempview where ([Row Index] between " + ((CurPage - 1) * PageSize + 1) + " and " + (CurPage * PageSize) + ") order by [Row Index]";
                    CountQuery = "select count(*) " + Query.replace(Selectpart, "") + " where (" + FilterString + ")";
                }                                
            }
            else {
                CurQuery = "with tempview as(select ROW_NUMBER() over(order by " + SortColumn + ") [Row Index]," + Query.replace("select", "") + ") select * from tempview where ([Row Index] between " + ((CurPage - 1) * PageSize + 1) + " and " + (CurPage * PageSize) + ") order by [Row Index]";
                CountQuery = "select count(*) " + Query.replace(Selectpart, "");
            }
            CommonService.GetQueryResult(CountQuery, OnSuccessGetTotalPages, OnFailed, CurQuery);
        }

        function GetFilteredData() {
            CurPage = 1;
            FilterBoxContent = "";
            FilterString = "";
            var TotalCol = document.getElementById("dynamictable").title;
            for (var i = 1; i < TotalCol; i++) {
                if (document.getElementById("filterBox" + i)) {
                    FilterBoxContent = FilterBoxContent + document.getElementById("filterBox" + i).value + "|";
                    if (document.getElementById("filterBox" + i).value != "") {
                        if (FilterString == "")
                            //FilterString += " [" + ColumnNames[i-1] + "] like '" + document.getElementById("filterBox" + i).value + "%'";
                            FilterString += " [" + ColumnNames[i - 1] + "] like '%" + document.getElementById("filterBox" + i).value + "%'";
                        else
                            //FilterString += " and [" + ColumnNames[i - 1] + "] like '" + document.getElementById("filterBox" + i).value + "%'";
                            FilterString += " and [" + ColumnNames[i - 1] + "] like '%" + document.getElementById("filterBox" + i).value + "%'";
                    }
                }
            }
            LoadPage();
        }

        function GetFirstPage() {
            CurPage = 1;
            LoadPage();
        }

        function GetLastPage() {
            CurPage = TotalPages;
            LoadPage();
        }

        function GetPrevPage() {
            if (CurPage > 1) {
                CurPage = CurPage - 1;
                LoadPage();
            }
        }

        function GetNextPage() {
            if (CurPage < TotalPages) {
                CurPage = CurPage + 1;
                LoadPage();
            }
        }

        function OnSuccessGetTotalPages(result, userContext, eventArgs) {
            var TotalRows = result.replace("!@", "");
            
            if (TotalRows % PageSize>0) 
                TotalPages=parseInt(TotalRows / PageSize,0)+1;
            else
                TotalPages = parseInt(TotalRows / PageSize, 0);
            
            
            document.getElementById("pagedisplaytext").value = CurPage + " of " + TotalPages;
            
            CommonService.GetDynamicTable3(userContext, onSuccessCreateTable, OnFailed);
            
        }

        function pageLoad() {
            Query = getURLParam("Query");
            SortColumn = getURLParam("SortCol");
            
            var exp = new RegExp(".Eq.", "g");
            Query = Query.replace(exp, "=");

            var exp1 = new RegExp(".Plus.", "g");
            Query = Query.replace(exp1, "+");

            CurPage = 1;
            var CurQuery = "with tempview as(select ROW_NUMBER() over(order by " + SortColumn + ") [Row Index]," + Query.replace("select", "") + ") select top 1 * from tempview ";
            
            ColumnNames = Query.substring(Query.indexOf("select ") + 7, Query.indexOf(" from ")).split(",");
            for (var i = 0; i < ColumnNames.length; i++) {
                var tempArray=ColumnNames[i].split(" ");
                ColumnNames[i] = tempArray[0];
                
            }
            //alert(ColumnNames);
            //alert(CurQuery);
            CommonService.GetDynamicTableHeader(CurQuery,false,"", onSuccessInitTable, OnFailed);
                        
        }

        function onSuccessInitTable(result, userContext, eventArgs) {            
            document.getElementById("tDiv").innerHTML = result;
            LoadPage();
        }

        function onSuccessCreateTable(result, userContext, eventArgs) {            
            var tbody = document.getElementById("dynamictablebody");
            for (var i = 1; i <= PageSize; i++) {
                var rowelem = document.getElementById("row" + i);
                if (rowelem) {
                    tbody.removeChild(rowelem);
                }
            }
            if (result != "No Record exists!!!") {
                console.log(result);
                var rowdata = result.split("~@~");
                for (var r = 1; r < rowdata.length; r++) {
                    var newrow = document.createElement("tr");
                    newrow.setAttribute("id", "row" + r);
                    newrow.onmouseover = function() {
                        this.style.cursor = 'pointer';
                    }
                    newrow.onclick = function() {
                        var rowdata = document.getElementById(this.id).getElementsByTagName("td");
                        var placement = getURLParam("Placement");
                        var selColumn = getURLParam("SelColumn");
                        var ColType = getURLParam("ColType");
                        if (ColType == "html")
                            opener.document.getElementById(placement).innerHTML = rowdata[selColumn * 1].innerHTML;
                        else if (ColType == "value") {
                            opener.document.getElementById(placement).value = rowdata[selColumn * 1].innerHTML;
                        }
                        else if (ColType == "valueload") {
                            opener.document.getElementById(placement).value = rowdata[selColumn * 1].innerHTML;
                            opener.document.getElementById(placement).onchange();
                        }
                        window.close();
                    }
                    
                    tbody.appendChild(newrow);
                    var coldata = rowdata[r].split("^|^");
                    for (var c = 1; c < coldata.length; c++) {
                        var newcol = document.createElement("td");
                        if (coldata[c].indexOf("align:right:") > -1) {
                            newcol.setAttribute("align", "right");
                            coldata[c] = coldata[c].replace("align:right:", "");
                        }
                        else {
                            newcol.setAttribute("align", "left");
                            coldata[c] = coldata[c].replace("align:left:", "");
                        }
                        newcol.innerHTML = coldata[c];
                        newrow.appendChild(newcol);
                        //if (r == rowdata.length - 1) {
                        //    document.getElementById("filterBox" + c).style.width = newcol.offsetWidth;
                        //}
                    }
                }
            }
        }
        
    </script>

    <div id="tDiv" runat="server">
    </div>
    </form>
</body>
</html>
