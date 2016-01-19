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
using ABB.Data.Sales;
using ABB.Flow.Common;
using ABB.Flow.Sales;

/// <summary>
/// Summary description for DiscountStep
/// </summary>
[System.ComponentModel.DataObject()]
public class DiscountStep
{
    public DiscountStep()
    {
    }

    private string sessionName = "discountstepitem";
    string _error = "";
    private MemberTypeFlow _flow;

    private MemberTypeFlow FlowObj
    {
        get { if (_flow == null) _flow = new MemberTypeFlow(); return _flow; }
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
    public DataTable GetDiscountStep(double memberType)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetDiscountStepItem(memberType);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetDiscountStepItem()
    {
        return FlowObj.GetDiscountStepItemBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeleteDiscountStep(double LOID)
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

    private bool VerifyData(DiscountStepData data)
    {
        bool ret = true;
        //if (data.LOWERPRICE == 0)
        //{
        //    ret = false;
        //    _error = "กรุณาระบุราคาขั้นต่ำ";
        //}
        if (data.DISCOUNT == 0)
        {
            ret = false;
            _error = "กรุณาระบุส่วนลด";
        }
        else if(IsDuplicate(data))
        {
            ret = false;
            _error = "ราคาขั้นต่ำที่ระบุซ้ำกับราคาที่มีอยู่";
        }
        return ret;
    }

    private bool IsDuplicate(DiscountStepData data)
    {
        bool ret = false;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            foreach (DataRow dRow in dt.Rows)
            {
                if (Convert.ToDouble(dRow["LOWERPRICE"]) == data.LOWERPRICE && Convert.ToDouble(dRow["LOID"]) != data.LOID)
                {
                    ret = true;
                    break;
                }
            }
        }
        return ret;
    }
    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateDiscountStep(decimal LOID, decimal LOWERPRICE, decimal DISCOUNT)
    {
        DiscountStepData data = new DiscountStepData();
        data.LOID = Convert.ToDouble(LOID);
        data.DISCOUNT = Convert.ToDouble(DISCOUNT);
        data.LOWERPRICE = Convert.ToDouble(LOWERPRICE);

        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            DataRow[] dRows = dt.Select("LOID = " + data.LOID.ToString());
            DataRow dRow = dRows[0];
            dRow["LOWERPRICE"] = data.LOWERPRICE;
            dRow["DISCOUNT"] = data.DISCOUNT;
            ReOrder(dt);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        else throw new ApplicationException(_error);
        return ret;
    }

    public bool InsertDiscountStep(DiscountStepData data)
    {
        bool ret = true;
        ret = VerifyData(data);
        if (ret)
        {
            DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
            if (dt != null)
            {
                DataRow dRow = dt.NewRow();
                dRow["LOID"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["LOWERPRICE"] = data.LOWERPRICE;
                dRow["DISCOUNT"] = data.DISCOUNT;
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
                DiscountStepData data = new DiscountStepData();
                data.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                data.LOWERPRICE = Convert.ToDouble(dRow["LOWERPRICE"]);
                arr.Add(data);
            }
        }
        return arr;
    }

}
