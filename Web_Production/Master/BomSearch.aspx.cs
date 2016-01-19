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
using ABB.Data.Production ;
using ABB.Flow.Production;

/// <summary>
/// Create by: Nang
/// Create Date: 13 Feb 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 
public partial class Transaction_BomSearch : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ToolbarControl1.ClientClickDelete = "return confirm('คุณต้องการลบข้อมูล BOM ที่เลือกใช่หรือไม่?');";
    }

    private BomFlow _flow;

    private BomFlow FlowObj
    {
        get { if (_flow == null) { _flow = new BomFlow(); } return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.gvResult.ClientID + "_ctl', '_chkItem')"; }
    }

    private void SetProductGroup()
    {
        ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + this.cmbProductType.SelectedValue + " ", "ทั้งหมด", "0");
    }

    private void SearchData()
    {
        BomSearchData data = new BomSearchData();
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedValue);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedValue);
        data.PRODUCTNAME = this.txtProductName.Text.Trim();

        this.gvResult.DataSource = FlowObj.GetBomProductList(data);
        this.gvResult.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "TYPE = '" + Constz.ProductType.Type.FG.Code + "' AND ACTIVE = '" + Constz.ActiveStatus.Active + "' ","ทั้งหมด","0");
            SetProductGroup();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetProductGroup();
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect("Bom.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        DeleteData();
    }

    public void DeleteData()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < gvResult.Rows.Count; i++)
        {
            CheckBox chkItem = (CheckBox)gvResult.Rows[i].Cells[0].FindControl("chkItem");
            if (chkItem.Checked == true)
            {
                arrLOID.Add(Convert.ToDouble(gvResult.Rows[i].Cells[1].Text));
            }
        }

        if (FlowObj.DeleteBomData(arrLOID))
        {
            Appz.ClientAlert(Page, "ทำการลบข้อมูล BOM เรียบร้อย");
            SearchData();
        }
        else
            Appz.ClientAlert(Page, FlowObj.ErrorMessage);
    }

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
    }
}
