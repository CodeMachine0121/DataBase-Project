<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Select.aspx.cs" Inherits="Select" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style type="text/css">
        .auto-style1 {
            margin-top: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <h2>選課系統</h2>
        <h3> 輸入課程編號: </h3>
        <div>
            <asp:Label ID="text" runat="server" Text="Label"></asp:Label>

        </div>
        <asp:TextBox ID="C_ID" runat="server" placeholder="Course ID" CssClass="auto-style1"></asp:TextBox> <br>
        <asp:Button ID="submit" runat="server" Text="Submit" />
        
        <asp:Table ID="Table1" runat="server" Border=9>
            
        </asp:Table>
        
        <br>
        
    </form>
    
</body>
</html>
