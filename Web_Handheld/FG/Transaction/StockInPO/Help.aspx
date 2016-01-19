<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="Help.aspx.cs" Inherits="FG_Transaction_StockInPO_Help" Title="รับสินค้าจากผู้จำหน่าย" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td height="175px" valign="top">
                <asp:Panel ID="Panel1" runat="server">
                </asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="ตกลง" CssClass="hButton" Width="80px" OnClick="btnSave_Click"/>
            </td> 
        </tr> 
    </table>
</asp:Content>