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
/// Summary description for StockInItem
/// </summary>
public class StockInItem
{
    public StockInItem()
    {
    }

    private string sessionName = "StockInItem";
    string _error = "";
    private StockInFlow _flow;
    public StockInFlow FlowObj
    {
        get { if (_flow == null) _flow = new StockInFlow(); return _flow; }
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
           // dRow["LOID"] = i;
            i += 1;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetStockInItem(double stockin, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetStockInItem(stockin);
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
    public bool UpdateStockInItem(decimal LOID, decimal PRODUCT, string LOTNO, decimal REFLOID, string CODE, decimal QCQTY, decimal PQTY, decimal UNIT, string BARCODE, string QCREMARK, string QCRESULT, decimal RANK, decimal SQTY, string REMARK)
    {
        StockInItemData data = new StockInItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.LOTNO = LOTNO;
        data.REFLOID = Convert.ToDouble(REFLOID);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.QCQTY = Convert.ToDouble(QCQTY);
        data.QTY = Convert.ToDouble(SQTY);
        data.PQTY = Convert.ToDouble(PQTY);
        data.REMARK = REMARK;

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["LOID"] = data.LOID;
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["LOTNO"] = data.LOTNO;
                dRow["QCQTY"] = data.QCQTY;
                dRow["SQTY"] = data.QTY;
                dRow["REFLOID"] = data.REFLOID;
                dRow["PRICE"] = data.PRICE;
                dRow["REMARK"] = data.REMARK;
                POItemData product = FlowObj.GetPOItemData(data.REFLOID);
                dRow["UNIT"] = product.UNIT;
                dRow["BARCODE"] = product.BARCODE;
                //dRow["UNITNAME"] = FlowObj.GetUnitData(product.UNIT).NAME;
                dRow["CODE"] = product.CODE;
                dRow["PQTY"] = product.QTY;

                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(StockInItemData data)
    {
        bool ret = true;
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกสินค้า";
        }
        if (data.LOTNO == "")
        {
            ret = false;
            _error = "กรุณาระบุ Lot No";
        }
        //else if (data.QCQTY == 0)
        //{
        //    ret = false;
        //    _error = "กรุณาระบุจำนวนส่งตรวจ QC";
        //}
        else if (data.QCQTY>data.QTY)
        {
            ret = false;
            _error = "จำนวนส่งตรวจ QC ต้องน้อยกว่าเท่ากับจำนวนที่รับ";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (dRow["LOTNO"].ToString() == data.LOTNO && Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    _error = "รายการสินค้าในเลขที่ใบสั่งซื้อนี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    goto ex;
                }
            }
        ex: ;
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertStokInItem(StockInItemData data)
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
                dRow["REFLOID"] = Convert.ToDouble(data.REFLOID);
                dRow["PRICE"] = Convert.ToDouble(data.PRICE);
                dRow["REMARK"] = data.REMARK;
                POItemData product = FlowObj.GetPOItemData(data.REFLOID);
                dRow["QCQTY"] = Convert.ToDouble(data.QCQTY);
                dRow["SQTY"] = Convert.ToDouble(data.QTY);
                dRow["UNIT"] = Convert.ToDouble(product.UNIT);
                // dRow["ACTIVE"] = Constz.ActiveStatus.Active;
                dRow["BARCODE"] = product.BARCODE;
                //dRow["UNITNAME"] = FlowObj.GetUnitData(product.UNIT).NAME;
                dRow["CODE"] = product.CODE;
                dRow["PQTY"] = product.QTY;

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
                StockInItemData data = new StockInItemData();
                //data.ACTIVE = Constz.ActiveStatus.Active;
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.LOTNO = dRow["LOTNO"].ToString();
                data.QCQTY = Convert.ToDouble(dRow["QCQTY"]);
                data.QTY = dRow["SQTY"] == DBNull.Value ? 0 : Convert.ToDouble(dRow["SQTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.REMARK = dRow["REMARK"].ToString();
                data.QCREMARK = dRow["QCREMARK"].ToString();
                data.QCRESULT = dRow["QCRESULT"].ToString();
                arr.Add(data);
            }
        }
        return arr;
    }

}

