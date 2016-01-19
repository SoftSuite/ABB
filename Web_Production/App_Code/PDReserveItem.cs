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
/// Summary description for PDReserveItem
/// </summary>
public class PDReserveItem
{
    public PDReserveItem()
    {
    }

    private string sessionName = "PDReserveItem";
    string _error = "";
    private ProductReserveFlow _flow;
    public ProductReserveFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductReserveFlow(); return _flow; }
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

    //[System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    //public DataTable GetPDItem(double PDReserve, string LotNO, string convertValue)
    //{
    //    double convertVal = 1;
    //    if (Convert.ToDouble(convertValue == "" ? "0" : convertValue) > 0)
    //        convertVal = Convert.ToDouble(convertValue);

    //    DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
    //    if (dt == null)
    //    {
    //        if (PDReserve == 0)
    //            dt = FlowObj.GetPDItem(LotNO, convertVal);
    //        else
    //            dt = FlowObj.GetPDItem(PDReserve);
    //        System.Web.HttpContext.Current.Session[sessionName] = dt;
    //    }
    //    return dt;
    //}

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPDItem(double PDReserve, string LotNO)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            if (PDReserve == 0)
                dt = FlowObj.GetPDItem(LotNO);
            else
                dt = FlowObj.GetPDItem(PDReserve);
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
    public bool UpdatePDItem(double LOID, string RWBARCODE, string RWNAME, string RWGROUPNAME, double MASTER, string UNAME, int RANK, double PDLOID, double UNIT)
    {
        PDReserveItemData data = new PDReserveItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.MASTER = Convert.ToDouble(MASTER);

        bool ret = true;
        ret = VerifyData(data);

        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("RANK = " + RANK.ToString());
                DataRow dRow = dRows[0];
                dRow["MASTER"] = Convert.ToDouble(MASTER);
                //dRow["RWBARCODE"] = RWBARCODE;
                //dRow["RWNAME"] = RWNAME;
                //dRow["RWGROUPNAME"] = RWGROUPNAME;
                //ProductSearchData product = FlowObj.GetProductData(Convert.ToDouble(PRODUCT));
                //dRow["BARCODE"] = product.BARCODE;
                //dRow["UNAME"] = FlowObj.GetUnitData(Convert.ToDouble(UNAME)).NAME;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(PDReserveItemData data)
    {
        bool ret = true;
        //if (data.RWNAME = "")
        //{
        //    ret = false;
        //    _error = "กรุณาเลือกวัตถุดิบ";
        //}
        if (data.MASTER == 0)
        {
            ret = false;
            _error = "กรุณาระบุปริมาณ";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (dRow["RWBARCODE"].ToString() == data.RWBARCODE && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    _error = "รายการวัตถุดิบนี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    break;
                }
            }
        }
        return ret;
    }

    ////0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 MINSTOCK, 0 MAXSTOCK, 0 STOCK, 0 OLDPRICE, 0 CURPRICE, 0 MINPRICE, 0 LAST3MON, 0 LASTYEAR, ''DUEDATE, '' BARCODE, '' UNITNAME
    //public bool InsertPDItem(PDReserveItemData data)
    //{
    //    bool ret = true;
    //    ret = VerifyData(data);
    //    if (ret)
    //    {
    //        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
    //        if (dt != null)
    //        {
    //            ReOrder(dt);
    //            DataRow dRow = dt.NewRow();
    //            dRow["LOID"] = Convert.ToDouble(dt.Rows.Count) + 1;
    //            dRow["RANK"] = Convert.ToDouble(dRow["LOID"]);
    //            dRow["RWBARCODE"] = Convert.ToDouble(dRow["RWBARCODE"]);
    //            dRow["RWNAME"] = Convert.ToDouble(dRow["RWNAME"]);
    //            dRow["RWGROUPNAME"] = Convert.ToDouble(dRow["RWGROUPNAME"]);
    //            //ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
    //            //dRow["BARCODE"] = product.BARCODE;
    //            //dRow["UNAME"] = FlowObj.GetUnitData(data.UNAME).NAME;
    //            dt.Rows.Add(dRow);
    //            System.Web.HttpContext.Current.Session[sessionName] = dt;
    //        }
    //    }
    //    return ret;
    //}

    public ArrayList GetItemList()
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        ArrayList arr = new ArrayList();
        if (dt != null)
        {
            foreach (DataRow dRow in dt.Rows)
            {
                PDReserveItemData data = new PDReserveItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.MASTER = Convert.ToDouble(dRow["MASTER"]);
                data.ACTIVE = Constz.ActiveStatus.Active;
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
                PDReserveItemData data = new PDReserveItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.MASTER = Convert.ToDouble(dRow["MASTER"]);
                //PRItemData RecentData = FlowObj.GetRecentPRItem(data.PRODUCT);
                data.ACTIVE = Constz.ActiveStatus.Active;

                arr.Add(data);
            }
        }
        return arr;
    }
}

