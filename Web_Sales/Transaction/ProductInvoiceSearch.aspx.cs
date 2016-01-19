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
public partial class Transaction_ProductInvoiceSearch : System.Web.UI.Page
{
    private InvoiceFlow _flow;
    public InvoiceFlow FlowObj
    {
        get { if (_flow == null) _flow = new InvoiceFlow(); return _flow; }
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
        data.RESERVEFROM = this.ctlReserveFrom.DateValue;
        data.RESERVETO = this.ctlReserveTo.DateValue;
        data.DATEFROM = this.ctlReqFrom.DateValue;
        data.DATETO = this.ctlReqTo.DateValue;
        data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedItem.Value);
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        ComboSource.BuildInvoiceStatusRankCombo(combo);
    }

    private void Search()
    {
        this.grvRequisition.DataSource = FlowObj.GetRequisitionList(GetData());
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
            this.cmbStatusTo.SelectedIndex = this.cmbStatusTo.Items.IndexOf(this.cmbStatusTo.Items.FindByValue(Constz.Requisition.Status.Reserve.Rank));

            //this.cmbStatusTo.SelectedValue = Constz.Requisition.Status.Reserve.Code;
            ComboSource.BuildCombo(this.cmbRequisitionType, "V_REQTYPE_INVOICE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบกำกับภาษีใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งคลังสำเร็จรูป?');";

        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
        {
            Search();
            Appz.ClientAlert(this, "ลบข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void NewClick(object sender, EventArgs e)
    {
        //ProductReserveData data = new ProductReserveData();
        //data.ACTIVE = Constz.ActiveStatus.Active;
        //data.CODE = "";
        //data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ11;
        //data.STATUS = Constz.Requisition.Status.Waiting.Code;
        //data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;

        //if (FlowObj.NewRequisition(Authz.CurrentUserInfo.UserID, data))
        //    Response.Redirect(Constz.HomeFolder + "Transaction/ProductInvoice.aspx?loid=" + FlowObj.LOID.ToString());
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductInvoice.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateRequisitionStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.Warehouse.ToString(), Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ส่งคลังสำเร็จรูปเรียบร้อยแล้ว");

        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
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
            //e.Row.Cells[2].Text = (e.Row.RowIndex + 1).ToString();

            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.InvoiceFull, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["LOID"].ToString();

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank || (drow["RANK"].ToString() == Constz.Requisition.Status.Reserve.Rank && drow["INVCODE"].ToString() != ""));
            if (Convert.ToDouble(drow["REQUISITIONTYPE"]) != Constz.Requisition.RequisitionType.REQ01) e.Row.Cells[7].Text = "";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

}
