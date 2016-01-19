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
using ABB.Data.Production;
using ABB.Flow.Common;
using ABB.Flow.Production;

/// <summary>
/// Summary description for BomItem
/// </summary>
[System.ComponentModel.DataObject()]
public class BomItem
{
    public BomItem()
    {
    }

    private string sessionName = "bomitem";
    private BomFlow _flow;
    private string _error = "";

    private BomFlow FlowObj
    {
        get { if (_flow == null) _flow = new BomFlow(); return _flow; }
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
            i += 1;
        }
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetBomItem(double productBarcode)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetBomList(productBarcode);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetBomItemBlank()
    {
        return FlowObj.GetBomListBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteBomItem(double RANK)
    {
        bool ret = true;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRow = dt.Select("RANK = " + RANK.ToString());
            dt.Rows.Remove(dRow[0]);
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        else
            ret = false;
        return ret;
    }

    private bool VerifyData(BomData data)
    {
        bool ret = true;
        if (data.MATERIAL == 0)
        {
            ret = false;
            _error = "กรุณาเลือกวัตถุดิบ";
        }
        else if (data.MASTER == 0)
        {
            ret = false;
            _error = "กรุณาระบุปริมาณการใช้";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    if (Convert.ToDouble(dRow["LOID"]) == data.MATERIAL && Convert.ToDouble(dRow["RANK"]) != data.LOID)
                    {
                        ret = false;
                        _error = "วัตถุดิบซ้ำ";
                        break;
                    }
                }
            }
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateBomItem(decimal RANK, string BARCODE, string NAME, decimal MASTER, string PRODUCTTYPE, string UNITNAME, decimal UNIT, decimal LOID)
    {
        BomData data = new BomData();
        data.LOID = Convert.ToDouble(RANK);
        data.CODE = BARCODE;
        data.MASTER = Convert.ToDouble(MASTER);
        data.MATERIAL = Convert.ToDouble(LOID);
        data.UNIT = Convert.ToDouble(UNIT);

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("RANK = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["BARCODE"] = data.CODE;
                dRow["NAME"] = NAME;
                dRow["MASTER"] = data.MASTER;
                dRow["PRODUCTTYPE"] = PRODUCTTYPE;
                dRow["UNITNAME"] = UNITNAME;
                dRow["UNIT"] = data.UNIT;
                dRow["LOID"] = Convert.ToDouble(data.MATERIAL);
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertBomItem(string BARCODE, string NAME, double MASTER, string PRODUCTTYPE, string UNITNAME, double UNIT, double MATERIAL)
    {
        bool ret = true;
        BomData data = new BomData();
        data.CODE = BARCODE;
        data.MASTER = Convert.ToDouble(MASTER);
        data.MATERIAL = Convert.ToDouble(MATERIAL);
        data.UNIT = Convert.ToDouble(UNIT);

        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                ReOrder(dt);
                DataRow dRow = dt.NewRow();
                dRow["RANK"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["BARCODE"] = data.CODE;
                dRow["NAME"] = NAME;
                dRow["MASTER"] = data.MASTER;
                dRow["PRODUCTTYPE"] = PRODUCTTYPE;
                dRow["UNITNAME"] = UNITNAME;
                dRow["UNIT"] = data.UNIT;
                dRow["LOID"] = Convert.ToDouble(data.MATERIAL);
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
                BomData data = new BomData();
                data.MASTER = Convert.ToDouble(dRow["MASTER"]);
                data.MATERIAL = Convert.ToDouble(dRow["LOID"]);
                data.UNIT = Convert.ToDouble(dRow["UNIT"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}
