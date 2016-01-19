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
using ABB.Flow.Inventory.WH;
using ABB.Data;
using ABB.Data.Inventory.WH;
using ABB.Flow.Reports;

public partial class WH_Transaction_Plan : System.Web.UI.Page
{
    public string space = "0"; //"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
    private PlanWHFlow _flow;

    private PlanWHFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PlanWHFlow(); } return _flow; }
    }

    private void SetProductGroup()
    {
        ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "PRODUCTTYPE = " + this.cmbProductType.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
    }

    private void SetMonthCombo()
    {
        this.cmbMonth.Items.Add(new ListItem("มกราคม", "1"));
        this.cmbMonth.Items.Add(new ListItem("กุมภาพันธ์", "2"));
        this.cmbMonth.Items.Add(new ListItem("มีนาคม", "3"));
        this.cmbMonth.Items.Add(new ListItem("เมษายน", "4"));
        this.cmbMonth.Items.Add(new ListItem("พฤษภาคม", "5"));
        this.cmbMonth.Items.Add(new ListItem("มิถุนายน", "6"));
        this.cmbMonth.Items.Add(new ListItem("กรกฎาคม", "7"));
        this.cmbMonth.Items.Add(new ListItem("สิงหาคม", "8"));
        this.cmbMonth.Items.Add(new ListItem("กันยายน", "9"));
        this.cmbMonth.Items.Add(new ListItem("ตุลาคม", "10"));
        this.cmbMonth.Items.Add(new ListItem("พฤศจิกายน", "11"));
        this.cmbMonth.Items.Add(new ListItem("ธันวาคม", "12"));
    }

    private void SetProductStatus()
    {
        this.cmbProductStatus.Items.Add(new ListItem("ทั้งหมด", ""));
        this.cmbProductStatus.Items.Add(new ListItem("วัตถุดิบที่สั่งผลิต", Constz.PlanProductStatus.Produce));
        this.cmbProductStatus.Items.Add(new ListItem("วัตถุดิบที่สั่งซื้อ", Constz.PlanProductStatus.Purchase));
        this.cmbProductStatus.Items.Add(new ListItem("วัตถุดิบที่ตำกว่า Min", Constz.PlanProductStatus.Minimum));
    }

    private void ResetState(double plan)
    {
        SetPlanData(FlowObj.GetPlanData(plan));
    }

    private void SetPlanData(PlanData data)
    {
        this.ctlToolbar.BtnCancelShow = (data.STATUS == Constz.Requisition.Status.Approved.Code);
        this.btnCalculate.Visible = (data.YEAR == (DateTime.Today.Year + 543).ToString() && data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.txtPlan.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE;
        this.txtYear.Text = data.YEAR;
        this.txtCreateOn.Text = data.CREATEON.ToString(Constz.DateFormat);
        if (data.CONFIRMDATE.Year != 1) this.txtConfirmDate.Text = data.CONFIRMDATE.ToString(Constz.DateFormat);
        this.txtDescription.Text = data.DESCRIPTION;
        this.txtStatus.Text = Appz.GetStatusName(data.STATUS);

        ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE = '" + Constz.ProductType.Type.WH.Code + "' ", "ทั้งหมด", "0");
        SetProductGroup();

        SetGridView();
    }

    private void SetGridView()
    {
        this.grvPlanItem.DataSource = FlowObj.GetPlanDetailList(GetSearchData());
        this.grvPlanItem.DataBind();

        if (this.cmbMonth.SelectedItem.Value == "4" || this.cmbMonth.SelectedItem.Value == "6" || this.cmbMonth.SelectedItem.Value == "9" || this.cmbMonth.SelectedItem.Value == "11")
        {
            this.grvPlanItem.Columns[31].Visible = true;
            this.grvPlanItem.Columns[32].Visible = true;
            this.grvPlanItem.Columns[33].Visible = false;
        }
        else if (this.cmbMonth.SelectedItem.Value == "2")
        {
            this.grvPlanItem.Columns[32].Visible = false;
            this.grvPlanItem.Columns[33].Visible = false;
            if (Math.IEEERemainder((Convert.ToDouble(this.txtYear.Text) - 543), 4) == 0)
            {
                this.grvPlanItem.Columns[31].Visible = true;
            }
            else
            {
                this.grvPlanItem.Columns[31].Visible = false;
            }
        }
        else
        {
            this.grvPlanItem.Columns[31].Visible = true;
            this.grvPlanItem.Columns[32].Visible = true;
            this.grvPlanItem.Columns[33].Visible = true;
        }

        for (int i = 3; i < 34; ++i)
        {
            if (this.grvPlanItem.Columns[i].Visible)
            {
                DateTime dt = new DateTime(Convert.ToInt32(this.txtYear.Text) - 543, Convert.ToInt32(this.cmbMonth.SelectedItem.Value), i - 2);
                if (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
                {
                    this.grvPlanItem.Columns[i].ItemStyle.CssClass = "planholiday";
                }
                else
                    this.grvPlanItem.Columns[i].ItemStyle.CssClass = "";
            }
        }
    }

    private PlanDetailSearchData GetSearchData()
    {
        PlanDetailSearchData data = new PlanDetailSearchData();
        data.PLAN = Convert.ToDouble(this.txtPlan.Text == "" ? "0" : this.txtPlan.Text);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.PRODUCTNAME = this.txtProductKey.Text.Trim();
        data.MONTH = Convert.ToDouble(this.cmbMonth.SelectedItem.Value);
        data.PRODUCTSTATUS = this.cmbProductStatus.SelectedItem.Value;
        return data;
    }

    #region Toolbar

    protected void CancelClick(object sender, EventArgs e)
    {
        if (FlowObj.CancelPlan(Authz.CurrentUserInfo.UserID, Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"])))
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/PlanSearch.aspx");
    }

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnCalculate.Text = "<img src='" + Constz.ImageFolder + "icn_cal.gif' border='0' align='AbsMiddle'> คำนวณ";
            btnCalculate.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnCalculate.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            SetMonthCombo();
            SetProductStatus();
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            this.ctlToolbar.ClientClickCancel = "return confirm('ต้องการยกเลิกแผนการสั่งซื้อนี้ใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการทำแผนการสั่งซื้อวัตถุดิบ');";
        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetProductGroup();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetGridView();
    }

    protected void grvPlanItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;

            LinkButton btn;

            for (int i = 3; i < 34; ++i)
            {
                btn = (LinkButton)e.Row.Cells[i].FindControl("btn" + (i - 2).ToString());
                if (Convert.ToDouble(drow["DAY" + (i - 2).ToString()]) > 0) e.Row.Cells[i].ToolTip = Convert.ToDouble(drow["DAY" + (i - 2).ToString()]).ToString(Constz.DblFormat);

                if (drow["RANK"].ToString() != Constz.PlanDetailType.Receive)
                {
                    btn.Visible = false;
                    e.Row.Cells[i].Text = (Convert.ToDouble(drow["DAY" + (i - 2).ToString()]) > 0 ? Convert.ToDouble(drow["DAY" + (i - 2).ToString()]).ToString(Constz.IntFormat) : "");
                }
                else
                {
                    btn.OnClientClick = "OpenNewModalDialog('" + Constz.HomeFolder + "WH/Transaction/PlanDetail.aspx?loid=" + Request["loid"] + "&plan=" + drow["PLAN"].ToString() + "&product=" + drow["PRODUCT"].ToString() + "&month=" + drow["MONTH"].ToString() + "&day=" + (i - 2).ToString() + "', '525', '500');";
                }

                if (drow["RANK"].ToString() == Constz.PlanDetailType.Produce)
                {
                    e.Row.Cells[1].RowSpan = 5;
                }
                else
                {
                    e.Row.Cells[1].CssClass = "zHidden";
                }

                if (drow["RANK"].ToString() == Constz.PlanDetailType.Remain)
                {
                    if (Convert.ToDouble(drow["DAY" + (i - 2).ToString()]) < Convert.ToDouble(Convert.IsDBNull(drow["MINIMUM"]) ? 0 : drow["MINIMUM"]))
                        e.Row.Cells[i].ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }

    protected void grvPlanItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SetGridView();
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        if (FlowObj.CalculatePlanUseAndRemain(Authz.CurrentUserInfo.UserID, Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"])))
        {
            SetGridView();
            Appz.ClientAlert(this, "คำนวณเรียบร้อย");
        }
        else
            Appz.ClientAlert(this.Page, FlowObj.ErrorMessage);
    }

    protected void PrintClick(object sender, EventArgs e)
    {

        if (ReportsFlow.PlanFGReport(Convert.ToDouble(this.txtPlan.Text == "" ? "0" : this.txtPlan.Text), Convert.ToDouble(this.cmbMonth.SelectedItem.Value), Convert.ToDouble(this.cmbProductType.SelectedItem.Value), Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value), this.txtProductKey.Text.Trim(), this.cmbProductStatus.SelectedValue) == true)
        {
            string temp = "";
            temp = "paramfield1=PLAN";
            temp += "&paramvalue1=" + Convert.ToDouble(this.txtPlan.Text == "" ? "0" : this.txtPlan.Text);
            temp += "&paramfield2=MONTH";
            temp += "&paramvalue2=" + Convert.ToDouble(this.cmbMonth.SelectedItem.Value);
            temp += "&paramfield3=producttype";
            temp += "&paramvalue3=" + Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
            temp += "&paramfield4=product";
            temp += "&paramvalue4=" + this.txtProductKey.Text.Trim();
            temp += "&paramfield5=productgroup";
            temp += "&paramvalue5=" + Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
            temp += "&paramfield6=status";
            temp += "&paramvalue6=" + this.cmbProductStatus.SelectedValue;

            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("Report_PlanWH", temp), true);
            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("ProductSaleSummaryReport", temp), true);

        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูล");
            return;
        }
    }
}
