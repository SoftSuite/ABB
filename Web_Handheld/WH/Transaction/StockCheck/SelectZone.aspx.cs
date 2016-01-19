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

public partial class WH_Transaction_StockCheck_SelectZone : System.Web.UI.Page
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
        ComboSource.BuildCombo(this.cmbLocation, "LOCATION", "NAME", "LOID", "NAME", "SUBSTR(CODE,0,2) = '" + Constz.Warehouse.Type.WH.Code + "' ");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetData(Request["loid"] == null ? 0 : Convert.ToDouble(Request["loid"]));
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/StockCheckList.aspx");
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/AddProduct.aspx?loid=" + txtLOID.Text + "&location=" + this.cmbLocation.SelectedItem.Value);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockCheck/ProductList.aspx?loid=" + this.txtLOID.Text);
    }
}
