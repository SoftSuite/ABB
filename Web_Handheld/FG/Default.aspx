<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="FG_Default" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="height:200px" valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td style="height: 20px" align="right" class="subheadertext"><asp:LoginStatus ID="LoginStatus1" runat="server" LoginText="" Font-Underline="false" CssClass="logoutbutton"
                                        LogoutText="[Logout]" LogoutAction="RedirectToLoginPage" />
                        </td>
                    </tr>
                    <tr>
                        <td style="height:5px">
                        </td>
                    </tr> 
                    <tr>
                        <td style="height:20px">
                            &nbsp;<asp:HyperLink ID="lnkStockInPD" runat="server">�Ѻ�Թ��Ҩҡ���¼�Ե</asp:HyperLink></td>
                    </tr> 
                    <tr>
                        <td style="height:20px">
                            &nbsp;<asp:HyperLink ID="lnlStockInPO" runat="server">�Ѻ�Թ��Ҩҡ����˹���</asp:HyperLink></td>
                    </tr> 
                    <tr>
                        <td style="height:20px">
                            &nbsp;<asp:HyperLink ID="lnkStockOut" runat="server">�ԡ�Թ���</asp:HyperLink></td>
                    </tr> 
                    <tr>
                        <td style="height:20px">
                            &nbsp;<asp:HyperLink ID="lnkStockCheck" runat="server">��Ǩ�Ѻ�Թ���</asp:HyperLink></td>
                    </tr> 
                </table>
            </td>
        </tr> 
    </table>
</asp:Content>