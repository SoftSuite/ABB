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

public partial class FG_Transaction_StockInPO_ProductDetail : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SetData(double stockInItem)
    {
        StockInPOProductDetailData data = FlowObj.GetProductPODetail(stockInItem);
        this.txtLOID.Text = data.LOID.ToString();
        this.lblCode.Text = data.CODE;
        this.lblOrderCode.Text = data.ORDERCODE;
        if (data.ORDERDATE.Year != 1) this.lblOrderDate.Text = data.ORDERDATE.ToString(Constz.DateFormat);
        this.lblProductName.Text = data.NAME;
        this.lblOrderQty.Text = data.ORDERQTY.ToString(Constz.IntFormat) + " " + data.UNITNAME;
        this.lblInvNo.Text = data.INVNO;
        this.lblSupplierName.Text = data.SUPPLIERNAME;
        this.lblQty.Text = data.QTY.ToString(Constz.IntFormat) + " " + data.UNITNAME;
        this.lblQCQty.Text = data.QCQTY.ToString(Constz.IntFormat) + " " + data.UNITNAME;
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
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/ProductList.aspx?loid=" + this.txtLOID.Text);
    }

}
