<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberLogin.aspx.cs" Inherits="MemberLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>เข้าสู่ระบบ</title>
    <link href="Template/BaseStyle.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="<%Response.Write(ABB.Data.Constz.HomeFolder);%>Template/BaseScript.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table id="tblLogonMain" border="1" cellpadding="2" cellspacing="0"
            height="220" width="220px" bordercolor="99CC99">
            <tr>
                <td align="center" style="height:20px" class="header">
                    Abhaibhubej&nbsp; HHT System</td>
            </tr>
            <tr>
                <td align="center" valign="top" height="200px">
                    <asp:Login ID="ctlLogin" runat="server"
                        BorderPadding="4" BorderStyle="Solid" DisplayRememberMe="False" OnAuthenticate="ctlLogin_Authenticate" OnLoggedIn="ctlLogin_LoggedIn">
                        <LayoutTemplate>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td>
                                        <table border="0" cellspacing="0" cellpadding="0" style="width: 220px; border-right: #00cc33 1px solid; border-top: #00cc33 1px solid; border-left: #00cc33 1px solid; border-bottom: #00cc33 1px solid;" bgcolor="#d6ffd6">
                                            <tr>
                                                <td style="width: 75px; height:20px"></td> 
                                                <td style="width: 145px; height:20px"></td> 
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 75px; height: 20px"><asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="ctlLogin">*</asp:RequiredFieldValidator>
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">ชื่อผู้ใช้</asp:Label></td>
                                                <td style="width: 145px">
                                                    <asp:TextBox ID="UserName" runat="server" CssClass="zTextbox" Width="115px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 75px; height: 20px"><asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctlLogin">*</asp:RequiredFieldValidator>
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">รหัสผ่าน</asp:Label></td>
                                                <td style="width: 145px">
                                                    <asp:TextBox ID="Password" runat="server" CssClass="zTextbox" TextMode="Password"
                                                        Width="115px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 75px; height: 20px">
                                                    <asp:Label ID="WarehouseDropDownList" runat="server" AssociatedControlID="Warehouse">คลังสินค้า</asp:Label></td>
                                                <td style="width: 145px">
                                                    <asp:DropDownList ID="Warehouse" runat="server" CssClass="zComboBox" Width="120px"></asp:DropDownList><asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Warehouse"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctlLogin">*</asp:RequiredFieldValidator></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: red; height:20px">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal><hr />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" valign="top" colspan="2" style="height:20">
                                                    <asp:LinkButton ID="LoginButton" runat="server" CommandName="Login" Text="ลงชื่อเข้าใช้" ValidationGroup="ctlLogin" CssClass="hButton" />&nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td> 
                                </tr>
                            </table> 
                        </LayoutTemplate>
                    </asp:Login>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>