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
using ABB.Flow.Production;
using ABB.Global;

public partial class Transaction_Production_MaterialCtrl : System.Web.UI.UserControl
{
    private  DataTable tempTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Page.IsPostBack == false)
        {
            base.OnInit(e);
            CreateTempTable();
            if (Request.QueryString["PDPLOID"] != null)
                LoadData(Request.QueryString["PDPLOID"].ToString());
        }
    }

    private void CreateTempTable()
    {
        tempTable = new DataTable();
        DataColumn dcORDERNO = new DataColumn("ORDERNO");
        DataColumn dcPGNAME = new DataColumn("PGNAME");
        DataColumn dcBARCODE = new DataColumn("BARCODE");
        DataColumn dcMTRNAME = new DataColumn("MTRNAME");
        DataColumn dcMASTER = new DataColumn("MASTER");
        // Add Lot No
        DataColumn dcMLOTNO = new DataColumn("MLOTNO");
        DataColumn dcUNAME = new DataColumn("UNAME");
        DataColumn dcUSEQTY = new DataColumn("USEQTY");
        DataColumn dcMTRLOID = new DataColumn("MTRLOID");
        DataColumn dcPDPLOID = new DataColumn("PDPLOID");
        DataColumn dcPGLOID = new DataColumn("PGLOID");
        DataColumn dcULOID = new DataColumn("ULOID");
        DataColumn dcPROCESS = new DataColumn("PROCESS");
        DataColumn dcBLOID = new DataColumn("BLOID");
        DataColumn dcWASTEQTYMAT = new DataColumn("WASTEQTYMAT");
        DataColumn dcWASTEQTYMAN = new DataColumn("WASTEQTYMAN");
        DataColumn dcRETURNQTY = new DataColumn("RETURNQTY");
        DataColumn dcCHANGEQTY = new DataColumn("CHANGEQTY");
        DataColumn dcUnit = new DataColumn("UNIT");
        DataColumn dcACTIVE = new DataColumn("ACTIVE");
        DataColumn dcREMARK = new DataColumn("REMARK");
        DataColumn dcYIELDMAT = new DataColumn("YIELDMAT");
        DataColumn dcYIELDMAM = new DataColumn("YIELDMAM");
        DataColumn dcALLQTY = new DataColumn("ALLQTY");
        DataColumn dcPGROUP = new DataColumn("PGROUP");
        DataColumn dcPRODSTATUS = new DataColumn("PRODSTATUS");
        DataColumn dcPOSTATUS = new DataColumn("POSTATUS");
        DataColumn dcUSEQTY2 = new DataColumn("USEQTY2");

        tempTable.Columns.Add(dcORDERNO);
        tempTable.Columns.Add(dcPGNAME);
        tempTable.Columns.Add(dcBARCODE);
        tempTable.Columns.Add(dcMTRNAME);
        tempTable.Columns.Add(dcMASTER);
        // Add Lot No
        tempTable.Columns.Add(dcMLOTNO);
        tempTable.Columns.Add(dcUNAME);
        tempTable.Columns.Add(dcUSEQTY);
        tempTable.Columns.Add(dcMTRLOID);
        tempTable.Columns.Add(dcPDPLOID);
        tempTable.Columns.Add(dcPGLOID);
        tempTable.Columns.Add(dcULOID);
        tempTable.Columns.Add(dcPROCESS);
        tempTable.Columns.Add(dcBLOID);
        tempTable.Columns.Add(dcWASTEQTYMAT);
        tempTable.Columns.Add(dcWASTEQTYMAN);
        tempTable.Columns.Add(dcRETURNQTY);
        tempTable.Columns.Add(dcCHANGEQTY);
        tempTable.Columns.Add(dcUnit);
        tempTable.Columns.Add(dcACTIVE);
        tempTable.Columns.Add(dcREMARK);
        tempTable.Columns.Add(dcYIELDMAT);
        tempTable.Columns.Add(dcYIELDMAM);
        tempTable.Columns.Add(dcALLQTY);
        tempTable.Columns.Add(dcPGROUP);
        tempTable.Columns.Add(dcPRODSTATUS);
        tempTable.Columns.Add(dcPOSTATUS);
        tempTable.Columns.Add(dcUSEQTY2);
    }

    private void LoadData(string PdpLoid)
    {
        DataTable dt = PDProductFlow.GetMaterialData(PdpLoid);
        tempTable.Rows.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = tempTable.Rows.Add();
            dr["ORDERNO"] = Convert.ToString(i + 1);
            dr["PGNAME"] = dt.Rows[i]["PGROUPNAME"].ToString();
            dr["BARCODE"] = dt.Rows[i]["BARCODE"].ToString();
            dr["MTRNAME"] = dt.Rows[i]["MTRNAME"].ToString();
            dr["MASTER"] = dt.Rows[i]["MASTER"].ToString();
            // Add Lot No
            dr["MLOTNO"] = dt.Rows[i]["MLOTNO"].ToString();
            dr["UNAME"] = dt.Rows[i]["UNAME"].ToString();
            dr["USEQTY"] = dt.Rows[i]["USEQTY"].ToString();
            dr["MTRLOID"] = dt.Rows[i]["MTRLOID"].ToString();
            dr["PDPLOID"] = dt.Rows[i]["PDPLOID"].ToString();
            dr["PGLOID"] = dt.Rows[i]["PGLOID"].ToString();
            dr["ULOID"] = dt.Rows[i]["ULOID"].ToString();
            txtProcess.Text = dt.Rows[i]["PROCESS"].ToString();
            dr["BLOID"] = dt.Rows[i]["BLOID"].ToString();
            dr["WASTEQTYMAT"] = dt.Rows[i]["WASTEQTYMAT"].ToString();
            dr["WASTEQTYMAN"] = dt.Rows[i]["WASTEQTYMAN"].ToString();
            dr["RETURNQTY"] = dt.Rows[i]["RETURNQTY"].ToString();
            dr["CHANGEQTY"] = dt.Rows[i]["CHANGEQTY"].ToString();
            dr["UNIT"] = dt.Rows[i]["UNIT"].ToString();
            dr["ACTIVE"] = dt.Rows[i]["ACTIVE"].ToString();
            dr["REMARK"] = dt.Rows[i]["REMARK"].ToString();
            dr["YIELDMAT"] = dt.Rows[i]["YIELDMAT"].ToString();
            dr["YIELDMAM"] = dt.Rows[i]["YIELDMAM"].ToString();
            dr["ALLQTY"] = dt.Rows[i]["ALLQTY"].ToString();
            dr["PGROUP"] = dt.Rows[i]["PGROUP"].ToString();
            dr["PRODSTATUS"] = dt.Rows[i]["PRODSTATUS"].ToString();
            dr["POSTATUS"] = dt.Rows[i]["POSTATUS"].ToString();
            dr["USEQTY2"] = dt.Rows[i]["USEQTY2"].ToString(); 
        }
        Session["tempMaterial"] = tempTable;
        gvResult.DataSource = Session["tempMaterial"];
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
            gvResult.DataSource = Session["tempMaterial"];
            gvResult.DataBind();
            TextBox txtUseQty = (TextBox)gvResult.Rows[e.NewEditIndex].FindControl("txtUseQty");
            ControlUtil.SetDblTextBox(txtUseQty);
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
            gvResult.DataSource = Session["tempMaterial"];
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

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRow drow = (DataRow)((DataRowView)e.Row.DataItem).Row;
            if (e.Row.RowIndex > 0)
            {
                int rowSpan = 0;
                //ผสานเซล ลำดับที่,ประเภท, รหัสวัตถุดิบ, ชื่อวัตถุดิบ, หน่วย, Master
                //Session["tempMaterial"]
                //while (drow["BARCODE"].ToString() == this.gvResult.Rows[e.Row.RowIndex - rowSpan - 1].Cells[4].Text)
                while (drow["BARCODE"].ToString() == ((DataTable)Session["tempMaterial"]).Rows[e.Row.RowIndex - rowSpan - 1]["BARCODE"].ToString())
                {

                    rowSpan += 1;
                    if (e.Row.RowIndex - rowSpan == 0) break;
                }
                if (rowSpan > 0)
                {
                    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[1].RowSpan = rowSpan + 1;
                    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[2].RowSpan = rowSpan + 1;
                    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[3].RowSpan = rowSpan + 1;
                    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[4].RowSpan = rowSpan + 1;
                    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[5].RowSpan = rowSpan + 1;
                    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[6].RowSpan = rowSpan + 1;
                    for (int i = 0; i < 6; ++i)
                    {
                        e.Row.Cells[i+1].CssClass = "zHidden";
                    }
                }


                //ผสานเซล Lot No, จำนวนที่เบิก, ปริมาณการใช้
                //rowSpan = 0;
                //while (drow["MLOTNO"].ToString() == this.gvResult.Rows[e.Row.RowIndex - rowSpan - 1].Cells[8].Text &&
                //       drow["BARCODE"].ToString() == this.gvResult.Rows[e.Row.RowIndex - rowSpan - 1].Cells[10].Text)
                //{

                //    rowSpan += 1;
                //    if (e.Row.RowIndex - rowSpan == 0) break;
                //}
                //if (rowSpan > 0)
                //{
                //    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[8].RowSpan = rowSpan + 1;
                //    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[9].RowSpan = rowSpan + 1;
                //    this.gvResult.Rows[e.Row.RowIndex - rowSpan].Cells[10].RowSpan = rowSpan + 1;
                //    for (int i = 8; i <= 10; ++i)
                //    {
                //        e.Row.Cells[i].CssClass = "zHidden";
                //    }
                //}
            }
        }
    }

    protected void gvResult_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridView gvResult = (GridView)sender;
        Label lblProdstatus = (Label)gvResult.Rows[e.RowIndex].FindControl("PRODSTATUS");
        Label lblPOStatus = (Label)gvResult.Rows[e.RowIndex].FindControl("POSTATUS");

        if (lblProdstatus.Text.Trim() != "AP" || lblPOStatus.Text.Trim() != "AP")
        {
            tempTable = (DataTable)Session["tempMaterial"];
            
            Label lblBarCode = (Label)gvResult.Rows[e.RowIndex].FindControl("lblBarCode");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                if (tempTable.Rows[i]["BARCODE"].ToString() == lblBarCode.Text.Trim())
                {
                    tempTable.Rows[i].Delete();
                }
            }
            //เรื่องลำดับหมายเลขใหม่หลังจากทำการลบ
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                tempTable.Rows[i]["ORDERNO"] = i + 1;
            }
            Session["tempMaterial"] = tempTable;
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
        GridView gvResult = (GridView)sender;
        Label lblProdstatus = (Label)gvResult.Rows[e.RowIndex].FindControl("PRODSTATUS");
        Label lblPOStatus = (Label)gvResult.Rows[e.RowIndex].FindControl("POSTATUS");

        if (lblProdstatus.Text.Trim() != "AP" || lblPOStatus.Text.Trim() != "AP")
        {
            tempTable = (DataTable)Session["tempMaterial"];
            Label lblBarCode = (Label)gvResult.Rows[e.RowIndex].FindControl("lblBarCode");
            TextBox txtUseQty = (TextBox)gvResult.Rows[e.RowIndex].FindControl("txtUseQty");

            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                if (tempTable.Rows[i]["BARCODE"].ToString() == lblBarCode.Text.Trim())
                {
                    tempTable.Rows[i]["USEQTY"] = txtUseQty.Text.Trim();
                }
            }
            Session["tempMaterial"] = tempTable;
            gvResult.EditIndex = -1;
            gvResult.DataSource = tempTable;
            gvResult.DataBind();
        }
        else
        {
            Appz.ClientAlert(Page, "ไม่สามารถแก้ไขรายการได้");
            return;
        }  
    }

    public void MaterialCtrlReLoadData(double PDLOID, double PDPLOID)
    {
        CreateTempTable();
        DataTable dt = PDProductFlow.GetMaterialReLoadData(PDLOID, PDPLOID);
        tempTable.Rows.Clear();
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = tempTable.Rows.Add();
            dr["ORDERNO"] = Convert.ToString(i + 1);
            dr["PGNAME"] = dt.Rows[i]["PGROUPNAME"].ToString();
            dr["BARCODE"] = dt.Rows[i]["BARCODE"].ToString();
            dr["MTRNAME"] = dt.Rows[i]["MTRNAME"].ToString();
            dr["MASTER"] = dt.Rows[i]["MASTER"].ToString();
            // Add Lot No
            dr["MLOTNO"] = dt.Rows[i]["MLOTNO"].ToString();
            dr["UNAME"] = dt.Rows[i]["UNAME"].ToString();
            dr["USEQTY"] = dt.Rows[i]["USEQTY"].ToString();
            dr["MTRLOID"] = dt.Rows[i]["MTRLOID"].ToString();
            dr["PDPLOID"] = dt.Rows[i]["PDPLOID"].ToString();
            dr["PGLOID"] = dt.Rows[i]["PGLOID"].ToString();
            dr["ULOID"] = dt.Rows[i]["ULOID"].ToString();
            txtProcess.Text = dt.Rows[i]["PROCESS"].ToString();
            dr["BLOID"] = dt.Rows[i]["BLOID"].ToString();
            dr["WASTEQTYMAT"] = dt.Rows[i]["WASTEQTYMAT"].ToString();
            dr["WASTEQTYMAN"] = dt.Rows[i]["WASTEQTYMAN"].ToString();
            dr["RETURNQTY"] = dt.Rows[i]["RETURNQTY"].ToString();
            dr["CHANGEQTY"] = dt.Rows[i]["CHANGEQTY"].ToString();
            dr["UNIT"] = dt.Rows[i]["UNIT"].ToString();
            dr["ACTIVE"] = dt.Rows[i]["ACTIVE"].ToString();
            dr["REMARK"] = dt.Rows[i]["REMARK"].ToString();
            dr["YIELDMAT"] = dt.Rows[i]["YIELDMAT"].ToString();
            dr["YIELDMAM"] = dt.Rows[i]["YIELDMAM"].ToString();
            dr["ALLQTY"] = dt.Rows[i]["ALLQTY"].ToString();
            dr["PGROUP"] = dt.Rows[i]["PGROUP"].ToString();
            dr["PRODSTATUS"] = dt.Rows[i]["PRODSTATUS"].ToString();
            dr["POSTATUS"] = dt.Rows[i]["POSTATUS"].ToString();
            dr["USEQTY2"] = dt.Rows[i]["USEQTY2"].ToString(); 
        }
        Session["tempMaterial"] = tempTable;
        gvResult.DataSource = tempTable;
        gvResult.DataBind();
    }
}
