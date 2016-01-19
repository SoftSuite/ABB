<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="FG_Transaction_StockCheck_ProductDetail" Title="ตรวจนับสินค้า" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td height="5">
                </td> 
        </tr> 
        <tr>
            <td height="170" valign="top">
                <asp:Panel ID="Panel1" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="205">
                        <tr height="20">
                            <td width="5">
                            </td> 
                            <td style="width: 85px">
                                <b>Batch No</b></td> 
                            <td style="width: 115px">
                                <asp:Label ID="lblBatchNo" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5" style="height: 20px">
                            </td>
                            <td style="width: 85px; height: 20px;">
                                <b>วันที่</b></td>
                            <td style="width: 115px; height: 20px;">
                                <asp:Label ID="lblCheckDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <strong>Location</strong></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblWarehouseName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>โซน</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblZoneName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <strong>สินค้า</strong></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblProductName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>Lot No</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblLotNo" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>จำนวนที่นับได้</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblQty" runat="server"></asp:Label></td>
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
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="ตกลง" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/></td>
                        <td align="right">
                            &nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
                    <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
</asp:Content>