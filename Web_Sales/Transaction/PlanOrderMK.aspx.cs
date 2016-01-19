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
using ABB.Flow.Sales;
using ABB.Data;
using ABB.Data.Sales;

public partial class Transaction_PlanOrderMK : System.Web.UI.Page
{
    public string space = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
    private PlanSaleFlow _flow;

    private PlanSaleFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PlanSaleFlow(); } return _flow; }
    }

    private void SetCheckBoxScript()
    {
        string script = "";
        if (this.grvPlanitem.Rows.Count > 0)
        {
            CheckBox chkAll = (CheckBox)this.grvPlanitem.HeaderRow.Cells[0].FindControl("chkAll");
            foreach (GridViewRow gRow in this.grvPlanitem.Rows)
            {
                CheckBox chkItem = (CheckBox)gRow.Cells[0].FindControl("chkItem");
                script += "if (document.getElementById('" + chkItem.ClientID + "').disabled == '') document.getElementById('" + chkItem.ClientID + "').checked = document.getElementById('" + chkAll.ClientID + "').checked;";
            }
            chkAll.Attributes.Add("onclick", script);
        }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPlanitem.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPlanitem.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPlanitem.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPlanitem.Rows[i].Cells[1].Text)); }
        }
        return arrLOID;
    }

    //private void SetProductGroup()
    //{
    //    ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "PRODUCTTYPE = " + this.cmbProductType.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
    //}

    private void ResetState(double plan)
    {
        SetPlanData(FlowObj.GetPlanData(plan));
    }

    private void SetPlanData(PlanData data)
    {
        //this.ctlToolbar.BtnCancelShow = (data.STATUS == Constz.Requisition.Status.Approved.Code);
        //this.ctlToolbar.BtnSubmitShow = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        //this.btnNewAll.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        //this.ctlToolbarItem.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.txtPlan.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE;
        this.txtYear.Text = data.YEAR;
        this.txtCreateOn.Text = data.CREATEON.ToString(Constz.DateFormat);
        //if (data.CONFIRMDATE.Year != 1) this.txtConfirmDate.Text = data.CONFIRMDATE.ToString(Constz.DateFormat);
        this.txtDescription.Text = data.DESCRIPTION;
        this.txtStatus.Text = Appz.GetStatusName(data.STATUS);

        //ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE = '" + Constz.ProductType.Type.FG.Code + "' ", "ทั้งหมด", "0");
        //SetProductGroup();

        SetGridView();
    }

    private void SetGridView()
    {
        this.grvPlanitem.DataSource = FlowObj.GetPlanMKItemList(GetSearchData());
        this.grvPlanitem.DataBind();
        SetCheckBoxScript();
    }

    private PlanItemSearchData GetSearchData()
    {
        PlanItemSearchData data = new PlanItemSearchData();
        data.PLAN = Convert.ToDouble(this.txtPlan.Text == "" ? "0" : this.txtPlan.Text);
        //data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        //data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        //data.PRODUCTNAME = this.txtProductKey.Text.Trim();
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnNewAll.Text = "<img src='" + Constz.ImageFolder + "icn_new_all.gif' border='0' align='AbsMiddle'> เพิ่มคู่ค้าทั้งหมด";
            btnNewAll.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnNewAll.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            //this.ctlToolbar.ClientClickCancel = "return confirm('ต้องการยกเลิกแผนการจำหน่ายนี้ใช่หรือไม่?');";
            //this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการทำแผนการจำหน่ายสินค้า');";

            //string script = "";
            //script += "document.getElementById('" + this.txtProduct.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/ProductPlan.aspx?plan=" + Request["LOID"] + "', '600', '450');";
            //script += "return ('undefined' !=  document.getElementById('" + this.txtProduct.ClientID + "').value && '' != document.getElementById('" + this.txtProduct.ClientID + "').value);  ";
            //this.ctlToolbarItem.ClientClickNew = script;

            this.btnNewAll.OnClientClick = "return confirm('ต้องการเพิ่มคู่ค้าทั้งหมดใช่หรือไม่?');";
            this.ctlToolbarItem.ClientClickDelete = "return confirm('ต้องการลบรายการคู่ค้าใช่หรือไม่?');";

            string temp = "";
            temp = "paramfield1=YEAR";
            temp += "&paramvalue1=" + this.txtYear.Text.Trim();

            this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript("ReportMarketPlan", temp) + " return false;";
            this.ctlToolbar2.ClientClickPrint = ABB.Global.Appz.ReportScript("ReportMarketSummary", temp) + " return false;";
            this.ctlToolbar3.ClientClickPrint = ABB.Global.Appz.ReportScript("ReportMarketDiff", temp) + " return false;";

        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SetProductGroup();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetGridView();
    }

    #region Toolbar

    protected void CancelClick(object sender, EventArgs e)
    {
        //if (FlowObj.CancelPlan(Authz.CurrentUserInfo.UserID, Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"])))
        //    ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/PlanSearchMK.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        //if (FlowObj.CommitPlan(Authz.CurrentUserInfo.UserID, Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"])))
        //    ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grvPlanitem.Rows)
        {
            string customer = row.Cells[2].Text.Trim();
            double LOID = Convert.ToDouble(row.Cells[1].Text);
            double percent = Convert.ToDouble(((TextBox)row.Cells[4].FindControl("txtPercent")).Text);

            PlanMarketingData PMdata = new PlanMarketingData();
            switch (customer)
            {
                case "1":
                    PMdata = FlowObj.DoGetValueFront(DateTime.Now.Year);
                    break;
                case "-1":
                    PMdata = FlowObj.DoGetValueOther(DateTime.Now.Year);
                    break;
                default:
                    PMdata = FlowObj.DoGetValue(DateTime.Now.Year, Convert.ToDouble(customer));
                    break;
            }

            PMdata.M1 += PMdata.M1 * percent / 100;
            PMdata.M2 += PMdata.M2 * percent / 100;
            PMdata.M3 += PMdata.M3 * percent / 100;
            PMdata.M4 += PMdata.M4 * percent / 100;
            PMdata.M5 += PMdata.M5 * percent / 100;
            PMdata.M6 += PMdata.M6 * percent / 100;
            PMdata.M7 += PMdata.M7 * percent / 100;
            PMdata.M8 += PMdata.M8 * percent / 100;
            PMdata.M9 += PMdata.M9 * percent / 100;
            PMdata.M10 += PMdata.M10 * percent / 100;
            PMdata.M11 += PMdata.M11 * percent / 100;
            PMdata.M12 += PMdata.M12 * percent / 100;

            PMdata.PERCENT = percent;

            FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, LOID, PMdata);

        }

        ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
    }

    #endregion

    #region Toolbar Item

    protected void NewClick(object sender, EventArgs e)
    {
        //if (this.txtProduct.Text.Trim() != "")
        //{
        //    if (FlowObj.AddSomeProduct(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]), Authz.CurrentUserInfo.UserID, this.txtProduct.Text.Trim()))
        //        SetGridView();
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        //if (FlowObj.DeletePlanOrder(GetChecked()))
        //    SetGridView();
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnNewAll_Click(object sender, EventArgs e)
    {
        if (FlowObj.AddAllCustomer(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]), Authz.CurrentUserInfo.UserID))
            SetGridView();
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    protected void grvPlanitem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            //chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;

            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            chk.Enabled = (drow["STATUS"].ToString() == Constz.Requisition.Status.Waiting.Code);
            LinkButton btn;

            for (int i = 4; i < 16; ++i)
            {
                btn = (LinkButton)e.Row.Cells[i].FindControl("btn" + (i - 3).ToString());
                if (Convert.ToDouble(drow["M" + (i - 3).ToString()]) > 0) e.Row.Cells[i].ToolTip = Convert.ToDouble(drow["M" + (i - 3).ToString()]).ToString(Constz.DblFormat);
                if (drow["STATUS"].ToString() == Constz.Requisition.Status.Waiting.Code || (drow["STATUS"].ToString() != Constz.Requisition.Status.Waiting.Code && Convert.ToDouble(drow["M" + (i - 3).ToString()]) > 0))
                {
                    e.Row.Cells[i].Attributes.Add("OnMouseOver", "this.className='planhover'");
                    e.Row.Cells[i].Attributes.Add("OnMouseOut", "this.className=''");
                    //                    btn.OnClientClick = "OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PlanOrderSale.aspx?plan=" + Request["loid"] + "&planorder=" + drow["LOID"].ToString() + "&product=" + drow["PRODUCT"].ToString() + "&month=" + (i - 3).ToString() + "&year=" + (Convert.ToInt32(this.txtYear.Text) - 543).ToString() + "', '525', '500');";
                }
                else
                {
                    btn.Visible = false;
                }
            }
        }
    }

    protected void grvPlanitem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        SetGridView();
    }
}
