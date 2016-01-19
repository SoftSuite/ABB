using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using System.Collections;
using ABB.DAL;
using ABB.Data;

namespace ABB.Flow.Reports
{
    public class ReportsFlow
    {
        public static  bool CheckSaleSummaryReport(string DateFrom, string DateTo,string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(RQ.LOID) CNT ";   
            str += " FROM REQUISITION RQ ";
            str += " INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION=RQ.LOID ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=RQI.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID=PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID=PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID=PD.UNIT ";
            str += " WHERE RQ.REQUISITIONTYPE=1 AND RQ.STATUS='AP' AND RQ.WAREHOUSE = " + warehouse +"";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND TO_CHAR(RQ.REQDATE,'YYYY/MM/DD') BETWEEN '"+ DateFrom +"' AND '"+ DateTo +"' ";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
       }

        public static bool CheckSaleSummaryDailyReport(string year)
        {
            string str = "";
            str = " SELECT COUNT(*) CNT ";
            str += " FROM V_SALESUMMARYDAILY ";
            str += " WHERE YEARS = '" + year + "'";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckProductSaleSummaryReport(string DateFrom, string DateTo, string PTLoid, string PGLoid, string Product, string Customer,string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(RQ.LOID) CNT ";
            str += " FROM REQUISITION RQ ";
            str += " INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION=RQ.LOID ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=RQI.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID=PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID=PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID=PD.UNIT ";
            str += " INNER JOIN V_CUSTOMER C ON C.LOID=RQ.CUSTOMER ";
            str += " WHERE RQ.REQUISITIONTYPE=1 AND RQ.STATUS='AP' AND RQ.WAREHOUSE = "+ warehouse +"";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND RQ.REQDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (PGLoid != "0")
                str += " AND PG.LOID = " + PGLoid;

            if (PTLoid != "0")
                str += " AND PT.LOID ="+ PTLoid;

            if (Product != "0")
                str += " AND PD.LOID = " + Product;

            if (Customer != "")
                str += " AND C.CUSTOMERNAME LIKE '%" + Customer + "%'";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckProductProductionSummaryReport(string DateFrom, string DateTo, string PGLoid)
        {
            string str = "";
            str = " SELECT COUNT(*) CNT ";
            str += "  FROM PDPRODUCT  PPD ";
            str += " INNER JOIN PRODUCT PD ON PPD.PRODUCT = PD.LOID ";
            str += " INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
            str += " INNER JOIN UNIT U2 ON PPD.BATCHSIZEUNIT = U2.LOID ";
            str += " WHERE PPD.PRODSTATUS='AP' ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND PPD.MFGDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (PGLoid != "99")
                str += " AND PD.PRODUCEGROUP = " + PGLoid;


            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckScheduleProduce(int year, string month, string product)
        {
            string str = "";
            str = " SELECT COUNT(*) CNT ";
            str += " FROM (SELECT PD.NAME PRODUCTNAME,TO_NUMBER(TO_CHAR(PP.MFGDATE,'MM')) MONTH,SUM(PP.STDQTY) QTY ";
            str += " FROM PDPRODUCT PP INNER JOIN PRODUCT PD ON PP.PRODUCT = PD.LOID WHERE  TO_NUMBER(TO_CHAR(PP.MFGDATE,'yyyy')) ="+ year;
            str += " GROUP BY PD.NAME,TO_NUMBER(TO_CHAR(PP.MFGDATE,'MM')) ) A ";
            str += " WHERE MONTH = '" + month + "' ";

            if (product != "")
                str += " AND PRODUCTNAME LIKE '%" + product + "%'";


            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }



        public static bool StockMovementReport(string DateFrom, string DateTo, string producttype, string productgroup, string product, string warehouse, string FromZone, string Tozone)
        {
            string str = "";
            str = " SELECT COUNT(LOID) CNT ";
            str += " FROM V_STOCKMOVEMENT_SEARCH ";
            str += " WHERE LOID IS NOT NULL ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND TO_DATE(CDATE,'YYYY-MM-DD') BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (producttype != "0")
                str += " AND PTLOID =" + producttype;

            if (productgroup != "0")
                str += " AND PGLOID =" + productgroup;

            if (product != "0")
                str += " AND PRODUCTNAME LIKE '%" + product + "%'";

            if (warehouse != "0")
                str += " AND FROMWAREHOUSE = " + warehouse;

            if (FromZone != "0")
                str += " AND FROMZONE =" + FromZone;

            if (Tozone != "0")
                str += " AND TOZONE = " + Tozone;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckSaleSummaryBillReport(string DateFrom, string DateTo, string InvcodeFrom, string InvcodeTo, string Customer, string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(RQ.LOID) CNT ";
            str += " FROM  REQUISITION RQ INNER JOIN V_CUSTOMER C ON C.LOID=RQ.CUSTOMER ";
            str += " WHERE RQ.REQUISITIONTYPE=1 AND RQ.STATUS='AP' AND RQ.WAREHOUSE ="+ warehouse +" ";

            if (DateFrom != "1/01/01" && DateTo != "1/01/01")
            {
                str += " AND TO_CHAR(NVL(RQ.REQDATE,RQ.RESERVEDATE),'YYYY/MM/DD') BETWEEN '" + DateFrom + "' AND '" + DateTo + "'";
            }

            if (InvcodeFrom != "" && InvcodeTo != "")
            {
                str += " AND ( RQ.INVCODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "'";
                str += " OR RQ.CODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "' )";
            }

            if (InvcodeFrom != "" )
            {
                str += " AND NVL(RQ.INVCODE,RQ.CODE) >= '" + InvcodeFrom + "'";
            }

            if (InvcodeTo != "")
            {
                str += " AND NVL(RQ.INVCODE,RQ.CODE) <= '" + InvcodeTo + "'";
            }

            if (Customer != "")
                str += " AND C.CUSTOMERNAME LIKE '%" + Customer + "%'";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckStockOutDocTypeReport(string DateFrom, string DateTo,string RiLoid,string InvcodeFrom, string InvcodeTo,string Customer, string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(RQ.LOID) CNT ";
            str += " FROM REQUISITION RQ INNER JOIN V_CUSTOMER C ON C.LOID=RQ.CUSTOMER ";
            str += " INNER JOIN V_REQTYPE_INVOICE RR ON RR.LOID=RQ.REQUISITIONTYPE ";
            str += " WHERE RQ.STATUS='AP' AND RQ.WAREHOUSE =" + warehouse +" ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
            {
                str += " AND ( TO_CHAR(RQ.REQDATE,'DD/MM/YYYY') BETWEEN '" + DateFrom + "' AND '" + DateTo + "' ";
                str += "  OR TO_CHAR(RQ.RESERVEDATE,'DD/MM/YYYY') BETWEEN '" + DateFrom + "' AND '" + DateTo + "' )";
            }

            if (InvcodeFrom != "" && InvcodeTo != "")
            {
                str += " AND ( RQ.INVCODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "'";
                str += " OR RQ.CODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "' )";
            }
            if (InvcodeFrom != "")
            {
                str += " AND NVL(RQ.INVCODE,RQ.CODE) >= '" + InvcodeFrom + "'";
            }

            if (InvcodeTo != "")
            {
                str += " AND NVL(RQ.INVCODE,RQ.CODE) <= '" + InvcodeTo + "'";
            }   

            if (Customer != "")
                str += " AND C.CUSTOMERNAME LIKE '%" + Customer + "%'";

            if (RiLoid != "0")
                str += " AND RR.LOID = " + RiLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckStockRemainReportReport(string PtLoid, string PgLoid, string PdLoid, string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(PS.LOID) CNT ";
            str += " FROM PRODUCTSTOCK PS INNER JOIN PRODUCTMASTER PM ON PM.LOID=PS.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID=PM.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID=PG.PRODUCTTYPE ";
            str += " INNER JOIN WAREHOUSE WH ON WH.LOID=PS.WAREHOUSE ";
            str += " INNER JOIN ZONE Z ON Z.LOID=PS.ZONE ";
            str += " WHERE Z.ISCOUNT = 'Y' AND PS.WAREHOUSE = " + warehouse + " ";

            if (PtLoid != "0")
                str += " AND PT.LOID = " + PtLoid;

            if (PgLoid != "0")
                str += " AND PG.LOID = " + PgLoid;

            if (PdLoid != "0")
                str += " AND PM.LOID = " + PdLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool BarcodeProductReport(string PtLoid, string PgLoid, string PdName)
        {
            string str = "";
            str = " SELECT COUNT(PD.LOID) CNT ";
            str += " FROM PRODUCT PD INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP=PG.LOID ";
            str += " INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE=PT.LOID ";
            str += " INNER JOIN UNIT U ON U.LOID=PD.UNIT ";
            str += " WHERE 1=1 ";

            if (PtLoid != "0")
                str += " AND PT.LOID = " + PtLoid;

            if (PgLoid != "0")
                str += " AND PG.LOID = " + PgLoid;

            if (PdName != "")
                str += " AND PD.NAME LIKE '%" + PdName + "%'";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool StockCheckBatchNoIncreaseReport(string ScLoid)
        {
            string str = "";
            str = " SELECT COUNT(SC.LOID) CNT ";
            str += " FROM STOCKCHECK SC INNER JOIN STOCKCHECKIMPROVE SCI ON SC.LOID=SCI.STOCKCHECK ";
            str += " INNER JOIN WAREHOUSE WH ON WH.LOID=SC.WAREHOUSE ";
            str += " INNER JOIN PRODUCTSTOCK PS ON PS.LOID=SCI.PRODUCTSTOCK ";
            str += " INNER JOIN PRODUCT PD ON PD.PRODUCTMASTER=PS.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID=PD.UNIT ";
            str += " WHERE SCI.IMPROVEQTY > 0 ";

            if (ScLoid != "0")
                str += " AND SC.LOID = " + ScLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool StockCheckBatchNoDecreaseReport(string ScLoid)
        {
            string str = "";
            str = " SELECT COUNT(SC.LOID) CNT ";
            str += " FROM STOCKCHECK SC INNER JOIN STOCKCHECKIMPROVE SCI ON SC.LOID=SCI.STOCKCHECK ";
            str += " INNER JOIN WAREHOUSE WH ON WH.LOID=SC.WAREHOUSE ";
            str += " INNER JOIN PRODUCTSTOCK PS ON PS.LOID=SCI.PRODUCTSTOCK ";
            str += " INNER JOIN PRODUCT PD ON PD.PRODUCTMASTER=PS.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID=PD.UNIT ";
            str += " WHERE SCI.IMPROVEQTY < 0 ";

            if (ScLoid != "0")
                str += " AND SC.LOID = " + ScLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckSendMoneyReport(string CodeFrom, string CodeTo)
        {
            string str = "";
            str = " SELECT COUNT(RQ.LOID) CNT ";
            str += " FROM REQUISITION RQ ";
            str += " WHERE RQ.REQUISITIONTYPE=13 AND RQ.STATUS='AP'";
            str += " AND RQ.CODE BETWEEN '" + CodeFrom +"' AND '"+ CodeTo +"'";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckProductStockoutReport(string DateFrom, string DateTo, string PTLoid, string PGLoid, string Product,string DocType)
        {
            string str = "";
            str = " SELECT COUNT(ST.LOID) CNT ";
            str += " FROM STOCKOUT ST INNER JOIN STOCKOUTITEM STI ON ST.LOID=STI.STOCKOUT ";
            str += " INNER JOIN DOCTYPE DT ON DT.LOID=ST.DOCTYPE ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=STI.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID=PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID=PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID=STI.UNIT ";
            str += " WHERE DT.WAREHOUSE<> 3 AND DT.TYPE='O' ";


            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND ST.REQDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (PGLoid != "0")
                str += " AND PG.LOID = " + PGLoid;

            if (PTLoid != "0")
                str += " AND PT.LOID =" + PTLoid;

            if (Product != "0")
                str += " AND PD.LOID = " + Product;

            if (DocType != "0")
                str += " AND DT.LOID = " + DocType;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckProductStockinReport(string DateFrom, string DateTo, string PTLoid, string PGLoid, string Product, string DocType,string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(ST.LOID) CNT ";
            str += " FROM STOCKIN ST INNER JOIN STOCKINITEM STI ON ST.LOID=STI.STOCKIN ";
            str += " INNER JOIN DOCTYPE DT ON DT.LOID=ST.DOCTYPE ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=STI.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID=PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID=PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID=STI.UNIT ";
            str += " WHERE  DT.TYPE='I' AND DT.WAREHOUSE = "+ warehouse +" ";


            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND ST.RECEIVEDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (PGLoid != "0")
                str += " AND PG.LOID = " + PGLoid;

            if (PTLoid != "0")
                str += " AND PT.LOID =" + PTLoid;

            if (Product != "0")
                str += " AND PD.LOID = " + Product;

            if (DocType != "0")
                str += " AND DT.LOID = " + DocType;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckStockOutYearSummaryReport(string year, string pdloid, string ptloid, string pgloid,string warehouse)
        {
            string str = "";

            str = " SELECT COUNT(ST.LOID) CNT ";
            str += " FROM STOCKOUTITEM STI INNER JOIN PRODUCT PD ON PD.LOID=STI.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID =  PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            str += " INNER JOIN STOCKOUT ST ON ST.LOID=STI.STOCKOUT ";
            str += " INNER JOIN DOCTYPE DT ON DT.LOID=ST.DOCTYPE ";
            str += " WHERE  ST.STATUS='AP' AND DT.WAREHOUSE ="+ warehouse +" ";

            if (year != "0")
                str += " AND TO_CHAR(ST.CREATEON,'YYYY')='"+ year +"'";

            if (pgloid != "0")
                str += " AND PG.LOID = " + pgloid;

            if (ptloid != "0")
                str += " AND PT.LOID =" + ptloid;

            if (pdloid != "0")
                str += " AND PD.LOID = " + pdloid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckStockinDocTypeReport(string DateFrom, string DateTo, string doctype, string InvcodeFrom, string InvcodeTo, string Customer)
        {
            string str = "";

            str = " SELECT COUNT(STLOID) CNT ";
            str += " FROM ( SELECT ST.LOID STLOID,DT.LOID DTLOID,ST.CREATEON,ST.CODE,ST.STATUS STSTATUS,0 AS CLOID ";
            str += " FROM STOCKIN ST LEFT JOIN REQUISITION RQ ON RQ.LOID=ST.REFLOID AND ST.REFTABLE='REQUISITION' ";
            str += " INNER JOIN DOCTYPE DT ON DT.LOID=ST.DOCTYPE ";
            str += " LEFT JOIN SUPPLIER S ON S.LOID=ST.SENDER ";
            str += " WHERE  ST.STATUS='AP' AND DT.TYPE='I' AND DT.WAREHOUSE<>3 ";
            str += " UNION ALL ";
            str += " SELECT ST.LOID STLOID,DT.LOID DTLOID,ST.CREATEON,ST.CODE ,ST.STATUS STSTATUS,C.LOID CLOID ";
            str += " FROM STOCKOUT ST INNER JOIN DOCTYPE DT ON DT.LOID=ST.DOCTYPE ";
            str += " LEFT JOIN REQUISITION RQ ON RQ.LOID=ST.REFLOID AND ST.REFTABLE='REQUISITION' ";
            str += " LEFT JOIN V_CUSTOMER C ON C.LOID=RQ.CUSTOMER ";
            str += " WHERE DT.TYPE='I' AND DT.WAREHOUSE<>3 )A ";
            str += " WHERE STSTATUS='AP' ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND TO_CHAR(CREATEON,'DD/MM/YYYY') BETWEEN '" + DateFrom + "' AND '" + DateTo + "'";

            if (InvcodeFrom != "" && InvcodeTo != "")
                str += " AND CODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "'";

            if (Customer != "0")
                str += " AND CLOID = " + Customer;

            if (doctype != "0")
                str += " AND DTLOID = " + doctype;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckPurchaseOrderReport(string DateFrom, string DateTo, string codeFrom, string codeTo, string SupLoid)
        {
            string str = "";
            str = " SELECT COUNT(POI.LOID) CNT ";
            str += " FROM POITEM POI INNER JOIN PDORDER PO ON PO.LOID=POI.PDORDER ";
            str += " INNER JOIN SUPPLIER SP ON SP.LOID=PO.SUPPLIER ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=POI.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID=POI.UNIT ";
            str += " INNER JOIN PRITEM PRI ON PRI.LOID=POI.PRITEM ";
            str += " INNER JOIN PDREQUEST PR ON PR.LOID=PRI.PDREQUEST ";
            str += " LEFT JOIN STOCKINITEM STI ON POI.LOID=STI.REFLOID AND STI.REFTABLE='POITEM' ";
            str += " LEFT JOIN STOCKIN ST ON ST.LOID=STI.STOCKIN ";
            str += " WHERE PO.ORDERTYPE='PO' AND PO.STATUS='AP' ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND PO.ORDERDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (codeFrom != "" && codeTo != "")
                str += " AND PO.CODE BETWEEN '" + codeFrom + "' AND '" + codeTo + "'";

            if (SupLoid != "0")
                str += " AND PO.SUPPLIER = " + SupLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckPurchaseOrderDetailReport(string DateFrom, string DateTo, string codeFrom, string codeTo, string SupLoid)
        {
            string str = "";
            str = " SELECT COUNT(POI.LOID) CNT ";
            str += " FROM POITEM POI INNER JOIN PDORDER PO ON PO.LOID=POI.PDORDER ";
            str += " INNER JOIN SUPPLIER SP ON SP.LOID=PO.SUPPLIER ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=POI.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID=POI.UNIT ";
            str += " INNER JOIN PRITEM PRI ON PRI.LOID=POI.PRITEM ";
            str += " INNER JOIN PDREQUEST PR ON PR.LOID=PRI.PDREQUEST ";
            str += " WHERE PO.ORDERTYPE='PO' AND PO.STATUS='AP'  ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND PO.ORDERDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY') ";

            if (codeFrom != "" && codeTo != "")
                str += " AND PO.CODE BETWEEN '" + codeFrom + "' AND '" + codeTo + "'";

            if (SupLoid != "0")
                str += " AND PO.SUPPLIER = " + SupLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckStockOutDocTypeShopReport(string DateFrom, string DateTo, string RiLoid, string InvcodeFrom, string InvcodeTo, string Customer, string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(CODE) ";
            str += " FROM (SELECT TO_CHAR(NVL(RQ.RESERVEDATE,RQ.REQDATE),'DD/MM/YYYY')  REQDATE,RQ.STATUS, ";
            str += " NVL(RQ.INVCODE, RQ.CODE) CODE, RQ.REFNO, RQ.CUSTOMER,C.CUSTOMERNAME,RQ.REQUISITIONTYPE,";
            str += " SS.DOCNAME REQTYPE, RQ.TOTDIS DISCOUNT, RQ.TOTAL PRICE, RQ.TOTVAT, RQ.GRANDTOT, ";
            str += " RQ.CASH-(RQ.CASH-RQ.GRANDTOT) CASH, RQ.CREDITCARDPAY, RQ.COUPON ";
            str += " FROM REQUISITION RQ INNER JOIN V_CUSTOMER C ON C.LOID=RQ.CUSTOMER ";
            str += " INNER JOIN V_SHOP_STOCKOUT_REPORT_TYPE SS ON SS.REFLOID=RQ.REQUISITIONTYPE AND SS.REFTABLE='REQUISITIONTYPE' ";
            str += " WHERE RQ.WAREHOUSE = " + warehouse + " ";
            str += " UNION ";
            str += " SELECT TO_CHAR(ST.REQDATE,'DD/MM/YYYY') REQDATE, ST.STATUS, ST.CODE, '', W.LOID,W.NAME WAREHOUSE, ";
            str += " ST.DOCTYPE, SS.DOCNAME REQTYPE, NULL DISCOUNT, SUM(STI.PRICE) PRICE, NULL TOTVAT, ";
            str += " SUM(STI.PRICE) GRANDTOT, NULL,NULL,NULL ";
            str += " FROM STOCKOUT ST INNER JOIN STOCKOUTITEM STI ON ST.LOID=STI.STOCKOUT ";
            str += " INNER JOIN WAREHOUSE W ON W.LOID = ST.RECEIVER  ";
            str += " INNER JOIN V_SHOP_STOCKOUT_REPORT_TYPE SS ON SS.REFLOID=ST.DOCTYPE AND SS.REFTABLE='DOCTYPE' ";
            str += " WHERE W.LOID = " + warehouse + " ";
            str += " GROUP BY ST.REQDATE, ST.CODE,W.LOID, W.NAME, SS.REFLOID, ST.DOCTYPE, SS.DOCNAME, ST.STATUS)A ";
            str += " WHERE STATUS = 'AP' ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
            {
                str += " AND REQDATE BETWEEN '" + DateFrom + "' AND '" + DateTo + "'";
            }

            if (InvcodeFrom != "" && InvcodeTo != "")
                str += " AND CODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "'";

            if (Customer != "0")
                str += " AND CUSTOMER = " + Customer;

            if (RiLoid != "0")
                str += " AND REQUISITIONTYPE = " + RiLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckProductSaleSummaryInvoiceReport(string DateFrom, string DateTo, string PTLoid, string PGLoid, string Product)
        {
            string str = "";
            str = " SELECT COUNT(RQ.LOID) CNT ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION=RQ.LOID ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID=RQI.PRODUCT ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID=PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID=PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID=PD.UNIT  ";
            str += " WHERE RQ.REQUISITIONTYPE=13 AND RQ.STATUS='AP' ";

            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
                str += " AND TO_CHAR(RQ.REQDATE,'DD/MM/YYYY') BETWEEN '" + DateFrom + "' AND '" + DateTo + "' ";

            if (PGLoid != "0")
                str += " AND PG.LOID = " + PGLoid;

            if (PTLoid != "0")
                str += " AND PT.LOID =" + PTLoid;

            if (Product != "0")
                str += " AND PD.LOID = " + Product;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool CheckStockOutDocTypeSTReport(string DateFrom, string DateTo, string RiLoid, string InvcodeFrom, string InvcodeTo, string Customer, string warehouse)
        {
            string str = "";
            str = " SELECT COUNT(ST.LOID) ";
            str += " FROM STOCKOUT ST INNER JOIN REQUISITION RQ ON RQ.LOID=ST.REFLOID AND ST.REFTABLE='REQUISITION' ";
            str += " INNER JOIN DOCTYPE DT ON DT.LOID=ST.DOCTYPE ";
            str += " INNER JOIN V_CUSTOMER C ON C.LOID=ST.RECEIVER ";
            str += " WHERE  ST.STATUS='AP' AND DT.TYPE='O' AND DT.WAREHOUSE<>3 AND DT.WAREHOUSE = "+ warehouse +" ";


            if (DateFrom != "01/01/1" && DateTo != "01/01/1")
            {
                str += " AND TO_CHAR(ST.CREATEON,'DD/MM/YYYY') BETWEEN '" + DateFrom + "' AND '" + DateTo + "'";
            }

            if (InvcodeFrom != "" && InvcodeTo != "")
                str += " AND ST.CODE BETWEEN '" + InvcodeFrom + "' AND '" + InvcodeTo + "'";

            if (Customer != "0")
                str += " AND C.LOID = " + Customer;

            if (RiLoid != "0")
                str += " AND DT.LOID = " + RiLoid;

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool PlanSaleReport(double plan, double producttype, double productgroup, string product)
        {
            string str = "";
            str = " SELECT COUNT(PO.LOID) CNT ";
            str += " FROM PLANORDER PO INNER JOIN PRODUCT PD ON PO.PRODUCT = PD.LOID ";
            str += " INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP = PG.LOID INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID";
            str += " WHERE PO.PLAN = " + plan;

            if (producttype != 0)
                str += " AND PRODUCTTYPE = " + producttype;

            if (productgroup != 0)
                str += " AND PRODUCTGROUP = " + productgroup;

            if (product != "")
                str += " AND UPPER(PRODUCTNAME) LIKE '%" + product.ToUpper() + "%'";

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }

        public static bool PlanFGReport(double plan, double month, double producttype, double productgroup, string product, string status)
        {
            string str = "";
            str = "SELECT COUNT(V.LOID) CNT FROM V_PLANINVENTORY V LEFT JOIN PLANREMAIN R ON R.PLAN = V.PLAN AND R.PRODUCT = V.PRODUCT AND R.MONTH = V.MONTH ";
            str += "INNER JOIN PLANPRODUCE P ON P.PLAN = V.PLAN AND P.PRODUCT = V.PRODUCT AND P.MONTH = V.MONTH ";
            str += "INNER JOIN PLANPURCHASE S ON S.PLAN = V.PLAN AND S.PRODUCT = V.PRODUCT AND S.MONTH = V.MONTH ";
            str += " WHERE V.PLAN = " + plan + "AND V.MONTH = " + month;

            if (producttype != 0)
                str += " AND V.PTYPE = " + producttype;

            if (productgroup != 0)
                str += " AND V.PGROUP = " + productgroup;

            if (product != "")
                str += " AND UPPER(V.PRODUCTNAME) LIKE '%" + product.ToUpper() + "%'";

            switch (status)
            {
                case Constz.PlanProductStatus.Minimum:
                    str += "AND (R.DAY1 < NVL(MINIMUM,0) OR R.DAY2 < NVL(MINIMUM,0) OR R.DAY3 < NVL(MINIMUM,0) OR R.DAY4 < NVL(MINIMUM,0) OR R.DAY5 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY6 < NVL(MINIMUM,0) OR R.DAY7 < NVL(MINIMUM,0) OR R.DAY8 < NVL(MINIMUM,0) OR R.DAY9 < NVL(MINIMUM,0) OR R.DAY10 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY11 < NVL(MINIMUM,0) OR R.DAY12 < NVL(MINIMUM,0) OR R.DAY13 < NVL(MINIMUM,0) OR R.DAY14 < NVL(MINIMUM,0) OR R.DAY15 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY16 < NVL(MINIMUM,0) OR R.DAY17 < NVL(MINIMUM,0) OR R.DAY18 < NVL(MINIMUM,0) OR R.DAY19 < NVL(MINIMUM,0) OR R.DAY20 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY21 < NVL(MINIMUM,0) OR R.DAY22 < NVL(MINIMUM,0) OR R.DAY23 < NVL(MINIMUM,0) OR R.DAY24 < NVL(MINIMUM,0) OR R.DAY25 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY26 < NVL(MINIMUM,0) OR R.DAY27 < NVL(MINIMUM,0) OR R.DAY28 < NVL(MINIMUM,0) OR R.DAY29 < NVL(MINIMUM,0) OR R.DAY30 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY31 < NVL(MINIMUM,0) ) ";
                    break;

                case Constz.PlanProductStatus.Produce:
                    str += "AND (P.DAY1>0 OR P.DAY2>0 OR P.DAY3>0 OR P.DAY4>0 OR P.DAY5>0 OR ";
                    str += "P.DAY6>0 OR P.DAY7>0 OR P.DAY8>0 OR P.DAY9>0 OR P.DAY10>0 OR ";
                    str += "P.DAY11>0 OR P.DAY12>0 OR P.DAY13>0 OR P.DAY14>0 OR P.DAY15>0 OR ";
                    str += "P.DAY16>0 OR P.DAY17>0 OR P.DAY18>0 OR P.DAY19>0 OR P.DAY20>0 OR ";
                    str += "P.DAY21>0 OR P.DAY22>0 OR P.DAY23>0 OR P.DAY24>0 OR P.DAY25>0 OR ";
                    str += "P.DAY26>0 OR P.DAY27>0 OR P.DAY28>0 OR P.DAY29>0 OR P.DAY30>0 OR ";
                    str += "P.DAY31>0 ) ";
                    break;

                case Constz.PlanProductStatus.Purchase:
                    str += "AND (S.DAY1>0 OR S.DAY2>0 OR S.DAY3>0 OR S.DAY4>0 OR S.DAY5>0 OR ";
                    str += "S.DAY6>0 OR S.DAY7>0 OR S.DAY8>0 OR S.DAY9>0 OR S.DAY10>0 OR ";
                    str += "S.DAY11>0 OR S.DAY12>0 OR S.DAY13>0 OR S.DAY14>0 OR S.DAY15>0 OR ";
                    str += "S.DAY16>0 OR S.DAY17>0 OR S.DAY18>0 OR S.DAY19>0 OR S.DAY20>0 OR ";
                    str += "S.DAY21>0 OR S.DAY22>0 OR S.DAY23>0 OR S.DAY24>0 OR S.DAY25>0 OR ";
                    str += "S.DAY26>0 OR S.DAY27>0 OR S.DAY28>0 OR S.DAY29>0 OR S.DAY30>0 OR ";
                    str += "S.DAY31>0 ) ";
                    break;
            }

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }
        public static bool PlanWHReport(double plan, double month, double producttype, double productgroup, string product, string status)
        {
            string str = "";
            str = "SELECT COUNT(V.LOID) CNT FROM V_PLANWH V LEFT JOIN PLANREMAIN R ON R.PLAN = V.PLAN AND R.PRODUCT = V.PRODUCT AND R.MONTH = V.MONTH ";
            str += "INNER JOIN PLANPRODUCE P ON P.PLAN = V.PLAN AND P.PRODUCT = V.PRODUCT AND P.MONTH = V.MONTH ";
            str += "INNER JOIN PLANPURCHASE S ON S.PLAN = V.PLAN AND S.PRODUCT = V.PRODUCT AND S.MONTH = V.MONTH ";
            str += " WHERE V.PLAN = " + plan + "AND V.MONTH = " + month;

            if (producttype != 0)
                str += " AND V.PTYPE = " + producttype;

            if (productgroup != 0)
                str += " AND V.PGROUP = " + productgroup;

            if (product != "")
                str += " AND UPPER(V.PRODUCTNAME) LIKE '%" + product.ToUpper() + "%'";

            switch (status)
            {
                case Constz.PlanProductStatus.Minimum:
                    str += "AND (R.DAY1 < NVL(MINIMUM,0) OR R.DAY2 < NVL(MINIMUM,0) OR R.DAY3 < NVL(MINIMUM,0) OR R.DAY4 < NVL(MINIMUM,0) OR R.DAY5 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY6 < NVL(MINIMUM,0) OR R.DAY7 < NVL(MINIMUM,0) OR R.DAY8 < NVL(MINIMUM,0) OR R.DAY9 < NVL(MINIMUM,0) OR R.DAY10 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY11 < NVL(MINIMUM,0) OR R.DAY12 < NVL(MINIMUM,0) OR R.DAY13 < NVL(MINIMUM,0) OR R.DAY14 < NVL(MINIMUM,0) OR R.DAY15 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY16 < NVL(MINIMUM,0) OR R.DAY17 < NVL(MINIMUM,0) OR R.DAY18 < NVL(MINIMUM,0) OR R.DAY19 < NVL(MINIMUM,0) OR R.DAY20 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY21 < NVL(MINIMUM,0) OR R.DAY22 < NVL(MINIMUM,0) OR R.DAY23 < NVL(MINIMUM,0) OR R.DAY24 < NVL(MINIMUM,0) OR R.DAY25 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY26 < NVL(MINIMUM,0) OR R.DAY27 < NVL(MINIMUM,0) OR R.DAY28 < NVL(MINIMUM,0) OR R.DAY29 < NVL(MINIMUM,0) OR R.DAY30 < NVL(MINIMUM,0) OR ";
                    str += "R.DAY31 < NVL(MINIMUM,0) ) ";
                    break;

                case Constz.PlanProductStatus.Produce:
                    str += "AND (P.DAY1>0 OR P.DAY2>0 OR P.DAY3>0 OR P.DAY4>0 OR P.DAY5>0 OR ";
                    str += "P.DAY6>0 OR P.DAY7>0 OR P.DAY8>0 OR P.DAY9>0 OR P.DAY10>0 OR ";
                    str += "P.DAY11>0 OR P.DAY12>0 OR P.DAY13>0 OR P.DAY14>0 OR P.DAY15>0 OR ";
                    str += "P.DAY16>0 OR P.DAY17>0 OR P.DAY18>0 OR P.DAY19>0 OR P.DAY20>0 OR ";
                    str += "P.DAY21>0 OR P.DAY22>0 OR P.DAY23>0 OR P.DAY24>0 OR P.DAY25>0 OR ";
                    str += "P.DAY26>0 OR P.DAY27>0 OR P.DAY28>0 OR P.DAY29>0 OR P.DAY30>0 OR ";
                    str += "P.DAY31>0 ) ";
                    break;

                case Constz.PlanProductStatus.Purchase:
                    str += "AND (S.DAY1>0 OR S.DAY2>0 OR S.DAY3>0 OR S.DAY4>0 OR S.DAY5>0 OR ";
                    str += "S.DAY6>0 OR S.DAY7>0 OR S.DAY8>0 OR S.DAY9>0 OR S.DAY10>0 OR ";
                    str += "S.DAY11>0 OR S.DAY12>0 OR S.DAY13>0 OR S.DAY14>0 OR S.DAY15>0 OR ";
                    str += "S.DAY16>0 OR S.DAY17>0 OR S.DAY18>0 OR S.DAY19>0 OR S.DAY20>0 OR ";
                    str += "S.DAY21>0 OR S.DAY22>0 OR S.DAY23>0 OR S.DAY24>0 OR S.DAY25>0 OR ";
                    str += "S.DAY26>0 OR S.DAY27>0 OR S.DAY28>0 OR S.DAY29>0 OR S.DAY30>0 OR ";
                    str += "S.DAY31>0 ) ";
                    break;
            }

            object sum = OracleDB.ExecSingleCmd(str);

            if (Convert.ToDouble(sum) == 0)
                return false;
            else
                return true;
        }
    }
}
