<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MemberLogin.aspx.cs" Inherits="MemberLogin" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>เข้าสู่ระบบ</title>
    <link href="Template/BaseStyle.css" rel="stylesheet" type="text/css" />
</head>
<body bottommargin="0" leftmargin="0" rightmargin="0" topmargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" /> 
        <table width="100%" cellspacing="0" cellpadding="0" border="0" height="100%">
            <tr height="100%">
                <td align="center" >
                    <asp:Login ID="ctlLogin" runat="server"
                        BorderPadding="4" BorderStyle="Solid" DisplayRememberMe="False" OnAuthenticate="ctlLogin_Authenticate" OnLoggedIn="ctlLogin_LoggedIn">
                        <LayoutTemplate>
                            <table border="0" cellspacing="0" cellpadding="0">
                                <tr height="45" >
                                    <td background="Images/head_Loginbg.png">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table border="0" cellspacing="0" cellpadding="0" style="width: 350px; border-right: #00cc33 1px solid; border-top: #00cc33 1px solid; border-left: #00cc33 1px solid; border-bottom: #00cc33 1px solid; background-color:#D6FFD6">
                                            <tr height="20">
                                                <td rowspan="7" align="center" valign="top" style="width: 70px"><br /><img src="Images/key.gif" /></td> 
                                                <td></td> 
                                                <td></td> 
                                            </tr>
                                            <tr>
                                                <td align="center" class="zHidden" colspan="2" valign="middle">
                                                    ลงชื่อเข้าใช้</td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" style="width: 80px; height: 25px">
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">ชื่อผู้ใช้ :&nbsp;&nbsp;</asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server" CssClass="zTextBox" Width="160px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="ctlLogin">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" style="width: 80px; height: 25px">
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">รหัสผ่าน :&nbsp;&nbsp;</asp:Label></td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" CssClass="zTextBox" TextMode="Password"
                                                        Width="160px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctlLogin">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="right" style="width: 80px; height: 25px">
                                                    <asp:Label ID="WarehouseDropDownList" runat="server" AssociatedControlID="Warehouse">คลังสินค้า :&nbsp;&nbsp;</asp:Label></td>
                                                <td>
                                                    <asp:DropDownList ID="Warehouse" runat="server" CssClass="zComboBox" Width="160px"></asp:DropDownList> 
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Warehouse"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="ctlLogin">*</asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr height="25">
                                                <td align="center" colspan="2" style="color: red">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal><hr />
                                                </td>
                                            </tr>
                                            <tr height="30">
                                                <td align="right" valign="top" colspan="2">
                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="ลงชื่อเข้าใช้" ValidationGroup="ctlLogin" CssClass="loginbutton" />&nbsp;
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
    </form>
</body>
</html>