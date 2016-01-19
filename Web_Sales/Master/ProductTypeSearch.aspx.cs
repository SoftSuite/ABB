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
using ABB.Flow.Sales;
using ABB.Global;

public partial class Master_ProductTypeSearch : System.Web.UI.Page
{
    private ProductTypeFlow _flow;
    private ProductTypeFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductTypeFlow(); } return _flow; }
    }

    private void ResetSate()
    {
        this.grvProductType.DataSource = FlowObj.GetDataList();
        this.grvProductType.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvProductType.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvProductType.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvProductType.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvProductType.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetSate();
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบประเภทสินค้าใช่หรือไม่?');";
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/ProductType.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        ArrayList arr = GetChecked();
            if (arr.Count > 0)
                {
                    if (FlowObj.DeleteData(arr))
                        {
                               ResetSate();
                                Appz.ClientAlert(this, "ลบรายการเรียบร้อยแล้ว");
                        }
                    else
                        Appz.ClientAlert(this, FlowObj.ErrorMessage);

                }
            else
                Appz.ClientAlert(this, "กรุณาเลือกรายการที่ต้องการ");
    }

    protected void grvProductType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
        }
    }
}
