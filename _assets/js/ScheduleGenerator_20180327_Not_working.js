﻿var TotalScheduleRow;

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
    var lastday;
    var year = date.getYear();
    var month = date.getMonth() + 1;
    var day = 0;

    //lastday.setFullYear(date.getYear());
    //lastday.setMonth(date.getMonth());
    var isLeavYear = date.getYear() % 4;
    var mn = date.getMonth();
    //var day = date.getDate();
    switch (mn) {
        case 0: //january
        case 2: //march
        case 4: //may
        case 6: //july
        case 7: //august
        case 9: //october
        case 11: //december
            //lastday.setDate(31);
            day = 31;
            break;
        case 1: //february
            if (isLeavYear == 0)
                day = 29;
            //lastday.setDate(29);
            else
                day = 28;
            //lastday.setDate(28);
            break;
        case 3: //april
        case 5: //jun
        case 8: //september
        case 10: //november
            //lastday.setDate(30);
            day = 30;
            break;
        default:
            return 0;
            break;
    }
    //alert(month + "-" + day + "-" + year);
    lastday = new Date(month + "-" + day + "-" + year);
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

function getAppropriateDate(month, day, year) {

    var isLeavYear = year % 4;


    switch (month) {
        case 0: //january
        case 2: //march
        case 4: //may
        case 6: //july
        case 7: //august
        case 9: //october
        case 11: //december
            return day;
            break;
        case 1: //february
            if (isLeavYear == 0) {
                if (day >= 29)
                    return 29;
                else
                    return day;
            }
            else {
                if (day >= 29)
                    return 28;
                else
                    return day;
            }
            break;
        case 3: //april
        case 5: //jun
        case 8: //september
        case 10: //november
            if (day > 30)
                return 30;
            else
                return day;
            break;
        default:
            return day;
            break;

    }


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
        var currentyear = startdate.getFullYear() + Math.floor((startdate.getMonth() + interval) / 12);

        var currentmonth = (startdate.getMonth() + interval) % 12;
        var currentdate = startdate.getDate();

        currentdate = getAppropriateDate(currentmonth, currentdate, currentyear);

        currentmonth = currentmonth + 1;

        diff = new Date(currentmonth + "-" + currentdate + "-" + currentyear);
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

function generateSchedule(grossprincipal, nominalrate, rental, execdate, inpstartdate, prinstartdate, capstartdate, InterestFrequency, PrinciapalFrequency, PremiumFrequency, InterestCapitalizationFrequency, Tenor, leasemode, leasetype, DayBasisInterest, monthendbasis, monthendbasiscapitalization, GracePeriod, GracePeriodFrequency, CapitalizeGraceInterestFrequency, isGracePeriodinTenor, GracePeriodInterestRate, InterestResting, residualvalue, ConsiderThirtyDaysMonthUnit, GracePeriodStartDate, FixedPartialpayment, FixedPrincipal, AddGRPIntWithFirstSchedule) {
    var rentalno = 1;
    var IntSeq = 1;
    var PrinSeq = 1;
    var CapSeq = 1;

    var startdate = new Date(inpstartdate);
    var startdateprin = new Date(prinstartdate);
    var startdatecap = new Date(capstartdate);
    var executiondate = new Date(execdate);

    var GPStartDate = new Date(GracePeriodStartDate);
    //if (DayBasisInterest != 1)
    //    startdate = executiondate;

    var i = 0;
    var enddate;
    var currentdate;
    var YearDay = 360;

    var NoOfPrincipalPayment = -1;
    var serialprincipal = -1;
    var GRPInt = 0;
    var restingno;

    if (leasetype == "Serial") {
        if (FixedPrincipal == "" || FixedPrincipal * 1 == 0) {
            if (isGracePeriodinTenor == 0)
                NoOfPrincipalPayment = Math.ceil(Tenor / PrinciapalFrequency);
            else
                NoOfPrincipalPayment = Math.ceil((Tenor * 1 - GracePeriod * 1) / PrinciapalFrequency);

            serialprincipal = grossprincipal / NoOfPrincipalPayment;
            serialprincipal = Math.round(serialprincipal);
            rental = serialprincipal;
        }
        else {
            serialprincipal = FixedPrincipal;
            rental = serialprincipal;
        }
    }

    if (isGracePeriodinTenor == 0)
        enddate = dateadd('month', executiondate, parseInt(Tenor) + parseInt(GracePeriod));
    else
        enddate = dateadd('month', executiondate, parseInt(Tenor));

    if (leasemode == 'BOM') {
        enddate = dateadd('day', enddate, -1);
    }
    else {
        enddate = dateadd('day', enddate, 2);
    }

    if (monthendbasis == 1 && CompareDate(executiondate, getLastDayofMonth(executiondate)) == 0)
        enddate = getLastDayofMonth(enddate);

    var datelist = new Array();

    var cumulativeinterest = 0;

    var PrinciapalDate = startdateprin;
    var InterestDate = startdate;
    var InterestCapitalizationDate = startdatecap;

    var PremiumDate = startdate;
    var InterestAmount = 0;
    var lastinterestdate = startdate;
    var lastGracePeriodinterestdate = lastinterestdate;

    var schedule = new Array(17);
    for (i = 0; i < 17; i++)
        schedule[i] = new Array(200);

    i = 0;
    var j = -1;
    
    schedule[0][i] = i;
    var index = 0;


    schedule[3][i] = grossprincipal; //APBBOM
    schedule[4][i] = grossprincipal; //PBBOM
    if (GracePeriod > 0) {
        leasemode = "EOM";
    }

    if (leasemode == 'BOM') {
        if (GracePeriod > 0) {
            i = -1;
        }
        else {
            schedule[0][i] = i;
            schedule[1][i] = executiondate;
            schedule[3][i] = 1*grossprincipal; //APBBOM
            schedule[4][i] = 1*grossprincipal; //PBBOM
            schedule[2][i] = "Execution"; //payment type
            schedule[5][i] = 0; //principal
            schedule[6][i] = 0; //interest
            schedule[7][i] = 0; //rental
            schedule[8][i] = 1*grossprincipal; //APBEOM
            schedule[9][i] = 1*grossprincipal; //PBEOM
            schedule[13][i] = 0; //rentalno
            schedule[11][i] = 0; //inflow
            schedule[10][i] = 0; //outflow
            schedule[12][i] = 0; //netflow
            schedule[14][i] = 0; //iRate
            schedule[15][i] = 0; //Period Int

            i = i + 1;

            schedule[0][i] = i;
            schedule[2][i] = "Principal Payment"; //payment type
            schedule[3][i] = 1*grossprincipal; //APBBOM
            schedule[4][i] = 1*grossprincipal; //PBBOM
            schedule[1][i] = executiondate;
            schedule[5][i] = rental; //principal
            schedule[6][i] = 0; //interest
            schedule[7][i] = rental; //rental
            schedule[8][i] = grossprincipal - rental;  //APBEOM
            schedule[9][i] = grossprincipal - rental; //PBEOM
            schedule[11][i] = rental; //inflow
            schedule[13][i] = rentalno; //rentalno
            rentalno = rentalno + 1;
            lastinterestdate = startdate;
            lastGracePeriodinterestdate = lastinterestdate;
            startdate = dateadd('month', startdate, InterestFrequency);
            startdateprin = dateadd('month', startdateprin, PrinciapalFrequency);
            startdatecap = dateadd('month', startdatecap, InterestCapitalizationFrequency);

            IntSeq = IntSeq + 1;
            PrinSeq = PrinSeq + 1;
            CapSeq = CapSeq + 1;

            InterestDate = startdate;
            PrinciapalDate = startdateprin;
            InterestCapitalizationDate = startdatecap;

            datelist[0] = PrinciapalDate;
            datelist[1] = InterestDate;
            datelist[2] = InterestCapitalizationDate;
        }
    }
    else {
        schedule[2][i] = "Execution"; //payment type
        schedule[1][i] = executiondate;
        schedule[5][i] = 0; //principal
        schedule[6][i] = 0; //interest
        schedule[7][i] = 0; //rental
        schedule[8][i] = grossprincipal; //APBEOM
        schedule[9][i] = grossprincipal; //PBEOM
        schedule[13][i] = 0; //rentalno
        schedule[11][i] = 0; //inflow
        if (DayBasisInterest == 1) {
            lastinterestdate = executiondate;
            lastGracePeriodinterestdate = lastinterestdate;
        }
        else {
            lastinterestdate = executiondate;
            lastGracePeriodinterestdate = lastinterestdate;
        }
        //InterestDate = dateadd('month', executiondate, InterestFrequency * IntSeq);
        //PrinciapalDate = dateadd('month', executiondate, PrinciapalFrequency * PrinSeq);

        InterestDate = dateadd('month', startdate, InterestFrequency * (IntSeq - 1));
        PrinciapalDate = dateadd('month', startdateprin, PrinciapalFrequency * (PrinSeq - 1));
        InterestCapitalizationDate = dateadd('month', startdatecap, InterestCapitalizationFrequency * (CapSeq - 1));

        IntSeq = IntSeq + 1;
        PrinSeq = PrinSeq + 1;
        CapSeq = CapSeq + 1;

        datelist[0] = PrinciapalDate;
        datelist[1] = InterestDate;
        datelist[2] = InterestCapitalizationDate;

    }
    //schedule[11][i] = rental; //inflow
    schedule[10][i] = grossprincipal; //outflow
    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
    schedule[14][i] = 0; //cumulative interest
    schedule[15][i] = nominalrate; //period interest rate
    schedule[16][i] = 'Due'; //Payment Status
    var tmpGraceperiod = GracePeriod;
    var endgraceperiod = dateadd('month', startdate, GracePeriod);

    var GracePeriodMonthIndex = 0;

    if (tmpGraceperiod > 0) {
        currentdate = GPStartDate;
    }
    var gi = 1;

    while (tmpGraceperiod > 0) {

        if (index == 1) {
            //currentdate = dateadd('month', currentdate, 1);
            currentdate = dateadd('month', startdate, gi);
            gi = gi * 1 + 1;
        }
        else {
            index = 1;
        }

        if (DayBasisInterest == 1)
            if (ConsiderThirtyDaysMonthUnit == 1)
                InterestAmount = InterestAmount + grossprincipal * GracePeriodInterestRate * datediff('month', lastinterestdate, currentdate) * 30 / (100 * YearDay);
            else
                InterestAmount = InterestAmount + grossprincipal * GracePeriodInterestRate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay);
        else
            InterestAmount = InterestAmount + grossprincipal * GracePeriodInterestRate * datediff('month', lastinterestdate, currentdate) / 1200;

        InterestAmount = Math.round(InterestAmount);

        if (((GracePeriodMonthIndex % CapitalizeGraceInterestFrequency) == 0 || GracePeriodMonthIndex == 0) && CapitalizeGraceInterestFrequency > 0) {
            i = i + 1;
            if (monthendbasis == 1)
                currentdate = getLastDayofMonth(currentdate);
            schedule[0][i] = i;
            schedule[1][i] = currentdate;
            schedule[2][i] = "Capitalize Interest"; //payment type
            schedule[3][i] = grossprincipal; //APBBOM
            schedule[4][i] = grossprincipal; //PBBOM

            schedule[5][i] = 0; //principal
            schedule[6][i] = InterestAmount; //interest
            schedule[7][i] = 0; //rental
            if (FixedPartialpayment > 0)
                InterestAmount = InterestAmount - FixedPartialpayment

            schedule[8][i] = schedule[8][i - 1] + InterestAmount;   //APBEOM
            schedule[9][i] = schedule[9][i - 1] + InterestAmount;   //PBEOM

            grossprincipal = grossprincipal + InterestAmount;

            schedule[11][i] = 0; //rentalno
            schedule[10][i] = 0; //outflow
            schedule[12][i] = 0; //netflow
            schedule[13][i] = 0; //rentalno
            schedule[14][i] = InterestAmount; //cumulative interest
            schedule[15][i] = GracePeriodInterestRate; //period interest rate
            schedule[16][i] = 'Due'; //Payment Status
            InterestAmount = 0;
            lastinterestdate = currentdate;
            lastGracePeriodinterestdate = currentdate;
            if (FixedPartialpayment > 0) {
                i = i + 1;
                schedule[0][i] = i;
                schedule[1][i] = currentdate;
                schedule[2][i] = "Interest Payment"; //payment type
                schedule[3][i] = grossprincipal; //APBBOM
                schedule[4][i] = grossprincipal; //PBBOM
                schedule[5][i] = 0; //principal
                schedule[6][i] = FixedPartialpayment; //interest
                schedule[7][i] = FixedPartialpayment; //rental
                schedule[8][i] = grossprincipal;
                schedule[9][i] = grossprincipal;
                schedule[11][i] = InterestAmount; //inflow                
                schedule[10][i] = 0; //outflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                rentalno = rentalno + 1;
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = GracePeriodInterestRate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
            }
        }
        else if (((GracePeriodMonthIndex % GracePeriodFrequency) == 0 || GracePeriodMonthIndex == 0) && GracePeriodFrequency > 0) {
            if (AddGRPIntWithFirstSchedule == 0) {
                i = i + 1;
                if (monthendbasis == 1)
                    currentdate = getLastDayofMonth(currentdate);
                schedule[0][i] = i;
                schedule[1][i] = currentdate;
                schedule[2][i] = "Interest Payment"; //payment type
                schedule[3][i] = grossprincipal; //APBBOM
                schedule[4][i] = grossprincipal; //PBBOM
                schedule[5][i] = 0; //principal
                schedule[6][i] = InterestAmount; //interest
                schedule[7][i] = InterestAmount; //rental
                schedule[8][i] = grossprincipal;
                schedule[9][i] = grossprincipal;
                schedule[11][i] = InterestAmount; //inflow                
                schedule[10][i] = 0; //outflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                rentalno = rentalno + 1;
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = GracePeriodInterestRate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                InterestAmount = 0;
                lastinterestdate = currentdate;
                lastGracePeriodinterestdate = currentdate;
            }
            else {
                if (monthendbasis == 1)
                    currentdate = getLastDayofMonth(currentdate);
                lastinterestdate = currentdate;
                lastGracePeriodinterestdate = currentdate;
                GRPInt = GRPInt + InterestAmount;
            }
        }
        lastinterestdate = currentdate;
        tmpGraceperiod = tmpGraceperiod - 1;
        GracePeriodMonthIndex = GracePeriodMonthIndex + 1;
    }

    if (leasetype == "Serial") {
        if (FixedPrincipal == "" || FixedPrincipal * 1 == 0) {
            if (isGracePeriodinTenor == 0)
                NoOfPrincipalPayment = Math.ceil(Tenor / PrinciapalFrequency);
            else
                NoOfPrincipalPayment = Math.ceil((Tenor * 1 - GracePeriod * 1) / PrinciapalFrequency);

            //NoOfPrincipalPayment = Math.ceil(Tenor / PrinciapalFrequency);
            serialprincipal = grossprincipal / NoOfPrincipalPayment;
            serialprincipal = Math.round(serialprincipal);
            rental = serialprincipal;
        }
        else {
            serialprincipal = FixedPrincipal;
            rental = serialprincipal;
        }
    }
    //PrinciapalDate = lastinterestdate;
    //InterestDate = lastinterestdate;
    var LastCapitalization = 0;
    restingno = InterestResting;
    var testnum = 0;
    datelist = new Array();
    index = 0;
    var IsEnd = 0;
    var LastPrinBalance = 1;
    var si = 0;
    //lastinterestdate = startdate;
    currentdate = startdate;
    lastinterestdate = lastGracePeriodinterestdate;

    while (CompareDate(currentdate, enddate) <= 0 && IsEnd == 0) {
        if (index == 0) {
            datelist[0] = PrinciapalDate;
            if (InterestFrequency * 1 > 0)
                datelist[1] = InterestDate;
            else
                datelist[1] = dateadd('month', startdate, 3000);

            if (InterestCapitalizationFrequency * 1 > 0)
                datelist[2] = InterestCapitalizationDate;
            else
                datelist[2] = dateadd('month', startdate, 3000);
            index = 1;
        }

        j = getMinMaxDate('min', datelist);
        currentdate = datelist[j];

        if (CompareDate(currentdate, enddate) <= 0 && IsEnd == 0) {
            if ((prinstartdate == inpstartdate) && (InterestFrequency == PrinciapalFrequency) && (InterestCapitalizationFrequency == 0))
                j = 4;

            //if (monthendbasis == 1 && ((j == 0 && CompareDate(getLastDayofMonth(startdateprin), startdateprin) == 0) || j != 0))
            //    currentdate = getLastDayofMonth(currentdate);

            switch (j) {
                case 0:
                    i = i + 1;
                    if (CompareDate(schedule[1][i - 1], currentdate) == 0) {
                        i = i - 1;
                        rentalno = rentalno - 1;
                        schedule[2][i] = "Installment Payment";
                        InterestAmount = schedule[6][i];
                    }
                    else {
                        InterestAmount = 0;
                        schedule[2][i] = "Principal Payment";
                    }

                    //InterestDate = dateadd('month', lastinterestdate, InterestFrequency);
                    //Check Again

                    if (DayBasisInterest == 1) {
                        //if (schedule[2][i] == "Installment Payment")
                        //    cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) - 1) / (100 * YearDay));
                        //else {
                        //Check Other Scenerio
                        //if (InterestCapitalizationFrequency > 0) {
                        if (CompareDate(executiondate, lastinterestdate) == 0 || LastPrinBalance == 1)
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                        else
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) - 1) / (100 * YearDay));
                        //}
                        //else
                        //    cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) - 1) / (100 * YearDay));
                        //}
                    }
                    else
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);

                    //alert("Principal " + schedule[9][i - 1] + " : " + LastPrinBalance + " : " + LastCapitalization + " : " + lastinterestdate + " : " + currentdate);
                    //alert(Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay)) + " : " + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) + 1) / (100 * YearDay)) + " : " + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) - 1) / (100 * YearDay)));

                    schedule[14][i] = cumulativeinterest; //cumulative interest
                    schedule[15][i] = nominalrate; //period interest rate 

                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;
                    lastinterestdate = currentdate;
                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM

                    schedule[5][i] = serialprincipal; //principal
                    schedule[6][i] = InterestAmount;  //Interest

                    if (leasetype == "Annuity") {
                        schedule[5][i] = rental * 1 - cumulativeinterest * 1; //principal
                    }
                    else {
                        schedule[5][i] = serialprincipal; //principal
                        rental = schedule[5][i] * 1 + InterestAmount * 1; //principal
                    }
                    //rental = schedule[5][i] + InterestAmount; //rental
                    schedule[7][i] = rental; //rental
                    schedule[8][i] = schedule[8][i - 1] * 1 - rental * 1 + InterestAmount * 1;  //APEOM
                    schedule[9][i] = schedule[8][i];  //PBEOM

                    if (schedule[9][i] * 1 < residualvalue * 1) {
                        IsEnd = 1;
                        schedule[8][i] = residualvalue;
                        schedule[9][i] = residualvalue;

                        schedule[5][i] = schedule[8][i - 1] * 1 - residualvalue * 1;
                        schedule[6][i] = cumulativeinterest;
                        cumulativeinterest = 0;
                        schedule[7][i] = schedule[6][i] * 1 + schedule[5][i] * 1;
                        if (schedule[6][i] * 1 > 0 && schedule[5][i] * 1 > 0)
                            schedule[2][i] = "Installment Payment";
                    }
                    else if ((schedule[9][i] * 1 - residualvalue * 1) <= 100) {
                        IsEnd = 1;
                        schedule[8][i] = residualvalue;
                        schedule[9][i] = residualvalue;

                        schedule[5][i] = schedule[8][i - 1] * 1 - residualvalue * 1;
                        schedule[6][i] = cumulativeinterest;
                        cumulativeinterest = 0;
                        schedule[7][i] = schedule[6][i] * 1 + schedule[5][i] * 1;
                        if (schedule[6][i] * 1 > 0 && schedule[5][i] * 1 > 0)
                            schedule[2][i] = "Installment Payment";
                    }

                    NoOfPrincipalPayment = NoOfPrincipalPayment - 1;
                    schedule[11][i] = schedule[7][i]; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] * 1 - schedule[10][i] * 1; //netflow
                    schedule[13][i] = schedule[0][i]; //rentalno
                    schedule[16][i] = 'Due'; //Payment Status
                    rentalno = rentalno + 1;
                    if (CompareDate(enddate, currentdate) <= 0) {
                        //schedule[6][i] = cumulativeinterest;
                        IsEnd = 1;
                    }
                    PrinciapalDate = dateadd('month', startdateprin, PrinciapalFrequency * (PrinSeq - 1));

                    PrinSeq = PrinSeq + 1;
                    if (monthendbasis == 1 && CompareDate(getLastDayofMonth(startdateprin), startdateprin) == 0)
                        PrinciapalDate = getLastDayofMonth(PrinciapalDate);
                    datelist[0] = PrinciapalDate;
                    LastPrinBalance = 1;
                    LastCapitalization = 0;
                    break;
                case 1:
                    restingno = restingno - 1;
                    i = i + 1;
                    si = i;
                    if (CompareDate(schedule[1][i - 1], currentdate) == 0) {
                        i = i - 1;
                        schedule[2][i] = "Installment Payment";
                        InterestAmount = schedule[14][i];
                        schedule[8][i] = schedule[8][i - 1] * 1 - schedule[5][i] * 1;
                        schedule[9][i] = schedule[8][i];  //PBEOM
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
                    if (DayBasisInterest == 1) {
                        if (schedule[2][i] == "Installment Payment")
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) - 1) / (100 * YearDay));
                        else if (CompareDate(executiondate, lastinterestdate) == 0)
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) + 1) / (100 * YearDay));
                        else if (LastPrinBalance == 1)
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) + 1) / (100 * YearDay));
                        else
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate)) / (100 * YearDay));
                    }
                    else
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);

                    schedule[14][i] = cumulativeinterest; //cumulative interest
                    schedule[15][i] = nominalrate; //period interest rate 

                    if (CompareDate(schedule[1][si - 1], currentdate) != 0) {
                        InterestAmount = InterestAmount * 1 + cumulativeinterest * 1;
                    }

                    if (AddGRPIntWithFirstSchedule == 1) {
                        InterestAmount = InterestAmount * 1 + GRPInt * 1;
                        AddGRPIntWithFirstSchedule = 0;
                        GRPInt = 0;
                    }

                    if (leasetype == "Annuity") {
                        schedule[5][i] = rental * 1 - InterestAmount * 1; //principal
                    }
                    else {
                        rental = schedule[5][i] * 1 + InterestAmount * 1;
                    }

                    cumulativeinterest = 0;
                    schedule[6][i] = InterestAmount;
                    schedule[7][i] = rental;
                    lastinterestdate = currentdate;
                    //InterestDate = dateadd('month', InterestDate, InterestFrequency);
                    //if (CompareDate(InterestDate, enddate) > 0 && CompareDate(currentdate, enddate) < 0)
                    //    InterestDate = enddate;
                    schedule[11][i] = schedule[7][i]; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] * 1 - schedule[10][i] * 1; //netflow
                    schedule[13][i] = schedule[0][i]; //rentalno
                    schedule[16][i] = 'Due'; //Payment Status
                    rentalno = rentalno + 1;
                    //if (rental == 0 && InterestAmount == 0)
                    //    i = i - 1;
                    InterestAmount = 0;
                    //InterestDate = dateadd('month', executiondate, InterestFrequency * IntSeq);
                    InterestDate = dateadd('month', startdate, InterestFrequency * (IntSeq - 1));

                    IntSeq = IntSeq + 1;

                    if (monthendbasis == 1)
                        InterestDate = getLastDayofMonth(InterestDate);

                    datelist[1] = InterestDate;
                    LastCapitalization = 0;
                    if (CompareDate(schedule[1][si - 1], currentdate) != 0)
                        LastPrinBalance = 0;
                    break;
                case 2:
                    i = i + 1;
                    if (DayBasisInterest == 0)
                        cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / (1200));
                    else {
                        if (LastPrinBalance == 1)
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate) + 1) / (100 * YearDay));
                        else
                            cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                    }

                    //alert("Capitalize " + schedule[9][i - 1]+" :" + LastPrinBalance + " : " + LastCapitalization + " : " + lastinterestdate + " : " + currentdate);
                    //alert(Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay))+" : "+Math.round(schedule[9][i - 1] * nominalrate * (datediff('day', lastinterestdate, currentdate)+1) / (100 * YearDay)));

                    lastinterestdate = currentdate;
                    schedule[14][i] = cumulativeinterest; //cumulative interest
                    schedule[15][i] = nominalrate; //period interest rate 

                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;
                    schedule[2][i] = "Capitalize Interest";

                    //InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                    InterestCapitalizationDate = dateadd('month', startdatecap, InterestCapitalizationFrequency * (CapSeq - 1));
                    CapSeq = CapSeq + 1;

                    if (monthendbasiscapitalization == 1)
                        InterestCapitalizationDate = getLastDayofMonth(InterestCapitalizationDate);

                    datelist[2] = InterestCapitalizationDate;

                    //                    if (InterestFrequency > 0) {
                    //                        if (CompareDate(InterestDate, currentdate) == 0) {
                    //                            //InterestDate = dateadd('month', InterestDate, InterestFrequency);
                    //                            InterestDate = dateadd('month', startdate, InterestFrequency * (IntSeq - 1));
                    //                            IntSeq = IntSeq + 1;
                    //                            datelist[1] = InterestDate;
                    //                        }
                    //                    }

                    schedule[3][i] = schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = schedule[9][i - 1];  //PBBOM
                    schedule[5][i] = 0; //principal
                    schedule[6][i] = cumulativeinterest;  //interest
                    schedule[7][i] = 0;  //rental
                    schedule[8][i] = schedule[3][i] * 1 + cumulativeinterest * 1; //APBEOM
                    schedule[9][i] = schedule[4][i] * 1 + cumulativeinterest * 1; //PBEOM

                    if (NoOfPrincipalPayment > 0)
                        serialprincipal = Math.round(schedule[8][i] / NoOfPrincipalPayment);

                    cumulativeinterest = 0;
                    schedule[11][i] = 0; //inflow
                    schedule[10][i] = 0; //outflow
                    schedule[12][i] = schedule[11][i] * 1 - schedule[10][i] * 1; //netflow
                    schedule[13][i] = schedule[0][i]; //rentalno
                    rentalno = rentalno + 1;
                    schedule[16][i] = 'Due'; //Payment Status
                    LastPrinBalance = 0;
                    LastCapitalization = 1;
                    break;
                case 4:
                    restingno = restingno - 1;
                    i = i + 1;
                    schedule[0][i] = i;
                    schedule[1][i] = currentdate;
                    schedule[3][i] = 1*schedule[8][i - 1]; //APBBOM
                    schedule[4][i] = 1*schedule[9][i - 1];  //PBBOM
                    schedule[9][i - 1] = schedule[9][i - 1] * 1;    //ahsan

                    if (DayBasisInterest == 0)
                        InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * PrinciapalFrequency / 1200);
                    else {
                        testnum = schedule[9][i - 1];
                        InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                    }
                    lastinterestdate = currentdate;

                    if (AddGRPIntWithFirstSchedule == 1) {
                        InterestAmount = InterestAmount * 1 + GRPInt * 1;
                        AddGRPIntWithFirstSchedule = 0;
                        GRPInt = 0;
                    }

                    if (leasetype == "Annuity") {
                        schedule[5][i] = rental * 1 - InterestAmount * 1; //principal
                    }
                    else {
                        schedule[5][i] = serialprincipal; //principal
                        rental = schedule[5][i] * 1 + InterestAmount * 1; //principal                        
                    }

                    schedule[16][i] = 'Due'; //Payment Status           
                    schedule[6][i] = 1*InterestAmount; //interest
                    schedule[14][i] = 1*InterestAmount; //cumulative interest
                    schedule[15][i] = nominalrate; //period interest rate  
                    schedule[2][i] = "Installment Payment";
                    schedule[7][i] = Math.round(rental); //rental
                    schedule[8][i] = schedule[8][i - 1] * 1 - rental * 1 + InterestAmount * 1;  //APEOM

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
                    schedule[13][i] = schedule[0][i]; //rentalno
                    rentalno = rentalno + 1;

                    if (CompareDate(enddate, currentdate) < 0)
                        i = i - 1;
                    /*
                    if (schedule[3][i] < 500)
                    i = i - 1;
                    */
                    //InterestDate = dateadd('month', executiondate, InterestFrequency * IntSeq);
                    //PrinciapalDate = dateadd('month', executiondate, PrinciapalFrequency * PrinSeq);

                    InterestDate = dateadd('month', startdate, InterestFrequency * (IntSeq - 1));
                    PrinciapalDate = dateadd('month', startdateprin, PrinciapalFrequency * (PrinSeq - 1));

                    IntSeq = IntSeq + 1;
                    PrinSeq = PrinSeq + 1;

                    datelist[0] = PrinciapalDate;
                    datelist[1] = InterestDate;
                    break;
                default:
                    break;
            }
        }
    }
    TotalScheduleRow = i;
    return schedule;
}


