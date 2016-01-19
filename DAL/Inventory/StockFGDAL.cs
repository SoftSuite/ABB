using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data.Sales;
using ABB.Data.Inventory.FG;
using ABB.Data;

namespace ABB.DAL.Inventory
{
    public class StockFGDAL
    {
        public DataTable GetStockOutList(StockOutFGSearchData whereData)
        {
            string whereString = "";
            if (whereData.DOCTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "DOCLOID = '" + whereData.DOCTYPE.ToString() + "' ";

            if (whereData.STOCKOUTCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(STOCKCODE) = '" + OracleDB.QRText(whereData.STOCKOUTCODE.Trim()).ToUpper() + "' ";

            if (whereData.RESERVEDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RESERVEDATE >= " + OracleDB.QRDate(whereData.RESERVEDATEFROM) + " ";

            if (whereData.RESERVEDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RESERVEDATE <= " + OracleDB.QRDate(whereData.RESERVEDATETO) + " ";

            if (whereData.REQUISITIONCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(REQCODE) = '" + OracleDB.QRText(whereData.REQUISITIONCODE.Trim()).ToUpper() + "' ";

            if (whereData.CREATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(whereData.CREATEFROM) + " ";

            if (whereData.CREATEBY.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CREATEBY) = '" + OracleDB.QRText(whereData.CREATEBY.Trim().ToUpper()) + "' ";

            if (whereData.CREATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(whereData.CREATETO) + " ";

            if (whereData.CUSTOMER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "CUSTOMER = " + whereData.CUSTOMER.ToString() + " ";

            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";

            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID LOID,ST.DOCTYPE DOCLOID,DT.DOCNAME DOCTYPE,RQ.CODE REQCODE,RQ.REQDATE,ST.CREATEBY ,ST.CREATEON, RQ.RESERVEDATE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, ST.CODE STOCKCODE,CU.LOID CUSTOMER, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ST.INVCODE, RQ.LOID REQUISITION ";
            sql += "FROM STOCKOUT ST  INNER JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID  ";
            sql += "INNER JOIN CUSTOMER CU ON ST.RECEIVER = CU.LOID INNER JOIN REQUISITION RQ ON ST.REFTABLE = 'REQUISITION' AND ST.REFLOID = RQ.LOID WHERE ST.DOCTYPE IN ( ";
            sql += Constz.DocType.Reserve.LOID.ToString() + "," + Constz.DocType.ReqOrgSupport.LOID.ToString() + "," + Constz.DocType.ReqFair.LOID.ToString() + "," + Constz.DocType.ReqDistribute.LOID.ToString() + "," + Constz.DocType.ReqSupport.LOID.ToString() + ") UNION ";
            sql += "SELECT ST.LOID LOID,ST.DOCTYPE DOCLOID,DT.DOCNAME DOCTYPE,RQ.CODE REQCODE,RQ.REQDATE,ST.CREATEBY ,ST.CREATEON, RQ.RESERVEDATE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, ST.CODE STOCKCODE,W.LOID CUSTOMER, W.NAME AS CUSTOMERNAME, ST.INVCODE, RQ.LOID REQUISITION ";
            sql += "FROM STOCKOUT ST  INNER JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID  ";
            sql += "INNER JOIN WAREHOUSE W ON ST.RECEIVER = W.LOID INNER JOIN REQUISITION RQ ON ST.REFTABLE = 'REQUISITION' AND ST.REFLOID = RQ.LOID WHERE ST.DOCTYPE = " + Constz.DocType.ReqProduct.LOID.ToString() + ") A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY STOCKCODE DESC ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutItemList(double stockOut)
        {
            string sql = "SELECT 1 NO, STI.REFLOID, P.CODE BARCODE, STI.PRODUCT, P.NAME PRODUCTNAME, STI.LOTNO, STI.QTY, STI.UNIT, STI.PRICE, UNIT.NAME UNITNAME, STI.REMAIN AS REMAINQTY ";
            sql += "FROM STOCKOUTITEM STI INNER JOIN PRODUCT P ON STI.PRODUCT = P.LOID ";
            sql += "INNER JOIN PRODUCTGROUP PG ON P.PRODUCTGROUP=PG.LOID ";
            sql += "INNER JOIN UNIT ON STI.UNIT = UNIT.LOID ";
            sql += "WHERE STI.STOCKOUT = " + stockOut.ToString() + " ";
            sql += "ORDER BY PG.NAME, P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutItemBlank()
        {
            string sql = "SELECT 1 NO, 0 REFLOID, '' BARCODE, 0 PRODUCT, '' LOTNO, 0 QTY, 0 UNIT, 0 PRICE, '' UNITNAME ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetRequisitionItemList(double requisition, double product, string barcode, OracleTransaction zTrans)
        {
            string where = "RQI.REQUISITION = " + requisition.ToString() + " ";
            if (product != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID = " + product.ToString() + " ";

            if (barcode != "")
                where += (where == "" ? "" : "AND ") + "(UPPER(P.BARCODE) = '" + barcode.ToUpper() + "' OR UPPER(P.CODE) = '" + barcode.ToUpper() + "') ";

            string sql = "SELECT 1 NO, RQI.LOID REFLOID, P.CODE BARCODE, RQI.PRODUCT, NULL LOTNO, RQI.QTY, RQI.UNIT, RQI.PRICE, UNIT.NAME UNITNAME ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON RQI.PRODUCT = P.LOID ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";
            return OracleDB.ExecListCmd(sql, zTrans);
        }

        public DataTable GetProductStock(double warehouse, double productBarcode)
        {
            return GetProductStock(warehouse, productBarcode, null);
        }
        public DataTable GetProductStock(double warehouse, double productBarcode, OracleTransaction trans)
        {
            double zone = Constz.Zone.Z30;
            string sql = "SELECT PS.* ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.QTY > 0 AND PS.WAREHOUSE = " + warehouse.ToString() + " AND P.LOID = " + productBarcode.ToString() + " AND ZONE = " + zone.ToString() + " ORDER BY PS.LOTNO ";
            return OracleDB.ExecListCmd(sql, trans);
        }

        public DataTable GetReturnList(StockoutSearchData whereData)
        {
            string whereString = "DOCLOID = " + Constz.DocType.RetProduct.LOID + " ";

            if (whereData.STOCKCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) = '" + OracleDB.QRText(whereData.STOCKCODE.Trim()).ToUpper() + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.SUPPLIER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "SUPPLIER = " + whereData.SUPPLIER.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID,ST.CODE,ST.DOCTYPE DOCLOID,ST.CREATEBY,ST.CREATEON,SP.LOID SUPPLIER, SP.SUPPLIERNAME,STI.TOTAL, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK FROM STOCKOUT ST  LEFT JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID ";
            sql += "INNER JOIN SUPPLIER SP ON ST.RECEIVER = SP.LOID LEFT JOIN (SELECT STOCKOUT,SUM(QTY*PRICE) TOTAL FROM STOCKOUTITEM GROUP BY STOCKOUT) STI ";
            sql += "ON STI.STOCKOUT = ST.LOID) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReturnWHList(StockoutSearchData whereData)
        {
            string whereString = "DOCLOID = " + Constz.DocType.RetRaw.LOID + " ";

            if (whereData.STOCKCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) = '" + OracleDB.QRText(whereData.STOCKCODE.Trim()).ToUpper() + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.SUPPLIER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "SUPPLIER = " + whereData.SUPPLIER.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(whereData.STATUSFROM.Trim()) + "' ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(whereData.STATUSTO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID,ST.CODE,ST.DOCTYPE DOCLOID,ST.CREATEBY,ST.CREATEON,SP.LOID SUPPLIER, SP.SUPPLIERNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK,ST.REASON FROM STOCKOUT ST  LEFT JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID ";
            sql += "INNER JOIN SUPPLIER SP ON ST.RECEIVER = SP.LOID) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionList(ProductReserveSearchData whereData)
        {
            string whereString = "";
            //string whereString = "DOCLOID IN (" + Constz.DocType.ReqRawPD.LOID.ToString() + "," + Constz.DocType.ReqRawPO.LOID.ToString() + ") ";
            if (whereData.REQUISITIONTYPE == 0)
                whereString += (whereString == "" ? "" : "AND ") + "DOCLOID IN (" + Constz.DocType.ReqRawPD.LOID.ToString() + "," + Constz.DocType.ReqRawPO.LOID.ToString() + ") OR DOCLOID = 24 ";
            if (whereData.REQUISITIONTYPE == 24)
                whereString += (whereString == "" ? "" : "AND ") + "DOCLOID = '" + whereData.REQUISITIONTYPE.ToString() + "' ";
            if (whereData.REQUISITIONTYPE == 11)
                whereString += (whereString == "" ? "" : "AND ") + "DOCLOID = '" + Constz.DocType.ReqRawPD.LOID.ToString() + "' ";
            if (whereData.REQUISITIONTYPE == 12)
                whereString += (whereString == "" ? "" : "AND ") + "DOCLOID = '" + Constz.DocType.ReqRawPO.LOID.ToString() + "' ";
            if (whereData.CUSTOMERNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CREATEBY = '" + OracleDB.QRText(whereData.CUSTOMERNAME.Trim()) + "' ";
            if (whereData.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(STOCKCODE) = '" + OracleDB.QRText(whereData.CODE.Trim()).ToUpper() + "' ";
            if (whereData.REQCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(REQCODE) = '" + OracleDB.QRText(whereData.REQCODE.Trim()).ToUpper() + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.CREATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(whereData.CREATEFROM) + " ";
            if (whereData.CREATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(whereData.CREATETO) + " ";
            if (whereData.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + whereData.PRODUCT.ToString() + " ";
            if (whereData.DIVISION != 0)
                whereString += (whereString == "" ? "" : "AND ") + "DIVISION = " + whereData.DIVISION.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID LOID,ST.DOCTYPE DOCLOID,DT.DOCNAME DOCTYPE,NVL(VPL.CODE,VRP.RQCODE) REQCODE,NVL(VPL.ORDERDATE,VRP.REQDATE) REQDATE,ST.CREATEBY ,ST.CREATEON, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, ST.CODE STOCKCODE,NVL(VPL.LOID,VRP.PDLOID) PRODUCT, ";
            sql += "CASE ST.REFTABLE WHEN 'REQUISITION' THEN '" + Constz.ReadyMadeDepartment.Name + "' ELSE S.SUPPLIERNAME END AS CUSTOMERNAME ";
            sql += "FROM STOCKOUT ST  INNER JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID  ";
            sql += "LEFT JOIN PDORDER VPL ON ST.REFTABLE = 'PDORDER' AND VPL.LOID = ST.REFLOID ";
            sql += "LEFT JOIN V_REQUISITION_PROD VRP ON ST.REFTABLE = 'REQUISITION' AND VRP.RQLOID = ST.REFLOID ";
            sql += "LEFT JOIN SUPPLIER S ON S.LOID = ST.RECEIVER ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY STOCKCODE DESC";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionOtherList(ProductReserveSearchData whereData)
        {
            string whereString = "DOCTYPE = " + Constz.DocType.RetSOther.LOID + " ";
            
            if (whereData.DIVISION != 0)
                whereString += (whereString == "" ? "" : "AND ") + "DIVISION = " + whereData.DIVISION.ToString() + " ";
            if (whereData.CUSTOMERNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CREATEBY = '" + OracleDB.QRText(whereData.CUSTOMERNAME.Trim()) + "' ";
            if (whereData.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(STOCKOUTCODE) = '" + OracleDB.QRText(whereData.CODE.Trim()).ToUpper() + "' ";
            if (whereData.CREATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(whereData.CREATEFROM) + " ";
            if (whereData.CREATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(whereData.CREATETO)+ " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID LOID,ST.DOCTYPE,ST.CODE STOCKOUTCODE,ST.CREATEON REQDATE,ST.CREATEBY ,ST.CREATEON, DV.TNAME DIVISIONNAME, ST.SUPPORTREFCODE,DV.LOID DIVISION, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM STOCKOUT ST ";
            sql += "INNER JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID ";
            sql += "INNER JOIN DIVISION DV ON DV.LOID=ST.DIVISION) A  ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY STOCKOUTCODE DESC";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReceiveList(StockInFGData whereData)
        {
            string whereString = " DOCTYPE = " + Constz.DocType.RecProduct.LOID.ToString() + " ";

            if (whereData.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) = '" + OracleDB.QRText(whereData.CODE.Trim()).ToUpper() + "' ";
            if (whereData.QCCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(QCCODE) = '" + OracleDB.QRText(whereData.QCCODE.Trim()).ToUpper() + "' ";
            if (whereData.INVNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVNO) = '" + OracleDB.QRText(whereData.INVNO.Trim()).ToUpper() + "' ";
            if (whereData.POCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOID IN (SELECT DISTINCT STOCKIN FROM STOCKINITEM STI INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND STI.REFLOID = POI.LOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID WHERE UPPER(PO.CODE) = '" + OracleDB.QRText(whereData.POCODE.Trim()).ToUpper() + "') ";
            if (whereData.RECEIVEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE >= " + OracleDB.QRDate(whereData.RECEIVEFROM) + " ";
            if (whereData.RECEIVETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE <= " + OracleDB.QRDate(whereData.RECEIVETO) + " ";
            if (whereData.ORDERFROM.Year != 1 && whereData.ORDERTO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "LOID IN (SELECT DISTINCT STOCKIN FROM STOCKINITEM STI INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND STI.REFLOID = POI.LOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID WHERE ORDERDATE >= " + OracleDB.QRDate(whereData.ORDERFROM) + " AND ORDERDATE <= " + OracleDB.QRDate(whereData.ORDERTO) + ") ";
            if (whereData.SENDER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "SENDER = " + whereData.SENDER.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID, ST.CODE, ST.DOCTYPE, ST.RECEIVEDATE, ST.INVNO,SP.SUPPLIERNAME ,ST.QCCODE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK , ST.SENDER , PD.PRODUCTNAME, STI.QCDUEDATE ";
            sql += "FROM STOCKIN ST LEFT JOIN SUPPLIER SP ON ST.SENDER = SP.LOID ";
            sql += "INNER JOIN (SELECT MAX(LOID) LOID, STOCKIN FROM STOCKINITEM STI GROUP BY STI.STOCKIN) SI ON SI.STOCKIN=ST.LOID ";
            sql += "INNER JOIN STOCKINITEM STI ON STI.LOID=SI.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=STI.PRODUCT)A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE DESC ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReceiveWHList(StockInFGData whereData)
        {
            string whereString = " DOCTYPE = " + Constz.DocType.RecRaw.LOID.ToString() + " ";

            if (whereData.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) = '" + OracleDB.QRText(whereData.CODE.Trim()).ToUpper() + "' ";
            if (whereData.QCCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(QCCODE) = '" + OracleDB.QRText(whereData.QCCODE.Trim()).ToUpper() + "' ";
            if (whereData.INVNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVNO) = '" + OracleDB.QRText(whereData.INVNO.Trim()).ToUpper() + "' ";
            if (whereData.POCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOID IN (SELECT DISTINCT STOCKIN FROM STOCKINITEM STI INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND STI.REFLOID = POI.LOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID WHERE UPPER(PO.CODE) = '" + OracleDB.QRText(whereData.POCODE.Trim()).ToUpper() + "') ";
            if (whereData.RECEIVEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE >= " + OracleDB.QRDate(whereData.RECEIVEFROM) + " ";
            if (whereData.RECEIVETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE <= " + OracleDB.QRDate(whereData.RECEIVETO) + " ";
            if (whereData.ORDERFROM.Year != 1 && whereData.ORDERTO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "LOID IN (SELECT DISTINCT STOCKIN FROM STOCKINITEM STI INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND STI.REFLOID = POI.LOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID WHERE ORDERDATE >= " + OracleDB.QRDate(whereData.ORDERFROM) + " AND ORDERDATE <= " + OracleDB.QRDate(whereData.ORDERTO) + ") ";
            if (whereData.SENDER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "SENDER = " + whereData.SENDER.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID, ST.CODE, ST.DOCTYPE, ST.RECEIVEDATE,ST.SENDER, ST.INVNO,SP.SUPPLIERNAME ,ST.QCCODE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK, PD.PRODUCTNAME, STI.QCDUEDATE ";
            sql += "FROM STOCKIN ST LEFT JOIN SUPPLIER SP ON ST.SENDER = SP.LOID ";
            sql += "INNER JOIN (SELECT MAX(LOID) LOID, STOCKIN FROM STOCKINITEM STI GROUP BY STI.STOCKIN) SI ON SI.STOCKIN=ST.LOID ";
            sql += "INNER JOIN STOCKINITEM STI ON STI.LOID=SI.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID=STI.PRODUCT )A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE DESC";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReceiveOTList(StockInFGData whereData)
        {
            string whereString = " DOCTYPE = " + 23 + " ";

            if (whereData.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) = '" + OracleDB.QRText(whereData.CODE.Trim()).ToUpper() + "' ";
            if (whereData.QCCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(QCCODE) = '" + OracleDB.QRText(whereData.QCCODE.Trim()).ToUpper() + "' ";
            if (whereData.INVNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(INVNO) = '" + OracleDB.QRText(whereData.INVNO.Trim()).ToUpper() + "' ";
            if (whereData.POCODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOID IN (SELECT DISTINCT STOCKIN FROM STOCKINITEM STI INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND STI.REFLOID = POI.LOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID WHERE UPPER(PO.CODE) = '" + OracleDB.QRText(whereData.POCODE.Trim()).ToUpper() + "') ";
            if (whereData.RECEIVEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE >= " + OracleDB.QRDate(whereData.RECEIVEFROM) + " ";
            if (whereData.RECEIVETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RECEIVEDATE <= " + OracleDB.QRDate(whereData.RECEIVETO) + " ";
            if (whereData.ORDERFROM.Year != 1 && whereData.ORDERTO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "LOID IN (SELECT DISTINCT STOCKIN FROM STOCKINITEM STI INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND STI.REFLOID = POI.LOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID WHERE ORDERDATE >= " + OracleDB.QRDate(whereData.ORDERFROM) + " AND ORDERDATE <= " + OracleDB.QRDate(whereData.ORDERTO) + ") ";
            if (whereData.SENDER != 0)
                whereString += (whereString == "" ? "" : "AND ") + "SENDER = " + whereData.SENDER.ToString() + " ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID, ST.CODE, ST.DOCTYPE, ST.RECEIVEDATE,ST.SENDER, ST.INVNO,SP.SUPPLIERNAME ,ST.QCCODE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM STOCKIN ST  LEFT JOIN SUPPLIER SP ON ST.SENDER = SP.LOID)A  ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE DESC";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReceiveItemList(double stockin)
        {
            string sql = "SELECT STI.LOID, ROWNUM RANK, STI.PRODUCT,STI.LOTNO, STI.PRICE, STI.QTY SQTY, STI.QCQTY ,PO.CODE,POI.QTY PQTY, STI.REFLOID, P.BARCODE,UNIT.LOID UNIT, STI.QCREMARK, ";
            sql += "CASE STI.QCRESULT WHEN '" + Constz.QCResult.Pass.Code + "' THEN '" + Constz.QCResult.Pass.Name + "' ";
            sql += "WHEN '" + Constz.QCResult.Fail.Code + "' THEN '" + Constz.QCResult.Fail.Name + "' ELSE '' END AS QCRESULT,STI.REMARK ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT INNER JOIN POITEM POI ON STI.REFTABLE = 'POITEM' AND POI.LOID = STI.REFLOID INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID ";
            sql += "WHERE STI.STOCKIN = " + stockin;
            return OracleDB.ExecListCmd(sql);
        }

        // To used in StockinProduct
        public DataTable GetProductItemList(double stockin)
        {
            string sql = "SELECT STI.LOID, ROWNUM RANK,PPD.LOID PDPLOID ,PPD.LOTNO,PPD.MFGDATE,P.NAME PRODUCTNAME,PPD.SENDFGQTY PDQTY,STI.QTY QTY,UNIT.NAME UNITNAME,P.LOID PRODUCT,UNIT.LOID UNIT,STI.REFLOID ";
            sql += "FROM STOCKINITEM STI INNER JOIN PDPRODUCT PPD ON PPD.LOID=STI.REFLOID AND STI.REFTABLE ='PDPRODUCT' ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = PPD.PRODUCT INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += "WHERE STI.STOCKIN = " + stockin;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 PDPLOID, '' LOTNO, 0 MFGDATE, '' PRODUCTNAME, 0 PDQTY, 0 QTY, 0 UNITNAME, 0 PRODUCT, 0 UNIT,0 REFLOID  ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReceiveItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, '' LOTNO, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReserveItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, P.NAME PRODUCTNAME, RQI.QTY, RQI.UNIT, RQI.PRICE, RQI.DISCOUNT, RQI.NETPRICE, RQI.ACTIVE, P.BARCODE, ";
            sql += "UNIT.NAME UNITNAME, RQI.ISVAT, RQI.LOID REFLOID ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition + " ORDER BY P.BARCODE ";
            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = (i + 1);
            }
            return dt;
        }


        public POItemData DoGetPOItem(double loid)
        {
            string sql = "SELECT PO.LOID, PO.CODE, POI.PRODUCT, PD.NAME, PD.BARCODE, POI.QTY, POI.UNIT FROM POITEM POI ";
            sql += "INNER JOIN PDORDER PO ON PO.LOID = POI.PDORDER INNER JOIN PRODUCT PD ON POI.PRODUCT = PD.LOID ";
            sql += "WHERE POI.LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            POItemData data = new POItemData();
            if (dt.Rows.Count > 0)
            {
                data.LOID = Convert.ToDouble(dt.Rows[0]["LOID"]);
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["QTY"]);
                data.UNIT = Convert.ToDouble(dt.Rows[0]["UNIT"]);
                data.BARCODE = dt.Rows[0]["BARCODE"].ToString();
                //     data.NAME = dt.Rows[0]["NMAE"].ToString();
            }

            return data;
        }

        public SupplierData DoGetSenderData(double loid)
        {
            string sql = "SELECT * FROM SUPPLIER WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            SupplierData data = new SupplierData();
            if (dt.Rows.Count > 0)
            {
                data.CODE = dt.Rows[0]["CODE"].ToString();
                data.NAME = dt.Rows[0]["SUPPLIERNAME"].ToString();

            }

            return data;
        }

        public string GetProduceType(double loid)
        {
            string sql = "SELECT PPD.PRODUCETYPE FROM STOCKIN ST INNER JOIN STOCKINITEM SI ON SI.STOCKIN=ST.LOID INNER JOIN PDPRODUCT PPD ON PPD.LOID=SI.REFLOID AND SI.REFTABLE ='PDPRODUCT' WHERE ST.LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string type = "";
            if (dt.Rows.Count > 0)
            {
                type = dt.Rows[0]["PRODUCETYPE"].ToString();

            }

            return type;
        }

        public StockinReturnData DoGetPDData(double loid)
        {
            string sql = "SELECT * FROM V_MATERIAL_RETURN_POPUP_LIST WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            StockinReturnData data = new StockinReturnData();
            if (dt.Rows.Count > 0)
            {
                data.PDBARCODE = dt.Rows[0]["BARCODE"].ToString();
                data.PDNAME = dt.Rows[0]["PRODUCTNAME"].ToString();
                data.QTY = Convert.ToDouble(dt.Rows[0]["PDQTY"]);
                data.UNAME = dt.Rows[0]["UNITNAME"].ToString();
                data.CODE = dt.Rows[0]["REQCODE"].ToString();
                data.REQDATE = Convert.ToDateTime(dt.Rows[0]["REQDATE"]);
                data.PPLOID = Convert.ToDouble(dt.Rows[0]["PPLOID"]);

            }

            return data;
        }

    }
}
