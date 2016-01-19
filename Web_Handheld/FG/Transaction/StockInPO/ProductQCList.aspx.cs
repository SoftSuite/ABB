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

public partial class FG_Transaction_StockInPO_ProductQCList : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SetData(double stockIn)
    {
        this.txtLOID.Text = stockIn.ToString();
        StockInQCData data = FlowObj.GetStockInQCData(stockIn);
        this.lblCode.Text = data.CODE;
        if (data.RECEIVEDATE.Year != 1) this.lblReceiveDate.Text = data.RECEIVEDATE.ToString(Constz.DateFormat);
        this.lblQCCode.Text = data.QCCODE;
        ResetState(stockIn);
    }

    private void ResetState(double stockIn)
    {
        this.pnlData.Visible = true;
        this.pnlMessage.Visible = false;
        this.ctlToolbar.Visible = true;
        this.grvData.DataSource = FlowObj.GetQCProductList(stockIn);
        this.grvData.DataBind();

        if (this.grvData.Rows.Count == 0) this.ctlToolbar.BtnSubmitShow = false;
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
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/AddProductQC.aspx?loid=" + txtLOID.Text);
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.QCStockIn(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.txtLOID.Text)))
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
            if (FlowObj.CancelStockInItemQC(Authz.CurrentUserInfo.UserID, Convert.ToDouble(this.grvData.SelectedValue)))
            {
                ResetState(Convert.ToDouble(this.txtLOID.Text));
            }
        }
        else if (pnlSave.Visible)
        {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/StockInList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/StockInList.aspx");
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
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/ProductQCDetail.aspx?loid=" + e.CommandArgument.ToString());
        }
    }

}