function generatebankguaranteeschedule(agstartdate, agenddate, security, commisionamount, frequency, vatrate, ismonthend, bankid, agreementid, scheduletype) {
    /*
    var startdate;
    var enddate;
    var security;
    var commisionamount;
    var frequency;
    var vatrate;
    var ismonthend;
    var bankid;
    var agreementid;
    var scheduletype;
        
    security = 500;
    vatrate = 15;
    commisionamount = 15000;
    ismonthend = 0;
    bankid = 'brac bank';
    scheduletype = 'bank';
    agreementid = 'bg-1232321';
    frequency = 3;
    */
    var i = 0;
    var period = 0;
    var totalrental = 0;
    var rentalamount = 0;
    var rentaldate;
    var xyz;


    //xyz = new Date(agstartdate);
    //alert(xyz);
    //return 0;
    var startdate = new Date('1/1/2010');
    var enddate = new Date('1/2/2011');
    period = datediff('month', startdate, enddate);
    totalrental = Math.ceil(period / frequency);
    rentalamount = Math.round(commisionamount / totalrental);

    var schedule = new Array(11);
    for (i = 0; i < 11; i++) {
        schedule[i] = new Array(100);
    }
    i = 0;
    rentaldate = startdate;
    schedule[0][i] = i; //sl no
    schedule[1][i] = i + 1; //rental no
    schedule[2][i] = rentaldate; //rental date
    schedule[3][i] = agreementid;
    schedule[4][i] = bankid;
    schedule[5][i] = scheduletype; //scheule type bank or nbfi
    schedule[6][i] = security; //security amount
    schedule[7][i] = rentalamount; //rental amount
    schedule[8][i] = Math.round((rentalamount * vatrate) / 100); //vat amount
    schedule[9][i] = parseInt(schedule[6][i]) + parseInt(schedule[7][i]) + parseInt(schedule[8][i]); //total Rental
    schedule[10][i] = 'Due'; //Status
    totalrental = totalrental - 1;
    while (totalrental > 0) {
        i = i + 1;
        rentaldate = dateadd('month', rentaldate, frequency);
        if (ismonthend == 1)
            rentaldate = getLastDayofMonth(rentaldate);

        schedule[0][i] = i; //sl no
        schedule[1][i] = i + 1; //rental no
        schedule[2][i] = rentaldate; //rental date
        schedule[3][i] = agreementid;
        schedule[4][i] = bankid;
        schedule[5][i] = scheduletype; //scheule type bank or nbfi
        schedule[6][i] = 0; //security amount
        schedule[7][i] = rentalamount; //rental amount
        schedule[8][i] = Math.round((rentalamount * vatrate) / 100); //vat amount
        schedule[9][i] = parseInt(schedule[6][i]) + parseInt(schedule[7][i]) + parseInt(schedule[8][i]); //total Rental

        totalrental = totalrental - 1;
    }
    TotalScheduleRow = i;
    return schedule;

}
function generatebankguaranteescheduleNew(agstartdate, agenddate, security, commisionamount, frequency, vatrate, ismonthend, agreementid, bankid, scheduletype, Mode) {
    var i = 0;
    var period = 0;
    var totalrental = 0;
    var rentalamount = 0;
    var rentaldate;
    var xyz;

    var startdate = new Date(agstartdate);
    var enddate = new Date(agenddate);
    period = datediff('month', startdate, enddate);

    if (Mode == "EOM")
        rentaldate = dateadd('month', startdate, frequency);
    else
        rentaldate = startdate;

    totalrental = 0;
    while (CompareDate(rentaldate, enddate) < 0) {
        totalrental = totalrental + 1;
        rentaldate = dateadd('month', rentaldate, frequency);
    }
    totalrental = totalrental + 1;
    //alert(totalrental);

    //totalrental = Math.ceil(period / frequency);
    var adjustedrental = 0;
    rentalamount = Math.round(commisionamount / totalrental);


    var schedule = new Array(11);
    for (i = 0; i < 11; i++) {
        schedule[i] = new Array(100);
    }
    i = 0;
    if (Mode == "EOM")
        rentaldate = dateadd('month', startdate, frequency);
    else
        rentaldate = startdate;
    if (ismonthend == 1)
        rentaldate = getLastDayofMonth(rentaldate);

    schedule[0][i] = i; //sl no
    schedule[1][i] = i + 1; //rental no
    schedule[2][i] = rentaldate; //rental date
    schedule[3][i] = agreementid;
    schedule[4][i] = bankid;
    schedule[5][i] = scheduletype; //scheule type bank or nbfi
    schedule[6][i] = security; //security amount
    schedule[7][i] = rentalamount; //rental amount
    adjustedrental = parseInt(rentalamount);
    schedule[8][i] = Math.round((rentalamount * vatrate) / 100); //vat amount
    schedule[9][i] = parseInt(schedule[6][i]) + parseInt(schedule[7][i]) + parseInt(schedule[8][i]); //total Rental
    schedule[10][i] = 'DUE'; //Status
    totalrental = totalrental - 1;
    while (totalrental > 0) {
        i = i + 1;
        rentaldate = dateadd('month', rentaldate, frequency);
        //alert(CompareDate(enddate, rentaldate));
        if (CompareDate(enddate, rentaldate) < 0)
            rentaldate = enddate;
        if (ismonthend == 1)
            rentaldate = getLastDayofMonth(rentaldate);


        schedule[0][i] = i; //sl no
        schedule[1][i] = i + 1; //rental no
        schedule[2][i] = rentaldate; //rental date
        schedule[3][i] = agreementid;
        schedule[4][i] = bankid;
        schedule[5][i] = scheduletype; //scheule type bank or nbfi
        schedule[6][i] = 0; //security amount        
        schedule[7][i] = rentalamount; //rental amount
        schedule[8][i] = Math.round((rentalamount * vatrate) / 100); //vat amount
        schedule[9][i] = parseInt(schedule[6][i]) + parseInt(schedule[7][i]) + parseInt(schedule[8][i]); //total Rental
        schedule[10][i] = 'DUE'; //Status
        totalrental = totalrental - 1;
        adjustedrental = adjustedrental + parseInt(rentalamount);
    }
    adjustedrental = 0;
    var k = 0;
    for (k = 0; k <= i; k++) {
        adjustedrental = adjustedrental + parseInt(schedule[7][i]);
    }
    adjustedrental = adjustedrental - parseInt(commisionamount);
    if (adjustedrental != 0) {
        rentalamount = rentalamount - adjustedrental;
        schedule[7][i] = rentalamount; //rental amount
        schedule[8][i] = Math.round((rentalamount * vatrate) / 100); //vat amount
        schedule[9][i] = parseInt(schedule[6][i]) + parseInt(schedule[7][i]) + parseInt(schedule[8][i]); //total Rental
    }

    TotalScheduleRow = i + 1;
    return schedule;

}


