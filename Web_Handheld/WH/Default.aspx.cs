using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ABB.Data;

public partial class WH_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lnkStockInPD.NavigateUrl = Constz.HomeFolder + "WH/Transaction/StockInPD/StockInList.aspx";
            this.lnlStockInPO.NavigateUrl = Constz.HomeFolder + "WH/Transaction/StockInPO/StockInList.aspx";
            this.lnkStockOut.NavigateUrl = Constz.HomeFolder + "WH/Transaction/StockOut/StockOutList.aspx";
            this.lnkStockCheck.NavigateUrl = Constz.HomeFolder + "WH/Transaction/StockCheck/StockCheckList.aspx";
            this.LoginStatus1.LogoutText = "<img src='" + Constz.ImageFolder + "Logout.png" + "' border='0' align='AbsMiddle'> ออกจากระบบ";
        }
    }
}
