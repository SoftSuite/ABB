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
using ABB.Data.Purchase;
using ABB.Data.Sales;
using ABB.Flow.Purchase;
using ABB.Flow.Common;

/// <summary>
/// Summary description for EditPOItem
/// </summary>
public class EditPOItem
{
    public EditPOItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string sessionName = "poItem";
    string _error = "";
    private PurchaseOrderFlow _flow;
    public PurchaseOrderFlow FlowObj
    {
        get { if (_flow == null) _flow = new PurchaseOrderFlow(); return _flow; }
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
    //        dRow["LOID"] = i;
            i += 1;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPOItem(double PDOrder, string refpoitem, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            if(refpoitem == "OLDPO")
            dt = FlowObj.GetPOItemPopup(PDOrder);
            else
            dt = FlowObj.GetPOItem(PDOrder);

            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPOItemBlank()
    {
        return FlowObj.GetPOItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePOItem(double LOID)
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

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, NULL DUEDATE, 0 PRITEM, '' BARCODE, '' UNITNAME , '' CODE, 0 NETPRICE, '' ISVAT
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdatePOItem(decimal LOID, decimal PRODUCT, decimal QTY, decimal RECEIVEQTY, decimal UNIT, decimal PRICE, decimal DISCOUNT, decimal NETPRICE, DateTime DUEDATE, decimal PRITEM, string BARCODE, string ISVAT, string PRODUCTNAME, string UNITNAME, string CODE, decimal RANK)
    {
        POItemData data = new POItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.QTY = Convert.ToDouble(QTY);
        data.RECEIVEQTY = Convert.ToDouble(RECEIVEQTY);
        data.PRICE = Convert.ToDouble(PRICE);
        data.DISCOUNT = Convert.ToDouble(DISCOUNT);
        data.DUEDATE = DUEDATE;

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                //  dRow["BARCODE"] = BARCODE;
                dRow["PRODUCTNAME"] = PRODUCTNAME;
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["QTY"] = data.QTY;
                dRow["RECEIVEQTY"] = data.RECEIVEQTY;
                dRow["UNITNAME"] = UNITNAME;
                dRow["UNIT"] = UNIT;
                dRow["PRICE"] = data.PRICE;
                dRow["DISCOUNT"] = data.DISCOUNT;
                dRow["NETPRICE"] = (data.QTY * data.PRICE) - data.DISCOUNT;
                dRow["DUEDATE"] = data.DUEDATE;
                dRow["CODE"] = CODE;
                dRow["PRITEM"] = PRITEM;
       //         dRow["ISVAT"] = ISVAT;

                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(POItemData data)
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
        else if (data.PRICE == 0)
        {
            ret = false;
            _error = "กรุณาระบุราคา/หน่วย";
        }
        else if (data.DUEDATE.Year == 1)
        {
            ret = false;
            _error = "กรุณาระบุกำหนดส่ง";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if ((Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT) && (Convert.ToDouble(dRow["PRITEM"]) == data.PRITEM) && (Convert.ToDouble(dRow["LOID"]) != data.LOID))
                {
                    _error = "รายการสินค้านี้มีอยู่ในรายการแล้ว";
                    ret = false;
                    break;
                }
            }
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, NULL DUEDATE, 0 PRITEM, '' BARCODE, '' UNITNAME , '' CODE, 0 NETPRICE, '' ISVAT
    public bool InsertPOItem(POItemData data)
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
                //    dRow["BARCODE"] = data.BARCODE;
                dRow["PRODUCTNAME"] = data.PRODUCTNAME;
                dRow["PRODUCT"] = data.PRODUCT;
                dRow["QTY"] = data.QTY;
                dRow["RECEIVEQTY"] = data.QTY;
                dRow["UNITNAME"] = data.UNITNAME;
                dRow["UNIT"] = data.UNIT;
                dRow["PRICE"] = data.PRICE;
                dRow["DISCOUNT"] = data.DISCOUNT;
                dRow["NETPRICE"] = data.NETPRICE;
                dRow["DUEDATE"] = data.DUEDATE;
                dRow["CODE"] = data.PRITEMCODE;
                dRow["PRITEM"] = data.PRITEM;
             //   dRow["ISVAT"] = data.ISVAT;

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
                POItemData data = new POItemData();
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                data.QTY = Convert.ToDouble(dRow["QTY"]);
                data.RECEIVEQTY = Convert.ToDouble(dRow["RECEIVEQTY"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                data.PRICE = Convert.ToDouble(dRow["PRICE"]);
                data.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                data.DUEDATE = Convert.ToDateTime(dRow["DUEDATE"]);
                data.PRITEM = Convert.ToDouble(dRow["PRITEM"]);
                data.ACTIVE = Constz.ActiveStatus.Active;
                data.ISVAT = dRow["ISVAT"].ToString();
                arr.Add(data);
            }
        }
        return arr;
    }
}