function getScheduleRentalIndex(Schedule, renalno) {
    var x = -1;

    for (x = 0; x < TotalScheduleRow; x++) {
        if (Schedule[13][x] == renalno) {
            return x;
        }
    }
    return x;
}

function getRemainingRental(schedule, i) {
    var j;
    var rt = 0;
    for (j = i; j <= TotalScheduleRow; j++) {
        if (schedule[14][i] != -1)
            rt = rt + 1;
    }
    if (rt == 0)
        rt = 1;
    return rt;
}


function ModifySchedule(changeoption, startrental, endrental, changedrental, changedprincipal, changedintrate, financetype, nominalrate, IsDayBasisInterest, schedule, tota_index) {
    /*
    var startrental = 3;
    var endrental = 6;
    var changedprincipal = 900;
    var changedrental = 1000;
    var leasemode = "Serial";
    var nominalrate = 12;
    var schedule = generateSchedule();
    var changedrate = 12;   
    */
    var YearDay = 360;

    var principalchanged = 0;
    var adjustedprincipal = 0;
    var effectiverate = 0;
    var cumulativeinterest = 0;

    var index = getScheduleRentalIndex(schedule, startrental);
    var rentalno = 0;
    var i = 0;
    var k = 0;
    var prevbalance = 0;
    var interestamount = 0;
    prevbalance = schedule[3][index];
    cumulativeinterest = schedule[14][index];
    var lastinterestdate = new Date();
    var currentdate = new Date();

    for (i = index; i < tota_index; i++) {
        rentalno = schedule[13][i];
        lastinterestdate = new Date(schedule[1][i - 1]);
        currentdate = new Date(schedule[1][i]);

        if (changeoption == "Interest Amount" && (rentalno >= startrental && rentalno <= endrental)) {
            interestamount = changedintrate;
        }
        else {
            if (changeoption == "Interest" && (rentalno >= startrental && rentalno <= endrental)) {
                effectiverate = changedintrate;
            }
            else {
                effectiverate = nominalrate;
            }

            if (IsDayBasisInterest == 1)
                interestamount = Math.round(prevbalance * effectiverate * (datediff('day', lastinterestdate, currentdate)) / (100 * YearDay));
            else
                interestamount = Math.round(prevbalance * effectiverate * datediff('month', lastinterestdate, currentdate) / 1200);
        }

        cumulativeinterest = cumulativeinterest + interestamount;

        schedule[6][i] = interestamount;

        if (changeoption == "Installment") {
            if (rentalno >= startrental && rentalno <= endrental) {
                schedule[7][i] = changedrental;
                schedule[5][i] = schedule[7][i] * 1 - schedule[6][i] * 1;
            }
            else {
                if (financetype == "Annuity") {
                    schedule[5][i] = schedule[7][i] * 1 - schedule[6][i] * 1;
                }
                else if (financetype == "Serial") {
                    schedule[7][i] = schedule[5][i] * 1 + schedule[6][i] * 1;
                }
            }
        }
        else if (changeoption == "Principal") {
            if (rentalno >= startrental && rentalno <= endrental) {
                schedule[5][i] = changedprincipal;
                schedule[7][i] = schedule[5][i] * 1 + schedule[6][i] * 1;
            }
            else {
                if (financetype == "Annuity") {
                    schedule[5][i] = schedule[7][i] * 1 - schedule[6][i] * 1;
                }
                else if (financetype == "Serial") {
                    schedule[7][i] = schedule[5][i] * 1 + schedule[6][i] * 1;
                }
            }
        }
        else if (changeoption == "Interest" || changeoption == "Interest Amount") {
            if (financetype == "Annuity") {
                schedule[5][i] = schedule[7][i] * 1 - schedule[6][i] * 1;
            }
            else if (financetype == "Serial") {
                schedule[7][i] = schedule[5][i] * 1 + schedule[6][i] * 1;
            }
        }
        else if (changeoption == "Capitalize") {
            if (rentalno >= startrental && rentalno <= endrental) {
                schedule[2][i] = "Interest Capitalization";
                schedule[5][i] = 0;
                schedule[7][i] = schedule[5][i] * 1 + schedule[6][i] * 1;
            }
            else {
                if (financetype == "Annuity") {
                    schedule[5][i] = schedule[7][i] * 1 - schedule[6][i] * 1;
                }
                else if (financetype == "Serial") {
                    schedule[7][i] = schedule[5][i] * 1 + schedule[6][i] * 1;
                }
            }
        }

        if (changeoption == "Capitalize" && (rentalno >= startrental && rentalno <= endrental)) {
            schedule[8][i] = parseInt(schedule[3][i]) + parseInt(schedule[6][i]);
            schedule[9][i] = parseInt(schedule[4][i]) + parseInt(schedule[6][i]);
        }
        else {
            schedule[8][i] = parseInt(schedule[3][i]) - parseInt(schedule[5][i]);
            schedule[9][i] = parseInt(schedule[4][i]) - parseInt(schedule[5][i]);
        }

        prevbalance = schedule[8][i];
        schedule[3][i + 1] = schedule[8][i];   //APBBOM
        schedule[4][i + 1] = schedule[9][i];  //PBBOM
        schedule[14][i] = cumulativeinterest;  //PBEOM
        schedule[15][i] = effectiverate;
    }
    i = tota_index;
    TotalScheduleRow = i;
    return schedule;
}

