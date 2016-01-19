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

public partial class FG_Transaction_StockCheck_ProductDetail : System.Web.UI.Page
{
    private StockCheckBatchFlow _flow;

    private StockCheckBatchFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockCheckBatchFlow(); } return _flow; }
    }

    private void SetData(double stockCheckItem)
    {
        StockCheckBatchItemData data = FlowObj.GetStockCheckItemData(stockCheckItem);
        this.txtLOID.Text = data.STOCKCHECK.ToString();
        this.lblBatchNo.Text = data.BATCHNO;
        this.lblCheckDate.Text = data.CHECKDATE.ToString(Constz.DateFormat);
        this.lblWarehouseName.Text = data.WAREHOUSENAME;
        this.lblZoneName.Text = data.LOCATIONNAME;
        this.lblProductName.Text = data.PRODUCTNAME;
        this.lblLotNo.Text = data.LOTNO;
        this.lblQty.Text = data.COUNTQTY.ToString(Constz.IntFormat) + " " + data.UNITNAME;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetData(Request["loid"] == null ? 0 : Convert.ToDouble(Request["loid"]));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockCheck/ProductList.aspx?loid=" + this.txtLOID.Text);
    }

}
