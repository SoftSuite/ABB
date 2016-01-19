using System;
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
using ABB.Global;

/// <summary>
/// Summary description for ProductStockItem
/// </summary>
public class ProductStockItem
{

    string _error = "";
    private ProductStockAdjustFlow _flow;

    private ProductStockAdjustFlow FlowObj
    {
        get { if (_flow == null) _flow = new ProductStockAdjustFlow(); return _flow; }
    }

    public string ErrorMessage
    {
        get { return _error; }
    }

    public ProductStockItem()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
    public DataTable GetStockItem(double warehouse)
    {
        double zone = 0;
        if (warehouse == 1)
            zone = Constz.Zone.Z01;
        else if (warehouse == 2)
            zone = Constz.Zone.Z04;
        else
            zone = Constz.Zone.Z11;
        DataTable dt = FlowObj.GetStockList(warehouse,zone);
        return dt;
    }

    [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Update, true)]
    public bool UpdateStock(decimal LOID, string PRODUCTNAME, decimal QTY, string UNITNAME)
    {
        bool ret = true;
        if (!FlowObj.UpdateData(Authz.CurrentUserInfo.UserID, Convert.ToDouble(LOID), Convert.ToDouble(QTY)))
        {
            _error = FlowObj.ErrorMessage;
            throw new ApplicationException(_error);
        }
        return ret;
    }

}
