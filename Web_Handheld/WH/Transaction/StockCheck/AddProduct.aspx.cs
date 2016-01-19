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

public partial class WH_Transaction_StockCheck_AddProduct : System.Web.UI.Page
{
    private StockCheckBatchFlow _flow;

    private StockCheckBatchFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockCheckBatchFlow(); } return _flow; }
    }

    private void SetData(double stockCheck, double location)
    {
        this.txtLOID.Text = stockCheck.ToString();
        StockCheckBatchData data = FlowObj.GetStockCheckData(stockCheck);
        this.lblBatchNo.Text = data.BATCHNO;
        this.lblWarehouseName.Text = data.WAREHOUSENAME;
        this.lblLocationName.Text = FlowObj.GetLocationName(location);
        this.txtLocation.Text = location.ToString();
        ResetState();
    }

    private void ResetState()
    {
        this.pnlData.Visible = true;
        this.ctlToolbar.Visible = true;
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
        this.pnlMessage.Visible = false;
        this.btnCancel.Visible = true;
    }

    private void SetError(string errorMessage)
    {
        this.pnlData.Visible = false;
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
            SetData(Request["loid"] == null ? 0 : Convert.ToDouble(Request["loid"]), Request["location"] == null ? 0 : Convert.ToDouble(Request["location"]));
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ResetState();
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/StockCheckList.aspx");
    }

    protected void ViewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/ProductList.aspx?loid=" + txtLOID.Text);
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        if (FlowObj.GetProductData(this.txtBarcode.Text.Trim(), Authz.CurrentUserInfo.Warehouse, Convert.ToDouble(this.txtLocation.Text)))
        {
            ProductSearchData data = FlowObj.ProductData;
            this.txtProduct.Text = data.LOID.ToString();
            this.lblProductName.Text = data.NAME;
            this.lblUnitName.Text = data.UNITNAME;
            this.txtUnit.Text = data.UNIT.ToString();
            ComboSource.BuildComboDistinct(this.cmbLotNo, "PRODUCTSTOCK", "LOTNO", "LOID", "LOTNO", "PRODUCT = (SELECT PRODUCTMASTER FROM PRODUCTBARCODE WHERE LOID = " + data.LOID.ToString() + ") AND ZONE IN (SELECT MIN(LOID) FROM ZONE WHERE LOCATION = " + this.txtLocation.Text + ") ");
            this.cmbLotNo.Enabled = true;
            this.txtQty.Enabled = true;
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
                StockCheckItemData data = new StockCheckItemData();
                data.PRODUCT = Convert.ToDouble(this.txtProduct.Text == "" ? "0" : this.txtProduct.Text);
                if (this.cmbLotNo.SelectedValue != "") data.LOTNO = this.cmbLotNo.SelectedItem.Text;
                data.COUNTQTY = Convert.ToDouble(this.txtQty.Text == "" ? "0" : this.txtQty.Text);
                data.STOCKCHECK = Convert.ToDouble(this.txtLOID.Text);
                data.LOCATION = Convert.ToDouble(this.txtLocation.Text);

                if (FlowObj.insertStockCheckItem(Authz.CurrentUserInfo.UserID, data))
                    ResetState();
                else
                    SetError(FlowObj.ErrorMessage);
            }
            catch (Exception ex)
            {
                SetError(ex.Message);
            }
        }
    }
}
