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
using ABB.Data.Inventory.FG;
using ABB.Flow.Inventory.FG;
/// <summary>
/// Summary description for StockinReturnItem
/// </summary>
[System.ComponentModel.DataObject()]
public class StockinReturnItem
{
    public StockinReturnItem()
    {
    }

    private string sessionName = "StockinReturnItem";
    string _error = "";
    private StockinReturnFlow _flow;
    public StockinReturnFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockinReturnFlow(); return _flow; }
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

    public bool DeleteItemAll()
    {
        bool ret = true;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            dt.Rows.Clear();
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        else
            ret = false;
        return ret;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetItem(double LOID, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetItem(LOID);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetItemBlank()
    {
        return FlowObj.GetItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteItem(double LOID)
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

    //0 LOID, 0 PRODUCT, '' BARCODE, '' PDNAME, '' LOTNO, 0 QTY, 0 UNIT, '' UNITNAME, 0 REFLOID, '' REFTABLE, '' STATUS, 0 PRICE, 0 NETPRICE
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateItem(decimal LOID, decimal PRODUCT, string BARCODE, string PDNAME, string LOTNO, decimal QTY, decimal QTYLOST, decimal UNIT, string UNITNAME, decimal REFLOID, string REFTABLE, string STATUS, decimal PRICE, decimal NETPRICE, decimal OLDQTY, decimal RANK)
    {
        StockinReturnItemData data = new StockinReturnItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.LOTNO = LOTNO;
        data.QTY = Convert.ToDouble(QTY);
        data.QTYLOST = Convert.ToDouble(QTYLOST);
        data.OLDQTY = Convert.ToDouble(OLDQTY);

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["PRODUCT"] = PRODUCT;
                dRow["BARCODE"] = BARCODE;
                dRow["PDNAME"] = PDNAME;
                dRow["LOTNO"] = LOTNO;
                dRow["QTY"] = QTY;
                dRow["QTYLOST"] = QTYLOST;
                dRow["UNIT"] = UNIT;
                dRow["UNITNAME"] = UNITNAME;
                dRow["PRICE"] = PRICE;
                dRow["NETPRICE"] = NETPRICE;
                dRow["OLDQTY"] = OLDQTY;
                dRow["REFLOID"] = REFLOID;
                dRow["REFTABLE"] = REFTABLE;
                dRow["STATUS"] = STATUS;

                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(StockinReturnItemData data)
    {
        bool ret = true;
        DataTable dt;
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        //else if (data.QTY == 0)
        //{
        //    ret = false;
        //    _error = "กรุณาระบุจำนวนดี";
        //}
        //else if (data.QTYLOST == 0)
        //{
        //    ret = false;
        //    _error = "กรุณาระบุจำนวนเสีย";
        //}
        else if (data.LOTNO == "")
        {
            ret = false;
            _error = "กรุณาระบุ Lot No";
        }
        else if (data.OLDQTY < data.QTYLOST)
        {
            ret = false;
            _error = "จำนวนเสียเกินจำนวนสินค้าที่มีอยู่";
        }
        else if (data.OLDQTY < (data.QTY + data.QTYLOST))
        {
            ret = false;
            _error = "จำนวนเกินจำนวนสินค้าที่มีอยู่";
        }
        else
        {
            dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if ((Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT) && (dRow["LOTNO"].ToString() == data.LOTNO) && (Convert.ToDouble(dRow["LOID"]) != data.LOID))
                {
                    _error = "รายการสินค้านี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    break;
                }
                //else if ((Convert.ToDouble(dRow["QTY"]) < (data.QTY + data.QTYLOST)))
                //{
                //    _error = "จำนวนเกินจำนวนสินค้าที่มีอยู่";
                //    ret = false;
                //    break;
                //}
            }
        }

        return ret;
    }

    public bool InsertItem(StockinReturnItemData data)
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
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["BARCODE"] = data.BARCODE;
                dRow["PDNAME"] = data.PDNAME;
                dRow["LOTNO"] = data.LOTNO;
                dRow["QTY"] = data.QTY;
                dRow["QTYLOST"] = data.QTYLOST;
                dRow["UNIT"] = data.UNIT;
                dRow["UNITNAME"] = data.UNITNAME;
                dRow["PRICE"] = data.PRICE;
                dRow["NETPRICE"] = data.PRICE * (data.QTY + data.QTYLOST);
                dRow["OLDQTY"] = data.OLDQTY;
                dRow["REFLOID"] = data.REFLOID;
                dRow["REFTABLE"] = data.REFTABLE;
                dRow["STATUS"] = data.STATUS;

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
                StockinReturnItemData data = new StockinReturnItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.LOTNO = dRow["LOTNO"].ToString();
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.QTYLOST = Convert.ToDouble(dRow["QTYLOST"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.OLDQTY = Convert.ToDouble(dRow["OLDQTY"]);
                data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                data.REFTABLE = dRow["REFTABLE"].ToString();
                data.STATUS = dRow["STATUS"].ToString();
                arr.Add(data);
            }
        }
        return arr;
    }
}
