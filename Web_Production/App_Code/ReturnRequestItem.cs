using System;
using System.Collections;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Production;
using ABB.Flow.Common;
using ABB.Flow.Production;

/// <summary>
/// Summary description for ReturnRequestItem
/// </summary>
public class ReturnRequestItem
{
    public ReturnRequestItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
      private string sessionName = "ReturnRequestItem";
    string _error = "";
    private ReturnRequestFlow _flow;
    public ReturnRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnRequestFlow(); return _flow; }
    }

    public string ErrorMessage
    {
        get { return _error; }
    }

    public void ClearSession()
    {
        System.Web.HttpContext.Current.Session.Remove(sessionName);
    }

    private void ReOrder(DataTable dt)
    {
        int i = 1;
        foreach (DataRow dRow in dt.Rows)
        {
            dRow["RANK"] = i;
            dRow["LOID"] = i;
            i += 1;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPDItem(double requisition, double pdploid)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            if (requisition == 0)
                dt = FlowObj.GetPDItem(pdploid);
            else
                dt = FlowObj.GetRQMItem(requisition);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPDItemBlank()
    {
        return FlowObj.GetPDItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePDItem(double RANK)
    {
        bool ret = true;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRow = dt.Select("RANK = " + RANK.ToString());
            dt.Rows.Remove(dRow[0]);
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        else
            ret = false;
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 MINSTOCK, 0 MAXSTOCK, 0 STOCK, 0 OLDPRICE, 0 CURPRICE, 0 MINPRICE, 0 LAST3MON, 0 LASTYEAR, ''DUEDATE, '' BARCODE, '' UNITNAME
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdatePDItem(double LOID, double PRODUCT, double MASTER, string PRODUCTNAME, double UNIT, double USEQTY, double WASTEQTYMAT, double RETURNQTY, double RANK)
    {
        ReturnItemData data = new ReturnItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.RETURNQTY = Convert.ToDouble(RETURNQTY);
        data.PDLOID = Convert.ToDouble(PRODUCT);
        data.PRODUCTNAME = PRODUCTNAME;
        data.MASTER = Convert.ToDouble(MASTER);
        data.USEQTY = Convert.ToDouble(USEQTY);
        data.WASTEQTYMAT = Convert.ToDouble(WASTEQTYMAT);
        data.UNIT = Convert.ToDouble(UNIT);
        bool ret = true;
        ret = VerifyData(data);

        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + LOID.ToString());
                DataRow dRow = dRows[0];
                //dRow["LOID"] = data.LOID;
                dRow["RETURNQTY"] = data.RETURNQTY;
                //dRow["PRODUCT"] = data.PDLOID;
                //dRow["MASTER"] = data.MASTER;
                //dRow["PRODUCTNAME"] = data.PRODUCTNAME;
                //dRow["USEQTY"] = data.USEQTY;
                //dRow["WASTEQTYMAT"] = data.WASTEQTYMAT;
                //dRow["UNIT"] = data.UNIT;

                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        return ret;
    }

    private bool VerifyData(ReturnItemData data)
    {
        bool ret = true;
        //if (data.RWNAME = "")
        //{
        //    ret = false;
        //    _error = "กรุณาเลือกวัตถุดิบ";
        //}
        if (data.RETURNQTY == 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวนที่คืน";
        }
        //else
        //{
        //    DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        //    foreach (DataRow dRow in dt.Rows)
        //    {
        //        if (dRow["RWBARCODE"] == data.RWBARCODE && Convert.ToDouble(dRow["LOID"]) != data.LOID)
        //        {
        //            _error = "รายการวัตถุดิบนี้มีอยู่ในรายการแล้ว";
        //            ret = false;
        //            break;
        //        }
        //    }
        //}
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 MINSTOCK, 0 MAXSTOCK, 0 STOCK, 0 OLDPRICE, 0 CURPRICE, 0 MINPRICE, 0 LAST3MON, 0 LASTYEAR, ''DUEDATE, '' BARCODE, '' UNITNAME
    public bool InsertPDItem(ReturnItemData data)
    {
        bool ret = true;
        //ret = VerifyData(data);
        //if (ret)
        //{
        //    DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        //    if (dt != null)
        //    {
        //        ReOrder(dt);
        //        DataRow dRow = dt.NewRow();
        //        dRow["LOID"] = Convert.ToDouble(dt.Rows.Count) + 1;
        //        dRow["RANK"] = Convert.ToDouble(dRow["LOID"]);
        //        dRow["RWBARCODE"] = Convert.ToDouble(dRow["RWBARCODE"]);
        //        dRow["RWNAME"] = Convert.ToDouble(dRow["RWNAME"]);
        //        dRow["RWGROUPNAME"] = Convert.ToDouble(dRow["RWGROUPNAME"]);
        //        //ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
        //        //dRow["BARCODE"] = product.BARCODE;
        //        //dRow["UNAME"] = FlowObj.GetUnitData(data.UNAME).NAME;
        //        dt.Rows.Add(dRow);
        //        System.Web.HttpContext.Current.Session[sessionName] = dt;
        //    }
        //}
        return ret;
    }

    public ArrayList GetItemList()
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        ArrayList arr = new ArrayList();
        if (dt != null)
        {
            foreach (DataRow dRow in dt.Rows)
            {
                ReturnItemData data = new ReturnItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PDLOID = Convert.ToDouble(dRow["PRODUCT"]);
                data.MASTER = Convert.ToDouble(dRow["MASTER"]);
                data.PRODUCTNAME = dRow["PRODUCTNAME"].ToString();
                data.USEQTY = dRow["USEQTY"]==DBNull.Value ? 0 : Convert.ToDouble(dRow["USEQTY"]);
                data.WASTEQTYMAT = dRow["WASTEQTYMAT"] == DBNull.Value ? 0 : Convert.ToDouble(dRow["WASTEQTYMAT"]);
                data.RETURNQTY = dRow["RETURNQTY"] == DBNull.Value ? 0 : Convert.ToDouble(dRow["RETURNQTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                arr.Add(data);
            }
        }
        return arr;
    }

    public ArrayList GetRecentItemList()
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        ArrayList arr = new ArrayList();
        if (dt != null)
        {
            foreach (DataRow dRow in dt.Rows)
            {
                ReturnItemData data = new ReturnItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PDLOID = Convert.ToDouble(dRow["PRODUCT"]);
                data.MASTER = Convert.ToDouble(dRow["MASTER"]);
                data.PRODUCTNAME = dRow["PRODUCTNAME"].ToString();
                data.USEQTY = Convert.ToDouble(dRow["USEQTY"]);
                data.WASTEQTYMAT = Convert.ToDouble(dRow["WASTEQTYMAT"]);
                data.RETURNQTY = Convert.ToDouble(dRow["RETURNQTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                arr.Add(data);
            }
        }
        return arr;
    }
}

