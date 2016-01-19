using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using ABB.DAL;

namespace ABB.Flow.EIS
{
    public class EISReportSupportFlow : EISCommonFlow
    {
        private const string TableName = "V_REPORT_SUPPORT_MONTH";

        public static DataTable GetYearlyPublished(int yearFrom, int yearTo, string warehouse, string pd_type, string pd_group, string pd_name)
        {
            string whStr = "";

            whStr += (warehouse == "0" ? "" : " WAREHOUSE = " + warehouse);
            whStr += (pd_type == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTTYPE = " + pd_type);
            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTGROUP = " + pd_group);
            whStr += (pd_name == "0" ? "" : (whStr == "" ? "" : " AND ") + " LOID = " + pd_name);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();


            string dispName = GetYearDisplay(yearFrom, yearTo);
            string sql = "SELECT * FROM " + TableName + " WHERE " + whStr;
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublished(int yearFrom, int yearTo, string warehouse, string pd_type, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT * FROM  " + TableName + "  WHERE " + GetProductPublishedCond(yearFrom, yearTo, warehouse, pd_type, pd_group, arrProduct);
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByYear(int yearFrom, int yearTo, string warehouse, string pd_type, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = yearFrom; i <= yearTo; ++i)
            {
                sql += ", SUM(CASE REQYEAR WHEN '" + i.ToString() + "' THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCond(yearFrom, yearTo, warehouse, pd_type, pd_group, arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByMonth(int year, string warehouse, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = 1; i <= 12; ++i)
            {
                sql += ", SUM(CASE MON WHEN " + i.ToString() + " THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCond(year, year, warehouse, "0", "0", arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
