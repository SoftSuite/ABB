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
using ABB.Data.Inventory;
using ABB.Flow.Common;
using ABB.Flow.Inventory;

/// <summary>
/// Summary description for StockinProductItem
/// </summary>
public class StockinProductItem
{
    public StockinProductItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string sessionName = "StockinProductItem";
    string _error = "";
    private StockinProductionFlow _flow;
    public StockinProductionFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinProductionFlow(); return _flow; }
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
    public DataTable GetStockInItem(double requisition, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetStockInItem(requisition);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetStockInItemBlank()
    {
        return FlowObj.GetStockInItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteStockInItem(double LOID)
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

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateStockInItem(decimal LOID, decimal PDQTY, decimal QTY, DateTime MFGDATE, string UNITNAME, decimal UNIT, decimal PRODUCT, decimal PDPLOID, string PRODUCTNAME, string LOTNO, decimal RANK, decimal REFLOID)
    {
        StockinProductData data = new StockinProductData();
        data.LOID = Convert.ToDouble(LOID);
        data.LOTNO = Convert.ToString(LOTNO);
        data.MFGDATE = Convert.ToDateTime(MFGDATE);
        data.PRODUCTNAME = Convert.ToString(PRODUCTNAME);
        data.PDQTY = Convert.ToDouble(PDQTY);
        data.QTY = Convert.ToDouble(QTY);
        data.REFLOID = Convert.ToDouble(REFLOID);
        data.UNITNAME = Convert.ToString(UNITNAME);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.UNIT = Convert.ToDouble(UNIT);
        data.PDPRODUCT = Convert.ToDouble(PDPLOID);
        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["PRODUCTNAME"] = data.PRODUCTNAME;
               // dRow["PRODUCTNAME"] = FlowObj.GetUnitData(data.PRODUCT).NAME;
                dRow["QTY"] = data.QTY;
                dRow["PDQTY"] = data.PDQTY;
                dRow["LOTNO"] = data.LOTNO;
                dRow["REFLOID"] = data.REFLOID;
                dRow["MFGDATE"] = data.MFGDATE;
                dRow["UNITNAME"] = data.UNITNAME;
                dRow["UNITNAME"] = FlowObj.GetUnitData(data.UNIT).NAME;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else
            throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(StockinProductData data)
    {
        bool ret = true;
        if (data.LOTNO.Trim() == "")
        {
            ret = false;
            _error = "กรุณาเลือกเลขที่การผลิต";
        }
        else if (data.QTY == 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวนรับ";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToString(dRow["LOTNO"]) == data.LOTNO && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    _error = "เลขที่การผลิตนี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    goto ex;
                }
            }
        ex: ;
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertStokInItem(StockinProductData data)
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
                dRow["QTY"] = Convert.ToDouble(data.QTY);
                dRow["PDQTY"] = Convert.ToDouble(data.PDQTY);
                dRow["UNITNAME"] = data.UNITNAME;
                //dRow["ACTIVE"] = Constz.ActiveStatus.Active;
                dRow["UNIT"] = Convert.ToDouble(data.UNIT);
                dRow["PDPLOID"] = Convert.ToDouble(data.PDPRODUCT);
                dRow["PRODUCT"] = Convert.ToDouble(data.PRODUCT);
                dRow["PRODUCTNAME"] = data.PRODUCTNAME;
                dRow["MFGDATE"] = Convert.ToDateTime(data.MFGDATE);
                dRow["LOTNO"] = data.LOTNO;
                dRow["REFLOID"] = Convert.ToDouble(data.REFLOID);
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
                StockinProductData data = new StockinProductData();
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.LOTNO = Convert.ToString(dRow["LOTNO"]);
                data.MFGDATE = (Convert.IsDBNull(dRow["MFGDATE"]) ? new DateTime(): Convert.ToDateTime(dRow["MFGDATE"]));
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.PRODUCTNAME = Convert.ToString(dRow["PRODUCTNAME"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.PDQTY = (Convert.IsDBNull(dRow["PDQTY"]) ? new double() : Convert.ToDouble(dRow["PDQTY"]));
                data.UNITNAME = Convert.ToString(dRow["UNITNAME"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.PDPRODUCT = Convert.ToDouble(dRow["PDPLOID"]);
                data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}

