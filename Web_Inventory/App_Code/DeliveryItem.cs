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
/// Summary description for DeliveryItem
/// </summary>
public class DeliveryItem
{
    public DeliveryItem()
    {
    }

    private string sessionName = "CtlDeliveryItem";
    string _error = "";
    private ControlTransportFlow _flow;
    public ControlTransportFlow FlowObj
    {
        get { if (_flow == null) _flow = new ControlTransportFlow(); return _flow; }
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
    public DataTable GetDeliveryItem(double ctrldelivery)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetDeliveryItem(ctrldelivery);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetDeliveryItemBlank()
    {
        return FlowObj.GetDeliveryItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteDeliveryItem(double LOID)
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
    public bool UpdateDeliveryItem(decimal LOID,decimal RANK, decimal REQUISITION, decimal BOXQTY, string INVCODE, string CONTACTNAME, string CNAME, string CADDRESS, string CTEL)
    {
        CtrlDeliveryItemData data = new CtrlDeliveryItemData();
        data.LOID = Convert.ToDouble(LOID);
        data.REQUISITION = Convert.ToDouble(REQUISITION);
        data.BOXQTY = Convert.ToDouble(BOXQTY);
        data.INVCODE = INVCODE.ToString();
        data.CONTACTNAME = CONTACTNAME.ToString();
        data.CNAME = CNAME.ToString();
        data.CADDRESS = CADDRESS.ToString();
        data.CTEL = (CTEL == null ? "" : CTEL.ToString());

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
                DataRow dRow = dRows[0];
                dRow["INVCODE"] = data.INVCODE;
                dRow["BOXQTY"] = data.BOXQTY;
                CtrlDeliveryItemData requisition = FlowObj.GetRequisition(data.REQUISITION, data.INVCODE);
                dRow["CONTACTNAME"] = data.CONTACTNAME;
                dRow["CNAME"] = data.CNAME;
                dRow["CADDRESS"] = data.CADDRESS;
                dRow["CTEL"] = data.CTEL;
                dRow["REQUISITION"] = requisition.REQUISITION;
                ReOrder(dt);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    private bool VerifyData(CtrlDeliveryItemData data)
    {
        bool ret = true;
        if (data.REQUISITION == 0)
        {
            ret = false;
            _error = "กรุณาระบุเลขที่ Invoice ";
        }
        else if (data.INVCODE == "")
        {
            ret = false;
            _error = "กรุณาตรวจสอบเลขที่ Invoice ";
        }
        else if (data.BOXQTY == 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวนกล่อง";
        }
        else
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                foreach (DataRow dRow in dt.Rows)
                {
                    if (Convert.ToDouble(dRow["REQUISITION"]) == data.REQUISITION && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                    {
                        _error = "เลขที่ Invoice นี้มีอยู่ในรายการแล้ว";
                        ret = false;
                        goto ex;
                    }
                }
            }
        ex: ;
        }
        return ret;
    }

    //0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
    public bool InsertRequisitionItem(CtrlDeliveryItemData data)
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
                dRow["INVCODE"] = data.INVCODE;
                dRow["BOXQTY"] = Convert.ToDouble(data.BOXQTY);
                CtrlDeliveryItemData requisition = FlowObj.GetRequisition(data.REQUISITION, data.INVCODE);
                dRow["REQUISITION"] = requisition.REQUISITION;
                dRow["CONTACTNAME"] = requisition.CONTACTNAME;
                dRow["CNAME"] = requisition.CNAME;
                dRow["CADDRESS"] = requisition.CADDRESS;
                dRow["CTEL"] = requisition.CTEL;

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
                CtrlDeliveryItemData data = new CtrlDeliveryItemData();
 
                data.BOXQTY = Convert.ToDouble(dRow["BOXQTY"]);
                data.LOID = Convert.ToDouble(dRow["LOID"]);
                data.CADDRESS = dRow["CADDRESS"].ToString();
                data.CNAME = dRow["CNAME"].ToString();
                data.CONTACTNAME = dRow["CONTACTNAME"].ToString();
                data.CTEL = dRow["CTEL"].ToString();
                data.REQUISITION = Convert.ToDouble(dRow["REQUISITION"]);

                arr.Add(data);
            }
        }
        return arr;
    }

}

