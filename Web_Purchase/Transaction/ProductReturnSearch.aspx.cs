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

public partial class Transaction_ProductReturnSearch : System.Web.UI.Page
{
    private PDReturnFlow _flow;
    public PDReturnFlow FlowObj
    {
        get { if (_flow == null) _flow = new PDReturnFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPDReturn.ClientID + "_ctl', '_chkItem')"; }
    }

    private ProductReturnData GetData()
    {
        ProductReturnData data = new ProductReturnData();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.SUPPLIER = Convert.ToDouble(this.cmbSupplier.SelectedItem.Value);
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvPDReturn.DataSource = FlowObj.GetPDReturnList(GetData());
        this.grvPDReturn.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDReturn.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDReturn.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPDReturn.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildStatusReturnRankCombo(this.cmbStatusFrom);
            ComboSource.BuildStatusReturnRankCombo(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "", "ทั้งหมด", "0");

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบรายการนี้ใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งให้จัดซื้อ?');";
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
        {
            Search();
            Appz.ClientAlert(this, "ลบรายการเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReturn.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdatePDReturnStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ส่งให้จัดซื้อเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvPDReturn_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "copy")
        //{
        //    if (FlowObj.CopyPDRequest(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
        //        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseRequest.aspx?loid=" + FlowObj.LOID.ToString());
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
        //else if (e.CommandName == "cancelpdrequest")
        //{
        //    ArrayList arr = new ArrayList();
        //    arr.Add(Convert.ToDouble(e.CommandArgument));
        //    if (FlowObj.UpdatePDRequestStatus(arr, Constz.Requisition.Status.Void.Code, Authz.CurrentUserInfo.UserID))
        //        Search();
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
    }

    protected void grvPDReturn_RowDataBound(object sender, GridViewRowEventArgs e)
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
            //ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            //ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductReturn, Convert.ToDouble(drow["LOID"])) + "return false;";
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

