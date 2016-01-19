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
/// Summary description for PlanOrderSale
/// </summary>
[System.ComponentModel.DataObject()]
public class PlanOrderSale
{
    public PlanOrderSale() { }

    private string sessionName = "planordersale";
    string _error = "";
    private PlanOrderSaleFlow _flow;

    private PlanOrderSaleFlow FlowObj
    {
        get { if (_flow == null) _flow = new PlanOrderSaleFlow(); return _flow; }
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
    public DataTable GetPlanOrderSale(double plan, int month, double product)
    {
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt == null)
        {
            dt = FlowObj.GetPLanOrderSaleList(plan, product, month);
            System.Web.HttpContext.Current.Session[sessionName] = dt;
        }
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetPlanOrderSaleBlank()
    {
        return FlowObj.GetPLanOrderSaleListBlank();
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Delete, true)]
    public bool DeletePlanOrderSale(double RANK)
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

    private bool VerifyData(PlanOrderSaleData data)
    {
        bool ret = true;
        if (data.SALEMAN == 0)
        {
            ret = false;
            _error = "กรุณาเลือกผู้ขาย";
        }
        else if (data.QTY == 0)
        {
            ret = false;
            _error = "กรุณาระบุจำนวน";
        }
        else if (IsDuplicate(data))
        {
            ret = false;
            _error = "ผู้ขายซ้ำ";
        }
        return ret;
    }

    private bool IsDuplicate(PlanOrderSaleData data)
    {
        bool ret = false;
        DataTable dt = (DataTable)System.Web.HttpContext.Current.Session[sessionName];
        if (dt != null)
        {
            DataRow[] dRow = dt.Select("SALEMAN = " + data.SALEMAN.ToString() + " AND RANK <> " + data.LOID.ToString());
            if (dRow != null)
            {
                ret = (dRow.Length > 0);
            }
        }
        return ret;
    }

    public bool InsertPlanOrderSale(PlanOrderSaleData data)
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
                dRow["RANK"] = Convert.ToDouble(dt.Rows.Count) + 1;
                dRow["SALEMAN"] = Convert.ToDouble(data.SALEMAN);
                dRow["QTY"] = Convert.ToDouble(data.QTY);
                dRow["SALENAME"] = data.SALENAME;
                dt.Rows.Add(dRow);
                System.Web.HttpContext.Current.Session[sessionName] = dt;
            }
        }
        return ret;
    }

}
