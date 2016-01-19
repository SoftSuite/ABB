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
using ABB.Flow.Handheld.FG;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Global;

public partial class FG_Transaction_StockInPO_StockInList : System.Web.UI.Page
{
    private StockInPOFlow _flow;

    private StockInPOFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPOFlow(); } return _flow; }
    }

    private void SearchData()
    {
        string status = this.cmbStatus.SelectedItem.Value;
        this.grvData.DataSource = FlowObj.GetStockInPOList(Constz.DocType.RecProduct.LOID, status);
        this.grvData.DataBind();
        ResetState();
    }

    private void ResetState()
    {
        this.txtLOID.Text = "";
        if (this.grvData.Rows.Count > 0)
        {
            this.grvData.SelectedIndex = 0;
            this.txtLOID.Text = this.grvData.Rows[0].Cells[1].Text;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.cmbStatus.Items.Clear();
            this.cmbStatus.Items.Add(new ListItem("รับเบื้องต้น", Constz.Requisition.Status.Waiting.Code));
            this.cmbStatus.Items.Add(new ListItem("รอรับเข้า", Constz.Requisition.Status.Approved.Code));
            SearchData();
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/NewPO.aspx");
    }

    protected void HelpClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/Help.aspx");
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Default.aspx");
    }

    protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.grvData.SelectedValue != null)
        {
            this.txtLOID.Text = this.grvData.SelectedValue.ToString();
            if (this.cmbStatus.SelectedItem.Value == Constz.Requisition.Status.Waiting.Code)
                Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/ProductList.aspx?loid=" + this.txtLOID.Text);
            else
                Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPO/ProductQCList.aspx?loid=" + this.txtLOID.Text);
        }
    }

    protected void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        SearchData();
    }

}