function restructure(startrental, endrental, changedprincipal, changedrental, leasemode, nominalrate, schedule, changedrate, tota_index) {
    /*
    var startrental = 3;
    var endrental = 6;
    var changedprincipal = 900;
    var changedrental = 1000;
    var leasemode = "Serial";
    var nominalrate = 12;
    var schedule = generateSchedule();
    var changedrate = 12;   
    */

    var principalchanged = 0;
    var adjustedprincipal = 0;
    var interestrate = changedrate;

    var index = getScheduleRentalIndex(schedule, startrental);
    var rentalno = 0;
    var i = 0;
    var k = 0;
    var prevbalance = 0;
    var periodinterest = 0;
    prevbalance = schedule[3][index];
    //alert('here');
    for (i = index; i < tota_index; i++) {
        rentalno = schedule[13][i];
        if (rentalno <= endrental && rentalno >= startrental) {
            if (leasemode == "Serial") {
                periodinterest = Math.round((schedule[3][i] * schedule[14][i] * changedrate) / (prevbalance * schedule[15][i]));
                schedule[5][i] = changedprincipal;
                schedule[15][j] = changedrate;
                schedule[14][i] = changedrate;
            }
            else {
                periodinterest = Math.round((schedule[3][i] * schedule[14][i] * schedule[15][i]) / (prevbalance * schedule[15][i]));
                schedule[7][i] = changedrental;
            }
        }
        else if (rentalno > endrental && leasemode == "Serial") {
            periodinterest = Math.round((schedule[3][i] * schedule[14][i] * schedule[15][i]) / (prevbalance * schedule[15][i]));
            if (principalchanged == 0) {
                adjustedprincipal = Math.round(parseInt(schedule[3][i]) / getRemainingRental(schedule, i));
                principalchanged = 1;
                schedule[5][i] = adjustedprincipal;
            }
            else {
                schedule[5][i] = adjustedprincipal;
            }
        }
        else {
            periodinterest = Math.round((schedule[3][i] * schedule[14][i] * schedule[15][i]) / (prevbalance * schedule[15][i]));
        }

        //periodinterest = Math.round((schedule[3][i] * schedule[6][i]) / prevbalance);
        schedule[6][i] = parseInt(schedule[6][i]) + parseInt(periodinterest) - parseInt(schedule[14][i]);

        if (leasemode == "Serial") {
            schedule[7][i] = schedule[5][i] + schedule[6][i];
        }
        else {
            schedule[5][i] = schedule[7][i] - schedule[6][i];
        }
        schedule[8][i] = parseInt(schedule[3][i]) - parseInt(schedule[5][i]);
        schedule[9][i] = parseInt(schedule[4][i]) - parseInt(schedule[5][i]);
        prevbalance = parseInt(schedule[3][i + 1]);
        schedule[3][i + 1] = schedule[8][i];   //APBBOM
        schedule[4][i + 1] = schedule[9][i];  //PBBOM
        schedule[14][i] = periodinterest;  //PBEOM
    }
    i = tota_index;
    //        var j = 0;
    //        var tb = "<table>"
    //        tb = tb + "<tr><td>" + "SL#" + "</td><td>" + "Rental No" + "</td><td>" + "Date" + "</td><td>" + "Particulars" + "</td><td>" + "APBBOM" + "</td><td>" + "PBBOM" + "</td><td>" + "Principal" + "</td><td>" + "Interest" + "</td><td>" + "Rental" + "</td><td>" + "APBEOM" + "</td><td>" + "PBEOM" + "</td><td>" + "In Flow" + "</td><td>" + "Out Flow" + "</td><td>" + "Net Flow" + "</td><td>" + "Cumulative Interest" + "</td></tr>";
    //        var dd;
    //        for (j = 0; j <= i; j++) {
    //            tb = tb + "<tr><td>" + schedule[0][j] + "</td><td>" + schedule[13][j] + "</td><td>" + formatdate(schedule[1][j]) + "</td><td>" + schedule[2][j] + "</td><td>" + schedule[3][j] + "</td><td>" + schedule[4][j] + "</td><td>" + schedule[5][j] + "</td><td>" + schedule[6][j] + "</td><td>" + schedule[7][j] + "</td><td>" + schedule[8][j] + "</td><td>" + schedule[9][j] + "</td><td>" + schedule[11][j] + "</td><td>" + schedule[10][j] + "</td><td>" + schedule[12][j] + "</td><td>" + schedule[14][j] + "</td></tr>";
    //        }
    //        tb = tb + "</table>";
    //        document.getElementById("sch").innerHTML = tb;
    TotalScheduleRow = i;
    return schedule;
}


