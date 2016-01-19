using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using ABB.Data;

namespace ABB.Global
{
    public class Appz
    {
        public static void ClientAlert(Control ctl, string alertText)
        {
            ScriptManager.RegisterStartupScript(ctl, typeof(UpdatePanel), "AlertMsg", "alert('" + alertText.Replace("'", "\"").Replace("\n", "") + "');", true);
        }

        public static void OpenReport(Control ctl, string reportName, double reportKey)
        {
            ScriptManager.RegisterStartupScript(ctl, typeof(UpdatePanel), "AlertMsg", ReportScript(reportName, reportKey), true);
        }

        public static void SetFocus(Page pageControl, string objClientID)
        {
            pageControl.ClientScript.RegisterStartupScript(pageControl.GetType(), "focus", "<script language=javascript> document.getElementById('" + objClientID + "').focus();</script>");
        }

        public static string ReportScript(string reportName, double loid)
        {
            return ReportScript(reportName, "paramfield1=LOID&paramvalue1=" + loid.ToString(), "0");
        }

        public static string ReportLandscapeScript(string reportName, double loid)
        {
            return ReportScript(reportName, "paramfield1=LOID&paramvalue1=" + loid.ToString(), "1");
        }

        public static string ReportScript(string reportName, string otherQueryString)
        {
            return ReportScript(reportName, otherQueryString, "0");
        }

        public static string ReportLandscapeScript(string reportName, string otherQueryString)
        {
            return ReportScript(reportName, otherQueryString, "1");
        }

        private static string ReportScript(string reportName, string otherQueryString, string landscape)
        {
            return "window.open('" + ConfigurationManager.AppSettings[Constz.WebConfigKey.WEB_REPORTS].ToString() + "Default.aspx?landscape=" + landscape + "&" + Constz.QueryString.ReportName + "=" + reportName + (otherQueryString == "" ? "" : "&") + otherQueryString + "', 'zReport', 'status=yes, toolbar=no, scrollbars=yes, menubar=no, width=800, height=600, resizable=yes');";
        }

        public static string GetStatusName(string code)
        {
            string ret = "";
            switch (code)
            {
                case Constz.Requisition.Status.SP.Code:
                    ret = Constz.Requisition.Status.SP.Name;
                    break;

                case Constz.Requisition.Status.Approved.Code:
                    ret = Constz.Requisition.Status.Approved.Name;
                    break;

                case Constz.Requisition.Status.Finish.Code:
                    ret = Constz.Requisition.Status.Finish.Name;
                    break;

                case Constz.Requisition.Status.QC.Code:
                    ret = Constz.Requisition.Status.QC.Name;
                    break;

                case Constz.Requisition.Status.Reserve.Code:
                    ret = Constz.Requisition.Status.Reserve.Name;
                    break;

                case Constz.Requisition.Status.Void.Code:
                    ret = Constz.Requisition.Status.Void.Name;
                    break;

                case Constz.Requisition.Status.Waiting.Code:
                    ret = Constz.Requisition.Status.Waiting.Name;
                    break;

                case Constz.Requisition.Status.RW.Code:
                    ret = Constz.Requisition.Status.RW.Name;
                    break;

                case Constz.Requisition.Status.XRay.Code:
                    ret = Constz.Requisition.Status.XRay.Name;
                    break;

                case Constz.Requisition.Status.QS.Code:
                    ret = Constz.Requisition.Status.QS.Name;
                    break;
            }
            return ret;
        }

    }
}
