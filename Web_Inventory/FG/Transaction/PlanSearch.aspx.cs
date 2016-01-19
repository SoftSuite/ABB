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
using ABB.Data.Inventory.FG;
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class FG_Transaction_PlanSearch : System.Web.UI.Page
{
    private PlanInventoryFlow _flow;
    public PlanInventoryFlow FlowObj
    {
        get { if (_flow == null) _flow = new PlanInventoryFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPlan.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPlan.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPlan.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPlan.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    private PlanSearchData GetData()
    {
        PlanSearchData data = new PlanSearchData();
        data.YEARFROM = this.txtYearFrom.Text.Trim();
        data.YEARTO = this.txtYearTo.Text.Trim();
        data.CREATEFROM = ctlCreateFrom.DateValue;
        data.CREATETO = ctlCreateTo.DateValue;
        data.CONFIRMFROM = ctlConfirmFrom.DateValue;
        data.CONFIRMTO = ctlConfirmTo.DateValue;
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
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
        this.grvPlan.DataSource = FlowObj.GetPlanList(GetData());
        this.grvPlan.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlUtil.SetYearTextbox(this.txtYearFrom);
            ControlUtil.SetYearTextbox(this.txtYearTo);
            SetRequisitionStatusCombo(this.cmbStatusFrom);
            SetRequisitionStatusCombo(this.cmbStatusTo);
            Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบแผนการสั่งซื้อใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการทำแผนการสั่งซื้อสินค้า');";

            string script = "";
            script += "document.getElementById('" + this.txtNewLOID.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "FG/Transaction/PlanNew.aspx', '435', '135');";
            script += "if ('undefined' ==  document.getElementById('" + this.txtNewLOID.ClientID + "').value || '' == document.getElementById('" + this.txtNewLOID.ClientID + "').value) ";
            script += "{ return false; } ";

            this.ctlToolbar.ClientClickNew = script;
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        ArrayList arr = GetChecked();
        if (arr.Count > 0)
        {
            if (FlowObj.DeletePlan(arr))
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
        string[] ret = this.txtNewLOID.Text.Split('#');
        try
        {
            PlanData data = new PlanData();
            data.ACTIVE = Constz.ActiveStatus.InActive;
            data.DESCRIPTION = ret[1];
            data.PLANTYPE = Constz.PlanType.FG;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.YEAR = ret[0];

            if (FlowObj.InsertPlan(Authz.CurrentUserInfo.UserID, data))
                Response.Redirect(Constz.HomeFolder + "FG/Transaction/Plan.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.CommitPlan(Authz.CurrentUserInfo.UserID, GetChecked()))
        {
            Search();
            Appz.ClientAlert(this, "ยืนยันรายการเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvPlan_RowDataBound(object sender, GridViewRowEventArgs e)
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
            ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserve, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["LOID"].ToString();
            btnCancel.CommandArgument = drow["LOID"].ToString();
            btnCancel.OnClientClick = "return confirm('ต้องการยกเลิกแผนการสั่งซื้อสินค้าปี พ.ศ." + drow["YEAR"].ToString() + " ใช่หรือไม่?')";

            if (drow["RANK"].ToString() == Constz.Requisition.Status.Approved.Rank)
                btnCancel.Visible = true;
            else
                btnCancel.Visible = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);
        }
    }

    protected void grvPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cancelItem")
        {
            double plan = Convert.ToDouble(e.CommandArgument);
            if (FlowObj.CancelPlan(Authz.CurrentUserInfo.UserID, plan))
                Search();
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

}