function generateDepositSchedule(grossprincipal, nominalrate, rental, execdate, inpstartdate, InterestFrequency, PrinciapalFrequency, PremiumFrequency, InterestCapitalizationFrequency, Tenor, leasemode, leasetype, DayBasisInterest, monthendbasis, GracePeriod, GracePeriodFrequency, isCapitalizeGraceInterest, isGracePeriodinTenor, GracePeriodInterestRate, InterestResting, TAXRate, DeductTaxOnCapitalize, ProductCategory, PremiumAmount, PremiumCount, IsDayUnitPeriod) {

    //    alert(grossprincipal);
    //    alert(nominalrate);
    //    alert(execdate);
    //    alert(inpstartdate);
    //    alert(InterestFrequency);
    //    alert(PrinciapalFrequency);
    //    alert(PremiumFrequency);
    //    alert(InterestCapitalizationFrequency);
    //    alert(Tenor);
    //    alert(DayBasisInterest);
    //    alert(monthendbasis);
    //    alert(TAXRate);
    //    alert(DeductTaxOnCapitalize);
    //    alert(ProductCategory);
    //    alert(PremiumAmount);
    //    alert(PremiumCount);

    var schedule = new Array(21);
    var i = 0;
    for (i = 0; i < 21; i++)
        schedule[i] = new Array(200);
    i = 0;
    var rentalno = 1;
    var startdate;
    var enddate;
    startdate = new Date(inpstartdate);
    if (IsDayUnitPeriod == 1)
        enddate = dateadd('day', startdate, Tenor);
    else
        enddate = dateadd('month', startdate, Tenor);

    var currentdate;
    var lastactivity;
    var YearDay = 360;
    var totaltaxableinterest = 0;

    var principalbalance = grossprincipal;


    var NoOfPrincipalPayment = -1;
    var serialprincipal = -1;

    var restingno;

    var datelist = new Array();

    var cumulativeinterest = 0;

    var PrinciapalDate = startdate;
    var InterestDate = startdate;
    var PremiumDate = startdate;
    var InterestCapitalizationDate = startdate;
    var InterestAmount = 0;
    var lastinterestdate = startdate;
    //var tax;
    var TAXAmout = 0;



    i = 0;
    var j = -1;


    var index = 0;



    schedule[0][i] = i + 1; //slno
    schedule[1][i] = startdate; //date
    schedule[2][i] = "Execution"; //payment type
    schedule[3][i] = 0; //APBBOM
    schedule[4][i] = grossprincipal; //PBBOM
    schedule[5][i] = 0; //principal
    schedule[6][i] = 0; //interest
    schedule[7][i] = 0; //rental
    schedule[8][i] = grossprincipal;  //APBEOM
    schedule[9][i] = grossprincipal; //PBEOM
    schedule[10][i] = 0; //outflow
    schedule[11][i] = grossprincipal; //inflow
    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
    schedule[13][i] = 0; //rentalno
    schedule[14][i] = 0; //cumulative interest
    schedule[15][i] = nominalrate; //period interest rate
    schedule[16][i] = 'NA'; //Payment Status
    schedule[17][i] = 0; //TAX Percent
    schedule[18][i] = 0; //TAX Amount
    schedule[19][i] = 0; //Interest Capitalize
    schedule[20][i] = 0; //Total Payable

    currentdate = startdate;

    PrinciapalDate = lastinterestdate;
    InterestDate = lastinterestdate;

    restingno = InterestResting;
    var testnum = 0;
    if (PrinciapalFrequency > 0) {
        if (IsDayUnitPeriod == 1)
            PrinciapalDate = dateadd('day', startdate, PrinciapalFrequency);
        else
            PrinciapalDate = dateadd('month', startdate, PrinciapalFrequency);
    }
    else
        PrinciapalDate = enddate;
    if (InterestFrequency > 0)
        InterestDate = dateadd('month', startdate, InterestFrequency);
    else {
        if (InterestCapitalizationFrequency > 0)
            InterestDate = dateadd('month', enddate, 1);
        else
            InterestDate = enddate;
    }
    if (PremiumFrequency > 0)
        PremiumDate = dateadd('month', startdate, PremiumFrequency);
    else
        PremiumDate = dateadd('month', enddate, 1);
    if (InterestCapitalizationFrequency > 0)
        InterestCapitalizationDate = dateadd('month', startdate, InterestCapitalizationFrequency);
    else
        InterestCapitalizationDate = dateadd('month', enddate, 1);
    currentdate = startdate;
    lastactivity = startdate;
    rentalno = 1;

    if (ProductCategory == "Profit First") {
        i = i + 1;
        if (DayBasisInterest == 0)
            cumulativeinterest = Math.round(principalbalance * nominalrate * datediff('month', currentdate, enddate) / 1200);
        else {
            cumulativeinterest = Math.round(principalbalance * nominalrate * datediff('day', currentdate, enddate) / (100 * YearDay));
        }
        TAXAmout = Math.round((cumulativeinterest * TAXRate / 100));
        schedule[0][i] = i + 1; //slno
        schedule[1][i] = currentdate; //date
        schedule[2][i] = "Interest Payment"; //payment type
        schedule[3][i] = principalbalance; //APBBOM
        schedule[4][i] = principalbalance; //PBBOM
        schedule[5][i] = 0; //principal
        schedule[6][i] = cumulativeinterest; //interest
        schedule[7][i] = cumulativeinterest; //rental
        schedule[8][i] = principalbalance;  //APBEOM
        schedule[9][i] = principalbalance; //PBEOM
        schedule[10][i] = cumulativeinterest; //outflow
        schedule[11][i] = 0; //inflow
        schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
        schedule[13][i] = rentalno; //rentalno
        schedule[14][i] = 0; //cumulative interest
        schedule[15][i] = nominalrate; //period interest rate
        schedule[16][i] = 'Due'; //Payment Status
        schedule[17][i] = TAXRate; //TAX Percent
        schedule[18][i] = TAXAmout; //TAX Amount
        schedule[19][i] = 0; //Interest Capitalize
        schedule[20][i] = cumulativeinterest - TAXAmout;  //Total Payable

        cumulativeinterest = 0;
        TAXAmout = 0;
        rentalno = rentalno + 1;
        i = i + 1;
        currentdate = enddate;

        schedule[0][i] = i + 1; //slno
        schedule[1][i] = currentdate; //date
        schedule[2][i] = "Principal Payment"; //payment type
        schedule[3][i] = principalbalance; //APBBOM
        schedule[4][i] = principalbalance; //PBBOM
        schedule[5][i] = principalbalance; //principal
        schedule[6][i] = 0; //interest
        schedule[7][i] = principalbalance; //rental
        schedule[8][i] = 0;  //APBEOM
        schedule[9][i] = 0; //PBEOM
        schedule[10][i] = principalbalance; //outflow
        schedule[11][i] = 0; //inflow
        schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
        schedule[13][i] = rentalno; //rentalno
        schedule[14][i] = 0; //cumulative interest
        schedule[15][i] = 0; //period interest rate
        schedule[16][i] = 'Due'; //Payment Status
        schedule[17][i] = 0; //TAX Percent
        schedule[18][i] = TAXAmout; //TAX Amount
        schedule[19][i] = 0; //Interest Capitalize
        schedule[20][i] = principalbalance;  //Total Payable
        currentdate = dateadd('month', enddate, 1);
    }
    while (CompareDate(currentdate, enddate) <= 0) {
        datelist = new Array();
        datelist[1] = InterestCapitalizationDate;
        datelist[2] = PrinciapalDate;
        datelist[0] = InterestDate;
        datelist[3] = PremiumDate;


        j = getMinMaxDate('min', datelist);
        currentdate = datelist[j];

        if (CompareDate(currentdate, enddate) > 0)
            break;

        switch (j) {
            case 2: //Principal Payment
                i = i + 1;
                //calculate cumulative interest
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                if (cumulativeinterest > 0) {
                    TAXAmout = Math.round((cumulativeinterest * TAXRate) / 100);

                    schedule[0][i] = i + 1; //slno
                    schedule[1][i] = currentdate; //date
                    schedule[2][i] = "Interest Payment"; //payment type
                    schedule[3][i] = principalbalance; //APBBOM
                    schedule[4][i] = principalbalance; //PBBOM
                    schedule[5][i] = 0; //principal
                    schedule[6][i] = cumulativeinterest //- TAXAmout; //interest
                    schedule[7][i] = cumulativeinterest //- TAXAmout; //rental
                    schedule[8][i] = principalbalance;  //APBEOM
                    schedule[9][i] = principalbalance; //PBEOM
                    schedule[10][i] = cumulativeinterest; //outflow
                    schedule[11][i] = 0; //inflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = rentalno; //rentalno
                    schedule[14][i] = cumulativeinterest; //cumulative interest
                    schedule[15][i] = nominalrate; //period interest rate
                    schedule[16][i] = 'Due'; //Payment Status
                    schedule[17][i] = TAXRate; //TAX Percent
                    schedule[18][i] = TAXAmout; //TAX Amount
                    schedule[19][i] = 0; //Interest Capitalize
                    schedule[20][i] = cumulativeinterest - TAXAmout; //Total Payable
                    /*********************/
                    cumulativeinterest = 0;
                    rentalno = rentalno + 1;
                    i = i + 1;
                }

                lastinterestdate = currentdate;
                if (CompareDate(currentdate, enddate) == 0) {
                    if (DeductTaxOnCapitalize == 0) {
                        TAXAmout = Math.round((totaltaxableinterest * TAXRate) / 100);
                        totaltaxableinterest = 0;
                    }
                    else
                        TAXAmout = 0;
                }
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "Principal Payment"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = principalbalance; //principal
                schedule[6][i] = 0; //interest
                schedule[7][i] = principalbalance; //rental
                schedule[8][i] = principalbalance - principalbalance;  //APBEOM
                schedule[9][i] = principalbalance - principalbalance; //PBEOM
                schedule[10][i] = principalbalance; //outflow
                schedule[11][i] = 0; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent
                schedule[18][i] = TAXAmout; //TAX Amount
                schedule[19][i] = 0; //Interest Capitalize
                schedule[20][i] = principalbalance - TAXAmout; //Total Payable
                /*********************/
                principalbalance = 0;
                rentalno = rentalno + 1;
                if (PrinciapalFrequency > 0)
                    PrinciapalDate = dateadd('month', PrinciapalDate, PrinciapalFrequency);
                else
                    PrinciapalDate = enddate;
                PrinciapalDate = dateadd('month', PrinciapalDate, 1);
                break;
            case 0: //Interest Payment
                i = i + 1;
                //calculate cumulative interest
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                lastinterestdate = currentdate;

                if (DeductTaxOnCapitalize == 1)
                    TAXAmout = Math.round((InterestAmount * TAXRate) / 100);
                else {
                    TAXAmout = Math.round((cumulativeinterest * TAXRate) / 100);
                    totaltaxableinterest = 0;
                }
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "Interest Payment"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = 0; //principal
                schedule[6][i] = cumulativeinterest //- TAXAmout; //interest
                schedule[7][i] = cumulativeinterest //- TAXAmout; //rental
                schedule[8][i] = principalbalance;  //APBEOM
                schedule[9][i] = principalbalance; //PBEOM
                schedule[10][i] = cumulativeinterest; //outflow
                schedule[11][i] = 0; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent
                schedule[18][i] = TAXAmout; //TAX Amount
                schedule[19][i] = 0; //Interest Capitalize
                schedule[20][i] = cumulativeinterest - TAXAmout; //Total Payable
                /*********************/
                cumulativeinterest = 0;
                rentalno = rentalno + 1;
                if (InterestFrequency > 0)
                    InterestDate = dateadd('month', InterestDate, InterestFrequency);
                else
                    InterestDate = dateadd('month', InterestDate, 1);
                break;
            case 3: //deposit premium
                if (PremiumCount <= 0) {
                    PremiumDate = dateadd('month', PremiumDate, PremiumFrequency);
                    break;
                }
                i = i + 1;
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                lastinterestdate = currentdate;

                //calculate cumulative interest
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "DPS Premium"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = PremiumAmount; //principal
                schedule[6][i] = 0; //interest
                schedule[7][i] = PremiumAmount; //rental
                principalbalance = principalbalance * 1 + PremiumAmount * 1;
                schedule[8][i] = principalbalance;  //APBEOM
                schedule[9][i] = principalbalance; //PBEOM
                schedule[10][i] = 0; //outflow
                schedule[11][i] = PremiumAmount; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent
                schedule[18][i] = 0; //TAX Amount
                schedule[19][i] = 0; //Interest Capitalize
                schedule[20][i] = principalbalance; //Total Payable
                /*********************/
                //cumulativeinterest = 0;
                rentalno = rentalno + 1;
                PremiumDate = dateadd('month', PremiumDate, PremiumFrequency);
                PremiumCount = PremiumCount - 1;
                break;
            case 1: //Intereste Capitalization
                i = i + 1;
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                if (cumulativeinterest <= 0) {
                    i = i - 1;
                    if (InterestCapitalizationFrequency > 0)
                        InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                    else
                        InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, 1);
                    break;
                }
                if (DeductTaxOnCapitalize == 1)
                    TAXAmout = Math.round((cumulativeinterest * TAXRate) / 100);
                else {
                    TAXAmout = 0;
                    totaltaxableinterest = totaltaxableinterest + cumulativeinterest;
                }

                lastinterestdate = currentdate;
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "Capitalize Interest"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = 0; //principal
                schedule[6][i] = cumulativeinterest; //interest
                schedule[7][i] = cumulativeinterest; //rental
                principalbalance = principalbalance + cumulativeinterest - TAXAmout;
                schedule[8][i] = principalbalance;  //APBEOM
                schedule[9][i] = principalbalance; //PBEOM
                schedule[10][i] = 0; //outflow
                schedule[11][i] = 0; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent                    
                schedule[18][i] = TAXAmout; //TAX Amount
                schedule[19][i] = cumulativeinterest; //Interest Capitalize
                schedule[20][i] = cumulativeinterest - TAXAmout; //Total Payable
                cumulativeinterest = 0;
                rentalno = rentalno + 1;
                if (InterestCapitalizationFrequency > 0)
                    InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                else
                    InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, 1);
                break;
            default:
                break;
        }
    }
    TotalScheduleRow = i;
    var k = 0;

    //    var tb = "<table class=\"yui\">";
    //    tb = tb + "<tr><td>slno</td>";
    //    tb = tb + "<td>date</td>";
    //    tb = tb + "<td>payment type</td>";
    //    tb = tb + "<td>APBBOM</td>";
    //    tb = tb + "<td>PBBOM</td>";
    //    tb = tb + "<td>principal</td>";
    //    tb = tb + "<td>interest</td>";
    //    //        tb = tb + "<td>rental</td>";
    //    tb = tb + "<td>APBEOM</td>";
    //    tb = tb + "<td>PBEOM</td>";
    //    //        tb = tb + "<td>outflow</td>";
    //    //        tb = tb + "<td>inflow</td>";
    //    //        tb = tb + "<td>netflow</td>";
    //    tb = tb + "<td>rentalno</td>";
    //    tb = tb + "<td>cumulative interest</td>";
    //    //        tb = tb + "<td>period interest rate</td>";
    //    //        tb = tb + "<td>Payment Status</td>";
    //    tb = tb + "<td>TAX Percent</td>";
    //    tb = tb + "<td>TAX Amount</td>";
    //    tb = tb + "<td>Interest Capitalize</td>";
    //    tb = tb + "<td>Total Payable</td></tr>";

    //    for (i = 0; i <= TotalScheduleRow; i++) {
    //        tb = tb + "<tr><td>" + schedule[0][i] + "</td>";
    //        tb = tb + "<td>" + schedule[1][i] + "</td>";
    //        tb = tb + "<td>" + schedule[2][i] + "</td>";
    //        tb = tb + "<td>" + schedule[3][i] + "</td>";
    //        tb = tb + "<td>" + schedule[4][i] + "</td>";
    //        tb = tb + "<td>" + schedule[5][i] + "</td>";
    //        tb = tb + "<td>" + schedule[6][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[7][i] + "</td>";
    //        tb = tb + "<td>" + schedule[8][i] + "</td>";
    //        tb = tb + "<td>" + schedule[9][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[10][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[11][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[12][i] + "</td>";
    //        tb = tb + "<td>" + schedule[13][i] + "</td>";
    //        tb = tb + "<td>" + schedule[14][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[15][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[16][i] + "</td>";
    //        tb = tb + "<td>" + schedule[17][i] + "</td>";
    //        tb = tb + "<td>" + schedule[18][i] + "</td>";
    //        tb = tb + "<td>" + schedule[19][i] + "</td>";
    //        tb = tb + "<td>" + schedule[20][i] + "</td></tr>";
    //    }
    //    tb = tb + "</table>";
    //    document.getElementById('sch').innerHTML = tb;

    return schedule;
}


