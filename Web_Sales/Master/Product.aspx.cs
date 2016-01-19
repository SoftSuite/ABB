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
using ABB.Global;
using ABB.Flow.Common;
using ABB.Flow.Sales;

public partial class Master_Product : System.Web.UI.Page
{
    protected override PageStatePersister PageStatePersister
    {
        get
        {
            return new SessionPageStatePersister(this);
        }
    }
    private ProductMasterFlow _flow;
    private ProductMasterFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductMasterFlow(); } return _flow; }
    }

    private void ClearData()
    {
        this.txtLOID.Text = "";
        this.txtPBLOID.Text = "";
        this.txtCode.Text = "";
        this.cmbProductType.SelectedIndex = 0;
        this.cmbProductGroup.SelectedIndex = 0;
        this.cmbUnit.SelectedIndex = 0;
        this.chkActive.Checked = true;
        this.txtName.Text = "";
        this.txtEName.Text = "";
        this.txtAbbName.Text = "";
        this.txtBarCode.Text = "";
        this.txtRegisNo.Text = "";
        this.txtLeadtime.Text = "";
        this.txtLotSize.Text = "";
        this.txtCost.Text = "";
        this.txtPrice.Text = "";
        this.rbtOrderType.SelectedIndex = 0;
        this.rbtIsDiscount.SelectedIndex = 0;
        this.rbtIsEdit.SelectedIndex = 0;
        this.rbtIsVat.SelectedIndex = 0;
        this.txtPacksize.Text = "";
        this.txtAge.Text = "1";
        this.cmbUnitPack.SelectedIndex = 0;
        this.rbtIsRefund.SelectedIndex = 0;
    }

    private void ResetState(double LOID)
    {
        if (LOID != 0)
        {
            ProductSearchData data = FlowObj.GetData(LOID);
            SetData(data);
        }
        else
        {
            ClearData();
        }
    }

    private ProductSearchData GetData()
    {
        ProductSearchData data = new ProductSearchData();
        data.ACTIVE = (this.chkActive.Checked ? Constz.ActiveStatus.Active : Constz.ActiveStatus.InActive);
        data.BARCODE = this.txtBarCode.Text.Trim();
        data.COST = Convert.ToDouble(this.txtCost.Text == "" ? "0" : this.txtCost.Text);
        data.ISDISCOUNT = this.rbtIsDiscount.SelectedValue.Trim();
        data.ISEDIT = this.rbtIsEdit.SelectedItem.Value.Trim();
        data.ISVAT = this.rbtIsVat.SelectedItem.Value.Trim();
        data.LEADTIME = Convert.ToDouble(this.txtLeadtime.Text == "" ? "0" : this.txtLeadtime.Text);
        data.LOTSIZE = Convert.ToDouble(this.txtLotSize.Text == "" ? "0" : this.txtLotSize.Text);
        data.LEADTIMEPD = Convert.ToDouble(this.txtLeadtimePD.Text == "" ? "0" : this.txtLeadtimePD.Text);
        data.LOTSIZEPD = Convert.ToDouble(this.txtLotSizePD.Text == "" ? "0" : this.txtLotSizePD.Text);
        data.ORDERTYPE = this.rbtOrderType.SelectedItem.Value.Trim();
        data.PRICE = Convert.ToDouble(this.txtPrice.Text == "" ? "0" : this.txtPrice.Text);
        data.STDPRICE = data.PRICE;
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.REGISNO = this.txtRegisNo.Text.Trim();
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.ENAME = this.txtEName.Text.Trim();
        data.ABBNAME = this.txtAbbName.Text.Trim();
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.UNIT = Convert.ToDouble(this.cmbUnit.SelectedItem.Value);
        data.PACKSIZEUNIT = Convert.ToDouble(this.cmbUnitPack.SelectedItem.Value);
        data.PACKSIZE = Convert.ToDouble(this.txtPacksize.Text == "" ? "0" : this.txtPacksize.Text);
        data.ISREFUND = this.rbtIsRefund.SelectedValue.Trim();
        data.AGE = Convert.ToDouble(this.txtAge.Text == "" ? "0" : this.txtAge.Text);
        data.PBLOID = Convert.ToDouble(this.txtPBLOID.Text == "" ? "0" : this.txtPBLOID.Text);
        data.CODE = this.txtCode.Text.Trim();
        data.PRODUCEGROUP = Convert.ToDouble(this.cmbProduceGroup.SelectedItem.Value);
        return data;
    }

    private void SetData(ProductSearchData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(data.PRODUCTTYPE.ToString()));
        this.chkActive.Checked = (data.ACTIVE == Constz.ActiveStatus.Active);
        this.txtName.Text = data.NAME.Trim();
        this.txtEName.Text = data.ENAME.Trim();
        this.txtAbbName.Text = data.ABBNAME.Trim();
        this.txtBarCode.Text = data.BARCODE.Trim();
        this.txtCost.Text = data.COST.ToString(Constz.DblFormat);
        this.rbtIsDiscount.SelectedValue = data.ISDISCOUNT;
        this.rbtIsEdit.SelectedValue = data.ISEDIT;
        this.rbtIsVat.SelectedValue = data.ISVAT;
        this.txtLeadtime.Text = data.LEADTIME.ToString(Constz.IntFormat);
        this.txtLotSize.Text = data.LOTSIZE.ToString(Constz.IntFormat);
        this.txtLeadtimePD.Text = data.LEADTIMEPD.ToString(Constz.IntFormat);
        this.txtLotSizePD.Text = data.LOTSIZEPD.ToString(Constz.IntFormat);
        this.rbtOrderType.SelectedValue = data.ORDERTYPE;
        this.txtPrice.Text = data.PRICE.ToString(Constz.DblFormat);
        getDataToGroup();
        this.cmbProductGroup.SelectedIndex = this.cmbProductGroup.Items.IndexOf(this.cmbProductGroup.Items.FindByValue(data.PRODUCTGROUP.ToString()));
        this.txtRegisNo.Text = data.REGISNO.ToString();
        this.cmbUnit.SelectedIndex = this.cmbUnit.Items.IndexOf(this.cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        this.cmbUnitPack.SelectedIndex = this.cmbUnitPack.Items.IndexOf(this.cmbUnitPack.Items.FindByValue(data.PACKSIZEUNIT.ToString()));
        this.rbtIsRefund.SelectedIndex = this.rbtIsRefund.Items.IndexOf(this.rbtIsRefund.Items.FindByValue(data.ISREFUND));
        this.txtPacksize.Text = data.PACKSIZE.ToString(Constz.IntFormat);
        this.txtAge.Text = data.AGE.ToString(Constz.IntFormat);
        this.txtPBLOID.Text = data.PBLOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.cmbProduceGroup.SelectedIndex = this.cmbProduceGroup.Items.IndexOf(this.cmbProduceGroup.Items.FindByValue(data.PRODUCEGROUP.ToString()));
        CheckOrderType();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlUtil.SetDblTextBox(this.txtCost);
            ControlUtil.SetDblTextBox(this.txtPrice);
            ControlUtil.SetIntTextBox(this.txtLeadtime);
            ControlUtil.SetIntTextBox(this.txtLeadtimePD);
            ControlUtil.SetDblTextBox(this.txtAge);
            ControlUtil.SetIntTextBox(this.txtLotSize);
            ControlUtil.SetIntTextBox(this.txtLotSizePD);
            ControlUtil.SetIntTextBox(this.txtPacksize);

            this.rbtOrderType.Items.Clear();
            this.rbtOrderType.Items.Add(new ListItem(Constz.OrderType.PO.Name, Constz.OrderType.PO.Code)); ;
            this.rbtOrderType.Items.Add(new ListItem(Constz.OrderType.PD.Name, Constz.OrderType.PD.Code));
            this.rbtOrderType.Items.Add(new ListItem(Constz.OrderType.AR.Name, Constz.OrderType.AR.Code));

            this.rbtIsVat.Items.Clear();
            this.rbtIsVat.Items.Add(new ListItem(Constz.VAT.Included.Name, Constz.VAT.Included.Code)); ;
            this.rbtIsVat.Items.Add(new ListItem(Constz.VAT.NotIncluded.Name, Constz.VAT.NotIncluded.Code));

            this.rbtIsDiscount.Items.Clear();
            this.rbtIsDiscount.Items.Add(new ListItem(Constz.Discount.Calculated.Name, Constz.Discount.Calculated.Code)); ;
            this.rbtIsDiscount.Items.Add(new ListItem(Constz.Discount.NotCalculated.Name, Constz.Discount.NotCalculated.Code));

            this.rbtIsEdit.Items.Clear();
            this.rbtIsEdit.Items.Add(new ListItem(Constz.Edit.Editable.Name, Constz.Edit.Editable.Code)); ;
            this.rbtIsEdit.Items.Add(new ListItem(Constz.Edit.DisEditable.Name, Constz.Edit.DisEditable.Code));

            this.rbtIsRefund.Items.Clear();
            this.rbtIsRefund.Items.Add(new ListItem(Constz.Refund.Yes.Name, Constz.Refund.Yes.Code)); ;
            this.rbtIsRefund.Items.Add(new ListItem(Constz.Refund.No.Name, Constz.Refund.No.Code));

            ComboSourceFlow TypeFlow = new ComboSourceFlow();
            this.cmbProductType.DataSource = TypeFlow.GetSource("PRODUCTTYPE", "NAME", "LOID", "NAME", "(TYPE = '" + Constz.ProductType.Type.FG.Code + "' OR TYPE = '" + Constz.ProductType.Type.Others.Code + "') AND ACTIVE = '" + Constz.ActiveStatus.Active + "' ");
            this.cmbProductType.DataTextField = "NAME";
            this.cmbProductType.DataValueField = "LOID";
            this.cmbProductType.DataBind();

            ComboSourceFlow UnitFlow = new ComboSourceFlow();
            this.cmbUnit.DataSource = UnitFlow.GetSource("UNIT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "'  AND (TYPE = '" + Constz.UnitType.FG.Code + "' OR TYPE = '" + Constz.UnitType.ALL.Code + "')");
            this.cmbUnit.DataTextField = "NAME";
            this.cmbUnit.DataValueField = "LOID";
            this.cmbUnit.DataBind();

            ComboSourceFlow UnitPackFlow = new ComboSourceFlow();
            this.cmbUnitPack.DataSource = UnitPackFlow.GetSource("UNIT", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' ");
            this.cmbUnitPack.DataTextField = "NAME";
            this.cmbUnitPack.DataValueField = "LOID";
            this.cmbUnitPack.DataBind();

            ComboSourceFlow ProduceGroupFlow = new ComboSourceFlow();
            this.cmbProduceGroup.DataSource = TypeFlow.GetSource("PRODUCEGROUP", "NAME", "LOID", "RANK", "");
            this.cmbProduceGroup.DataTextField = "NAME";
            this.cmbProduceGroup.DataValueField = "LOID";
            this.cmbProduceGroup.DataBind();

            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            getDataToGroup();
            CheckOrderType();
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "Master/ProductSearch.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
        try
        {
            if (this.txtLOID.Text == "")
            { 
                FlowObj.InsertData(Authz.CurrentUserInfo.UserID, GetData());
                ResetState(FlowObj.LOID);
            }

            else
                FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData());
            //ClearData();
            Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void SubmitClick(object sender, EventArgs e)
    {
        try
        {
            if (this.txtLOID.Text == "")
            {
                FlowObj.InsertData(Authz.CurrentUserInfo.UserID, GetData());
                ResetState(FlowObj.LOID);
            }

            else
                FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData());
            //ClearData();
            Response.Redirect(Constz.HomeFolder + "Master/ProductBarcode.aspx?loid=" + this.txtLOID.Text);
        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        //ComboSourceFlow GroupFlow = new ComboSourceFlow();
        getDataToGroup();
    }

    private void getDataToGroup()
    {
        ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + cmbProductType.SelectedValue);
    }

    private void CheckOrderType()
    {
        if (this.rbtOrderType.SelectedValue == Constz.OrderType.PO.Code)
        {
            this.txtLotSizePD.CssClass = "zTextboxR-View";
            this.txtLeadtimePD.CssClass = "zTextboxR-View";
            this.txtLotSizePD.ReadOnly = true;
            this.txtLeadtimePD.ReadOnly = true;
            this.txtLeadtimePD.Text = "";
            this.txtLotSizePD.Text = "";

            this.txtLotSize.CssClass = "zTextboxR";
            this.txtLeadtime.CssClass = "zTextboxR";
            this.txtLotSize.ReadOnly = false;
            this.txtLeadtime.ReadOnly = false;

        }
        else if (this.rbtOrderType.SelectedValue == Constz.OrderType.PD.Code)
        {
            this.txtLotSize.CssClass = "zTextboxR-View";
            this.txtLeadtime.CssClass = "zTextboxR-View";
            this.txtLotSize.ReadOnly = true;
            this.txtLeadtime.ReadOnly = true;
            this.txtLeadtime.Text = "";
            this.txtLotSize.Text = "";
            this.txtLotSizePD.CssClass = "zTextboxR";
            this.txtLeadtimePD.CssClass = "zTextboxR";
            this.txtLotSizePD.ReadOnly = false;
            this.txtLeadtimePD.ReadOnly = false;
        }
        else if (this.rbtOrderType.SelectedValue == Constz.OrderType.AR.Code)
        {
            this.txtLotSize.CssClass = "zTextboxR";
            this.txtLeadtime.CssClass = "zTextboxR";
            this.txtLotSize.ReadOnly = false;
            this.txtLeadtime.ReadOnly = false;

            this.txtLotSizePD.CssClass = "zTextboxR";
            this.txtLeadtimePD.CssClass = "zTextboxR";
            this.txtLotSizePD.ReadOnly = false;
            this.txtLeadtimePD.ReadOnly = false;
        }

    }
    protected void rbtOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckOrderType();
    }
}
