<%@ Page Language="C#" AutoEventWireup="true" CodeFile="generate.aspx.cs" Inherits="GenDAL_generate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Panel ID="Panel1" runat="server"  BackColor="#FFC0C0" ForeColor="Blue" Width="417px">
            <table>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Oracle Database" Font-Size="Medium" ></asp:Label></td>
                     <td></td>
                    <td style="width: 3px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Data Source"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtDataSource" runat="server" Width="140px">apb</asp:TextBox></td>
             <td></td>
                    <td style="width: 3px"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="UserID"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtUserid" runat="server" Width="140px">apb</asp:TextBox></td>
           <td></td>
                    <td style="width: 3px"></td>
                </tr>
                                <tr>
                    <td>
                        <asp:Label ID="Label4" runat="server" Text="Password"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtPassword" runat="server" CssClass="zTextbox" Width="140px">gvru[u</asp:TextBox></td>
           <td></td>
                    <td style="width: 3px"></td>
                </tr>
                <tr><td>
                    <asp:Label ID="Label5" runat="server" Text="Table"></asp:Label></td><td>
                    <asp:TextBox ID="txtTable" runat="server" CssClass="zTextbox" Width="140px"></asp:TextBox></td>
                     <td>
                    </td>
                    <td style="width: 3px">
                    </td>
                    </tr>
                <tr>
                    <td style="height: 26px">
                        <asp:Label ID="Label6" runat="server" Text="NameSpace"></asp:Label></td>
                    <td style="height: 26px">
                        <asp:TextBox ID="txtNameSpace" runat="server" CssClass="zTextbox" Width="140px">ABB.DAL</asp:TextBox></td>
                    <td style="height: 26px">
                    </td>
                    <td style="height: 26px; width: 3px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label7" runat="server" Text="Class"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtClass" runat="server" CssClass="zTextbox" Width="140px"></asp:TextBox></td>
                    <td>
                    <asp:Button ID="btnGen" runat="server" Text="Generate" Width="74px" OnClick="btnGen_Click" /></td>
                    <td style="width: 3px">
                    </td>
                </tr>
            </table>
            </asp:Panel>
        <br />
        <asp:Panel ID="Panel2" runat="server" Height="261px" Width="596px">
            <asp:TextBox ID="txtShowDAL" runat="server" Height="410px" TextMode="MultiLine" Width="907px"> </asp:TextBox></asp:Panel>
    
    </div>
    </form>
</body>
</html>
