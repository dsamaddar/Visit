    function formatdate(date) {
        var d;
        var m;
        var y;
        y = date.getYear();
        switch (date.getMonth()) {
            case 0: //january
                m = "Jan";
                break;                
            case 2: //march
                m = "Mar";
                break;
            case 4: //may
                m = "May";
                break;
            case 6: //july
                m = "Jul";
                break;
            case 7: //august
                m = "Aug";
                break;
            case 9: //october
                m = "Oct";
                break;
            case 11: //december
                m = "Dec";
                break;
            case 1: //february
                m = "Feb";
                break;
            case 3: //april
                m = "Apr";
                break;
            case 5: //jun
                m = "Jun";
                break;
            case 8: //september
                m = "Sep";
                break;
            case 10: //november
                m = "Nov";
                break;                
            default:                
                break;
        }
        d = date.getDate();
        return d + "-" + m + "-" + y;
    }
    function isLastDayofMonth(date) {
        var isLeavYear = date.getYear() % 4;
        var month = date.getMonth();
        var day = date.getDate();
        switch (month) {
            case 0: //january
            case 2: //march
            case 4: //may
            case 6: //july
            case 7: //august
            case 9: //october
            case 11: //december
                if (day == 31)
                    return 1;
                else
                    return 0;
                break;
            case 1: //february
                if (isLeavYear == 0 && day == 29)
                    return 1;
                else
                    return 0;
                break;
            case 3: //april
            case 5: //jun
            case 8: //september
            case 10: //november
                if (day == 30)
                    return 1;
                else
                    return 0;
                break;
            default:
                return 0;
                break;

        }

    }
    function getLastDayofMonth(date) {
        var lastday = new Date();
        lastday.setFullYear(date.getYear());
        lastday.setMonth(date.getMonth());
        var isLeavYear = date.getYear() % 4;
        var month = date.getMonth();
        var day = date.getDate();
        switch (month) {
            case 0: //january
            case 2: //march
            case 4: //may
            case 6: //july
            case 7: //august
            case 9: //october
            case 11: //december
                lastday.setDate(31);
                break;
            case 1: //february
                if (isLeavYear == 0)
                    lastday.setDate(29);
                else
                    lastday.setDate(28);
                break;
            case 3: //april
            case 5: //jun
            case 8: //september
            case 10: //november
                lastday.setDate(30);
                break;
            default:
                return 0;
                break;
        }
        return lastday;
    }
    function CompareDate(firstdate, seconddate) {
        return firstdate.getTime() - seconddate.getTime();
    }
    function getMinMaxDate(mode, datelist) {
        var i = 0;
        var length = datelist.length;
        var minindex = -1;
        var minstamp = -1;
        var mindate = new Date();
        if (length > 0) {
            minstamp = datelist[i].getTime();
            minindex = i;
        }
        for (i = 0; i < length; i++) {
            if (datelist[i].getTime() < minstamp) {
                minindex = i;
                minstamp = datelist[i].getTime();
            }
        }
        mindate.setTime(minstamp);
        return minindex;

    }
    function dateadd(mode, startdate, interval) {
        var diff;
        interval = parseInt(interval);
        var timestamp = startdate.getTime();
        diff = new Date();
        if (mode == "day") {
            var one_day = 1000 * 60 * 60 * 24;
            timestamp = timestamp + interval * one_day;
            diff.setTime(timestamp);

        }
        else if (mode == "month") {        
            var currentyear = startdate.getYear() + Math.floor((startdate.getMonth() + interval) / 12);
            var currentmonth = (startdate.getMonth() + interval) % 12;
            diff.setTime(timestamp);
            diff.setMonth(currentmonth);
            diff.setYear(currentyear);
        }
        else if (mode == "year") {
            var currentyear = startdate.getYear() + interval;
            diff.setTime(timestamp);
            diff.setYear(currentyear);
        }
        return diff;
    }
    function datediff(mode, startdate, enddate) {
        var diff;
        if (mode == "day") {
            var one_day = 1000 * 60 * 60 * 24;
            diff = Math.ceil((enddate.getTime() - startdate.getTime()) / (one_day));
        }
        else if (mode == "month") {
            diff = (enddate.getYear() - startdate.getYear()) * 12 + (enddate.getMonth() - startdate.getMonth());
        }
        else if (mode == "year") {
            diff = (enddate.getYear() - startdate.getYear());
        }
        return diff;
    }

    function GetBlankGrid1() {
        var tb = "<table class=\"yui\">"
        tb = tb + "<tr><th>" + "SL#" + "</th><th>" + "Installment No" + "</th><th>" + "Date" + "</th><th>" + "Particulars" + "</th><th>" + "APBBOM" + "</th><th>" + "PBBOM" + "</th><th>" + "Principal" + "</th><th>" + "Interest" + "</th><th>" + "Installment" + "</th><th>" + "APBEOM" + "</th><th>" + "PBEOM" + "</th><th>" + "In Flow" + "</th><th>" + "Out Flow" + "</th><th>" + "Net Flow" + "</th></tr>";
        tb = tb + "<tr><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr></table>";
        return tb;        
    }

    function generateSchedule(grossprincipal, nominalrate, rental, execdate, inpstartdate, InterestFrequency, PrinciapalFrequency, PremiumFrequency, InterestCapitalizationFrequency, Tenor, leasemode, leasetype, DayBasisInterest, monthendbasis, GracePeriod, GracePeriodFrequency, isCapitalizeGraceInterest, isGracePeriodinTenor, GracePeriodInterestRate, InterestResting) {
        //var grossprincipal;
        //grossprincipal = document.getElementById("txtGrossPrincipal").value;
        //var nominalrate;
        //nominalrate = document.getElementById("txtNominalRate").value;

        //var rental;
        //rental = document.getElementById("txtRental").value;

        var rentalno = 1;

        var startdate;

        startdate = new Date(inpstartdate);

        //var PrinciapalFrequency;
        //PrinciapalFrequency = document.getElementById("txtPrinciapalFrequency").value;

        //var InterestFrequency;
        //InterestFrequency = document.getElementById("txtInterestFrequency").value;
        //var PremiumFrequency;
        //PremiumFrequency = document.getElementById("txtPremiumFrequency").value;
        //var InterestCapitalizationFrequency;
        //InterestCapitalizationFrequency = document.getElementById("txtInterestCapitalizationFrequency").value;
        //var Tenor;
        //Tenor = document.getElementById("txtTenor").value;

        //var InterestResting;
        //InterestResting = document.getElementById("txtInterestResting").value;
        //var leasemode = 'EOM';
        //leasemode = document.getElementById("txtleasemode").value;
        //var leasetype = 'Annuity';
        //leasetype = document.getElementById("txtleasetype").value;
        //ctrl.checked = 'checked';

        //var DayBasisInterest = 0;
        
        //if (document.getElementById("txtDayBasisInterest").checked == true)
        //    DayBasisInterest = 1;
        //else
        //    DayBasisInterest = 0;
            
        //var monthendbasis = 0;
        //if (document.getElementById("txtmonthendbasis").checked == true)
        //    monthendbasis = 1;
        //else
        //    monthendbasis = 0;

        //var GracePeriod;
        //GracePeriod = document.getElementById("txtGracePeriod").value;

        //var GracePeriodFrequency;
        //GracePeriodFrequency = document.getElementById("txtGracePeriodFrequency").value;

        //var isCapitalizeGraceInterest;
        //if (document.getElementById("txtisCapitalizeGraceInterest").checked == true)
        //    isCapitalizeGraceInterest = 1;
        //else
        //    isCapitalizeGraceInterest = 0;

        //var isGracePeriodinTenor;
        //if (document.getElementById("txtisGracePeriodinTenor").checked == true)
        //    isGracePeriodinTenor = 1;
        //else
        //    isGracePeriodinTenor = 0;

        //var GracePeriodInterestRate;
        //GracePeriodInterestRate = document.getElementById("txtGracePeriodInterestRate").value;

        var i = 0;
        var enddate;
        var currentdate;
        var YearDay = 360;


        var NoOfPrincipalPayment = -1;
        var serialprincipal = -1;

        var restingno;

        if (PrinciapalFrequency != InterestFrequency) {
            leasetype = "Serial";
        }
        if (leasetype == "Serial") {
            NoOfPrincipalPayment = Math.ceil(Tenor / PrinciapalFrequency);
            serialprincipal = grossprincipal / NoOfPrincipalPayment;
            serialprincipal = Math.round(serialprincipal);
            rental = serialprincipal;
            
        }

        if (isGracePeriodinTenor == 0)
            enddate = dateadd('month', startdate, parseInt(Tenor) +parseInt(GracePeriod));
        else
            enddate = dateadd('month', startdate, Tenor);


        var datelist = new Array();

        var cumulativeinterest = 0;

        var PrinciapalDate = startdate;
        var InterestDate = startdate;
        var PremiumDate = startdate;
        var InterestCapitalizationDate = startdate;
        var InterestAmount = 0;
        var lastinterestdate = startdate;

        var schedule = new Array(14);
        for (i = 0; i < 14; i++)
            schedule[i] = new Array(100);
        i = 0;
        var j = -1;
        //{
        schedule[0][i] = i;
        schedule[1][i] = startdate;

        schedule[3][i] = grossprincipal; //APBBOM
        schedule[4][i] = grossprincipal; //PBBOM
        if (leasemode == 'BOM') {
            schedule[2][i] = "Installment Payment"; //payment type
            schedule[5][i] = rental; //principal
            schedule[6][i] = 0; //interest
            schedule[7][i] = rental; //rental
            schedule[8][i] = grossprincipal - rental;  //APBEOM
            schedule[9][i] = grossprincipal - rental; //PBEOM
            schedule[11][i] = rental; //inflow
            schedule[13][i] = rentalno; //rentalno
            rentalno = rentalno + 1;
        }
        else {
            schedule[2][i] = "Execution"; //payment type
            schedule[5][i] = 0; //principal
            schedule[6][i] = 0; //interest
            schedule[7][i] = 0; //rental
            schedule[8][i] = grossprincipal; //APBEOM
            schedule[9][i] = grossprincipal; //PBEOM
            schedule[13][i] = -1; //rentalno
            schedule[11][i] = 0; //inflow
        }
        //schedule[11][i] = rental; //inflow
        schedule[10][i] = grossprincipal; //outflow
        schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow

        var tmpGraceperiod = GracePeriod;
        var endgraceperiod = dateadd('month', startdate, GracePeriod);
        lastinterestdate = startdate;
        currentdate = startdate;

        while (tmpGraceperiod > 0) {
            i = i + 1;
            currentdate = dateadd('month', currentdate, GracePeriodFrequency);
            if (CompareDate(endgraceperiod, currentdate) < 0)
                currentdate = endgraceperiod;
            if (monthendbasis == 1)
                currentdate = getLastDayofMonth(currentdate);
            schedule[0][i] = i;
            schedule[1][i] = currentdate;
            schedule[2][i] = "Execution"; //payment type
            schedule[3][i] = schedule[8][i - 1]; //APBBOM
            schedule[4][i] = schedule[9][i - 1]; //PBBOM
            
            if (DayBasisInterest == 1)
                InterestAmount = schedule[4][i] * GracePeriodInterestRate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay);
            else
                InterestAmount = schedule[4][i] * GracePeriodInterestRate * datediff('month', lastinterestdate, currentdate) / 1200;
            InterestAmount = Math.round(InterestAmount);    

            if (isCapitalizeGraceInterest == 1) {
                schedule[5][i] = 0; //principal
                schedule[6][i] = InterestAmount; //interest
                schedule[7][i] = 0; //rental
                
                schedule[8][i] = schedule[8][i - 1] + InterestAmount;   //APBEOM
                schedule[9][i] = schedule[9][i - 1] + InterestAmount;   //PBEOM
                grossprincipal = grossprincipal + InterestAmount;
                schedule[13][i] = -1; //rentalno
            }
            else {
                schedule[5][i] = 0; //principal
                schedule[6][i] = InterestAmount; //interest
                schedule[7][i] = InterestAmount; //rental
                schedule[8][i] = schedule[8][i - 1];    //APBEOM
                schedule[9][i] = schedule[9][i - 1];    //PBEOM
                schedule[11][i] = InterestAmount; //inflow
                schedule[10][i] = 0; //outflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                rentalno = rentalno + 1;
            }
            lastinterestdate = currentdate;
            tmpGraceperiod = tmpGraceperiod - GracePeriodFrequency;
        }
        if (leasetype == "Serial") {
            NoOfPrincipalPayment = Math.ceil(Tenor / PrinciapalFrequency);
            serialprincipal = grossprincipal / NoOfPrincipalPayment;
            serialprincipal = Math.round(serialprincipal);
            rental = serialprincipal;
        }

        PrinciapalDate = lastinterestdate;
        InterestDate = lastinterestdate;

        restingno = InterestResting;
        while (CompareDate(PrinciapalDate, enddate) <= 0 || CompareDate(InterestDate, enddate) <= 0) {
            datelist = new Array();
            datelist[0] = dateadd('month', PrinciapalDate, PrinciapalFrequency);
            datelist[1] = dateadd('month', InterestDate, InterestFrequency);
            if (PremiumFrequency != -1)
                datelist[2] = dateadd('month', PremiumDate, PremiumFrequency);
            else
                datelist[2] = dateadd('month', startdate, 300);

            if (InterestCapitalizationFrequency != -1)
                datelist[3] = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
            else
                datelist[3] = dateadd('month', startdate, 300);
            j = getMinMaxDate('min', datelist);
            currentdate = datelist[j];
            if (monthendbasis == 1)
                currentdate = getLastDayofMonth(currentdate);
            if (PrinciapalFrequency == InterestFrequency)
                j = 4;
            switch (j) {
                case 0:
                    i = i + 1;
                    if (CompareDate(schedule[1][i - 1], currentdate) == 0) {
                        i = i - 1;
                        schedule[2][i] = "Installment Payment";
                        InterestAmount = schedule[6][i];
                    }
                    else {
                        InterestAmount = 0;
                        schedule[2][i] = "PrinciaplPayment";
                    }
                    if (DayBasisInterest == 1)
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                    else
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;
                    lastinterestdate = currentdate;
                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM

                    schedule[5][i] = serialprincipal; //principal
                    schedule[6][i] = InterestAmount;  //Interest
                    rental = schedule[5][i] + InterestAmount; //rental
                    PrinciapalDate = dateadd('month', PrinciapalDate, PrinciapalFrequency);
                    schedule[7][i] = rental; //rental
                    schedule[8][i] = schedule[8][i - 1] - rental + InterestAmount;  //APEOM
                    if (restingno <= 0) {
                        schedule[9][i] = schedule[9][i - 1] - rental + InterestAmount;  //PBEOM
                        restingno = InterestResting;
                    }
                    else
                        schedule[9][i] = schedule[9][i - 1];
                    NoOfPrincipalPayment = NoOfPrincipalPayment - 1;
                    schedule[11][i] = schedule[7][i]; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = rentalno; //rentalno
                    rentalno = rentalno + 1;
                    break;
                case 1:
                    restingno = restingno - 1;
                    i = i + 1;
                    if (CompareDate(schedule[1][i - 1], currentdate) == 0) {
                        i = i - 1;
                        schedule[2][i] = "Installment Payment";
                        InterestAmount = schedule[6][i];
                    }
                    else {
                        schedule[2][i] = "Interest Payment";
                        schedule[5][i] = 0; //principal
                        schedule[8][i] = schedule[8][i - 1];  //APEOM
                        schedule[9][i] = schedule[9][i - 1];  //PBEOM
                    }
                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;

                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM
                    if (DayBasisInterest == 1)
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                    else
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);

                    InterestAmount = InterestAmount + cumulativeinterest;
                    rental = schedule[5][i] + InterestAmount;
                    cumulativeinterest = 0;
                    schedule[6][i] = InterestAmount;
                    schedule[7][i] = rental;
                    lastinterestdate = currentdate;
                    InterestDate = dateadd('month', InterestDate, InterestFrequency);
                    schedule[11][i] = schedule[7][i]; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = rentalno; //rentalno
                    rentalno = rentalno + 1;
                    if (rental == 0 && InterestAmount == 0)
                        i = i - 1;
                    InterestAmount = 0;
                    
                    break;
                case 2: //deposit premium
                    var PremiumAmount = 0;
                    i = i + 1;
                    schedule[0][i] = i;
                    schedule[1][i] = datelist[j];
                    schedule[2][i] = "Principal Premium";
                    PremiumDate = dateadd('month', PremiumDate, PremiumFrequency);
                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM
                    cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                    lastinterestdate = currentdate;
                    schedule[5][i] = PremiumAmount; //principal
                    schedule[6][i] = 0;  //interest
                    schedule[7][i] = 0;  //rental
                    schedule[8][i] = schedule[3][i] + PremiumAmount; //APBBOM
                    schedule[9][i] = schedule[4][i] + PremiumAmount;  //PBBOM
                    schedule[11][i] = schedule[5][i]; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = rentalno; //rentalno
                    rentalno = rentalno + 1;
                    break;
                case 3:
                    if (CompareDate(lastinterestdate, currentdate) == 0) {
                        InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                        lastinterestdate = currentdate;
                        break;
                    }
                    i = i + 1;
                    if (DayBasisInterest == 0)
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / (1200));
                    else
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                    lastinterestdate = currentdate;

                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;
                    schedule[2][i] = "Interest Capitalization";
                    InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM
                    schedule[5][i] = 0; //principal
                    schedule[6][i] = cumulativeinterest;  //interest
                    schedule[7][i] = 0;  //rental
                    schedule[8][i] = schedule[3][i] + cumulativeinterest; //APBEOM
                    schedule[9][i] = schedule[4][i] + cumulativeinterest; //PBEOM
                    if (NoOfPrincipalPayment > 0)
                        serialprincipal = Math.round(schedule[8][i] / NoOfPrincipalPayment);
                    cumulativeinterest = 0;
                    schedule[11][i] = 0; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = -1; //rentalno                    
                    break;
                case 4:
                    restingno = restingno - 1;
                    i = i + 1;
                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;
                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * PrinciapalFrequency / 1200);

                    if (leasetype == "Annuity") {
                        schedule[5][i] = rental - InterestAmount; //principal
                    }
                    else {
                        schedule[5][i] = serialprincipal; //principal
                        rental = schedule[5][i] + InterestAmount; //principal
                        //schedule[2][i] = "PrinciaplPayment";
                    }
                    schedule[6][i] = InterestAmount; //interest                        
                    schedule[2][i] = "Installment Payment";
                    InterestDate = dateadd('month', InterestDate, InterestFrequency);
                    PrinciapalDate = dateadd('month', PrinciapalDate, PrinciapalFrequency);
                    schedule[7][i] = rental; //rental
                    schedule[8][i] = schedule[8][i - 1] - rental + InterestAmount;  //APEOM
                    if (restingno == 0) {
                        //schedule[9][i] = schedule[8][i] - rental + InterestAmount;  //PBEOM
                        schedule[9][i] = schedule[8][i];  //PBEOM
                        restingno = InterestResting;
                    }
                    else {
                        schedule[9][i] = schedule[9][i - 1];  //PBEOM
                    }
                    schedule[11][i] = schedule[7][i]; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = rentalno; //rentalno
                    rentalno = rentalno + 1;
                    if (CompareDate(enddate, currentdate) < 0)
                        i = i - 1;
                    if (schedule[3][i] < 500)
                        i = i - 1;
                    break;
                default:
                    break;

            }
        }
        var tb = "<table class=\"yui\" border=\"1\">"
        tb = tb + "<tr><th>" + "SL#" + "</th><th>" + "Installment No" + "</th><th>" + "Date" + "</th><th>" + "Type" + "</th><th>" + "APBBOM" + "</th><th>" + "PBBOM" + "</th><th>" + "Principal" + "</th><th>" + "Interest" + "</th><th>" + "Installment" + "</th><th>" + "APBEOM" + "</th><th>" + "PBEOM" + "</th><th>" + "In Flow" + "</th><th>" + "Out Flow" + "</th><th>" + "Net Flow" + "</th></tr>";
        var dd;
        for (j = 0; j <= i; j++) {
            if (j == 0) {
                dd = new Date(schedule[1][j]);
            }
            tb = tb + "<tr><td id=\"row" + j + "-SLNo\">" + schedule[0][j] + "</td><td id=\"row" + j + "-InstallmentNo\">" + schedule[13][j] + "</td><td id=\"row" + j + "-InstallmentDate\">" + formatdate(schedule[1][j]) + "</td><td id=\"row" + j + "-AmountType\">" + schedule[2][j] + "</td><td id=\"row" + j + "-APBBOM\">" + schedule[3][j] + "</td><td id=\"row" + j + "-PBBOM\">" + schedule[4][j] + "</td><td id=\"row" + j + "-Principal\">" + schedule[5][j] + "</td><td id=\"row" + j + "-Interest\">" + schedule[6][j] + "</td><td id=\"row" + j + "-Installment\">" + schedule[7][j] + "</td><td id=\"row" + j + "-APBEOM\">" + schedule[8][j] + "</td><td id=\"row" + j + "-PBEOM\">" + schedule[9][j] + "</td><td id=\"row" + j + "-InFlow\">" + schedule[11][j] + "</td><td id=\"row" + j + "-OutFlow\">" + schedule[10][j] + "</td><td id=\"row" + j + "-NetFlow\">" + schedule[12][j] + "</td></tr>";
        }
        tb = tb + "</table>";
        return i+"~~"+tb;

    }
