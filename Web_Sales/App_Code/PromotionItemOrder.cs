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
/// Summary description for PromotionItemOrder
/// </summary>
[System.ComponentModel.DataObject()]
public class PromotionItemOrder
{
    public PromotionItemOrder()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string sessionName = "PromotionItemOrder";
    string _error = "";
    private PromotionSaleFlow _flow;
    public PromotionSaleFlow FlowObj
    {
        get { if (_flow == null) _flow = new PromotionSaleFlow(); return _flow; }
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
    public DataTable GetPromotionSaleItem(double loid)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetPromotionItem(loid);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPromotionSaleItemBlank()
    {
        return FlowObj.GetPromotionItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePromotionSaleItem(double LOID)
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
    public bool UpdatePromotionSaleItem(decimal LOID, string BARCODE, string NAME, string UNAME, decimal PRICEOLD, decimal PRICENEW, decimal RANK)
    {
        PromotionSalesItemData data = new PromotionSalesItemData();
        data.LOID = Convert.ToDouble(LOID);
        //data.DISCOUNT = Convert.ToDouble(DISCOUNT);
        //data.NETPRICE = Convert.ToDouble(NETPRICE);
        //data.PRICE = Convert.ToDouble(PRICE);
        data.PRICENEW = Convert.ToDouble(PRICENEW);
        data.PRICEOLD = Convert.ToDouble(PRICEOLD);
        data.UNAME = UNAME;
        data.NAME = NAME;

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["NAME"] = data.NAME;
                dRow["UNAME"] = data.UNAME;
                PromotionSaleData Promotion = FlowObj.GetPromotionData(data.LOID);
                //data.PRICE = product.PRICE;
                //dRow["PRICE"] = data.PRICE;
                //dRow["DISCOUNT"] = data.DISCOUNT;
                //dRow["NETPRICE"] = Convert.ToDouble((data.QUANTITY * data.PRICE) - data.DISCOUNT);
                dRow["BARCODE"] = data.BARCODE;
                //dRow["UNIT"] = FlowObj.GetUnitData(data.UNIT).NAME;
                dRow["PRICENEW"] = data.PRICENEW;
                //dRow["ISVAT"] = product.ISVAT;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(PromotionSalesItemData data)
    {
        bool ret = true;
        if (data.NAME == "")
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        else if (data.UNAME == "")
        {
            ret = false;
            _error = "กรุณาระบุหน่วย";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToString(dRow["NAME"]) == data.NAME && Convert.ToDouble(dRow["LOID"]) != data.LOID)
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
    public bool InsertPromotionSalesItem(PromotionSalesItemData data)
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
                dRow["NAME"] = data.NAME;
                PromotionSaleData Promotion = FlowObj.GetPromotionData(data.LOID);
                //data.PRICE = product.PRICE;
                dRow["PRICENEW"] = data.PRICENEW;
                dRow["PRICEOLD"] = data.PRICEOLD;
                //dRow["PRICE"] = Convert.ToDouble(data.PRICE);
                //dRow["DISCOUNT"] = Convert.ToDouble(data.DISCOUNT);
                //dRow["NETPRICE"] = Convert.ToDouble((data.QUANTITY * data.PRICE) - data.DISCOUNT);
                //dRow["ACTIVE"] = Constz.ActiveStatus.Active;
                dRow["BARCODE"] = data.BARCODE;
                //dRow["UNIT"] = FlowObj.GetUnitData(data.UNIT).NAME;
                //dRow["ISVAT"] = product.ISVAT;
                dRow["UNAME"] = data.UNAME;
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
                PromotionSalesItemData data = new PromotionSalesItemData();
                //data.ACTIVE = Constz.ActiveStatus.Active;
                //data.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                //data.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                //data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.NAME = Convert.ToString(dRow["NAME"]);
                data.PRICENEW = Convert.ToDouble(dRow["PRICENEW"]);
                data.PRICEOLD = Convert.ToDouble(dRow["PRICEOLD"]);
                data.UNAME = Convert.ToString(dRow["UNAME"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}

