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
using ABB.Data.Handheld.Common;
using ABB.Data.Handheld.WH;
using ABB.Global;

public partial class WH_Transaction_StockOut_AddProduct : System.Web.UI.Page
{
    private StockOutWHFlow _flow;

    private StockOutWHFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockOutWHFlow(); } return _flow; }
    }

    private void SetData(double loid)
    {
        this.txtLOID.Text = loid.ToString();
        StockOutWHData data = FlowObj.GetStockOutData(loid);
        this.lblCode.Text = data.CODE;
        this.lblReqCode.Text = data.REQCODE;
        this.lblDocName.Text = data.DOCNAME;
        this.lblOrderLotNo.Text = data.ORDERLOTNO;
        ResetState();
    }

    private void ResetState()
    {
        this.pnlData.Visible = true;
        this.ctlToolbar.Visible = true;
        this.txtRefLOID.Text = "";
        this.txtProduct.Text = "0";
        this.txtBarcode.Text = "";
        this.lblProductName.Text = "";
        this.lblUnitName.Text = "";
        this.cmbLotNo.DataSource = null;
        this.cmbLotNo.DataBind();
        this.cmbLotNo.Enabled = false;
        this.txtQty.Enabled = false;
        this.txtQty.Text = "1";
        this.txtUnit.Text = "0";
        this.txtPrice.Text = "0";
        this.pnlMessage.Visible = false;
        this.btnCancel.Visible = true;
    }

    private void SetError(string errorMessage)
    {
        this.cmbLotNo.DataSource = null;
        this.cmbLotNo.Enabled = false;
        this.txtQty.Enabled = false;
        this.ctlToolbar.Visible = false;
        this.btnCancel.Visible = false;
        this.pnlMessage.Visible = true;
        this.lblError.Text = errorMessage;
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
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockOut/StockOutList.aspx");
    }

    protected void ViewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockOut/ProductList.aspx?loid=" + txtLOID.Text);
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        if (FlowObj.GetProductData(Convert.ToDouble(this.txtLOID.Text), this.txtBarcode.Text.Trim()))
        {
            ProductSearchData data = FlowObj.ProductData;
            this.txtProduct.Text = data.LOID.ToString();
            this.lblProductName.Text = data.NAME;
            this.lblUnitName.Text = data.UNITNAME;
            this.txtUnit.Text = data.UNIT.ToString();
            this.txtRefLOID.Text = data.REFLOID.ToString();
            this.cmbLotNo.DataSource = FlowObj.GetProductStock(data.LOID, Convert.ToDouble(this.txtLOID.Text));
            this.cmbLotNo.DataTextField = "LOTNO";
            this.cmbLotNo.DataValueField = "LOTNO";
            this.cmbLotNo.DataBind();
            this.cmbLotNo.Enabled = true;
            this.txtQty.Enabled = true;
            this.txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        }
        else
        {
            SetError(FlowObj.ErrorMessage);
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
            try
            {
                StockOutItemData data = new StockOutItemData();
                data.REFTABLE = "REQMATERIAL";
                data.REFLOID = Convert.ToDouble(this.txtRefLOID.Text);
                data.PRICE = Convert.ToDouble(this.txtPrice.Text == "" ? "0" : this.txtPrice.Text);
                data.PRODUCT = Convert.ToDouble(this.txtProduct.Text);
                if (this.cmbLotNo.SelectedValue != "") data.LOTNO = this.cmbLotNo.SelectedItem.Text;
                data.QTY = Convert.ToDouble(this.txtQty.Text == "" ? "0" : this.txtQty.Text);
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.STATUS = Constz.Requisition.Status.Waiting.Code;
                data.STOCKOUT = Convert.ToDouble(this.txtLOID.Text);
                data.UNIT = Convert.ToDouble(this.txtUnit.Text == "" ? "0" : this.txtUnit.Text);

                if (data.REFLOID != 0)
                {
                    if (FlowObj.UpdateStockOutItem(Authz.CurrentUserInfo.UserID, data))
                        ResetState();
                    else
                        SetError(FlowObj.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
        }
    }
}
