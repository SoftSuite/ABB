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
using ABB.Data.Inventory.FG;
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class FG_Transaction_StockoutSearch : System.Web.UI.Page
{
    private StockoutFlow _flow;
    public StockoutFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockoutFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private StockOutFGSearchData GetData()
    {
        StockOutFGSearchData data = new StockOutFGSearchData();
        data.REQUISITIONCODE = this.txtReqCode.Text.Trim();
        data.STOCKOUTCODE = this.txtStockCode.Text.Trim();
        data.CUSTOMER = Convert.ToDouble(this.cmbCustomer.SelectedItem.Value);
        data.CREATEFROM = this.ctlApproveDateFrom.DateValue;
        data.CREATETO = this.ctlApproveDateTo.DateValue;
        data.RESERVEDATEFROM = this.ctlRequestDateFrom.DateValue;
        data.RESERVEDATETO = this.ctlRequestDateTo.DateValue;
        data.DOCTYPE = Convert.ToDouble(this.cmbDocType.SelectedItem.Value);
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        data.CREATEBY = this.txtCreateby.Text;
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        combo.Items.Clear();
        ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
        combo.Items.Add(item);
    }

    private void Search()
    {
        this.grvRequisition.DataSource = FlowObj.GetStockOutList(GetData());
        this.grvRequisition.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvRequisition.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvRequisition.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvRequisition.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetRequisitionStatusCombo(this.cmbStatusFrom);
            SetRequisitionStatusCombo(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbDocType, "V_REQTYPE_STOCKOUT", "DOCTYPENAME", "DOCTYPE", "DOCTYPENAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbCustomer, "CUSTOMER", "NAME", "LOID", "NAME", "LOID IN (SELECT RECEIVER FROM STOCKOUT INNER JOIN V_REQTYPE_STOCKOUT ST ON ST.DOCTYPE = STOCKOUT.DOCTYPE)", "ทั้งหมด", "0");
            //Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบเบิกสินค้าออกใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันส่งคลังใช่หรือไม่?');";
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        ArrayList arr = GetChecked();
        if (arr.Count > 0)
        {
            if (FlowObj.DeleteData(arr))
            {
                Search();
                Appz.ClientAlert(this, "ลบรายการเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else
            Appz.ClientAlert(this, "กรุณาเลือกรายการที่ต้องการ");
    }

    protected void NewClick(object sender, EventArgs e)
    {
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/Stockout.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateStockOutStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "อนุมัติการเบิกเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
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

            double docID = Convert.ToDouble(drow["DOCLOID"]);
            if (docID == Constz.DocType.Reserve.LOID)
                btnPrint.Visible = false;

            else if (docID == Constz.DocType.ReqOrgSupport.LOID)
                btnPrint.Visible = false;

            else if (docID == Constz.DocType.ReqFair.LOID)
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockOutBorrow, Convert.ToDouble(drow["LOID"])) + " return false;";

            else if (docID == Constz.DocType.ReqDistribute.LOID || docID == Constz.DocType.ReqProduct.LOID)
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockOut, Convert.ToDouble(drow["LOID"])) + " return false;";

            else
                btnPrint.OnClientClick = "return false; ";

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

}


