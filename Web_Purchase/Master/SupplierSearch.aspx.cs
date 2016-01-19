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
using ABB.Data.Purchase;
using ABB.Flow.Purchase;
using ABB.Global;

/// <summary>
/// Create by: Ta
/// Create Date: 8 Jan 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

public partial class Master_SupplierSearch : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ToolbarControl1.ClientClickDelete = "return confirm('คุณต้องการลบข้อมูลผู้จำหน่ายที่เลือกใช่หรือไม่?');";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + gvResult.ClientID + "_ctl', '_chkItem')"; }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect("Supplier.aspx");
    }

    protected void imbSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchSupplier();
    }

    private void SearchSupplier()
    {
        SupplierSearchFlow ssFlow = new SupplierSearchFlow();
        ArrayList zArr = ssFlow.GetSearchSupplier(GetSearchData());
        gvResult.DataSource = zArr;
        gvResult.DataBind();
    }

    private SupplierSearchData GetSearchData()
    {
        SupplierSearchData supData = new SupplierSearchData();
        supData.CODE = txtSupCode.Text.Trim();
        supData.SUPPLIERNAME = txtSupName.Text.Trim();

        return supData;
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
            HyperLink hplSupplierName = (HyperLink)e.Row.FindControl("hplSupplierName");
            hplSupplierName.NavigateUrl = "Supplier.aspx?SupplierLOID=" + e.Row.Cells[1].Text.Trim();
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
        SupplierSearchFlow ssFlow = new SupplierSearchFlow();
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
            ret = ssFlow.DeleteData(arrLOID);
        }

        if (ret == true)
        {
            Appz.ClientAlert(Page, "ทำการลบข้อมูลผู้จำหน่ายเรียบร้อย");
            SearchSupplier();
        }
        else
            Appz.ClientAlert(Page, ssFlow.ErrorMessage);
    }
}
