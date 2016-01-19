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

public partial class Transaction_ReturnRequest : System.Web.UI.Page
{
    #region Variables & Properties

    private ReturnRequestFlow _flow;
    private ReturnRequestItem item;

    public ReturnRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnRequestFlow(); return _flow; }
    }

    public ReturnRequestItem ItemObj
    {
        get { if (item == null) item = new ReturnRequestItem(); return item; }
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

        //ItemObj.ClearSession();
        //SetData(FlowObj.GetAllData(loid));

        if (loid != 0)
        {
            txtLOID.Text = loid.ToString();
            txtLotNo.ReadOnly = true;
            txtLotNo.CssClass = "zTextbox-View";
            btnSearch.Visible = false;
            //this.txtRefLoid = data.REFLOID;
            //SetDataPD(FlowObj.GetRefData(data.REFLOID));
        }
        ItemObj.ClearSession();
        SetData(FlowObj.GetAllData(loid));
    }

    private void ResetState1(double loid)
    {
        // Get Data From LOT (new one)

        ItemObj.ClearSession();
        SetData(FlowObj.GetRefData(loid));
    }

    #endregion

    #region Data
    
    private void SetData(ReturnRequestData data)
    {
        // Setdata from Old REQUISITION
        if (data.REQDATE.Year == 1) data.REQDATE = DateTime.Now.Date;

        this.txtLOID.Text = data.LOID.ToString(); //######
        this.txtStatus.Text = (data.STATUS != "" ? data.STATUS: txtStatus.Text);
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : (data.STATUS == Constz.Requisition.Status.QC.Code ? Constz.Requisition.Status.QC.Name : (data.STATUS == Constz.Requisition.Status.Finish.Code ? Constz.Requisition.Status.Finish.Name : Constz.Requisition.Status.Waiting.Name))));
        this.txtWareHouse.Text = (data.WAREHOUSE != 0 ? data.WAREHOUSE.ToString() : txtWareHouse.Text);
        this.txtRequisitionCode.Text = data.CODE;
        this.txtLotNo.Text = data.LOTNO;
        this.ctlReqDate.DateValue = data.REQDATE;
        this.txtRemark.Text = data.REMARK;
        //this.txtCreateBy.Text = data.CREATEBY;
        this.txtPDCode.Text = data.PDBARCODE;
        this.txtPDName.Text = data.PDNAME;
        if (this.txtLOID.Text != "0")
        {
            this.txtRefLoid.Text = data.REFLOID.ToString();
            SetDataPD(FlowObj.GetRefData(data.REFLOID));
        }


        //this.txtQty.Text = Convert.ToString(data.BATCHSIZE);
        //this.txtQtyUnit.Text = data.BATCHSIZEUNITNAME;
        //this.txtVPLOID.Text = data.VPLOID.ToString();

        SetGrvItem(this.txtStatus.Text);

        if (data.STATUS == Constz.Requisition.Status.Approved.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnSubmitShow = false;
        }
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ReturnRequestPD, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + "return false;";
    }

    private void SetDataPD(ReturnRequestData data)
    {
        this.txtPDCode.Text = data.PDBARCODE;
        this.txtPDName.Text = data.PDNAME;
    }

    private ReturnRequestData GetData()
    {
        ReturnRequestData data = new ReturnRequestData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ14;
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.WAREHOUSE = Convert.ToDouble(this.txtWareHouse.Text == "" ? "0" : this.txtWareHouse.Text);
        data.LOTNO = this.txtLotNo.Text.Trim();
        data.PDNAME = this.txtPDName.Text  ;
        data.PDBARCODE = this.txtPDCode.Text.Trim();
        data.REMARK = this.txtRemark.Text.Trim();data.STATUS = this.txtStatus.Text.Trim();
        data.REFTABLE = "PDPRODUCT";
        data.REFLOID = Convert.ToDouble(this.txtRefLoid.Text == "" ? "0" : this.txtRefLoid.Text);
        data.ITEM = ItemObj.GetItemList();
        data.REQDATE = ctlReqDate.DateValue;
        data.STATUS = txtStatus.Text.Trim();
        data.CODE = txtRequisitionCode.Text.Trim();
        
        return data;
    }

    private ArrayList GetItemData()
    {
        ArrayList arrItem = new ArrayList();
        for (int i = 0; i < grvItem.Rows.Count; i++)
        {
            ProductMaterialData pmData = new ProductMaterialData();
            pmData.PRODUCT = Convert.ToDouble( grvItem.Rows[i].Cells[14].Text);
            pmData.MASTER = Convert.ToDouble(((Label)grvItem.Rows[i].Cells[5].FindControl("txtQtyView")).Text);
            pmData.ACTIVE = "1";
            pmData.UNIT = Convert.ToDouble(grvItem.Rows[i].Cells[15].Text);
            arrItem.Add(pmData);

        }

        return arrItem;
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
           // this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ReturnRequest, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
            txtWareHouse.Text = Authz.CurrentUserInfo.Warehouse.ToString();
            txtCreateBy.Text = Authz.CurrentUserInfo.UserID;
        

        string script = "";
        script += "document.getElementById('" + this.txtRefLoid.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Transaction/PopUpRequestSearch.aspx' + (document.getElementById('" + this.txtLotNo.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtLotNo.ClientID + "').value)), '600', '550');";
        script += "if ('undefined' ==  document.getElementById('" + this.txtRefLoid.ClientID + "').value || '' == document.getElementById('" + this.txtRefLoid.ClientID + "').value) ";
        script += "{ return false; } ";

        this.btnSearch.OnClientClick = script;
        this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันการอนุมัติใช่หรือไม่?');";

        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        ResetState1(Convert.ToDouble(this.txtRefLoid.Text.Trim()));
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
        if (e.ExceptionHandled)
        {
            e.KeepInEditMode = true;
            Appz.ClientAlert(this, e.Exception.Message);
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
        TextBox txtQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtQty");

        e.NewValues["RETURNQTY"] = txtQty.Text;
    }

    #endregion

    #region Toolbar

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Transaction/ReturnRequestSearch.aspx");
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
        ReturnRequestData data = GetData();
        data.STATUS = Constz.Requisition.Status.Approved.Code;
        if (FlowObj.CommitData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "อนุมัติข้อมูลเรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    #endregion

    #endregion
}
