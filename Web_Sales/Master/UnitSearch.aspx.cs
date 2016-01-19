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

public partial class Master_UnitSearch : System.Web.UI.Page
{
    private UnitFlow _flow;
    private UnitFlow FlowObj
    {
        get { if (_flow == null) { _flow = new UnitFlow(); } return _flow; }
    }

    private void ResetSate(string sWhere)
    {
        this.grvUnitSearch.DataSource = FlowObj.GetDataList(sWhere);
        this.grvUnitSearch.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvUnitSearch.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvUnitSearch.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvUnitSearch.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvUnitSearch.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบหน่วยนับใช่หรือไม่?');";
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/Unit.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (!FlowObj.DeleteData(GetChecked()))
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
        else
            ResetSate((string)ViewState["sWhere"]);
    }

    protected void grvUnitSearch_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string sWhere = " WHERE 1=1";
        if (this.txtUNName.Text.Trim() != "")
        {
            sWhere += " AND NAME LIKE '%" + this.txtUNName.Text.Trim() + "%'";
        }
        if (this.txtUNEname.Text.Trim() != "")
        {
            sWhere += " AND ENAME LIKE '%" + this.txtUNEname.Text.Trim() + "%'";
        }
        if (this.txtUNName.Text.Trim() != "" && this.txtUNEname.Text.Trim() != "")
        {
            sWhere += " AND NAME LIKE '%" + this.txtUNName.Text.Trim() + "%'";
            sWhere += " AND ENAME LIKE '%" + this.txtUNEname.Text.Trim() + "%'";
        }
        ViewState["sWhere"] = sWhere;
        ResetSate(sWhere);
    }
    
}
