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

public partial class _Default : System.Web.UI.Page 
{
    private void BindReport(string reportName)
    {
        CrystalDecisions.CrystalReports.Engine.ReportDocument rpt = new CrystalDecisions.CrystalReports.Engine.ReportDocument();
        string reportPath = Server.MapPath(Constz.HomeFolder + "Reports/" + reportName + ".rpt");
        rpt.Load(reportPath);
        CrystalDecisions.Shared.TableLogOnInfo logonInfo = new CrystalDecisions.Shared.TableLogOnInfo();
        logonInfo = rpt.Database.Tables[0].LogOnInfo;
        logonInfo.ConnectionInfo.ServerName = ConfigurationManager.AppSettings["DB_SERVER"].ToString().Trim();
        logonInfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["DB_USER"].ToString().Trim();
        logonInfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["DB_PASSWORD"].ToString();
        rpt.Database.Tables[0].ApplyLogOnInfo(logonInfo);

        CrystalDecisions.Shared.ParameterValues curValue = new CrystalDecisions.Shared.ParameterValues();
        CrystalDecisions.Shared.ParameterDiscreteValue paraValue = new CrystalDecisions.Shared.ParameterDiscreteValue();
        for (int i = 0; i < Request.QueryString.Count; ++i)
        {
            string field = Request["paramfield" + (i + 1).ToString()];
            string value = Request["paramvalue" + (i + 1).ToString()];
            if (field != null && value != null)
            {
                paraValue.Value = value;
                curValue = rpt.ParameterFields[field].CurrentValues;
                curValue.Add(paraValue);
                rpt.ParameterFields[field].CurrentValues = curValue;
            }
            else
                break;
        }
        //paraValue.Value = reportKey;
        //curValue = rpt.ParameterFields["LOID"].CurrentValues;
        //curValue.Add(paraValue);
        //rpt.ParameterFields["LOID"].CurrentValues = curValue;

        rpt.PrintOptions.PaperSource = CrystalDecisions.Shared.PaperSource.Auto;

        if (Request["landscape"] != null)
        {
            if (Request["landscape"] == "1") rpt.PrintOptions.PaperOrientation = CrystalDecisions.Shared.PaperOrientation.Landscape;
        }
        Page.Cache["rpt"] = rpt;
        ctlReportViewer.ReportSource = Page.Cache["rpt"];
        //ctlReportViewer.DataBind();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (IsPostBack && Page.Cache["rpt"] != null)
        //{
        //    ctlReportViewer.ReportSource = Page.Cache["rpt"];
        //    ctlReportViewer.DataBind();
        //}
        //else if (Request[Constz.QueryString.ReportName] != null)
        //{
        //    //BindReport(Request[Constz.QueryString.ReportName], Convert.ToDouble(Request[Constz.QueryString.ReportKey]));
        //    BindReport(Request[Constz.QueryString.ReportName]);
        //}
        BindReport(Request[Constz.QueryString.ReportName]);
    }

}