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
using ABB.Flow.Common;
using ABB.Flow.Sales;

/// <summary>
/// Summary description for RequisitionItem
/// </summary>
[System.ComponentModel.DataObject()]
public class InvoiceItem
{
    public InvoiceItem()
    {
    }

    private string sessionName = "requisitionItem";
    string _error = "";
    private double _totalVat = 0;
    private double _totalDiscount = 0;
    private double _total = 0;
    private double _grandTotal = 0;
    private double _discountPercent = 0;
    private InvoiceFlow _flow;
    public InvoiceFlow FlowObj
    {
        get { if (_flow == null) _flow = new InvoiceFlow(); return _flow; }
    }

    public double TOTALVAT
    {
        get { return _totalVat; }
    }

    public double TOTALDISCOUNT
    {
        get { return _totalDiscount; }
    }

    public double TOTAL
    {
        get { return _total; }
    }

    public double GRANDTOTAL
    {
        get { return _grandTotal; }
    }

    public double DISCOUNTPERCENT
    {
        get { return _discountPercent; }
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
    public DataTable GetRequisitionItem(double requisition, string status, string popup, string NewBind, string warehouse, string customer, string requisitiontype)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null || dt.Rows.Count == 0 || NewBind == "1")
        {
            if (popup == "0")
            {
                
            }
            else if (popup == null || popup == "")
            {
                dt = FlowObj.GetRequisitionItem(requisition,warehouse);
            }
            else
            {
                if (dt == null || dt.Rows.Count == 0)
                {
                    dt = FlowObj.GetProductItemList(popup, warehouse, customer, requisitiontype);
                }
                else
                {
                    DataTable dtTemp = FlowObj.GetProductItemList(popup, warehouse, customer, requisitiontype);
                    foreach (DataRow dRow in dtTemp.Rows)
                    {
                        dt.ImportRow(dRow);
                    }
                    //for (int i = 0; i < dtTemp.Rows.Count; i++)
                    //{
                    //    DataRow dr = dtTemp.Rows[i];
                    //    dt.ImportRow(dr);
                    //}
                    ReOrder(dt);
                }
            }

            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetRequisitionItemBlank()
    {
        return FlowObj.GetRequisitionItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteRequisitionItem(double LOID)
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
    public bool UpdateRequisitionItem(decimal LOID, decimal PRODUCT, decimal PDQTY, decimal QTY, decimal UNIT, decimal PRICE, decimal DISCOUNT, decimal NORMALDISCOUNT, decimal NETPRICE, string BARCODE, string ISVAT, decimal RANK, string PRODUCTNAME, string UNITNAME)
    {
        RequisitionItemData data = new RequisitionItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.DISCOUNT = Convert.ToDouble(NORMALDISCOUNT);
        data.NETPRICE = Convert.ToDouble(NETPRICE);
        data.PRICE = Convert.ToDouble(PRICE);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.QTY = Convert.ToDouble(QTY);
        data.UNIT = Convert.ToDouble(UNIT);
        data.BarCode = BARCODE;
        data.ISVAT = ISVAT;
        data.ProductName = PRODUCTNAME;
        data.UnitName = UNITNAME;

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
                dRow["PRICE"] = data.PRICE;
                dRow["NORMALDISCOUNT"] = data.DISCOUNT;
                dRow["DISCOUNT"] = data.DISCOUNT;
                dRow["NETPRICE"] = Convert.ToDouble(data.NETPRICE);
                dRow["BARCODE"] = data.BarCode;
                dRow["UNITNAME"] = data.UnitName;
                dRow["ISVAT"] = data.ISVAT;
                dRow["PRODUCTNAME"] = data.ProductName;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else
            throw new ApplicationException(_error);
        return ret;
    }

    public void UpdateItem(double PRODUCT, double QTY, double NETPRICE)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRows = dt.Select("PRODUCT = " + PRODUCT.ToString());
            DataRow dRow = dRows[0];
            dRow["QTY"] = QTY;
            dRow["NETPRICE"] = NETPRICE;
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
    }

    private bool VerifyData(RequisitionItemData data)
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
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    _error = "รายการสินค้านี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    goto ex;
                }
            }
        ex: ;
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertRequisitionItem(RequisitionItemData data)
    {
        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow dRow = dt.NewRow();
                dRow["LOID"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["RANK"] = Convert.ToDouble(dRow["LOID"]);
                dRow["PRODUCT"] = Convert.ToDouble(data.PRODUCT);
                dRow["QTY"] = Convert.ToDouble(data.QTY);
                dRow["UNIT"] = Convert.ToDouble(data.UNIT);
                dRow["PRICE"] = Convert.ToDouble(data.PRICE);
                dRow["NORMALDISCOUNT"] = Convert.ToDouble(data.DISCOUNT);
                dRow["DISCOUNT"] = Convert.ToDouble(data.DISCOUNT);
                dRow["NETPRICE"] = Convert.ToDouble(data.NETPRICE);
                //dRow["ACTIVE"] = Constz.ActiveStatus.Active;
                dRow["BARCODE"] = data.BarCode;
                dRow["UNITNAME"] = data.UnitName;
                dRow["ISVAT"] = data.ISVAT;
                dRow["PRODUCTNAME"] = data.ProductName;
                dt.Rows.Add(dRow);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        return ret;
    }

    public void CalculateDiscount(double customer, double vatPercent)
    {
        _discountPercent = 0;
        _total = 0;
        _totalVat = 0;
        _totalDiscount = 0;
        _grandTotal = 0;
        SaleFlow sFlow = new SaleFlow();
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            foreach (DataRow dRow in dt.Rows)
            {
                _totalDiscount += Convert.ToDouble(dRow["NORMALDISCOUNT"]) * Convert.ToDouble(dRow["QTY"]);
                _grandTotal += Convert.ToDouble(dRow["NETPRICE"]) - (Convert.ToDouble(dRow["NORMALDISCOUNT"]) * Convert.ToDouble(dRow["QTY"]));
            }
            _totalVat = sFlow.CalculateTotalVat(_grandTotal + _totalDiscount, vatPercent);
            _total = _grandTotal + _totalDiscount - _totalVat;

            if (sFlow.GetCustomerDiscount(customer, _total + _totalVat))
                _discountPercent = sFlow.DISCOUNT;
            if (_discountPercent > 0)
            {
                if (_totalDiscount < sFlow.CalcucateDiscount(_total + _totalVat, _discountPercent))
                {
                    _total = 0;
                    _totalVat = 0;
                    _totalDiscount = 0;
                    _grandTotal = 0;
                    foreach (DataRow dRow in dt.Rows)
                    {
                        dRow["DISCOUNT"] = sFlow.CalcucateDiscount(Convert.ToDouble(dRow["PRICE"]), _discountPercent);
                        dRow["NETPRICE"] = sFlow.CalcucateProductTotalItem(Convert.ToDouble(dRow["PRICE"]), Convert.ToDouble(dRow["QTY"]), 0);
                        _totalDiscount += Convert.ToDouble(dRow["DISCOUNT"]) * Convert.ToDouble(dRow["QTY"]);
                        _grandTotal += Convert.ToDouble(dRow["NETPRICE"]) - (Convert.ToDouble(dRow["DISCOUNT"]) * Convert.ToDouble(dRow["QTY"]));
                    }
                    System.Web.HttpContext.Current.Session[sessionName] = dt;
                    _totalVat = sFlow.CalculateTotalVat(_grandTotal + _totalDiscount, vatPercent);
                    _total = _grandTotal + _totalDiscount - _totalVat;
                }
            }
        }
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
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.NETPRICE = data.QTY * (data.PRICE - data.DISCOUNT);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}
