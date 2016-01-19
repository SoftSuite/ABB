using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.DAL;
using System.Collections;

namespace ABB.Flow.EIS
{
    public class EISReportProductionMonthFlow : EISCommonFlow
    {
        private const string TableName1 = "V_REPORT_PRODUCTION_EIS";
        private const string TableName2 = "V_REPORT_PRODUCTION_DAY_EIS";

        public static DataTable GetYearlyPublished(int yearFrom, int yearTo, string pd_group, string pd_name)
        {
            string whStr = "";

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (pd_name == "0" ? "" : (whStr == "" ? "" : " AND ") + " LOID = " + pd_name);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();


            string dispName = GetYearDisplay(yearFrom, yearTo);
            string sql = "SELECT * FROM " + TableName1 + " WHERE " + whStr;
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetMonthPublished(int yearFrom, int yearTo, int month, string pd_group, string pd_name)
        {
            string whStr = "";

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (pd_name == "0" ? "" : (whStr == "" ? "" : " AND ") + " LOID = " + pd_name);
            whStr += (whStr == "" ? "" : " AND ") + " REQMON = " + month.ToString();
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();


            //string dispName = GetMonthDisplay(monthFrom, monthTo);
            string sql = "SELECT * FROM " + TableName2 + " WHERE " + whStr;
            return OracleDB.ExecListCmd(sql);
        }


    }
}
