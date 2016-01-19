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
using ABB.Data.Search;
using ABB.Flow.Search;

public partial class Search_CustRetProductSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        this.txtCustomer.Text = "";
        SearchFlow flow = new SearchFlow();
        SearchCustomerData data = new SearchCustomerData();
        data.CODE = this.txtCode.Text.Trim();
        data.CUSTOMERTYPE = this.cmbCustomerType.SelectedItem.Value;
        data.FULLNAME = this.txtFullName.Text.Trim();
        data.MEMBERTYPE = Convert.ToDouble(this.cmbMemberType.SelectedItem.Value);
        this.grvCustomer.DataSource = flow.GetCustRetProductList(data);
        this.grvCustomer.DataBind();
        if (this.grvCustomer.SelectedValue == null)
            this.txtCustomer.Text = "";
        else
            this.txtCustomer.Text = this.grvCustomer.SelectedValue.ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            ComboSource.BuildCombo(this.cmbMemberType, "MEMBERTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            this.cmbCustomerType.Items.Clear();
            this.cmbCustomerType.Items.Add(new ListItem("เลือก", ""));
            this.cmbCustomerType.Items.Add(new ListItem(Constz.CustomerType.Company.Name, Constz.CustomerType.Company.Code));
            this.cmbCustomerType.Items.Add(new ListItem(Constz.CustomerType.Government.Name, Constz.CustomerType.Government.Code));
            this.cmbCustomerType.Items.Add(new ListItem(Constz.CustomerType.Personal.Name, Constz.CustomerType.Personal.Code));
            this.cmbCustomerType.DataBind();

            this.btnClose.OnClientClick = "window.close(); return false;";

            if (Request["code"] != null) this.txtCode.Text = Request["code"];
            SearchData();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvCustomer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((ImageButton)e.Row.Cells[0].FindControl("btnSelect")).OnClientClick = "window.returnValue='" + drow["LOID"].ToString() + "'; window.close(); return false;";
        }
    }
}
