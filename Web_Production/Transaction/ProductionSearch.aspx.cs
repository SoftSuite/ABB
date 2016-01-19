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
using ABB.Flow.Production;
using ABB.Flow;
using ABB.Data;
using ABB.Global;

/// <summary>
/// Create by: Nang
/// Create Date: 20 Feb 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 

public partial class Transaction_ProductionSearch : System.Web.UI.Page
{
    string str_dateFrom ="";
    string str_dateTo = "";
    private PDProductFlow _flow;
    public PDProductFlow FlowObj
    {
        get { if (_flow == null) _flow = new PDProductFlow(); return _flow; }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ToolbarControl1.ClientClickDelete = "return confirm('คุณต้องการลบข้อมูลบันทึกการผลิตผลิตภัณฑ์จากสมุนไพรที่เลือกใช่หรือไม่?');";
    }
    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.gvResult.ClientID + "_ctl', '_chkItem')"; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SearchData();
        }  
    }

    private string setDateFrom()
    {
        string  str = "";
        str = PkDateFrom.DateValue.Day.ToString() + '/';
        str += PkDateFrom.DateValue.Month.ToString() + '/';
        str += PkDateFrom.DateValue.Year.ToString();
        return str;
    }

    private string setDateTo()
    {
        string str = "";
        str = PkDateTo.DateValue.Day.ToString() + '/';
        str += PkDateTo.DateValue.Month.ToString() + '/';
        str += PkDateTo.DateValue.Year.ToString();
        return str;
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    private void SearchData()
    {
        ArrayList arr = new ArrayList();
        str_dateFrom = setDateFrom();
        str_dateTo = setDateTo();
        if (str_dateFrom == "1/1/1" && str_dateTo != "1/1/1")
        {
            Appz.ClientAlert(Page, "กรุณากรอกช่วงวันที่ผลิต");
            return;
        }

        if (str_dateFrom != "1/1/1" && str_dateTo == "1/1/1")
        {
            Appz.ClientAlert(Page, "กรุณากรอกช่วงวันที่ผลิต");
            return;
        }
        arr = PDProductFlow.GetPDProductSearch(txtLotNo.Text.Trim(), str_dateFrom.ToString(), str_dateTo.ToString(), txtProductName.Text.Trim());
        this.gvResult.DataSource = arr;
        gvResult.AllowPaging = true;
        gvResult.PageSize = 900000;
        this.gvResult.DataBind();
    }


    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //DataRow dr = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ABB.Data.Production.PDProductSearchData drow = (ABB.Data.Production.PDProductSearchData)e.Row.DataItem;
            HyperLink hplLotNo = (HyperLink)e.Row.FindControl("hplLotNo");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            ImageButton btnPrintL = (ImageButton)e.Row.Cells[1].FindControl("btnPrintL");
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            hplLotNo.NavigateUrl = "Production.aspx?PDPLOID=" + e.Row.Cells[2].Text.Trim() + "&PDLOID=" + e.Row.Cells[3].Text.Trim();
            double  dd = Convert.ToDouble(e.Row.Cells[3].Text.Trim() == "" ? "0" : e.Row.Cells[3].Text.Trim());
            //string report = FlowObj.GetReport(dd);
            string reportLand = FlowObj.GetReportLand(dd);
            //btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(report, Convert.ToDouble(e.Row.Cells[2].Text.Trim() == "" ? "0" : e.Row.Cells[2].Text.Trim())) + "return false;";
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.Productionherb01, Convert.ToDouble(e.Row.Cells[2].Text.Trim() == "" ? "0" : e.Row.Cells[2].Text.Trim())) + "return false;";
            btnPrintL.OnClientClick = ABB.Global.Appz.ReportScript(reportLand, Convert.ToDouble(e.Row.Cells[2].Text.Trim() == "" ? "0" : e.Row.Cells[2].Text.Trim())) + "return false;";
            chk.Enabled = (drow.RANK.ToString() == Constz.ProductionStatus.Status.WA.Rank);
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect("Production.aspx?PDPLOID=null");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        DeleteData();
    }

    public void DeleteData()
    {
        double PDPLOID;
        ArrayList arrLOID = new ArrayList();
        PDProductFlow csFlow = new PDProductFlow();
        bool ret = true;

        for (int i = 0; i < gvResult.Rows.Count; i++)
        {
            CheckBox chkItem = (CheckBox)gvResult.Rows[i].Cells[0].FindControl("chkItem");
            if (chkItem.Checked == true)
            {
                PDPLOID = Convert.ToDouble(gvResult.Rows[i].Cells[2].Text);
                arrLOID.Add(PDPLOID);
            }
        }
        if (arrLOID.Count != 0)
        {
            ret = csFlow.DeleteData(arrLOID);
        }

        if (ret == true)
        {
            Appz.ClientAlert(Page, "ทำการลบข้อมูลบันทึกการผลิตผลิตภัณฑ์จากสมุนไพรเรียบร้อย");
            SearchData();
        }
        else
            Appz.ClientAlert(Page, csFlow.ErrorMessage);
    }
}
