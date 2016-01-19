using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Data.OracleClient;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;
using ABB.DAL;
using ABB.Data.Sales;
using ABB.Data.Inventory.WH;
using ABB.Global;
using ABB.Flow.Common;
using ABB.Flow.Inventory.WH;

public partial class WH_Master_Product : System.Web.UI.Page
{
    private ProductFlow _flow;
    private ProductFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductFlow(); } return _flow; }
    }
    private ProductMonthFlow _flow2;
    private ProductMonthFlow FlowObj2
    {
        get { if (_flow2 == null) { _flow2 = new ProductMonthFlow(); } return _flow2; }
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
        this.txtLeadtime.Text = "";
        this.txtLotSize.Text = "";
        this.txtCost.Text = "";
        this.txtPrice.Text = "";
        this.txtStdPrice.Text = "";
        this.rbtOrderType.SelectedIndex = 0;
        this.txtPacksize.Text = "";
        //this.txtAge.Text = "1";
        this.cmbUnitPack.SelectedIndex = 0;

        foreach (ListItem item in chkMonth.Items)
        {
            if (item.Selected)
                item.Selected = false;
        }

    }

    private void ResetState(double LOID)
    {
        if (LOID != 0)
        {
            ProductSearchData data = FlowObj.GetData(LOID);
            SetData(data);
            ProductMonthData data2 = FlowObj2.GetData(LOID);
            SetData2(data2);
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
        data.CODE = this.txtCode.Text.Trim();
        data.BARCODE = this.txtBarCode.Text.Trim();
        data.COST = Convert.ToDouble(this.txtCost.Text == "" ? "0" : this.txtCost.Text);
        data.LEADTIME = Convert.ToDouble(this.txtLeadtime.Text == "" ? "0" : this.txtLeadtime.Text);
        data.LOTSIZE = Convert.ToDouble(this.txtLotSize.Text == "" ? "0" : this.txtLotSize.Text);
        data.LEADTIMEPD = Convert.ToDouble(this.txtLeadtimePD.Text == "" ? "0" : this.txtLeadtimePD.Text);
        data.LOTSIZEPD = Convert.ToDouble(this.txtLotSizePD.Text == "" ? "0" : this.txtLotSizePD.Text);
        data.ORDERTYPE = this.rbtOrderType.SelectedItem.Value.Trim();
        data.PRICE = Convert.ToDouble(this.txtPrice.Text == "" ? "0" : this.txtPrice.Text);
        data.STDPRICE = Convert.ToDouble(this.txtStdPrice.Text == "" ? "0" : this.txtStdPrice.Text);
        data.PRODUCTGROUP = Convert.ToDouble(this.cmbProductGroup.SelectedItem.Value);
        data.LOID = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);
        data.NAME = this.txtName.Text.Trim();
        data.ENAME = this.txtEName.Text.Trim();
        data.ABBNAME = this.txtAbbName.Text.Trim();
        data.PRODUCTTYPE = Convert.ToDouble(this.cmbProductType.SelectedItem.Value);
        data.UNIT = Convert.ToDouble(this.cmbUnit.SelectedItem.Value);
        data.PACKSIZEUNIT = Convert.ToDouble(this.cmbUnitPack.SelectedItem.Value);
        data.PACKSIZE = Convert.ToDouble(this.txtPacksize.Text == "" ? "0" : this.txtPacksize.Text);
        //data.AGE = Convert.ToDouble(this.txtAge.Text == "" ? "0" : this.txtAge.Text);
        data.PBLOID = Convert.ToDouble(this.txtPBLOID.Text == "" ? "0" : this.txtPBLOID.Text);

        return data;
    }

    private ProductMonthData GetData2()
    {
        ProductMonthData data2 = new ProductMonthData();
        string[] m = new string[12];

        foreach (ListItem item in chkMonth.Items)
        {
            if (item.Selected)
                m[int.Parse(item.Value) - 1] = "1";
            else
                m[int.Parse(item.Value) - 1] = "0";
        }
        data2.Month = m;
        data2.CODE = this.txtCode.Text.Trim();
        data2.PRODUCT = Convert.ToDouble(this.txtLOID.Text == "" ? "0" : this.txtLOID.Text);

        return data2;
    }

    private void SetData(ProductSearchData data)
    {
        this.txtLOID.Text = data.LOID.ToString();
        this.txtCode.Text = data.CODE.Trim();
        this.cmbProductType.SelectedIndex = this.cmbProductType.Items.IndexOf(this.cmbProductType.Items.FindByValue(data.PRODUCTTYPE.ToString()));
        this.chkActive.Checked = (data.ACTIVE == Constz.ActiveStatus.Active);
        this.txtName.Text = data.NAME.Trim();
        this.txtEName.Text = data.ENAME.Trim();
        this.txtAbbName.Text = data.ABBNAME.Trim();
        this.txtBarCode.Text = data.BARCODE.Trim();
        this.txtCost.Text = data.COST.ToString();
        this.txtLeadtime.Text = data.LEADTIME.ToString();
        this.txtLotSize.Text = data.LOTSIZE.ToString();
        this.txtLeadtimePD.Text = data.LEADTIMEPD.ToString();
        this.txtLotSizePD.Text = data.LOTSIZEPD.ToString();
        this.rbtOrderType.SelectedValue = data.ORDERTYPE;
        this.txtPrice.Text = data.PRICE.ToString();
        this.txtStdPrice.Text = data.STDPRICE.ToString();
        getDataToGroup();
        this.cmbProductGroup.SelectedIndex = this.cmbProductGroup.Items.IndexOf(this.cmbProductGroup.Items.FindByValue(data.PRODUCTGROUP.ToString()));
        this.cmbUnit.SelectedIndex = this.cmbUnit.Items.IndexOf(this.cmbUnit.Items.FindByValue(data.UNIT.ToString()));
        this.cmbUnitPack.SelectedIndex = this.cmbUnitPack.Items.IndexOf(this.cmbUnitPack.Items.FindByValue(data.PACKSIZEUNIT.ToString()));
        this.txtPacksize.Text = data.PACKSIZE.ToString();
        //this.txtAge.Text = data.AGE.ToString();
        this.txtPBLOID.Text = data.PBLOID.ToString();
    }

    private void SetData2(ProductMonthData data2)
    {
        for (int i = 0; i < data2.Month.Length; i++)
        {
            if (data2.Month[i] == "1")
                chkMonth.Items[i].Selected = true;
            else
                chkMonth.Items[i].Selected = false;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ControlUtil.SetDblTextBox(this.txtCost);
            ControlUtil.SetDblTextBox(this.txtPrice);
            ControlUtil.SetIntTextBox(this.txtLeadtime);
            ControlUtil.SetIntTextBox(this.txtLeadtimePD);
            ControlUtil.SetDblTextBox(this.txtStdPrice);
            ControlUtil.SetIntTextBox(this.txtLotSize);
            ControlUtil.SetIntTextBox(this.txtLotSizePD);
            //ControlUtil.SetDblTextBox(this.txtAge);
            ControlUtil.SetIntTextBox(this.txtPacksize);

            ComboSource.BuildCombo(this.cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "TYPE = '" + Constz.ProductType.Type.WH.Code + "' AND ACTIVE = '1' ");
            ComboSource.BuildCombo(this.cmbUnit, "UNIT", "NAME", "LOID", "NAME", "");
            ComboSource.BuildCombo(this.cmbUnitPack, "UNIT", "NAME", "LOID", "NAME", "ACTIVE = 1");
            ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = '" + Constz.ActiveStatus.Active + "' AND PRODUCTTYPE = " + cmbProductType.SelectedValue);

            this.rbtOrderType.Items.Clear();
            this.rbtOrderType.Items.Add(new ListItem(Constz.OrderType.PD.Name, Constz.OrderType.PD.Code));
            this.rbtOrderType.Items.Add(new ListItem(Constz.OrderType.PO.Name, Constz.OrderType.PO.Code));
            this.rbtOrderType.Items.Add(new ListItem(Constz.OrderType.AR.Name, Constz.OrderType.AR.Code));
            ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
            CheckOrderType();
        }
    }

    protected void BackClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Master/ProductSerach.aspx");
    }

    protected void CancelClick(object sender, EventArgs e)
    {
        ResetState(Convert.ToDouble(txtLOID.Text == "" ? "0" : txtLOID.Text));
    }

    protected void SaveClick(object sender, EventArgs e)
    {
       try
        {
            if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
            {
                this.txtLOID.Text = FlowObj.LOID.ToString();
                if (FlowObj2.UpdateData(Authz.CurrentUserInfo.UserID, GetData2()))
                {
                    ResetState(FlowObj.LOID);
                    Appz.ClientAlert(this, "บันทึกข้อมูลเรียบร้อยแล้ว");
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);

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
            if (FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, GetData()))
            {
                this.txtLOID.Text = FlowObj.LOID.ToString();
                if (FlowObj2.UpdateData(Authz.CurrentUserInfo.UserID, GetData2()))
                {
                    ResetState(FlowObj.LOID);
                    Response.Redirect(Constz.HomeFolder + "WH/Master/ProductBarcode.aspx?loid=" + this.txtLOID.Text);
                }
                else
                    Appz.ClientAlert(this, FlowObj.ErrorMessage);
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);

        }
        catch (Exception ex)
        {
            Appz.ClientAlert(this, ex.Message);
        }
    }

    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        getDataToGroup();
    }

    private void getDataToGroup()
    {
        ComboSource.BuildCombo(this.cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", "ACTIVE = 1 AND PRODUCTTYPE = " + cmbProductType.SelectedValue);
    }

    protected void rbtOrderType_SelectedIndexChanged(object sender, EventArgs e)
    {
        CheckOrderType();
    }
    private void CheckOrderType()
    {
        if (this.rbtOrderType.SelectedValue == Constz.OrderType.PO.Code)
        {
            this.txtLotSizePD.CssClass = "zTextbox-View";
            this.txtLeadtimePD.CssClass = "zTextbox-View";
            this.txtLotSizePD.ReadOnly = true;
            this.txtLeadtimePD.ReadOnly = true;
            this.txtLeadtimePD.Text = "";
            this.txtLotSizePD.Text = "";

            this.txtLotSize.CssClass = "zTextbox";
            this.txtLeadtime.CssClass = "zTextbox";
            this.txtLotSize.ReadOnly = false;
            this.txtLeadtime.ReadOnly = false;

        }
        else if (this.rbtOrderType.SelectedValue == Constz.OrderType.PD.Code)
        {
            this.txtLotSize.CssClass = "zTextbox-View";
            this.txtLeadtime.CssClass = "zTextbox-View";
            this.txtLotSize.ReadOnly = true;
            this.txtLeadtime.ReadOnly = true;
            this.txtLeadtime.Text = "";
            this.txtLotSize.Text = "";
            this.txtLotSizePD.CssClass = "zTextbox";
            this.txtLeadtimePD.CssClass = "zTextbox";
            this.txtLotSizePD.ReadOnly = false;
            this.txtLeadtimePD.ReadOnly = false;
        }
        else if (this.rbtOrderType.SelectedValue == Constz.OrderType.AR.Code)
        {
            this.txtLotSize.CssClass = "zTextbox";
            this.txtLeadtime.CssClass = "zTextbox";
            this.txtLotSize.ReadOnly = false;
            this.txtLeadtime.ReadOnly = false;

            this.txtLotSizePD.CssClass = "zTextbox";
            this.txtLeadtimePD.CssClass = "zTextbox";
            this.txtLotSizePD.ReadOnly = false;
            this.txtLeadtimePD.ReadOnly = false;
        }

    }

}
