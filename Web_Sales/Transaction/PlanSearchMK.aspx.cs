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

public partial class Transaction_PlanSearchMK : System.Web.UI.Page
{
    private PlanSaleFlow _flow;
    public PlanSaleFlow FlowObj
    {
        get { if (_flow == null) _flow = new PlanSaleFlow(); return _flow; }
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

    private PlanOrderSearchData GetData()
    {
        PlanOrderSearchData data = new PlanOrderSearchData();
        data.YEARFROM = this.txtYearFrom.Text.Trim();
        data.YEARTO = this.txtYearTo.Text.Trim();
        data.CREATEFROM = ctlCreateFrom.DateValue;
        data.CREATETO = ctlCreateTo.DateValue;
        //data.CONFIRMFROM = ctlConfirmFrom.DateValue;
        //data.CONFIRMTO = ctlConfirmTo.DateValue;
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
        this.grvPlan.DataSource = FlowObj.GetPlanMarketList(GetData());
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

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบแผนการตลาดใช่หรือไม่?');";
            //this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการทำแผนการจำหน่ายสินค้า');";

            string script = "";
            script += "document.getElementById('" + this.txtNewLOID.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PlanNewMK.aspx', '435', '135');";
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
            data.PLANTYPE = Constz.PlanType.MK;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.YEAR = ret[0];

            if (FlowObj.InsertPlan(Authz.CurrentUserInfo.UserID, data))
                Response.Redirect(Constz.HomeFolder + "Transaction/PlanOrderMK.aspx?loid=" + FlowObj.LOID.ToString());
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
        //if (FlowObj.CommitPlan(Authz.CurrentUserInfo.UserID, GetChecked()))
        //{
        //    Search();
        //    Appz.ClientAlert(this, "ยืนยันรายการเรียบร้อยแล้ว");
        //}
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
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
            ImageButton btnPrint2 = (ImageButton)e.Row.Cells[1].FindControl("btnPrint2");
            ImageButton btnPrint3 = (ImageButton)e.Row.Cells[1].FindControl("btnPrint3");
            ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            //ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");
            TextBox txtNew = (TextBox)e.Row.Cells[1].FindControl("txtNew");

            string temp = "";
            temp = "paramfield1=YEAR";
            temp += "&paramvalue1=" + drow["YEAR"].ToString().Trim();

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript("ReportMarketPlan", temp) + " return false;";
            btnPrint2.OnClientClick = ABB.Global.Appz.ReportScript("ReportMarketSummary", temp) + " return false;";
            btnPrint3.OnClientClick = ABB.Global.Appz.ReportScript("ReportMarketDiff", temp) + " return false;";

           // btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductReserve, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["YEAR"].ToString();
            btnPrint2.CommandArgument = drow["YEAR"].ToString();
            btnPrint3.CommandArgument = drow["YEAR"].ToString();
            btnCopy.CommandArgument = drow["LOID"].ToString();
            //btnCancel.CommandArgument = drow["LOID"].ToString();
            string script = "";
            script += "if (confirm('ต้องการคัดลอกแผนการตลาดปี พ.ศ." + drow["YEAR"].ToString() + " ใช่หรือไม่?')) ";
            script += "{";
            script += "  document.getElementById('" + txtNew.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PlanNewMK.aspx?copyplan=" + drow["LOID"].ToString() + "', '425', '125');";
            script += "  return ('undefined' !=  document.getElementById('" + txtNew.ClientID + "').value && '' != document.getElementById('" + txtNew.ClientID + "').value);  ";
            script += "} else { return false; }";

            btnCopy.OnClientClick = script;
            //btnCancel.OnClientClick = "return confirm('ต้องการยกเลิกแผนการตลาดสินค้าปี พ.ศ." + drow["YEAR"].ToString() + " ใช่หรือไม่?')";

            //if (drow["RANK"].ToString() == Constz.Requisition.Status.Approved.Rank)
            //    btnCancel.Visible = true;
            //else
            //    btnCancel.Visible = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);
        }
    }

    protected void grvPlan_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            TextBox txtNew = (TextBox)this.grvPlan.Rows[((GridViewRow)((ImageButton)e.CommandSource).Parent.Parent).RowIndex].Cells[1].FindControl("txtNew");
            double plan = Convert.ToDouble(e.CommandArgument);
            string[] ret = txtNew.Text.Split('#');
            if (FlowObj.CopyPlan(Authz.CurrentUserInfo.UserID, Convert.ToDouble(ret[0]), ret[1], plan))
                Response.Redirect(Constz.HomeFolder + "Transaction/PlanOrderMK.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        //else if (e.CommandName == "cancelItem")
        //{
        //    double plan = Convert.ToDouble(e.CommandArgument);
        //    if (FlowObj.CancelPlan(Authz.CurrentUserInfo.UserID, plan))
        //        Search();
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
    }

}
