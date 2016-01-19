using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.DAL;
using System.Collections;

namespace ABB.Flow.EIS
{
    public class EISReportProductionFlow : EISCommonFlow
    {
        private const string TableName = "V_REPORT_PRODUCTION_EIS";
        public static DataTable GetYearlyPublished(int yearFrom, int yearTo, string pd_group, string pd_name)
        {
            string whStr = "";

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (pd_name == "0" ? "" : (whStr == "" ? "" : " AND ") + " LOID = " + pd_name);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();


            string dispName = GetYearDisplay(yearFrom, yearTo);
            string sql = "SELECT * FROM " + TableName + " WHERE " + whStr;
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublished(int yearFrom, int yearTo, string BarcodeFrom, string BarcodeTo, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT * FROM  " + TableName + "  WHERE " + GetProductPublishedCondProduction(yearFrom, yearTo,BarcodeFrom,BarcodeTo, pd_group, arrProduct);
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByYear(int yearFrom, int yearTo, string BarcodeFrom,string BarcodeTo, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = yearFrom; i <= yearTo; ++i)
            {
                sql += ", SUM(CASE REQYEAR WHEN '" + i.ToString() + "' THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCondProduction(yearFrom, yearTo, BarcodeFrom,BarcodeTo, pd_group, arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }
        public static DataTable GetProductPublishedGroupByDate(DateTime DateFrom, DateTime DateTo, string BarcodeFrom, string BarcodeTo, string pd_group, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            DateTime date = DateFrom;
            while (date <= DateTo)
            {
                sql += ", SUM(CASE MFGDATE WHEN " + OracleDB.SetDate(date) + " THEN QTY ELSE 0 END) QTY" + date.Day.ToString() + "" + date.Month.ToString() + "" + date.Year.ToString() + " ";
                date = date.AddDays(1);
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCondDate(DateFrom, DateTo, BarcodeFrom, BarcodeTo, pd_group, arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }
        public static DataTable GetProductPublishedGroupByMonth(int year, string BarcodeFrom,string BarcodeTo, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = 1; i <= 12; ++i)
            {
                sql += ", SUM(CASE MON WHEN " + i.ToString() + " THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCondProduction(year, year, BarcodeFrom, BarcodeTo, "0", arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByDay(int yearFrom, int yearTo, string BarcodeFrom, string BarcodeTo, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME ";
            for (int i = 1; i <= 12; ++i)
            {
                sql += ", SUM(CASE MON WHEN " + i.ToString() + " THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedCondProduction(yearFrom, yearTo, BarcodeFrom,BarcodeTo, "0", arrProduct) + " ";
            sql += "GROUP BY LOID, NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPublishedGroupByDay1(int year, int month, ArrayList arrProduct)
        {
            string sql = "SELECT LOID, NAME";
            for (int i = 1; i <= 31; ++i)
            {
                if (i <= 9)
                {
                    sql += ", SUM(CASE REQDAY WHEN '0" + i.ToString() + "' THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
                }
                else
                    sql += ", SUM(CASE REQDAY WHEN '" + i.ToString() + "' THEN QTY ELSE 0 END) QTY" + i.ToString() + " ";
            }
            sql += "FROM  " + TableName + "  ";
            sql += "WHERE " + GetProductPublishedDayProduction(year, year, "0", arrProduct) + " ";
            sql += "GROUP BY LOID, NAME,REQYEAR ,REQDAY,QTY ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
