using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using ABB.DAL;
using System.Text;

namespace ABB.Flow.EIS
{
    public class EISCommonFlow
    {

        protected static string GetYearDisplay(Int32 yearFrom, Int32 yearTo)
        {
            string dispName = "";
            while (yearFrom <= yearTo)
            {
                dispName += (dispName == "" ? "" : ",") + "'" + yearFrom.ToString() + "'";
                yearFrom += 1;
            }
            return dispName;
        }

        protected static string GetMonthDisplay(Int32 MonthFrom, Int32 MonthTo)
        {
            string dispName = "";
            string monthName = "";
            while (MonthFrom <= MonthTo)
            {
                switch (MonthFrom)
                { 
                    case 1 :
                        monthName = "มกราคม";
                        break;

                    case 2:
                        monthName = "กุมภาพันธ์";
                        break;

                    case 3:
                        monthName = "มีนาคม";
                        break;

                    case 4:
                        monthName = "เมษายน";
                        break;

                    case 5:
                        monthName = "พฤษภาคม";
                        break;

                    case 6:
                        monthName = "มิถุนายน";
                        break;

                    case 7:
                        monthName = "กรกฎาคม";
                        break;

                    case 8:
                        monthName = "สิงหาคม";
                        break;

                    case 9:
                        monthName = "กันยายน";
                        break;

                    case 10:
                        monthName = "ตุลาคม";
                        break;

                    case 11:
                        monthName = "พฤศจิกายน";
                        break;

                    case 12:
                        monthName = "ธันวาคม";
                        break;
                }
                dispName += (dispName == "" ? "" : ",") + "'" + monthName + "'";
                MonthFrom += 1;
            }
            return dispName;
        }

