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

/// <summary>
/// Summary description for StockOutFGItem
/// </summary>
[System.ComponentModel.DataObject()]
public class StockOutFGItem
{
    public StockOutFGItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private string sessionName = "StockOutFGItem";
    string _error = "";
    private StockOutFGFlow _flow;
    public StockOutFGFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockOutFGFlow(); return _flow; }
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
            dRow["NO"] = i;
            i += 1;
        }
    }

    public void CopyItem(DataTable dt)
    {
        System.Web.HttpContext.Current.Session[sessionName] = dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetStockOutItem(double stockOut)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetStockOutItemList(stockOut);
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
    public bool DeleteStockOutItem(double No)
    {
        bool ret = true;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRow = dt.Select("NO = " + No.ToString());
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
    public bool UpdateStockOutItem(decimal NO, decimal REFLOID, string BARCODE, decimal PRODUCT, string LOTNO, decimal REMAINQTY, decimal QTY, decimal UNIT, decimal PRICE, string UNITNAME, decimal REQUISITION)
    {
        StockOutFGItemData data = new StockOutFGItemData();
        data.NO = Convert.ToDouble(NO);
        data.LOTNO = LOTNO;
        data.PRICE = Convert.ToDouble(PRICE);
        data.BARCODE = BARCODE;
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.REMAIN = Convert.ToDouble(REMAINQTY);
        data.QTY = Convert.ToDouble(QTY);
        data.REFLOID = Convert.ToDouble(REFLOID);
        data.UNIT = Convert.ToDouble(UNIT);
        data.UNITNAME = UNITNAME;
        data.REQUISITION = Convert.ToDouble(REQUISITION);
        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("NO = " + data.NO);
                DataRow dRow = dRows[0];
                dRow["REFLOID"] = data.REFLOID;
                dRow["BARCODE"] = data.BARCODE;
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["LOTNO"] = data.LOTNO;
                dRow["REMAINQTY"] = data.REMAIN;
                dRow["QTY"] = data.QTY;
                dRow["UNIT"] = data.UNIT;
                dRow["PRICE"] = data.PRICE;
                dRow["UNITNAME"] = data.UNITNAME;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(StockOutFGItemData data)
    {
        bool ret = true;
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        else if (data.LOTNO == "")
        {
            ret = false;
            _error = "กรุณาเลือก Lot";
        }
        else if (data.QTY == 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวน";
        }

        else
        {
            if (data.QTY > 0)
            {
                double STOCK = FlowObj.GetQTYStock(data.LOTNO, data.PRODUCT);
                if (data.QTY > STOCK)
                {
                    _error = "จำนวนสินค้าที่เบิกต้องไม่เกินจำนวนที่มีใน Stock";
                    ret = false;
                }
            }

            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    if (Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT && Convert.ToDouble(dRow["NO"]) != data.NO)
                    {
                        _error = "รายการสินค้านี้มีอยู่ในรายการแล้ว";
                        ret = false;
                        goto ex;
                    }
                }

                if (FlowObj.GetReqItemProductData(data.REQUISITION, data.PRODUCT))
                {
                    StockOutFGItemData itemData = FlowObj.ReqItemProductData;
                    if (data.QTY > itemData.QTY)
                    {
                        _error = "จำนวนสินค้าที่เบิกต้อง ไม่เกิน จำนวนที่ขอ";
                        ret = false;
                    }
                }
            }

        ex: ;
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertStockOutItem(StockOutFGItemData data)
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
                dRow["NO"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["REFLOID"] = data.REFLOID;
                dRow["BARCODE"] = data.BARCODE;
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["LOTNO"] = data.LOTNO;
                dRow["REMAINQTY"] = data.REMAIN;
                dRow["QTY"] = data.QTY;
                dRow["UNIT"] = data.UNIT;
                dRow["PRICE"] = data.PRICE;
                dRow["UNITNAME"] = data.UNITNAME;
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
                StockOutItemData data = new StockOutItemData();
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.LOTNO = dRow["LOTNO"].ToString();
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]); ;
                data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                data.REFTABLE = "REQUISITIONITEM";
                data.STATUS = Constz.Requisition.Status.Waiting.Code;
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.REMAIN = Convert.ToDouble(dRow["REMAINQTY"]);
                arr.Add(data);
            }
        }
        return arr;
    }

    public int CheckSave(DataTable dtItem)
    {
        int iSaveType = 0; // 0 : เท่ากัน , 1 :Qty > allQty, 2 : Qty < allQty
        DataTable dtStockItem = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        for (int i = 0; i < dtStockItem.Rows.Count; i++)
        {
            double product = Convert.ToDouble(dtStockItem.Rows[i]["PRODUCT"]);
            double Qty = 0;
            DataRow[] dr = dtStockItem.Select("PRODUCT = " + product.ToString());
            for (int j = 0; j < dr.Length; j++)
            {
                Qty = Qty + Convert.ToDouble(dr[j]["QTY"]);
            }

            DataRow[] drItem = dtItem.Select("PRODUCT = " + product.ToString());
            double allQty = Convert.ToDouble(drItem[0]["QTY"]);

            if (Qty > allQty)
            {
                iSaveType = 1;
            }
            else if (Qty < allQty)
            {
                iSaveType = 2;
            }
        }

        return iSaveType;
    }
}
