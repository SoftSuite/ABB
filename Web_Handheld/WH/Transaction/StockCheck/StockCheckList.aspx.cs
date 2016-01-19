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
using ABB.Flow.Handheld;
using ABB.Data;
using ABB.Data.Handheld.Common;
using ABB.Global;

public partial class WH_Transaction_StockCheck_StockCheckList : System.Web.UI.Page
{
    private StockCheckBatchFlow _flow;

    private StockCheckBatchFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockCheckBatchFlow(); } return _flow; }
    }

    private void SearchData()
    {
        this.grvData.DataSource = FlowObj.GetStockCheckist(Authz.CurrentUserInfo.Warehouse);
        this.grvData.DataBind();
        ResetState();
    }

    private void ResetState()
    {
        this.txtLOID.Text = "";
        if (this.grvData.Rows.Count > 0)
        {
            this.grvData.SelectedIndex = 0;
            this.txtLOID.Text = this.grvData.Rows[0].Cells[1].Text;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SearchData();
        }
    }

    protected void HelpClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/Help.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Default.aspx");
    }

    protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.grvData.SelectedValue != null)
        {
            this.txtLOID.Text = this.grvData.SelectedValue.ToString();
            double stockCheck = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/ProductList.aspx?loid=" + stockCheck.ToString());
        }
    }
}
