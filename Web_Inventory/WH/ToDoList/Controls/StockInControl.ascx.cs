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
using ABB.Data.Inventory.FG;
using ABB.Flow.Inventory.WH;
using ABB.Global;

public partial class WH_ToDoList_Controls_StockInControl : System.Web.UI.UserControl
{
    private string _orderType = "";
    private ToDoListFlow _flow;
    private int indexLOID = 0;
    private int indexRequestCode = 1;
    private int indexCheckBox = 2;
    private int indexDueDate = 3;
    private int indexName = 4;
    private int indexLink = 5;
    private int indexQty = 6;
    private int indexRemain = 7;
    private int indexUnitName = 8;
    private int indexSupplierName = 9;
    private int indexOrderTypeName = 10;
    private int indexCode = 11;
    private int indexStatus = 12;
    private int indexOrderType = 13;
    private int indexUnit = 14;
    private int indexProduct = 15;
    private int indexPrice = 16;
    private int indexSupplier = 17;

    public ToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ToDoListFlow(); } return _flow; }
    }

    private ToDoListStockInData GetSearchData()
    {
        ToDoListStockInData data = new ToDoListStockInData();
        data.CODE = this.txtCode.Text.Trim();
        data.DUEDATE = this.dtpDueDate.DateValue;
        data.ORDERTYPE = this.cmbOrderType.SelectedItem.Value;
        data.PRODUCTNAME = this.txtName.Text.Trim();
        data.STATUS = this.cmbStatus.SelectedItem.Value;
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
        return data;
    }

    private void SearchData()
    {
        this.grvRequisition.DataSource = FlowObj.GetStockInkList(GetSearchData());
        this.grvRequisition.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private ToDoListStockinOrderData GetData()
    {
        ToDoListStockinOrderData data = new ToDoListStockinOrderData();
        data.RECEIVEDATE = DateTime.Now.Date;
        data.RECEIVER = Authz.CurrentUserInfo.Warehouse;
        data.SENDER = Authz.CurrentUserInfo.Warehouse;
        data.STATUS = Constz.Requisition.Status.Waiting.Code;

        string orderType = "";
        string supplierName = "";
        double sender = 0;

        for (int i = 0; i < this.grvRequisition.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvRequisition.Rows[i].Cells[indexCheckBox].FindControl("chkItem");
            if (chk.Checked && chk.Enabled && this.grvRequisition.Rows[i].Cells[indexCheckBox].CssClass != "zhidden")
            {
                if (orderType == "")
                    orderType = this.grvRequisition.Rows[i].Cells[indexOrderType].Text;
                else if (orderType != this.grvRequisition.Rows[i].Cells[indexOrderType].Text)
                    throw new ApplicationException("ไม่สามารถทำรายการได้ เนื่องจากไม่ใช่ประเภทคำสั่งเดียวกัน");

                if (supplierName == "")
                    supplierName = this.grvRequisition.Rows[i].Cells[indexSupplierName].Text;
                else if (supplierName != this.grvRequisition.Rows[i].Cells[indexSupplierName].Text)
                     throw new ApplicationException("ไม่สามารถทำรายการได้ เนื่องจากไม่ใช่ผู้จำหน่ายดียวกัน");

                 sender = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[indexSupplier].Text);

                ToDoListStockInOrderItemData itemData = new ToDoListStockInOrderItemData();
                itemData.PRODUCT = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[indexProduct].Text);
                itemData.QTY = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[indexRemain].Text);
                itemData.LOTNO = "";
                string price = this.grvRequisition.Rows[i].Cells[indexPrice].Text;
                itemData.PRICE = Convert.ToDouble(price == "" ? "0" : price);
                itemData.REFLOID = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[indexLOID].Text);
                if (orderType == Constz.OrderType.PD.Code)
                    itemData.REFTABLE = "PDPRODUCT";
                else
                    itemData.REFTABLE = "POITEM";
                itemData.STATUS = Constz.Requisition.Status.Waiting.Code;
                itemData.UNIT = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[indexUnit].Text);
                data.ACCCODE = this.grvRequisition.Rows[i].Cells[indexRequestCode].Text;
                
                data.ITEM.Add(itemData);
            }
        }
        if (orderType == Constz.OrderType.PD.Code)
            data.DOCTYPE = Constz.DocType.DelRaw.LOID;
        else if (orderType == Constz.OrderType.PO.Code)
            data.DOCTYPE = Constz.DocType.RecRaw.LOID;
        data.SENDER = sender;
        _orderType = orderType;
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildOrderTypeCombo(this.cmbOrderType, "ทั้งหมด", "");
            ComboSource.BuildStatusCombo(this.cmbStatus, "ทั้งหมด", "");
            SearchData();
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        try
        {
            ToDoListStockinOrderData data = GetData();
            if (data.ITEM.Count == 0)
                throw new ApplicationException("กรุณาเลือกรายการสินค้า");
            else
            {
                if (FlowObj.NewStockIn(Authz.CurrentUserInfo.UserID, data))
                {
                    if (_orderType == Constz.OrderType.PD.Code)
                        Response.Redirect(Constz.HomeFolder + "Transaction/StockInProduction.aspx?poducetype=" + Constz.ProductType.Type.WH.Code + "&loid=" + FlowObj.LOID.ToString());
                    else if (_orderType == Constz.OrderType.PO.Code)
                        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInSupplier.aspx?loid=" + FlowObj.LOID.ToString());
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[indexCheckBox].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            string orderType = drow["ORDERTYPE"].ToString();
            string stockInCode = "";
            if (!Convert.IsDBNull(drow["CODE"])) { stockInCode = drow["CODE"].ToString(); }
            HyperLink lnkRequisition = (HyperLink)e.Row.Cells[indexLink].FindControl("lnkRequisition");
            lnkRequisition.Text = drow["REQUESTCODE"].ToString();
            if (orderType == Constz.OrderType.PD.Code)
            {
                lnkRequisition.NavigateUrl = ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_PRODUCTION].ToString() + "Transaction/Production.aspx?loid=" + drow["REQUESTID"].ToString();
            }
            else if (orderType == Constz.OrderType.PO.Code)
            {
                lnkRequisition.NavigateUrl = ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_PURCHASE].ToString() + "Transaction/PurchaseOrder.aspx?loid=" + drow["REQUESTID"].ToString();
            }

            ((CheckBox)e.Row.Cells[indexCheckBox].FindControl("chkItem")).Enabled = (stockInCode == "");

            if (e.Row.RowIndex > 0)
            {
                int rowSpan = 0;
                while (drow["LOID"].ToString() == this.grvRequisition.Rows[e.Row.RowIndex - rowSpan - 1].Cells[0].Text)
                {
                    rowSpan += 1;
                    if (e.Row.RowIndex - rowSpan == 0) break;
                }
                if (rowSpan > 0)
                {
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[2].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[3].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[4].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[5].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[6].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[7].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[8].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[9].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[10].RowSpan = rowSpan + 1;
                    for (int i = 0; i < 9; ++i)
                    {
                        e.Row.Cells[i + 2].CssClass = "zHidden";
                    }
                }
            }

        }
    }

}
