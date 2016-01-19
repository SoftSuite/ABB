using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class ReturnProductDAL
    {
        public DataTable GetReserveList(ProductReserveSearchData whereData)
        {
            string whereString = "";
            if (whereData.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(whereData.CODEFROM.Trim()).ToUpper() + "' ";
            if (whereData.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(whereData.CODETO.Trim()).ToUpper() + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.CUSTOMERNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CUSTOMERNAME) LIKE '%" + OracleDB.QRText(whereData.CUSTOMERNAME.Trim()).ToUpper() + "%' ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.CODE, RQ.LOID, RQ.REQUISITIONTYPE, RQ.REQDATE, RQ.RESERVEDATE, RQ.DUEDATE, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.ApproveWH.Code + "' THEN '" + Constz.Requisition.Status.ApproveWH.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.ApproveWH.Code + "' THEN '" + Constz.Requisition.Status.ApproveWH.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK, RQ.CREATEBY ";
            sql += "FROM REQUISITION RQ ";
            sql += "INNER JOIN CUSTOMER CU ON RQ.CUSTOMER = CU.LOID AND RQ.REQUISITIONTYPE = '12') A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY REQDATE, CODE ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReserveItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.QTY, RQI.UNIT, RQI.PRICE, RQI.DISCOUNT, RQI.NETPRICE, RQI.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME,(SELECT SUM(P.QTY) FROM V_PRODUCT_RETURNREQUEST P WHERE P.LOID=RQI.PRODUCT GROUP BY P.LOID) PDQTY ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReqProductionItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.Qty QTY, P.UNIT, P.PRICE, 0 AS DISCOUNT, RQI.Qty*P.PRICE AS NETPRICE, RQI.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME,P.QTY PDQTY ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN V_PRODUCT_RETURNREQUEST P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionItemList(double product)
        {
            string sql = "SELECT B.LOID, ROWNUM RANK, B.MATERIAL AS PRODUCT, B.MASTER QTY, P.UNIT, P.PRICE, 0 AS DISCOUNT, B.MASTER*P.PRICE AS NETPRICE, B.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME ";
            sql += "FROM BOM B INNER JOIN PRODUCT P ON P.LOID = B.MATERIAL ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = B.UNIT ";
            sql += "WHERE B.MAINPRODUCT = " + product;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStockList(double productBarcode)
        {
            string sql = "SELECT PS.* ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE P.LOID = " + productBarcode.ToString() + " ORDER BY PS.LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReserveItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, 0 PDQTY, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}

