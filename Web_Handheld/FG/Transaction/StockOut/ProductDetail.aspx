<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="FG_Transaction_StockOut_ProductDetail" Title="�ԡ�Թ���" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td height="5">
                &nbsp;</td> 
        </tr> 
        <tr>
            <td height="170px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="205">
                        <tr height="20">
                            <td width="5">
                            </td> 
                            <td style="width: 85px">
                                <b>�Ţ�����ԡ</b></td> 
                            <td style="width: 115px">
                                <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>�Ţ�����ԡ</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblReqCode" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <strong>������</strong></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblDocName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>�Թ���</b></td>
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
                                <b>�ӹǹ</b></td>
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
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="��ŧ" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/></td>
                        <td align="right">
                            &nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
                    <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
</asp:Content>