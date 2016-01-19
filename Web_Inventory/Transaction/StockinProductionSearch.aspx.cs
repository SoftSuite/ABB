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
using ABB.Data.Inventory;
using ABB.Flow;
using ABB.Flow.Inventory;
using ABB.Global;

public partial class Transaction_StockinProductionSearch : System.Web.UI.Page
{
    private StockinProductionFlow _flow;
    public StockinProductionFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinProductionFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvStockIn.ClientID + "_ctl', '_chkItem')"; }
    }

    private StockinProductSearchData GetData()
    {
        StockinProductSearchData data = new StockinProductSearchData();
        data.PRODUCETYPE = Request["producetype"];
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.LOTNOFROM = this.txtLotNoFrom.Text.Trim();
        data.LOTNOTO = this.txtLotNoTo.Text.Trim();
        data.CREATEONFROM = this.ctlProduceDateFrom.DateValue;
        data.CREATEONTO = this.ctlProduceDateTo.DateValue;
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
        return data;
    }

    private void SearchWH()
    {
        this.grvStockIn.DataSource = FlowObj.GetStockInListWH(GetData());
        this.grvStockIn.DataBind();
    }
    private void SearchFG()
    {
        this.grvStockIn.DataSource = FlowObj.GetStockInListFG(GetData());
        this.grvStockIn.DataBind();
    }
    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvStockIn.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvStockIn.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvStockIn.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Search();
            if (Request["producetype"] == Constz.ProductType.Type.WH.Code)
            {
                SearchWH();
                this.lblHeader.Text = Constz.DocType.DelRaw.NAME;
                this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบบันทึกรายการ" + Constz.DocType.DelRaw.NAME + "ใช่หรือไม่?');";
            }
            else
            {
                SearchFG();
                this.lblHeader.Text = Constz.DocType.DelProduct.NAME;
                this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบบันทึกรายการ" + Constz.DocType.DelProduct.NAME + "ใช่หรือไม่?');";
            }
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
        {
            if (Request["producetype"] == Constz.ProductType.Type.WH.Code)
            {
                SearchWH();
            }
            else
            {
                SearchFG();
            }
        }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/StockinProduction.aspx?producetype=" + Request["producetype"]);
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitPDStockin(GetChecked(), Authz.CurrentUserInfo.UserID))
        {
            if (Request["producetype"] == Constz.ProductType.Type.WH.Code)
            {
                SearchWH();
            }
            else
            {
                SearchFG();
            }
            Appz.ClientAlert(this, "ยืนยันการส่งคลัง");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (Request["producetype"] == Constz.ProductType.Type.WH.Code)
        {
            SearchWH();
        }
        else
        {
            SearchFG();
        }
    }

    protected void grvStockIn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        
    }

    protected void grvStockIn_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            if (drow["PRODUCETYPE"].ToString() == Constz.ProductType.Type.WH.Code)
            {
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockinProductWH, Convert.ToDouble(drow["LOID"])) + "return false;";
            }
            else
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockInProduction, Convert.ToDouble(drow["LOID"])) + "return false;";

            HyperLink lnkStockIn = (HyperLink)e.Row.Cells[4].FindControl("lnkStockIn");
            lnkStockIn.Text = drow["CODE"].ToString();
            lnkStockIn.NavigateUrl = Constz.HomeFolder + "Transaction/StockinProduction.aspx?producetype=" + drow["PRODUCETYPE"].ToString() + "&loid=" + drow["LOID"].ToString();

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);
            
        }
    }
}


