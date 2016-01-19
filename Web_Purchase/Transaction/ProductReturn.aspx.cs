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
using ABB.Data.Purchase;
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Flow.Purchase;
using ABB.Global;

public partial class Transaction_ProductReturn : System.Web.UI.Page
{
    private PDReturnFlow _flow;
    private StockoutFlow _flow2;
    public PDReturnFlow FlowObj
    {
        get { if (_flow == null) _flow = new PDReturnFlow(); return _flow; }
    }
    public StockoutFlow FlowObj2
    {
        get { if (_flow2 == null) _flow2 = new StockoutFlow(); return _flow2; }
    }

    private void ResetState(double loid)
    {
        ProductReturnData data = FlowObj.GetData(loid);
        if (loid == 0)
        {
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.CODE = "";
            data.PDRETURNDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
        }
        SetData(data);
    }

    private void ResetState2(double loid)
    {
        SetData2(FlowObj2.GetData(loid));
    }

    private void SetData(ProductReturnData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.ToString();
        this.txtSTCode.Text = FlowObj.GetSTCode(data.REFLOID);
        this.txtStatus.Text = data.STATUS;
        this.txtSTLoid.Text = data.REFLOID.ToString();
        this.txtSupplier.Text = data.SUPPLIER.ToString();
        this.txtRemark.Text = data.REMARK;
        this.txtReason.Text = data.REASON;
        this.txtName.Text = data.CNAME;
        this.txtAddress.Text = data.CADDRESS;
        this.txtTel.Text = data.CTEL;
        this.txtFax.Text = data.CFAX;
        this.ctlPDReturnDate.DateValue = data.PDRETURNDATE;
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : Constz.Requisition.Status.Waiting.Name));
        this.ctlSTDate.DateValue = FlowObj.GetSTDate(data.REFLOID);
        ABB.Data.Purchase.SupplierData sData = FlowObj.GetSupplierData(data.SUPPLIER);
        this.txtSupplierName.Text = sData.SUPPLIERNAME;
        this.grvPDReturn.DataSource = FlowObj.GetPDReturnItemList(data.LOID);
        this.grvPDReturn.DataBind();

        if (data.STATUS == Constz.Requisition.Status.Approved.Code || data.STATUS == Constz.Requisition.Status.Void.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
            this.btnSearch.Visible = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductReturn, data.LOID) + " return false;";
    }

    private void SetData2(StockoutFGData data)
    {
        this.txtSTCode.Text = data.CODE.ToString();
        this.txtSTLoid.Text = data.LOID.ToString();
        this.txtSupplier.Text = data.RECEIVER.ToString();
        this.txtRemark.Text = data.REMARK;
        this.txtReason.Text = data.REASON;
        this.ctlSTDate.DateValue = data.CREATEON;
        ABB.Data.Purchase.SupplierData sData = FlowObj.GetSupplierData(data.RECEIVER);
        this.txtSupplierName.Text = sData.SUPPLIERNAME;
        this.txtName.Text = (sData.CNAME + " " + sData.CLASTNAME).Trim();
        this.txtAddress.Text = sData.CADDRESS;
        this.txtTel.Text = sData.CTEL;
        this.txtFax.Text = sData.CFAX;
        this.grvPDReturn.DataSource = FlowObj.GetStockoutItemList(data.LOID);
        this.grvPDReturn.DataBind();
    }

    private ProductReturnData GetData()
    {
        ProductReturnData data = new ProductReturnData();
        data.LOID = Convert.ToDouble(txtLOID.Text.Trim());
        data.REFLOID = Convert.ToDouble(this.txtSTLoid.Text.Trim());
        data.CODE = this.txtCode.Text.Trim();
        data.STATUS = this.txtStatus.Text.Trim();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.REFLOID = Convert.ToDouble(this.txtSTLoid.Text.Trim());
        data.REFTABLE = "STOCKOUT";
        data.CNAME = this.txtName.Text.Trim();
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.SUPPLIER = Convert.ToDouble(this.txtSupplier.Text.Trim());
        data.REMARK = this.txtRemark.Text.Trim();
        data.REASON = this.txtReason.Text.Trim();
        data.PDRETURNDATE = this.ctlPDReturnDate.DateValue;
        data.ITEM = FlowObj.GetItemList(data.REFLOID);
        data.TYPE = "1";

        return data;
    }
    private ProductReturnData GetRecentData()
    {
        ProductReturnData data = new ProductReturnData();
        data.LOID = Convert.ToDouble(txtLOID.Text.Trim());
        data.REFLOID = Convert.ToDouble(this.txtSTLoid.Text.Trim());
        data.CODE = this.txtCode.Text.Trim();
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.REFLOID = Convert.ToDouble(this.txtSTLoid.Text.Trim());
        data.REFTABLE = "STOCKOUT";
        data.CNAME = this.txtName.Text.Trim();
        data.CADDRESS = this.txtAddress.Text.Trim();
        data.CTEL = this.txtTel.Text.Trim();
        data.CFAX = this.txtFax.Text.Trim();
        data.SUPPLIER = Convert.ToDouble(this.txtSupplier.Text.Trim());
        data.REMARK = this.txtRemark.Text.Trim();
        data.REASON = this.txtReason.Text.Trim();
        data.PDRETURNDATE = this.ctlPDReturnDate.DateValue;
        data.ITEM = FlowObj.GetItemList(data.REFLOID);
        data.TYPE = "1";

        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
           ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }

        string scriptRefNo = "";
        scriptRefNo += "document.getElementById('" + this.txtSTLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/PopupStockoutSearch.aspx' + (document.getElementById('" + this.txtSTCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtSTCode.ClientID + "').value)), '600', '550');";
        scriptRefNo += "if ('undefined' ==  document.getElementById('" + this.txtSTLoid.ClientID + "').value || '' == document.getElementById('" + this.txtSTLoid.ClientID + "').value) ";
        scriptRefNo += "{ return false; } ";

        this.btnSearch.OnClientClick = scriptRefNo;
        this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการส่งให้จัดซื้อ?');";

    }


    protected void SaveClick(object sender, EventArgs e)
    {
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, GetRecentData()))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ส่งให้จัดซื้อเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReturnSearch.aspx");
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
          ResetState2(Convert.ToDouble(this.txtSTLoid.Text));

    }



}



