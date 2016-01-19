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
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;
using ABB.Global;

public partial class Transaction_PlanPurchaseSearch : System.Web.UI.Page
{
    private PlanPOFlow _flow;
    public PlanPOFlow FlowObj
    {
        get { if (_flow == null) _flow = new PlanPOFlow(); return _flow; }
    }

    private void Search()
    {
        this.grvPlan.DataSource = FlowObj.GetPlanList();
        this.grvPlan.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Search();
        }
    }

}
