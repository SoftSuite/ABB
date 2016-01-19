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

public partial class FG_Transaction_StockInPD_ProductDetail : System.Web.UI.Page
{
    private StockInPDFlow _flow;

    private StockInPDFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPDFlow(); } return _flow; }
    }

    private void SetData(double stockInItem)
    {
        StockInProductDetailData data = FlowObj.GetProductDetail(stockInItem);
        this.txtLOID.Text = data.STOCKIN.ToString();
        this.lblCode.Text = data.CODE;
        this.lblLotNo.Text = data.LOTNO;
        if (data.MFGDATE.Year != 1) this.lblDate.Text = data.MFGDATE.ToString(Constz.DateFormat);
        this.lblProductName.Text = data.PRODUCTNAME;
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
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/ProductList.aspx?loid=" + this.txtLOID.Text);
    }

}
