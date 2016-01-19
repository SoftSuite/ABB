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
using ABB.Data.Admin;
using ABB.Flow.Admin;
using ABB.Global;

public partial class Master_OfficerSearch : System.Web.UI.Page
{
    private OfficerFlow _flow;
    private OfficerFlow FlowObj
    {
        get { if (_flow == null) { _flow = new OfficerFlow(); } return _flow; }
    }

    private void ResetSate()
    {
        this.grvItem.DataSource = FlowObj.GetDataList();
        this.grvItem.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvItem.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvItem.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvItem.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvItem.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetSate();
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบข้อมูลพนักงานนี้ใช่หรือไม่?');";
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/Officer.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        try
        {
            FlowObj.DeleteData(GetChecked());
            ResetSate();
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
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
