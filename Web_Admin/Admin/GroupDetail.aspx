<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="GroupDetail.aspx.cs" Inherits="Admin_GroupDetail" Title="สิทธิ์การใช้งานในระบบ" %>
<%@ Register Src="../Controls/Z2BoxControl.ascx" TagName="Z2BoxControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarCtl.ascx" TagName="ToolbarCtl" TagPrefix="uc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
<table cellspacing="0" cellpadding="0" border="0" width="100%">
    <tr>
        <td style="height: 27px" class="headtext">
            &nbsp;รายละเอียดสิทธิ์การใช้งานของผู้ใช้</td>
    </tr>
    <tr>
        <td class="toolbarplace">
            <uc1:ToolbarCtl ID="ToolbarCtl1" runat="server" BtnBackShow="true" BtnCancelShow="true" BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="false" OnBackClick="BackClick" onSaveClick="SaveClick" OnCancelClick="CancelClick"/>
        </td>
    </tr>
    <tr>
        <td style="height: 15px">
            <asp:TextBox ID="txtGroupID" runat="server" Visible="False"></asp:TextBox></td>
    </tr>
    <tr>
        <td style="padding-left:10px; padding-right:10px; padding-bottom:10px; padding-top:10px">        
            <asp:Label ID="lblRoleError" runat="server" ForeColor="Red" Text="ใส่ชื่อกลุ่ม และกดบันทึก เพื่อแก้ไขสิทธิ์การใช้งาน"
                Visible="False"></asp:Label>
        <table cellspacing="0" cellpadding="0" border="0" width="650" bgcolor="#eeeeee">
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 15px; text-align: right">
                </td>
                <td style="height: 15px">
                </td>
            </tr>
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 23px; text-align: right">
                    รหัสเข้าใช้งาน :</td>
                <td>
                    <asp:TextBox ID="txtGroupName" runat="server" CssClass="zTextbox" Width="300px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="padding-right: 5px; width: 120px; height: 15px; text-align: right">
                </td>
                <td style="height: 15px">
                </td>
            </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td style="height: 15px;padding-left:10px">
            <asp:Panel ID="pnlRole" runat="server"  Width="100%">
                <table cellspacing="0" cellpadding="0" border="0" width="650">
                    <tr>
                        <td style="height:15px"></td>
                    </tr>
                    <tr>
                        <td style="height: 15px">
                        <hr size="1" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#eeeeee" style="padding-top:10px; padding-bottom:10px">
                            <asp:Panel ID="pnlMenu" runat="server"  Width="100%">
                                <uc2:Z2BoxControl ID="z2Menu" runat="server" txtDestHead="เมนูที่มีสิทธิ์เข้าใช้งาน" txtSourceHead="เมนูทั้งหมด" />
                           </asp:Panel>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
</asp:Content>

