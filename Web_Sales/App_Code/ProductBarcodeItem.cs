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
using ABB.Global;
using ABB.Flow.Common;
using ABB.Flow.Sales;

/// <summary>
/// Summary description for ProductBarcodeItem
/// </summary>
[System.ComponentModel.DataObject()]
public class ProductBarcodeItem
{
    public ProductBarcodeItem()
    {
    }
    private string sessionName = "pbItem";
    string _error = "";
    private ProductMasterFlow _flow;
    public ProductMasterFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductMasterFlow(); return _flow; }
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
            dRow["LOID"] = i;
            i += 1;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetItem(double ProductMaster)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetItem(ProductMaster);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
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

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateItem(decimal LOID, string BARCODE, string ABBNAME, decimal MULTIPLY, string UNITMASTER, decimal UNIT, decimal COST, decimal PRICE, string ISVAT, string ISDISCOUNT, decimal PACKSIZE, decimal UNITPACK, decimal ACTIVE)
    {
        ProductBarcodeData data = new ProductBarcodeData();
        data.LOID = Convert.ToDouble(LOID);
        data.BARCODE = BARCODE;
        data.UNIT = Convert.ToDouble(UNIT);
        data.COST = Convert.ToDouble(COST);
        data.PRICE = Convert.ToDouble(PRICE);
        data.MULTIPLY = Convert.ToDouble(MULTIPLY);
        data.ACTIVE = Convert.ToDouble(ACTIVE);
        data.ABBNAME = ABBNAME;

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["BARCODE"] = data.BARCODE;
                dRow["ABBNAME"] = data.ABBNAME;
                dRow["MULTIPLY"] = data.MULTIPLY;
                dRow["UNITMASTER"] = UNITMASTER;
                dRow["UNIT"] = data.UNIT;
                dRow["COST"] = data.COST;
                dRow["PRICE"] = data.PRICE;
                dRow["ISVAT"] = ISVAT;
                dRow["ISDISCOUNT"] = ISDISCOUNT;
                dRow["ACTIVE"] = data.ACTIVE;
                dRow["PACKSIZE"] = Convert.ToDouble(PACKSIZE);
                dRow["UNITPACK"] = UNITPACK;

                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(ProductBarcodeData data)
    {
        bool ret = true;
        if (data.BARCODE == "")
        {
            ret = false;
            _error = "กรุณาระบุ Barcode";
        }
        else if (data.UNIT == 0)
        {
            ret = false;
            _error = "กรุณาเลือกหน่วย";
        }
        else if (data.MULTIPLY < 1)
        {
            ret = false;
            _error = "จำนวนต้องมากกว่า 1";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if ((dRow["BARCODE"].ToString() == data.BARCODE) && (Convert.ToDouble(dRow["LOID"]) != data.LOID))
                {
                    _error = "บาร์โค้ดนี้มีอยู่แล้ว";
                    ret = false;
                    break;
                }
                if ((dRow["ABBNAME"].ToString() == data.ABBNAME) && (Convert.ToDouble(dRow["LOID"]) != data.LOID))
                {
                    _error = "ชื่อย่อนี้มีอยู่แล้ว";
                    ret = false;
                    break;
                }
                if ((Convert.ToDouble(dRow["UNIT"]) == data.UNIT) && (Convert.ToDouble(dRow["LOID"]) != data.LOID))
                {
                    _error = "รายการสินค้าที่ใช้หน่วยนับนี้มีอยู่แล้ว";
                    ret = false;
                    break;
                }
            }
        }
        return ret;
    }

    public bool InsertItem(ProductBarcodeData data)
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
                dRow["BARCODE"] = data.BARCODE;
                dRow["ABBNAME"] = data.ABBNAME;
                dRow["MULTIPLY"] = data.MULTIPLY;
                dRow["UNITMASTER"] = data.UNITMASTER;
                dRow["UNIT"] = data.UNIT;
                dRow["COST"] = data.COST;
                dRow["PRICE"] = data.PRICE;
                dRow["ISVAT"] = data.ISVAT;
                dRow["ISDISCOUNT"] = data.ISDISCOUNT;
                dRow["PACKSIZE"] = data.PACKSIZE;
                dRow["ACTIVE"] = data.ACTIVE;
                dRow["UNITPACK"] = data.UNITPACK;

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
                ProductBarcodeData data = new ProductBarcodeData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.BARCODE = dRow["BARCODE"].ToString();
                data.ABBNAME = dRow["ABBNAME"].ToString();
                data.MULTIPLY = Convert.ToDouble(dRow["MULTIPLY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.ACTIVE = Convert.ToDouble(dRow["ACTIVE"]);
                data.COST = Convert.ToDouble(dRow["COST"]);
                data.UNITPACK = Convert.ToDouble(dRow["UNITPACK"]);
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.PACKSIZE = Convert.ToDouble(dRow["PACKSIZE"]);
                data.ISVAT = dRow["ISVAT"].ToString();
                data.ISDISCOUNT = dRow["ISDISCOUNT"].ToString();
                arr.Add(data);
            }
        }
        return arr;
    }
}
