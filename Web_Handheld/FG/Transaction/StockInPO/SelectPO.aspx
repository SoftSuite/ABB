<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="SelectPO.aspx.cs" Inherits="FG_Transaction_StockInPO_SelectPO" Title="รับสินค้าจากผู้จำหน่าย" %>

<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class = "toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnHelpShow="false" BtnNewShow="false"
                    BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"
                    BtnViewShow="true" OnViewClick="ViewClick" />
            </td> 
        </tr> 
        <tr>
            <td height="150px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="200">
                        <tr height="5">
                            <td style="width: 94px">
                            </td>
                            <td>
                            </td> 
                        </tr>
                        <tr height="20">
                            <td style="width: 94px">
                                &nbsp;<b>เลขที่ใบตรวจรับ&nbsp;:</b>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblCode" runat="server"></asp:Label>
                            </td> 
                        </tr>
                        <tr height="20">
                            <td style="width: 94px">
                                &nbsp;<b>เลขที่ใบส่งของ&nbsp;:</b>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblInvNo" runat="server"></asp:Label>
                            </td> 
                        </tr>
                        <tr height="20">
                            <td style="width: 94px">
                                &nbsp;<b>ผู้จำหน่าย&nbsp;:</b>&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                            </td> 
                        </tr>
                    </table>
                    <hr /><br />
                    <table border="0" cellpadding="0" cellspacing="0" width="200">
                        <tr height="20">
                            <td style="width: 95px">
                                &nbsp;<b>เลขที่ใบสั่งซื้อ&nbsp;:</b>&nbsp;
                            </td>
                            <td style="width: 121px">
                                <asp:DropDownList ID="cmbPR" runat="server" CssClass="zCombobox" Width="110px">
                                </asp:DropDownList>
                            </td> 
                        </tr>
                    </table>
                    <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></asp:Panel>
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