function generateDPSSchedule(grossprincipal, nominalrate, rental, execdate, inpstartdate, InterestFrequency, PrinciapalFrequency, PremiumFrequency, InterestCapitalizationFrequency, Tenor, leasemode, leasetype, DayBasisInterest, monthendbasis, GracePeriod, GracePeriodFrequency, isCapitalizeGraceInterest, isGracePeriodinTenor, GracePeriodInterestRate, InterestResting, TAXRate, DeductTaxOnCapitalize, ProductCategory, PremiumAmount, PremiumCount, IsDayUnitPeriod) {

    //    alert(grossprincipal);
    //    alert(nominalrate);
    //    alert(execdate);
    //    alert(inpstartdate);
    //    alert(InterestFrequency);
    //    alert(PrinciapalFrequency);
    //    alert(PremiumFrequency);
    //    alert(InterestCapitalizationFrequency);
    //    alert(Tenor);
    //    alert(DayBasisInterest);
    //    alert(monthendbasis);
    //    alert(TAXRate);
    //    alert(DeductTaxOnCapitalize);
    //    alert(ProductCategory);
    //    alert(PremiumAmount);
    //    alert(PremiumCount);

    var schedule = new Array(21);
    var i = 0;
    for (i = 0; i < 21; i++)
        schedule[i] = new Array(200);
    i = 0;
    var rentalno = 1;
    var startdate;
    var enddate;
    startdate = new Date(inpstartdate);
    if (IsDayUnitPeriod == 1)
        enddate = dateadd('day', startdate, Tenor);
    else
        enddate = dateadd('month', startdate, Tenor);

    var currentdate;
    var lastactivity;
    var YearDay = 360;
    var totaltaxableinterest = 0;

    var principalbalance = grossprincipal;


    var NoOfPrincipalPayment = -1;
    var serialprincipal = -1;

    var restingno;

    var datelist = new Array();

    var cumulativeinterest = 0;

    var PrinciapalDate = startdate;
    var InterestDate = startdate;
    var PremiumDate = startdate;
    var InterestCapitalizationDate = startdate;
    var InterestAmount = 0;
    var lastinterestdate = startdate;
    //var tax;
    var TAXAmout = 0;

    i = 0;
    var j = -1;


    var index = 0;

    schedule[0][i] = i + 1; //slno
    schedule[1][i] = execdate; //date
    schedule[2][i] = "Execution"; //payment type
    schedule[3][i] = 0; //APBBOM
    schedule[4][i] = grossprincipal; //PBBOM
    schedule[5][i] = grossprincipal; //principal
    schedule[6][i] = 0; //interest
    schedule[7][i] = grossprincipal; //rental
    schedule[8][i] = grossprincipal;  //APBEOM
    schedule[9][i] = grossprincipal; //PBEOM
    schedule[10][i] = 0; //outflow
    schedule[11][i] = grossprincipal; //inflow
    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
    schedule[13][i] = 0; //rentalno
    schedule[14][i] = 0; //cumulative interest
    schedule[15][i] = nominalrate; //period interest rate
    schedule[16][i] = 'NA'; //Payment Status
    schedule[17][i] = 0; //TAX Percent
    schedule[18][i] = 0; //TAX Amount
    schedule[19][i] = 0; //Interest Capitalize
    schedule[20][i] = 0; //Total Payable

    currentdate = startdate;

    PrinciapalDate = lastinterestdate;
    InterestDate = lastinterestdate;

    restingno = InterestResting;
    var testnum = 0;
    if (PrinciapalFrequency > 0) {
        if (IsDayUnitPeriod == 1)
            PrinciapalDate = dateadd('day', startdate, PrinciapalFrequency);
        else
            PrinciapalDate = dateadd('month', startdate, PrinciapalFrequency);
    }
    else
        PrinciapalDate = enddate;
    if (InterestFrequency > 0)
        InterestDate = dateadd('month', startdate, InterestFrequency);
    else {
        if (InterestCapitalizationFrequency > 0)
            InterestDate = dateadd('month', enddate, 1);
        else
            InterestDate = enddate;
    }
//    if (PremiumFrequency > 0)
//        PremiumDate = dateadd('month', startdate, PremiumFrequency);
//    else
    //        PremiumDate = dateadd('month', enddate, 1);

    if (InterestCapitalizationFrequency > 0)
        InterestCapitalizationDate = dateadd('month', startdate, InterestCapitalizationFrequency);
    else
        InterestCapitalizationDate = dateadd('month', enddate, 1);
    currentdate = startdate;
    lastactivity = startdate;
    rentalno = 1;

    if (ProductCategory == "Profit First") {
        i = i + 1;
        if (DayBasisInterest == 0)
            cumulativeinterest = Math.round(principalbalance * nominalrate * datediff('month', currentdate, enddate) / 1200);
        else {
            cumulativeinterest = Math.round(principalbalance * nominalrate * datediff('day', currentdate, enddate) / (100 * YearDay));
        }
        TAXAmout = Math.round((cumulativeinterest * TAXRate / 100));
        schedule[0][i] = i + 1; //slno
        schedule[1][i] = currentdate; //date
        schedule[2][i] = "Interest Payment"; //payment type
        schedule[3][i] = principalbalance; //APBBOM
        schedule[4][i] = principalbalance; //PBBOM
        schedule[5][i] = 0; //principal
        schedule[6][i] = cumulativeinterest; //interest
        schedule[7][i] = cumulativeinterest; //rental
        schedule[8][i] = principalbalance;  //APBEOM
        schedule[9][i] = principalbalance; //PBEOM
        schedule[10][i] = cumulativeinterest; //outflow
        schedule[11][i] = 0; //inflow
        schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
        schedule[13][i] = rentalno; //rentalno
        schedule[14][i] = 0; //cumulative interest
        schedule[15][i] = nominalrate; //period interest rate
        schedule[16][i] = 'Due'; //Payment Status
        schedule[17][i] = TAXRate; //TAX Percent
        schedule[18][i] = TAXAmout; //TAX Amount
        schedule[19][i] = 0; //Interest Capitalize
        schedule[20][i] = cumulativeinterest - TAXAmout;  //Total Payable

        cumulativeinterest = 0;
        TAXAmout = 0;
        rentalno = rentalno + 1;
        i = i + 1;
        currentdate = enddate;

        schedule[0][i] = i + 1; //slno
        schedule[1][i] = currentdate; //date
        schedule[2][i] = "Principal Payment"; //payment type
        schedule[3][i] = principalbalance; //APBBOM
        schedule[4][i] = principalbalance; //PBBOM
        schedule[5][i] = principalbalance; //principal
        schedule[6][i] = 0; //interest
        schedule[7][i] = principalbalance; //rental
        schedule[8][i] = 0;  //APBEOM
        schedule[9][i] = 0; //PBEOM
        schedule[10][i] = principalbalance; //outflow
        schedule[11][i] = 0; //inflow
        schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
        schedule[13][i] = rentalno; //rentalno
        schedule[14][i] = 0; //cumulative interest
        schedule[15][i] = 0; //period interest rate
        schedule[16][i] = 'Due'; //Payment Status
        schedule[17][i] = 0; //TAX Percent
        schedule[18][i] = TAXAmout; //TAX Amount
        schedule[19][i] = 0; //Interest Capitalize
        schedule[20][i] = principalbalance;  //Total Payable
        currentdate = dateadd('month', enddate, 1);
    }
    while (CompareDate(currentdate, enddate) <= 0) {
        datelist = new Array();
        datelist[1] = InterestCapitalizationDate;
        datelist[2] = PrinciapalDate;
        datelist[0] = InterestDate;
        datelist[3] = PremiumDate;


        j = getMinMaxDate('min', datelist);
        currentdate = datelist[j];

        if (CompareDate(currentdate, enddate) > 0)
            break;

        switch (j) {
            case 2: //Principal Payment
                i = i + 1;
                //calculate cumulative interest
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                if (cumulativeinterest > 0) {
                    TAXAmout = Math.round((cumulativeinterest * TAXRate) / 100);

                    schedule[0][i] = i + 1; //slno
                    schedule[1][i] = currentdate; //date
                    schedule[2][i] = "Interest Payment"; //payment type
                    schedule[3][i] = principalbalance; //APBBOM
                    schedule[4][i] = principalbalance; //PBBOM
                    schedule[5][i] = 0; //principal
                    schedule[6][i] = cumulativeinterest //- TAXAmout; //interest
                    schedule[7][i] = cumulativeinterest //- TAXAmout; //rental
                    schedule[8][i] = principalbalance;  //APBEOM
                    schedule[9][i] = principalbalance; //PBEOM
                    schedule[10][i] = cumulativeinterest; //outflow
                    schedule[11][i] = 0; //inflow
                    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                    schedule[13][i] = rentalno; //rentalno
                    schedule[14][i] = cumulativeinterest; //cumulative interest
                    schedule[15][i] = nominalrate; //period interest rate
                    schedule[16][i] = 'Due'; //Payment Status
                    schedule[17][i] = TAXRate; //TAX Percent
                    schedule[18][i] = TAXAmout; //TAX Amount
                    schedule[19][i] = 0; //Interest Capitalize
                    schedule[20][i] = cumulativeinterest - TAXAmout; //Total Payable
                    /*********************/
                    cumulativeinterest = 0;
                    rentalno = rentalno + 1;
                    i = i + 1;
                }

                lastinterestdate = currentdate;
                if (CompareDate(currentdate, enddate) == 0) {
                    if (DeductTaxOnCapitalize == 0) {
                        TAXAmout = Math.round((totaltaxableinterest * TAXRate) / 100);
                        totaltaxableinterest = 0;
                    }
                    else
                        TAXAmout = 0;
                }
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "Principal Payment"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = principalbalance; //principal
                schedule[6][i] = 0; //interest
                schedule[7][i] = principalbalance; //rental
                schedule[8][i] = principalbalance - principalbalance;  //APBEOM
                schedule[9][i] = principalbalance - principalbalance; //PBEOM
                schedule[10][i] = principalbalance; //outflow
                schedule[11][i] = 0; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent
                schedule[18][i] = TAXAmout; //TAX Amount
                schedule[19][i] = 0; //Interest Capitalize
                schedule[20][i] = principalbalance - TAXAmout; //Total Payable
                /*********************/
                principalbalance = 0;
                rentalno = rentalno + 1;
                if (PrinciapalFrequency > 0)
                    PrinciapalDate = dateadd('month', PrinciapalDate, PrinciapalFrequency);
                else
                    PrinciapalDate = enddate;
                PrinciapalDate = dateadd('month', PrinciapalDate, 1);
                break;
            case 0: //Interest Payment
                i = i + 1;
                //calculate cumulative interest
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                lastinterestdate = currentdate;

                if (DeductTaxOnCapitalize == 1)
                    TAXAmout = Math.round((InterestAmount * TAXRate) / 100);
                else {
                    TAXAmout = Math.round((cumulativeinterest * TAXRate) / 100);
                    totaltaxableinterest = 0;
                }
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "Interest Payment"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = 0; //principal
                schedule[6][i] = cumulativeinterest //- TAXAmout; //interest
                schedule[7][i] = cumulativeinterest //- TAXAmout; //rental
                schedule[8][i] = principalbalance;  //APBEOM
                schedule[9][i] = principalbalance; //PBEOM
                schedule[10][i] = cumulativeinterest; //outflow
                schedule[11][i] = 0; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent
                schedule[18][i] = TAXAmout; //TAX Amount
                schedule[19][i] = 0; //Interest Capitalize
                schedule[20][i] = cumulativeinterest - TAXAmout; //Total Payable
                /*********************/
                cumulativeinterest = 0;
                rentalno = rentalno + 1;
                if (InterestFrequency > 0)
                    InterestDate = dateadd('month', InterestDate, InterestFrequency);
                else
                    InterestDate = dateadd('month', InterestDate, 1);
                break;
            case 3: //deposit premium
                if (PremiumCount <= 0) {
                    PremiumDate = dateadd('month', PremiumDate, PremiumFrequency);
                    break;
                }
                i = i + 1;
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                lastinterestdate = currentdate;

                //calculate cumulative interest
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "DPS Premium"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = PremiumAmount; //principal
                schedule[6][i] = 0; //interest
                schedule[7][i] = PremiumAmount; //rental
                principalbalance = principalbalance * 1 + PremiumAmount * 1;
                schedule[8][i] = principalbalance;  //APBEOM
                schedule[9][i] = principalbalance; //PBEOM
                schedule[10][i] = 0; //outflow
                schedule[11][i] = PremiumAmount; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent
                schedule[18][i] = 0; //TAX Amount
                schedule[19][i] = 0; //Interest Capitalize
                schedule[20][i] = principalbalance; //Total Payable
                /*********************/
                //cumulativeinterest = 0;
                rentalno = rentalno + 1;
                PremiumDate = dateadd('month', PremiumDate, PremiumFrequency);
                PremiumCount = PremiumCount - 1;
                break;
            case 1: //Intereste Capitalization
                i = i + 1;
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);
                else {
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                cumulativeinterest = cumulativeinterest + InterestAmount;
                if (cumulativeinterest <= 0) {
                    i = i - 1;
                    if (InterestCapitalizationFrequency > 0)
                        InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                    else
                        InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, 1);
                    break;
                }
                if (DeductTaxOnCapitalize == 1)
                    TAXAmout = Math.round((cumulativeinterest * TAXRate) / 100);
                else {
                    TAXAmout = 0;
                    totaltaxableinterest = totaltaxableinterest + cumulativeinterest;
                }

                lastinterestdate = currentdate;
                schedule[0][i] = i + 1; //slno
                schedule[1][i] = currentdate; //date
                schedule[2][i] = "Capitalize Interest"; //payment type
                schedule[3][i] = principalbalance; //APBBOM
                schedule[4][i] = principalbalance; //PBBOM
                schedule[5][i] = 0; //principal
                schedule[6][i] = cumulativeinterest; //interest
                schedule[7][i] = cumulativeinterest; //rental
                principalbalance = principalbalance + cumulativeinterest - TAXAmout;
                schedule[8][i] = principalbalance;  //APBEOM
                schedule[9][i] = principalbalance; //PBEOM
                schedule[10][i] = 0; //outflow
                schedule[11][i] = 0; //inflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
                schedule[16][i] = 'Due'; //Payment Status
                schedule[17][i] = TAXRate; //TAX Percent                    
                schedule[18][i] = TAXAmout; //TAX Amount
                schedule[19][i] = cumulativeinterest; //Interest Capitalize
                schedule[20][i] = cumulativeinterest - TAXAmout; //Total Payable
                cumulativeinterest = 0;
                rentalno = rentalno + 1;
                if (InterestCapitalizationFrequency > 0)
                    InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
                else
                    InterestCapitalizationDate = dateadd('month', InterestCapitalizationDate, 1);
                break;
            default:
                break;
        }
    }
    TotalScheduleRow = i;
    var k = 0;

    //    var tb = "<table class=\"yui\">";
    //    tb = tb + "<tr><td>slno</td>";
    //    tb = tb + "<td>date</td>";
    //    tb = tb + "<td>payment type</td>";
    //    tb = tb + "<td>APBBOM</td>";
    //    tb = tb + "<td>PBBOM</td>";
    //    tb = tb + "<td>principal</td>";
    //    tb = tb + "<td>interest</td>";
    //    //        tb = tb + "<td>rental</td>";
    //    tb = tb + "<td>APBEOM</td>";
    //    tb = tb + "<td>PBEOM</td>";
    //    //        tb = tb + "<td>outflow</td>";
    //    //        tb = tb + "<td>inflow</td>";
    //    //        tb = tb + "<td>netflow</td>";
    //    tb = tb + "<td>rentalno</td>";
    //    tb = tb + "<td>cumulative interest</td>";
    //    //        tb = tb + "<td>period interest rate</td>";
    //    //        tb = tb + "<td>Payment Status</td>";
    //    tb = tb + "<td>TAX Percent</td>";
    //    tb = tb + "<td>TAX Amount</td>";
    //    tb = tb + "<td>Interest Capitalize</td>";
    //    tb = tb + "<td>Total Payable</td></tr>";

    //    for (i = 0; i <= TotalScheduleRow; i++) {
    //        tb = tb + "<tr><td>" + schedule[0][i] + "</td>";
    //        tb = tb + "<td>" + schedule[1][i] + "</td>";
    //        tb = tb + "<td>" + schedule[2][i] + "</td>";
    //        tb = tb + "<td>" + schedule[3][i] + "</td>";
    //        tb = tb + "<td>" + schedule[4][i] + "</td>";
    //        tb = tb + "<td>" + schedule[5][i] + "</td>";
    //        tb = tb + "<td>" + schedule[6][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[7][i] + "</td>";
    //        tb = tb + "<td>" + schedule[8][i] + "</td>";
    //        tb = tb + "<td>" + schedule[9][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[10][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[11][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[12][i] + "</td>";
    //        tb = tb + "<td>" + schedule[13][i] + "</td>";
    //        tb = tb + "<td>" + schedule[14][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[15][i] + "</td>";
    //        //            tb = tb + "<td>" + schedule[16][i] + "</td>";
    //        tb = tb + "<td>" + schedule[17][i] + "</td>";
    //        tb = tb + "<td>" + schedule[18][i] + "</td>";
    //        tb = tb + "<td>" + schedule[19][i] + "</td>";
    //        tb = tb + "<td>" + schedule[20][i] + "</td></tr>";
    //    }
    //    tb = tb + "</table>";
    //    document.getElementById('sch').innerHTML = tb;

    return schedule;
}

