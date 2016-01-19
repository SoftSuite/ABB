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

public partial class Master_PromotionSalesSearch : System.Web.UI.Page
{
    private PromotionSaleFlow _flow;
    private PromotionSaleFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PromotionSaleFlow(); } return _flow; }
    }

    private void ResetSate(string sWhere)
    {
        this.grvPromotionSale.DataSource = FlowObj.GetDataList(sWhere);
        this.grvPromotionSale.DataBind();
    }
    private PromotionSaleData GetData()
    {
        PromotionSaleData data = new PromotionSaleData();
        data.WAREHOUSE = Convert.ToDouble(this.cmbWarehouse.SelectedItem.Value);
        //data.ZONE = Convert.ToDouble(this.cmbZone.SelectedItem.Value);
        data.EFDATEFROM = this.ctlEFDateFrom.DateValue;
        data.EFDATETO = this.ctlEFDateTo.DateValue;
        data.EPDATEFROM = this.ctlEPDateFrom.DateValue;
        data.EPDATETO = this.ctlEPDateTo.DateValue;
        return data;
    }
    private void Search()
    {
        this.grvPromotionSale.DataSource = FlowObj.GetPDRequestList(GetData());
        this.grvPromotionSale.DataBind();
    }
    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvPromotionSale.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvPromotionSale.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvPromotionSale.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked) { arrLOID.Add(Convert.ToDouble(this.grvPromotionSale.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    //private void SetZoneCombo()
    //{
    //    ComboSource.BuildCombo(cmbZone, "ZONE", "NAME", "LOID", "NAME", "ACTIVE='" + Constz.ActiveStatus.Active + "' AND WAREHOUSE = " + this.cmbWarehouse.SelectedItem.Value + " ", "ทั้งหมด", "0");
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "TYPE = '" + Constz.Warehouse.Type.FG.Code + "' AND ACTIVE='" + Constz.ActiveStatus.Active + "' ", "ทั้งหมด", "0");
            //SetZoneCombo();
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบรายการส่งเสริมการขายใช่หรือไม่?');";
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/PromotionSales.aspx");
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        if (FlowObj.DeleteData(GetChecked()))
        {
            ResetSate((string)ViewState["sWhere"]);
        }
        else
        {
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    protected void grvPromotionSale_RowDataBound(object sender, GridViewRowEventArgs e)
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
        //string sWhere = " WHERE 1=1";
        //if (this.cmbWarehouse.SelectedValue != "" && this.cmbWarehouse.SelectedValue != "0")
        //{
        //    sWhere += " AND WAREHOUSE = " + this.cmbWarehouse.SelectedValue;
        //}
        //if (this.cmbZone.SelectedValue != "" && this.cmbZone.SelectedValue != "0")
        //{
        //    sWhere += " AND ZONE = " + this.cmbZone.SelectedValue;
        //}
        //if (this.ctlEFDateFrom.DateValue.Year != 1)
        //{
        //    sWhere += " AND EFDATE >= '" + this.ctlEFDateFrom.DateValue + "' ";
        //}
        //if (this.ctlEFDateTo.DateValue.Year != 1)
        //{
        //    sWhere += " AND EFDATE <= '" + this.ctlEFDateTo.DateValue + "' ";
        //}
        //if (this.ctlEPDateFrom.DateValue.Year != 1)
        //{
        //    sWhere += " AND EPDATE >= '" + this.ctlEPDateFrom.DateValue + "' ";
        //}
        //if (this.ctlEPDateTo.DateValue.Year != 1)
        //{
        //    sWhere += " AND EPDATE <= '" + this.ctlEPDateTo.DateValue + "' ";
        //}
        //ViewState["sWhere"] = sWhere;
        //ResetSate(sWhere);
        Search();
    }

    protected void cmbWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {
        //SetZoneCombo();
    }

}
