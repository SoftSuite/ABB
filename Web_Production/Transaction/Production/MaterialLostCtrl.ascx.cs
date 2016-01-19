using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using ABB.Data;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Global;
using ABB.Flow.Production;

public partial class Transaction_Production_MaterialLostCtrl : System.Web.UI.UserControl
{
    private DataTable tempTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            CreateTempTable();
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    public void SetPrintScript(double PDPLoid)
    {
        ToolbarControl1.ClientClickPrint = ABB.Global.Appz.ReportScript(Constz.Report.MaterialLost, PDPLoid) + " return false;";
    }

    private void CreateTempTable()
    {
        tempTable = new DataTable();
        DataColumn dcORDERNO = new DataColumn("ORDERNO");
        DataColumn dcMTRNAME = new DataColumn("MTRNAME");
        DataColumn dcBMASTER = new DataColumn("BMASTER");
        DataColumn dcRQMASTER = new DataColumn("RQMASTER");
        DataColumn dcWASTEQTYMAT = new DataColumn("WASTEQTYMAT");
        DataColumn dcWASTEQTYMAN = new DataColumn("WASTEQTYMAN");
        DataColumn dcUSEQTY = new DataColumn("USEQTY");
        DataColumn dcRETURNQTY = new DataColumn("RETURNQTY");
        DataColumn dcCHANGEQTY = new DataColumn("CHANGEQTY");
        DataColumn dcYIELDMAT = new DataColumn("YIELDMAT");
        DataColumn dcYIELDMAM = new DataColumn("YIELDMAM");
        DataColumn dcREMARK = new DataColumn("REMARK");
        DataColumn dcBLOID = new DataColumn("BLOID");
        DataColumn dcPDPLOID = new DataColumn("PDPLOID");
        DataColumn dcMTRLOID = new DataColumn("MTRLOID");
        DataColumn dcULOID = new DataColumn("ULOID");
        DataColumn dcACTIVE = new DataColumn("ACTIVE");
        DataColumn dcALLQTY = new DataColumn("ALLQTY");
        DataColumn dcPGROUP = new DataColumn("PGROUP");
        DataColumn dcPRODSTATUS = new DataColumn("PRODSTATUS");
        DataColumn dcPOSTATUS = new DataColumn("POSTATUS");
        DataColumn dcUSEQTY2 = new DataColumn("USEQTY2");

        tempTable.Columns.Add(dcORDERNO);
        tempTable.Columns.Add(dcMTRNAME);
        tempTable.Columns.Add(dcBMASTER);
        tempTable.Columns.Add(dcRQMASTER);
        tempTable.Columns.Add(dcWASTEQTYMAT);
        tempTable.Columns.Add(dcWASTEQTYMAN);
        tempTable.Columns.Add(dcUSEQTY);
        tempTable.Columns.Add(dcRETURNQTY);
        tempTable.Columns.Add(dcCHANGEQTY);
        tempTable.Columns.Add(dcYIELDMAT);
        tempTable.Columns.Add(dcYIELDMAM);
        tempTable.Columns.Add(dcREMARK);
        tempTable.Columns.Add(dcBLOID);
        tempTable.Columns.Add(dcPDPLOID);
        tempTable.Columns.Add(dcMTRLOID);
        tempTable.Columns.Add(dcULOID);
        tempTable.Columns.Add(dcACTIVE);
        tempTable.Columns.Add(dcALLQTY);
        tempTable.Columns.Add(dcPGROUP);
        tempTable.Columns.Add(dcPRODSTATUS);
        tempTable.Columns.Add(dcPOSTATUS);
        tempTable.Columns.Add(dcUSEQTY2);
    }

