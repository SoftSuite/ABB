using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.DAL;
using System.Collections;

namespace ABB.Flow.EIS
{
    public class EISReportPriceSaleMonthFlow : EISCommonFlow
    {
        private const string TableName1 = "V_REPORT_SALEPRICE_MONTH";
        private const string TableName2 = "V_REPORT_SALEPRICE_DAY";

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
            string sql = "SELECT REQYEAR,MON,SUM(PRICE)QTY FROM " + TableName1 + " WHERE " + whStr + "GROUP BY REQYEAR,MON ";
            return OracleDB.ExecListCmd(sql);
        }
        public static DataTable GetmonthPublished(int yearFrom, int yearTo, int month, string warehouse, string pd_type, string pd_group, string pd_name, string customer)
        {
            string whStr = "";

            whStr += (warehouse == "0" ? "" : " WAREHOUSE = " + warehouse);
            whStr += (pd_type == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTTYPE = " + pd_type);
            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTGROUP = " + pd_group);
            whStr += (pd_name == "0" ? "" : (whStr == "" ? "" : " AND ") + " LOID = " + pd_name);
            whStr += (customer == "0" ? "" : (whStr == "" ? "" : " AND ") + " CUSTOMER = " + customer);
            whStr += (whStr == "" ? "" : " AND ") + " REQMON = " + month.ToString();
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();


            //string dispName = GetMonthDisplay(monthFrom, monthTo);
            string sql = "SELECT REQYEAR,REQMON,REQDAY,SUM(PRICE)QTY FROM " + TableName2 + " WHERE " + whStr + "GROUP BY REQYEAR,REQMON,REQDAY ";
            return OracleDB.ExecListCmd(sql);
        }



    }
}

