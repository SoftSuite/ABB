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


public partial class Search_Request : System.Web.UI.Page
{
    private string _dateFrom = "";
    private string _dateTo = "";
    private DataTable tempTable = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Expires = 0;
            Response.AddHeader("pragma", "no-cache");
            Response.AddHeader("cache-control", "private");
            Response.CacheControl = "no-cache";

            LoadData();
        }
    }

    private void LoadData()
    {
        CreateTempTable();
        _dateFrom = setDateFrom();
        _dateTo =  setDateTo();

        //if (txtCFrom.Text.Trim() != "" && txtCTo.Text.Trim() == "")
        //{
        //    Appz.ClientAlert(Page, "กรุณากรอกช่วงเลขที่บันทึกสั่งผลิต");
        //    return;
        //}

        //if (txtCFrom.Text.Trim() == "" && txtCTo.Text.Trim() != "")
        //{
        //    Appz.ClientAlert(Page, "กรุณากรอกช่วงเลขที่บันทึกสั่งผลิต");
        //    return;
        //}

        if (_dateFrom == "1/1/1" && _dateTo != "1/1/1")
        {
            Appz.ClientAlert(Page, "กรุณากรอกช่วงวันที่สั่งผลิต");
            return;
        }

        if (_dateFrom != "1/1/1" && _dateTo == "1/1/1")
        {
            Appz.ClientAlert(Page, "กรุณากรอกช่วงวันที่สั่งผลิต");
            return;
        }
        DataTable dt = SearchRequestPopupFlow.GetRequestPopup(txtCFrom.Text.Trim(), txtCTo.Text.Trim(), _dateFrom, _dateTo,txtPName.Text.Trim());
        if (dt.Rows.Count > 0)
        {
            tempTable.Rows.Clear();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = tempTable.Rows.Add();
                dr["ORDERNO"] = Convert.ToString(i + 1);
                dr["RQILOID"] = dt.Rows[i]["RQILOID"].ToString();
                dr["CODE"] = dt.Rows[i]["CODE"].ToString();
                dr["REQDATE"] = dt.Rows[i]["REQDATE"].ToString();
                dr["NAME"] = dt.Rows[i]["NAME"].ToString();
                dr["QTY"] = dt.Rows[i]["QTY"].ToString();
                dr["UNAME"] = dt.Rows[i]["UNAME"].ToString();
                dr["PDLOID"] = dt.Rows[i]["PDLOID"].ToString();
            }
            Session["tempRequestPopup"] = tempTable;
            grvRequest.DataSource = tempTable;
            grvRequest.DataBind();
        }
    }

    private string  setDateFrom()
    {
        string str = "";
        str = dpReqDateFrom.DateValue.Day.ToString()+'/';
        str += dpReqDateFrom.DateValue.Month.ToString() + '/';
        str += dpReqDateFrom.DateValue.Year.ToString();
        return str;
    }
    private string setDateTo()
    {
        string str = "";
        str = dpReqDateTo.DateValue.Day.ToString() + '/';
        str += dpReqDateTo.DateValue.Month.ToString() + '/';
        str += dpReqDateTo.DateValue.Year.ToString();
        return str;
    }

    private void CreateTempTable()
    {
        tempTable = new DataTable();
        DataColumn dcORDERNO = new DataColumn("ORDERNO");
        DataColumn dcRQILOID = new DataColumn("RQILOID");
        DataColumn dcCODE = new DataColumn("CODE");
        DataColumn dcREQDATE = new DataColumn("REQDATE");
        DataColumn dcNAME = new DataColumn("NAME");
        DataColumn dcQTY = new DataColumn("QTY");
        DataColumn dcUNAME = new DataColumn("UNAME");
        DataColumn dcPDLOID = new DataColumn("PDLOID");

        tempTable.Columns.Add(dcORDERNO);
        tempTable.Columns.Add(dcRQILOID);
        tempTable.Columns.Add(dcCODE);
        tempTable.Columns.Add(dcREQDATE);
        tempTable.Columns.Add(dcNAME);
        tempTable.Columns.Add(dcQTY);
        tempTable.Columns.Add(dcUNAME);
        tempTable.Columns.Add(dcPDLOID);
    }
    protected void btnSearch_Click(object sender, ImageClickEventArgs e)
    {  
        LoadData();
    }
 
    protected void grvRequest_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hplCode = (HyperLink)e.Row.FindControl("hplCode");

            //string code = e.Row.Cells[7].Text.Trim();
            //string reqdate = e.Row.Cells[3].Text.Trim();
            string reqiloid = e.Row.Cells[0].Text.Trim();
            //string qty = e.Row.Cells[5].Text.Trim();
            //string pdloid = e.Row.Cells[8].Text.Trim();
            //string script = "window.opener.document.getElementById('" + Request.QueryString["reqcode"] + "').value = '" + code + "';";
            //script += "window.opener.document.getElementById('" + Request.QueryString["reqdate"] + "').value = '" + reqdate + "';";
            //script += "window.opener.document.getElementById('" + Request.QueryString["reqiloid"] + "').value = '" + reqiloid + "';";
            //script += "window.opener.document.getElementById('" + Request.QueryString["qty"] + "').value = '" + qty + "';";
            //script += "window.opener.document.getElementById('" + Request.QueryString["Pdloid"] + "').value = '" + pdloid + "';";
            //script += "window.close();";
            string script = "window.returnValue = '" + reqiloid + "';window.close(true);";
            hplCode.Attributes["onclick"] = script;
           
        }

    }
}
