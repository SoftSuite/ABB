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
using ABB.Data.Handheld.FG;
using ABB.Global;

public partial class FG_Transaction_StockOut_ProductDetail : System.Web.UI.Page
{
    private StockOutFGFlow _flow;

    private StockOutFGFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockOutFGFlow(); } return _flow; }
    }

    private void SetData(double stockOutItem)
    {
        StockOutItemFGData data = FlowObj.GetStockOutItemData(stockOutItem);
        this.txtLOID.Text = data.STOCKOUT.ToString();
        this.lblCode.Text = data.CODE;
        this.lblReqCode.Text = data.REQCODE;
        this.lblDocName.Text = data.DOCNAME;
        this.lblProductName.Text = data.PRODUCTNAME;
        this.lblLotNo.Text = data.LOTNO;
        this.lblQty.Text = data.QTY.ToString(Constz.IntFormat) + " " + data.UNITNAME;
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
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockOut/ProductList.aspx?loid=" + this.txtLOID.Text);
    }

}