function generateTreasurySchedule(grossprincipal, nominalrate, rental, execdate, inpstartdate, InterestFrequency, PrinciapalFrequency, PremiumFrequency, InterestCapitalizationFrequency, Tenor, leasemode, leasetype, DayBasisInterest, monthendbasis, GracePeriod, GracePeriodFrequency, isCapitalizeGraceInterest, isGracePeriodinTenor, GracePeriodInterestRate, InterestResting, principaldate) {

    var rentalno = 1;
    var startdate;
    var principalstartdate = new Date(principaldate);
    startdate = new Date(inpstartdate);
    var executiondate;
    executiondate = new Date(execdate);
    if (DayBasisInterest != 1)
        startdate = executiondate;

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

    if (isGracePeriodinTenor == 0) {
        if (DayBasisInterest == 1)
            enddate = dateadd('month', executiondate, parseInt(Tenor) + parseInt(GracePeriod));
        else
            enddate = dateadd('month', startdate, parseInt(Tenor) + parseInt(GracePeriod));
    }
    else {
        if (DayBasisInterest == 1)
            enddate = dateadd('month', executiondate, parseInt(Tenor));
        else
            enddate = dateadd('month', startdate, parseInt(Tenor));
    }


    if (leasemode == 'BOM') {
        enddate = dateadd('day', enddate, -1);
    }

    var datelist = new Array();

    var cumulativeinterest = 0;

    var PrinciapalDate = startdate;
    var InterestDate = startdate;
    var PremiumDate = startdate;
    var InterestCapitalizationDate = startdate;
    var InterestAmount = 0;
    var lastinterestdate = startdate;

    var schedule = new Array(17);
    for (i = 0; i < 17; i++)
        schedule[i] = new Array(200);

    i = 0;
    var j = -1;

    schedule[0][i] = i;
    var index = 0;

    schedule[3][i] = grossprincipal; //APBBOM
    schedule[4][i] = grossprincipal; //PBBOM
    if (leasemode == 'BOM') {
        schedule[2][i] = "Installment Payment"; //payment type
        schedule[1][i] = executiondate;
        schedule[5][i] = rental; //principal
        schedule[6][i] = 0; //interest
        schedule[7][i] = rental; //rental
        schedule[8][i] = grossprincipal - rental;  //APBEOM
        schedule[9][i] = grossprincipal - rental; //PBEOM
        schedule[11][i] = rental; //inflow
        schedule[13][i] = rentalno; //rentalno
        schedule[16][i] = 'Due'; //Payment Status
        rentalno = rentalno + 1;
        lastinterestdate = startdate;
        if (inpstartdate == principaldate) {
            PrinciapalDate = dateadd('month', startdate, PrinciapalFrequency);
            InterestDate = dateadd('month', startdate, InterestFrequency);
        }
        index = 1;
    }
    else {
        schedule[2][i] = "Execution"; //payment type
        schedule[1][i] = executiondate;
        schedule[5][i] = 0; //principal
        schedule[6][i] = 0; //interest
        schedule[7][i] = 0; //rental
        schedule[8][i] = grossprincipal; //APBEOM
        schedule[9][i] = grossprincipal; //PBEOM
        schedule[13][i] = -1; //rentalno
        schedule[11][i] = 0; //inflow
        schedule[16][i] = 'NA'; //Payment Status
        if (DayBasisInterest == 1) {
            lastinterestdate = executiondate;
        }
        else {
            lastinterestdate = startdate;
            index = 1;

        }
        if (inpstartdate == principaldate) {
            PrinciapalDate = dateadd('month', startdate, PrinciapalFrequency);
            InterestDate = dateadd('month', startdate, InterestFrequency);
        }
        else {
            PrinciapalDate = principalstartdate;
            InterestDate = startdate;
        }
    }
    //schedule[11][i] = rental; //inflow
    schedule[10][i] = grossprincipal; //outflow
    schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
    schedule[14][i] = 0; //cumulative interest
    schedule[15][i] = nominalrate; //period interest rate        
    var tmpGraceperiod = GracePeriod;
    var endgraceperiod = dateadd('month', startdate, GracePeriod);
    //lastinterestdate = startdate;
    currentdate = startdate;

    while (tmpGraceperiod > 0) {
        i = i + 1;
        currentdate = dateadd('month', currentdate, GracePeriodFrequency);
        if (isCapitalizeGraceInterest == 0) {
            if (DayBasisInterest == 1 && index == 0) {
                currentdate = startdate;
                index = 1;
            }
        }
        if (CompareDate(endgraceperiod, currentdate) < 0)
            currentdate = endgraceperiod;
        if (monthendbasis == 1)
            currentdate = getLastDayofMonth(currentdate);

        schedule[0][i] = i;
        schedule[1][i] = currentdate;
        schedule[2][i] = "Installment Payment"; //payment type
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
        schedule[14][i] = InterestAmount; //cumulative interest
        schedule[15][i] = GracePeriodInterestRate; //period interest rate
        schedule[16][i] = 'Due'; //Payment Status
        lastinterestdate = currentdate;
        tmpGraceperiod = tmpGraceperiod - GracePeriodFrequency;
    }
    if (leasetype == "Serial") {
        NoOfPrincipalPayment = Math.ceil(Tenor / PrinciapalFrequency);
        serialprincipal = grossprincipal / NoOfPrincipalPayment;
        serialprincipal = Math.round(serialprincipal);
        rental = serialprincipal;
    }

    restingno = InterestResting;
    var testnum = 0;

    while (CompareDate(PrinciapalDate, enddate) <= 0 || CompareDate(InterestDate, enddate) <= 0) {
        datelist = new Array();
        datelist[0] = PrinciapalDate;
        datelist[1] = InterestDate;
        //            if (index == 1) {
        //                datelist[0] = dateadd('month', PrinciapalDate, PrinciapalFrequency);
        //                datelist[1] = dateadd('month', InterestDate, InterestFrequency);
        //            }
        //            else {
        //                if (inpstartdate != principalstartdate) {
        //                    datelist[0] = principalstartdate;
        //                    PrinciapalDate = principalstartdate;
        //                    datelist[1] = startdate;
        //                    InterestDate = startdate;
        //                }
        //                else {
        //                    datelist[0] = startdate;
        //                    datelist[1] = startdate;
        //                }
        //            }
        if (PremiumFrequency != -1)
            datelist[2] = dateadd('month', PremiumDate, PremiumFrequency);
        else
            datelist[2] = dateadd('month', enddate, 1);

        if (InterestCapitalizationFrequency != -1)
            datelist[3] = dateadd('month', InterestCapitalizationDate, InterestCapitalizationFrequency);
        else
            datelist[3] = dateadd('month', enddate, 1);
        j = getMinMaxDate('min', datelist);
        currentdate = datelist[j];
        //alert(currentdate);

        if (monthendbasis == 1)
            currentdate = getLastDayofMonth(currentdate);
        if (PrinciapalFrequency == InterestFrequency) {
            if (inpstartdate == principaldate)
                j = 4;
        }

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
                    schedule[2][i] = "Principal Payment";
                }
                if (DayBasisInterest == 1)
                    cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                else
                    cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('month', lastinterestdate, currentdate) / 1200);

                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate 

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
                schedule[16][i] = 'Due'; //Payment Status
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

                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate 

                InterestAmount = InterestAmount + cumulativeinterest;
                rental = schedule[5][i] + InterestAmount;
                cumulativeinterest = 0;
                schedule[6][i] = InterestAmount;
                schedule[7][i] = rental;
                lastinterestdate = currentdate;
                InterestDate = dateadd('month', InterestDate, InterestFrequency);
                if (CompareDate(InterestDate, enddate) > 0 && CompareDate(currentdate, enddate) < 0)
                    InterestDate = enddate;
                schedule[11][i] = schedule[7][i]; //inflow
                schedule[10][i] = 0; //outflow
                schedule[12][i] = schedule[11][i] - schedule[10][i]; //netflow
                schedule[13][i] = rentalno; //rentalno
                schedule[16][i] = 'Due'; //Payment Status
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
                schedule[2][i] = "PrincipalPremium";
                PremiumDate = dateadd('month', PremiumDate, PremiumFrequency);
                schedule[3][i] = schedule[8][i - 1]; //APBBOM
                schedule[4][i] = schedule[9][i - 1];  //PBBOM
                cumulativeinterest = cumulativeinterest + Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));

                schedule[14][i] = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay)); //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate

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
                schedule[16][i] = 'Due'; //Payment Status
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
                schedule[14][i] = cumulativeinterest; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate
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
                schedule[16][i] = 'NA'; //Payment Status           
                break;
            case 4:
                restingno = restingno - 1;
                i = i + 1;
                schedule[0][i] = i;
                schedule[1][i] = currentdate;
                schedule[3][i] = schedule[8][i - 1]; //APBBOM
                schedule[4][i] = schedule[9][i - 1];  //PBBOM
                if (DayBasisInterest == 0)
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * PrinciapalFrequency / 1200);
                else {
                    testnum = schedule[9][i - 1];
                    InterestAmount = Math.round(schedule[9][i - 1] * nominalrate * datediff('day', lastinterestdate, currentdate) / (100 * YearDay));
                }
                lastinterestdate = currentdate;

                if (leasetype == "Annuity") {
                    schedule[5][i] = rental - InterestAmount; //principal
                }
                else {
                    schedule[5][i] = serialprincipal; //principal
                    rental = schedule[5][i] + InterestAmount; //principal
                    //schedule[2][i] = "PrinciaplPayment";
                }
                schedule[16][i] = 'Due'; //Payment Status           
                schedule[6][i] = InterestAmount; //interest
                schedule[14][i] = InterestAmount; //cumulative interest
                schedule[15][i] = nominalrate; //period interest rate  
                schedule[2][i] = "Installment Payment";
                //if (index == 1) {
                InterestDate = dateadd('month', InterestDate, InterestFrequency);
                PrinciapalDate = dateadd('month', PrinciapalDate, PrinciapalFrequency);
                /*}
                else {
                InterestDate = startdate;
                PrinciapalDate = startdate;
                index = 1;
                }*/
                schedule[7][i] = Math.round(rental); //rental
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
    TotalScheduleRow = i;
    //    var tb = "<table border=\"1\">";

    //    tb = tb + "<tr><td>SLNO</td>";
    //    tb = tb + "<td>DATE</td>";
    //    tb = tb + "<td>Payment Type</td>";
    //    tb = tb + "<td>APBBOM</td>";
    //    tb = tb + "<td>PBBOM</td>";
    //    tb = tb + "<td>Principal</td>";
    //    tb = tb + "<td>Interest</td>";
    //    tb = tb + "<td>Rental</td>";
    //    tb = tb + "<td>APBEOM</td>";
    //    tb = tb + "<td>PBEOM</td>";
    //    tb = tb + "<td>Outflow</td>";
    //    tb = tb + "<td>Inflow</td>";
    //    tb = tb + "<td>Netflow</td>";
    //    tb = tb + "<td>Rental No</td>";
    //    tb = tb + "<td>Cumulative Interest</td>";
    //    tb = tb + "<td>Interest Rate</td>";
    //    tb = tb + "<td>Status</td></tr>";

    //    for (i = 0; i <= TotalScheduleRow; i++) {
    //        tb = tb + "<tr><td>" + schedule[0][i] + "</td>";
    //        tb = tb + "<td>" + ConvertDate2(schedule[1][i]) + "</td>";
    //        tb = tb + "<td>" + schedule[2][i] + "</td>";
    //        tb = tb + "<td>" + schedule[3][i] + "</td>";
    //        tb = tb + "<td>" + schedule[4][i] + "</td>";
    //        tb = tb + "<td>" + schedule[5][i] + "</td>";
    //        tb = tb + "<td>" + schedule[6][i] + "</td>";
    //        tb = tb + "<td>" + schedule[7][i] + "</td>";
    //        tb = tb + "<td>" + schedule[8][i] + "</td>";
    //        tb = tb + "<td>" + schedule[9][i] + "</td>";
    //        tb = tb + "<td>" + schedule[10][i] + "</td>";
    //        tb = tb + "<td>" + schedule[11][i] + "</td>";
    //        tb = tb + "<td>" + schedule[12][i] + "</td>";
    //        tb = tb + "<td>" + schedule[13][i] + "</td>";
    //        tb = tb + "<td>" + schedule[14][i] + "</td>";
    //        tb = tb + "<td>" + schedule[15][i] + "</td>";
    //        tb = tb + "<td>" + schedule[16][i] + "</td></tr>";
    //    }
    //    tb = tb + "</table>";
    //    document.getElementById('sch').innerHTML = tb;
    return schedule;
}