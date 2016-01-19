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

public partial class Transaction_ProductStockoutQuarantineSearch : System.Web.UI.Page
{
    private ProductionFlow _flow;
    public ProductionFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductionFlow(); return _flow; }
    }



    private ProductStockoutQuarantineSearchData GetData()
    {
        ProductStockoutQuarantineSearchData data = new ProductStockoutQuarantineSearchData();
        data.MFGDATEFROM = this.ctlMFGDateFrom.DateValue;
        data.MFGDATETO = this.ctlMFGDateTo.DateValue;
        data.SENDFGDATEFROM = this.ctlSendFGDateFrom.DateValue;
        data.SENDFGDATETO = this.ctlSendFGDateTo.DateValue;
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.LOTNO = this.txtLotNo.Text.Trim();
        return data;
    }

    private void Search()
    {
        this.grvPDOrder.DataSource = FlowObj.GetProductionStockoutQuarantineList(GetData());
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
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[2].Visible = false;
            e.Row.Cells[3].Visible = false;
            e.Row.Cells[4].Visible = false;
            e.Row.Cells[5].Visible = false;
            e.Row.Cells[9].Visible = false;
            e.Row.Cells[10].Visible = false;
            e.Row.Cells[11].Visible = false;
            e.Row.Cells[12].Visible = false;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[2].Text = (e.Row.RowIndex + 1).ToString();

            HyperLink hplLotNo = (HyperLink)e.Row.FindControl("hplLotNo");
            hplLotNo.NavigateUrl = "Production.aspx?PDPLOID=" + e.Row.Cells[1].Text.Trim() + "&PDLOID=" + e.Row.Cells[13].Text.Trim();

            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            //CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductStockoutDetain, Convert.ToDouble(drow["LOID"])) + "return false;";


        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }




    protected void grvPDOrder_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView gView = (GridView)sender;
            GridViewRow gRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
            TableCell tCell = new TableCell();
            tCell.RowSpan = 2;
            tCell.Width = 30;
            gRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "ชื่อผลิตภัณฑ์";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 150;
            gRow.Cells.Add(tCell); 
            tCell = new TableCell();
            tCell.Text = "เลขที่การผลิต";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 80;
            gRow.Cells.Add(tCell); 
            tCell = new TableCell();
            tCell.Text = "วันที่ผลิต";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 80;
            gRow.Cells.Add(tCell); 
            tCell = new TableCell();
            tCell.Text = "ยอดจริง";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 60;
            gRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = "QC Sampling";
            tCell.ColumnSpan = 3;
            tCell.HorizontalAlign = HorizontalAlign.Center;
            gRow.Cells.Add(tCell);

            tCell = new TableCell();
            tCell.Text = "ยอดเสีย";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 60;
            gRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "ยอดจ่าย";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 60;
            gRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "วันที่เข้าคลัง";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 80;
            gRow.Cells.Add(tCell);
            tCell = new TableCell();
            tCell.Text = "หมายเหตุ";
            tCell.HorizontalAlign = HorizontalAlign.Center;
            tCell.RowSpan = 2;
            tCell.Width = 120;
            gRow.Cells.Add(tCell); 

            gView.Controls[0].Controls.AddAt(0, gRow);
        }
    }
}

