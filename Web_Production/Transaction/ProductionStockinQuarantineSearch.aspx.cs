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
using ABB.Global;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
public partial class Transaction_ProductionStockinQuarantineSearch : System.Web.UI.Page
{
    private ProductionFlow _flow;
    public ProductionFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductionFlow(); return _flow; }
    }



    private ProductStockinQuarantineSearchData GetData()
    {
        ProductStockinQuarantineSearchData data = new ProductStockinQuarantineSearchData();
        data.MFGDATE= this.ctlMFGDate.DateValue;
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.LOTNO = this.txtLotNo.Text.Trim();
        return data;
    }

    private void Search()
    {
        this.grvPDOrder.DataSource = FlowObj.GetProductionStockinQuarantineList(GetData());
        this.grvPDOrder.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            //Search();

        }
    }



    protected void grvPDOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grvPDOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {

        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[2].Text = (e.Row.RowIndex + 1).ToString();

            HyperLink hplLotNo = (HyperLink)e.Row.FindControl("hplLotNo");
            hplLotNo.NavigateUrl = "Production.aspx?PDPLOID=" + e.Row.Cells[1].Text.Trim() + "&PDLOID=" + e.Row.Cells[2].Text.Trim();

            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            //CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductionStockinQuarantine, Convert.ToDouble(drow["LOID"])) + "return false;";
            //HyperLink lnkProduction = (HyperLink)e.Row.Cells[4].FindControl("lnkProduction");
            //lnkProduction.NavigateUrl = Constz.HomeFolder + "Transaction/Production.aspx?PDLOID=" + drow["PRODUCT"].ToString() + "&PDPLOID=" + drow["LOID"].ToString();

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }
}

