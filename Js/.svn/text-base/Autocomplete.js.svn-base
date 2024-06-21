var strReturn = "";
var param="param";
var value="";
var paramfields="";
var strHref = window.location.href;
var exp = new RegExp("[+]", "g");
if (strHref.indexOf("?") > -1) {
    var strQueryString = strHref.substr(strHref.indexOf("?")).toLowerCase();
    var aQueryString = strQueryString.split("&");
    for (var iParam = 0; iParam < aQueryString.length; iParam++) {
        if (aQueryString[iParam].indexOf(param.toLowerCase() + "=") > -1) {
            var aParam = aQueryString[iParam].split("=");
            strReturn = aParam[1].replace(exp, " ");
            break;
        }
    }
}
value= unescape(strReturn);
paramfields=value.split("!");

$(document).ready(function() {
          $("#"+paramfields[0]+"").autocomplete({ 
            source: ["433", "992", "567", "123"], minLength: 0, delay: 5]
          });
});
