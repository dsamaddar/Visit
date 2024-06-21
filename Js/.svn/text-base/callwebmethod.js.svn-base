function SucceededCallback(result,userContext, eventArgs)
 { //alert(   result);
    // Page element to display feedback.
    //var dept= document.getElementById("ResultId");

    document.getElementById(userContext).value=result; 
    
 }
 
function SucceededCallbackLabel(result,userContext, eventArgs)
 { //alert(   result);
    // Page element to display feedback.
    //var dept= document.getElementById("ResultId");

    document.getElementById(userContext).innerHTML=result; 
    
 }
 
 function FailedCallback(error)
{
    // Display the error.    
   alert( error.get_message());
}

function DateAdd(timeU,byMany,dateObj) {
	var millisecond=1;
	var second=millisecond*1000;
	var minute=second*60;
	var hour=minute*60;
	var day=hour*24;
	var year=day*365;

	var newDate;
	var dVal=dateObj.valueOf();
	switch(timeU) {
		case "ms": newDate=new Date(dVal+millisecond*byMany); break;
		case "s": newDate=new Date(dVal+second*byMany); break;
		case "mi": newDate=new Date(dVal+minute*byMany); break;
		case "h": newDate=new Date(dVal+hour*byMany); break;
		case "d": newDate=new Date(dVal+day*byMany); break;
		case "y": newDate=new Date(dVal+year*byMany); break;
	}
	return newDate;
}

function roundNumber(num, dec) {
	var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	return result;
}

function trim(stringToTrim) {
	return stringToTrim.replace(/^\s+|\s+$/g,"");
}
function ltrim(stringToTrim) {
	return stringToTrim.replace(/^\s+/,"");
}
function rtrim(stringToTrim) {
	return stringToTrim.replace(/\s+$/,"");
}
