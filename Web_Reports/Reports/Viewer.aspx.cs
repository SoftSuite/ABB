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
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

public partial class Reports_Viewer : System.Web.UI.Page
{
    ReportDocument reportDoc;

    protected void Page_Load(object sender, EventArgs e)
    {     
        if (Request.QueryString["repname"] != null && Request.QueryString["param"] != null)
        { 
            reportDoc = new ReportDocument();
            reportDoc.Load(Server.MapPath(Request.QueryString["repname"]));

            string param = Request.QueryString["param"];
            int index = -1;

            index = param.IndexOf("|");

            if (index == -1)
                AssignParam(param);
            else
            {
                string[] temp = param.Split('|');
                foreach (string prtemp in temp)
                {
                    AssignParam(prtemp);
                }
            }

            //set connection
            TableLogOnInfo logonInfo = new TableLogOnInfo();
            logonInfo.ConnectionInfo.ServerName = ConfigurationManager.AppSettings["DB_SERVER"].ToString().Trim();
            logonInfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["DB_USER"].ToString().Trim();
            logonInfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["DB_PASSWORD"].ToString();
            crViewer.LogOnInfo.Add(logonInfo);

            crViewer.EnableDatabaseLogonPrompt = false;
            crViewer.EnableDrillDown = false;
            crViewer.DisplayGroupTree = false;
            crViewer.HasToggleGroupTreeButton = false;
            crViewer.ReportSource = reportDoc;
        }
    }

    private void AssignParam(string param)
    {
        string[] parameter = param.Split(',');
        ParameterDiscreteValue pdv = new ParameterDiscreteValue();
        pdv.Value = parameter[1].Trim();

        ParameterField pf = new ParameterField();
        pf.ParameterValueType = ParameterValueKind.StringParameter;
        pf.CurrentValues.Add(pdv);
        pf.Name = parameter[0].Trim();
        crViewer.ParameterFieldInfo.Add(pf);
    }
}
