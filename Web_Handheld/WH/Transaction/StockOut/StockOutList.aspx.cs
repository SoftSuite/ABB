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
using ABB.Flow.Handheld.WH;
using ABB.Data;
using ABB.Data.Handheld.WH;
using ABB.Global;

public partial class WH_Transaction_StockOut_StockOutList : System.Web.UI.Page
{
    private StockOutWHFlow _flow;

    private StockOutWHFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockOutWHFlow(); } return _flow; }
    }

    private void SearchData()
    {
        this.grvData.DataSource = FlowObj.GetStockOutList();
        this.grvData.DataBind();
        ResetState();
    }

    private void ResetState()
    {
        if (this.grvData.Rows.Count > 0)
        {
            this.grvData.SelectedIndex = 0;
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
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockOut/Help.aspx");
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Default.aspx");
    }

    protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.grvData.SelectedValue != null)
        {
            double stockOut = Convert.ToDouble(this.grvData.SelectedRow.Cells[1].Text == "" ? "0" : this.grvData.SelectedRow.Cells[1].Text);
            if (stockOut == 0)
            {
                double requisition = Convert.ToDouble(this.grvData.SelectedRow.Cells[5].Text == "" ? "0" : this.grvData.SelectedRow.Cells[5].Text);
                if (FlowObj.NewStockOut(Authz.CurrentUserInfo.UserID, requisition, Authz.CurrentUserInfo.Warehouse, Authz.CurrentUserInfo.Warehouse))
                    stockOut = FlowObj.LOID;
            }
            if (stockOut > 0) Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockOut/ProductList.aspx?loid=" + stockOut.ToString());
        }
    }

}
