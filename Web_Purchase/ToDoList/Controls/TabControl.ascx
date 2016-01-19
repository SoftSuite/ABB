<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="ToDoList_Controls_TabControl" %>
<table border="0" cellpadding="0" cellspacing="0" width="910px">
    <tr style="height:32">
        <td width="1px" style="background-image: url(../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD1L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Purchase.ProductPurchaseList.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="172px">
            <asp:Label ID="lblProductPurchaseList" runat="server" Text="สินค้าที่รอรับสินค้า" Width="170px"></asp:Label>
            <asp:LinkButton ID="btnProductPurchaseList" runat="server" Text="สินค้าที่รอรับสินค้า" Width="170px" OnClick="btnProductPurchaseList_Click" Visible="False" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD1R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD2L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Purchase.ProductReceiveList.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="80px">
            <asp:Label ID="lblProductReceiveList" runat="server" Text="สินค้าที่รอทำใบสั่งซื้อ" Width="170px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnProductReceiveList" runat="server" Text="สินค้าที่รอทำใบสั่งซื้อ" Width="170px" OnClick="btnProductReceiveList_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD2R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td width="735px" style="background-image: url(../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
            <asp:Label ID="lblTab" runat="server" CssClass="zHidden" Text="1"></asp:Label>  
        </td> 
    </tr>
</table>