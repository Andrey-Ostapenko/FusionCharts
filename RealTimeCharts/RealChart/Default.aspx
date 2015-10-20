<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RealChart.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FusionCharts Examples - Export Chart</title>

    <script type="text/javascript" src="Scripts/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="FusionCharts/fusioncharts.js"></script>

    <script src="FusionCharts/themes/fusioncharts.theme.fint.js"></script>
    <script type="text/javascript">
        var count = 0;
        var dbresult = [];
        FusionCharts.ready(function () {
            var stockPriceChart = new FusionCharts({
                id: "stockRealTimeChart",
                type: 'realtimeline',
                renderAt: 'chartcontainer',
                width: '500',
                height: '300',
                dataFormat: 'json',
                dataSource: {
                    "chart": {
                        "caption": "Real-time stock price monitor",
                        "subCaption": "Harry's SuperMart",
                        "xAxisName": "Time",
                        "yAxisName": "Stock Price",
                        "numberPrefix": "$",
                        "refreshinterval": "5",
                        "yaxisminvalue": "35",
                        "yaxismaxvalue": "36",
                        "numdisplaysets": "10",
                        "labeldisplay": "rotate",
                        "showValues": "0",
                        "showRealTimeValue": "0",
                        "theme": "fint"
                    },
                    "categories": [{
                        "category": [{
                            "label": "Day Start"
                        }]
                    }],
                    "dataset": [{
                        "data": [{
                            "value": "35.27"
                        }]
                    }]
                },
                "events": {
                    "initialized": function (e) {                      
                        function updateData() {
                            // Get reference to the chart using its ID
                            var chartRef = FusionCharts("stockRealTimeChart");  
                            // Feed it to chart.
                                if (count == 7)
                                    count = 1;
                                else count++;
                                var result = loadFeedData(count);
                                if (result != "") {
                                    console.log("final " + result);
                                    strData = "&label=" + result[0]
                                                   + "&value="
                                                   + result[1];
                                    chartRef.feedData(strData);
                                }
                        }

                        var myVar = setInterval(function () {
                            updateData();
                        }, 5000);
                    }
                }
            })
            .render();
        });

        function loadFeedData(id) {
            var result;
            $.ajax({
                url: '/api/values/' + id + '',
                type: 'GET',
                cache: false,
                data: "",
                success: function (data) {                                 
                    dbresult = data;                
                },
                error: function (a, b, c) {
                    console.log("error");                 
                }
            });         
            return dbresult;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div id="chartcontainer">
        </div>
    </form>
</body>
</html>
