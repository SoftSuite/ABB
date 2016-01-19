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

public partial class FG_Transaction_StockInPD_AddProduct : System.Web.UI.Page
{
    private StockInPDFlow _flow;

    private StockInPDFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPDFlow(); } return _flow; }
    }

    private void SetData(double loid)
    {
        this.txtLOID.Text = loid.ToString();
        StockInData data = FlowObj.GetData(loid);
        this.lblCode.Text = data.CODE;
        ResetState();
    }

    private void ResetState()
    {
        this.pnlData.Visible = true;
        this.ctlToolbar.Visible = true;
        this.pnlMessage.Visible = false;
        this.btnCancel.Visible = true;
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
    }

    private void setError(string errorMessage)
    {
        this.ctlToolbar.Visible = false;
        this.pnlData.Visible = false;
        this.pnlMessage.Visible = true;
        this.lblError.Text = errorMessage;
        this.btnCancel.Visible = false;
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
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/StockInList.aspx");
    }

    protected void ViewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/ProductList.aspx?loid=" + txtLOID.Text);
    }

    protected void txtBarcode_TextChanged(object sender, EventArgs e)
    {
        StockInProductData data = FlowObj.GetPrductData(this.txtBarcode.Text.Trim(), Constz.ProductType.Type.FG.Code);
        if (data.LOID > 0)
        {
            this.txtProduct.Text = data.LOID.ToString();
            this.lblProductName.Text = data.NAME;
            this.lblUnitName.Text = data.UNITNAME;
            this.txtUnit.Text = data.UNIT.ToString();
            ComboSource.BuildCombo(this.cmbLotNo, "V_STOCKINPD_LIST", "LOTNO", "LOID", "LOTNO", "PRODUCT = " + data.LOID.ToString());
            this.cmbLotNo.Enabled = true;
            this.txtQty.Enabled = true;
            this.txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        }
        else
            setError("ไม่พบสินค้า");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.pnlMessage.Visible)
            ResetState();
        else
        {
            try
            {
                StockInItemData data = new StockInItemData();
                if (this.cmbLotNo.SelectedValue != "")
                {
                    data.REFTABLE = "PDPRODUCT";
                    data.REFLOID = Convert.ToDouble(this.cmbLotNo.SelectedItem.Value);
                    data.LOTNO = this.cmbLotNo.SelectedItem.Text;
                }
                data.PRICE = Convert.ToDouble(this.txtPrice.Text == "" ? "0" : this.txtPrice.Text);
                data.PRODUCT = Convert.ToDouble(this.txtProduct.Text);
                data.QTY = Convert.ToDouble(this.txtQty.Text == "" ? "0" : this.txtQty.Text);
                data.STATUS = Constz.Requisition.Status.Waiting.Code;
                data.STOCKIN = Convert.ToDouble(this.txtLOID.Text);
                data.UNIT = Convert.ToDouble(this.txtUnit.Text == "" ? "0 " : this.txtUnit.Text);

                if (FlowObj.InsertStockInItem(Authz.CurrentUserInfo.UserID, data))
                    ResetState();
                else
                    setError(FlowObj.ErrorMessage);
            }
            catch (InvalidCastException ex)
            {
                setError(ex.Message);
            }

        }
    }
}
