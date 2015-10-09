<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Ajax.aspx.cs" Inherits="Ajax" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server" id="head1">
    <title>Update Panel Sample</title>
    <script src="fusioncharts/jquery-1.11.0.js"></script>
    <script type="text/javascript" src="fusioncharts/fusioncharts.js"></script>
    <script type="text/javascript">     
        function chartMethod(p_chartid, p_charttype, p_width, p_height, p_datasource, p_renderid) {
            // Recreate render div and append  fusion script to body tag;            
            $('body').append('<script type="text/javascript">FusionCharts && FusionCharts.ready(function () {if (FusionCharts("' + p_chartid + '") ) FusionCharts("' + p_chartid + '").dispose();    var chart_myChart = new FusionCharts({        "id" : "' + p_chartid + '",         "width" : "' + p_width + '",         "dataSource" : "' + p_datasource + '",         "type" : "' + p_charttype + '",         "height" : "' + p_height + '",         "renderAt" : "' + p_renderid + '",         "dataFormat" : "jsonurl",     }).render();});');
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <label id="hfHiddenFieldID"></label>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>

        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional" 
                ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Post Back" />
                <br />
                <asp:Label ID="Label1" runat="server" Text="Label1"></asp:Label>
            </ContentTemplate>
            <Triggers>
     <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click"/>
</Triggers>
        </asp:UpdatePanel>      
    </form>
</body>
</html>
