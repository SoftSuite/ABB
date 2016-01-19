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
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
using ABB.Global;

public partial class Transaction_PopupRequestSearch : System.Web.UI.Page
{
    private void SearchData()
    {

        ReturnRequestFlow flow = new ReturnRequestFlow();
        ReturnRequestData data = new ReturnRequestData();
        data.LOTNO = this.txtLotno.Text.Trim();
        data.PRODUCT = this.cmbProduct.SelectedValue;

        this.grvRequisition.DataSource = flow.GetPopUpList(data);

        this.grvRequisition.DataBind();
        // this.btnSelect.Visible = (this.grvReserve.Rows.Count > 0);
        if (this.grvRequisition.SelectedValue == null)
            this.txtRef.Text = "";
        else
            this.txtRef.Text = this.grvRequisition.SelectedValue.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProduct, "V_PRODUCT_LIST", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");

            //this.cmbRequisitionType.DataBind();

            // this.btnSelect.OnClientClick = "window.returnValue=document.getElementById('" + this.txtRefNo.ClientID + "').value; window.close(); return false;";
            this.btnClose.OnClientClick = "window.returnValue=document.getElementById('" + this.txtRef.ClientID + "').value; window.close(); return false;";

            if (Request["code"] != null) this.txtLotno.Text = Request["code"];
            SearchData();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
}
