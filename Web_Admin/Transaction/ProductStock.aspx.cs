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

public partial class Transaction_ProductStock : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "LOID in (1,2)");
            this.grvStock.DataBind();
        }
    }

    protected void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.grvStock.DataBind();
    }

    protected void grvStock_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtQty = (TextBox)e.Row.Cells[4].FindControl("txtQty");
            if (txtQty != null) ControlUtil.SetIntTextBox(txtQty);
        }
    }

    protected void grvStock_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        e.ExceptionHandled = (e.Exception != null);
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.InnerException.Message);
        }
    }

    protected void grvStock_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow gRow = this.grvStock.Rows[e.RowIndex];
        TextBox txtQty = (TextBox)gRow.Cells[4].FindControl("txtQty");
        e.NewValues["QTY"] = (txtQty.Text == "" ? "0" : txtQty.Text);
    }

}
