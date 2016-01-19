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
using ABB.Data.Inventory.FG;
using ABB.Flow.Common;
using ABB.Flow.Inventory.FG;
using ABB.Data.Sales;

/// <summary>
/// Summary description for RequisitionItem
/// </summary>
[System.ComponentModel.DataObject()]
public class StockOutItem
{
    public StockOutItem()
    {
    }

    private string sessionName = "stockoutitem";
    string _error = "";
    private StockoutFlow _flow;
    public StockoutFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockoutFlow(); return _flow; }
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
    public DataTable GetStockOutItem(string stockout, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetStockOutItem(stockout);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetStockOutItemBlank()
    {
        return FlowObj.GetStockOutItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteStockOutItem(double LOID)
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
    public bool UpdateStockOutItem(decimal LOID, decimal PRODUCT, decimal QTY, decimal UNIT, decimal PRICE, string LOTNO, decimal DISCOUNT, decimal NETPRICE, string BARCODE, string ISVAT, decimal RANK)
    {
        RequisitionItemData data = new RequisitionItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.DISCOUNT = Convert.ToDouble(DISCOUNT);
        data.NETPRICE = Convert.ToDouble(NETPRICE);
        data.PRICE = Convert.ToDouble(PRICE);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.QTY = Convert.ToDouble(QTY);
        data.UNIT = Convert.ToDouble(UNIT);
        data.LOTNO = LOTNO;

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["QTY"] = data.QTY;
                dRow["UNIT"] = data.UNIT;
                dRow["LOTNO"] = data.LOTNO;
                ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
                data.PRICE = product.PRICE;
                dRow["PRICE"] = data.PRICE;
                dRow["DISCOUNT"] = data.DISCOUNT;
                dRow["NETPRICE"] = Convert.ToDouble((data.QTY * data.PRICE) - data.DISCOUNT);
                dRow["BARCODE"] = product.BARCODE;
           //     dRow["UNITNAME"] = FlowObj.GetUnitData(data.UNIT).NAME;
                dRow["ISVAT"] = product.ISVAT;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(RequisitionItemData data)
    {
        bool ret = true;
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        else if (data.QTY== 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวน";
        }
        else if (data.LOTNO == "")
        {
            ret = false;
            _error = "กรุณาระบุ Lot";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT && Convert.ToDouble(dRow["LOID"]) != data.LOID && dRow["LOTNO"].ToString() == data.LOTNO)
                {
                    _error = "รายการสินค้าใน Lot นี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    goto ex;
                }
            }
        ex: ;
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertStockOutItem(RequisitionItemData data)
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
                dRow["LOTNO"] = data.LOTNO;
                ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
                data.PRICE = product.PRICE;
                dRow["QTY"] = Convert.ToDouble(data.QTY);
                dRow["UNIT"] = Convert.ToDouble(data.UNIT);
                dRow["PRICE"] = Convert.ToDouble(data.PRICE);
                dRow["DISCOUNT"] = Convert.ToDouble(data.DISCOUNT);
                dRow["NETPRICE"] = Convert.ToDouble((data.QTY * data.PRICE) - data.DISCOUNT);
                dRow["ACTIVE"] = Constz.ActiveStatus.Active;
                dRow["BARCODE"] = product.BARCODE;
           //     dRow["UNITNAME"] = FlowObj.GetUnitData(data.UNIT).NAME;
                dRow["ISVAT"] = product.ISVAT;
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
                RequisitionItemData data = new RequisitionItemData();
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.LOTNO = dRow["LOTNO"].ToString();
                arr.Add(data);
            }
        }
        return arr;
    }

}
