<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PlanPurchaseSearch.aspx.cs" Inherits="Transaction_PlanPurchaseSearch" Title="Ἱ�����觫����Թ���/�ѵ�شԺ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;Ἱ�����觫����Թ���/�ѵ�شԺ</td> 
        </tr> 
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPlan" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***��辺������***</center>"
                    Width="400px" >
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField HeaderText="�ӴѺ���">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:HyperLinkField DataNavigateUrlFields="YEAR" DataNavigateUrlFormatString="PlanPurchase.aspx?year={0}"
                            DataTextField="YEAR" HeaderText="�� �.�." DataTextFormatString="{0}" >
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:HyperLinkField> 
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>