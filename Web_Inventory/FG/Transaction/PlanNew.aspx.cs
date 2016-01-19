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
using ABB.Flow.Inventory.FG;

public partial class FG_Transaction_PlanNew : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmbYear.Items.Clear();
            for (int i = DateTime.Now.Year + 543; i < DateTime.Now.Year + 10 + 543; ++i)
            {
                this.cmbYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
            }
            this.btnCancel.OnClientClick = "window.close(); return false;";
            this.btnSave.OnClientClick = "window.returnValue=document.getElementById('" + this.cmbYear.ClientID + "').value + '#' + document.getElementById('" + this.txtDescription.ClientID + "').value; window.close(); return false;";
            if (Request["copyplan"] != null)
            {
                PlanInventoryFlow pFlow = new PlanInventoryFlow();
                PlanData data = pFlow.GetPlanData(Convert.ToDouble(Request["copyplan"]));
                this.txtDescription.Text = data.DESCRIPTION;
            }
        }
    }
}
