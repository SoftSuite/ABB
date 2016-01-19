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
using ABB.Flow.Sales;
using ABB.Global;

/// <summary>
/// Create by: Pom
/// Create Date: 13 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

public partial class Master_CustomerSearch : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ToolbarControl1.ClientClickDelete = "return confirm('คุณต้องการลบข้อมูลลูกค้าที่เลือกใช่หรือไม่?');";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
            SerComboSource();
    }

    private void SerComboSource()
    {
        this.radPersonal.Text = Constz.CustomerType.Personal.Name;
        this.radOrganize.Text = Constz.CustomerType.Government.Name;
        this.radPrivate.Text = Constz.CustomerType.Company.Name;
        ComboSource.BuildCombo(cmbMemberType, "MEMBERTYPE", "NAME", "LOID", "", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
        ComboSource.BuildCombo(cmbProvince, "PROVINCE", "NAME", "LOID", "", "ACTIVE= '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + gvResult.ClientID + "_ctl', '_chkItem')"; }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect("Customer.aspx");
    }

    protected void imbSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchCustomer();
    }

    private void SearchCustomer()
    {
        CustomerSearchFlow csFlow = new CustomerSearchFlow();
        ArrayList zArr = csFlow.GetSearchCustomer(GetSearchData());
        gvResult.DataSource = zArr;
        gvResult.DataBind();
    }

    private CustomerSearchData GetSearchData()
    {
        CustomerSearchData cusData = new CustomerSearchData();
        cusData.CUSCODE = txtCusCode.Text.Trim();
        cusData.CUSNAME = txtCusName.Text.Trim();
        cusData.LASTNAME = txtLastName.Text.Trim();

        if (radPersonal.Checked == true)
            cusData.CUSTYPE = Constz.CustomerType.Personal.Code;
        else if (radPrivate.Checked == true)
            cusData.CUSTYPE = Constz.CustomerType.Company.Code;
        else if (radOrganize.Checked == true)
            cusData.CUSTYPE = Constz.CustomerType.Government.Code;

        cusData.MEMBERTYPE = Convert.ToDouble(cmbMemberType.SelectedItem.Value);
        cusData.PROVINCE = Convert.ToDouble(cmbProvince.SelectedItem.Value);
        return cusData;
    }

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hplCusName = (HyperLink)e.Row.FindControl("hplCusName");
            hplCusName.NavigateUrl = "Customer.aspx?LOID=" + e.Row.Cells[1].Text.Trim();
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        DeleteData();
    }

    public void DeleteData()
    {
        double LOID;
        ArrayList arrLOID = new ArrayList();
        CustomerSearchFlow csFlow = new CustomerSearchFlow();
        bool ret = true;
      
        for (int i = 0; i < gvResult.Rows.Count; i++)
        {
            CheckBox chkItem = (CheckBox)gvResult.Rows[i].Cells[0].FindControl("chkItem");
            if (chkItem.Checked == true)
            {
                LOID = Convert.ToDouble(gvResult.Rows[i].Cells[1].Text);
                arrLOID.Add(LOID);
            }
        }
        if (arrLOID.Count != 0)
        {
            ret = csFlow.DeleteData(arrLOID);
        }

        if (ret == true)
        {
            Appz.ClientAlert(Page, "ทำการลบข้อมูลลูกค้าเรียบร้อย");
            SearchCustomer();
        } 
        else
            Appz.ClientAlert(Page, csFlow.ErrorMessage);       
    }
}
