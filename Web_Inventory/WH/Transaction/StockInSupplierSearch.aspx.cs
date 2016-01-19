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
using ABB.Flow.Inventory.FG;
using ABB.Flow.Sales;
using ABB.Global;
public partial class WH_Transaction_StockInSupplierSearch : System.Web.UI.Page
{
    private StockInFlow _flow;
    public StockInFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockInFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private StockInFGData GetData()
    {
        StockInFGData data = new StockInFGData();
        data.CODE = this.txtSTCode.Text.Trim();
        data.RECEIVEFROM = this.ctlReceiveFrom.DateValue;
        data.RECEIVETO = this.ctlRecriveTo.DateValue;
        data.INVNO = this.txtInvNo.Text.Trim();
        data.POCODE = this.txtPoCode.Text.Trim();
        data.ORDERFROM = this.ctlOrderFrom.DateValue;
        data.ORDERTO = this.ctlOrderTo.DateValue;
        data.SENDER = Convert.ToDouble(this.cmbSupplier.SelectedValue);
        data.QCCODE = this.txtQCCode.Text.Trim();
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void SetStockInStatusCombo(DropDownList combo)
    {
        ComboSource.BuildStockInStatusRankCombo(combo);
    }

    private void Search()
    {
        this.grvRequisition.DataSource = FlowObj.GetStockInWHList(GetData());
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
            SetStockInStatusCombo(this.cmbStatusFrom);
            SetStockInStatusCombo(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "", "ทั้งหมด", "0");
            // Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบตรวจรับวัตถุดิบใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งตรวจ QC?');";
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        ArrayList arr = GetChecked();
        if (arr.Count > 0)
        {
            if (FlowObj.DeleteData(arr))
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

        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInSupplier.aspx");

    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateStockInQCStatus(GetChecked(), Constz.Requisition.Status.QC.Code, Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ส่งตรวจ QC เรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName == "copy")
        //{
        //    if (FlowObj.CopyRequisition(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
        //        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReserve.aspx?loid=" + FlowObj.LOID.ToString());
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
        //else if (e.CommandName == "cancel")
        //{
        //    ArrayList arr = new ArrayList();
        //    arr.Add(Convert.ToDouble(e.CommandArgument));
        //    if (FlowObj.UpdateRequisitionStatus(arr, Constz.Requisition.Status.Void.Code))
        //        Search();
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
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
            //e.Row.Cells[2].Text = (e.Row.RowIndex + 1).ToString();

            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            //ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            //ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockInSupplier, Convert.ToDouble(drow["LOID"])) + " return false;";

            //btnCopy.CommandArgument = drow["LOID"].ToString();
            //btnCancel.CommandArgument = drow["LOID"].ToString();

            //if (drow["RANK"].ToString() == Constz.Requisition.Status.Approved.Rank)
            //    btnCancel.Visible = true;
            //else
            //    btnCancel.Visible = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

}

