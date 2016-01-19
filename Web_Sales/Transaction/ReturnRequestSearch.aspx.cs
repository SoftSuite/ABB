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

public partial class Transaction_ReturnRequestSearch : System.Web.UI.Page
{
    private ReturnRequestFlow _flow;
    public ReturnRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnRequestFlow(); return _flow; }
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
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ05;
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        ComboSource.BuildStatusRankComboReturn(combo);
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
            Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบลดหนี้ใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ต้องการส่งคลังสำเร็จรูปใช่หรือไม่?');";
        }
    }

    //protected void DeleteClick(object sender, EventArgs e)
    //{
    //    if (FlowObj.DeleteData(GetChecked()))
    //    {
    //        Search();
    //        Appz.ClientAlert(this, "ยืนยันการลบรายการ");
    //    }
    //    else
    //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
    //}

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ReturnRequest.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitReturnRequisition(GetChecked(), Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ยืนยันการส่งคลัง");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            if (FlowObj.CopyRequisition(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Response.Redirect(Constz.HomeFolder + "Transaction/ReturnRequest.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else if (e.CommandName == "cancelItem")
        {
            if (FlowObj.CancelReturnRequisition(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Search();
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
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
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ReturnRequest, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["LOID"].ToString();
            btnCopy.CommandArgument = drow["LOID"].ToString();
            btnCancel.CommandArgument = drow["LOID"].ToString();

            if (drow["STATUSNAME"].ToString() == Constz.Requisition.Status.Approved.Name)
                btnCancel.Visible = true;
            else
                btnCancel.Visible = false;

            if (drow["STATUSNAME"].ToString() == Constz.Requisition.Status.Waiting.Name)
                chk.Enabled = true;
            else
                chk.Enabled = false;

            //chk.Enabled = (drow["RANK"].ToString() != Constz.Requisition.Status.ApproveWH.Rank);
            btnCopy.OnClientClick = "return confirm('ยืนยันการคัดลอกใบบันทึกรายการเพื่อการออกใบลดหนี้เป็นเลขที่ใหม่');";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }
}
