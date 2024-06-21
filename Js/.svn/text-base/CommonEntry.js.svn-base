var ColumnNo;
var TableHeader;
var TableName;

var SelectQuery;
var InsertQuery;
var UpdateQuery;
var DeleteQuery;
var UserID;
var PKField;

function OnFailed(error) {
    alert("Failed");
}

function getURLParam(strParamName) {
    var strReturn = "";
    var strHref = window.location.href;
    var exp = new RegExp("[+]", "g");
    if (strHref.indexOf("?") > -1) {
        var strQueryString = strHref.substr(strHref.indexOf("?")).toLowerCase();
        var aQueryString = strQueryString.split("&");
        for (var iParam = 0; iParam < aQueryString.length; iParam++) {
            if (aQueryString[iParam].indexOf(strParamName.toLowerCase() + "=") > -1) {
                var aParam = aQueryString[iParam].split("=");
                strReturn = aParam[1].replace(exp, " ");
                break;
            }
        }
    }
    return unescape(strReturn);
}

function CreateTable(TH, TN, UID) {
    TableHeader = TH;
    TableName = TN;
    InsertQuery = "pI" + TableName;
    UpdateQuery = "pU" + TableName;
    DeleteQuery = "pD" + TableName;
    UserID = UID;

    CommonService.GetParameterList(InsertQuery, onSuccessGetParam1, OnFailed);
    CommonService.GetParameterList(DeleteQuery, onSuccessGetParam2, OnFailed);
    CommonService.TableColumnList(TableName, onSuccessTableColumnList, OnFailed);

}

function LoadData() {
    CommonService.GetDynamicTable(SelectQuery, "2", "1", OnCreateTableSuccess, OnFailed);
}

function RowMouseOver(row) {
    var selectedrow;
    var colName;
    selectedrow = document.getElementById("row" + row.toString());
    selectedrow.style.cursor = 'hand';
}

function RowSelected(row) {
    if (document.getElementById("HeaderRow") != null) {

        var rowdata, headerdata;
        var colName;
        rowdata = document.getElementById("row" + row.toString()).getElementsByTagName("td");
        headerdata = document.getElementById("HeaderRow").getElementsByTagName("th");

        for (var i = 1; i < rowdata.length - 1; i++) {
            colName = headerdata[i].getElementsByTagName("a")[0].innerHTML;
            if (colName.trim() != "Row Index") {
                alert(colName);

                //document.getElementById('ctl00_Body_lbl' + colName).innerHTML = rowdata[i].innerHTML;
                //document.getElementById('lbl' + colName).innerHTML = rowdata[i].innerHTML;
                //if (colName != "LastUpdate" && colName != "Version" && colName != "UserID") {
                //document.getElementById("ctl00$Body$txt" + colName).value = rowdata[i].innerHTML;
                //document.getElementById("txt" + colName).value = rowdata[i].innerHTML;
                //}

            }
        }
    }
}

function NewEntry() {
    document.getElementById("FormView").style.display = "none";
    document.getElementById("FormEdit").style.display = "block";
    document.getElementById(PKField).disabled = "";
    var headerdata;
    var colName;

    document.getElementById("ctl00$Body$btnNew").style.display = "none";
    document.getElementById("ctl00$Body$btnEdit").style.display = "none";
    document.getElementById("ctl00$Body$btnInsert").style.display = "block";
    document.getElementById("ctl00$Body$btnUpdate").style.display = "none";
    document.getElementById("ctl00$Body$btnDelete").style.display = "none";
    document.getElementById("ctl00$Body$btnCancel").style.display = "block";

    if (document.getElementById("HeaderRow") != null) {
        headerdata = document.getElementById("HeaderRow").getElementsByTagName("th");
        for (var i = 0; i < ColumnNo; i++) {
            colName = headerdata[i].getElementsByTagName("a")[0].innerHTML;
            if (colName != "LastUpdate" && colName != "Version" && colName != "UserID")
                document.getElementById('ctl00$Body$txt' + colName).value = "";
        }
    }
}

