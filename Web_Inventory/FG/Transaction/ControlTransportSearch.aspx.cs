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
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class FG_Transaction_ControlTransportSearch : System.Web.UI.Page
{
    private ControlTransportFlow _flow;
    public ControlTransportFlow FlowObj
    {
        get { if (_flow == null) _flow = new ControlTransportFlow(); return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private CtrlDeliveryData GetData()
    {
        CtrlDeliveryData data = new CtrlDeliveryData();
        data.CODE = this.txtCode.Text.Trim();
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.CARNO = this.txtCarNo.Text.Trim();
        data.DELIVERYNAME = this.txtDeliveryName.Text.Trim();
        return data;
    }

    private void Search()
    {
        this.grvRequisition.DataSource = FlowObj.GetDeliveryList(GetData());
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
            //   Search();

            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบใบคุมสินค้าส่งใช่หรือไม่?');";
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
 
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/ControlTransport.aspx");

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
            btnPrint.OnClientClick = ABB.Global.Appz.ReportScript(Constz.Report.ControlTransport, Convert.ToDouble(drow["LOID"])) + " return false;";
            btnPrint.CommandArgument = drow["LOID"].ToString();

            //ImageButton btnCopy = (ImageButton)e.Row.Cells[1].FindControl("btnCopy");

            //btnPrint.CommandArgument = drow["LOID"].ToString();
            //btnCopy.CommandArgument = drow["LOID"].ToString();


            //chk.Enabled = (drow["RANK"].ToString() != Constz.Requisition.Status.Approved.Rank);

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }
}
