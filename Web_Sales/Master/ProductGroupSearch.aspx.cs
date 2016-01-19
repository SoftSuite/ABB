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

public partial class Master_ProductGroupSearch : System.Web.UI.Page
{
    private ProductGroupFlow _flow;
    private ProductGroupFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductGroupFlow(); } return _flow; }
    }

    private void ResetSate()
    {
        this.grvProductGroup.DataSource = FlowObj.GetDataList();
        this.grvProductGroup.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvProductGroup.ClientID + "_ctl', '_chkItem')"; } 
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i=0; i<this.grvProductGroup.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvProductGroup.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) {arrLOID.Add(Convert.ToDouble(this.grvProductGroup.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetSate();
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบกลุ่มสินค้าใช่หรือไม่?');";
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/ProductGroup.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
        {
            ResetSate();
            Appz.ClientAlert(this, "ลบรายการเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void grvProductGroup_RowDataBound(object sender, GridViewRowEventArgs e)
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
