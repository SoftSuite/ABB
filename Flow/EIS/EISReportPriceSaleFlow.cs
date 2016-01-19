using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.DAL;
using System.Collections;

namespace ABB.Flow.EIS
{
    public class EISReportPriceSaleFlow : EISCommonFlow
    {
        private const string TableName = "V_REPORT_SALEPRICE_MONTH";
        public static DataTable GetYearlyPublished(int yearFrom, int yearTo, string warehouse, string pd_type, string pd_group, string pd_name, string customer)
        {
            string whStr = "";

            whStr += (warehouse == "0" ? "" : " WAREHOUSE = " + warehouse);
            whStr += (pd_type == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTTYPE = " + pd_type);
            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTGROUP = " + pd_group);
            whStr += (pd_name == "0" ? "" : (whStr == "" ? "" : " AND ") + " LOID = " + pd_name);
            whStr += (customer == "0" ? "" : (whStr == "" ? "" : " AND ") + " CUSTOMER = " + customer);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();


            string dispName = GetYearDisplay(yearFrom, yearTo);
            string sql = "SELECT REQYEAR,MON,SUM(PRICE)QTY FROM " + TableName + " WHERE " + whStr + "GROUP BY REQYEAR,MON ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublished(int yearFrom, int yearTo, string warehouse, string pd_type, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT * FROM  " + TableName + "  WHERE " + GetProductPublishedCond(yearFrom, yearTo, warehouse, pd_type, pd_group, arrProduct);
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByYear(int yearFrom, int yearTo, string warehouse, string customer, string pd_type, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = yearFrom; i <= yearTo; ++i)
            {
                sql += ", SUM(CASE REQYEAR WHEN '" + i.ToString() + "' THEN PRICE ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCondCus(yearFrom, yearTo, warehouse, customer, pd_type, pd_group, arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByMonth(int year, string warehouse, string customer, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = 1; i <= 12; ++i)
            {
                sql += ", SUM(CASE MON WHEN " + i.ToString() + " THEN PRICE ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCondCus(year, year, warehouse, customer, "0", "0", arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}

