<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductDetail.aspx.cs" Inherits="FG_Transaction_StockInPD_ProductDetail" Title="�Ѻ�Թ��Ҩҡ���¼�Ե" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td height="5"></td> 
        </tr> 
        <tr>
            <td height="170px" valign="top">
                <asp:Panel ID="Panel1" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0" width="205">
                        <tr height="20">
                            <td width="5">
                            </td> 
                            <td style="width: 75px">
                                <b>�Ţ���㺹���</b></td> 
                            <td width="125">
                                <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�Ţ����ü�Ե</b></td>
                            <td width="125">
                                <asp:Label ID="lblLotNo" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�ѹ���</b></td>
                            <td width="125">
                                <asp:Label ID="lblDate" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�Թ���</b></td>
                            <td width="125">
                                <asp:Label ID="lblProductName" runat="server"></asp:Label></td>
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 75px">
                                <b>�ӹǹ</b></td>
                            <td width="125">
                                <asp:Label ID="lblQty" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                <asp:LinkButton ID="btnSave" runat="server" Text="��ŧ" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/>
            </td> 
        </tr> 
    </table>
</asp:Content>