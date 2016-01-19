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
using ABB.Data.Search;
using ABB.Flow.Search;
using ABB.Global;

public partial class Search_ProductPlan : System.Web.UI.Page
{
    private SearchProductPlanData GetSearchData()
    {
        SearchProductPlanData data = new SearchProductPlanData();
        data.PLAN = Convert.ToDouble(Request["plan"] == null ? "0" : Request["plan"]);
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        return data;
    }

    private void Search()
    {
        SearchFlow sFlow = new SearchFlow();
        this.grvProduct.DataSource = sFlow.GetProductPlanList(GetSearchData());
        this.grvProduct.DataBind();
        string scriptItem = "";
        if (this.grvProduct.Rows.Count > 0)
        {
            this.btnOK.Visible = true;
            CheckBox chkAll = (CheckBox)this.grvProduct.HeaderRow.Cells[0].FindControl("chkAll");
            scriptItem += "document.getElementById('" + this.txtProduct.ClientID + "').value = '';\r\n";
            scriptItem += "var chk = document.getElementById('" + chkAll.ClientID + "').checked;\r\n";
            foreach (GridViewRow gRow in this.grvProduct.Rows)
            {
                scriptItem += "document.getElementById('" + ((CheckBox)gRow.Cells[0].FindControl("chkItem")).ClientID + "').checked = chk;\r\n";
                scriptItem += "if (chk) document.getElementById('" + this.txtProduct.ClientID + "').value += '" + gRow.Cells[1].Text + ",';\r\n";
            }
            chkAll.Attributes.Add("onclick", scriptItem);
        }
        else
            this.btnOK.Visible = false;
    }

    private void SetProductGroup()
    {
        ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "PRODUCTTYPE = " + this.cmbProductType.SelectedItem.Value + " AND ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND TYPE = '" + Constz.ProductType.Type.FG.Code + "' ", "ทั้งหมด", "0");
            SetProductGroup();
            //Search();
            this.btnClose.OnClientClick = "window.close(); return false;";
            this.btnOK.OnClientClick = "if (document.getElementById('" + this.txtProduct.ClientID + "').value == '') { alert('กรุณาเลือกรายการสินค้า'); } else {window.returnValue=document.getElementById('" + this.txtProduct.ClientID + "').value; window.close(); } return false; ";
        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetProductGroup();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        Search();
    }

    protected void grvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chkItem = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            string scriptItem = "";
            scriptItem += "if (document.getElementById('" + chkItem.ClientID + "').checked) document.getElementById('" + this.txtProduct.ClientID + "').value += '" + drow["LOID"].ToString() + ",'; ";
            scriptItem += "else document.getElementById('" + this.txtProduct.ClientID + "').value = document.getElementById('" + this.txtProduct.ClientID + "').value.replace('" + drow["LOID"].ToString() + ",',''); ";
            chkItem.Attributes.Add("onclick", scriptItem);
        }
    }

}