function CreateEntryForm() {
    var insertparams = document.getElementById('ctl00$Body$Param1').value;
    var EditFormStr, ViewFormStr;
    EditFormStr = "";
    ViewFormStr = "";
    var tokens = insertparams.split("|");

    EditFormStr = "<table class=\"yui\" border=\"1\">";
    ViewFormStr = "<table class=\"yui\" border=\"1\">";

    while (i < tokens.length) {
        if (tokens[i] != "UserID") {
            ctrlvalue = UserID;
        }
    }
}


function InsertData() {
    var ctrlname;
    var ctrlvalue;
    var ctrl;
    var submitvalue;
    submitvalue = "";
    var insertparams = document.getElementById('ctl00$Body$Param1').value;

    var tokens = insertparams.split("|");
    var i = 0;
    while (i < tokens.length) {

        if (tokens[i] == "UserID") {
            ctrlvalue = UserID;
        }
        else {
            ctrlname = 'ctl00$Body$txt' + tokens[i];
            ctrl = document.getElementById(ctrlname);
            if (ctrl == null) {
                if (tokens[i + 1] == "checkbox")
                    ctrlvalue = "false";
                else
                    ctrlvalue = "";
            }
            else {
                if (ctrl.type == 'text')
                    ctrlvalue = ctrl.value;
                else if (ctrl.type == "checkbox")
                    ctrlvalue = ctrl.checked;
                else
                    ctrlvalue = ctrl.value;
            }

        }
        submitvalue = submitvalue + tokens[i] + "|" + ctrlvalue + "|";
        i = i + 2;
    }

    CommonService.AjaxExecute(InsertQuery, submitvalue, onInsertSuccess, OnFailed);
}

function onInsertSuccess(result, userContext, eventArgs) {
    LoadData();
    CancelEntry();
    alert(result);
}

function EditData() {
    if (document.getElementById("HeaderRow") != null) {
        document.getElementById("FormView").style.display = "none";
        document.getElementById("FormEdit").style.display = "block";
        document.getElementById(PKField).disabled = "true";
        var headerdata;
        var colName;
        headerdata = document.getElementById("HeaderRow").getElementsByTagName("th");

        document.getElementById("ctl00$Body$btnNew").style.display = "none";
        document.getElementById("ctl00$Body$btnEdit").style.display = "none";
        document.getElementById("ctl00$Body$btnInsert").style.display = "none";
        document.getElementById("ctl00$Body$btnUpdate").style.display = "block";
        document.getElementById("ctl00$Body$btnDelete").style.display = "none";
        document.getElementById("ctl00$Body$btnCancel").style.display = "block";
    }
}

function CancelEntry() {
    document.getElementById("FormView").style.display = "block";
    document.getElementById("FormEdit").style.display = "none";

    document.getElementById("ctl00$Body$btnNew").style.display = "block";
    document.getElementById("ctl00$Body$btnEdit").style.display = "block";
    document.getElementById("ctl00$Body$btnInsert").style.display = "none";
    document.getElementById("ctl00$Body$btnUpdate").style.display = "none";
    document.getElementById("ctl00$Body$btnDelete").style.display = "block";
    document.getElementById("ctl00$Body$btnCancel").style.display = "none";

    var headerdata;
    var colName;
    if (document.getElementById("HeaderRow") != null) {

        headerdata = document.getElementById("HeaderRow").getElementsByTagName("th");
        for (var i = 0; i < ColumnNo - 1; i++) {
            colName = headerdata[i].getElementsByTagName("a")[0].innerHTML;
            if (colName != "LastUpdate" && colName != "Version" && colName != "UserID")
                document.getElementById("ctl00$Body$txt" + colName).value = document.getElementById('ctl00_Body_lbl' + colName).innerHTML

        }
    }
}

