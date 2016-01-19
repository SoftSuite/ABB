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
using ABB.Flow.Inventory.WH;
using ABB.Global;

public partial class WH_ToDoList_Controls_MinStockControl : System.Web.UI.UserControl
{
    private ToDoListFlow _flow;

    public ToDoListFlow FlowObj
    {
        get { if (_flow == null) { _flow = new ToDoListFlow(); } return _flow; }
    }

    private ToDoListMinimumStockData GetSearchData()
    {
        ToDoListMinimumStockData data = new ToDoListMinimumStockData();
        data.ORDERTYPE = this.cmbOrderType.SelectedItem.Value;
        data.PRODUCTNAME = this.txtName.Text.Trim();
        data.STATUS = this.cmbStatus.SelectedItem.Value;
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;
        return data;
    }

    private void SearchData()
    {
        this.grvRequisition.DataSource = FlowObj.GetMinimumStockList(GetSearchData());
        this.grvRequisition.DataBind();
    }

    protected string CheckAll
    {
        get { return "chkAllBox(this, '" + this.grvRequisition.ClientID + "_ctl', '_chkItem')"; }
    }

    private ToDoListMinimumStockRequestData GetData()
    {
        ToDoListMinimumStockRequestData data = new ToDoListMinimumStockRequestData();
        data.ACTIVE = Constz.ActiveStatus.Active;
        data.DIVISION = Authz.CurrentUserInfo.DivisionID;
        data.REQUESTBY = Authz.CurrentUserInfo.OfficerID;
        data.REQUISITIONTYPE = Constz.Requisition.RequisitionType.REQ07;
        data.STATUS = Constz.Requisition.Status.Waiting.Code;
        data.WAREHOUSE = Authz.CurrentUserInfo.Warehouse;

        for (int i = 0; i < this.grvRequisition.Rows.Count; ++i)
        {
            CheckBox chk = (CheckBox)this.grvRequisition.Rows[i].Cells[1].FindControl("chkItem");
            if (chk.Checked && chk.Enabled && this.grvRequisition.Rows[i].Cells[1].CssClass != "zhidden")
            {
                if (data.ORDERTYPE == "")
                    data.ORDERTYPE = this.grvRequisition.Rows[i].Cells[12].Text;
                else if (data.ORDERTYPE != this.grvRequisition.Rows[i].Cells[12].Text)
                    throw new ApplicationException( "ไม่สามารถทำรายการได้ เนื่องจากไม่ใช่ประเภทคำสั่งเดียวกัน");
                ToDoListMinimumStockRequestItemData itemData = new ToDoListMinimumStockRequestItemData();
                itemData.PRODUCT = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[0].Text);
                itemData.QTY = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[9].Text);
                itemData.UNIT = Convert.ToDouble(this.grvRequisition.Rows[i].Cells[13].Text);
                data.ITEM.Add(itemData);
            }
        }
        return data;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btnNewNorm.Text = "<img src='" + Constz.ImageFolder + "icn_new.gif' border='0' align='AbsMiddle'> สร้างใบจัดซื้อ";
            btnNewNorm.Attributes.Add("OnMouseOver", "this.className='toolbarbuttonhover'");
            btnNewNorm.Attributes.Add("OnMouseOut", "this.className='toolbarbutton'");

            ComboSource.BuildOrderTypeCombo(this.cmbOrderType, "ทั้งหมด", "");
            ComboSource.BuildStatusCombo(this.cmbStatus, "ทั้งหมด", "");
            SearchData();
        }
    }

    protected void btnNewNorm_Click(object sender, EventArgs e)
    {
        ToDoListMinimumStockRequestData data = GetData();
        if (data.ORDERTYPE == Constz.OrderType.PO.Code || data.ORDERTYPE == Constz.OrderType.AR.Code)
        {
            try
            {
                if (data.ITEM.Count == 0)
                    throw new ApplicationException("กรุณาเลือกรายการสินค้า");
                else
                {
                    if (FlowObj.NewPODocument(Authz.CurrentUserInfo.UserID, data))
                    {
                        Response.Redirect(ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_PURCHASE].ToString() + "Transaction/PurchaseRequest.aspx?loid=" + FlowObj.LOID.ToString());
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
        else
        {
            Appz.ClientAlert(this, "กรุณาเลือกรายการสินค้าที่มีประเภทการสั่งเป็น สั่งซื้อ");
        }
    }

    protected void NewClick(object sender, EventArgs e)
    {
        ToDoListMinimumStockRequestData data = GetData();
        if (data.ORDERTYPE == Constz.OrderType.PD.Code || data.ORDERTYPE == Constz.OrderType.AR.Code)
        {
            try
            {
                if (data.ITEM.Count == 0)
                    throw new ApplicationException("กรุณาเลือกรายการสินค้า");
                else
                {
                    if (FlowObj.NewPDDocument(Authz.CurrentUserInfo.UserID, data))
                    {
                        Response.Redirect(ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_SALES].ToString() + "Transaction/ProductOrder.aspx?loid=" + FlowObj.LOID.ToString());
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
        else
        {
            Appz.ClientAlert(this, "กรุณาเลือกรายการสินค้าที่มีประเภทการสั่งเป็น สั่งผลิต");
        }
    }

    //protected void NewClick(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ToDoListMinimumStockRequestData data = GetData();
    //        if (data.ITEM.Count == 0)
    //            throw new ApplicationException("กรุณาเลือกรายการสินค้า");
    //        else
    //        {
    //            if (FlowObj.NewDocument(Authz.CurrentUserInfo.UserID, data))
    //            {
    //                if (data.ORDERTYPE == Constz.OrderType.PO.Code)
    //                    Response.Redirect(ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_PURCHASE].ToString() + "Transaction/PurchaseRequest.aspx?loid=" + FlowObj.LOID.ToString());
    //                else
    //                    Response.Redirect(ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_SALES].ToString() + "Transaction/ProductOrder.aspx?loid=" + FlowObj.LOID.ToString());
    //            }
    //            else
    //                Appz.ClientAlert(this, FlowObj.ErrorMessage);
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        Appz.ClientAlert(this, ex.Message);
    //    }
    //}

    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {
        SearchData();
    }

    protected void grvRequisition_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("chkAll");
            chk.Attributes.Add("onclick", CheckAll);
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            //string orderType = drow["ORDERTYPE"].ToString();
            string reftable = drow["REFTABLE"].ToString();
            HyperLink lnkRequisition = (HyperLink)e.Row.Cells[8].FindControl("lnkRequisition");
            if (!Convert.IsDBNull(drow["REQUESTCODE"]))
                lnkRequisition.Text = drow["REQUESTCODE"].ToString();
            else
                lnkRequisition.Text = "";
            if (lnkRequisition.Text == "")
            {
                lnkRequisition.NavigateUrl = "#";
            }
            else
            {
                if (reftable == "RQ")
                {
                    lnkRequisition.NavigateUrl = ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_SALES].ToString() + "Transaction/ProductOrder.aspx?loid=" + drow["REQUESTID"].ToString();
                }
                else if (reftable == "PD")
                {
                    lnkRequisition.NavigateUrl = ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_PURCHASE].ToString() + "Transaction/PurchaseRequest.aspx?loid=" + drow["REQUESTID"].ToString();
                }
            }

            ((CheckBox)e.Row.Cells[0].FindControl("chkItem")).Enabled = (lnkRequisition.Text == "");

            if (e.Row.RowIndex > 0)
            {
                int rowSpan = 0;
                while (drow["LOID"].ToString() == this.grvRequisition.Rows[e.Row.RowIndex - rowSpan - 1].Cells[0].Text)
                {
                    rowSpan += 1;
                    if (e.Row.RowIndex - rowSpan == 0) break;
                }
                if (rowSpan > 0)
                {
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[1].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[2].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[3].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[4].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[5].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[6].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[7].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[8].RowSpan = rowSpan + 1;
                    this.grvRequisition.Rows[e.Row.RowIndex - rowSpan].Cells[9].RowSpan = rowSpan + 1;
                    for (int i = 0; i < 9; ++i)
                    {
                        e.Row.Cells[i+1].CssClass = "zHidden";
                    }
                }
            }

        }
    }

}
