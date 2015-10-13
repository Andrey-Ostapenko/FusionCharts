<%@ Page Language="C#" AutoEventWireup="true" CodeFile="oauth2callback.aspx.cs" Inherits="oauth2callback" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fusion Chart Google Spread Sheet Sample</title>
    <script src="Scripts/fusioncharts.js"></script>
    <script src="Scripts/fusioncharts.charts.js"></script>
    <style>       

        h1 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
         <h1>Google Spread Sheet Sample</h1>
        <div>
            Please select a spread Sheet:
            <asp:DropDownList ID="ddlexcellst" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlexcellst_SelectedIndexChanged"></asp:DropDownList>         
            <asp:Label ID="Label2" runat="server" Text="Please select a Spread Sheet" ForeColor="Red" Visible="False"></asp:Label>
        </div>
        <div>
            <asp:Table ID="Table1" runat="server"></asp:Table>
        </div>
        <div>
              <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </div>

    </form>
</body>
</html>
