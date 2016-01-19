using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class InvoiceDAL
    {
        public DataTable GetPurchaseList(ProductReserveSearchData whereData)
        {
            string whereString = "";
            if (whereData.REQUISITIONTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "REQUISITIONTYPE = " + whereData.REQUISITIONTYPE.ToString() + " ";
            if (whereData.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVCODE) >= '" + OracleDB.QRText(whereData.CODEFROM.Trim()).ToUpper() + "' ";
            if (whereData.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVCODE) <= '" + OracleDB.QRText(whereData.CODETO.Trim()).ToUpper() + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.CUSTOMERNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CUSTOMERNAME) LIKE '%" + OracleDB.QRText(whereData.CUSTOMERNAME.Trim()).ToUpper() + "%' ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(whereData.STATUSFROM.Trim()) + "' ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(whereData.STATUSTO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.INVCODE, RQ.LOID, RQ.CODE, RQ.REQUISITIONTYPE, RQ.REFTYPELOID, RT.NAME REQUISITIONTYPENAME, RQ.REQDATE, RQ.DUEDATE, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, RQ.CREATEBY, RQ.RESERVEDATE, RQ.GRANDTOT, RQ.RESERVEDATE+14 AS SENDDATE ";
            sql += "FROM REQUISITION RQ INNER JOIN REQUISITIONTYPE RT ON RQ.REQUISITIONTYPE = RT.LOID ";
            sql += "AND (RQ.REQUISITIONTYPE =  " + Constz.Requisition.RequisitionType.REQ11.ToString() + " OR (RQ.REQUISITIONTYPE =  " + Constz.Requisition.RequisitionType.REQ01.ToString() + " AND RQ.STATUS <> '" + Constz.Requisition.Status.Waiting.Code + "')) LEFT JOIN CUSTOMER CU ON RQ.CUSTOMER = CU.LOID ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetInvoiceList(ProductReserveSearchData whereData)
        {
            string whereString = "";
            if (whereData.REQUISITIONTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "REFTYPELOID = " + whereData.REQUISITIONTYPE.ToString() + " ";
            if (whereData.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVCODE) >= '" + OracleDB.QRText(whereData.CODEFROM.Trim()).ToUpper() + "' ";
            if (whereData.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVCODE) <= '" + OracleDB.QRText(whereData.CODETO.Trim()).ToUpper() + "' ";
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

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.INVCODE, RQ.LOID, RQ.CODE, RQ.REQUISITIONTYPE, RQ.REFTYPELOID, RT.NAME REQUISITIONTYPENAME, RQ.REQDATE, RQ.DUEDATE, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, RQ.CREATEBY, RQ.RESERVEDATE, RQ.GRANDTOT, RQ.RESERVEDATE+14 AS SENDDATE ";
            sql += "FROM REQUISITION RQ INNER JOIN REQUISITIONTYPE RT ON RQ.REQUISITIONTYPE = RT.LOID ";
            sql += "AND (RQ.REQUISITIONTYPE =  " + Constz.Requisition.RequisitionType.REQ11.ToString() + " OR (RQ.REQUISITIONTYPE =  " + Constz.Requisition.RequisitionType.REQ01.ToString() + " AND RQ.STATUS <> '" + Constz.Requisition.Status.Waiting.Code + "')) LEFT JOIN CUSTOMER CU ON RQ.CUSTOMER = CU.LOID ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPurchaseItemList(double requisition, string warehouse)
        {
            string sql = "SELECT P.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.QTY, RQI.UNIT, RQI.PRICE, RQI.DISCOUNT, RQI.NETPRICE, RQI.ACTIVE, P.BARCODE, ";
            sql += "UNIT.NAME UNITNAME, P.ISVAT, FN_GETPRODUCTDISCOUNT(RQI.PRODUCT, " + warehouse.ToString() + ", P.ISDISCOUNT) NORMALDISCOUNT, P.NAME PRODUCTNAME ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
            sql += "ORDER BY PG.NAME, P.NAME";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPurchaseItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 RANK, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, ";
            sql += "'' ISVAT, 0 NORMALDISCOUNT, '' PRODUCTNAME ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public double GetRequisitionLOID(string code)
        {
            string sql = "SELECT LOID FROM REQUISITION WHERE CODE = '" + code + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double LOID = 0;
            if (dt.Rows.Count > 0)
            {
                LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
            }

            return LOID;
        }

        public double GetRequisitionLOID2(string invcode)
        {
            string sql = "SELECT LOID FROM REQUISITION WHERE INVCODE = '" + invcode + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double LOID = 0;
            if (dt.Rows.Count > 0)
            {
                LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
            }

            return LOID;
        }

        public DataTable GetProductItemList(string popup, string warehouse, string customer, string requisitiontype)
        {
            string sql = "SELECT PD.PDLOID LOID ,ROWNUM RANK, PD.PDLOID PRODUCT, PD.QTY, PD.ULOID UNIT, PD.PRICE, 0 DISCOUNT, PD.QTY*PD.PRICE NETPRICE, ";
            sql += "PD.PDACTIVE ACTIVE, PD.BARCODE, PD.UNAME UNITNAME, PD.ISVAT, ";
            sql += "FN_GETPRODUCTDISCOUNT(PD.PDLOID, " + warehouse.ToString() + ", PD.ISDISCOUNT) NORMALDISCOUNT, PD.PRODUCTNAME ";
            sql += "FROM V_PRODUCT_INVOICE PD ";
            sql += "WHERE PD.PDLOID IN (" + popup + ") AND PD.CULOID = " + customer + " AND REFTYPELOID = '" +requisitiontype+ "' ";
            sql += "ORDER BY PD.PRODUCTGROUPNAME, PD.PRODUCTNAME";
            return OracleDB.ExecListCmd(sql);
        }

        public string GetRefTypeTable(double reftype)
        {
            string sql = "SELECT REFTYPETABLE FROM V_REQTYPE_INVOICE WHERE LOID = " + reftype.ToString() + "";
            DataTable dt = OracleDB.ExecListCmd(sql);
            string REFTYPETABLE = "";
            if (dt.Rows.Count > 0)
            {
                REFTYPETABLE = dt.Rows[0]["REFTYPETABLE"].ToString();
            }

            return REFTYPETABLE;
        }

        public string GetUsedProduct(double loid)
        {
            string sql = "SELECT PRODUCT FROM REQUISITIONITEM WHERE REQUISITION = " + loid.ToString();
            DataTable dt = OracleDB.ExecListCmd(sql);
            string Product = "";
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Product += (Product == "" ? "" : ",") + dt.Rows[i]["PRODUCT"].ToString();
                }
            }

            return Product;
        }

    }
}

