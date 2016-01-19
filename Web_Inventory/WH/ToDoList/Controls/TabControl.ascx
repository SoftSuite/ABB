<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="WH_ToDoList_Controls_TabControl" %>
<table border="0" cellpadding="0" cellspacing="0" width="910px">
    <tr style="height:25">
        <td width="1px" style="background-image: url(../../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD1L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.WH.MinimumStock.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="172px">
            <asp:Label ID="lblMinimumStock" runat="server" Text="วัตถุดิบที่ตำกว่า Minimum Stock" Width="190px"></asp:Label>
            <asp:LinkButton ID="btnMinimumStock" runat="server" Text="วัตถุดิบที่ตำกว่า Minimum Stock" Width="190px" OnClick="btnMinimumStock_Click" Visible="False" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD1R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD2L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.WH.StockIn.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="80px">
            <asp:Label ID="lblStockIn" runat="server" Text="วัตถุดิบที่รอรับเข้า" Width="110px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnStockIn" runat="server" Text="วัตถุดิบที่รอรับเข้า" Width="110px" OnClick="btnStockIn_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD2R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD3L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.WH.StockOut.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="80px">
            <asp:Label ID="lblStockOut" runat="server" Text="วัตถุดิบที่รอจ่ายออก" Width="110px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnStockOut" runat="server" Text="วัตถุดิบที่รอจ่ายออก" Width="110px" OnClick="btnStockOut_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD3R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
          <td valign="top" align="center" width="1px">
            <asp:image id="imgBD4L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Inventory.WH.Expire.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="172px">
            <asp:Label ID="lblExpire" runat="server" Text="วัตถุดิบที่ใกล้หมดอายุ" Width="143px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnExpire" runat="server" Text="วัตถุดิบที่ใกล้หมดอายุ" Width="147px" OnClick="btnExpire_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" width="1px">
            <asp:image id="imgBD4R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td width="735px" style="background-image: url(../../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
            <asp:Label ID="lblTab" runat="server" CssClass="zHidden" Text="1"></asp:Label>  
        </td> 
    </tr>
</table>