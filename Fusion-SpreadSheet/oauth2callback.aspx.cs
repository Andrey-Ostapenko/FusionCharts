using Google.GData.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Google.GData.Spreadsheets;
using FusionCharts.Charts;
using Newtonsoft.Json;
using System.Text;
using System.Web.Script.Serialization;

public partial class oauth2callback : System.Web.UI.Page
{
    // OAuth2Parameters parameters;// = new OAuth2Parameters();
    static string refresh_token = string.Empty;
    static string access_token = string.Empty;


    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {


            if (!IsPostBack)
            {
               

                OAuth2Parameters parameters_lst = oauthcredentials();
                GOAuth2RequestFactory requestFactory =
                  new GOAuth2RequestFactory(null, "Fusion-SpreadSheet", parameters_lst);
                SpreadsheetsService service = new SpreadsheetsService("Fusion-SpreadSheet");
                service.RequestFactory = requestFactory;

                SpreadsheetQuery query = new SpreadsheetQuery();


                // Make a request to the API and get all spreadsheets.
                SpreadsheetFeed feed = service.Query(query);

                // Iterate through all of the spreadsheets returned
                ddlexcellst.Items.Add("------------------- Select------------------");
                foreach (SpreadsheetEntry entry in feed.Entries)
                {
                    // Print the title of this spreadsheet to the screen

                    //Response.Write(entry.Title.Text);
                    ddlexcellst.Items.Add(entry.Title.Text);
                }



            }
        }
        catch (Exception)
        {

            Response.Redirect("Default.aspx");
        }

    }
    protected void ddlexcellst_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (ddlexcellst.SelectedIndex == 0) {
                Label2.Visible = true;
            }
            else 
            {
                Label2.Visible = false;
                OAuth2Parameters parameters_lst = oauthcredentials();
                GOAuth2RequestFactory requestFactory =
                     new GOAuth2RequestFactory(null, "Fusion-SpreadSheet", parameters_lst);
                SpreadsheetsService service = new SpreadsheetsService("Fusion-SpreadSheet");
                service.RequestFactory = requestFactory;


                service.RequestFactory = requestFactory;

                // TODO: Authorize the service object for a specific user (see other sections)

                // Instantiate a SpreadsheetQuery object to retrieve spreadsheets.
                SpreadsheetQuery query = new SpreadsheetQuery();

                // Make a request to the API and get all spreadsheets.
                SpreadsheetFeed feed = service.Query(query);

                if (feed.Entries.Count == 0)
                {
                    // TODO: There were no spreadsheets, act accordingly.
                }

                // TODO: Choose a spreadsheet more intelligently based on your
                // app's needs.
                SpreadsheetEntry spreadsheet = (SpreadsheetEntry)feed.Entries[Convert.ToInt32(ddlexcellst.SelectedIndex) - 1];
                //Response.Write(spreadsheet.Title.Text + "\n");

                // Get the first worksheet of the first spreadsheet.
                // TODO: Choose a worksheet more intelligently based on your
                // app's needs.
                WorksheetFeed wsFeed = spreadsheet.Worksheets;
                WorksheetEntry worksheet = (WorksheetEntry)wsFeed.Entries[0];

                // Define the URL to request the list feed of the worksheet.
                AtomLink listFeedLink = worksheet.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

                // Fetch the list feed of the worksheet.

                ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
                listQuery.StartIndex = 0;
                ListFeed listFeed = service.Query(listQuery);

                // Iterate through each row, printing its cell values.

                List<category> chart_categories = new List<category>();
                ChartModels result = new ChartModels();
                categories final_categories = new categories();
                List<categories> list_categories = new List<categories>();
                List<dataset> collect_dataset = new List<dataset>();
                foreach (ListEntry row in listFeed.Entries)
                {
                    // Print the first column's cell value
                    TableRow tr = new TableRow();
                    //  Response.Write(row.Title.Text + "\n");
                    // Iterate over the remaining columns, and print each cell value
                    dataset final_dataset = new dataset();
                    List<data> collect_data = new List<data>();
                    foreach (ListEntry.Custom element in row.Elements)
                    {
                        if (row.Title.Text == "Row: 2")
                        {
                            category chart_category = new category();
                            chart_category.label = element.Value.ToString();
                            chart_categories.Add(chart_category);
                        }
                        else
                        {
                            data data_value = new data();
                            final_dataset.seriesname = row.Title.Text;
                            int n;
                            bool isNumeric = int.TryParse(element.Value, out n);
                            if (isNumeric)
                            {
                                data_value.value = element.Value.ToString();
                                collect_data.Add(data_value);
                            }
                        }
                        // Response.Write(element.Value + "\n");

                        TableCell tc = new TableCell();
                        if (row.Title.Text == "Row: 2")
                        {
                            tc.Text = "";
                        }
                        else
                            tc.Text = element.Value;
                        tr.Cells.Add(tc);
                    }
                    Table1.Rows.Add(tr);
                    if (collect_data.Count != 0)
                    {
                        final_dataset.data = collect_data;
                        collect_dataset.Add(final_dataset);
                    }
                }
                final_categories.category = chart_categories;
                result.dataset = collect_dataset;
                list_categories.Add(final_categories);
                result.categories = list_categories;

                JavaScriptSerializer js = new JavaScriptSerializer();
                string res = js.Serialize(result);
                string chartjson = JsonConvert.SerializeObject(result.categories);

                StringBuilder strJson = new StringBuilder();

                strJson.Append("{" +
                        "'chart': {" +
                              "'caption': 'Quarterly revenue'," +
                              "'subCaption':'Last year'," +
                              "'xAxisName':'Quarter (Click to drill down)'," +
                              "'subcaptionFontColor':'#0075c2'," +
                              "'numberPrefix': '$'," +
                              "'formatNumberScale': '1'," +
                              "'placeValuesInside': '1'," +
                              "'decimals': '0'" +
                                   "},");
                strJson.Append("'categories':");

                strJson.Append(chartjson);
                strJson.Append(",");
                strJson.Append("'dataset':");
                string chartdatajson = JsonConvert.SerializeObject(result.dataset);
                strJson.Append(chartdatajson);
                strJson.Append("}");


                // Initialize the chart.
                Chart sales = new Chart("mscolumn3d", "myChart", "600", "350", "json", strJson.ToString());

                // Render the chart.
                Label1.Text = sales.Render();
            }
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
        }

    }

    public OAuth2Parameters oauthcredentials()
    {
        try
        {
            string CLIENT_ID = "641157605077-rr2l9ma85ps6n5grp031js3pccehfv5u.apps.googleusercontent.com";
            string CLIENT_SECRET = "Gq-ekOvi9grFi9K3rznIpoT9";
            string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";
            string REDIRECT_URI = "http://localhost:1918/oauth2callback";
            string TOKEN_TYPE = "Bearer";
            OAuth2Parameters parameters = new OAuth2Parameters();
            parameters.ClientId = CLIENT_ID;
            parameters.ClientSecret = CLIENT_SECRET;
            parameters.Scope = SCOPE;
            parameters.RedirectUri = REDIRECT_URI;
            parameters.AccessType = "offline";


            parameters.TokenType = TOKEN_TYPE;

            parameters.AccessCode = Request.QueryString["code"];

            if (access_token == string.Empty)
            {
                OAuthUtil.GetAccessToken(parameters);
                string accessToken = parameters.AccessToken;
                string refreshToken = parameters.RefreshToken;
                access_token = accessToken;
                refresh_token = refreshToken;
            }
            else
            {
                parameters.AccessToken = access_token;
                parameters.RefreshToken = refresh_token;
            }
            return parameters;
        }
        catch (Exception)
        {
            Response.Redirect("Default.aspx");
            return new OAuth2Parameters();
        }

    }

    public class ChartModels
    {
        public List<categories> categories { get; set; }
        public List<dataset> dataset { get; set; }
    }
    public class data
    {
        public string value { get; set; }
    }
    public class category
    {
        public string label { get; set; }
    }
    public class categories
    {
        public List<category> category { get; set; }
    }
    public class dataset
    {
        public string seriesname { get; set; }
        public List<data> data { get; set; }
    }
}