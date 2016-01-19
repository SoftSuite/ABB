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

public partial class Transaction_ProductOrderSearch : System.Web.UI.Page
{
    private ProductOrderFlow _flow;
    public ProductOrderFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductOrderFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private ProductOrderSearchData GetData()
    {
        ProductOrderSearchData data = new ProductOrderSearchData();
        data.CODE = this.txtCode.Text.Trim();
        data.PDNAME = this.cmbProductView.SelectedItem.Value;
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        ComboSource.BuildStatusRankCombo(combo);
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
            ComboSource.BuildCombo(cmbProductView, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
            SetRequisitionStatusCombo(this.cmbStatusFrom);
            SetRequisitionStatusCombo(this.cmbStatusTo);
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบบันทึกการผลิตใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งฝ่ายผลิต?');";
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
        //ProductOrderData data = new ProductOrderData();
        //data.ACTIVE = Constz.ActiveStatus.Active;
        //data.CODE = "";
        //data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ07;
        //data.RESERVEDATE = DateTime.Now.Date;
        //data.STATUS = Constz.Requisition.Status.Waiting.Code;

        //if (FlowObj.NewRequisition(Authz.CurrentUserInfo.UserID, data))
        //    Response.Redirect(Constz.HomeFolder + "Transaction/ProductOrder.aspx?loid=" + FlowObj.LOID.ToString());
        //else
        //    Appz.ClientAlert(this, FlowObj.ErrorMessage);

        Response.Redirect(Constz.HomeFolder + "Transaction/ProductOrder.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateRequisitionStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "ส่งฝ่ายผลิตเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            if (FlowObj.CopyRequisition(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Response.Redirect(Constz.HomeFolder + "Transaction/ProductOrder.aspx?loid=" + FlowObj.LOID.ToString());
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
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ProductOrder, Convert.ToDouble(drow["LOID"])) + " return false; ";
            btnCopy.OnClientClick = "return confirm('ยืนยันการคัดลอกบันทึกสั่งผลิตเป็นเลขที่ใหม่ ?');";
            btnCopy.CommandArgument = drow["LOID"].ToString();

            chk.Enabled = (drow["RANK"].ToString() != Constz.Requisition.Status.Approved.Rank);
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }
}

