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
using ABB.Data.Inventory.FG.Master;
using ABB.Flow.Inventory.FG.Master;
using ABB.Global;

public partial class FG_Master_BasketSearch : System.Web.UI.Page
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ToolbarControl1.ClientClickDelete = "return confirm('คุณต้องการลบรายการกระเช้าที่เลือกใช่หรือไม่?');";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + gvResult.ClientID + "_ctl', '_chkItem')"; }
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
            //HyperLink hplBarCode = (HyperLink)e.Row.FindControl("hplBarCode");
            //hplBarCode.NavigateUrl = "Basket.aspx?Barcode=" + e.Row.Cells[4].Text.Trim();
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect("Basket.aspx");
    }

    protected void imbSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchBasket();
    }

    private void SearchBasket()
    {
        BasketSearchFlow csFlow = new BasketSearchFlow();
        ArrayList zArr = csFlow.GetSearchBasket(GetSearchData());
        gvResult.DataSource = zArr;
        gvResult.DataBind();
    }

    private BasketSearchData GetSearchData()
    {
        BasketSearchData data = new BasketSearchData();
        data.BARCODE = txtBarcode.Text.Trim();
        data.PRODUCTNAME = txtBasketName.Text.Trim();
        return data;
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        DeleteData();
    }

    public void DeleteData()
    {
        double LOID;
        ArrayList arrLOID = new ArrayList();
        BasketSearchFlow csFlow = new BasketSearchFlow();
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
            Appz.ClientAlert(Page, "ทำการลบข้อมูลกระเช้าเรียบร้อย");
            SearchBasket();
        }
        else
            Appz.ClientAlert(Page, csFlow.ErrorMessage);
    }
}
