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
using ABB.Data.Handheld.Common.StockIn;
using ABB.Data.Sales;
using ABB.Global;

public partial class WH_Transaction_StockInPO_ProductList : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SetData(double loid)
    {
        this.txtLOID.Text = loid.ToString();
        StockInPOData data = FlowObj.GetStockInPOData(loid);
        this.txtStatus.Text = data.STATUS;
        this.lblCode.Text = data.CODE;
        this.lblInvNo.Text = data.INVNO;
        this.lblSupplierName.Text = data.SUPPLIERNAME;
        ComboSource.BuildCombo(this.cmbPR, "PDORDER", "CODE", "LOID", "CODE", "LOID IN (SELECT POI.PDORDER FROM STOCKINITEM STI INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' WHERE STI.STOCKIN=" + loid.ToString() + ") ");
        SetGridView(loid, Convert.ToDouble(this.cmbPR.SelectedValue == "" ? 0 : Convert.ToDouble(this.cmbPR.SelectedItem.Value)));
        ResetState(loid);
    }

    private void ResetState(double stockIn)
    {
        this.pnlData.Visible = true;
        this.btnSave.Visible = false;
        this.cmbPR.Enabled = true;
        this.pnlMessage.Visible = false;
        this.ctlToolbar.Visible = true;
        this.ctlToolbar1.Visible = true;
        SetGridView(stockIn, Convert.ToDouble(this.cmbPR.SelectedValue == "" ? 0 : Convert.ToDouble(this.cmbPR.SelectedItem.Value)));

        if (this.grvData.Rows.Count == 0) this.ctlToolbar.BtnSaveShow = false;
        this.pnlSave.Visible = false;
        this.pnlDelete.Visible = false;

        if (this.txtStatus.Text != Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.Visible = false;
            this.ctlToolbar1.Visible = false;

            foreach (GridViewRow gRow in this.grvData.Rows)
            {
                ((ImageButton)gRow.Cells[0].FindControl("btnDelete")).Visible = false;
            }
        }
    }

    private void SetGridView(double stockIn, double PDOrder)
    {
        this.grvData.DataSource = FlowObj.GetProductList(stockIn, PDOrder);
        this.grvData.DataBind();
        this.ctlToolbar.BtnSubmitShow = (this.grvData.Rows.Count > 0);
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
        this.ctlToolbar1.Visible = false;
        this.pnlDelete.Visible = true;
        this.cmbPR.Enabled = false;
        this.btnSave.Visible = true;
    }

    protected void NewClick(object sender, EventArgs e)
    {
        if (this.cmbPR.SelectedValue == "")
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/SelectPO.aspx?loid=" + txtLOID.Text);
        else
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/AddProduct.aspx?loid=" + txtLOID.Text + "&pdorder=" + this.cmbPR.SelectedItem.Value);
    }

    protected void NewPOClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/SelectPO.aspx?loid=" + txtLOID.Text);
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        this.pnlData.Visible = false;
        this.pnlMessage.Visible = true;
        this.ctlToolbar.Visible = false;
        this.ctlToolbar1.Visible = false;
        this.pnlSave.Visible = true;
        this.cmbPR.Enabled = false;
        this.btnSave.Visible = true;
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
            if (FlowObj.ReceiveStockIn(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text)))
            {
                Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/StockInList.aspx");
            }
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (pnlDelete.Visible || pnlSave.Visible)
        {
            ResetState(Convert.ToDouble(this.txtLOID.Text));
        }
        else
        {
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/StockInList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/ProductDetail.aspx?loid=" + e.CommandArgument.ToString());
        }
    }

    protected void cmbPR_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetGridView(Convert.ToDouble(this.txtLOID.Text), Convert.ToDouble(this.cmbPR.SelectedValue == null ? 0 : Convert.ToDouble(this.cmbPR.SelectedItem.Value)));
    }
}
