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
using ABB.Flow;
using ABB.Data.Sales;
using ABB.Flow.Common;
using ABB.Flow.Sales;

/// <summary>
/// Summary description for RequisitionItem
/// </summary>
[System.ComponentModel.DataObject()]
public class RequisitionItemReserve
{
    public RequisitionItemReserve()
    {
    }

    private string sessionName = "requisitionItem";
    string _error = "";
    private ProductReserveFlow _flow;
    private double _totalVat = 0;
    private double _totalDiscount = 0;
    private double _total = 0;
    private double _grandTotal = 0;
    private double _discountPercent = 0;

    private ProductReserveFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductReserveFlow(); return _flow; }
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
    public DataTable GetRequisitionItem(double requisition, double warehouse)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetRequisitionItem(requisition, warehouse);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    public void DeleteRequisitionItem(ArrayList arrLOID)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            for (int i = 0; i < arrLOID.Count; ++i)
            {
                DataRow[] dRow = dt.Select("LOID = " + arrLOID[i].ToString());
                dt.Rows.Remove(dRow[0]);
            }
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
    }

    public void InsertRequisitionItem(DataTable dtProduct)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            foreach (DataRow dRow in dtProduct.Rows)
            {
                DataRow dtRow = dt.NewRow();
                dtRow["RANK"] = dRow["RANK"];
                dtRow["LOID"] = dRow["LOID"];
                dtRow["PRODUCT"] = dRow["PRODUCT"];
                dtRow["UNIT"] = dRow["UNIT"];
                dtRow["BARCODE"] = dRow["BARCODE"];
                dtRow["PRODUCTNAME"] = dRow["PRODUCTNAME"];
                dtRow["UNITNAME"] = dRow["UNITNAME"];
                dtRow["PRICE"] = dRow["PRICE"];
                dtRow["QTY"] = dRow["QTY"];
                dtRow["NETPRICE"] = dRow["NETPRICE"];
                dtRow["STOCKQTY"] = dRow["STOCKQTY"];
                dtRow["NORMALDISCOUNT"] = dRow["NORMALDISCOUNT"];
                dtRow["DISCOUNT"] = dRow["DISCOUNT"];
                dtRow["ISVAT"] = dRow["ISVAT"];
                dtRow["ISDISCOUNT"] = dRow["ISDISCOUNT"];
                dt.Rows.Add(dtRow);
            }
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        else
        {
            ReOrder(dtProduct);
            System.Web.HttpContext.Current.Session[sessionName] = dtProduct;
        }
    }

    public void UpdateRequisition(double LOID, double QTY)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRow = dt.Select("LOID = " + LOID.ToString());
            dRow[0]["QTY"] = QTY;
            dRow[0]["NETPRICE"] = Convert.ToDouble(dRow[0]["PRICE"]) * QTY;
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
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
                _totalDiscount += Convert.ToDouble(dRow["NORMALDISCOUNT"])*Convert.ToDouble(dRow["QTY"]);
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
                data.NETPRICE = (data.PRICE - data.DISCOUNT) * data.QTY;
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.ISVAT = dRow["ISVAT"].ToString();
                if (data.QTY >0) arr.Add(data);
            }
        }
        return arr;
    }

}
