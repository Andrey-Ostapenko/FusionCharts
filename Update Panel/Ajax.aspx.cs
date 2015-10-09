using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FusionCharts.Charts;

public partial class Ajax : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Chart sales = new Chart();
            sales.SetChartParameter(Chart.ChartParameter.chartId, "myChart");
            string param = sales.GetChartParameter(Chart.ChartParameter.chartId);

            // Setting chart type to Column 3D chart
            sales.SetChartParameter(Chart.ChartParameter.chartType, "column3d");

            // Setting chart width to 500px
            sales.SetChartParameter(Chart.ChartParameter.chartWidth, "600");

            // Setting chart height to 400px
            sales.SetChartParameter(Chart.ChartParameter.chartHeight, "350");

            // Setting chart data as XML URL
            sales.SetData("Data/Data.aspx", Chart.DataFormat.xmlurl);
         
            Label1.Text = sales.Render();

        }       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        chartproperty chprop = new chartproperty();
        chprop.chartId = "myChart1";
        chprop.chartType = "column3d";
        chprop.chartWidth = "600";
        chprop.chartHeight = "350";
        chprop.xmlurl = "Data/maindata.json";
        chprop.renderId = "Label1";
        List<chartproperty> listofchart = new List<chartproperty>();
        listofchart.Add(chprop);       
        foreach (var item in listofchart.Select((x, i) => new { Value = x, Index = i }))
        {
            // calling javacript function passing all chart properties.
           ScriptManager.RegisterStartupScript(UpdatePanel1, UpdatePanel1.GetType(), "codeMethod" + item.Index + "", "chartMethod('" + item.Value.chartId + "','" + item.Value.chartType + "','" + item.Value.chartWidth + "','" + item.Value.chartHeight + "','" + item.Value.xmlurl + "','" + item.Value.renderId + "');", true);           
        }        
    }  
    public class chartproperty  // Chart Properties
    {
        public string chartId { get; set; }
        public string chartType { get; set; }
        public string xmlurl { get; set; }
        public string chartWidth { get; set; }
        public string chartHeight { get; set; }
        public string renderId { get; set; }
    }
}