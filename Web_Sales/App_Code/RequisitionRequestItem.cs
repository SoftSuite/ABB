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
/// Summary description for RequisitionRequestItem
/// </summary>
[System.ComponentModel.DataObject()]
public class RequisitionRequestItem
{
    public RequisitionRequestItem()
    {
    }

    private string sessionName = "requisitionRequestItem";
    string _error = "";
    private ReturnRequestFlow _flow;
    private double _discount = 0;
    private double _correctValue = 0;

    public ReturnRequestFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnRequestFlow(); return _flow; }
    }

    public string ErrorMessage
    {
        get { return _error; }
    }

    public double DISCOUNT
    {
        get { return _discount; }
    }

    public double CORRECTVALUE
    {
        get { return _correctValue; }
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

    public void SetInvoiceItem(DataTable dt)
    {
        System.Web.HttpContext.Current.Session[sessionName] = dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetRequisitionItem(double requisition)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetRequestItemList(requisition);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
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

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateRequisitionItem(decimal LOID, decimal RANK, string BARCODE, decimal PRODUCT, string PRODUCTNAME, decimal QTY, decimal UNIT, string UNITNAME, 
        decimal PRICE, decimal NETPRICE, decimal DISCOUNT, decimal OLDQTY, decimal OLDDISCOUNT, decimal REFLOID)
    {
        if (Convert.ToDouble(QTY) <= 0)
            throw new ApplicationException("กรุณาระบุจำนวน");
        else if (Convert.ToDouble(QTY) > Convert.ToDouble(OLDQTY))
            throw new ApplicationException("จำนวนที่รับคืนต้องไม่เกิน " + OLDQTY.ToString(Constz.DblFormat));

        bool ret = true;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRows = dt.Select("LOID = " + LOID.ToString());
            DataRow dRow = dRows[0];
            dRow["QTY"] = Convert.ToDouble(QTY);
            dRow["NETPRICE"] = Convert.ToDouble(QTY) * (Convert.ToDouble(PRICE)-Convert.ToDouble(OLDDISCOUNT));

            System.Web.HttpContext.Current.Session[sessionName] = dt;
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
                data.DISCOUNT = Convert.ToDouble(dRow["OLDDISCOUNT"]);
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                data.REFTABLE = "REQUISITIONITEM";
                data.NETPRICE = data.QTY * (data.PRICE - data.DISCOUNT);
                arr.Add(data);
            }
        }
        return arr;
    }

    public void GetCorrectValue(double vatPercent)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            foreach (DataRow dRow in dt.Rows)
            {
                _discount += Convert.ToDouble(dRow["DISCOUNT"]);
                _correctValue += Convert.ToDouble(dRow["NETPRICE"]);
            }
        }
       // _correctValue = Math.Round((_correctValue * 100) / (100 + vatPercent), 2);
    }

}
