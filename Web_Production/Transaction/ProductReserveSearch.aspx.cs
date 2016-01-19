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
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
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
        get { return "chkAllBox(this, '" + this.grvPDReserve.ClientID + "_ctl', '_chkItem')"; }
    }

    private PDReserveSearchData GetData()
    {
        PDReserveSearchData data = new PDReserveSearchData();
        data.CODE = this.txtCode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.LOTNO = this.txtLotNo.Text.Trim();
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        data.REFWAREHOUSE = Convert.ToDouble(this.cmbWarehouse.SelectedItem.Value);
        return data;
    }

    private void Search()
    {
        this.grvPDReserve.DataSource = FlowObj.GetPDRequestList(GetData());
        this.grvPDReserve.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDReserve.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDReserve.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPDReserve.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildStatusRankComboPDReserver(this.cmbStatusFrom);
            ComboSource.BuildStatusRankComboPDReserver(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "", "เลือก", "0");
            Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบบันทึกรายการเพื่อการร้องขอเบิกวัตถุดิบและบรรจุภัณฑ์ใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งคลังวัตถุดิบ?');";
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
        {
            Search();
            Appz.ClientAlert(this, "ยืนยันการลบรายการ");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReserve.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitPDRequisition(GetChecked(), Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ยืนยันการส่งคลัง");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvPDReserve_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            if (FlowObj.CopyPDRequest(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseRequest.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else if (e.CommandName == "cancelpdrequest")
        {
            ArrayList arr = new ArrayList();
            arr.Add(Convert.ToDouble(e.CommandArgument));
            if (FlowObj.UpdatePDRequestStatus(arr, Constz.Requisition.Status.Void.Code, Authz.CurrentUserInfo.UserID))
                Search();
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    protected void grvPDReserve_RowDataBound(object sender, GridViewRowEventArgs e)
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
            ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductMaterialReserve, Convert.ToDouble(drow["LOID"])) + "return false;";
            btnCopy.CommandArgument = drow["LOID"].ToString();
            btnCancel.CommandArgument = drow["LOID"].ToString();

            if (drow["RANK"].ToString() == Constz.Requisition.Status.SendWareHouse.Rank)
                btnCancel.Visible = false;
            else
                btnCancel.Visible = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.DoWaiting.Rank);
            btnCopy.OnClientClick = "return confirm('ยืนยันการคัดลอกใบบันทึกรายการเพื่อการขอเบิกเป็นเลขที่ใหม่');";
        }
    }
}

