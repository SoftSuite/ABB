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
/// Summary description for ReturnProductItem
/// </summary>
public class ReturnProductItem
{
    public ReturnProductItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    private string sessionName = "ReturnproductItem";
    string _error = "";
    private ReturnProductFlow _flow;
    public ReturnProductFlow FlowObj
    {
        get { if (_flow == null) _flow = new ReturnProductFlow(); return _flow; }
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
    public DataTable GetRequisitionItem(double requisition, string status)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetRequisitionItem(requisition);
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
    public bool UpdateRequisitionItem(decimal LOID, decimal PRODUCT, decimal PDQTY, decimal QTY, string UNITNAME, decimal PRICE, decimal DISCOUNT, decimal NETPRICE, string BARCODE, decimal RANK)
    {
        RequisitionItemData data = new RequisitionItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.DISCOUNT = Convert.ToDouble(DISCOUNT);
        data.NETPRICE = Convert.ToDouble(NETPRICE);
        data.PRICE = Convert.ToDouble(PRICE);
        data.PRODUCT = Convert.ToDouble(PRODUCT);
        data.PDQTY = Convert.ToDouble(PDQTY);
        data.QTY = Convert.ToDouble(QTY);
        data.UnitName = Convert.ToString(UNITNAME);

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
                dRow["PDQTY"] = data.PDQTY;
                dRow["UNITNAME"] = data.UnitName;
                ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
                data.PRICE = product.PRICE;
                dRow["PRICE"] = data.PRICE;
                dRow["DISCOUNT"] = data.DISCOUNT;
                dRow["NETPRICE"] = Convert.ToDouble((data.QTY * data.PRICE) - data.DISCOUNT);
                dRow["BARCODE"] = product.BARCODE;
                dRow["UNITNAME"] = FlowObj.GetUnitData(data.UNIT).NAME;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else
            throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(RequisitionItemData data)
    {
        bool ret = true;
        if (data.PRODUCT == 0)
        {
            ret = false;
            _error = "��س����͡�Թ���";
        }
        else if (data.QTY == 0)
        {
            ret = false;
            _error = "��س��кبӹǹ";
        }
        else if (data.QTY > data.PDQTY)
        {
            ret = false;
            _error = "�ӹǹ��ͧ���¡��Ҩӹǹ�����";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToDouble(dRow["PRODUCT"]) == data.PRODUCT && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    _error = "��¡���Թ��ҹ�����������¡������";
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
                ReOrder(dt);
                DataRow dRow = dt.NewRow();
                dRow["LOID"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["RANK"] = Convert.ToDouble(dRow["LOID"]);
                dRow["PRODUCT"] = Convert.ToDouble(data.PRODUCT);
                ProductSearchData product = FlowObj.GetProductData(data.PRODUCT);
                data.PRICE = product.PRICE;
                dRow["QTY"] = Convert.ToDouble(data.QTY);
                dRow["PDQTY"] = Convert.ToDouble(data.PDQTY);
                dRow["UNITNAME"] = data.UnitName;
                dRow["PRICE"] = Convert.ToDouble(data.PRICE);
                dRow["DISCOUNT"] = Convert.ToDouble(data.DISCOUNT);
                dRow["NETPRICE"] = Convert.ToDouble((data.QTY * data.PRICE) - data.DISCOUNT);
                dRow["ACTIVE"] = Constz.ActiveStatus.Active;
                dRow["BARCODE"] = product.BARCODE;
                dRow["UNIT"] = Convert.ToDouble(data.UNIT);
                //dRow["UNITNAME"] = FlowObj.GetUnitData(data.UNIT).NAME;
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
                data.PDQTY = Convert.ToDouble(dRow["PDQTY"]);
                data.UnitName = Convert.ToString(dRow["UNITNAME"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}

