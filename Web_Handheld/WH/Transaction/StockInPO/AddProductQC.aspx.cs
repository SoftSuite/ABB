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

public partial class WH_Transaction_StockInPO_AddProductQC : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SetData(double loid)
    {
        this.txtLOID.Text = loid.ToString();
        StockInQCData data = FlowObj.GetStockInQCData(loid);
        this.lblCode.Text = data.CODE;
        if (data.RECEIVEDATE.Year != 1) this.lblReceiveDate.Text = data.RECEIVEDATE.ToString(Constz.DateFormat);
        this.lblQCCode.Text = data.QCCODE;
        ResetState();
    }

    private void ResetState()
    {
        this.pnlData.Visible = true;
        this.ctlToolbar.Visible = true;
        this.txtStockInItem.Text = "0";
        this.txtBarcode.Text = "";
        this.lblProductName.Text = "";
        this.lblUnitName.Text = "";
        this.txtQty.Enabled = false;
        this.txtQty.Text = "1";
        this.pnlMessage.Visible = false;
        this.btnCancel.Visible = true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlUtil.SetIntTextBox(this.txtQty);
            SetData(Request["loid"] == null ? 0 : Convert.ToDouble(Request["loid"]));
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetState();
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/StockInList.aspx");
    }

    protected void ViewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInPO/ProductQCList.aspx?loid=" + txtLOID.Text);
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        StockInProductData data = FlowObj.GetProductQCData(this.txtBarcode.Text.Trim(), Convert.ToDouble(this.txtLOID.Text));
        if (data.LOID > 0)
        {
            this.txtStockInItem.Text = data.LOID.ToString();
            this.lblProductName.Text = data.NAME;
            this.lblUnitName.Text = data.UNITNAME;
            this.txtQty.Enabled = true;
            this.txtQty.Text = data.QTY.ToString(Constz.IntFormat);
        }
        else
        {
            this.pnlData.Visible = false;
            this.ctlToolbar.Visible = false;
            this.btnCancel.Visible = false;
            this.pnlMessage.Visible = true;
            this.lblError.Text = "ไม่พบวัตถุดิบในใบสั่งซื้อ";
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.pnlMessage.Visible)
        {
            ResetState();
        }
        else
        {
            double stockInItem = Convert.ToDouble(this.txtStockInItem.Text);
            double qty = Convert.ToDouble(this.txtQty.Text == "" ? "0" : this.txtQty.Text);
            if (stockInItem > 0 && qty > 0)
            {
                if (FlowObj.UpdateStockInItemQty(Authz.CurrentUserInfo.UserID, stockInItem, qty))
                    ResetState();
                else
                {
                    this.pnlData.Visible = false;
                    this.ctlToolbar.Visible = false;
                    this.btnCancel.Visible = false;
                    this.pnlMessage.Visible = true;
                    this.lblError.Text = FlowObj.ErrorMessage;
                }
            }
        }
    }
}
