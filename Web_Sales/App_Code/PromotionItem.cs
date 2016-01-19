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
/// Summary description for PromotionItem
/// </summary>
public class PromotionItem
{
    public PromotionItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string sessionName = "PromotionItem";
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

    public void SetAllProductList(DataTable dt)
    {
        System.Web.HttpContext.Current.Session[sessionName] = dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPromotionItem(double loid)
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
    public DataTable GetPromotionItemBlank()
    {
        return FlowObj.GetPromotionItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePromotionItem(double LOID)
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
    public bool UpdatePromotionItem(decimal LOID, string BARCODE, string NAME, string UNAME, decimal PRICEOLD, decimal PRICENEW, decimal PRODUCT, decimal RANK)
    {
        PromotionSalesItemData data = new PromotionSalesItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.BARCODE = Convert.ToString(BARCODE);
        data.NAME = Convert.ToString(NAME);
        data.UNAME = Convert.ToString(UNAME);
        data.PRICEOLD = Convert.ToDouble(PRICEOLD);
        data.PRICENEW = Convert.ToDouble(PRICENEW);
        data.PRODUCT = Convert.ToDouble(PRODUCT);

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
                PromotionSaleData product = FlowObj.GetPromotionData(data.LOID);
                //data.PRICE = product.PRICE;
                dRow["BARCODE"] = data.BARCODE;
                dRow["PRICENEW"] = data.PRICENEW;
                dRow["PRICEOLD"] = data.PRICEOLD;
                dRow["PRODUCT"] = data.PRODUCT;
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
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        //else if (data.PRICENEW == 0 || data.PRICENEW == data.PRICEOLD)
        //{
        //    ret = false;
        //    _error = "กรุณาระบุส่วนลด";
        //}
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
                dRow["PRICENEW"] = data.PRICENEW;
                dRow["PRICEOLD"] = data.PRICEOLD;
                dRow["BARCODE"] = data.BARCODE;
                dRow["UNAME"] = data.UNAME;
                dRow["BARCODE"] = data.BARCODE;
                dRow["PROMOTION"] = data.PROMOTION;
                dRow["PRODUCT"] = data.PRODUCT;
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
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.NAME = Convert.ToString(dRow["NAME"]);
                data.PRICENEW = Convert.ToDouble(dRow["PRICENEW"]);
                data.PRICEOLD = Convert.ToDouble(dRow["PRICEOLD"]);
                data.UNAME = Convert.ToString(dRow["UNAME"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.PROMOTION = Convert.ToDouble(dRow["PROMOTION"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}
