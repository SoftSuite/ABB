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
using ABB.Flow.Handheld.FG;
using ABB.Data;
using ABB.Data.Handheld.FG;
using ABB.Global;

public partial class FG_Transaction_StockOut_ProductList : System.Web.UI.Page
{
    private StockOutFGFlow _flow;

    private StockOutFGFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockOutFGFlow(); } return _flow; }
    }

    private void SetData(double stockOut)
    {
        this.txtLOID.Text = stockOut.ToString();
        StockOutFGData data = FlowObj.GetStockOutData(stockOut);
        this.lblCode.Text = data.CODE;
        this.lblReqCode.Text = data.REQCODE;
        this.lblDocName.Text = data.DOCNAME;
        ResetState(stockOut);
    }

    private void ResetState(double stockOut)
    {
        this.pnlData.Visible = true;
        this.pnlMessage.Visible = false;
        this.ctlToolbar.Visible = true;
        this.grvData.DataSource = FlowObj.GetStockOutItemList(stockOut);
        this.grvData.DataBind();

        if (this.grvData.Rows.Count == 0) this.ctlToolbar.BtnSaveShow = false;
        this.pnlSave.Visible = false;
        this.pnlDelete.Visible = false;
        this.btnSave.Visible = false;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetData(Request["loid"] == null ? 0 : Convert.ToDouble(Request["loid"]));
        }
    }

    protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.pnlData.Visible = false;
        this.pnlMessage.Visible = true;
        this.lblProductName.Text = this.grvData.Rows[this.grvData.SelectedIndex].Cells[2].Text;
        this.ctlToolbar.Visible = false;
        this.pnlDelete.Visible = true;
        this.btnSave.Visible = true;
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockOut/AddProduct.aspx?loid=" + txtLOID.Text);
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitStockOut(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text)))
        {
            this.pnlData.Visible = false;
            this.pnlMessage.Visible = true;
            this.ctlToolbar.Visible = false;
            this.pnlSave.Visible = true;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (pnlDelete.Visible)
        {
            if (FlowObj.DeleteStockOutItem(Convert.ToDouble(this.grvData.SelectedValue)))
            {
                ResetState(Convert.ToDouble(this.txtLOID.Text));
            }
        }
        else if (pnlSave.Visible)
        {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockOut/StockOutList.aspx");
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (pnlDelete.Visible)
        {
            ResetState(Convert.ToDouble(this.txtLOID.Text));
        }
        else
        {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockOut/StockOutList.aspx");
        }
    }

    protected void grvData_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((ImageButton)e.Row.Cells[0].FindControl("btnDetail")).CommandArgument = drow["LOID"].ToString();
        }
    }

    protected void grvData_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "view")
        {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockOut/ProductDetail.aspx?loid=" + e.CommandArgument.ToString());
        }
    }

}
