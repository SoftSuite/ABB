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

public partial class FG_Transaction_ControlTransport : System.Web.UI.Page
{
    private ControlTransportFlow _flow;
    private DeliveryItem item;

    public ControlTransportFlow FlowObj
    {
        get { if (_flow == null) _flow = new ControlTransportFlow(); return _flow; }
    }

    public DeliveryItem ItemObj
    {
        get { if (item == null) item = new DeliveryItem(); return item; }
    }

    private void SetGrvItem()
    {
        this.grvItem.DataBind();
        this.grvItemNew.DataBind();

        if (grvItem.Rows.Count > 0)
        {
            this.grvItem.ShowFooter = true;
            this.grvItem.Columns[0].Visible = true;
            this.grvItem.Visible = true;
            this.grvItemNew.Visible = false;
        }
        else
        {
            this.grvItem.Visible = false;
            this.grvItemNew.Visible = true;
        }

        Calculation();
    }

    private void Calculation()
    {
        double QTY = 0;
        if (grvItem.Rows.Count > 0)
        {
            foreach (GridViewRow row in grvItem.Rows)
            {
                if (row.RowState == DataControlRowState.Edit || row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate))
                {
                    TextBox txtQTY = (TextBox)row.Cells[4].FindControl("txtBoxQty");
                    QTY += Convert.ToDouble(txtQTY.Text);
                }
                else
                {
                    Label txtQTY = (Label)row.Cells[4].FindControl("txtBoxQty");
                    QTY += Convert.ToDouble(txtQTY.Text);
                }
            }
        }
        txtTotal.Text = QTY.ToString(Constz.IntFormat);
    }

    private void ResetState(double loid)
    {
        ItemObj.ClearSession();
        SetData(FlowObj.GetData(loid));
    }

    private void SetData(CtrlDeliveryData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCarNo.Text = data.CARNO;
        this.ctlDeliveryDate.DateValue = (data.DELIVERYDATE.Year == 1 ? DateTime.Today : data.DELIVERYDATE);
        this.txtDeliveryName.Text = data.DELIVERYNAME;
        this.txtCreateBy.Text = (data.CREATEBY == "" ? Authz.CurrentUserInfo.UserID : data.CREATEBY);
        this.txtCode.Text = data.CODE;
        this.ctlToolbar.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.ControlTransport, data.LOID) + " return false;";

        SetGrvItem();
    }

    private CtrlDeliveryData GetData()
    {
        CtrlDeliveryData data = new CtrlDeliveryData();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.CARNO = this.txtCarNo.Text.Trim();
        data.DELIVERYDATE = this.ctlDeliveryDate.DateValue;
        data.DELIVERYNAME = this.txtDeliveryName.Text.Trim();
        data.ITEM = ItemObj.GetItemList();

        return data;
    }

    #region Event

    protected void Page_Load(object sender, EventArgs e)
    {
		 btnPrintDetail.Text = "<img src='" + Constz.ImageFolder + "icn_print.gif' border='0' align='AbsMiddle'> �����㺻�˹�ҡ��ͧ";
        if (!IsPostBack)
        {
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
        }
    }

    #region grvItem "Insert"

    protected void grvItemNew_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            Int16 rowIndex = 0;
            TextBox txtBoxQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewBoxQty");
            TextBox txtRequisition = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewRequisition");
            TextBox txtInvcode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewInvcode");


            CtrlDeliveryItemData data = new CtrlDeliveryItemData();

            data.REQUISITION = Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text);
            data.BOXQTY = Convert.ToDouble(txtBoxQty.Text == "" ? "0" : txtBoxQty.Text);
            data.INVCODE = txtInvcode.Text;


            if (ItemObj.InsertRequisitionItem(data))
            {
                SetGrvItem();

            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
        else if (e.CommandName == "Search")
        {
            Int16 rowIndex = 0;
            TextBox txtInvCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewInvcode");
            TextBox txtBoxQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewBoxQty");
            Label txtContactName = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewContactName");
            Label txtCname = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewCname");
            Label txtAddress = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewAddress");
            Label txtTel = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewTel");
            TextBox txtRequisition = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewRequisition");

            CtrlDeliveryItemData data = FlowObj.GetRequisition(Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text), txtInvCode.Text.Trim());
            txtInvCode.Text = data.INVCODE;
            txtContactName.Text = data.CONTACTNAME;
            txtCname.Text = data.CNAME;
            txtAddress.Text = data.CADDRESS;
            txtTel.Text = data.CTEL;
            txtRequisition.Text = data.REQUISITION.ToString();
        }
    }

    protected void grvItemNew_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[4].FindControl("txtNewBoxQty"));

            TextBox txtRequisition = (TextBox)e.Row.Cells[2].FindControl("txtNewRequisition");
            TextBox txtInvCode = (TextBox)e.Row.Cells[2].FindControl("txtNewInvcode");
            string script = "";
            script += "document.getElementById('" + txtRequisition.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/InvoiceSearch.aspx' + (document.getElementById('" + txtInvCode.ClientID + "').value == '' ? '' : '?invcode=' + escape(document.getElementById('" + txtInvCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + txtRequisition.ClientID + "').value || '' == document.getElementById('" + txtRequisition.ClientID + "').value) ";
            script += "{ document.getElementById('" + txtRequisition.ClientID + "').value = ''; return false; } ";
            ImageButton btnSearch = (ImageButton)e.Row.Cells[2].FindControl("imbNewSearch");
            btnSearch.OnClientClick = script;

        }
    }

    protected void txtNewInvcode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;

        TextBox txtInvCode = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewInvcode");
        TextBox txtBoxQty = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[4].FindControl("txtNewBoxQty");
        Label txtContactName = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewContactName");
        Label txtCname = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewCname");
        Label txtAddress = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewAddress");
        Label txtTel = (Label)this.grvItemNew.Rows[rowIndex].Cells[3].FindControl("txtNewTel");
        TextBox txtRequisition = (TextBox)this.grvItemNew.Rows[rowIndex].Cells[2].FindControl("txtNewRequisition");

        CtrlDeliveryItemData data = FlowObj.GetRequisition(Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text), txtInvCode.Text.Trim());

        txtInvCode.Text = data.INVCODE;
        txtContactName.Text = data.CONTACTNAME;
        txtCname.Text = data.CNAME;
        txtAddress.Text = data.CADDRESS;
        txtTel.Text = data.CTEL;
        txtRequisition.Text = data.REQUISITION.ToString();
    }



    #endregion

    #region grvItem

    protected void grvItem_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox txtBoxQty = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewBoxQty");
            TextBox txtRequisition = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewRequisition");
            TextBox txtInvcode = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewInvcode");

            CtrlDeliveryItemData data = new CtrlDeliveryItemData();

            data.REQUISITION = Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text);
            data.BOXQTY = Convert.ToDouble(txtBoxQty.Text == "" ? "0" : txtBoxQty.Text);
            data.INVCODE = txtInvcode.Text;


            if (ItemObj.InsertRequisitionItem(data))
            {
                SetGrvItem();
            }
            else
                Appz.ClientAlert(this, ItemObj.ErrorMessage);
        }
        else if (e.CommandName == "EditSearch")
        {
            int rowIndex = ((GridViewRow)((ImageButton)e.CommandSource).Parent.Parent).RowIndex;

            TextBox txtInvCode = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtInvcode");
            TextBox txtBoxQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtBoxQty");
            Label txtContactName = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtContactName");
            Label txtCname = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtCname");
            Label txtAddress = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtAddress");
            Label txtTel = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtTel");
            TextBox txtRequisition = (TextBox)this.grvItem.Rows[rowIndex].Cells[2].FindControl("txtRequisition");

            CtrlDeliveryItemData data = FlowObj.GetRequisition(Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text), txtInvCode.Text.Trim());
            txtInvCode.Text = data.INVCODE;
            txtContactName.Text = data.CONTACTNAME;
            txtCname.Text = data.CNAME;
            txtAddress.Text = data.CADDRESS;
            txtTel.Text = data.CTEL;
            txtRequisition.Text = data.REQUISITION.ToString();
        }
        else if (e.CommandName == "Search")
        {
            int rowIndex = ((GridViewRow)((ImageButton)e.CommandSource).Parent.Parent).RowIndex;

            TextBox txtInvCode = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewInvcode");
            TextBox txtBoxQty = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewBoxQty");
            Label txtContactName = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewContactName");
            Label txtCname = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewCname");
            Label txtAddress = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewAddress");
            Label txtTel = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewTel");
            TextBox txtRequisition = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewRequisition");

            CtrlDeliveryItemData data = FlowObj.GetRequisition(Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text), txtInvCode.Text.Trim());
            txtInvCode.Text = data.INVCODE;
            txtContactName.Text = data.CONTACTNAME;
            txtCname.Text = data.CNAME;
            txtAddress.Text = data.CADDRESS;
            txtTel.Text = data.CTEL;
            txtRequisition.Text = data.REQUISITION.ToString();
        }
    }

    protected void grvItem_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.RowState == (DataControlRowState.Edit | DataControlRowState.Alternate) || e.Row.RowState == DataControlRowState.Edit)
            {
                ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[4].FindControl("txtBoxQty"));
                TextBox txtRequisition = (TextBox)e.Row.Cells[2].FindControl("txtRequisition");
                TextBox txtInvCode = (TextBox)e.Row.Cells[2].FindControl("txtInvcode");
                string script = "";
                script += "document.getElementById('" + txtRequisition.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/InvoiceSearch.aspx' + (document.getElementById('" + txtInvCode.ClientID + "').value == '' ? '' : '?invcode=' + escape(document.getElementById('" + txtInvCode.ClientID + "').value)), '600', '550');";
                script += "if ('undefined' ==  document.getElementById('" + txtRequisition.ClientID + "').value || '' == document.getElementById('" + txtRequisition.ClientID + "').value) ";
                script += "{ document.getElementById('" + txtRequisition.ClientID + "').value = ''; return false; } ";
                ImageButton btnSearch = (ImageButton)e.Row.Cells[2].FindControl("imbSearch");
                btnSearch.OnClientClick = script;
            }
        }
        else if (e.Row.RowType == DataControlRowType.Footer)
        {
            ControlUtil.SetIntTextBox((TextBox)e.Row.Cells[4].FindControl("txtNewBoxQty"));

            TextBox txtRequisition = (TextBox)e.Row.Cells[2].FindControl("txtNewRequisition");
            TextBox txtInvCode = (TextBox)e.Row.Cells[2].FindControl("txtNewInvcode");
            string script = "";
            script += "document.getElementById('" + txtRequisition.ClientID + "').value = OpenNewModalDialog('" + Constz.HomeFolder + "Search/InvoiceSearch.aspx' + (document.getElementById('" + txtInvCode.ClientID + "').value == '' ? '' : '?invcode=' + escape(document.getElementById('" + txtInvCode.ClientID + "').value)), '600', '550');";
            script += "if ('undefined' ==  document.getElementById('" + txtRequisition.ClientID + "').value || '' == document.getElementById('" + txtRequisition.ClientID + "').value) ";
            script += "{ document.getElementById('" + txtRequisition.ClientID + "').value = ''; return false; } ";
            ImageButton btnSearch = (ImageButton)e.Row.Cells[2].FindControl("imbNewSearch");
            btnSearch.OnClientClick = script;
        }
    }

    protected void txtInvcode_TextChanged(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        Int16 rowIndex = (Int16)((GridViewRow)txt.Parent.Parent).RowIndex;
        this.grvItem.Rows[rowIndex].Cells[1].Text = (rowIndex + 1).ToString();
        TextBox txtBoxQty = (TextBox)this.grvItem.Rows[rowIndex].Cells[4].FindControl("txtBoxQty");
        Label txtContactName = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtContactName");
        Label txtCname = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtCname");
        Label txtAddress = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtAddress");
        Label txtTel = (Label)this.grvItem.Rows[rowIndex].Cells[3].FindControl("txtTel");
        TextBox txtRequisition = (TextBox)this.grvItem.Rows[rowIndex].Cells[2].FindControl("txtRequisition");

        CtrlDeliveryItemData data = FlowObj.GetRequisition(Convert.ToDouble(txtRequisition.Text == "" ? "0" : txtRequisition.Text), txt.Text.Trim());

        txtRequisition.Text = data.INVCODE;
        txtContactName.Text = data.CONTACTNAME;
        txtCname.Text = data.CNAME;
        txtAddress.Text = data.CADDRESS;
        txtTel.Text = data.CTEL;
        txtRequisition.Text = data.REQUISITION.ToString();

    }



    protected void txtNewInvcode_TextChanged1(object sender, EventArgs e)
    {
        TextBox txt = (TextBox)sender;
        TextBox txtNewBoxQty = (TextBox)this.grvItem.FooterRow.Cells[4].FindControl("txtNewBoxQty");
        Label txtNewContactName = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewContactName");
        Label txtNewCname = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewCname");
        Label txtNewAddress = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewAddress");
        Label txtNewTel = (Label)this.grvItem.FooterRow.Cells[3].FindControl("txtNewTel");
        TextBox txtNewRequisition = (TextBox)this.grvItem.FooterRow.Cells[2].FindControl("txtNewRequisition");

        CtrlDeliveryItemData data = FlowObj.GetRequisition(Convert.ToDouble(txtNewRequisition.Text == "" ? "0" : txtNewRequisition.Text), txt.Text.Trim());

        txt.Text = data.INVCODE;
        txtNewContactName.Text = data.CONTACTNAME;
        txtNewCname.Text = data.CNAME;
        txtNewAddress.Text = data.CADDRESS;
        txtNewTel.Text = data.CTEL;
        txtNewRequisition.Text = data.REQUISITION.ToString();

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
            Calculation();
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
            SetGrvItem();
            //Calculation();
        }
    }

    protected void grvItem_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        TextBox txtBoxQty = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[4].FindControl("txtBoxQty");
        TextBox txtInvcode = (TextBox)this.grvItem.Rows[e.RowIndex].Cells[2].FindControl("txtInvcode");
        CtrlDeliveryItemData data = new CtrlDeliveryItemData();

        data.BOXQTY = Convert.ToDouble(txtBoxQty.Text == "" ? "0" : txtBoxQty.Text);
        data.INVCODE = txtInvcode.Text;


        e.NewValues["LOID"] = this.grvItem.Rows[e.RowIndex].Cells[5].Text;
        e.NewValues["BOXQTY"] = data.BOXQTY.ToString();
        e.NewValues["INVCODE"] = data.INVCODE;


    }

    #endregion

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "FG/Transaction/ControlTransportSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        if (this.txtCarNo.Text.Trim() == "")
            Appz.ClientAlert(this.Page, "��س��к��Ţ����¹ö");
        else if (this.txtDeliveryName.Text.Trim() == "")
            Appz.ClientAlert(this.Page, "��س��кت��ͤ��Ѻ");
        else if (ItemObj.GetItemList().Count == 0)
            Appz.ClientAlert(this.Page, "��س��к���¡��");
        else
        {
            if (this.txtLOID.Text == "" || this.txtLOID.Text == "0")
            {
                CtrlDeliveryData data = GetData();

                data.CODE = "";
                data.TYPE = 1;
                data.DELIVERYDATE = DateTime.Now.Date;
                data.CARNO = this.txtCarNo.Text.Trim();
                data.DELIVERYNAME = this.txtDeliveryName.Text.Trim();

                if (FlowObj.NewRequisition(Authz.CurrentUserInfo.UserID, data))
                {
                    ResetState(FlowObj.LOID);
                    Appz.ClientAlert(this, "�ѹ�֡���������º��������");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }

            if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
            {
                ResetState(FlowObj.LOID);
                Appz.ClientAlert(this, "�ѹ�֡���������º��������");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);
        }
    }

    protected void PrintClick(object sender, EventArgs e)
    {

    }

    #endregion

    protected void btnPrintDetail_Click(object sender, EventArgs e)
    {
        double loid = Convert.ToDouble((Request["loid"] == null ? txtLOID.Text.Trim() : Request["loid"]));
        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "controlTransportDetail", ABB.Global.Appz.ReportScript(Constz.Report.ControlTransportDetail, loid), true);
    }
}

