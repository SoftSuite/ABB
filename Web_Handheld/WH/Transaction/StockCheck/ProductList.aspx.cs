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

public partial class WH_Transaction_StockCheck_ProductList : System.Web.UI.Page
{
    private StockCheckBatchFlow _flow;

    private StockCheckBatchFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockCheckBatchFlow(); } return _flow; }
    }

    private void SetData(double stockCheck)
    {
        this.txtLOID.Text = stockCheck.ToString();
        StockCheckBatchData data = FlowObj.GetStockCheckData(stockCheck);
        this.lblBatchNo.Text = data.BATCHNO;
        this.lblWarehouseName.Text = data.WAREHOUSENAME;
        ComboSource.BuildComboDistinct(this.cmbLocation, "LOCATION", "NAME", "LOID", "NAME", "LOID IN (SELECT LOCATION FROM STOCKCHECKITEM WHERE STOCKCHECK = " + data.STOCKCHECK.ToString() + " AND CREATEBY = '" + Authz.CurrentUserInfo.UserID + "') ");
        ResetState(stockCheck);
    }

    private void SearchData()
    {
        double location = 0;
        if (this.cmbLocation.Items.Count != 0) location = Convert.ToDouble(this.cmbLocation.SelectedItem.Value);
        this.grvData.DataSource = FlowObj.GetStockCheckItemList(Convert.ToDouble(this.txtLOID.Text), location, Authz.CurrentUserInfo.UserID);
        this.grvData.DataBind();
        if (this.grvData.Rows.Count == 0) this.ctlToolbar.BtnSaveShow = false;
    }

    private void ResetState(double stockOut)
    {
        this.pnlData.Visible = true;
        this.pnlMessage.Visible = false;
        this.ctlToolbar.Visible = true;
        this.ctlToolbarZone.Visible = true;
        SearchData();
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
        this.ctlToolbarZone.Visible = false;
        this.pnlDelete.Visible = true;
        this.btnSave.Visible = true;
    }

    protected void NewClick(object sender, EventArgs e)
    {
        if (this.cmbLocation.Items.Count == 0)
            NewZoneClick(sender, e);
        else
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/AddProduct.aspx?loid=" + txtLOID.Text + "&location=" + this.cmbLocation.SelectedItem.Value);
    }

    protected void NewZoneClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/SelectZone.aspx?loid=" + txtLOID.Text);
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateStockCheckStatus(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text), Constz.Requisition.Status.Approved.Code))
        {
            this.pnlData.Visible = false;
            this.pnlMessage.Visible = true;
            this.ctlToolbar.Visible = false;
            this.ctlToolbarZone.Visible = false;
            this.pnlSave.Visible = true;
            this.btnSave.Visible = true;
            this.btnCancel.Visible = false;
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockCheck/StockCheckList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (pnlDelete.Visible)
        {
            if (FlowObj.DeleteStockCheckItem(Convert.ToDouble(this.grvData.SelectedValue)))
            {
                ResetState(Convert.ToDouble(this.txtLOID.Text));
            }
        }
        else if (pnlSave.Visible)
        {
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/StockCheckList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/StockCheckList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/ProductDetail.aspx?loid=" + e.CommandArgument.ToString());
        }
    }

    protected void cmbLocation_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }
}
