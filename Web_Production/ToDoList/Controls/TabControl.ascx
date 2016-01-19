<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TabControl.ascx.cs" Inherits="ToDoList_Controls_TabControl" %>
<table border="0" cellpadding="0" cellspacing="0" width="100%">
    <tr style="height:25">
        <td style="background-image: url(../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent; width:1px;" valign="middle" align="center">
        </td>
        
        <td valign="top" align="center" style="width:1px">
            <asp:image id="imgBD1L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Production.ProductionWaitList.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent; width:172px;" valign="middle" align="center">
            <asp:Label ID="lblProductionWaitList" runat="server" Text="รายการสินค้าที่รอผลิต" Width="170px"></asp:Label>
            <asp:LinkButton ID="btnProductionWaitList" runat="server" Text="รายการสินค้าที่รอผลิต" Width="170px" OnClick="btnProductionWaitList_Click" Visible="False" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" style="width:1px">
            <asp:image id="imgBD1R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td valign="top" align="center" style="width:1px">
            <asp:image id="imgBD2L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
        </td>
        <td style="background-image: url(../Images/<%= (SelectedTab == ABB.Data.Constz.ToDoListTab.Production.ProductionDuringList.Index ? "pMenuS" : "pMenuN") %>.png); background-repeat: repeat-x; background-color: transparent; width:80px;" valign="middle" align="center">
            <asp:Label ID="lblProductionDuringList" runat="server" Text="รายการสินค้าที่อยู่ระหว่างรอการผลิต" Width="200px" Visible="False"></asp:Label>
            <asp:LinkButton ID="btnProductionDuringList" runat="server" Text="รายการสินค้าที่อยู่ระหว่างรอการผลิต" Width="200px" OnClick="btnProductionDuringList_Click" CssClass="tabtext"></asp:LinkButton>
        </td> 
        <td valign="top" align="center" style="width:1px">
            <asp:image id="imgBD2R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
        </td>
        
        <td style="background-image: url(../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent; width:100%;" valign="middle" align="center">
            <asp:Label ID="lblTab" runat="server" CssClass="zHidden" Text="1"></asp:Label>  
        </td> 
    </tr>
</table>