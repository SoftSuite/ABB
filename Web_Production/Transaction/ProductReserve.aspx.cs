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
using ABB.Data.Admin;
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
using ABB.Global;

public partial class Transaction_ProductReserve : System.Web.UI.Page
{
    #region Variables & Properties

    private ProductReserveFlow _flow;
    private PDReserveItem item;

    public ProductReserveFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductReserveFlow(); return _flow; }
    }

    public PDReserveItem ItemObj
    {
        get { if (item == null) item = new PDReserveItem(); return item; }
    }

    #endregion

    #region Methods

    #region Others

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItem.Visible = true;
            this.grvItem.ShowFooter = false;
        }
        else
        {
            this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code);
        }

    }

    private void ResetState(double loid)
    {
        // Get Data From LOID (existing)
        if (loid != 0)
        {
            txtLOID.Text = loid.ToString();
            txtLotNo.ReadOnly = true;
            txtLotNo.CssClass = "zTextbox-View";
            btnSearch.Visible = false;
        }
        ItemObj.ClearSession();
        SetData(FlowObj.GetAllData(loid));
    }

    private void ResetState1(string code)
    {
        // Get Data From LOT (new one)

        ItemObj.ClearSession();
        SetData(FlowObj.GetPDDataFromLOT(code));
    }

    #endregion

    #region Data
    
    private void SetData(PDReserveData data)
    {
        // Setdata from Old REQUISITION
        this.txtLOID.Text = data.LOID.ToString();
        if (data.LOID == 0)
        {
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.REQDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;

            //data.VAT = Convert.ToDouble(SysConfigFlow.GetValue(Constz.ConfigName.VAT));
        }
        if (data.REQDATE.Year == 1) data.REQDATE = DateTime.Now.Date;

        this.txtStatus.Text = (data.STATUS != "" ? data.STATUS : txtStatus.Text);
        //this.txtStatus1.Text = (data.STATUS != "" ? data.STATUS : txtStatus.Text);
        this.txtStatus1.Text = (data.STATUS == Constz.Requisition.Status.SendWareHouse.Code ? Constz.Requisition.Status.SendWareHouse.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : (data.STATUS == Constz.Requisition.Status.Finish.Code ? Constz.Requisition.Status.Finish.Name : Constz.Requisition.Status.DoWaiting.Name)));
        this.txtWareHouse.Text = (data.WAREHOUSE != 0 ? data.WAREHOUSE.ToString() : txtWareHouse.Text);
        this.txtRequisitionCode.Text = data.CODE;
        if (data.LOTNO == "0")
        {
            this.txtLotNo.Text = "";
        }
        else
        {
            this.txtLotNo.Text = data.LOTNO;
        }
        this.txtDate.Text = data.REQDATE.ToString(Constz.DateFormat);
        this.txtDUEDate.Text = data.MFGDATE.ToString(Constz.DateFormat);
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtPDCode.Text = data.PDBARCODE;
        this.txtPDName.Text = data.PDNAME;
        this.txtQty.Text = Convert.ToString(data.BATCHSIZE);
        this.txtQtyUnit.Text = data.BATCHSIZEUNITNAME;
        this.txtSTDQty.Text = Convert.ToString(data.STDQTY);
        this.txtPDQtyUnit.Text = data.PDUNITNAME;
        this.txtBatchsizeUnit.Text = Convert.ToString(data.BATCHSIZEUNIT);
        this.txtPacksize.Text = Convert.ToString(data.PACKSIZE);
        this.txtPacksizeunit.Text = Convert.ToString(data.PACKSIZEUNIT);
        SetStdQty(); //คำนวณผลผลิตตามทฤษฎี
        this.txtPdpStdqty.Text = Convert.ToString(data.BATCHSIZE * Convert.ToDouble(txtPdpStdqty.Text));
        this.txtVPLOID.Text = data.VPLOID.ToString();

        SetGrvItem(this.txtStatus.Text);

        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductMaterialReserve, data.LOID) + " return false;";
    }

    private PDReserveData GetData()
    {
        PDReserveData data = new PDReserveData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ08;
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.LOTNO = this.txtLotNo.Text.Trim();
        data.PDNAME = this.txtPDName.Text  ;
        data.REQDATE = Convert.ToDateTime(this.txtDate.Text);
        data.MFGDATE = Convert.ToDateTime(this.txtDUEDate.Text);
        data.CODE = this.txtRequisitionCode.Text.Trim();
        data.PDBARCODE = this.txtPDCode.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();
        data.STATUS = this.txtStatus.Text.Trim();
        data.VPLOID = Convert.ToDouble(this.txtVPLOID.Text == "" ? "0" : this.txtVPLOID.Text);

        data.ITEM = GetItemData();
        
        return data;
    }

    private ArrayList GetItemData()
    {
        ArrayList arrItem = new ArrayList();
        for (int i = 0; i < grvItem.Rows.Count; i++)
        {
            ProductMaterialData pmData = new ProductMaterialData();
            pmData.PRODUCT = Convert.ToDouble( grvItem.Rows[i].Cells[16].Text);
            pmData.MASTER = Convert.ToDouble(((Label)grvItem.Rows[i].Cells[5].FindControl("txtQtyView")).Text);
            pmData.ACTIVE = "1";
            pmData.UNIT = Convert.ToDouble(grvItem.Rows[i].Cells[17].Text);
            arrItem.Add(pmData);

        }

        return arrItem;
    }

    private void SetStdQty()
    {
        double stdqty = 0;
        stdqty = ProductReserveFlow.ConvertUnit(txtBatchsizeUnit.Text.Trim(), txtPacksizeunit.Text.Trim(), txtQty.Text.Trim(), txtPacksize.Text.Trim());
        txtPdpStdqty.Text = stdqty.ToString();
    }

    #endregion

    #region GridView

    private void NewRowDataBound(GridViewRow gRow)
    {
        //ComboSource.BuildCombo((DropDownList)gRow.Cells[3].FindControl("cmbNewProduct"), "V_BOM_LIST", "RWNAME", "BOLOID", "RWNAME", "", "เลือก", "0");
        //ComboSource.BuildCombo((DropDownList)gRow.Cells[5].FindControl("cmbNewUnit"), "V_BOM_LIST", "UNAME", "BOLOID", "UNAME", "", "เลือก", "0");
        //ControlUtil.SetIntTextBox((TextBox)gRow.Cells[5].FindControl("txtNewQty"));
        //string script = "document.getElementById('" + ((TextBox)gRow.Cells[7].FindControl("txtNewNetPrice")).ClientID + "').value = ";
        //script += "document.getElementById('" + ((TextBox)gRow.Cells[4].FindControl("txtNewQty")).ClientID + "').value * ";
        //script += "document.getElementById('" + ((TextBox)gRow.Cells[6].FindControl("txtNewPrice")).ClientID + "').value";
        //((TextBox)gRow.Cells[4].FindControl("txtNewQty")).Attributes.Add("onchange", script);
    }

    #endregion

    #endregion

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            txtWareHouse.Text = Authz.CurrentUserInfo.Warehouse.ToString();
        }

        string script = "";
        script += "var a; a = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PopUpProductSearch.aspx' + (document.getElementById('" + this.txtLotNo.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtLotNo.ClientID + "').value)), '650', '550' ,'yes'); ";
        script += "if (a == 'undefined') { return false; }";// document.getElementById('" + this.txtLotNo.ClientID + "').value || '' == document.getElementById('" + this.txtInvoicecode.ClientID + "').value) ";
        script += " else { document.getElementById('" + this.txtLotNo.ClientID + "').value = a;  }";
        //script += "{ return false; } ";

        this.btnSearch.OnClientClick = script;
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ResetState1(this.txtLotNo.Text);
        //txtAddress.Text = "TEST!!!";
    }


    #region grvItem


    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;

            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[5].FindControl("txtQty"));
            }
           
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            NewRowDataBound(e.Row);
        }
    }


    protected void grvItem_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {
        e.ExceptionHandled = (e.Exception != null);
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.InnerException.Message);
        }
        else
        {
            //Calculation();
        }
    }

    protected void grvItem_RowDeleted(object sender, GridViewDeletedEventArgs e)
    {
        if (e.ExceptionHandled)
        {
            Appz.ClientAlert(this, e.Exception.Message);
        }
        else
        {
            SetGrvItem(this.txtStatus.Text);
            //Calculation();
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[5].FindControl("txtQty");

        e.NewValues["MASTER"] = txtQty.Text;
    }

    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ProductReserveSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
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
        PDReserveData data = GetData();
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ยืนยันการส่งคลัง");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    #endregion
}
