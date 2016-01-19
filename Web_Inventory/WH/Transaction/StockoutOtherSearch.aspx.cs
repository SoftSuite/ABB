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
using ABB.Data.Inventory.WH;
using ABB.Flow;
using ABB.Flow.Inventory.WH;
using ABB.Global;

public partial class WH_Transaction_StockoutSearch : System.Web.UI.Page
{
    private StockoutFlow _flow;
    public StockoutFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockoutFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private ProductReserveSearchData GetData()
    {
        ProductReserveSearchData data = new ProductReserveSearchData();
        //data.REQCODE = this.txtReqCode.Text.Trim();
        data.CODE = this.txtStockCode.Text.Trim();
        data.CREATEFROM = this.ctlApproveDateFrom.DateValue;
        data.CREATETO = this.ctlApproveDateTo.DateValue.AddDays(1);
        //data.DATEFROM = this.ctlRequestDateFrom.DateValue;
        //data.DATETO = this.ctlRequestDateTo.DateValue;
        //data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedItem.Value);
        //data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        data.CUSTOMERNAME = this.txtCreateby.Text.Trim();
        data.DIVISION = Convert.ToDouble(this.cmbDivision.SelectedItem.Value);
        return data;
    }

    private void SetRequisitionStatusCombo(DropDownList combo)
    {
        combo.Items.Clear();
        ListItem item = new ListItem(Constz.Requisition.Status.Waiting.Name, Constz.Requisition.Status.Waiting.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Approved.Name, Constz.Requisition.Status.Approved.Rank);
        combo.Items.Add(item);
        item = new ListItem(Constz.Requisition.Status.Void.Name, Constz.Requisition.Status.Void.Rank);
        combo.Items.Add(item);
    }

    private void Search()
    {
        this.grvRequisition.DataSource = FlowObj.GetProductionOtherList(GetData());
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
            //ComboSource.BuildCombo(this.cmbRequisitionType, "DOCTYPE", "DOCNAME", "LOID", "DOCNAME", "REQUISITIONTYPE IN (" + Constz.Requisition.RequisitionType.REQ08.ToString() + "," + Constz.Requisition.RequisitionType.REQ09.ToString() + ") OR LOID = 24", "ทั้งหมด", "0");
            //ComboSource.BuildCombo(this.cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbDivision, "DIVISION", "TNAME", "LOID", "TNAME", "", "ทั้งหมด", "0");
            //Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบเบิกวัสดุอื่นๆใช่หรือไม่?');";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันรายการใช่หรือไม่?');";

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

            Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockoutOther.aspx");

    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateStockOutOtherStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
        {
            Search();
            Appz.ClientAlert(this, "อนุมัติรายการเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvRequisition_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "print")
        {

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

            //btnPrint.CommandArgument = drow["LOID"].ToString();
            //if (drow["DOCTYPE"].ToString() == Constz.DocType.ReqRawPO.NAME.ToString())
            //    btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockoutExportMaterial, Convert.ToDouble(drow["LOID"])) + " return false;";
            //else if (drow["DOCTYPE"].ToString() == Constz.DocType.ReqRawPD.NAME.ToString())
            //    btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockoutMaterialWH, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.StockoutOther, Convert.ToDouble(drow["LOID"])) + " return false;";

            chk.Enabled = (drow["RANK"].ToString() != Constz.Requisition.Status.Approved.Rank);

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

}


