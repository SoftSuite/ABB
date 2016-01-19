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
using ABB.Data.Handheld.Common.StockIn;
using ABB.Data.Sales;
using ABB.Global;

public partial class FG_Transaction_StockInPD_ProductList : System.Web.UI.Page
{
    private StockInPDFlow _flow;

    private StockInPDFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPDFlow(); } return _flow; }
    }

    private void SetData(double stockIn)
    {
        this.txtLOID.Text = stockIn.ToString();
        StockInData data = FlowObj.GetData(stockIn);
        this.txtCode.Text = data.CODE;
        ResetState(stockIn);
    }

    private void ResetState(double stockIn)
    {
        this.pnlData.Visible = true;
        this.pnlMessage.Visible = false;
        this.ctlToolbar.Visible = true;
        this.grvData.DataSource = FlowObj.GetProductList(stockIn);
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
        this.lblProductName.Text = this.grvData.Rows[this.grvData.SelectedIndex].Cells[3].Text;
        this.ctlToolbar.Visible = false;
        this.pnlDelete.Visible = true;
        this.btnSave.Visible = true;
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/AddProduct.aspx?loid=" + txtLOID.Text);
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitStockIn(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text)))
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
            if (FlowObj.DeleteStockInItem(Convert.ToDouble(this.grvData.SelectedValue)))
            {
                ResetState(Convert.ToDouble(this.txtLOID.Text));
            }
        }
        else if (pnlSave.Visible)
        {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/StockInList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/StockInList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/ProductDetail.aspx?loid=" + e.CommandArgument.ToString());
        }
    }
}
