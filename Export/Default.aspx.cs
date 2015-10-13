using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using FusionCharts.Charts;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        // The data to plot the sample dual-y combination chart is contained in the
        // `arrData` 2D array having six rows and three columns.

        // Each row contains the sales data for product A for each quarter.

        // The first column stores the labels to be rendered for each quarter,

        // the second column stores the revenue generated, and

        // the third column stores the no. of units sold in each quarter.

        object[,] arrData = new object[4, 3];

        //Store the labels for each quarter.
        arrData[0, 0] = "Quarter 1";
        arrData[1, 0] = "Quarter 2";
        arrData[2, 0] = "Quarter 3";
        arrData[3, 0] = "Quarter 4";

        //Store revenue data for each quarter.
        arrData[0, 1] = 576000;
        arrData[1, 1] = 448000;
        arrData[2, 1] = 956000;
        arrData[3, 1] = 734000;

        //Store quantity details for each quarter.
        arrData[0, 2] = 576;
        arrData[1, 2] = 448;
        arrData[2, 2] = 956;
        arrData[3, 2] = 734;

        // Use string concatenation to convert the data in the `arrData` array into XML data for the
        // combination chart.

        //Create objects of the `StringBuilder` class are created to

        //store the converted XML strings.


        // Define the the `strXML` object

        //to store the entire chart data as an XML string.
        StringBuilder strXML = new StringBuilder();

        // Define the `strCategories` object
        //to store the labels for each quarter, converted to XML strings.

        StringBuilder strCategories = new StringBuilder();

        //Define the `strDataRev` and the `strDataQty` objects

        // to store the revenue and quantity data, respectively, for product A.
        StringBuilder strDataRev = new StringBuilder();
        StringBuilder strDataQty = new StringBuilder();

        //Initialize the <chart> element. Define the chart-level attributes and
        // append them as strings to the `strXML` object.

        strXML.Append("<chart palette='4' caption='Product A - Sales Details' PYAxisName='Revenue' SYAxisName='Quantity (in Units)' numberPrefix='$' formatNumberScale='0' showValues='0' decimals='0' theme='fint' exportEnabled='1'  exportAtClient= '0' exportAction='Save' exportFileName ='ExportDemo' exportHandler='FusionCharts/ExportHandlers/ASP_Net/FCExporter.aspx' >");

        //Initialize the <categories> element.
        strCategories.Append("<categories>");

        //Initiate the <dataset> elements for the revenue and quantity data.
        strDataRev.Append("<dataset seriesName='Revenue'>");
        strDataQty.Append("<dataset seriesName='Quantity' parentYAxis='S'>");

        //Iterate through the data in the `arrData` array
        for (int i = 0; i < arrData.GetLength(0); i++)
        {
            //Append <category name='...' /> to `strCategories`
            strCategories.AppendFormat("<category name='{0}' />", arrData[i, 0]);
            //AppendAdd <set value='...' /> to both the datasets.
            strDataRev.AppendFormat("<set value='{0}' />", arrData[i, 1]);
            strDataQty.AppendFormat("<set value='{0}' />", arrData[i, 2]);
        }

        //Close the `<categories>` element..
        strCategories.Append("</categories>");

        //Close the `<dataset>` element for the revenue and quantity datasets.
        strDataRev.Append("</dataset>");
        strDataQty.Append("</dataset>");

        //Append the complete chart data converted to a string to the the `strXML` object.
        strXML.Append(strCategories.ToString());
        strXML.Append(strDataRev.ToString());
        strXML.Append(strDataQty.ToString());
        strXML.Append("</chart>");

        // Initialize the chart.
        Chart sales = new Chart("mscombidy2d", "myChart", "600", "350", "xml", strXML.ToString());

        // Render the chart.
        label1.Text = sales.Render();

      
    }
}