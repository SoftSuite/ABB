using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;

public partial class WH_ToDoList_ToDoList : System.Web.UI.Page
{
    private void SetControl()
    {
        this.ctlMinStock.Visible = (this.ctlTab.SelectedTab == Constz.ToDoListTab.Inventory.WH.MinimumStock.Index);
        this.ctlStockIn.Visible = (this.ctlTab.SelectedTab == Constz.ToDoListTab.Inventory.WH.StockIn.Index);
        this.ctlStockOut.Visible = (this.ctlTab.SelectedTab == Constz.ToDoListTab.Inventory.WH.StockOut.Index);
        this.ctlExpire.Visible = (this.ctlTab.SelectedTab == Constz.ToDoListTab.Inventory.WH.Expire.Index);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ctlTab_SelectedChange(object sender, EventArgs e)
    {
        SetControl();
    }

}