function UpdateData() {
    var ctrlname;
    var ctrlvalue;
    var ctrl;
    var submitvalue;
    submitvalue = "";
    var UpdateParams = document.getElementById('ctl00$Body$Param1').value;

    var tokens = UpdateParams.split("|");
    var i = 0;
    while (i < tokens.length) {
        if (tokens[i] == "UserID") {
            ctrlvalue = UserID;
        }
        else {
            ctrlname = 'ctl00$Body$txt' + tokens[i];
            ctrl = document.getElementById(ctrlname);
            if (ctrl == null) {
                if (tokens[i + 1] == "checkbox")
                    ctrlvalue = "false";
                else
                    ctrlvalue = "";
            }
            else {
                if (ctrl.type == 'text')
                    ctrlvalue = ctrl.value;
                else if (ctrl.type == "checkbox")
                    ctrlvalue = ctrl.checked;
                else
                    ctrlvalue = ctrl.value;
            }
        }
        submitvalue = submitvalue + tokens[i] + "|" + ctrlvalue + "|";
        i = i + 2;
    }
    CommonService.AjaxExecute(UpdateQuery, submitvalue, onUpdateSuccess, OnFailed);
}

function onUpdateSuccess(result, userContext, eventArgs) {
    LoadData();
    CancelEntry();
    alert("Updated Successfully");
}

function DeleteData(row) {
    var submitvalue;
    var deleteparams = document.getElementById('ctl00$Body$Param2').value;
    var tokens = deleteparams.split("|");
    if (document.getElementById("HeaderRow") != null) {

        rowdata = document.getElementById("row" + row.toString()).getElementsByTagName("td");
        submitvalue = tokens[0] + "|" + rowdata[0].innerHTML + "|" + tokens[2] + "|" + UserID + "|";
        CommonService.AjaxExecute(DeleteQuery, submitvalue, onDeleteSuccess, OnFailed);
    }
}

function DeleteData2() {
    var submitvalue;
    var deleteparams = document.getElementById('ctl00$Body$Param2').value;
    var tokens = deleteparams.split("|");
    if (document.getElementById("HeaderRow") != null) {
        submitvalue = tokens[0] + "|" + document.getElementById("ctl00$Body$txt" + tokens[0]).value + "|" + tokens[2] + "|" + UserID + "|";
        CommonService.AjaxExecute(DeleteQuery, submitvalue, onDeleteSuccess, OnFailed);
    }
}

function onDeleteSuccess(result, userContext, eventArgs) {
    LoadData();
    alert("Deleted Successfully");
}

function onSuccessGetParam1(result, userContext, eventArgs) {
    document.getElementById('ctl00$Body$Param1').value = result;
}

function onSuccessGetParam2(result, userContext, eventArgs) {
    document.getElementById('ctl00$Body$Param2').value = result;
}

function onSuccessTableColumnList(result, userContext, eventArgs) {
    var tokens = result.split("@");
    var tokenvalues;
    var colDesc, tmpstr;
    var FormEditStr, FormViewStr;

    FormEditStr = "<table class=\"yui\" border=\"1\">";
    FormViewStr = "<table class=\"yui\" border=\"1\">";
    SelectQuery = "select ROW_NUMBER() over(order by LastUpdate desc) [Row Index],";
    var index = 0;
    ColumnNo = tokens.length;
    for (index = 0; index < ColumnNo * 1; index++) {
        if (tokens[index] != "") {
            tokenvalues = tokens[index].split(">");
            if (index == 0)
                PKField = 'ctl00$Body$txt' + tokenvalues[0];

            colDesc = tokenvalues[3].split("!");
            if (tokenvalues[0] != "Version" && tokenvalues[0] != "UserID" && tokenvalues[0] != "LastUpdate") {
                tmpstr = "<tr><th>" + colDesc[0] + "</th><td>" + "<input type=\"text\" id=\"txt" + tokenvalues[0] + "\" /></td></tr>";
                FormEditStr += tmpstr;
                tmpstr = "<tr><th>" + colDesc[0] + "</th><td>" + "<asp:Label ID=\"lbl" + tokenvalues[0] + "\" ></asp:Label></td></tr>";
                FormViewStr += tmpstr
            }
            tmpstr = "<tr><th>" + colDesc[0] + "</th><td>" + "<asp:Label ID=\"lbl" + tokenvalues[0] + "\" ></asp:Label></td></tr>";
            FormViewStr += tmpstr;
            if (tokenvalues[0] == "LastUpdate") {
                SelectQuery = SelectQuery + tokenvalues[0] + " from " + TableName;
            }
            else
                SelectQuery = SelectQuery + tokenvalues[0] + ",";
        }
    }

    document.getElementById("FormView").innerHTML = FormViewStr + "</table>";
    document.getElementById("FormEdit").innerHTML = FormEditStr + "</table>";

    document.getElementById("FormView").style.display = "block";
    document.getElementById("FormEdit").style.display = "none";
    document.getElementById("ctl00$Body$btnNew").style.display = "block";
    document.getElementById("ctl00$Body$btnEdit").style.display = "block";
    document.getElementById("ctl00$Body$btnInsert").style.display = "none";
    document.getElementById("ctl00$Body$btnUpdate").style.display = "none";
    document.getElementById("ctl00$Body$btnDelete").style.display = "block";
    document.getElementById("ctl00$Body$btnCancel").style.display = "none";

    LoadData();

}


