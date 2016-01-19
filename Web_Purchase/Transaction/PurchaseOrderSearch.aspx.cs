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
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;
using ABB.Global;

public partial class Transaction_PurchaseOrderSearch : System.Web.UI.Page
{
    private PurchaseOrderFlow _flow;
    public PurchaseOrderFlow FlowObj
    {
        get { if (_flow == null) _flow = new PurchaseOrderFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPDOrder.ClientID + "_ctl', '_chkItem')"; }
    }

    private PurchaseOrderSearchData GetData()
    {
        PurchaseOrderSearchData data = new PurchaseOrderSearchData();
        data.POCODE = this.txtPOCode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRCODE = this.txtPRCode.Text.Trim();
        data.PURCHASETYPE = Convert.ToDouble(this.cmbPurchaseType.SelectedItem.Value);
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.DIVISION = Convert.ToDouble(this.cmbDivision.SelectedItem.Value);
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvPDOrder.DataSource = FlowObj.GetPDOrderList(GetData());
        this.grvPDOrder.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDOrder.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDOrder.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPDOrder.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnNewNorm.Text = "<img src='" + Constz.ImageFolder + "icn_new.gif' border='0' align='AbsMiddle'> เพิ่มสั่งซื้อปกติ";
            btnNewNorm.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnNewNorm.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");

            ComboSource.BuildStatusRankCombo(this.cmbStatusFrom);
            ComboSource.BuildStatusRankCombo(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbPurchaseType, "PURCHASETYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbDivision, "DIVISION", "TNAME", "LOID", "TNAME", "", "ทั้งหมด", "0");
            //Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบสั่งซื้อใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการอนุมัติจัดซื้อ');";
        }
    }

    //protected void DeleteClick(object sender, EventArgs e)
    //{
    //    if (FlowObj.DeleteData(GetChecked()))
    //        Search();
    //    else
    //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
    //}

    protected void btnNewNorm_Click(object sender, EventArgs e)
    {
        //if (FlowObj.AddAllProduct(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]), Authz.CurrentUserInfo.UserID))
        //    SetGridView();
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);
        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseOrder.aspx?TYPE=N");
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseOrder.aspx?TYPE=B");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdatePDOrderStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
        {
            //Appz.ClientAlert(Page, "อนุมัติบันทึกขอซื้อแล้ว");
            Search();
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvPDOrder_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {

        }
    }

    protected void grvPDOrder_RowDataBound(object sender, GridViewRowEventArgs e)
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

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.PurchaseOrder, Convert.ToDouble(drow["LOID"])) + "return false;";
            //btnCopy.CommandArgument = drow["LOID"].ToString();
            //btnCancel.CommandArgument = drow["LOID"].ToString();

            //if (drow["RANK"].ToString() == Constz.Requisition.Status.Approved.Rank)
            //    btnCancel.Visible = true;
            //else
            //    btnCancel.Visible = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);

        }
    }
}
