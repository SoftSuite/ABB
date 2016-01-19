using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.DAL.Sales
{
    public class StockInShopDAL
    {
        private string V_PRODUCTSTOCKINSHOP
        {
            get
            {
                string view= "(SELECT S.CODE STOCKOUTCODE, SI.STOCKOUT, S.APPROVEDATE, SI.LOID REFLOID, SI.PRODUCT, SI.UNIT, P.NAME PRODUCTNAME, SI.LOTNO, SI.QTY OUTQTY, SI.QTY INQTY, ";
                view += "P.BARCODE, UNIT.NAME UNITNAME, SI.PRICE, SI.PRICE*SI.QTY TOTAL, 'STOCKOUTITEM' REFTABLE, RQ.CODE REQUISITIONCODE, RQ.RESERVEDATE ";
                view += "FROM STOCKOUT" + Constz.ABBSERV + " S INNER JOIN STOCKOUTITEM" + Constz.ABBSERV + " SI ON S.LOID = SI.STOCKOUT ";
                view += "INNER JOIN PRODUCT P ON P.LOID = SI.PRODUCT ";
                view += "INNER JOIN UNIT ON UNIT.LOID = SI.UNIT ";
                view += "INNER JOIN REQUISITIONITEM" + Constz.ABBSERV + " RQI ON RQI.LOID = SI.REFLOID AND SI.REFTABLE = 'REQUISITIONITEM' ";
                view += "INNER JOIN REQUISITION" + Constz.ABBSERV + " RQ ON RQ.LOID = RQI.REQUISITION ";
                view += "WHERE S.DOCTYPE = " + Constz.DocType.ReqProduct.LOID.ToString() + ") ";
                return view;
            }
        }

        public DataTable GetStockInShopList(StockInShopSearchData data)
        {
            string where = "STI.DOCTYPE = " + Constz.DocType.RecShop.LOID.ToString() + " ";
            if (data.STOCKINCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(STI.CODE) LIKE '%" + data.STOCKINCODE.Trim().ToUpper() + "%' ";

            if (data.REQUISITIONCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(STI.REFCODE) LIKE '%" + data.REQUISITIONCODE.Trim().ToUpper() + "%' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(STI.RECEIVEDATE, 'YYYYMMDD') >= TO_CHAR(" + OracleDB.QRDate(data.DATEFROM) + ", 'YYYYMMDD') ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(STI.RECEIVEDATE, 'YYYMMDD')  <= TO_CHAR(" + OracleDB.QRDate(data.DATETO) + ", 'YYYYMMDD') ";

            //string sql = "SELECT 0 ORDERNO, STI.LOID, STI.CODE STOCKINCODE, STI.RECEIVEDATE, RQ.CODE REQUISITIONCODE, RQ.RESERVEDATE, STI.GRANDTOT, ";
            //sql += "CASE STI.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            //sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            //sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' END STATUSNAME ";
            //sql += "FROM STOCKIN STI LEFT JOIN STOCKOUT" + Constz.ABBSERV + " STO ON STO.LOID = STI.REFLOID AND STI.REFTABLE = 'STOCKOUT' AND STO.DOCTYPE = " + Constz.DocType.ReqProduct.LOID.ToString() + " ";
            //sql += "LEFT JOIN REQUISITION" + Constz.ABBSERV + " RQ ON RQ.LOID = STO.REFLOID AND STO.REFTABLE = 'REQUISITION' ";
            string sql = "SELECT 0 ORDERNO, STI.LOID, STI.CODE STOCKINCODE, STI.RECEIVEDATE, STI.REFCODE REQUISITIONCODE, STI.GRANDTOT, ";
            sql += "CASE STI.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' END STATUSNAME ";
            sql += "FROM STOCKIN STI ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY STI.CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockInProductList(double stockIn)
        {
            //string sql = "SELECT 0 ORDERNO, SI.PRODUCT, SI.UNIT, P.BARCODE, P.NAME PRODUCTNAME, SI.PRICE, SUM(SO.QTY) OUTQTY, SI.QTY INQTY, UNIT.NAME UNITNAME, ";
            //sql += "SI.PRICE*SI.QTY TOTAL ";
            //sql += "FROM STOCKINITEM SI INNER JOIN STOCKIN S ON S.LOID = SI.STOCKIN ";
            //sql += "INNER JOIN PRODUCT P ON P.LOID = SI.PRODUCT ";
            //sql += "INNER JOIN UNIT ON UNIT.LOID = SI.UNIT ";
            //sql += "INNER JOIN STOCKOUTITEM" + Constz.ABBSERV + " SO ON SO.STOCKOUT = S.REFLOID AND S.REFTABLE = 'STOCKOUT' AND SO.PRODUCT = SI.PRODUCT ";
            //sql += "WHERE SI.STOCKIN = " + stockIn.ToString() + " ";
            //sql += "GROUP BY SI.PRODUCT, SI.UNIT, P.BARCODE, P.NAME, SI.PRICE, SI.QTY, UNIT.NAME, SI.PRICE*SI.QTY ";
            //sql += "ORDER BY P.BARCODE ";

            string sql = "SELECT 0 ORDERNO, SI.PRODUCT, SI.UNIT, P.BARCODE, P.NAME, SI.PRICE, SI.QTY QTY, UNIT.NAME UNITNAME, ";
            sql += "SI.PRICE*SI.QTY TOTAL ";
            sql += "FROM STOCKINITEM SI INNER JOIN STOCKIN S ON S.LOID = SI.STOCKIN ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = SI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = SI.UNIT ";
            //sql += "INNER JOIN STOCKOUTITEM" + Constz.ABBSERV + " SO ON SO.STOCKOUT = S.REFLOID AND S.REFTABLE = 'STOCKOUT' AND SO.PRODUCT = SI.PRODUCT ";
            sql += "WHERE SI.STOCKIN = " + stockIn.ToString() + " ";
            //sql += "GROUP BY SI.PRODUCT, SI.UNIT, P.BARCODE, P.NAME, SI.PRICE, SI.QTY, UNIT.NAME, SI.PRICE*SI.QTY ";
            sql += "ORDER BY P.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutProductList(double stockOut)
        {
            string sql = "SELECT 0 ORDERNO, V.PRODUCT, V.UNIT, V.BARCODE, V.PRODUCTNAME, V.PRICE, SUM(V.OUTQTY) OUTQTY, SUM(V.INQTY) INQTY, V.UNITNAME, ";
            sql += "V.TOTAL ";
            sql += "FROM " + V_PRODUCTSTOCKINSHOP + " V ";
            sql += "WHERE STOCKOUT = " + stockOut.ToString() + " ";
            sql += "GROUP BY V.PRODUCT, V.UNIT, V.BARCODE, V.PRODUCTNAME, V.UNITNAME, V.PRICE, V.TOTAL ";
            sql += "ORDER BY V.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutList(ABB.Data.Search.StockOutProductSearchData data, double currentStockIn)
        {
            string where = "V.STOCKOUT NOT IN (SELECT REFLOID FROM STOCKIN WHERE STATUS <> '" + Constz.Requisition.Status.Void.Code + "' " +
                    "AND REFTABLE = 'STOCKOUT' AND LOID <> " + currentStockIn.ToString() + " AND DOCTYPE = " + Constz.DocType.RecShop.LOID.ToString() + ") ";

            if (data.REQUISITIONCODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.REQUISITIONCODE) >= '" + data.REQUISITIONCODEFROM.Trim().ToUpper() + "' ";
            if (data.REQUISITIONCODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.REQUISITIONCODE) <= '" + data.REQUISITIONCODETO.Trim().ToUpper() + "' ";
            if (data.RESERVEDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(V.RESERVEDATE, 'YYYYMMDD') >= TO_CHAR(" + OracleDB.QRDate(data.RESERVEDATEFROM) + ", 'YYYYMMDD')  ";
            if (data.RESERVEDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(V.RESERVEDATE, 'YYYYMMDD') <= TO_CHAR(" + OracleDB.QRDate(data.RESERVEDATETO) + ", 'YYYYMMDD')  ";

            string sql = "SELECT distinct 0 ORDERNO, V.STOCKOUT, V.REQUISITIONCODE, V.RESERVEDATE ";
            sql += "FROM " + V_PRODUCTSTOCKINSHOP + " V ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY V.REQUISITIONCODE ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
