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
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;

public partial class Transaction_EditPOSearch : System.Web.UI.Page
{
    private EditPOFlow _flow;
    public EditPOFlow FlowObj
    {
        get { if (_flow == null) _flow = new EditPOFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPDOrder.ClientID + "_ctl', '_chkItem')"; }
    }

    private POEditData GetData()
    {
        POEditData data = new POEditData();
        data.PECODE = this.txtPECode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.PODATEFROM = this.ctlPODateFrom.DateValue;
        data.PODATETO = this.ctlPODateTo.DateValue;
        data.POCODE = this.txtPOCode.Text.Trim();
        data.STATUSFROM = this.cmbStatusFrom.SelectedItem.Value;
        data.STATUSTO = this.cmbStatusTo.SelectedItem.Value;
        data.SUPPLIER = this.cmbSupplier.SelectedItem.Value;
        return data;
    }

    private void Search()
    {
        this.grvPDOrder.DataSource = FlowObj.GetPOEditList(GetData());
        this.grvPDOrder.DataBind();
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDOrder.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDOrder.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPDOrder.Rows[i].Cells[5].Text)); }
        }
        return arrLOID;
    }
    private ArrayList GetCheckedOld()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPDOrder.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPDOrder.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvPDOrder.Rows[i].Cells[4].Text)); }
        }
        return arrLOID;
    }
    private ArrayList GetCheckedPOEdit()
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
            ComboSource.BuildStatusRankCombo(this.cmbStatusFrom);
            ComboSource.BuildStatusRankCombo(this.cmbStatusTo);
            ComboSource.BuildCombo(this.cmbSupplier, "SUPPLIER", "SUPPLIERNAME", "LOID", "SUPPLIERNAME", "", "ทั้งหมด", "0");
            //Search();
           
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบสั่งซื้อใช่หรือไม่?');";
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateActiveStatus(GetCheckedOld(), Constz.ActiveStatus.Active, Authz.CurrentUserInfo.UserID))
        {
            if (FlowObj.DeleteData(GetChecked()))
            {
                if (FlowObj.DeleteDataPOEdit(GetCheckedPOEdit()))
                    Search();
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/EditPO.aspx");
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdatePOEditStatus(GetCheckedPOEdit(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
        {
            if (FlowObj.UpdatePDOrderStatus(GetCheckedOld(), Constz.Requisition.Status.Void.Code, Authz.CurrentUserInfo.UserID))
            {
                if (FlowObj.UpdatePDOrderStatus(GetChecked(), Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
                { 
                        Search();
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
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
        //if (e.CommandName == "copy")
        //{
        //    if (FlowObj.CopyPDOrder(Authz.CurrentUserInfo.UserID, Convert.ToDouble(e.CommandArgument)))
        //        Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseOrder.aspx?loid=" + FlowObj.LOID.ToString());
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
        //else if (e.CommandName == "cancelpdorder")
        //{
        //    ArrayList arr = new ArrayList();
        //    arr.Add(Convert.ToDouble(e.CommandArgument));
        //    if (FlowObj.UpdatePDOrderStatus(arr, Constz.Requisition.Status.Void.Code, Authz.CurrentUserInfo.UserID))
        //        Search();
        //    else
        //        Appz.ClientAlert(this, FlowObj.ErrorMessage);
        //}
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
            //ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");
            //ImageButton btnCancel = (ImageButton)e.Row.Cells[1].FindControl("btnCancel");

            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.PurchaseOrder, Convert.ToDouble(drow["PONEW"])) + "return false;";
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
