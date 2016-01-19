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
using ABB.Global;
using ABB.Flow.Handheld.FG;

public partial class FG_Transaction_StockInPO_NewPO : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string where = "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND LOID IN (";
            where += "SELECT SUPPLIER FROM PDORDER ";
            where += "WHERE ORDERTYPE = '" + Constz.OrderType.PO.Code + "' AND STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND LOID NOT IN ";
            where += "(SELECT POI.PDORDER ";
            where += "FROM STOCKINITEM STI INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' ";
            where += "INNER JOIN STOCKIN ST ON ST.LOID = STI.STOCKIN WHERE ST.STATUS <> '" + Constz.Requisition.Status.Void.Code + "') AND LOID IN ";
            where += "(SELECT PO.LOID ";
            where += "FROM PDORDER PO INNER JOIN POITEM POI ON PO.LOID= POI.PDORDER ";
            where += "INNER JOIN PRODUCT P ON P.LOID = POI.PRODUCT ";
            where += "INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            where += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE WHERE PT.TYPE = '" + Constz.ProductType.Type.FG.Code + "') ) ";
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", where);
        }
    }

    private void SaveData()
    {
        StockInData data = new StockInData();
        data.ACCCODE = "";
        data.DOCTYPE = Constz.DocType.RecProduct.LOID;
        data.INVNO = this.txtInvNo.Text.Trim();
        data.RECEIVEDATE = DateTime.Now;
        data.RECEIVER = Authz.CurrentUserInfo.Warehouse;
        data.SENDER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        data.STATUS = Constz.Requisition.Status.Waiting.Code;
        if (FlowObj.InsertStockIn(Authz.CurrentUserInfo.UserID, data))
        {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/SelectPO.aspx?loid=" + FlowObj.LOID.ToString());
        }
        else
        {
            this.pnlError.Visible = true;
            this.pnlMain.Visible = false;
            this.lblError.Text = FlowObj.ErrorMessage;
            this.btnSave.Text = "ตกลง";
            this.btnCancel.Visible = false;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (this.pnlMain.Visible)
            SaveData();
        else
        {
            this.pnlError.Visible = false;
            this.pnlMain.Visible = true;
            this.lblError.Text = "";
            this.btnSave.Text = "บันทึก";
            this.btnCancel.Visible = true;
        }
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/StockInList.aspx");
    }
}
