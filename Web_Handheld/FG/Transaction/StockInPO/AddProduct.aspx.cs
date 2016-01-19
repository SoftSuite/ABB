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

public partial class FG_Transaction_StockInPO_AddProduct : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SetData(double loid, double PDOrder)
    {
        this.txtLOID.Text = loid.ToString();
        this.txtPDOrder.Text = PDOrder.ToString();
        StockInPOData data = FlowObj.GetStockInPOData(loid, PDOrder);
        this.lblCode.Text = data.CODE;
        this.lblInvNo.Text = data.INVNO;
        this.lblPDOrderCode.Text = data.ORDERCODE;
        this.lblSupplierName.Text = data.SUPPLIERNAME;
        this.txtPDOrder.Text = data.PDORDER.ToString();
        ResetState();
    }

    private void ResetState()
    {
        this.pnlData.Visible = true;
        this.ctlToolbar.Visible = true;
        this.txtPOItem.Text = "";
        this.txtProduct.Text = "0";
        this.txtBarcode.Text = "";
        this.lblProductName.Text = "";
        this.lblUnitName.Text = "";
        this.txtQty.Enabled = false;
        this.txtQty.Text = "1";
        this.txtQCQty.Enabled = false;
        this.txtQCQty.Text = "1";
        this.pnlMessage.Visible = false;
        this.btnCancel.Visible = true;
        this.txtUnit.Text = "0";
        this.txtLotNo.Text = "";
        this.txtPrice.Text = "0";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlUtil.SetIntTextBox(this.txtQty);
            ControlUtil.SetIntTextBox(this.txtQCQty);
            SetData(Request["loid"] == null ? 0 : Convert.ToDouble(Request["loid"]), Request["pdorder"] == null ? 0 : Convert.ToDouble(Request["pdorder"]));
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetState();
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/StockInList.aspx");
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/SelectPO.aspx?loid=" + txtLOID.Text);
    }

    protected void ViewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/ProductList.aspx?loid=" + txtLOID.Text);
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        StockInProductData data = FlowObj.GetProductData(this.txtBarcode.Text.Trim(), Convert.ToDouble(this.txtPDOrder.Text));
        if (data.LOID > 0)
        {
            this.txtPOItem.Text = data.REFLOID.ToString();
            this.txtProduct.Text = data.LOID.ToString();
            this.lblProductName.Text = data.NAME;
            this.lblUnitName.Text = data.UNITNAME;
            this.txtQty.Enabled = true;
            this.txtQCQty.Enabled = true;
            this.txtUnit.Text = data.UNIT.ToString();
            this.txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        }
        else
        {
            this.pnlData.Visible = false;
            this.ctlToolbar.Visible = false;
            this.btnCancel.Visible = false;
            this.pnlMessage.Visible = true;
            this.lblError.Text = "ไม่พบสินค้าในใบสั่งซื้อ";
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
            StockInItemData data = new StockInItemData();
            double number = 0;
            if (Double.TryParse(this.txtQty.Text, out number) && Double.TryParse(this.txtQCQty.Text, out number))
            {
                data.REFTABLE = "POITEM";
                data.REFLOID = Convert.ToDouble(this.txtPOItem.Text == "" ? "0" : this.txtPOItem.Text);
                data.PRICE = Convert.ToDouble(this.txtPrice.Text == "" ? "0" : this.txtPrice.Text);
                data.PRODUCT = Convert.ToDouble(this.txtProduct.Text == "" ? "0" : this.txtProduct.Text);
                data.QTY = Convert.ToDouble(this.txtQty.Text == "" ? "0" : this.txtQty.Text);
                data.QCQTY = Convert.ToDouble(this.txtQCQty.Text == "" ? "0" : this.txtQCQty.Text);
                data.STATUS = Constz.Requisition.Status.Waiting.Code;
                data.STOCKIN = Convert.ToDouble(this.txtLOID.Text);
                data.UNIT = Convert.ToDouble(this.txtUnit.Text == "" ? "0" : this.txtUnit.Text);
                data.LOTNO = this.txtLotNo.Text.Trim();

                if (data.REFLOID != 0)
                {
                    if (FlowObj.InsertStockInItem(Authz.CurrentUserInfo.UserID, data))
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


}
