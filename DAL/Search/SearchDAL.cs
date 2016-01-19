using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Inventory.FG;
using ABB.Data.Search;

namespace ABB.DAL.Search
{
    public class SearchDAL
    {
        public static DataTable GetCustomerList(SearchCustomerData data)
        {
            string where = "C.ACTIVE = '" + Constz.ActiveStatus.Active + "'  ";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            //if (data.FULLNAME.Trim() != "")
            //    where += (where == "" ? "" : "AND ") + "UPPER(C.NAME || ' ' || C.LASTNAME) LIKE '%" + data.FULLNAME.Trim().ToUpper() + "%' ";

            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.NAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";

            if (data.LASTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.LASTNAME) LIKE '%" + data.LASTNAME.Trim().ToUpper() + "%' ";

            if (data.CUSTOMERTYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.CUSTOMERTYPE) = '" + data.CUSTOMERTYPE.Trim().ToUpper() + "' ";

            if (data.MEMBERTYPE != 0)
                where += (where == "" ? "" : "AND ") + "C.MEMBERTYPE = " + data.MEMBERTYPE.ToString() + " ";

            string sql = "SELECT C.LOID, C.CODE, TITLE.NAME || C.NAME || '  ' || C.LASTNAME AS CUSTOMERNAME, MT.NAME AS MEMBERTYPE, ";
            sql += "CASE C.CUSTOMERTYPE WHEN '" + Constz.CustomerType.Company.Code + "' THEN '" + Constz.CustomerType.Company.Name + "' ";
            sql += "WHEN '" + Constz.CustomerType.Government.Code + "' THEN '" + Constz.CustomerType.Government.Name + "' ";
            sql += "WHEN '" + Constz.CustomerType.Personal.Code + "' THEN '" + Constz.CustomerType.Personal.Name + "' ";
            sql += "ELSE '' END CUSTOMERTYPE ";
            sql += "FROM CUSTOMER C INNER JOIN MEMBERTYPE MT ON C.MEMBERTYPE = MT.LOID AND C.EPDATE > SYSDATE AND MT.ACTIVE = '" + Constz.ActiveStatus.Active + "' ";
            sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY TITLE.NAME || C.NAME || '  ' || C.LASTNAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetCustRetProductList(SearchCustomerData data)
        {
            string where = "";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.CUCODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.FULLNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.CUSTOMERNAME) LIKE '%" + data.FULLNAME.Trim().ToUpper() + "%' ";

            if (data.CUSTOMERTYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.CUSTOMERTYPE) = '" + data.CUSTOMERTYPE.Trim().ToUpper() + "' ";

            if (data.MEMBERTYPE != 0)
                where += (where == "" ? "" : "AND ") + "V.MEMBERTYPE = " + data.MEMBERTYPE.ToString() + " ";

            string sql = "SELECT DISTINCT V.CULOID LOID, V.CUCODE CODE, V.CUSTOMERNAME, MT.NAME AS MEMBERTYPE, ";
            sql += "CASE V.CUSTOMERTYPE WHEN '" + Constz.CustomerType.Company.Code + "' THEN '" + Constz.CustomerType.Company.Name + "' ";
            sql += "WHEN '" + Constz.CustomerType.Government.Code + "' THEN '" + Constz.CustomerType.Government.Name + "' ";
            sql += "WHEN '" + Constz.CustomerType.Personal.Code + "' THEN '" + Constz.CustomerType.Personal.Name + "' ";
            sql += "ELSE '' END CUSTOMERTYPE ";
            sql += "FROM V_PRODUCT_RETURNREQUEST V INNER JOIN MEMBERTYPE MT ON V.MEMBERTYPE = MT.LOID ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY V.CUSTOMERNAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetSupplierList(SearchCustomerData data)
        {
            string where = "ACTIVE = '" + Constz.ActiveStatus.Active + "' ";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.FULLNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(SUPPLIERNAME) LIKE '%" + data.FULLNAME.Trim().ToUpper() + "%' ";


            string sql = "SELECT * FROM SUPPLIER ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductList(SearchProductData data)
        {
            string where = " ACTIVE = '1' ";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "(UPPER(BARCODE) = '" + data.CODE.Trim().ToUpper() + "' OR UPPER(ABBNAME) = '" + data.CODE.Trim().ToUpper() + "' )";

            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PNAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PGLOID = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PTLOID = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.WAREHOUSE != 0)
                where += (where == "" ? "" : "AND ") + "WAREHOUSE = " + data.WAREHOUSE.ToString() + " ";

            if (data.ZONE != 0)
                where += (where == "" ? "" : "AND ") + "ZONE = " + data.ZONE.ToString() + " ";

            if (data.TYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "TYPE = '" + data.TYPE + "' ";

            string sql = "SELECT DISTINCT LOID, BARCODE, PNAME, NAME PRODUCTGROUP, TNAME PRODUCTTYPE ";
            sql += "FROM V_PRODUCT_LIST_REQUISITION ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductMasterList(SearchProductData data)
        {
            string where = " ACTIVE = '1' ";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(BARCODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PNAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.TYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "TYPE = '" + data.TYPE + "' ";

            string sql = "SELECT DISTINCT PRODUCTMASTER LOID, BARCODE, PRODUCTNAME PNAME, GNAME PRODUCTGROUP, PTNAME PRODUCTTYPE ";
            sql += "FROM V_PRODUCT_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetSaleList(SearchSaleData data)
        {
            string where = "RQ.REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ13.ToString() + " ";

            if (data.REQDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(REQDATE, 'YYYYMMDD') >= '" + data.REQDATEFROM.Year.ToString() + data.REQDATEFROM.ToString("MMdd") + "' ";

            if (data.REQDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(REQDATE, 'YYYYMMDD') <= '" + data.REQDATETO.Year.ToString() + data.REQDATETO.ToString("MMdd") + "' ";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.CODE >= '" + OracleDB.QRText(data.CODEFROM) + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.CODE <= '" + OracleDB.QRText(data.CODETO) + "' ";

            if (data.CUSTOMERNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.NAME || ' ' || C.LASTNAME) LIKE '%" + OracleDB.QRText(data.CUSTOMERNAME) + "%' ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "RQI.PRODUCT = " + data.PRODUCT.ToString() + " ";

            if (data.CUSTOMER != 0)
                where += (where == "" ? "" : "AND ") + "RQ.CUSTOMER = " + data.CUSTOMER.ToString() + " ";

            if (data.INVCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.INVCODE = " + data.INVCODE.ToString() + " ";

            if (data.STATUS.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.STATUS = '" + data.STATUS.Trim() + "' ";

            string sql = "SELECT DISTINCT RQ.LOID, RQ.REQDATE, RQ.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME AS CUSTOMERNAME, RQ.REFNO, RQ.CUSTOMER, ";
            sql += "RQ.TOTDIS, RQ.TOTAL, RQ.TOTVAT, RQ.GRANDTOT, RQ.INVCODE, C.CODE AS CUSTOMERCODE ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON RQ.CUSTOMER = C.LOID ";
            sql += "INNER JOIN REQUISITIONITEM RQI ON RQ.LOID = RQI.REQUISITION ";
            sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY RQ.REQDATE, RQ.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME ";
            return OracleDB.ExecListCmd(sql);
        }
        public static DataTable GetSaleList2(SearchSaleData data)
        {
            string where = "RQ.REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ13.ToString() + " ";

            if (data.REQDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(REQDATE, 'YYYYMMDD') >= '" + data.REQDATEFROM.Year.ToString() + data.REQDATEFROM.ToString("MMdd") + "' ";

            if (data.REQDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(REQDATE, 'YYYYMMDD') <= '" + data.REQDATETO.Year.ToString() + data.REQDATETO.ToString("MMdd") + "' ";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.CODE = '" + OracleDB.QRText(data.CODEFROM) + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.CODE <= '" + OracleDB.QRText(data.CODETO) + "' ";

            if (data.CUSTOMERNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.NAME || ' ' || C.LASTNAME) LIKE '%" + OracleDB.QRText(data.CUSTOMERNAME) + "%' ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "RQI.PRODUCT = " + data.PRODUCT.ToString() + " ";

            if (data.CUSTOMER != 0)
                where += (where == "" ? "" : "AND ") + "RQ.CUSTOMER = " + data.CUSTOMER.ToString() + " ";

            if (data.INVCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.INVCODE = " + data.INVCODE.ToString() + " ";

            if (data.STATUS.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.STATUS = '" + data.STATUS.Trim() + "' ";

            string sql = "SELECT DISTINCT RQ.LOID, RQ.REQDATE, RQ.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME AS CUSTOMERNAME, RQ.REFNO, RQ.CUSTOMER, ";
            sql += "RQ.TOTDIS, RQ.TOTAL, RQ.TOTVAT, RQ.GRANDTOT, RQ.INVCODE, C.CODE AS CUSTOMERCODE ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON RQ.CUSTOMER = C.LOID ";
            sql += "INNER JOIN REQUISITIONITEM RQI ON RQ.LOID = RQI.REQUISITION ";
            sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY RQ.REQDATE, RQ.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetReserveList(ProductReserveSearchData data)
        {
            string where = "QTY > 0";

            if (data.REQUISITIONTYPE != 0)
                where += (where == "" ? "" : "AND ") + "REFTYPELOID = " + data.REQUISITIONTYPE.ToString() + " ";
            if (data.CUSTOMER != 0)
                where += (where == "" ? "" : "AND ") + "CULOID = " + data.CUSTOMER.ToString() + " ";
            if (data.CODE != "")
                where += (where == "" ? "" : "AND ") + "PDLOID NOT IN (" + data.CODE.ToString() + ") ";


            string sql = "SELECT * FROM V_PRODUCT_INVOICE ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY PRODUCTGROUPNAME, PRODUCTNAME ";
            return OracleDB.ExecListCmd(sql);
        }
        public static DataTable GetInvList(SearchSaleData data)
        {
            string where = "";

            if (data.CUSTOMERNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C.NAME || ' ' || C.LASTNAME) LIKE '%" + OracleDB.QRText(data.CUSTOMERNAME) + "%' ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "RQI.PRODUCT = " + data.PRODUCT.ToString() + " ";

            if (data.CUSTOMER != 0)
                where += (where == "" ? "" : "AND ") + "RQ.CUSTOMER = " + data.CUSTOMER.ToString() + " ";

            if (data.INVCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "RQ.INVCODE = '" + data.INVCODE.ToString() + " ' ";

            string sql = "SELECT  RQ.LOID, RQ.REQDATE, RQ.CODE, TITLE.NAME || RQ.CNAME || ' ' || RQ.CLASTNAME AS CUSTOMERNAME, ";
            sql += "RQ.TOTDIS, RQ.TOTAL, RQ.TOTVAT, RQ.GRANDTOT, RQ.INVCODE, C.CODE AS CUSTOMERCODE ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON RQ.CUSTOMER = C.LOID ";
            sql += "INNER JOIN REQUISITIONITEM RQI INNER JOIN PRODUCT P ON RQI.PRODUCT = P.LOID ";
            sql += "ON RQ.LOID = RQI.REQUISITION LEFT JOIN TITLE ON TITLE.LOID = C.TITLE  ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY RQ.INVCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        /// <summary>
        ///   แสดงใบสั่งซื้อ/สั่งจอง ใบขอเบิกสินค้าฝากขาย ใบขอเบิกขายนอกสถานที่ และ ใบขอเบิกหน่วยงานสนับสนุน ที่ยังไม่มีใบเบิกออกจากคลัง (โดยไม่รวมใบเบิกที่ยกเลิก)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static DataTable GetRequisitionList(StockoutSearchData data)
        {
            string where = "STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND RQ_LOID NOT IN (SELECT REFLOID FROM STOCKOUT WHERE REFTABLE = 'REQUISITION') ";

            if (data.CUSTOMERNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(NAME) LIKE '%" + OracleDB.QRText(data.CUSTOMERNAME.Trim()).ToUpper() + "%' ";

            if (data.CUSTOMERCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(C_CODE) LIKE '%" + OracleDB.QRText(data.CUSTOMERCODE.Trim()).ToUpper() + "%' ";

            if (data.REQCODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.REQCODEFROM.Trim()).ToUpper() + "' ";

            if (data.REQCODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.REQCODETO.Trim()).ToUpper() + "' ";

            if (data.REQUESTDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.REQUESTDATEFROM) + " ";

            if (data.REQUESTDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.REQUESTDATETO) + " ";


            string sql = "SELECT RQ_LOID LOID, CODE, REQDATE, C_LOID, C_CODE, NAME, REQUISITIONTYPE, REQUISITIONTYPENAME RTNAME, WAREHOUSE, STATUS, REFTABLE, REFLOID FROM V_REQUISITION_LIST  ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY REQUISITIONTYPE, LOID ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductionList(StockoutSearchData data)
        {
            string where = " ISMATERIAL = 'Y' AND POI_LOID NOT IN (SELECT REFLOID FROM STOCKINITEM WHERE REFTABLE = 'POITEM') ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PD_LOID = " + data.PRODUCT.ToString() + " ";

            if (data.REQCODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(POCODE) >= '" + OracleDB.QRText(data.REQCODEFROM.Trim()).ToUpper() + "' ";

            if (data.REQCODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(POCODE) <= '" + OracleDB.QRText(data.REQCODETO.Trim()).ToUpper() + "' ";

            if (data.REQUESTDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(ORDERDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.REQUESTDATEFROM) + " ";

            if (data.REQUESTDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(ORDERDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.REQUESTDATETO) + " ";


            string sql = "SELECT PO_LOID LOID,POI_LOID REFLOID,POCODE CODE,ORDERDATE REQDATE,REFTABLE,S_LOID,S_CODE,SUPPLIERNAME NAME,PD_LOID PDLOID, PDNAME FROM V_PDORDER_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetReqProductionList(StockoutSearchData data)
        {
            string where = " RQLOID NOT IN (SELECT REFLOID FROM STOCKOUT WHERE REFTABLE = 'REQUISITION') ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PDLOID = " + data.PRODUCT.ToString() + " ";

            if (data.REQCODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(RQCODE) >= '" + OracleDB.QRText(data.REQCODEFROM.Trim()).ToUpper() + "' ";

            if (data.REQCODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(RQCODE) <= '" + OracleDB.QRText(data.REQCODETO.Trim()).ToUpper() + "' ";

            if (data.REQUESTDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.REQUESTDATEFROM) + " ";

            if (data.REQUESTDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.REQUESTDATETO) + " ";


            string sql = "SELECT RQLOID REFLOID,POLOID LOID,RQCODE CODE,REQDATE," + Constz.ProductionDepartment.LOID.ToString() + " AS S_LOID,'' AS S_CODE, ";
            sql += " '" + Constz.ProductionDepartment.Name + "' AS NAME,PDLOID, PDNAME FROM V_REQUISITION_PROD_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetOrderProductList(StockInFGData data)
        {
            string where = " REMAIN > 0 ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PDLOID = " + data.PRODUCT.ToString() + " ";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.SENDER != 0)
                where += (where == "" ? "" : "AND ") + "SPLOID = " + data.SENDER.ToString() + " ";

            string sql = "SELECT * FROM (SELECT PRI.DUEDATE ,PD.BARCODE,PD.LOID PDLOID, PD.NAME PDNAME, PD.PRICE,PO.LOID, PO.CODE,POI.QTY,A.AMOUNT,POI.QTY-NVL(A.AMOUNT,0) AS REMAIN,U.LOID UNIT, U.NAME UNITNAME,SP.LOID SPLOID,SP.SUPPLIERNAME SUPPLIER,POI.LOID REFLOID ";
            sql += "FROM PDORDER PO INNER JOIN POITEM POI ON POI.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRITEM PRI ON POI.PRITEM = PRI.LOID INNER JOIN PDREQUEST PR ON PR.LOID = PRI.PDREQUEST ";
            sql += "INNER JOIN V_PRODUCT_LIST PD ON POI.PRODUCT = PD.LOID AND PD.TYPE = 'FG' INNER JOIN UNIT U ON POI.UNIT = U.LOID ";
            sql += "INNER JOIN SUPPLIER SP ON PO.SUPPLIER = SP.LOID ";
            sql += "LEFT JOIN (SELECT REFLOID,SUM(QTY) AMOUNT FROM STOCKINITEM GROUP BY REFLOID,REFTABLE HAVING REFTABLE = 'POITEM') A ";
            sql += "ON A.REFLOID = POI.LOID)A ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductWHList(StockInFGData data)
        {
            string where = " REMAIN > 0 ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PDLOID = " + data.PRODUCT.ToString() + " ";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.SENDER != 0)
                where += (where == "" ? "" : "AND ") + "SPLOID = " + data.SENDER.ToString() + " ";

            string sql = "SELECT * FROM V_STOCKINSUPPLIERWH_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductOTList(StockInFGData data)
        {
            string where = " REMAIN > 0 ";

            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PDLOID = " + data.PRODUCT.ToString() + " ";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.SENDER != 0)
                where += (where == "" ? "" : "AND ") + "SPLOID = " + data.SENDER.ToString() + " ";

            string sql = "SELECT * FROM V_STOCKINSUPPLIEROT_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPRList(PopupProductPRSearchData data)
        {
            string where = "QTY > 0 ";

            if (data.PRCODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRCODE) >= '" + OracleDB.QRText(data.PRCODEFROM.Trim()).ToUpper() + "' ";

            if (data.PRCODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRCODE) <= '" + OracleDB.QRText(data.PRCODETO.Trim()).ToUpper() + "' ";

            if (data.DUEDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DUEDATEFROM) + " ";

            if (data.DUEDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DUEDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DUEDATETO) + " ";

            if (data.PURCHASETYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PURCHASETYPE) = '" + OracleDB.QRText(data.PURCHASETYPE.Trim()).ToUpper() + "' ";

            if (data.PRODUCT.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PDNAME) = '" + OracleDB.QRText(data.PRODUCT.Trim()).ToUpper() + "' ";

            if (data.DIVISION.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(DVNAME) = '" + OracleDB.QRText(data.DIVISION.Trim()).ToUpper() + "' ";

            string sql = "SELECT * FROM v_product_po_popup_list ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY PRCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetPOList(PopupPOSearchData data)
        {
            string where = "STATUS = 'AP' AND LOID NOT IN (SELECT POOLD FROM POEDIT)";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(ORDERDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(ORDERDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.SUPPLIER.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(SUPPLIER) = '" + OracleDB.QRText(data.SUPPLIER.Trim()).ToUpper() + "' ";

            string sql = "SELECT * FROM (SELECT PO.LOID,PO.CODE,PO.ORDERDATE,PO.SUPPLIER,S.SUPPLIERNAME,PO.STATUS FROM PDORDER PO INNER JOIN SUPPLIER S ON PO.SUPPLIER = S.LOID) A ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductReturnList(PopupStockoutSearchData data)
        {
            string where = "STATUS = 'AP' AND LOID NOT IN (SELECT REFLOID FROM PDRETURN WHERE REFTABLE = 'STOCKOUT') ";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CREATEON, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CREATEON, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) = '" + OracleDB.QRText(data.PRODUCTNAME.Trim()).ToUpper() + "' ";


            string sql = "SELECT A.* FROM (SELECT ST.LOID,ST.CODE,ST.CREATEON,ST.STATUS,STI.LOID STICODE,STI.PRODUCT,PD.NAME PRODUCTNAME,STI.QTY,STI.UNIT,U.NAME UNITNAME FROM STOCKOUT ST ";
            sql += "INNER JOIN (SELECT MIN(LOID) LOID,STOCKOUT FROM STOCKOUTITEM GROUP BY STOCKOUT)A ON ST.DOCTYPE IN (9,10) AND ST.LOID = A.STOCKOUT ";
            sql += "INNER JOIN STOCKOUTITEM STI ON A.LOID = STI.LOID INNER JOIN PRODUCT PD ON STI.PRODUCT = PD.LOID ";
            sql += "INNER JOIN UNIT U ON STI.UNIT = U.LOID)A ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY LOID ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetBasketList(PopupStockoutBasketData data)
        {
            string where = "ACTIVE = '1' ";

            if (data.BARCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(BARCODE) LIKE '%" + OracleDB.QRText(data.BARCODE.Trim()).ToUpper() + "%' ";

            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(NAME) LIKE '%" + OracleDB.QRText(data.NAME.Trim()).ToUpper() + "%' ";

            string sql = "SELECT LOID, BARCODE, NAME ";
            sql += "FROM V_PRODUCT_PACKAGE ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetStockinReturnList(PopupStockinReturnData data)
        {
            string where = "";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(DOCCODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(DOCCODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DOCDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(DOCDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.CUSTOMERNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CUSTOMERNAME) LIKE '%" + OracleDB.QRText(data.CUSTOMERNAME.Trim()).ToUpper() + "%' ";

            if (data.REFLOID != 0)
                where += (where == "" ? "" : "AND ") + "DOCTYPE = " + data.REFLOID.ToString() + " ";

            string sql = "SELECT REFLOID, DOCCODE, DOCDATE, CUSTOMERNAME, CUSTOMER CUSTOMERLOID, DOCTYPE ";
            sql += "FROM V_RETURN_WAIT_DOC ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY DOCCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetStockinReturnWHList(PopupStockinReturnData data)
        {
            string where = "";

            if (data.LOTNO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "LOTNO >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.BARCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "BARCODE <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + OracleDB.QRText(data.CUSTOMERNAME.Trim()).ToUpper() + "%' ";

            if (data.REFLOID != 0)
                where += (where == "" ? "" : "AND ") + "DOCTYPE = " + data.REFLOID.ToString() + " ";

            string sql = "SELECT * ";
            sql += "FROM V_MATERIAL_RETURN_POPUP_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetStockinReturnSearchList(PopupStockinReturnSearchData data)
        {
            string where = "QTY > 0 AND CUSTOMER = " + data.CUSTOMER.ToString() + " ";

            if (data.DOCTYPE != 0)
                where += (where == "" ? "" : "AND ") + "DOCTYPE = " + data.DOCTYPE.ToString() + " ";

            //if (data.CUSTOMER != 0)
            //    where += (where == "" ? "" : "AND ") + "CUSTOMER = " + data.CUSTOMER.ToString() + " ";

            if (data.REFLOID != 0)
                where += (where == "" ? "" : "AND ") + "REFLOID = " + data.REFLOID.ToString() + " ";

            string sql = "SELECT PDLOID, PDNAME, LOTNO, QTY, UNAME, DOCCODE, DOCNAME, REFLOID ";
            sql += "FROM V_RETURN_WAIT_LIST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY PDNAME, DOCCODE DESC,LOTNO DESC";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPlanList(SearchProductPlanData data)
        {
            string where = "PT.TYPE = '" + Constz.ProductType.Type.FG.Code + "' ";

            if (data.PLAN != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID NOT IN (SELECT PRODUCT FROM PLANORDER WHERE PLAN = " + data.PLAN.ToString() + ") ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "P.NAME LIKE '%" + data.PRODUCTNAME.Trim() + "%' ";

            string sql = "SELECT P.LOID, P.BARCODE, P.NAME, PG.NAME PRODUCTGROUP, PT.NAME PRODUCTTYPE ";
            sql += "FROM PRODUCT P INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP AND P.ACTIVE = '" + Constz.ActiveStatus.Active + "' AND P.ISDEFAULT = 'Y' ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY P.BARCODE, P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetInvoiceList(SearchInvoiceData data)
        {
            string where = "";
            if (data.CUSTOMER.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CUSTOMERNAME) LIKE '%" + data.CUSTOMER.Trim().ToUpper() + "%' ";

            if (data.INVCODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(INVCODE) >= '%" + data.INVCODEFROM.Trim().ToUpper() + "%' ";

            if (data.INVCODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(INVCODE) <= '%" + data.INVCODETO.Trim().ToUpper() + "%' ";

            if (data.REQUISITIONTYPE != 0)
                where += (where == "" ? "" : "AND ") + "REFTYPELOID = " + data.REQUISITIONTYPE.ToString() + " ";

            string sql = "SELECT LOID, INVCODE, TYPENAME, CUSTOMERNAME FROM V_INVOICE_FOR_DELIVERLY ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY TYPENAME, CUSTOMERNAME, INVCODE";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetInvoiceRequest(InvoiceRequestSearchData data, double currentInvoice)
        {
            string where = "REQUISITION NOT IN (SELECT REFLOID FROM REQUISITION WHERE REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ05.ToString() + " ";
            where += "AND REFTABLE = 'REQUISITION' AND STATUS <> '" + Constz.Requisition.Status.Void.Code + "' AND LOID<> " + currentInvoice.ToString() + ") ";

            if (data.INVCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(INVCODE) LIKE '%" + data.INVCODE.Trim().ToUpper() + "%' ";

            if (data.CUSTOMERCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CUSTOMERCODE) LIKE '%" + data.CUSTOMERCODE.Trim().ToUpper() + "%' ";

            if (data.CUSTOMERNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CUSTOMERNAME) LIKE '%" + data.CUSTOMERNAME.Trim().ToUpper() + "%' ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            string sql = "SELECT DISTINCT REQUISITION LOID, INVCODE, REQDATE, CUSTOMERNAME FROM V_INVOICE_FOR_REQUEST ";
            sql += (where == "" ? "" : "WHERE " + where);
            sql += "ORDER BY INVCODE, REQDATE, CUSTOMERNAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductReserveList(SearchProductData data)
        {
            string where = "";
            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(NAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.LOIDLIST.Trim() != "")
                where += (where == "" ? "" : "AND ") + "LOID NOT IN (" + data.LOIDLIST.Trim() + ") ";

            string sql = "SELECT LOID, CODE BARCODE, NAME, GNAME, PTNAME FROM V_PRODUCT_LIST ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY GNAME, NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductShopList(SearchProductData data)
        {
            string where = "";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "(UPPER(BARCODE) = '" + data.CODE.Trim().ToUpper() + "' OR UPPER(ABBNAME) = '" + data.CODE.Trim().ToUpper() + "' )";

            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(NAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            string sql = "SELECT LOID, BARCODE, NAME PNAME, GNAME PRODUCTGROUP, PTNAME PRODUCTTYPE FROM V_PRODUCT_LIST ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductPRList(SearchProductData data)
        {
            string where = "";
            if (data.NAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PDNAME) LIKE '%" + data.NAME.Trim().ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            string sql = "SELECT LOID, CODE, PDNAME, PGNAME, PTNAME FROM V_PRODUCT_PR_LIST ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY PGNAME, PDNAME ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
