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

public partial class Transaction_BomSearch : System.Web.UI.Page
{
    private void SearchData()
    {
        
        
        this.txtProduct.Text = "";
        //SearchFlow flow = new SearchFlow();
        //SearchCustomerData data = new SearchCustomerData();
        //data.CODE = this.txtCode.Text.Trim();
        //data.CUSTOMERTYPE = this.cmbCustomerType.SelectedItem.Value;
        //data.FULLNAME = this.txtFullName.Text.Trim();
        //data.MEMBERTYPE = Convert.ToDouble(this.cmbMemberType.SelectedItem.Value);
        //this.grvCustomer.DataSource = flow.GetCustomerList(data);
        //this.grvCustomer.DataBind();
        //if (this.grvCustomer.SelectedValue == null)
        //    this.txtCustomer.Text = "";
        //else
        //this.txtProduct.Text = this.grvCus
        
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '1'");
        ComboSource.BuildCombo(this.cmbProductGroup,"PRODUCTGROUP","NAME","LOID","NAME","ACTIVE = '1' AND PRODUCTTYPE = "+ this.cmbProductType.SelectedValue +"");
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }
}