function CreateGroupTable() {

    var combo = document.getElementById("Grpselect");
    var targetcol = combo.value;

    var dataArray = new Array();
    var sdata = new Array();
    document.getElementById("tDiv1").style.display = "block";
    document.getElementById("tDiv").style.display = "none";
    var rowdata;
    var datastr = "";
    var count = 0;
    var index = 0;
    var tmpstr, tmparr;

    var table = document.getElementById("ReportTable");
    var tablebody = table.getElementsByTagName("tbody");
    var tablerows = tablebody[0].getElementsByTagName("tr");

    for (var i = 1; i <= tablerows.length; i++) {
        rowdata = document.getElementById("row" + i).getElementsByTagName("td");
        index = datastr.indexOf(rowdata[targetcol].innerHTML, 0);

        if (index == -1) {
            datastr = datastr + rowdata[targetcol].innerHTML + '|';
            dataArray[count] = rowdata[targetcol].innerHTML + '@' + rowdata[targetcol].outerHTML + '~' + i + '^';
            count++;
        }
        else {
            tmpstr = datastr.substring(0, index);
            tmparr = tmpstr.split("|");
            dataArray[tmparr.length - 1] = dataArray[tmparr.length - 1] + i + '^';
        }
    }

    var headerdata = document.getElementById("HeaderRow");
    var coldata = headerdata.getElementsByTagName("th");
    var sumcoldata,Rowindexdata;
    var headerfulltext, headercuttext;
    var TableBody = "<tbody>";
    var colValue, colinfo;
    for (var k = 0; k < ColumnNo; k++) {
        sdata[k] = 0;
    }
    for (var i = 0; i < count; i++) {
        tmparr = dataArray[i].split('~');
        colValue = tmparr[0].split('@');
        colinfo = tmparr[1].split('^');
        TableBody = TableBody + "<tr><th colspan=\"8\">" + coldata[targetcol].innerHTML + " : " + colValue[0] + " (" + (colinfo.length - 1) + ")</th></tr>";
        for (var j = 0; j < colinfo.length - 1; j++) {
            rowdata = document.getElementById("row" + colinfo[j]);
            headerfulltext = rowdata.innerHTML;
            headercuttext = headerfulltext.replace(colValue[1], "");
            TableBody = TableBody + "<tr id=\"row" + colinfo[j] + "\">" + headercuttext + "</tr>";
            sumcoldata = rowdata.getElementsByTagName("td");
            for (var k = 0; k < ColumnNo; k++) {
                if (sumcoldata[k].align == "right") {
                    sdata[k] = sdata[k] * 1 + sumcoldata[k].innerHTML.replace(",", "") * 1;
                    //String.Format("{0:#,##0}", sdata[k]);   
                }
                else
                    sdata[k] = "";
            }
        }

        TableBody = TableBody + "<tr style=\"border: solid 1px #7f7f7f;background-color:#dfdaf1;border-color:Black\">";
        for (var k = 0; k < ColumnNo; k++) {
            if (k != targetcol) {
                TableBody = TableBody + "<td align=\"right\"><b>" + sdata[k] + "</b></td>";
            }
        }
        TableBody = TableBody + "</tr>";
        for (var k = 0; k < ColumnNo; k++) {
            sdata[k] = 0;
        }
    }
    TableBody = TableBody + "</tbody>";
    var replacestr = coldata[targetcol].outerHTML;
    var originalstr = headerdata.innerHTML;
    var index = originalstr.indexOf(replacestr, 0);

    var dynamictable = "<table id=\"ReportTable\" class=\"yui\" ><thead><tr id=\"HeaderRow\">" + originalstr.replace(replacestr, "");
    dynamictable = dynamictable + "</tr>" + TableBody + "</table>";
    var tDiv = document.getElementById("tDiv1");
    tDiv.innerHTML = dynamictable;
    document.getElementById("Restore").style.display = "block";
}

