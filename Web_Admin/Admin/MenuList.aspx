<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="MenuList.aspx.cs" Inherits="Admin_MenuList" Title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MenuContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table width="100%" cellspacing="0" cellpadding="0" border="0">
        <tr>
            <td></td>
        </tr>
        <tr>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="50px" Width="125px">
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                        SelectMethod="GetMenuItem" TypeName="MenuDataSource">
                    </asp:ObjectDataSource>
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" Height="50px" Width="125px">
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>