    private void LoadData(string PdpLoid)
    {
        DataTable dt = PDProductFlow.GetMaterialLostData(PdpLoid);
        tempTable.Rows.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = tempTable.Rows.Add();
            dr["ORDERNO"] = Convert.ToString(i + 1);
            dr["MTRNAME"] = dt.Rows[i]["MTRNAME"].ToString();
            dr["BMASTER"] = dt.Rows[i]["BMASTER"].ToString();
            dr["RQMASTER"] = dt.Rows[i]["RQMASTER"].ToString();
            dr["WASTEQTYMAT"] = dt.Rows[i]["WASTEQTYMAT"].ToString();
            dr["WASTEQTYMAN"] = dt.Rows[i]["WASTEQTYMAN"].ToString();
            dr["USEQTY"] = dt.Rows[i]["USEQTY"].ToString();
            dr["RETURNQTY"] = dt.Rows[i]["RETURNQTY"].ToString();
            dr["CHANGEQTY"] = dt.Rows[i]["CHANGEQTY"].ToString();
            dr["YIELDMAT"] = dt.Rows[i]["YIELDMAT"].ToString();
            dr["YIELDMAM"] = dt.Rows[i]["YIELDMAM"].ToString();
            dr["REMARK"] = dt.Rows[i]["REMARK"].ToString();
            dr["BLOID"] = dt.Rows[i]["BLOID"].ToString();
            dr["PDPLOID"] = dt.Rows[i]["PDPLOID"].ToString();
            dr["MTRLOID"] = dt.Rows[i]["MTRLOID"].ToString();
            dr["ULOID"] = dt.Rows[i]["ULOID"].ToString();
            dr["ACTIVE"] = dt.Rows[i]["ACTIVE"].ToString();
            dr["ALLQTY"] = dt.Rows[i]["ALLQTY"].ToString();
            dr["PGROUP"] = dt.Rows[i]["PGROUP"].ToString();
            dr["PRODSTATUS"] = dt.Rows[i]["PRODSTATUS"].ToString();
            dr["POSTATUS"] = dt.Rows[i]["POSTATUS"].ToString();
            dr["USEQTY2"] = dt.Rows[i]["USEQTY2"].ToString();
        }
        Session["tempMaterialLost"] = tempTable;
        gvResult.DataSource = tempTable;
        gvResult.DataBind();
    }

    protected void gvResult_RowEditing(object sender, GridViewEditEventArgs e)
    {
        GridView gvResult = (GridView)sender;
        Label lblProdstatus = (Label)gvResult.Rows[e.NewEditIndex].FindControl("PRODSTATUS");
        Label lblPOStatus = (Label)gvResult.Rows[e.NewEditIndex].FindControl("POSTATUS");

        if (lblProdstatus.Text.Trim() != "AP" || lblPOStatus.Text.Trim() != "AP")
        {
            gvResult.EditIndex = e.NewEditIndex;
            gvResult.DataSource = Session["tempMaterialLost"];
            gvResult.DataBind();
            TextBox txtWasteQtyMat = (TextBox)gvResult.Rows[e.NewEditIndex].FindControl("txtWasteQtyMat");
            TextBox txtWasteQtyMan = (TextBox)gvResult.Rows[e.NewEditIndex].FindControl("txtWasteQtyMan");
            TextBox txtReturnQty = (TextBox)gvResult.Rows[e.NewEditIndex].FindControl("txtReturnQty");
            TextBox txtChangeQty = (TextBox)gvResult.Rows[e.NewEditIndex].FindControl("txtChangeQty");
            ControlUtil.SetDblTextBox(txtWasteQtyMat);
            ControlUtil.SetDblTextBox(txtWasteQtyMan);
            ControlUtil.SetDblTextBox(txtReturnQty);
            ControlUtil.SetDblTextBox(txtChangeQty);
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่สามารถแก้ไขรายการได้");
            return;
        }  
    }

    protected void gvResult_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView gvResult = (GridView)sender;
        Label lblProdstatus = (Label)gvResult.Rows[e.RowIndex].FindControl("PRODSTATUS");
        Label lblPOStatus = (Label)gvResult.Rows[e.RowIndex].FindControl("POSTATUS");

        if (lblProdstatus.Text.Trim() != "AP" || lblPOStatus.Text.Trim() != "AP")
        {
            gvResult.EditIndex = -1;
            gvResult.DataSource = Session["tempMaterialLost"];
            gvResult.DataBind();
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่สามารถแก้ไขรายการได้");
            return;
        }
    }

    protected void gvResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        ImageButton imbDelete = (ImageButton)e.Row.FindControl("imbDelete");
        if (imbDelete != null)
            imbDelete.OnClientClick = "return confirm('คุณต้องการลบรายการใช่หรือไม่');";   
    }

    protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gvResult = (GridView)sender;
        Label lblProdstatus = (Label)gvResult.Rows[e.RowIndex].FindControl("PRODSTATUS");
        Label lblPOStatus = (Label)gvResult.Rows[e.RowIndex].FindControl("POSTATUS");

        if (lblProdstatus.Text.Trim() != "AP" || lblPOStatus.Text.Trim() != "AP")
        {
            tempTable = (DataTable)Session["tempMaterialLost"];
            Label lblPdName = (Label)gvResult.Rows[e.RowIndex].FindControl("lblPdName");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                if (tempTable.Rows[i]["MTRNAME"].ToString() == lblPdName.Text.Trim())
                {
                    tempTable.Rows[i].Delete();
                }
            }

            //เรื่องลำดับหมายเลขใหม่หลังจากทำการลบ
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                tempTable.Rows[i]["ORDERNO"] = i + 1;
            }

            Session["tempMaterialLost"] = tempTable;
            gvResult.DataSource = tempTable;
            gvResult.DataBind();
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่สามารถแก้ไขรายการได้");
            return;
        }  
    }


    protected void gvResult_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        double summt = 0;
        double summn = 0;
        GridView gvResult = (GridView)sender;
        Label lblProdstatus = (Label)gvResult.Rows[e.RowIndex].FindControl("PRODSTATUS");
        Label lblPOStatus = (Label)gvResult.Rows[e.RowIndex].FindControl("POSTATUS");

        if (lblProdstatus.Text.Trim() != "AP" || lblPOStatus.Text.Trim() != "AP")
        {
            tempTable = (DataTable)Session["tempMaterialLost"];
            Label lblPdName = (Label)gvResult.Rows[e.RowIndex].FindControl("lblPdName");
            Label lblBoMaster = (Label)gvResult.Rows[e.RowIndex].FindControl("lblBoMaster");
            TextBox txtWasteQtyMat = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtWasteQtyMat");
            TextBox txtWasteQtyMan = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtWasteQtyMan");
            TextBox txtReturnQty = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtReturnQty");
            TextBox txtChangeQty = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtChangeQty");
            TextBox txtRemark = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtRemark");
            ControlUtil.SetDblTextBox(txtWasteQtyMat);
            ControlUtil.SetDblTextBox(txtWasteQtyMan);
            ControlUtil.SetDblTextBox(txtReturnQty);
            ControlUtil.SetDblTextBox(txtChangeQty);

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                if (tempTable.Rows[i]["MTRNAME"].ToString() == lblPdName.Text.Trim())
                {
                    double tempt = 0;
                    double tempn = 0;
                    tempTable.Rows[i]["WASTEQTYMAT"] = txtWasteQtyMat.Text.Trim();
                    tempTable.Rows[i]["WASTEQTYMAN"] = txtWasteQtyMan.Text.Trim();
                    tempTable.Rows[i]["RETURNQTY"] = txtReturnQty.Text.Trim();
                    tempTable.Rows[i]["CHANGEQTY"] = txtChangeQty.Text.Trim();
                    tempTable.Rows[i]["REMARK"] = txtRemark.Text.Trim();
                    tempt = Convert.ToDouble(txtWasteQtyMat.Text.Trim()) * 100 / Convert.ToDouble(lblBoMaster.Text.Trim());
                    tempTable.Rows[i]["YIELDMAT"] = tempt.ToString();// ("#,##0.00");
                    tempn = Convert.ToDouble(txtWasteQtyMan.Text.Trim()) * 100 / Convert.ToDouble(lblBoMaster.Text.Trim());
                    tempTable.Rows[i]["YIELDMAM"] = tempn.ToString();// ("#,##0.00");
                }
            }

            Session["tempMaterialLost"] = tempTable;
            gvResult.EditIndex = -1;
            gvResult.DataSource = tempTable;
            gvResult.DataBind();
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                summt += Convert.ToDouble(tempTable.Rows[i]["YIELDMAT"].ToString());
                summn += Convert.ToDouble(tempTable.Rows[i]["YIELDMAM"].ToString());
            }
            lblSumYieldmat.Text = summt.ToString();
            lblSumYieldman.Text = summn.ToString();
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่สามารถแก้ไขรายการได้");
            return;
        }  
    }

    public  void MaterialLostReLoadData(double  PdLoid,double PDPLOId)
    {
        CreateTempTable();
        double summt = 0;
        double summn = 0;
        DataTable dt = PDProductFlow.GetMaterialLostReLoadData(PdLoid,PDPLOId);
        tempTable.Rows.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = tempTable.Rows.Add();
            dr["ORDERNO"] = Convert.ToString(i + 1);
            dr["MTRNAME"] = dt.Rows[i]["MTRNAME"].ToString();
            dr["BMASTER"] = dt.Rows[i]["BMASTER"].ToString();
            dr["RQMASTER"] = dt.Rows[i]["RQMASTER"].ToString();
            dr["WASTEQTYMAT"] = dt.Rows[i]["WASTEQTYMAT"].ToString();
            dr["WASTEQTYMAN"] = dt.Rows[i]["WASTEQTYMAN"].ToString();
            dr["USEQTY"] = dt.Rows[i]["USEQTY"].ToString();
            dr["RETURNQTY"] = dt.Rows[i]["RETURNQTY"].ToString();
            dr["CHANGEQTY"] = dt.Rows[i]["CHANGEQTY"].ToString();
            dr["YIELDMAT"] = dt.Rows[i]["YIELDMAT"].ToString();
            dr["YIELDMAM"] = dt.Rows[i]["YIELDMAM"].ToString();
            dr["REMARK"] = dt.Rows[i]["REMARK"].ToString();
            dr["BLOID"] = dt.Rows[i]["BLOID"].ToString();
            dr["PDPLOID"] = dt.Rows[i]["PDPLOID"].ToString();
            dr["MTRLOID"] = dt.Rows[i]["MTRLOID"].ToString();
            dr["ULOID"] = dt.Rows[i]["ULOID"].ToString();
            dr["ACTIVE"] = dt.Rows[i]["ACTIVE"].ToString();
            dr["ALLQTY"] = dt.Rows[i]["ALLQTY"].ToString();
            dr["PGROUP"] = dt.Rows[i]["PGROUP"].ToString();
            dr["PRODSTATUS"] = dt.Rows[i]["PRODSTATUS"].ToString();
            dr["POSTATUS"] = dt.Rows[i]["POSTATUS"].ToString();
            dr["USEQTY2"] = dt.Rows[i]["USEQTY2"].ToString();
            summt += Convert.ToDouble(dt.Rows[i]["YIELDMAT"].ToString());
            summn += Convert.ToDouble(dt.Rows[i]["YIELDMAM"].ToString());
        }
        lblSumYieldmat.Text = summt.ToString();
        lblSumYieldman.Text = summn.ToString();
        Session["tempMaterialLost"] = tempTable;
        gvResult.DataSource = tempTable;
        gvResult.DataBind();
    }
}
