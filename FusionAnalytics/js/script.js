
var jsonValue = new Array();
var analyticsAccount = new Array();
var monthString = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var g_profileid = "";


function loadAnalyticsAccount(p_accountlist) {
    analyticsAccount = p_accountlist;
    $.each(analyticsAccount, function (index, value) {
        $('#ddlAccountname').append("<option value=" + value.id + ">" + value.name + "</options>");
    });
    showOptions();
}
function showOptions() {
    jsonValue = new Array();
    var selected_id = $('#ddlAccountname').val();
    fusionQueryProperties(selected_id);
   // var account_id = _.where(analyticsAccount, { id: selected_id });
}
function fusionQueryProperties(accountId) {
    // Get a list of all the properties for the account.
    gapi.client.analytics.management.webproperties.list(
        { 'accountId': accountId })
      .then(fusionHandleProperties)
      .then(null, function (err) {
          // Log any errors.
          console.log(err);
      });
}


function fusionHandleProperties(response) {
    // Handles the response from the webproperties list method.
    if (response.result.items && response.result.items.length) {

        // Get the first Google Analytics account
        var firstAccountId = response.result.items[0].accountId;
        $('#txtstartdate').datepicker('setDate', new Date(response.result.items[0].created));
        // Get the first property ID
        var firstPropertyId = response.result.items[0].id;

        // Query for Views (Profiles).
        fusionQueryProfiles(firstAccountId, firstPropertyId);
    } else {
        console.log('No properties found for this user.');
    }
}

function fusionQueryProfiles(accountId, propertyId) {
    // Get a list of all Views (Profiles) for the first property
    // of the first Account.
    gapi.client.analytics.management.profiles.list({
        'accountId': accountId,
        'webPropertyId': propertyId
    })
    .then(fusionHandleProfiles)
    .then(null, function (err) {
        // Log any errors.
        console.log(err);
    });
}

function fusionHandleProfiles(response) {
    // Handles the response from the profiles list method.
    if (response.result.items && response.result.items.length) {
        // Get the first View (Profile) ID.
        var firstProfileId = response.result.items[0].id;

        // Query the Core Reporting API.
        // fusionQueryCoreReportingApi(firstProfileId);
        g_profileid = firstProfileId;
    } else {
        console.log('No views (profiles) found for this user.');
    }
}

function fusionQueryCoreReportingApi(profileId,p_start,p_end) {
    // Query the Core Reporting API for the number sessions for
    // the past seven days.
    var pastDate = $('#txtstartdate').datepicker('getDate');
    pastDate = new Date(pastDate);
    var finalDate = pastDate.getFullYear() + "-" + pastDate.getMonth() + "-" + pastDate.getDay();
    gapi.client.analytics.data.ga.get({
        'ids': 'ga:' + profileId,
        'start-date': p_start,
        'end-date': p_end,
        'metrics': 'ga:users'
    })
    .then(function (response) {
        var formattedJson = JSON.stringify(response.result, null, 2);
        var getstdate = parseInt(response.result.query["start-date"].split("daysAgo")[0]);
        var monthShort = addDays(new Date(), -getstdate);
        var analyticsobject = new Object();
       
        analyticsobject.label = monthString[monthShort];
        analyticsobject.value = response.result.totalsForAllResults["ga:users"];
        jsonValue.push(analyticsobject);
        //document.getElementById('query-output').value += formattedJson;       
    })
    .then(null, function (err) {
        // Log any errors.
        console.log(err);
    });    
}

function analyticsChart() {
    if ($('#ddlAccountname').children().length == 0) {
        alert("There is no google analytics account in your profile");
        return false;
    }
    $('#loader').show();
    var statictext = "daysAgo";
    if ($('#txtstartdate').val() != "" && $('#txtenddate').val() != "") {
        var startDate = $('#txtstartdate').datepicker('getDate');
        var endDate = $('#txtenddate').datepicker('getDate');
        //startDate = new Date(pastDate);
        var date1 = new Date(startDate);
        var date2 = new Date(endDate);
        var timeDiff = Math.abs(date2.getTime() - date1.getTime());
        var diffDays = Math.ceil(timeDiff / (1000 * 3600 * 24));
        var noofmonths = Math.floor(diffDays / 30);
        if (noofmonths <= 0) {
            fusionQueryCoreReportingApi(g_profileid, 1 + statictext, "today");
        }
        else {
            for (var i = 0; i < noofmonths; i++) {
                fusionQueryCoreReportingApi(g_profileid, diffDays + statictext, (diffDays - 30) + statictext);
                diffDays = diffDays - 30;
            }
            setTimeout(function () {
                loadAnalytics();
            }, 9000);
        }

    }
    else {
        $('#loader').hide();
        alert("Please select the end data");

    }
}
function addDays(theDate, days) {
    return new Date(theDate.getTime() + days * 24 * 60 * 60 * 1000).getMonth();
}

function loadAnalytics() {   
    var excelStores = new FusionCharts({
        type: 'column3d',
        renderAt: 'chart-container1',
        width: '600',
        height: '400',
        dataFormat: 'json',
        dataSource: {
            "chart": {
                "caption": "Analytics Chart",
                "captionPadding": "20px",
                "subCaption": "Month",
                "yAxisName": "Users",
                "xAxisName": "Months",               
                "paletteColors": "#0075c2,#1aaf5d,#f2c500",
                "bgColor": "#ffffff",
                "showBorder": "0",
                "showCanvasBorder": "0",
                "usePlotGradientColor": "0",
                "plotBorderAlpha": "10",
                "placeValuesInside": "1",
                "valueFontColor": "#ffffff",
                "showAxisLines": "1",
                "axisLineAlpha": "25",
                "divLineAlpha": "10",
                "alignCaptionWithCanvas": "0",
                "showAlternateVGridColor": "0",
                "captionFontSize": "14",
                "subcaptionFontSize": "14",
                "subcaptionFontBold": "0",
                "subcaptionFontColor": "#0075c2",
                "toolTipColor": "#ffffff",
                "toolTipBorderThickness": "0",
                "toolTipBgColor": "#000000",
                "toolTipBgAlpha": "80",
                "toolTipBorderRadius": "2",
                "toolTipPadding": "5",
                "labelFont": "Calibri",
                "labelFontSize": "15px",
                "labelFontColor": "#4990e2",
                "labelFontItalic": "1",
                "labelBorderColor": "#aaa",
                "labelBorderRadius": "04",
                "labelBgColor": "#eee",
                "vLine": "true",
                "chartLeftMargin": "80px",
                "chartTopMargin": "180px",
                "yAxisNamePadding": "20px",
                "formatNumberScale": "0",
                "xAxisNameFontColor": "#fff",
                "showHoverEffect": "0",
                "plotHoverEffect": "0",
                "captionPadding": "150px",
                "chartBottomMargin": "10px",
                "canvasBottomMargin": "10px",
                "labelPadding": "20px",
                "logoLink": "http://www.fusioncharts.com",
                "exportEnabled": "1",
                "logoPosition": "BR"
            },

            "data": jsonValue
        }
    })
   .render();
    $('#loader').hide();
}