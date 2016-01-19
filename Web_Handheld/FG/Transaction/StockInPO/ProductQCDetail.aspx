<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductQCDetail.aspx.cs" Inherits="FG_Transaction_StockInPO_ProductQCDetail" Title="รับสินค้าจากผู้จำหน่าย" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td height="5"></td> 
        </tr> 
        <tr>
            <td height="170px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="205">
                        <tr height="20">
                            <td width="5">
                            </td> 
                            <td style="width: 85px">
                                <b>เลขที่ใบตรวจรับ</b></td> 
                            <td style="width: 115px">
                                <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <strong>วันที่</strong></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblReceiveDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>สินค้า</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblProductName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>จำนวนส่งตรวจ</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblQCQty" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <hr />
                    <table border="0" cellpadding="0" cellspacing="0" width="205">
                        <tr height="20">
                            <td width="5">
                            </td> 
                            <td style="width: 85px">
                                <b>เลขที่ใบแจ้ง QC</b></td> 
                            <td style="width: 115px">
                                <asp:Label ID="lblQCCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>จำนวนรับ</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblQty" runat="server"></asp:Label></td>
                        </tr>
                    </table></asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="ตกลง" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/>
            </td> 
        </tr> 
    </table>
                    <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
</asp:Content>