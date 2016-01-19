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
using ABB.Flow.Inventory;
using ABB.Data.Inventory;
using ABB.Global;
using ABB.Data;

public partial class Transaction_StockCheckSearch : System.Web.UI.Page
{
    private StockCheckFlow _flow;
    public StockCheckFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockCheckFlow(); return _flow; }
    }

    private StockCheckSearchData GetData()
    {
        StockCheckSearchData data = new StockCheckSearchData();
        data.BATCHNO = this.cmbBatchNo.Items.Count == 0 ? "xxx" : this.cmbBatchNo.SelectedItem.Text;
        data.WAREHOUSE = Convert.ToDouble(this.cmbWarehouse.SelectedItem.Value);
        data.LOCATION = Convert.ToDouble(this.cmbLocation.SelectedItem.Value);
        data.DATEFROM = this.ctlDateFrom.DateValue;
        data.DATETO = this.ctlDateTo.DateValue;
        data.BARCODE = this.txtBarcode.Text.Trim();
        data.PRODUCTNAME = this.txtProductName.Text.Trim();
        data.LOTNO = this.cmbLotNo.SelectedItem.Text;
        data.DIFFCHECK = this.chkDiff.Checked;
        return data;
    }

    private void Search()
    {
        this.grvStockCheckItem.DataSource = FlowObj.GetStockCheckItemList(GetData());
        this.grvStockCheckItem.DataBind();
    }

    private void SetNameBtnSubmit()
    {
        string batch = "";
        if (this.cmbBatchNo.SelectedValue != "") batch = this.cmbBatchNo.SelectedItem.Text;
        txtBatchNoStatus.Text = FlowObj.GetStatusByBatchNo(batch);
        this.ctlToolbar.BtnSubmitShow = true;
        if (txtBatchNoStatus.Text == Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.NameBtnSubmit = "หยุดนับ";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ต้องการหยุดนับสินค้า/วัตถุดิบ สำหรับ batch นี้ใช่หรือไม่?');";
        }
        else if (txtBatchNoStatus.Text == Constz.Requisition.Status.Approved.Code)
        {
            this.ctlToolbar.NameBtnSubmit = "เสร็จสิ้น";
            this.ctlToolbar.ClientClickSubmit = "return confirm('ต้องการยืนยันการนับสินค้า/วัตถุดิบ สำหรับ batch นี้ใช่หรือไม่?');";
        }
        else
        {
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.SetButtonText();
    }

    private void SetBatchNO()
    {
        ComboSource.BuildComboDistinct(this.cmbBatchNo, "STOCKCHECK", "BATCHNO", "", "BATCHNO DESC", "STATUS='" + Constz.Requisition.Status.Approved.Code + "' OR STATUS='" + Constz.Requisition.Status.Waiting.Code + "'");
        SetNameBtnSubmit();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            SetBatchNO();
            ComboSource.BuildCombo(this.cmbWarehouse, "WAREHOUSE", "NAME", "LOID", "NAME", "ISCOUNT = 'Y'", "ทั้งหมด", "0");
            ComboSource.BuildCombo(this.cmbLocation, "LOCATION", "NAME", "LOID", "NAME", "", "ทั้งหมด", "0");
            ComboSource.BuildComboDistinct(this.cmbLotNo, "STOCKCHECKITEM", "LOTNO", "", "LOTNO", "", "ทั้งหมด", "0");
        }
     }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/StockCheck.aspx");
    }
    protected void PrintClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/StockcheckParameter.aspx");
    }
    protected void SubmitClick(object sender, EventArgs e)
    {
        if (txtBatchNoStatus.Text == Constz.Requisition.Status.Waiting.Code)
        {
            if (!FlowObj.UpdateStockCheckStatus(this.cmbBatchNo.SelectedItem.Text, Constz.Requisition.Status.Approved.Code, Authz.CurrentUserInfo.UserID))
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
            SetNameBtnSubmit();
        }
        else if (txtBatchNoStatus.Text == Constz.Requisition.Status.Approved.Code)
        {
            ArrayList arr = new ArrayList();

            for (int i = 0; i < this.grvStockCheckItem.Rows.Count; i++)
            {
                TextBox txtImproveQty = (TextBox)this.grvStockCheckItem.Rows[i].Cells[8].FindControl("txtImproveQty");
                TextBox txtReason = (TextBox)this.grvStockCheckItem.Rows[i].Cells[9].FindControl("txtReason");
                double ImproveQty = 0;
                try
                {
                    ImproveQty = Convert.ToDouble(txtImproveQty.Text.Replace(",",""));
                }
                catch (Exception)
                {
                    Appz.ClientAlert(this, "ค่าปรับปรุงยอดลำดับที่ " + i + 1 + " ไม่ถูกต้อง");
                    return;
                }
                if ((ImproveQty != 0) || (txtReason.Text != ""))
                {
                    StockCheckImproveData idata = new StockCheckImproveData();
                    idata.STOCKCHECK = Convert.ToDouble(this.grvStockCheckItem.Rows[i].Cells[10].Text);
                    idata.PRODUCTSTOCK = Convert.ToDouble(this.grvStockCheckItem.Rows[i].Cells[12].Text);
                    idata.SYSQTY = Convert.ToDouble(this.grvStockCheckItem.Rows[i].Cells[5].Text);
                    idata.IMPROVEQTY = ImproveQty;
                    idata.REASON = txtReason.Text;
                    arr.Add(idata);
                }

            }
            if (!FlowObj.UpdateStockCheckStatus(this.cmbBatchNo.SelectedItem.Text, Constz.Requisition.Status.Finish.Code, Authz.CurrentUserInfo.UserID, arr))
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
            SetBatchNO();
        }

        Search();
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        if (this.cmbBatchNo.SelectedValue != "")
        {
            Search();
        }
        else
        {
            Appz.ClientAlert(this, "กรุณาสร้าง การตรวจนับสินค้า ใหม่");
        }
    }

    protected void cmbBatchNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetNameBtnSubmit();
    }

    protected void grvStockCheckItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            TextBox txtQty = (TextBox)e.Row.Cells[8].FindControl("txtImproveQty");
            txtQty.Text = Convert.ToDouble(e.Row.Cells[7].Text).ToString(Constz.IntFormat);
            ControlUtil.SetMinusIntTextBox(txtQty);
        }
    }
}
