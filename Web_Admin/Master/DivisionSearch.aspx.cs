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

public partial class Master_DivisionSearch : System.Web.UI.Page
{
    private DivisionFlow _flow;
    private DivisionFlow FlowObj
    {
        get { if (_flow == null) { _flow = new DivisionFlow(); } return _flow; }
    }

    private void ResetSate()
    {
        this.grvDivision.DataSource = FlowObj.GetDivisionList();
        this.grvDivision.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvDivision.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvDivision.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvDivision.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvDivision.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetSate();
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบฝ่ายนี้ใช่หรือไม่?');";
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/Division.aspx");
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

    protected void grvDivision_RowDataBound(object sender, GridViewRowEventArgs e)
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


