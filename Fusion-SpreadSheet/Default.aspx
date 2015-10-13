<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Fusion Chart Google Spread Sheet Sample</title>
    <style>
        .btn {
               background: url('images/signin_button.png') no-repeat;
    border: 0;
    width: 320px;
    height: 103px;
    cursor: pointer;
}
        h1 {
            text-align:center;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h1>Google Spread Sheet Sample</h1>
    <div>
        <asp:Button ID="Button1" runat="server" Text="" CssClass="btn" OnClick="Button1_Click" />
    </div>
    </form>
</body>
</html>
