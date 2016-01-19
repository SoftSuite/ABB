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
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Handheld.Common.StockIn;
using ABB.Global;
using ABB.Flow.Handheld.FG;

public partial class FG_Transaction_StockInPO_SelectPO : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SetData(double loid)
    {
        this.txtLOID.Text = loid.ToString();
        StockInPOData data = FlowObj.GetStockInPOData(loid);
        this.lblCode.Text = data.CODE;
        this.lblInvNo.Text = data.INVNO;
        this.lblSupplierName.Text = data.SUPPLIERNAME;
        string strWhere = "ORDERTYPE = '" + Constz.OrderType.PO.Code + "' AND STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
        strWhere += "AND SUPPLIER = " + data.SENDER.ToString() + " AND LOID NOT IN (";
        strWhere += "SELECT POI.PDORDER FROM STOCKINITEM STI INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' ";
        strWhere += "INNER JOIN STOCKIN ST ON ST.LOID = STI.STOCKIN WHERE ST.STATUS <> '" + Constz.Requisition.Status.Void.Code + "') ";
        strWhere += "AND LOID IN (";
        strWhere += "SELECT PO.LOID ";
        strWhere += "FROM PDORDER PO INNER JOIN POITEM POI ON PO.LOID= POI.PDORDER ";
        strWhere += "INNER JOIN PRODUCT P ON P.LOID = POI.PRODUCT ";
        strWhere += "INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
        strWhere += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
        strWhere += "WHERE PT.TYPE = '" + Constz.ProductType.Type.FG.Code + "') ";

        ComboSource.BuildCombo(this.cmbPR, "PDORDER", "CODE", "LOID", "CODE", strWhere, "เลือก","0");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetData(Convert.ToDouble(Request["LOID"] == null ? "0" : Request["LOID"]));
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.cmbPR.SelectedItem.Value != "0")
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/AddProduct.aspx?loid=" + this.txtLOID.Text + "&pdorder=" + this.cmbPR.SelectedItem.Value);
    }

    protected void ViewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/ProductDetail.aspx?loid=" + this.txtLOID.Text );
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/StockInList.aspx");
    }
}
