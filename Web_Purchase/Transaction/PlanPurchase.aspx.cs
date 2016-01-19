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
using ABB.Flow.Purchase;
using ABB.Data;
using ABB.Data.Purchase;

public partial class Transaction_PlanPurchase : System.Web.UI.Page
{
    public string space = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
    private PlanPOFlow _flow;

    private PlanPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PlanPOFlow(); } return _flow; }
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

    private void ResetState(double year)
    {
        SetPlanData(year);
    }

    private void SetPlanData(double year)
    {
        this.txtYear.Text = year.ToString();
        //this.btnCalculate.Visible = (year.ToString() == (DateTime.Today.Year + 543).ToString());

        ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
        SetProductGroup();

        SetGridView();
    }

    private void SetGridView()
    {
        this.grvPlanItem.DataSource = FlowObj.GetPlanDetailList(GetSearchData());
        this.grvPlanItem.DataBind();

        if (this.cmbMonth.SelectedItem.Value == "4" || this.cmbMonth.SelectedItem.Value == "6" || this.cmbMonth.SelectedItem.Value == "9" || this.cmbMonth.SelectedItem.Value == "11")
        {
            this.grvPlanItem.Columns[29].Visible = true;
            this.grvPlanItem.Columns[30].Visible = true;
            this.grvPlanItem.Columns[31].Visible = false;
        }
        else if (this.cmbMonth.SelectedItem.Value == "2")
        {
            this.grvPlanItem.Columns[30].Visible = false;
            this.grvPlanItem.Columns[31].Visible = false;
            if (Math.IEEERemainder((Convert.ToDouble(this.txtYear.Text) - 543), 4) == 0)
            {
                this.grvPlanItem.Columns[29].Visible = true;
            }
            else
            {
                this.grvPlanItem.Columns[29].Visible = false;
            }
        }
        else
        {
            this.grvPlanItem.Columns[29].Visible = true;
            this.grvPlanItem.Columns[30].Visible = true;
            this.grvPlanItem.Columns[31].Visible = true;
        }

        for (int i = 1; i < 32; ++i)
        {
            if (this.grvPlanItem.Columns[i].Visible)
            {
                DateTime dt = new DateTime(Convert.ToInt32(this.txtYear.Text) - 543, Convert.ToInt32(this.cmbMonth.SelectedItem.Value), i);
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
        data.YEAR = Convert.ToDouble(this.txtYear.Text == "" ? "0" : this.txtYear.Text);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.PRODUCTNAME = this.txtProductKey.Text.Trim();
        data.MONTH = Convert.ToDouble(this.cmbMonth.SelectedItem.Value);
        return data;
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/PlanProduceSearch.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnCalculate.Text = "<img src='" + Constz.ImageFolder + "icn_cal.gif' border='0' align='AbsMiddle'> คำนวณ";
            btnCalculate.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnCalculate.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            SetMonthCombo();
            ResetState(Convert.ToDouble(Request["year"] == null ? "0" : Request["year"]));
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
            for (int i = 1; i < 32; ++i)
            {
                if (Convert.ToDouble(drow["DAY" + i.ToString()]) > 0) e.Row.Cells[i].ToolTip = Convert.ToDouble(drow["DAY" + i.ToString()]).ToString(Constz.DblFormat);
            }
        }
    }

    protected void grvPlanItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SetGridView();
    }

    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        //if (FlowObj.CalculatePlanUseAndRemain(Authz.CurrentUserInfo.UserID, Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"])))
        //{
        //    SetGridView();
        //    Appz.ClientAlert(this, "คำนวณเรียบร้อย");
        //}
        //else
        //    Appz.ClientAlert(this.Page, FlowObj.ErrorMessage);
    }
}
