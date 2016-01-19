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

public partial class ToDoList_ToDoList : System.Web.UI.Page
{
    private void SetControl()
    {
        this.ctlProductionWaitList.Visible = (this.ctlTab.SelectedTab == Constz.ToDoListTab.Production.ProductionWaitList.Index);
        this.ctlProductionDuringList.Visible = (this.ctlTab.SelectedTab == Constz.ToDoListTab.Production.ProductionDuringList.Index);
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void ctlTab_SelectedChange(object sender, EventArgs e)
    {
        SetControl();
    }
}
