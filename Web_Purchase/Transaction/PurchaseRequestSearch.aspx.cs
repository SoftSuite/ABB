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

public partial class Transaction_PurchaseRequestSearch : System.Web.UI.Page
{
    private PurchaseRequestFlow _flow;
    public PurchaseRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new PurchaseRequestFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPDRequest.ClientID + "_ctl', '_chkItem')"; }
    }

    private PurchaseRequestSearchData GetData()
    {
        PurchaseRequestSearchData data = new PurchaseRequestSearchData();
        data.CODEFROM = this.txtCodeFrom.Text.Trim();
        data.CODETO = this.txtCodeTo.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PURCHASETYPE = Convert.ToDouble(this.cmbPurchaseType.SelectedItem.Value);
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.DIVISION = Authz.CurrentUserInfo.DivisionID;
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        data.STATUSPRFROM = this.cmbStatusPRFrom.SelectedItem.Value;
        data.STATUSPRTO = this.cmbStatusPRTo.SelectedItem.Value;
        data.STATUSPOFROM = this.cmbStatusPOFrom.SelectedItem.Value;
        data.STATUSPOTO = this.cmbStatusPOTo.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvPDRequest.DataSource = FlowObj.GetPDRequestList(GetData());
        this.grvPDRequest.DataBind();
        Renumber();
    }

    private void Renumber()
    {
        int num = 1;
        foreach (GridViewRow row in this.grvPDRequest.Rows)
        {
            if (row.Cells[2].CssClass != "zHidden")
            {
                row.Cells[2].Text = num.ToString();
                num++;
            }
        }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDRequest.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDRequest.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPDRequest.Rows[i].Cells[3].Text)); }
        }
        return arrLOID;
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnVoid.Text = "<img src='" + Constz.ImageFolder + "icn_delete.gif' border='0' align='AbsMiddle'> ไม่อนุมัติรายการ";
            btnVoid.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnVoid.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            this.btnVoid.OnClientClick = "return confirm('ต้องการไม่อนุมัติรายการนี้ใช่หรือไม่?');";

            btnCancelPR.Text = "<img src='" + Constz.ImageFolder + "icn_delete.gif' border='0' align='AbsMiddle'>  ยกเลิก PR";
            btnCancelPR.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnCancelPR.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            this.btnCancelPR.OnClientClick = "return confirm('ยืนยันการยกเลิก PR ใช่หรือไม่?');";

            ComboSource.BuildSTStatusRankCombo(this.cmbStatusFrom, "เลือก", "0");
            ComboSource.BuildSTStatusRankCombo(this.cmbStatusTo, "เลือก", "0");
            ComboSource.BuildStatusPRRankCombo(this.cmbStatusPRFrom, "เลือก", "0");
            ComboSource.BuildStatusPRRankCombo(this.cmbStatusPRTo, "เลือก", "0");
            ComboSource.BuildStatusRankCombo(this.cmbStatusPOFrom, "เลือก", "0");
            ComboSource.BuildStatusRankCombo(this.cmbStatusPOTo, "เลือก", "0");
            ComboSource.BuildCombo(this.cmbPurchaseType, "PURCHASETYPE", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            //ComboSource.BuildCombo(this.cmbDivision, "DIVISION", "TNAME", "LOID", "TNAME", "", "ทั้งหมด", "0");
            //Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบบันทึกรายการเพื่อการจัดซื้อ/จัดจ้างใช่หรือไม่?');";
           

            if (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID || Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID)
            {
                this.ctlToolbar.NameBtnSubmit = "อนุมัติรายการ";
                this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการอนุมัติรายการ');";
                this.ctlToolbar.SetButtonText();
                this.btnVoid.Visible = false;
                //if (Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID)
                this.btnCancelPR.Visible = true;
            }
            else
            {
                this.ctlToolbar.NameBtnSubmit = "ส่งให้จัดซื้อ";
                this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งให้จัดซื้อ');";
                this.ctlToolbar.SetButtonText();
                this.btnVoid.Visible = false;
            }
        }
    }

    //protected void DeleteClick(object sender, EventArgs e)
    //{
    //    if (FlowObj.DeleteData(GetChecked()))
    //        Search();
    //    else
    //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
    //}

    protected void btnCancelPR_Click(object sender, EventArgs e)
    {
        if (FlowObj.UpdateRequestStatus(GetChecked(), (Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID ? Constz.Requisition.Status.Void.Code : Constz.Requisition.Status.Void.Code), Authz.CurrentUserInfo.UserID))
        {
            Search();
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseRequest.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdatePDRequestStatus(GetChecked(), (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID ? Constz.Requisition.Status.Approved.Code : Constz.Requisition.Status.SP.Code), Authz.CurrentUserInfo.UserID))
        {
            Search();
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvPDRequest_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "copy")
        {
            if (FlowObj.CopyPDRequest(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
                Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseRequest.aspx?loid=" + FlowObj.LOID.ToString());
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    protected void grvPDRequest_RowDataBound(object sender, GridViewRowEventArgs e)
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

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.Purchase, Convert.ToDouble(drow["PRLOID"])) + "return false;";
            btnCopy.CommandArgument = drow["PRLOID"].ToString();

            chk.Enabled = (drow["PRSTATUSNAME"].ToString() == Constz.Requisition.Status.Waiting.Name || (drow["PRSTATUSNAME"].ToString() == Constz.Requisition.Status.SP.Name && (Authz.CurrentUserInfo.DivisionID == Constz.PurchaseDepartment.LOID || Authz.CurrentUserInfo.DivisionID == Constz.AdminDepartment.LOID)));

            btnCopy.OnClientClick = "return confirm('ยืนยันการคัดลอกใบบันทึกรายการเพื่อการจัดซื้อเป็นเลขที่ใหม่');";

            if (drow["PRSTATUS"].ToString() == Constz.Requisition.Status.Waiting.Code & drow["REDWA"].ToString() == "Y")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (drow["PRSTATUS"].ToString() == Constz.Requisition.Status.SP.Code & drow["REDWA"].ToString() == "Y")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
            else if (drow["PRSTATUS"].ToString() == Constz.Requisition.Status.Approved.Code & drow["POSTATUS"].ToString() == Constz.Requisition.Status.Waiting.Code & drow["REDAP"].ToString() == "Y")
            {
                e.Row.ForeColor = System.Drawing.Color.Red;
            }

            if (e.Row.RowIndex > 0)
            {
                //ผสานเซล ลำดับที่, เลขที่, วันที่
                int rowSpan = 0;
                while (drow["PRLOID"].ToString() == this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan - 1].Cells[3].Text)
                {
                    rowSpan += 1;
                    if (e.Row.RowIndex - rowSpan == 0) break;
                }
                if (rowSpan > 0)
                {
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[0].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[1].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[2].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[3].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[4].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[5].RowSpan = rowSpan + 1;

                    for (int i = 0; i <= 5; ++i)
                    {
                        e.Row.Cells[i].CssClass = "zHidden";
                    }
                }

                //ผสานเซล ชื่อสินค้า,จำนวน PR, หน่วย, ผู้ขอซื้อ, ประเภท, สถานะ PR
                rowSpan = 0;
                while (drow["PRODUCTNAME"].ToString() == this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan - 1].Cells[6].Text &&
                    drow["PRLOID"].ToString() == this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan - 1].Cells[3].Text)
                {
                    
                    rowSpan += 1;
                    if (e.Row.RowIndex - rowSpan == 0) break;
                }
                if (rowSpan > 0)
                {
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[6].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[7].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[8].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[9].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[10].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[11].RowSpan = rowSpan + 1;
                    for (int i = 6; i <= 11; ++i)
                    {
                        e.Row.Cells[i].CssClass = "zHidden";
                    }
                }


                //ผสานเซล เลขที่ PO, จำนวน PO, สถานะ PO
                rowSpan = 0;
                while (drow["POCODE"].ToString() == this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan - 1].Cells[12].Text &&
                    drow["PRILOID"].ToString() == this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan - 1].Cells[19].Text)
                {

                    rowSpan += 1;
                    if (e.Row.RowIndex - rowSpan == 0) break;
                }
                if (rowSpan > 0)
                {
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[12].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[13].RowSpan = rowSpan + 1;
                    this.grvPDRequest.Rows[e.Row.RowIndex - rowSpan].Cells[14].RowSpan = rowSpan + 1;
                    for (int i = 12; i <= 14; ++i)
                    {
                        e.Row.Cells[i].CssClass = "zHidden";
                    }
                }
            }
        }
    }

    protected void btnVoid_Click(object sender, EventArgs e)
    {
        if (FlowObj.VoidData(Authz.CurrentUserInfo.UserID, GetChecked()))
            Search();
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }
}
