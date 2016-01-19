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
using ABB.Data.Sales;
using ABB.Data;

public partial class Transaction_PlanOrderSale : System.Web.UI.Page
{
    private PlanOrderSaleFlow _flow;
    private PlanOrderSale item;

    private PlanOrderSaleFlow FlowObj
    {
        get { if (_flow == null) _flow = new PlanOrderSaleFlow(); return _flow; }
    }

    private PlanOrderSale ItemObj
    {
        get { if (item == null) item = new PlanOrderSale(); return item; }
    }

    #region Methods

    private void SetSalemanCombo(GridViewRow gRow)
    {
        ComboSource.BuildCombo((DropDownList)gRow.Cells[2].FindControl("cmbSaleman"), "SALEMAN", "NAME", "LOID", "NAME", "", "เลือก", "0");
    }

    private void SetQtyTextbox(TextBox txtQty)
    {
        ControlUtil.SetIntTextBox(txtQty);
    }

    private void InsertData(GridViewRow gRow)
    {
        DropDownList cmbSaleman = (DropDownList)gRow.Cells[2].FindControl("cmbSaleman");
        TextBox txtQty = (TextBox)gRow.Cells[3].FindControl("txtQty");
        PlanOrderSaleData data = new PlanOrderSaleData();
        data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
        data.SALEMAN = Convert.ToDouble(cmbSaleman.SelectedItem.Value);
        data.SALENAME = cmbSaleman.SelectedItem.Text;

        if (ItemObj.InsertPlanOrderSale(data))
        {
            SetGrvItem(this.lblStatus.Text);
        }
        else
            Appz.ClientAlert(this, ItemObj.ErrorMessage);
    }

    private void SetGrvItem(string status)
    {
        this.grvPlanOrderSale.DataBind();
        this.grvPlanOrderSaleNew.DataBind();

        if (grvPlanOrderSale.Rows.Count > 0)
        {
            this.grvPlanOrderSale.ShowFooter = (status == Constz.Requisition.Status.Waiting.Code && this.btnSave.Visible);
            this.grvPlanOrderSale.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code && this.btnSave.Visible);
            this.grvPlanOrderSale.Visible = true;
            this.grvPlanOrderSaleNew.Visible = false;
        }
        else
        {
            this.grvPlanOrderSale.Visible = (status != Constz.Requisition.Status.Waiting.Code && this.btnSave.Visible);
            this.grvPlanOrderSaleNew.Visible = (status == Constz.Requisition.Status.Waiting.Code && this.btnSave.Visible);
        }
    }

    private void ResetState(double planOrder, int year, int month)
    {
        ItemObj.ClearSession();
        if (new DateTime(year, month, 1) < new DateTime(DateTime.Now.Year, DateTime.Now.Month,1)) this.btnSave.Visible = false;
        SetData(FlowObj.GetPlanOrderData(planOrder, month));
    }

    #region Data

    private void SetData(PlanOrderData data)
    {
        this.lblStatus.Text = data.STATUS;
        this.lblProductName.Text = data.PRODUCTNAME;
        this.lblUnitName.Text = data.UNITNAME;
        this.lblMonthName.Text = data.MONTHNAME;
        if (this.btnSave.Visible) this.btnSave.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code);
        this.chkCopyAll.Visible = (data.STATUS == Constz.Requisition.Status.Waiting.Code && this.btnSave.Visible);

        SetGrvItem(this.lblStatus.Text);
    }

    private ArrayList GetData()
    {
        ArrayList arr = new ArrayList();
        int month = Convert.ToInt16(Request["month"] == null ? "0" : Request["month"]);
        foreach (GridViewRow gRow in this.grvPlanOrderSale.Rows)
        {
            if (gRow.RowType == DataControlRowType.DataRow)
            {
                TextBox txtQty=(TextBox)gRow.Cells[3].FindControl("txtQtyEdit");
                PlanOrderSaleData data = new PlanOrderSaleData();
                data.MONTH = month;
                data.QTY = Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text);
                data.SALEMAN = Convert.ToDouble(gRow.Cells[4].Text);
                if (data.QTY == 0) Appz.ClientAlert(this, "กรุณาระบุจำนวนสั่ง");
                arr.Add(data);
            }
        }
        return arr;
    }

    #endregion

    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        btnSave.Text = "<img src='" + Constz.ImageFolder + "icn_save.gif' border='0' align='AbsMiddle'> บันทึก";
        btnSave.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
        btnSave.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
        btnSave.Attributes.Add("href", "#");
        btnSave.Attributes.Add("OnClick", "__doPostBack('" + this.btnSave.ClientID.Replace("_", "$") + "','')");

        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["planorder"] == null ? "0" : Request["planorder"]), Convert.ToInt16(Request["year"] == null ? "0" : Request["year"]), Convert.ToInt16(Request["month"] == null ? "0" : Request["month"]));
            this.ctlToolbar.ClientClickBack = "window.close(); return false;";
            this.ctlToolbar.ClientClickCancel = this.ctlToolbar.ClientClickBack;
            //this.ctlToolbar.ClientClickSave = "return false;";

            Response.Expires = 0;
            Response.AddHeader("pragma","no-cache");
            Response.AddHeader("cache-control","private");
            Response.CacheControl = "no-cache";
        }
    }

    #region grvPlanOrderSale

    protected void grvPlanOrderSale_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvPlanOrderSale.FooterRow);
        }
    }

    protected void grvPlanOrderSale_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            SetSalemanCombo(e.Row);
            SetQtyTextbox((TextBox)e.Row.Cells[3].FindControl("txtQty"));
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            TextBox txtQty = (TextBox)e.Row.Cells[3].FindControl("txtQtyEdit");
            SetQtyTextbox(txtQty);
            ((ImageButton)e.Row.Cells[0].FindControl("imbDelete")).OnClientClick = "return confirm('ต้องการลบรายการผู้ขาย " + drow["SALENAME"].ToString() + " ใช่หรือไม่?')";
            if (this.lblStatus.Text != Constz.Requisition.Status.Waiting.Code)
            {
                txtQty.ReadOnly = true;
                txtQty.CssClass = "zTextboxR-View";
            }
            //txtQty.Attributes.Add("onchange", "if (parseFloat(this.value=='' ? '0' : this.value) <= 0) { this.focus(); alert('กรุณาระบุจำนวนสั่ง'); }");
        }
    }

    protected void grvPlanOrderSale_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            Appz.ClientAlert(this, e.Exception.Message);
        }
        else
        {
            SetGrvItem(this.lblStatus.Text);
        }
    }

    #endregion

    #region grvPlanOrderSaleNew

    protected void grvPlanOrderSaleNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            InsertData(this.grvPlanOrderSaleNew.Rows[0]);
        }
    }

    protected void grvPlanOrderSaleNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            SetSalemanCombo(e.Row);
            SetQtyTextbox((TextBox)e.Row.Cells[3].FindControl("txtQty"));
        }
    }

    #endregion

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, Convert.ToDouble(Request["planorder"]), Convert.ToInt16(Request["month"] == null ? "0" : Request["month"]), GetData(), this.chkCopyAll.Checked))
        {
            ResetState(Convert.ToDouble(Request["planorder"] == null ? "0" : Request["planorder"]), Convert.ToInt16(Request["year"] == null ? "0" : Request["year"]), Convert.ToInt16(Request["month"] == null ? "0" : Request["month"]));
            //Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "closewin", "window.close()", true);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

}
