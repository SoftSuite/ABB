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

public partial class FG_Transaction_StockInPD_StockInList : System.Web.UI.Page
{
    private StockInPDFlow _flow;

    private StockInPDFlow FlowObj
    {
        get { if (_flow == null) { _flow = new StockInPDFlow(); } return _flow; }
    }

    private void SearchData()
    {
        this.grvData.DataSource = FlowObj.GetStockInPDList(Constz.DocType.DelProduct.LOID);
        this.grvData.DataBind();
        ResetState();
    }

    private void ResetState()
    {
        this.pnlData.Visible = true;
        this.txtLOID.Text = "";
        if (this.grvData.Rows.Count > 0) 
        {
            this.grvData.SelectedIndex = 0;
            this.txtLOID.Text = this.grvData.Rows[0].Cells[1].Text;
        }
        this.btnSelect.Visible = false;
        this.ctlToolbar.Visible = true;
        this.pnlMessage.Visible = false;
        this.btnCancel.Text = "กลับเมนู";
        this.lblCode.Text = "";
        this.lblDate.Text = "";
    }

    private void AddData()
    {
        bool ret = true;
        StockInData data = new StockInData();
        data.DOCTYPE = Constz.DocType.DelProduct.LOID;
        data.RECEIVEDATE = DateTime.Now;
        data.SENDER = Authz.CurrentUserInfo.Warehouse;
        data.RECEIVER = Authz.CurrentUserInfo.Warehouse;
        data.STATUS = Constz.Requisition.Status.Waiting.Code;
        ret = FlowObj.InsertStockIn(Authz.CurrentUserInfo.UserID, data);
        if (ret)
        {
            this.pnlData.Visible = false;
            this.btnSelect.Visible = true;
            this.ctlToolbar.Visible = false;
            this.pnlMessage.Visible = true;
            this.btnCancel.Text = "ยกเลิก";
            data = FlowObj.GetData(FlowObj.LOID);
            this.txtLOID.Text = FlowObj.LOID.ToString();
            this.lblCode.Text = data.CODE;
            this.lblDate.Text = data.RECEIVEDATE.ToString(Constz.DateFormat);
        }
        else
        {
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SearchData();
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        AddData();
    }

    protected void HelpClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/Help.aspx");
    }

    protected void btnSelect_Click(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/AddProduct.aspx?loid=" + this.txtLOID.Text);
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (this.pnlMessage.Visible)
        {
            FlowObj.DeleteStockIn(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
            ResetState();
        }
        else
        {
            Response.Redirect(Constz.HomeFolder + "FG/Default.aspx");
        }
    }

    protected void grvData_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.grvData.SelectedValue != null)
        {
            this.txtLOID.Text = this.grvData.SelectedValue.ToString();
            Response.Redirect(Constz.HomeFolder + "FG/Transaction/StockInPD/AddProduct.aspx?loid=" + this.txtLOID.Text);
        }
    }

}
