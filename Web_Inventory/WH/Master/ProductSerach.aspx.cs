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
using ABB.Data.Inventory.WH;
using ABB.Flow.Inventory.WH;
using ABB.Global;
using ABB.Flow.Reports;

public partial class WH_Master_ProductSerach : System.Web.UI.Page
{
    private ProductFlow _flow;
    private ProductFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ProductFlow(); } return _flow; }
    }

    private void ResetSate(string sWhere)
    {
        this.grvProduct.DataSource = FlowObj.GetProductList(sWhere);
        this.grvProduct.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvProduct.ClientID + "_ctl', '_chkItem')"; }
    }

    private ArrayList GetChecked()
    {
        ArrayList arrLOID = new ArrayList();
        for (int i = 0; i < this.grvProduct.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvProduct.Rows[i].Cells[0].FindControl("chkItem");
            if (chk.Checked && chk.Enabled) { arrLOID.Add(Convert.ToDouble(this.grvProduct.Rows[i].Cells[2].Text)); }
        }
        return arrLOID;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnDownload.Text = "<img src='" + Constz.ImageFolder + "ttf.gif' border='0' align='AbsMiddle'> ดาวน์โหลด font บาร์โค้ดสำหรับพิมพ์รายงาน";
            btnDownload.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnDownload.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");
            this.btnDownload.OnClientClick = "window.open('/Web_Reports/Utility/IDAutomationHC39M_Free.ttf'); return false;";

            ComboSource.BuildCombo(cmbProductType, "PRODUCTTYPE", "NAME", "LOID", "NAME", "TYPE = '" + Constz.ProductType.Type.WH.Code + "' AND ACTIVE = '1' ", "เลือก", "0");
            SetProductGroupCombo();
            this.ctlToolbar.ClientClickDelete = "return confirm('ต้องการลบสินค้าใช่หรือไม่?');";
       
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        Response.Redirect(Constz.HomeFolder + "WH/Master/Product.aspx");
    }

    protected void PrintClick(object sender, EventArgs e)
    {
        if (ReportsFlow.BarcodeProductReport(cmbProductType.SelectedItem.Value, cmbProductGroup.SelectedItem.Value, txtName.Text) == true)
        {
            string temp = "";
            temp = "paramfield1=producttype";
            temp += "&paramvalue1=" + cmbProductType.SelectedItem.Value;
            temp += "&paramfield2=productgroup";
            temp += "&paramvalue2=" + cmbProductGroup.SelectedItem.Value;
            temp += "&paramfield3=product";
            temp += "&paramvalue3=" + txtName.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "report", Appz.ReportScript("repBarcodeMaterial", temp), true);
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่พบข้อมูล");
            return;
        }
    }

    protected void DeleteClick(object sender, EventArgs e)
    {
        ArrayList arr = GetChecked();
        if (arr.Count > 0)
        {
            if (FlowObj.DeleteData(arr))
            {
                ResetSate((string)ViewState["sWhere"]);
                Appz.ClientAlert(this, "ลบรายการเรียบร้อยแล้ว");
            }
            else
                Appz.ClientAlert(this, FlowObj.ErrorMessage);

        }
        else
            Appz.ClientAlert(this, "กรุณาเลือกรายการที่ต้องการ");
    }

    protected void grvProduct_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            ((Label)e.Row.Cells[1].FindControl("lblNo")).Text = (e.Row.RowIndex + 1).ToString();
        }
    }

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        string sWhere = " WHERE PT.TYPE ='" + Constz.ProductType.Type.WH.Code + "' AND PD.ISDEFAULT = 'Y' ";
        if (this.cmbProductType.SelectedValue != "0")
        {
            sWhere += " AND PRODUCTTYPE = " + this.cmbProductType.SelectedValue;
        }
        if (this.cmbProductGroup.SelectedValue != "0")
        {
            sWhere += " AND PRODUCTGROUP = " + this.cmbProductGroup.SelectedValue;
        }
        if (this.txtName.Text.Trim() != "")
        {
            sWhere += " AND UPPER(PD.NAME) LIKE UPPER('%" + this.txtName.Text.Trim() + "%')";
        }
        ViewState["sWhere"] = sWhere;
        ResetSate(sWhere);
    }
    protected void cmbProductType_SelectedIndexChanged(object sender, EventArgs e)
    {
        SetProductGroupCombo();
    }

    private void SetProductGroupCombo()
    {
        string sFilter = "PRODUCTTYPE = " + this.cmbProductType.SelectedValue + " AND ACTIVE = '1' ";
        ComboSource.BuildCombo(cmbProductGroup, "PRODUCTGROUP", "NAME", "LOID", "NAME", sFilter, "เลือก", "0");
    }
}
