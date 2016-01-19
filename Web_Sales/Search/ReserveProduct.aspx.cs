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
using ABB.Data.Sales;
using ABB.Data.Search;
using ABB.Flow.Search;
public partial class Search_ReserveProduct : System.Web.UI.Page
{
    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvReserve.ClientID + "_ctl', '_chkItem')"; }
    }
    private void SearchData()
    {
        this.txtRefNo.Text = "";
        SearchFlow flow = new SearchFlow();
        ProductReserveSearchData data = new ProductReserveSearchData();
        data.REQUISITIONTYPE = Convert.ToDouble(this.cmbRequisitionType.SelectedValue);
        if (this.cmbCustomer.SelectedValue != "")
            data.CUSTOMER = Convert.ToDouble(this.cmbCustomer.SelectedItem.Value == "" ? "0" : this.cmbCustomer.SelectedItem.Value);
        data.CODE = this.txtPopup.Text;


        this.grvReserve.DataSource = flow.GetReserveList(data);
        this.grvReserve.DataBind();
        if (this.grvReserve.SelectedValue == null)
            this.txtRefNo.Text = "";
        else
            this.txtRefNo.Text = this.grvReserve.SelectedValue.ToString();

        this.btnSelect.Visible = (this.grvReserve.Rows.Count > 0);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";

        if (!IsPostBack)
        {

            this.btnClose.OnClientClick = "window.close(); return false;";

            if (Request["customer"] != "0")
            {
                string where1 = " LOID <> 1 ";
                where1 += (where1 == "" ? "" : "AND ") + "LOID = " + Request["requsitiontype"] + " ";
                ComboSource.BuildCombo(this.cmbRequisitionType, "V_REQTYPE_INVOICE", "NAME", "LOID", "NAME", where1);//ไม่เอาใบสั่งซื้อ/สั่งจอง
                this.cmbRequisitionType.DataBind();
                string where2 = " REFTYPELOID = " + this.cmbRequisitionType.SelectedValue;
                where2 += (where2 == "" ? "" : "AND ") + "LOID = " + Request["customer"] + " ";

                ComboSource.BuildCombo(this.cmbCustomer, "CUSTOMER", "NAME", "LOID", "NAME", "LOID = " + Request["customer"]);
                this.cmbCustomer.DataBind();
            }
            else
            {
                ComboSource.BuildCombo(this.cmbRequisitionType, "V_REQTYPE_INVOICE", "NAME", "LOID", "NAME", " LOID <> 1" );//ไม่เอาใบสั่งซื้อ/สั่งจอง
                this.cmbRequisitionType.DataBind();
                ComboSource.BuildComboDistinct(this.cmbCustomer, "V_PRODUCT_INVOICE", "CUSTOMERNAME", "CULOID", "CUSTOMERNAME", " REFTYPELOID = '" + this.cmbRequisitionType.SelectedValue + "' " );
                this.cmbCustomer.DataBind();

            }

                

            if (Request["popup"] != null) this.txtPopup.Text = Request["popup"];

            SearchData();
        }
        AddClientClick();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvReserve_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            //      ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
    protected void btnSelect_Click(object sender, EventArgs e)
    {

    }

    public void AddClientClick()
    {
        string sReturn = cmbRequisitionType.SelectedValue;
        sReturn += "/";
        sReturn += cmbCustomer.SelectedValue;
        sReturn += "/";

        string chkGrid = "";
        foreach (GridViewRow row in grvReserve.Rows)
        {
            CheckBox chk = (CheckBox)row.Cells[0].FindControl("chkItem");
            if (chk.Checked)
                chkGrid += (chkGrid.Length > 0 ? "," : "") + row.Cells[5].Text;
        }

        sReturn += chkGrid;
        btnSelect.OnClientClick = "window.returnValue='" + sReturn + "'; window.close(); return false;";
    }
    protected void cmbRequisitionType_SelectedIndexChanged(object sender, EventArgs e)
    {
        ComboSource.BuildComboDistinct(this.cmbCustomer, "V_PRODUCT_INVOICE", "CUSTOMERNAME", "CULOID", "CUSTOMERNAME", "REFTYPELOID =" + this.cmbRequisitionType.SelectedValue);
        this.cmbCustomer.DataBind();

        this.grvReserve.DataSource = null;
        this.grvReserve.DataBind();

        AddClientClick();
    }
    protected void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        AddClientClick();
    }
    protected void chk_CheckChanged(object sender, EventArgs e)
    {
        AddClientClick();
    }

}
