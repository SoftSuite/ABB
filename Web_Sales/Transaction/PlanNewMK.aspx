<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PlanNewMK.aspx.cs" Inherits="Transaction_PlanNewMK" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>สร้างแผนการตลาด</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div align="center">
        <table border="0" width="100%" height="100%" cellpadding = "0" cellspacing="0">
            <tr height="10px">
                <td height="10px"></td>
            </tr> 
            <tr>
                <td style="height:100%">
                    <table border="0" width="400" cellpadding = "0" cellspacing="0" class="searchTable">
                        <tr height="10px">
                            <td></td>
                            <td></td> 
                        </tr> 
                        <tr height="25px">
                            <td width="80px" align="right">
                                ปี พ.ศ. :&nbsp;
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cmbYear" runat="server" CssClass="zComboBox" Width="80px">
                                </asp:DropDownList></td> 
                        </tr> 
                        <tr height="25px">
                            <td align="right">
                                คำอธิบาย :&nbsp;
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtDescription" runat="server" CssClass="zTextbox" Width="300px"></asp:TextBox></td> 
                        </tr> 
                        <tr height="5px">
                            <td></td>
                            <td></td> 
                        </tr> 
                        <tr height="25px">
                            <td></td>
                            <td align="left">
                                <asp:Button ID="btnSave" runat="server" CssClass="zButton" Text="สร้างแผน" Width="80px" />
                                <asp:Button ID="btnCancel" runat="server" CssClass="zButton" Text="ยกเลิก" Width="80px" /></td> 
                        </tr> 
                        <tr height="10px">
                            <td></td>
                            <td></td> 
                        </tr> 
                    </table>
                </td> 
            </tr>
            <tr height="10px">
                <td></td>
            </tr> 
         </table>
    </div>
    </form>
</body>
</html>
