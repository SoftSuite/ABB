<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductQCDetail.aspx.cs" Inherits="FG_Transaction_StockInPO_ProductQCDetail" Title="�Ѻ�Թ��Ҩҡ����˹���" %>
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
                                <b>�Ţ���㺵�Ǩ�Ѻ</b></td> 
                            <td style="width: 115px">
                                <asp:Label ID="lblCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <strong>�ѹ���</strong></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblReceiveDate" runat="server"></asp:Label></td>
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
                                <b>�ӹǹ�觵�Ǩ</b></td>
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
                                <b>�Ţ������ QC</b></td> 
                            <td style="width: 115px">
                                <asp:Label ID="lblQCCode" runat="server"></asp:Label></td> 
                        </tr>
                        <tr height="20">
                            <td width="5">
                            </td>
                            <td style="width: 85px">
                                <b>�ӹǹ�Ѻ</b></td>
                            <td style="width: 115px">
                                <asp:Label ID="lblQty" runat="server"></asp:Label></td>
                        </tr>
                    </table></asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="��ŧ" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/>
            </td> 
        </tr> 
    </table>
                    <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
</asp:Content>