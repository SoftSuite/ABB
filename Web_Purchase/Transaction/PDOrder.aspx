<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PDOrder.aspx.cs" Inherits="Transaction_PDOrder" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>

<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ยืนยันข้อมูลการส่งใบสั่งซื้อให้ผู้จำหน่าย</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr height="5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr height="25px">
                <td width="5px">
                </td>
                <td class="headtext">
                    &nbsp;ยืนยันข้อมูลการส่งใบสั่งซื้อให้ผู้จำหน่าย</td>
            </tr> 
            <tr height="10px">
                <td width="5px">
                </td>
                <td>
                    <uc1:ToolbarControl ID="ToolbarControl1" runat="server" BtnBackShow="false" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" 
                    OnSaveClick="SaveClick" OnCancelClick="CancelClick"/>
                </td>
            </tr> 
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" height="20px">
                                &nbsp;</td> 
                        </tr>
                        <tr height="5px">
                            <td width="10px">
                            </td> 
                            <td style="width: 108px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td style="width: 31px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td width="40px">
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 108px">
                                เลขที่ใบสั่งซื้อ</td>  
                            <td width="150px">
                                 <asp:Label ID="lblCode" runat="server" CssClass="zTextbox-View" Height="21px"
                                            Width="85%"></asp:Label></td> 
                            <td style="width: 31px">
                             <asp:TextBox id="txtLOID" runat="server" CssClass="zHidden" Width="9px">
                                        </asp:TextBox></td>  
                            <td width="150px">
                             </td> 
                            <td>
                            </td>  
                        </tr>
                        <tr height="25">
                            <td width="10" style="height: 25px">
                            </td>
                            <td style="width: 108px; height: 25px;">
                                วันที่ส่งใบสั่งซื้อ</td>
                            <td width="150" style="height: 25px">
                            <uc2:DatePickerControl ID="DatePickerControl1" runat="server" /></td>
                            <td style="width: 31px; height: 25px;">
                            </td>
                            <td width="150" style="height: 25px">
                            </td>
                            <td style="height: 25px">
                            </td>
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 108px">
                                วิธีการส่งใบสั่งซื้อ</td>  
                            <td width="150px">
                                 <asp:DropDownList ID="cmbSendPO" runat="server" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="cmbSendPO_SelectedIndexChanged">
                                            <asp:ListItem Value="0">เลือก</asp:ListItem>
                                            <asp:ListItem Value="FA">ส่งแฟกซ์</asp:ListItem>
                                            <asp:ListItem Value="TE">โทรศัพท์</asp:ListItem>
                                            <asp:ListItem Value="SA">ไปซื้อเอง</asp:ListItem>
                                            <asp:ListItem Value="OT">อื่นๆ</asp:ListItem>
                                        </asp:DropDownList></td> 
                            <td style="width: 31px">
                                <asp:Label id="lblRemark" runat="server" CssClass="zRemark" Text="*" Width="2px"></asp:Label></td>  
                            <td width="150px">
                                <asp:TextBox ID="txtSendPO" runat="server"   Enabled="false"></asp:TextBox></td> 
                            <td>
                                </td>  
                        </tr>
                        <tr height="25">
                            <td width="10">
                            </td>
                            <td style="width: 108px">
                                เลขที่อ้างอิง</td>
                            <td width="150">
                            <asp:TextBox ID="txtRefSuppcode" runat="server" Width="126px"></asp:TextBox></td>
                            <td style="width: 31px">
                            </td>
                            <td width="150">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr height="5px">
                            <td width="10px">
                            </td> 
                            <td style="width: 108px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td style="width: 31px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                    </table>
                </td>
            </tr> 
        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
