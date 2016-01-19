using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class ReturnTesterDAL
    {
        public DataTable GetReturnTesterList(ReturnTesterSearchData data)
        {
            string where = "";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : " AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : " AND ") + "TO_DATE(CREATEON) >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : " AND ") + "TO_DATE(CREATEON) <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.STATUSFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(data.STATUSFROM.Trim()) + " ";

            if (data.STATUSTO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(data.STATUSTO.Trim()) + " ";

            if (data.SENDER != 0)
                where += (where == "" ? "" : " AND ") + "SENDER = " + data.SENDER.ToString() + " ";

            string sql = "SELECT * FROM (SELECT 0 AS BOX, ST.LOID, ST.CODE, ST.CREATEON, ST.SENDER, W.NAME AS WAREHOUSENAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "ELSE '' END AS STATUS, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN " + Constz.Requisition.Status.Approved.Rank + " ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN " + Constz.Requisition.Status.Void.Rank + " ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN " + Constz.Requisition.Status.Waiting.Rank + " ";
            sql += "ELSE 0 END AS RANK, ";
            sql += "COUNT(STI.LOID) AS TOTAL ";
            sql += "FROM STOCKOUT ST INNER JOIN STOCKOUTITEM STI ON STI.STOCKOUT = ST.LOID ";
            sql += " INNER JOIN WAREHOUSE W ON W.LOID = ST.RECEIVER ";
            sql += "GROUP BY ST.LOID, ST.CODE, ST.CREATEON, ST.SENDER, W.NAME, ST.STATUS) A ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE, CREATEON, WAREHOUSENAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutItem(double stockOut)
        {
            string sql = "SELECT ROWNUM ORDERNO, P.BARCODE, P.NAME, STI.QTY, P.PRICE, P.PRICE*STI.QTY AS NETPRICE, UNIT.NAME AS UNITNAME, STI.PRODUCT, P.UNIT ";
            sql += "FROM STOCKOUTITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += "WHERE STI.STOCKOUT = " + stockOut.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProduct(double product, string barcode)
        {
            string where = "";
            if (product != 0)
                where += (where == "" ? "" : "AND ") + "PD.LOID = " + product.ToString() + " ";

            if (barcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PD.BARCODE) = '" + product.ToString() + "' ";

            string sql = "SELECT PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME UNITNAME, PD.PRICE, PD.ISVAT ";
            sql += "FROM PRODUCT PD INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where;
            return OracleDB.ExecListCmd(sql);
        }

    }
}
