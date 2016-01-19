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

public partial class Transaction_ReturnProductSearch : System.Web.UI.Page
{
    private ReturnProductFlow _flow;
    public ReturnProductFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnProductFlow(); return _flow; }
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
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ12;
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        combo.Items.Clear();
        ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.ApproveWH.Name, Constz.Requisition.Status.ApproveWH.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Finish.Name, Constz.Requisition.Status.Finish.Rank);
        combo.Items.Add(item);
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

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบรับคืนสินค้าฝากขายใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ส่งคลังสำเร็จรูปใช่หรือไม่  ?');";
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
        Response.Redirect(Constz.HomeFolder + "Transaction/ReturnProduct.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.SubmitRPRequisition(GetChecked(),Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ยืนยันรายการส่งคลัง");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            if (FlowObj.CopyRequisition(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Response.Redirect(Constz.HomeFolder + "Transaction/ReturnProduct.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else if (e.CommandName == "cancelItem")
        {
            ArrayList arr = new ArrayList();
            arr.Add(Convert.ToDouble(e.CommandArgument));
            if (FlowObj.UpdateRequisitionStatus(arr, Constz.Requisition.Status.Void.Code, Authz.CurrentUserInfo.UserID))
            {
                Search();
                Appz.ClientAlert(this, "ยกเลิกรายการเรียบร้อยแล้ว");
            }
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
            //e.Row.Cells[2].Text = (e.Row.RowIndex + 1).ToString();

            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            ImageButton btnPrint = (ImageButton)e.Row.Cells[1].FindControl("btnPrint");
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockInReturnProduct, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["LOID"].ToString();
            //btnCancel.CommandArgument = drow["LOID"].ToString();

            //if (drow["RANK"].ToString() == Constz.Requisition.Status.Reserve.Rank)
            //    btnCancel.Visible = true;
            //else
            //    btnCancel.Visible = false;

            if (drow["RANK"].ToString() == Constz.Requisition.Status.Reserve.Rank)
                chk.Enabled = true;
            else
                chk.Enabled = false;

            chk.Enabled = (drow["RANK"].ToString() == Constz.Requisition.Status.Waiting.Rank);

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

}