        public static DataTable GetProductList(string TableName, string TextField, string ValueField, string OrderField, string WhereStr)
        {
            DataTable dt = null;
            string sql = "SELECT DISTINCT " + TextField + ", " + ValueField + " FROM " + TableName + " ";
            sql += (WhereStr == "" ? "" : " WHERE " + WhereStr);
            sql += " ORDER BY " + (OrderField == "" ? TextField : OrderField) + " ";
            try
            {
                dt = OracleDB.ExecListCmd(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return dt;
        }

        protected static string GetProductPublishedCond(int yearFrom, int yearTo, string warehouse, string pd_type, string pd_group, ArrayList arrProduct)
        {
            string whStr = "";
            string loidlist = "(";
            int count = 0;

            for (int i = 0; i < arrProduct.Count; i++)
            {
                count += 1;
                if (count < arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "',";
                else if (count == arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "')";
            }

            whStr += (warehouse == "0" ? "" : " WAREHOUSE = " + warehouse);
            whStr += (pd_type == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTTYPE = " + pd_type);
            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTGROUP = " + pd_group);
            whStr += (arrProduct.Count == 0 ? "" : (whStr == "" ? "" : " AND ") + " LOID IN " + loidlist);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();

            return whStr;
        }
        protected static string GetProductPublishedCondCus(int yearFrom, int yearTo, string warehouse, string customer, string pd_type, string pd_group, ArrayList arrProduct)
        {
            string whStr = "";
            string loidlist = "(";
            int count = 0;

            for (int i = 0; i < arrProduct.Count; i++)
            {
                count += 1;
                if (count < arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "',";
                else if (count == arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "')";
            }

            whStr += (warehouse == "0" ? "" : " WAREHOUSE = " + warehouse);
            whStr += (pd_type == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTTYPE = " + pd_type);
            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCTGROUP = " + pd_group);
            whStr += (customer == "0" ? "" : (whStr == "" ? "" : " AND ") + " CUSTOMER = " + customer);
            whStr += (arrProduct.Count == 0 ? "" : (whStr == "" ? "" : " AND ") + " LOID IN " + loidlist);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();

            return whStr;
        }
        protected static string GetProductPublishedCondProduction(int yearFrom, int yearTo, string BarcodeFrom, string BarcodeTo,string pd_group, ArrayList arrProduct)
        {
            string whStr = "";
            string loidlist = "(";
            int count = 0;

            for (int i = 0; i < arrProduct.Count; i++)
            {
                count += 1;
                if (count < arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "',";
                else if (count == arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "')";
            }

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (arrProduct.Count == 0 ? "" : (whStr == "" ? "" : " AND ") + " LOID IN " + loidlist);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();
            //if (BarcodeFrom != "" && BarcodeTo != "" && BarcodeFrom != null && BarcodeTo != null)
            //whStr += (whStr == "" ? "" : " AND ") + "( BARCODE BETWEEN " + BarcodeFrom.ToString() + " AND " + BarcodeTo.ToString() + ")";
            if (BarcodeFrom != "" && BarcodeFrom != null)
            whStr += (whStr == "" ? "" : " AND ") + "( BARCODE >= " + BarcodeFrom.ToString() + ")";
            else if (BarcodeTo != "" && BarcodeTo != null)
            whStr += (whStr == "" ? "" : " AND ") + "( BARCODE <= " + BarcodeTo.ToString() + ")";
            return whStr;
        }
        protected static string GetProductPublishedCondDate(DateTime DateFrom, DateTime DateTo, string BarcodeFrom, string BarcodeTo, string pd_group, ArrayList arrProduct)
        {
            string whStr = "";
            string loidlist = "(";
            int count = 0;

            for (int i = 0; i < arrProduct.Count; i++)
            {
                count += 1;
                if (count < arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "',";
                else if (count == arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "')";
            }

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (arrProduct.Count == 0 ? "" : (whStr == "" ? "" : " AND ") + " LOID IN " + loidlist);
            whStr += (whStr == "" ? "" : " AND ") + " " + OracleDB.SetDateToStringField("MFGDATE") + " BETWEEN " + OracleDB.SetDateToStringValue(DateFrom) + " AND " + OracleDB.SetDateToStringValue(DateTo);
            //if (BarcodeFrom != "" && BarcodeTo != "")
            //    whStr += (whStr == "" ? "" : " AND ") + "( BARCODE BETWEEN " + BarcodeFrom.ToString() + " AND " + BarcodeTo.ToString() + ")";
            if (BarcodeFrom != "" && BarcodeFrom != null)
                whStr += (whStr == "" ? "" : " AND ") + "( BARCODE >= " + BarcodeFrom.ToString() + ")";
            else if (BarcodeTo != "" && BarcodeTo != null)
                whStr += (whStr == "" ? "" : " AND ") + "( BARCODE <= " + BarcodeTo.ToString() + ")";

            return whStr;
        }
        protected static string GetProductPublishedDayProduction(int yearFrom, int yearTo, string pd_group, ArrayList arrProduct)
        {
            string whStr = "";
            string loidlist = "(";
            int count = 0;

            for (int i = 0; i < arrProduct.Count; i++)
            {
                count += 1;
                if (count < arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "',";
                else if (count == arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "')";
            }

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (arrProduct.Count == 0 ? "" : (whStr == "" ? "" : " AND ") + " LOID IN " + loidlist);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();

            return whStr;
        }
        public static string GetmonthPublished(int yearFrom, int yearTo, int month, string pd_group, ArrayList arrProduct)
        {
            string whStr = "";
            string loidlist = "(";
            int count = 0;

            for (int i = 0; i < arrProduct.Count; i++)
            {
                count += 1;
                if (count < arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "',";
                else if (count == arrProduct.Count)
                    loidlist += "'" + arrProduct[i] + "')";
            }

            whStr += (pd_group == "0" ? "" : (whStr == "" ? "" : " AND ") + " PRODUCEGROUP = " + pd_group);
            whStr += (arrProduct.Count == 0 ? "" : (whStr == "" ? "" : " AND ") + " LOID IN " + loidlist);
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();
            whStr += (whStr == "" ? "" : " AND ") + " REQMON = " + month.ToString();
            whStr += (whStr == "" ? "" : " AND ") + " REQYEAR BETWEEN " + yearFrom.ToString() + " AND " + yearTo.ToString();

            return whStr;
            
        }
    }
}