function RestoreTable() {
    document.getElementById("tDiv1").style.display = "none";
    document.getElementById("tDiv").style.display = "block";
    document.getElementById("Restore").style.display = "none";
}

function CreateGroupCombo() {
    var Combostr;
    var headerdata = document.getElementById("HeaderRow");
    var coldata = headerdata.getElementsByTagName("th");
    Combostr = "Select Column: &nbsp;<select id=\"Grpselect\">";
    for (var k = 0; k < ColumnNo; k++) {
        if (coldata[k].innerText != "Version" && coldata[k].innerText != "LastUpdate" && coldata[k].innerText != "SLNo")
            Combostr = Combostr + "<option value=\"" + k + "\">" + coldata[k].innerText + "</option>";
    }
    Combostr = Combostr + "</select>&nbsp;<input type=\"button\" id=\"Group\" value=\"Group\" onclick=\"CreateGroupTable()\"/>&nbsp;<input type=\"button\"  id=\"Restore\" value=\"<<Back\" onclick=\"RestoreTable()\"/>";
    var tDiv = document.getElementById("DivGroupCombo");
    tDiv.innerHTML = Combostr;
}

function OnCreateTableSuccess(result, userContext, eventArgs) {

    var dynamictable = "<table id=\"tableOne\" class=\"yui\" >" + TableHeader + "<thead> ";
    dynamictable += result;
    dynamictable += "<tfoot><tr id=\"pagerOne\"><td colspan=\"7\"><img src=\"_assets/img/first.png\" class=\"first\"/><img src=\"_assets/img/prev.png\" class=\"prev\"/><input type=\"text\" class=\"pagedisplay\"/><img src=\"_assets/img/next.png\" class=\"next\"/><img src=\"_assets/img/last.png\" class=\"last\"/><select class=\"pagesize\"><option value=\"30\">30</option><option value=\"20\">20</option><option value=\"10\">10</option></select></td></tr></tfoot></table>";
    var tDiv = document.getElementById("tDiv");
    tDiv.innerHTML = dynamictable;
    if (result != "No Record exists!!!") {
        RowSelected(1);
        CreateGroupCombo();
        $(document).ready(function() {
            $("#tableOne").tablesorter({ debug: false, sortList: [[0, 0]], widgets: ['zebra'] })
                        .tablesorterPager({ container: $("#pagerOne"), positionFixed: false })
                        .tablesorterFilter({ filterContainer: $("#filterBox0"),
                            filterClearContainer: $("#filterClear0"),
                            filterColumns: ColumnNo,
                            filterCaseSensitive: false
                        });

            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox1"),
                filterClearContainer: $("#filterClear1"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });

            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox2"),
                filterClearContainer: $("#filterClear2"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox3"),
                filterClearContainer: $("#filterClear3"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox4"),
                filterClearContainer: $("#filterClear4"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox5"),
                filterClearContainer: $("#filterClear5"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox6"),
                filterClearContainer: $("#filterClear6"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox7"),
                filterClearContainer: $("#filterClear7"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox8"),
                filterClearContainer: $("#filterClear8"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });
            $("#tableOne").tablesorterFilter({ filterContainer: $("#filterBox9"),
                filterClearContainer: $("#filterClear9"),
                filterColumns: ColumnNo,
                filterCaseSensitive: false
            });

        });
    }
}
