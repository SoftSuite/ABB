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
using ABB.Data.Admin;
using ABB.Data.Production;
using ABB.Flow;
using ABB.Flow.Production;
using ABB.Global;


public partial class Transaction_QCAnalysis : System.Web.UI.Page
{
    //#region Variables & Properties

    //private StockinQCFlow _flow;
    //private StockinQCFlow item;

    //public ReturnRequestFlow FlowObj
    //{
    //    get { if (_flow == null) _flow = new ReturnRequestFlow(); return _flow; }
    //}

    //public ReturnRequestItem ItemObj
    //{
    //    get { if (item == null) item = new ReturnRequestItem(); return item; }
    //}

    //#endregion

    //#region Others

    //private void ResetState(double loid)
    //{
    //    // Get Data From LOID (existing)

    //    //ItemObj.ClearSession();
    //    //SetData(FlowObj.GetAllData(loid));

    //    if (loid != 0)
    //    {
    //        txtLOID.Text = loid.ToString();
    //        txtLotNo.ReadOnly = true;
    //        txtLotNo.CssClass = "zTextbox-View";
    //        btnSearch.Visible = false;
    //        //this.txtRefLoid = data.REFLOID;
    //        //SetDataPD(FlowObj.GetRefData(data.REFLOID));
    //    }
    //    ItemObj.ClearSession();
    //    SetData(FlowObj.GetAllData(loid));
    //}

    //private void ResetState1(double loid)
    //{
    //    // Get Data From LOT (new one)

    //    ItemObj.ClearSession();
    //    SetData(FlowObj.GetRefData(loid));
    //}

    //#endregion

    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        ResetState(Convert.ToDouble(Request["loid"] == null ? "0" : Request["loid"]));
    //    }
    //}
}
