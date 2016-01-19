using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class ReturnRequestDAL
    {
        public DataTable GetRequestList(ProductReserveSearchData whereData)
        {
            //TESTSETSETSET
            string whereString = "";
            if (whereData.REQUISITIONTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "REQUISITIONTYPE = " + whereData.REQUISITIONTYPE.ToString() + " ";
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

            //string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.LOID, RQ.CODE, RQ.REQUISITIONTYPE, RT.NAME REQUISITIONTYPENAME, RQ.RESERVEDATE, RQ.DUEDATE, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ";
            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.LOID, RQ.CODE, RQ.REQUISITIONTYPE, RT.NAME REQUISITIONTYPENAME, RQ.RESERVEDATE, RQO.INVCODE, RQ.GRANDTOT, RQ.REQDATE, RQ.DUEDATE, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ";
            sql += "RQ.REFLOID, CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.ApproveWH.Code + "' THEN '" + Constz.Requisition.Status.ApproveWH.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.ApproveWH.Code + "' THEN '" + Constz.Requisition.Status.ApproveWH.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, RQ.CREATEBY ";
            sql += "FROM REQUISITION RQ INNER JOIN REQUISITION RQO ON RQO.LOID = RQ.REFLOID AND RQ.REFTABLE = 'REQUISITION' ";
            sql += "INNER JOIN REQUISITIONTYPE RT ON RQ.REQUISITIONTYPE = RT.LOID ";
            sql += "LEFT JOIN CUSTOMER CU ON RQ.CUSTOMER = CU.LOID ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetRequestItemList(double requisition)
        {
            string sql = "SELECT 0 RANK, RQI.LOID, RQI.PRODUCT, PD.BARCODE, PD.NAME PRODUCTNAME, RQI.QTY, RQI.PRICE, RQI.UNIT, ";
            sql += "UNIT.NAME UNITNAME, RQI.DISCOUNT, (RQI.QTY * (RQI.PRICE-RQV.DISCOUNT)) NETPRICE, RQI.ISVAT, RQV.QTY OLDQTY, ";
            sql += "RQV.DISCOUNT OLDDISCOUNT, RQI.REFLOID ";
            sql += "FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQ.LOID= RQI.REQUISITION ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "INNER JOIN REQUISITIONITEM RQV ON RQV.LOID = RQI.REFLOID AND RQI.REFTABLE = 'REQUISITIONITEM' ";
            sql += "WHERE RQ.LOID = " + requisition.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetInvoiceItemList(double requisition)
        {
            string sql = "SELECT 0 RANK, LOID, PRODUCT, BARCODE, PRODUCTNAME, QTY, PRICE, UNIT, UNITNAME, DISCOUNT, (QTY* (PRICE-OLDDISCOUNT)) NETPRICE, ISVAT, OLDQTY, ";
            sql += "OLDDISCOUNT, LOID REFLOID ";
            sql += "FROM V_INVOICE_FOR_REQUEST ";
            sql += "WHERE REQUISITION = " + requisition.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetInvoiceRequest(double requisition)
        {
            string sql = "SELECT RQ.LOID, RQ.CODE, RQ.REQDATE, RQ.REFLOID, RQ.STATUS, RQ.CUSTOMER, RQ.WAREHOUSE, RQ.CTITLE, RQ.CNAME, RQ.CLASTNAME, ";
            sql += "RQ.CADDRESS, RQ.CTEL, RQ.CFAX, RQ.REASON, RQ.REMARK, RQ.TOTAL, RQ.TOTDIS, RQ.VAT, RQ.TOTVAT, RQ.GRANDTOT, RQ.CREATEBY, ";
            sql += "ROUND((RQO.TOTAL + RQO.TOTVAT - RQO.TOTDIS) * (100/ (100+RQO.VAT)),2) OLDTOTAL, RQO.INVCODE,  C.CODE CUSTOMERCODE, ";
            sql += "T.NAME || C.NAME || ' ' || C.LASTNAME CUSTOMERNAME ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON C.LOID = RQ.CUSTOMER ";
            sql += "LEFT JOIN TITLE T ON T.LOID = C.TITLE ";
            sql += "LEFT JOIN REQUISITION RQO ON RQO.LOID = RQ.REFLOID AND RQ.REFTABLE = 'REQUISITION' ";
            sql += "WHERE RQ.LOID = " + requisition.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetOldInvoiceRequest(double requisition)
        {
            string sql = "SELECT RQ.LOID, RQ.CODE, RQ.REQDATE, RQ.REFLOID, RQ.STATUS, RQ.CUSTOMER, RQ.WAREHOUSE, RQ.CTITLE, RQ.CNAME, RQ.CLASTNAME, ";
            sql += "RQ.CADDRESS, RQ.CTEL, RQ.CFAX, RQ.REASON, RQ.REMARK, RQ.TOTAL, RQ.TOTDIS, RQ.VAT, RQ.TOTVAT, RQ.GRANDTOT, RQ.CREATEBY, ";
            sql += "ROUND((RQ.TOTAL + RQ.TOTVAT - RQ.TOTDIS) * (100/ (100+RQ.VAT)),2) OLDTOTAL, RQ.INVCODE,  C.CODE CUSTOMERCODE, ";
            sql += "T.NAME || C.NAME || ' ' || C.LASTNAME CUSTOMERNAME ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON C.LOID = RQ.CUSTOMER ";
            sql += "LEFT JOIN TITLE T ON T.LOID = C.TITLE ";
            sql += "WHERE RQ.LOID = " + requisition.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetDataByRefLOID(double currentLOID, double refLOID, System.Data.OracleClient.OracleTransaction trans)
        {
            string sql = "SELECT LOID FROM REQUISITION WHERE REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ05.ToString() + " ";
            sql += "AND STATUS <> '" + Constz.Requisition.Status.Void.Code + "' AND REFTABLE = 'REQUISITION' AND REFLOID = " + refLOID.ToString() + " ";
            sql += "AND LOID <> " + currentLOID.ToString() + " ";
            return OracleDB.ExecListCmd(sql, trans);
        }

    }
}
