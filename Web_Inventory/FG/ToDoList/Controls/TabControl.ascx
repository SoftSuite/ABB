<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="FG_ToDoList_Controls_TabControl" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr style="height:25">
        <td width="1px" style="background-image: url(../../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" align="center">
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD1L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.FG.MinimumStock.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="172px">
            <asp:Label ID="lblMinimumStock" runat="server" Text="สินค้าที่ตำกว่า Minimum Stock" Width="170px"></asp:Label>
            <asp:LinkButton ID="btnMinimumStock" runat="server" Text="สินค้าที่ตำกว่า Minimum Stock" Width="170px" OnClick="btnMinimumStock_Click" Visible="False" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD1R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD2L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.FG.StockIn.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="80px">
            <asp:Label ID="lblStockIn" runat="server" Text="สินค้าที่รอรับเข้า" Width="110px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnStockIn" runat="server" Text="สินค้าที่รอรับเข้า" Width="110px" OnClick="btnStockIn_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD2R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD3L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.FG.StockOut.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="80px">
            <asp:Label ID="lblStockOut" runat="server" Text="สินค้าที่รอจ่ายออก" Width="110px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnStockOut" runat="server" Text="สินค้าที่รอจ่ายออก" Width="110px" OnClick="btnStockOut_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD3R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
         <td valign="top" align="center" width="1px">
            <asp:image id="imgBD4L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.FG.Expire.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="80px">
            <asp:Label ID="lblExpire" runat="server" Text="สินค้าที่ใกล้หมดอายุ" Width="148px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnExpire" runat="server" Text="สินค้าที่ใกล้หมดอายุ" Width="148px" OnClick="btnExpire_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD4R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" width="100%" style="background-image: url(../../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" align="center">
            <asp:Label ID="lblTab" runat="server" CssClass="zHidden" Text="1"></asp:Label>  
        </td> 
    </tr>
</table>