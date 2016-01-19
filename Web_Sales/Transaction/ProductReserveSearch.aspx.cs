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
using ABB.Flow;
using ABB.Flow.Sales;
using ABB.Global;

public partial class Transaction_ProductReserveSearch : System.Web.UI.Page
{
    private ProductReserveFlow _flow;
    public ProductReserveFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductReserveFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private ProductReserveSearchData GetData()
    {
        ProductReserveSearchData data = new ProductReserveSearchData();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.CUSTOMERNAME = this.txtName.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedItem.Value);
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        //data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        combo.Items.Clear();
        ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Reserve.Name, Constz.Requisition.Status.Reserve.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
        combo.Items.Add(item);
        //item = new ListItem(Constz.Requisition.Status.Finish.Name, Constz.Requisition.Status.Finish.Rank);
        //combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
        combo.Items.Add(item);
    }

    private void Search(string sortField)
    {
        this.grvRequisition.DataSource = FlowObj.GetRequisitionList(GetData(), sortField);
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
            ComboSource.BuildCombo(this.cmbRequisitionType, "V_REQTYPE_RESERVE", "NAME", "LOID", "NAME", "", "ทั้งหมด","0");
            Search("");

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบรับคำสั่งซื้อ/สั่งจองใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการสั่งซื้อ/สั่งจอง');";
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        ArrayList arr = GetChecked();
        if (arr.Count > 0)
        {
            if (FlowObj.DeleteData(arr))
            {
                Search("");
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
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReserve.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.CommitData(GetChecked(), Authz.CurrentUserInfo.UserID))
        {
            Search("");
            Appz.ClientAlert(this, "ยืนยันการสั่งซื้อ/สั่งจองแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            if (FlowObj.CopyRequisition(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Response.Redirect(Constz.HomeFolder + "Transaction/ProductReserve.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else if (e.CommandName == "cancelItem")
        {
            if (FlowObj.CancelData(Convert.ToDouble(e.CommandArgument), Authz.CurrentUserInfo.UserID))
            {
                Search("");
                Appz.ClientAlert(this, "ยกเลิกรายการเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
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
            ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");

            if (drow["REQUISITIONTYPE"].ToString() == Constz.Requisition.RequisitionType.REQ01.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserve, Convert.ToDouble(drow["LOID"])) + " return false;";
            else if (drow["REQUISITIONTYPE"].ToString() == Constz.Requisition.RequisitionType.REQ02.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserveSale, Convert.ToDouble(drow["LOID"])) + " return false;";
            else if (drow["REQUISITIONTYPE"].ToString() == Constz.Requisition.RequisitionType.REQ03.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductRequestInShop, Convert.ToDouble(drow["LOID"])) + " return false;";
            else if (drow["REQUISITIONTYPE"].ToString() == Constz.Requisition.RequisitionType.REQ10.ToString())
                btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductRequestInShop, Convert.ToDouble(drow["LOID"])) + " return false;";

            btnCopy.CommandArgument = drow["LOID"].ToString();
            btnCancel.CommandArgument = drow["LOID"].ToString();

            if (drow["RANK"].ToString() == Constz.Requisition.Status.Approved.Rank)
            {
                btnCancel.Visible = true;
                btnCancel.OnClientClick = "return confirm('ต้องการยกเลิกรายการเลขที่ " + drow["CODE"].ToString() + " ใช่หรือไม่?'); ";
                if (Convert.ToDouble(drow["CNT"]) > 0) btnCancel.OnClientClick = "alert('ไม่สามารถยกเลิกรายการได้ เนื่องจากสร้างใบเบิกสินค้าแล้ว'); return false;";
            }
            else
                btnCancel.Visible = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search("");
    }

    protected void grvRequisition_Sorting(object sender, GridViewSortEventArgs e)
    {
        Search(e.SortExpression);
    }
}
