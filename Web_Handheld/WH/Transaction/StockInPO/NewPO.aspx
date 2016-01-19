<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="NewPO.aspx.cs" Inherits="WH_Transaction_StockInPO_NewPO" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class = "subheadertext">
                &nbsp;สร้างใบตรวจรับวัตถุดิบใหม่</td> 
        </tr> 
        <tr>
            <td height="150px" valign="top">
                <asp:Panel ID="pnlMain" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="200">
                        <tr height="5">
                            <td align="right">
                            </td>
                        </tr>
                        <tr height="20">
                            <td>
                                &nbsp;<b>เลขที่ใบส่งของ&nbsp;:</b>&nbsp;<asp:TextBox ID="txtInvNo" runat="server" CssClass="zTextbox" Width="110px"></asp:TextBox></td> 
                        </tr>
                        <tr height="20">
                            <td>
                                &nbsp;<b>ผู้จำหน่าย&nbsp;:</b>&nbsp;<asp:DropDownList ID="cmbSupplier" runat="server" CssClass="zCombobox" Width="130px">
                                </asp:DropDownList>
                            </td> 
                        </tr>
                    </table> 
                </asp:Panel>
                <asp:Panel ID="pnlError" runat="server" Visible="false">
                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td height="75">
                            </td>
                        </tr>
                        <tr>
                            <td class="messageLayer" valign="bottom">
                                <table border="0" cellpadding="0" cellspacing="0" width="220">
                                    <tr height="3">
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                    <tr height="18">
                                        <td align="center" valign="top" width="50">
                                            <img src="../../../Images/msg_Error.gif" /></td>
                                        <td>
                                            <b>
                                                <asp:Label ID="lblError" runat="server"></asp:Label></b>
                                        </td>
                                    </tr>
                                    <tr height="3">
                                        <td colspan="2">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="บันทึก" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/></td>
                        <td align="right"><asp:LinkButton ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="hButton" Width="80px" OnClick="btnCancel_Click"/>&nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
</asp:Content>