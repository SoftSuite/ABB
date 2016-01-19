<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <link href="Template/BaseStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table border="0" cellspacing="0" cellpadding = "0" width="100%">
            <tr height="25px">
                <td>
                    <asp:TextBox ID="txtKey" runat="server" CssClass="zTextbox" Width="300px"></asp:TextBox>
                    <asp:Button ID="btnEncrypt" runat="server" Text="เข้ารหัส" OnClick="btnEncrypt_Click" />
                    <asp:Button ID="btnDecrypt" runat="server" Text="ถอดรหัส" OnClick="btnDecrypt_Click" /></td>
            </tr> 
            <tr height="25px">
                <td>
                    <asp:TextBox ID="txtResult" runat="server" CssClass="zTextbox-view" Width="300px" ReadOnly="True"></asp:TextBox></td>
            </tr> 
        </table> 
        <div>
        </div>
    </form>
</body>
</html>
