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
using ABB.Data.Inventory.FG;
using ABB.Flow;
using ABB.Flow.Inventory.FG;
using ABB.Global;

public partial class WH_Transaction_StockInSupplier : System.Web.UI.Page
{
    private StockInFlow _flow;
    private StockInItem item;

    public StockInFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockInFlow(); return _flow; }
    }

    public StockInItem ItemObj
    {
        get { if (item == null) item = new StockInItem(); return item; }
    }

    private void SetGrvItem(string status)
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.ShowFooter = (status == Constz.Requisition.Status.Waiting.Code);
            this.grvItem.Columns[0].Visible = (status == Constz.Requisition.Status.Waiting.Code || status == Constz.Requisition.Status.Approved.Code);
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = (status != Constz.Requisition.Status.Waiting.Code);
            this.grvItemNew.Visible = (status == Constz.Requisition.Status.Waiting.Code);
        }
    }


    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        StockInFGData data = FlowObj.GetData(loid);
        SetData(data);
    }

    private void SetData(StockInFGData data)
    {
        if (data.LOID == 0)
        {
            data.ACTIVE = Constz.ActiveStatus.Active;
            data.RECEIVEDATE = DateTime.Now.Date;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.CREATEBY = Authz.CurrentUserInfo.UserID;
        }
        this.txtCode.Text = data.CODE;
        this.txtLOID.Text = data.LOID.ToString();
        this.txtInvNo.Text = data.INVNO;
        this.txtStatus.Text = data.STATUS;
        this.txtSender.Text = data.SENDER.ToString();
        this.txtSenderCode.Text = "";
        this.txtSenderName.Text = "";
        this.txtRemark.Text = data.REMARK;
        this.txtCreateBy.Text = data.CREATEBY;
        this.txtStockInCode.Text = data.CODE;
        this.txtQCCode.Text = data.QCCODE;
        this.ctlReceiveDate.DateValue = data.RECEIVEDATE;
        this.txtStatusName.Text = (data.STATUS == Constz.Requisition.Status.Approved.Code ? Constz.Requisition.Status.Approved.Name : (data.STATUS == Constz.Requisition.Status.Void.Code ? Constz.Requisition.Status.Void.Name : (data.STATUS == Constz.Requisition.Status.QC.Code ? Constz.Requisition.Status.QC.Name : (data.STATUS == Constz.Requisition.Status.Finish.Code ? Constz.Requisition.Status.Finish.Name : Constz.Requisition.Status.Waiting.Name))));
        SetSender(data.SENDER);
        SetToolbar(data.STATUS);
        SetGrvItem(data.STATUS);

        if (data.STATUS != Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar.BtnSaveShow = false;
            this.ctlToolbar.BtnCancelShow = false;
            this.btnSearch.Visible = false;
            this.txtSenderCode.CssClass = "zTextbox-View";
            this.txtInvNo.CssClass = "zTextbox-View";
            this.txtSenderCode.ReadOnly = true;
            this.txtInvNo.ReadOnly = true;

            if (data.STATUS == Constz.Requisition.Status.QC.Code || data.STATUS == Constz.Requisition.Status.Finish.Code)
            {
                this.ctlToolbar.BtnSubmitShow = false;
            }
        }
        this.ctlToolbar2.BtnSubmitShow = (data.STATUS == Constz.Requisition.Status.Approved.Code);
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ProductQC, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";
        this.ctlToolbar2.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.StockInSupplier, Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text)) + " return false;";

    }

    private StockInFGData GetData()
    {
        StockInFGData data = new StockInFGData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.CODE = this.txtCode.Text;
        data.SENDER = Convert.ToDouble(this.txtSender.Text == "" ? "0" : this.txtSender.Text);
        data.ITEM = ItemObj.GetItemList();
        data.REMARK = this.txtRemark.Text.Trim();
        data.STATUS = this.txtStatus.Text.Trim();
        data.RECEIVEDATE = this.ctlReceiveDate.DateValue;
        data.INVNO = this.txtInvNo.Text.Trim();
        data.QCCODE = this.txtQCCode.Text.Trim();
        data.DOCTYPE = Constz.DocType.RecRaw.LOID;
        data.RECEIVER = Authz.CurrentUserInfo.Warehouse;

        return data;
    }

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));

            string script = "";
            script += "document.getElementById('" + this.txtSender.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/SupplierSearch.aspx' + (document.getElementById('" + this.txtSenderCode.ClientID + "').value == '' ? '' : '?code=' + escape(document.getElementById('" + this.txtSenderCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + this.txtSender.ClientID + "').value || '' == document.getElementById('" + this.txtSender.ClientID + "').value) ";
            script += "{ return false; } ";

            this.btnSearch.OnClientClick = script;
            this.ctlToolbar.ClientClickSubmit = "return confirm('ยืนยันส่งตรวจ QC ใช่หรือไม่?');";
            this.ctlToolbar2.ClientClickSubmit = "return confirm('ยืนยันรับเข้าคลังใช่หรือไม่?');";

        }
    }

    private void SetToolbar(string status)
    {
        if (status == Constz.Requisition.Status.Waiting.Code)
        {
            this.ctlToolbar2.BtnSubmitShow = false;
        }
        else if (status == Constz.Requisition.Status.Approved.Code)
        {
            this.ctlToolbar.BtnSubmitShow = false;
        }
        else if (status == Constz.Requisition.Status.QC.Code || status == Constz.Requisition.Status.Finish.Code)
        {
            this.ctlToolbar.BtnSubmitShow = false;
            this.ctlToolbar2.BtnSubmitShow = false;
        }

    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SetSender(Convert.ToDouble(this.txtSender.Text));
    }

    #region grvItem "Insert"

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Int16 rowIndex = 0;
            DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
            TextBox txtSQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewSQty");
            TextBox txtPQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPQty");
            TextBox txtLotNo = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewLotNo");
            TextBox txtRemark = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[12].FindControl("txtNewRemark");
            DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("cmbNewUnit");
            TextBox txtNewQCQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewQCQty");
            TextBox txtRefLoid = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtRefLoid");
            TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtPrice");
            StockInItemData data = new StockInItemData();
            data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.QTY = Convert.ToDouble(txtSQty.Text == "" ? "0" : txtSQty.Text);
            data.PQTY = Convert.ToDouble(txtPQty.Text == "" ? "0" : txtPQty.Text);
            data.LOTNO = txtLotNo.Text.Trim();
            data.REMARK = txtRemark.Text.Trim();
            data.QCQTY = Convert.ToDouble(txtNewQCQty.Text == "" ? "0" : txtNewQCQty.Text);
            data.REFLOID = Convert.ToDouble(txtRefLoid.Text == "" ? "0" : txtRefLoid.Text);
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            if (ItemObj.InsertStokInItem(data))
            {
                this.grvItem.DataBind();
                this.grvItemNew.DataBind();
                SetGrvItem(this.txtStatus.Text);
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), "V_RAW_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[9].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[7].FindControl("txtNewSQty"));
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[8].FindControl("txtNewQCQty"));


            ImageButton btnNewSearch = (ImageButton)e.Row.Cells[2].FindControl("btnNewSearch");
            TextBox txtNewBarCode = (TextBox)e.Row.Cells[2].FindControl("txtNewBarCode");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            TextBox txtRefLoid = (TextBox)e.Row.Cells[2].FindControl("txtRefLoid");
            TextBox txtPrice = (TextBox)e.Row.Cells[2].FindControl("txtPrice");
            DropDownList cmbNewProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct");
            TextBox txtNewPOCode = (TextBox)e.Row.Cells[5].FindControl("txtNewPOCode");//
            TextBox txtNewPQty = (TextBox)e.Row.Cells[6].FindControl("txtNewPQty");
            TextBox txtNewQCQty = (TextBox)e.Row.Cells[8].FindControl("txtNewQCQty");
            DropDownList cmbNewUnit = (DropDownList)e.Row.Cells[9].FindControl("cmbNewUnit");
            string script = "";
            script += "if(document.getElementById('" + txtSender.ClientID + "').value=='0' || document.getElementById('" + txtSender.ClientID + "').value=='undefined') { ";
            script += "alert('กรุณาเลือกผู้จำหน่ายก่อน'); } else{ ";
            script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/ProductWHSearch.aspx?sender=' + document.getElementById('" + txtSender.ClientID + "').value + (document.getElementById('" + txtNewBarCode.ClientID + "').value == '' ? '' : '&code=' + escape(document.getElementById('" + txtNewBarCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + txtGetData.ClientID + "').value || '' == document.getElementById('" + txtGetData.ClientID + "').value) ";
            script += "{ return false; } ";
            /*            script += "else{ ";
                        script += "var sData = document.getElementById('" + txtGetData.ClientID + "').value.split('|'); ";
                        script += "document.getElementById('" + cmbNewProduct.ClientID + "').value = sData[0];";
                        script += "document.getElementById('" + txtNewBarCode.ClientID + "').value = sData[1];";
                        script += "document.getElementById('" + txtNewPOCode.ClientID + "').value = sData[2];";
                        script += "document.getElementById('" + txtNewPQty.ClientID + "').value = sData[3];";
                        script += "document.getElementById('" + txtNewQCQty.ClientID + "').value = sData[4];";
                        script += "document.getElementById('" + cmbNewUnit.ClientID + "').value = sData[5];";
                        script += "document.getElementById('" + txtRefLoid.ClientID + "').value = sData[6];";
                        script += "document.getElementById('" + txtPrice.ClientID + "').value = sData[7];"; 
                        script += "}"; */
            script += "}";
            btnNewSearch.OnClientClick = script;
        }
    }

    protected void btnNewSearch_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        Int16 rowIndex = (Int16)((GridViewRow)btn.Parent.Parent).RowIndex;

        TextBox txtNewBarCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        TextBox txtGetData = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtGetData");
        TextBox txtRefLoid = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtRefLoid");
        TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtPrice");
        DropDownList cmbNewProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
        TextBox txtNewPOCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("txtNewPOCode");//
        TextBox txtNewPQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPQty");
        TextBox txtNewQCQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[8].FindControl("txtNewQCQty");
        DropDownList cmbNewUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[9].FindControl("cmbNewUnit");
        TextBox txtNewSQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewSQty");


        string[] sData = txtGetData.Text.Split('|');
        if (sData.Length == 8)
        {
            cmbNewProduct.SelectedValue = sData[0];
            txtNewBarCode.Text = sData[1];
            txtNewPOCode.Text = sData[2];
            txtNewPQty.Text = sData[3];
            txtNewSQty.Text = sData[4];
            cmbNewUnit.SelectedValue = sData[5];
            txtRefLoid.Text = sData[6];
            txtPrice.Text = sData[7];
            // txtNewSQty.Text = sData[8];
        }
    }

    protected void txtNewBarCode_TextChanged(object sender, EventArgs e)
    {
        //TextBox txt = (TextBox)sender;
        //Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        //DropDownList cmbProduct = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("cmbNewProduct");
        //TextBox txtSQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewSQty");
        //DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("cmbNewUnit");
        //TextBox txtPQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewSQty");


        //POItemData data = FlowObj.GetPOItemData(txt.Text.Trim());

        //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        //txtPQty.Text = data.QTY.ToString();

    }

    protected void cmbNewProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList cmb = (DropDownList)sender;
        //Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        //TextBox txtCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewBarCode");
        //TextBox txtQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewQty");
        //DropDownList cmbUnit = (DropDownList)this.grvItemNew.Rows[rowIndex].Cells[5].FindControl("cmbNewUnit");
        //TextBox txtPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[6].FindControl("txtNewPrice");
        //TextBox txtNetPrice = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[7].FindControl("txtNewNetPrice");

        //ProductSearchData data = FlowObj.GetProductData(Convert.ToDouble(cmb.SelectedItem.Value));

        //txtCode.Text = data.BARCODE;
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        //txtPrice.Text = data.PRICE.ToString();
        //txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
    }

    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");
            TextBox txtSQty = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewSQty");
            TextBox txtPQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewPQty");
            TextBox txtLotNo = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewLotNo");
            TextBox txtRemark = (TextBox)this.grvItem.FooterRow.Cells[12].FindControl("txtNewRemark");

            DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[9].FindControl("cmbNewUnit");

            TextBox txtNewQCQty = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewQCQty");
            TextBox txtRefLoid = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtRefLoid");
            TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtPrice");
            StockInItemData data = new StockInItemData();
            data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
            data.QTY = Convert.ToDouble(txtSQty.Text == "" ? "0" : txtSQty.Text);
            data.LOTNO = txtLotNo.Text.Trim();
            data.REMARK = txtRemark.Text.Trim();
            data.QCQTY = Convert.ToDouble(txtNewQCQty.Text == "" ? "0" : txtNewQCQty.Text);
            data.REFLOID = Convert.ToDouble(txtRefLoid.Text == "" ? "0" : txtRefLoid.Text);
            data.PRICE = Convert.ToDouble(txtPrice.Text == "" ? "0" : txtPrice.Text);
            data.PQTY = Convert.ToDouble(txtPQty.Text == "" ? "0" : txtPQty.Text);
            if (ItemObj.InsertStokInItem(data))
            {
                SetGrvItem(this.txtStatus.Text);

            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[1].Text = (e.Row.RowIndex +1).ToString();

            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProduct");
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[9].FindControl("cmbUnit");

                if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
                    ComboSource.BuildCombo(cmbProduct, "V_RAW_LIST", "NAME", "LOID", "NAME", "");
                else
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");

                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[7].FindControl("txtSQty"));
                ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[8].FindControl("txtQCQty"));

                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));

                TextBox txtQCQty = (TextBox)e.Row.Cells[8].FindControl("txtQCQty");
                TextBox txtSQty = (TextBox)e.Row.Cells[7].FindControl("txtSQty");
                TextBox txtLotNo = (TextBox)e.Row.Cells[4].FindControl("txtLotNo");
                TextBox txtRemark = (TextBox)e.Row.Cells[12].FindControl("txtRemark");

                if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code || this.txtStatus.Text == Constz.Requisition.Status.QC.Code)
                {
                    txtQCQty.CssClass = "zTextboxR";

                }
                else
                {
                    txtQCQty.CssClass = "zTextboxR-View";
                    txtQCQty.ReadOnly = true;
                    txtLotNo.CssClass = "zTextboxR-View";
                    txtLotNo.ReadOnly = true;
                }
            }
            else if (e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate)
            {
                ImageButton imbDelete = (ImageButton)e.Row.Cells[0].FindControl("imbDelete");
                imbDelete.Attributes.Add("onclick", "return confirm('ยืนยันการลบสินค้า?');");
                DropDownList cmbProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbProductView");
                DropDownList cmbUnit = (DropDownList)e.Row.Cells[9].FindControl("cmbUnitView");

                if (this.txtStatus.Text == Constz.Requisition.Status.Waiting.Code)
                    ComboSource.BuildCombo(cmbProduct, "V_RAW_LIST", "NAME", "LOID", "NAME", "");
                else
                    ComboSource.BuildCombo(cmbProduct, "PRODUCT", "NAME", "LOID", "NAME", "");

                ComboSource.BuildCombo(cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
                DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
                cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(drow["PRODUCT"].ToString()));
                cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(drow["UNIT"].ToString()));
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct"), "V_RAW_LIST", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ComboSource.BuildCombo((DropDownList)e.Row.Cells[9].FindControl("cmbNewUnit"), "UNIT", "NAME", "LOID", "NAME", "", "เลือก", "0");
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[7].FindControl("txtNewSQty"));
            ControlUtil.SetDblTextBox6((TextBox)e.Row.Cells[8].FindControl("txtNewQCQty"));

            ImageButton btnNewSearch = (ImageButton)e.Row.Cells[2].FindControl("btnNewSearch");
            TextBox txtNewBarCode = (TextBox)e.Row.Cells[2].FindControl("txtNewBarCode");
            TextBox txtGetData = (TextBox)e.Row.Cells[2].FindControl("txtGetData");
            TextBox txtRefLoid = (TextBox)e.Row.Cells[2].FindControl("txtRefLoid");
            TextBox txtPrice = (TextBox)e.Row.Cells[2].FindControl("txtPrice");
            DropDownList cmbNewProduct = (DropDownList)e.Row.Cells[3].FindControl("cmbNewProduct");
            TextBox txtNewPOCode = (TextBox)e.Row.Cells[5].FindControl("txtNewPOCode");//
            TextBox txtNewPQty = (TextBox)e.Row.Cells[6].FindControl("txtNewPQty");
            TextBox txtNewQCQty = (TextBox)e.Row.Cells[8].FindControl("txtNewQCQty");
            DropDownList cmbNewUnit = (DropDownList)e.Row.Cells[9].FindControl("cmbNewUnit");
            string script = "";
            script += "if(document.getElementById('" + txtSender.ClientID + "').value=='0' || document.getElementById('" + txtSender.ClientID + "').value=='undefined') { ";
            script += "alert('กรุณาเลือกผู้จำหน่ายก่อน'); } else{ ";
            script += "document.getElementById('" + txtGetData.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/ProductWHSearch.aspx?sender=' + document.getElementById('" + txtSender.ClientID + "').value + (document.getElementById('" + txtNewBarCode.ClientID + "').value == '' ? '' : '&code=' + escape(document.getElementById('" + txtNewBarCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + txtGetData.ClientID + "').value || '' == document.getElementById('" + txtGetData.ClientID + "').value) ";
            script += "{ return false; } ";
            /*           script += "else{ ";
                       script += "var sData = document.getElementById('" + txtGetData.ClientID + "').value.split('|'); ";
                       script += "document.getElementById('" + cmbNewProduct.ClientID + "').value = sData[0];";
                       script += "document.getElementById('" + txtNewBarCode.ClientID + "').value = sData[1];";
                       script += "document.getElementById('" + txtNewPOCode.ClientID + "').value = sData[2];";
                       script += "document.getElementById('" + txtNewPQty.ClientID + "').value = sData[3];";
                       script += "document.getElementById('" + txtNewQCQty.ClientID + "').value = sData[4];";
                       script += "document.getElementById('" + cmbNewUnit.ClientID + "').value = sData[5];";
                       script += "document.getElementById('" + txtRefLoid.ClientID + "').value = sData[6];";
                       script += "document.getElementById('" + txtPrice.ClientID + "').value = sData[7];";
                       script += "}"; */
            script += "}";

            btnNewSearch.OnClientClick = script;

        }
    }

    protected void txtBarCode_TextChanged(object sender, EventArgs e)
    {
        //TextBox txt = (TextBox)sender;
        //Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        //this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        //DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[rowIndex].Cells[3].FindControl("cmbProduct");
        //TextBox txtSQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtSQty");
        //TextBox txtPQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtPQty");
        //DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowIndex].Cells[5].FindControl("cmbUnit");


        //POItemData data = FlowObj.GetPOItemData(txt.Text.Trim());

        //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        //txtPQty.Text = data.QTY.ToString();

    }

    protected void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DropDownList cmb = (DropDownList)sender;
        //Int16 rowIndex = (Int16)((GridViewRow)cmb.Parent.Parent).RowIndex;
        //this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        //TextBox txtCode = (TextBox)this.grvItem.Rows[rowIndex].Cells[2].FindControl("txtBarCode");
        //TextBox txtQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtQty");
        //DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[rowIndex].Cells[5].FindControl("cmbUnit");
        //TextBox txtPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[6].FindControl("txtPrice");
        //TextBox txtNetPrice = (TextBox)this.grvItem.Rows[rowIndex].Cells[7].FindControl("txtNetPrice");

        //ProductSearchData data = FlowObj.GetProductData(Convert.ToDouble(cmb.SelectedItem.Value));

        //txtCode.Text = data.BARCODE;
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        //txtPrice.Text = data.PRICE.ToString();
        //txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
    }

    protected void txtNewBarCode_TextChanged1(object sender, EventArgs e)
    {
        //TextBox txt = (TextBox)sender;
        //DropDownList cmbProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");
        //TextBox txtSQty = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewQty");
        //DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[5].FindControl("cmbNewUnit");
        //TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewPrice");
        //TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewNetPrice");

        //POItemData data = FlowObj.GetPOItemData(txt.Text.Trim());

        //cmbProduct.SelectedIndex = cmbProduct.Items.IndexOf(cmbProduct.Items.FindByValue(data.LOID.ToString()));
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));

    }

    protected void btnNewSearchItem_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton btn = (ImageButton)sender;
        TextBox txtNewBarCode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewBarCode");
        TextBox txtGetData = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtGetData");
        TextBox txtRefLoid = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtRefLoid");
        TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtPrice");
        DropDownList cmbNewProduct = (DropDownList)this.grvItem.FooterRow.Cells[3].FindControl("cmbNewProduct");
        TextBox txtNewPOCode = (TextBox)this.grvItem.FooterRow.Cells[5].FindControl("txtNewPOCode");//
        TextBox txtNewPQty = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewPQty");
        TextBox txtNewQCQty = (TextBox)this.grvItem.FooterRow.Cells[8].FindControl("txtNewQCQty");
        DropDownList cmbNewUnit = (DropDownList)this.grvItem.FooterRow.Cells[9].FindControl("cmbNewUnit");
        TextBox txtNewSQty = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewSQty");

        string[] sData = txtGetData.Text.Split('|');
        if (sData.Length == 8)
        {
            cmbNewProduct.SelectedValue = sData[0];
            txtNewBarCode.Text = sData[1];
            txtNewPOCode.Text = sData[2];
            txtNewPQty.Text = sData[3];
            txtNewSQty.Text = sData[4];
            cmbNewUnit.SelectedValue = sData[5];
            txtRefLoid.Text = sData[6];
            txtPrice.Text = sData[7];

        }
    }

    protected void cmbNewProduct_SelectedIndexChanged1(object sender, EventArgs e)
    {
        //DropDownList cmb = (DropDownList)sender;
        //TextBox txtCode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewBarCode");
        //TextBox txtQty = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewQty");
        //DropDownList cmbUnit = (DropDownList)this.grvItem.FooterRow.Cells[5].FindControl("cmbNewUnit");
        //TextBox txtPrice = (TextBox)this.grvItem.FooterRow.Cells[6].FindControl("txtNewPrice");
        //TextBox txtNetPrice = (TextBox)this.grvItem.FooterRow.Cells[7].FindControl("txtNewNetPrice");

        //ProductSearchData data = FlowObj.GetProductData(Convert.ToDouble(cmb.SelectedItem.Value));

        //txtCode.Text = data.BARCODE;
        //cmbUnit.SelectedIndex = cmbUnit.Items.IndexOf(cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        //txtPrice.Text = data.PRICE.ToString();
        //txtNetPrice.Text = (Convert.ToDouble(txtQty.Text == "" ? "0" : txtQty.Text) * data.PRICE).ToString();
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

        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        DropDownList cmbProduct = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[3].FindControl("cmbProduct");
        TextBox txtSQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[7].FindControl("txtSQty");
        TextBox txtPQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[6].FindControl("txtPQty");
        DropDownList cmbUnit = (DropDownList)this.grvItem.Rows[e.RowIndex].Cells[9].FindControl("cmbUnit");

        StockInItemData data = new StockInItemData();
        data.PRODUCT = Convert.ToDouble(cmbProduct.SelectedItem.Value);
        data.QTY = Convert.ToDouble(txtSQty.Text == "" ? "0" : txtSQty.Text);
        data.PQTY = Convert.ToDouble(txtPQty.Text == "" ? "0" : txtPQty.Text);


        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[10].Text;
        e.NewValues["PRODUCT"] = data.PRODUCT.ToString();
        e.NewValues["SQTY"] = data.QTY.ToString();
        e.NewValues["PQTY"] = data.QTY.ToString();



    }

    #endregion

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Transaction/StockInSupplierSearch.aspx");
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

    protected void SubmitQCClick(object sender, EventArgs e)
    {
        StockInFGData data = GetData();
        data.STATUS = Constz.Requisition.Status.QC.Code;
        if (FlowObj.CommitQCData(Authz.CurrentUserInfo.UserID, data))
        {
            ResetState(FlowObj.LOID);
            Appz.ClientAlert(this, "ส่งตรวจ QC เรียบร้อยแล้ว");
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    protected void SubmitFNClick(object sender, EventArgs e)
    {
        //foreach (GridViewRow row in grvItem.Rows)
        //{
        //    double loid = Convert.ToDouble(row.Cells[12].Text);
        //    double poloid = Convert.ToDouble(row.Cells[13].Text);
        //    double pdloid = Convert.ToDouble(row.Cells[14].Text);
        //    double qty = Convert.ToDouble(((Label)row.Cells[8].FindControl("txtSQtyView")).Text);
        //    FlowObj.UpdateQty(loid, poloid, pdloid, qty, Authz.CurrentUserInfo.UserID);
        //}
        if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
        {
            if (FlowObj.UpdateStockInStatus(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text), Constz.Requisition.Status.Finish.Code, Authz.CurrentUserInfo.UserID))
            {
                foreach (GridViewRow row in grvItem.Rows)
                {
                    double loid = Convert.ToDouble(row.Cells[13].Text);
                    double poloid = Convert.ToDouble(row.Cells[14].Text);
                    double pdloid = Convert.ToDouble(row.Cells[15].Text);
                    double qty = Convert.ToDouble(((Label)row.Cells[7].FindControl("txtSQtyView")).Text);
                    FlowObj.UpdateQty(loid, poloid, pdloid, qty, Authz.CurrentUserInfo.UserID);
                }
                ResetState(Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text));
                Appz.ClientAlert(this, "ยืนยันรับเข้าคลังเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
        else
            Appz.ClientAlert(this, FlowObj.ErrorMessage);
    }

    private void SetSender(double sender)
    {
        SupplierData data = FlowObj.GetSenderData(sender);
        this.txtSenderCode.Text = data.CODE;
        this.txtSenderName.Text = data.NAME;

    }

    #endregion
}
