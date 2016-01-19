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
using ABB.Data.Purchase;
using ABB.Flow.Common;
using ABB.Flow.Purchase;

/// <summary>
/// Summary description for PRItem
/// </summary>
[System.ComponentModel.DataObject()]
public class PRItem
{
    public PRItem()
    {
    }

    private string sessionName = "prItem";
    string _error = "";
    private PurchaseRequestFlow _flow;
    public PurchaseRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new PurchaseRequestFlow(); return _flow; }
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
    public DataTable GetPRItem(double PDRequest, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetPRItem(PDRequest);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPRItemBlank()
    {
        return FlowObj.GetPRItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePRItem(double LOID)
    {
        bool ret = true;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRow = dt.Select("LOID = " + LOID.ToString());
            dt.Rows.Remove(dRow[0]);
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        else
            ret = false;
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 MINSTOCK, 0 MAXSTOCK, 0 STOCK, 0 OLDPRICE, 0 CURPRICE, 0 MINPRICE, 0 LAST3MON, 0 LASTYEAR, ''DUEDATE, '' BARCODE, '' UNITNAME, '' URGENT, '' ISMATERIAL
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdatePRItem(decimal LOID, decimal PRODUCT, decimal QTY, decimal UNIT, decimal MINSTOCK, decimal MAXSTOCK, decimal STOCK, decimal OLDPRICE, decimal CURPRICE, decimal MINPRICE, decimal LAST3MON, decimal LASTYEAR, decimal RANK, string BARCODE, DateTime DUEDATE, string URGENT, string ISMATERIAL, string PRODUCTNAME)
    {
        PRItemData data = new PRItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.QTY = Convert.ToDouble(QTY);
        data.CURPRICE = Convert.ToDouble(CURPRICE);
        data.DUEDATE = DUEDATE;

        bool ret = true;
        ret = VerifyData(data);

        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["PRODUCT"] = Convert.ToDouble(PRODUCT);
                dRow["QTY"] = Convert.ToDouble(QTY);
                dRow["UNIT"] = Convert.ToDouble(UNIT);
                dRow["MINSTOCK"] = Convert.ToDouble(MINSTOCK);
                dRow["MAXSTOCK"] = Convert.ToDouble(MAXSTOCK);
                dRow["STOCK"] = Convert.ToDouble(STOCK);
                dRow["OLDPRICE"] = Convert.ToDouble(OLDPRICE);
                dRow["CURPRICE"] = Convert.ToDouble(CURPRICE);
                dRow["MINPRICE"] = Convert.ToDouble(MINPRICE);
                dRow["LAST3MON"] = Convert.ToDouble(LAST3MON);
                dRow["LASTYEAR"] = Convert.ToDouble(LASTYEAR);
                dRow["DUEDATE"] = DUEDATE;
                ProductSearchData product = FlowObj.GetProductData(Convert.ToDouble(PRODUCT));
                dRow["BARCODE"] = product.BARCODE;
                dRow["PRODUCTNAME"] = product.NAME;
                dRow["UNITNAME"] = FlowObj.GetUnitData(Convert.ToDouble(UNIT)).NAME;
                dRow["URGENT"] = URGENT;
                dRow["ISMATERIAL"] = ISMATERIAL;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(PRItemData data)
    {
        bool ret = true;
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        else if (data.QTY == 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวน";
        }
        //else if (data.CURPRICE == 0)
        //{
        //    ret = false;
        //    _error = "กรุณาระบุราคาปัจจุบัน";
        //}
        else if (data.DUEDATE.Year == 1)
        {
            ret = false;
            _error = "กรุณาระบุกำหนดส่ง";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    _error = "รายการสินค้านี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    break;
                }
            }
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 MINSTOCK, 0 MAXSTOCK, 0 STOCK, 0 OLDPRICE, 0 CURPRICE, 0 MINPRICE, 0 LAST3MON, 0 LASTYEAR, ''DUEDATE, '' BARCODE, '' UNITNAME, '' URGENT, '' ISMATERIAL
    public bool InsertPRItem(PRItemData data)
    {
        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                ReOrder(dt);
                DataRow dRow = dt.NewRow();
                dRow["LOID"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["RANK"] = Convert.ToDouble(dRow["LOID"]);
                dRow["PRODUCT"] = Convert.ToDouble(data.PRODUCT);
                dRow["QTY"] = Convert.ToDouble(data.QTY);
                dRow["UNIT"] = Convert.ToDouble(data.UNIT);
                dRow["MINSTOCK"] = Convert.ToDouble(data.MINSTOCK);
                dRow["MAXSTOCK"] = Convert.ToDouble(data.MAXSTOCK);
                dRow["STOCK"] = Convert.ToDouble(data.STOCK);
                dRow["OLDPRICE"] = Convert.ToDouble(data.OLDPRICE);
                dRow["CURPRICE"] = Convert.ToDouble(data.CURPRICE);
                dRow["MINPRICE"] = Convert.ToDouble(data.MINPRICE);
                dRow["LAST3MON"] = Convert.ToDouble(data.LAST3MON);
                dRow["LASTYEAR"] = Convert.ToDouble(data.LASTYEAR);
                dRow["DUEDATE"] = data.DUEDATE;
              //  ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
                dRow["PRODUCTNAME"] = data.PRODUCTNAME;
                dRow["BARCODE"] = data.BARCODE;
                dRow["UNITNAME"] = FlowObj.GetUnitData(data.UNIT).NAME;
                dRow["URGENT"] = data.URGENT;
                dRow["ISMATERIAL"] = data.ISMATERIAL;
                dt.Rows.Add(dRow);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
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
                PRItemData data = new PRItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.MINSTOCK = Convert.ToDouble(dRow["MINSTOCK"]);
                data.MAXSTOCK = Convert.ToDouble(dRow["MAXSTOCK"]);
                data.STOCK = Convert.ToDouble(dRow["STOCK"]);
                data.OLDPRICE = Convert.ToDouble(dRow["OLDPRICE"]);
                data.CURPRICE = Convert.ToDouble(dRow["CURPRICE"]);
                data.MINPRICE = Convert.ToDouble(dRow["MINPRICE"]);
                data.LAST3MON = Convert.ToDouble(dRow["LAST3MON"]);
                data.LASTYEAR = Convert.ToDouble(dRow["LASTYEAR"]);
                data.DUEDATE = Convert.ToDateTime(dRow["DUEDATE"]);
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.URGENT = dRow["URGENT"].ToString();
                data.ISMATERIAL = dRow["ISMATERIAL"].ToString();
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
                PRItemData data = new PRItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                PRItemData RecentData = FlowObj.GetRecentPRItem(data.PRODUCT);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.MINSTOCK = RecentData.MINSTOCK;
                data.MAXSTOCK = RecentData.MAXSTOCK;
                data.STOCK = RecentData.STOCK;
                data.OLDPRICE = RecentData.OLDPRICE;
                data.CURPRICE = Convert.ToDouble(dRow["CURPRICE"]);
                data.MINPRICE = RecentData.MINPRICE;
                data.LAST3MON = RecentData.LAST3MON;
                data.LASTYEAR = RecentData.LASTYEAR;
                data.DUEDATE = Convert.ToDateTime(dRow["DUEDATE"]);
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.URGENT = dRow["URGENT"].ToString();
                data.ISMATERIAL = dRow["ISMATERIAL"].ToString();
                
                arr.Add(data);
            }
        }
        return arr;
    }
}
