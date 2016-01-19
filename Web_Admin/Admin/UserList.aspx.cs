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
using ABB.Flow;
using ABB.Data;
using ABB.Global;

public partial class Admin_UserList : System.Web.UI.Page
{
    private UserFlow _flow;

    private UserFlow FlowObj
    {
        get { if (_flow == null) { _flow = new UserFlow(); } return _flow; }
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.gvMain.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrRole = new ArrayList();
        for (int i = 0; i < this.gvMain.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.gvMain.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrRole.Add(Convert.ToDouble(this.gvMain.Rows[i].Cells[5].Text)); }
        }
        return arrRole;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
            RefreshData();
            this.ToolbarCtl1.ClientClickDelete = "return confirm('ต้องการยกเลิกสิทธิ์ผู้ใช้งานที่เลือกใช่หรือไม่?');";
        }
    }

    private void RefreshData()
    {
        if (Page.Cache["UDATA"] == null)
        {
            LoadData();
        }
        gvMain.DataSource = Page.Cache["UDATA"];
        gvMain.DataBind();

    }

    private void LoadData()
    {
        Page.Cache["UDATA"] = FlowObj.GetUserList(txtSUserID.Text, txtSName.Text, cmbSLevel.SelectedValue.ToString());
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        LoadData();
        RefreshData();
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        try
        {
            if (FlowObj.InvokeRole(GetChecked()))
            {
                LoadData();
                RefreshData();
                Appz.ClientAlert(this, "ยกเลิกสิทธิ์การใช้งานเรียบร้อย");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void gvMain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkItem");
            if (Convert.ToDouble(drow["ROLE"]) == 0) chk.Enabled = false;
        }
    }
}
