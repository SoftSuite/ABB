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
using ABB.Data.Inventory;
using ABB.Flow;
using ABB.Flow.Inventory;
using ABB.Global;

public partial class Transaction_PopupProductSearch : System.Web.UI.Page
{
    private PopupProductSearchFlow _flow;
    private PopupProductSearchFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PopupProductSearchFlow(); } return _flow; }
    }

    private void ResetSate(string sWhere)
    {
        this.grvProduct.DataSource = FlowObj.GetDataList(sWhere);
        this.grvProduct.DataBind();
    }
    
    private void SetData(StockinProductData data)
    {
        this.txtLotNoFrom.Text = data.LOTNO;
        this.txtLotNoTo.Text = data.LOTNO;
        this.txtProductName.Text = data.PRODUCTNAME;
        this.PDDateFrom.DateValue = data.MFGDATE;
        this.PDDateTo.DateValue = data.MFGDATE;
        SearchData();
    }
    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvProduct.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvProduct.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvProduct.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvProduct.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    private void SearchData()
    {
        string sWhere = "PPD.TOWAREHOUSE = '" + this.txtWarehouse.Text + "' ";
        if (Request["producetype"] != "")
            sWhere += (sWhere == "" ? "" : " AND ") + "PPD.PRODUCETYPE = '" + Request["producetype"] + "' ";
        if (this.txtProductName.Text.Trim() != "")
        {
            sWhere += (sWhere == "" ? "" : " AND ") + "PD.NAME LIKE '%" + this.txtProductName.Text.Trim() + "%'";
        }
        if (this.txtLotNoFrom.Text.Trim() != "")
        {
            sWhere += (sWhere == "" ? "" : " AND ") + "PPD.LOTNO >= '" + this.txtLotNoFrom.Text.Trim() + "'";
        }
        if (this.txtLotNoTo.Text.Trim() != "")
        {
            sWhere += (sWhere == "" ? "" : " AND ") + "PPD.LOTNO <= '" + this.txtLotNoTo.Text.Trim() + "'";
        }
        if (this.PDDateFrom.DateValue.Year != 1)
        {
            sWhere += (sWhere == "" ? "" : "AND ") + "MFGDATE >= " + this.PDDateFrom.DateValue + " ";
        }
        if (this.PDDateTo.DateValue.Year != 1)
        {
            sWhere += (sWhere == "" ? "" : "AND ") + "MFGDATE <= " + this.PDDateTo.DateValue + " ";
        }
        ViewState["sWhere"] = (sWhere == "" ? "" : " WHERE ") + sWhere;
        ResetSate(sWhere);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Expires = 0;
        Response.AddHeader("pragma", "no-cache");
        Response.AddHeader("cache-control", "private");
        Response.CacheControl = "no-cache";
        if (Request["warehouse"] != null) this.txtWarehouse.Text = Request["warehouse"];

        if (!IsPostBack)
        {
            if (Request["producetype"] == Constz.ProductType.Type.WH.Code)
            {
                this.lblHeader.Text = "รายการสินค้าที่รอรับเข้าคลังวัตถุดิบ";
            }
            else
            {
                this.lblHeader.Text = "รายการสินค้าที่รอรับเข้าคลังสำเร็จรูป";
            }
        }
    }

    protected void grvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
            ((ImageButton)e.Row.Cells[0].FindControl("imbSelect")).OnClientClick = "window.returnValue = '" + drow["PRODUCTNAME"].ToString() + "|" + drow["LOTNO"].ToString() + "|" + drow["PDQTY"].ToString() + "|" + drow["UNITNAME"].ToString() + "|" + drow["PDPLOID"].ToString() + "|" + Convert.ToDateTime(drow["MFGDATE"]).ToString("dd/MM/yyyy") + "|" + drow["PRODUCTLOID"].ToString() + "|" + drow["UNITLOID"].ToString() + "|" + drow["PDPLOID"].ToString() + "';window.close(true);";
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }
}
