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
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Purchase;
using ABB.Global;

public partial class ToDoList_Controls_ProductPurchaseList : System.Web.UI.UserControl
{
    private int indexLOID = 0;
    private int indexPRLOID = 1;
    private int indexCheckBox = 2;
    private int indexPRCODE = 3;
    private int indexREQUESTDATE = 4;
    private int indexPRODUCTNAME = 5;
    private int indexQTY = 7;
    private int indexUNITNAME = 8;
    private int indexOLDPRICE = 9;
    private int indexCURPRICE = 10;
    private int indexOrderTypeName = 11;
    private int indexMINPRICE = 12;
    private int indexSTATUSNAME = 13;
    private int indexPRODUCT = 14;
    private int indexUNIT = 15;
    private int indexSTATUS = 16;
    private int indexDUEDATE = 17;

    private PurchaseToDoListFlow _flow;
    public PurchaseToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new PurchaseToDoListFlow(); } return _flow; }
    }

    private ProductPurchaseListSearchData GetSearchData()
    {
        ProductPurchaseListSearchData data = new ProductPurchaseListSearchData();
        data.CODE = this.txtCode.Text.Trim();
        data.DATEFROM = this.dtpDateFrom.DateValue;
        data.DATETO = this.dtpDateTo.DateValue;
        data.PURCHASETYPE = Convert.ToDouble(this.cmbPurchaseType.SelectedItem.Value);
        data.PRODUCT = Convert.ToDouble(this.cmbProduct.SelectedItem.Value);
        return data;
    }

    private void SearchData()
    {
        this.grvProductPurchase.DataSource = FlowObj.GetProductPurchaseList(GetSearchData());
        this.grvProductPurchase.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvProductPurchase.ClientID + "_ctl', '_chkItem')"; }
    }

    private PurchaseOrderData GetData()
    {
        PurchaseOrderData data = new PurchaseOrderData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CODE = "";
        data.ORDERDATE = DateTime.Now.Date;
        data.STATUS = Constz.Requisition.Status.Waiting.Code;
        data.VAT = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.VAT));

        for (int i = 0; i < this.grvProductPurchase.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvProductPurchase.Rows[i].Cells[indexCheckBox].FindControl("chkItem");
            if (chk.Checked && chk.Enabled && this.grvProductPurchase.Rows[i].Cells[indexCheckBox].CssClass != "zHidden")
            {
                POItemData itemData = new POItemData();
                itemData.PRODUCT = Convert.ToDouble(this.grvProductPurchase.Rows[i].Cells[indexPRODUCT].Text);
                itemData.PRITEM = Convert.ToDouble(this.grvProductPurchase.Rows[i].Cells[indexLOID].Text);
                itemData.QTY = Convert.ToDouble(this.grvProductPurchase.Rows[i].Cells[indexQTY].Text);
                itemData.UNIT = Convert.ToDouble(this.grvProductPurchase.Rows[i].Cells[indexUNIT].Text);
                itemData.PRICE = Convert.ToDouble(this.grvProductPurchase.Rows[i].Cells[indexCURPRICE].Text);
                itemData.DUEDATE = Convert.ToDateTime(this.grvProductPurchase.Rows[i].Cells[indexDUEDATE].Text);
                itemData.ACTIVE = Constz.ActiveStatus.Active;

                data.ITEM.Add(itemData);
            }
        }
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ComboSource.BuildCombo(this.cmbPurchaseType, "PURCHASETYPE", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
            ComboSource.BuildCombo(this.cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ", "เลือก", "0");
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        try
        {
            PurchaseOrderData data = GetData();
            if (data.ITEM.Count == 0)
                throw new ApplicationException("กรุณาเลือกรายการสินค้า");
            else
            {
                if (FlowObj.NewPDOrder(Authz.CurrentUserInfo.UserID, data))
                {
                    Response.Redirect(Constz.HomeFolder + "Transaction/PurchaseOrder.aspx?loid=" + FlowObj.LOID.ToString());
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

    protected void grvProductPurchase_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[indexCheckBox].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[18].Text == "Y")
                e.Row.Cells[3].Text = "<font color=red>!</font>";
        }
    }
}
