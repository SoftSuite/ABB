using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class PurchaseDAL
    {
        public DataTable GetReserveList(ProductReserveSearchData whereData, string sortField)
        {
            string whereString = "";
            if (whereData.REQUISITIONTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "REQUISITIONTYPE = " + whereData.REQUISITIONTYPE.ToString() + " ";
            if (whereData.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(whereData.CODEFROM.Trim()).ToUpper() + "' ";
            if (whereData.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(whereData.CODETO.Trim()).ToUpper() + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RESERVEDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "RESERVEDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.CUSTOMERNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(CUSTOMERNAME) LIKE '%" + OracleDB.QRText(whereData.CUSTOMERNAME.Trim()).ToUpper() + "%' ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";
            if (whereData.WAREHOUSE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "WAREHOUSE = " + whereData.WAREHOUSE.ToString() + " ";
            if (sortField == "") sortField = "REQUISITIONTYPENAME, CODE ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RT.NAME REQUISITIONTYPENAME, RQ.CODE, RQ.LOID, RQ.REQUISITIONTYPE, RQ.REQDATE, RQ.RESERVEDATE, RQ.DUEDATE, CU.NAME || ' ' || CU.LASTNAME AS CUSTOMERNAME, ";
            sql += "RQ.WAREHOUSE, CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.QC.Code + "' THEN '" + Constz.Requisition.Status.QC.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Rank + "' ";
            sql += "ELSE '' END AS RANK, RQ.CREATEBY, ";
            sql += "(SELECT COUNT(*) FROM STOCKOUT WHERE REFTABLE = 'REQUISITION' AND REFLOID = RQ.LOID AND STATUS <> '" + Constz.Requisition.Status.Void.Code + "' ) CNT ";
            sql += "FROM REQUISITION RQ INNER JOIN V_REQTYPE_RESERVE RT ON RQ.REQUISITIONTYPE = RT.LOID ";
            sql += "LEFT JOIN CUSTOMER CU ON RQ.CUSTOMER = CU.LOID ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += (sortField == "" ? "" : "ORDER BY " + sortField);

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReserveItemList(double requisition, double warehouse)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.QTY, RQI.UNIT, RQI.PRICE, RQI.DISCOUNT, RQI.PRICE*RQI.QTY NETPRICE, RQI.ACTIVE, P.BARCODE, ";
            sql += "UNIT.NAME UNITNAME, RQI.ISVAT, FN_GETPRODUCTDISCOUNT(RQI.PRODUCT, RQ.WAREHOUSE, P.ISDISCOUNT) NORMALDISCOUNT, P.ISDISCOUNT, ";
            sql += "FN_GETPRODUCTSTOCKQTY(1, " + Constz.Zone.Z01.ToString() + ", P.PRODUCTMASTER) STOCKQTY, P.NAME PRODUCTNAME ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "INNER JOIN REQUISITION RQ ON RQ.LOID = RQI.REQUISITION ";
            sql += "INNER JOIN PRODUCTGROUP PG ON P.PRODUCTGROUP = PG.LOID ";
            sql += "WHERE RQI.REQUISITION = " + requisition + " ORDER BY PG.NAME, P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReserveItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.QTY, RQI.UNIT, RQI.PRICE, RQI.DISCOUNT, RQI.NETPRICE, RQI.ACTIVE, P.BARCODE, ";
            sql += "UNIT.NAME UNITNAME, RQI.ISVAT, FN_GETPRODUCTSTOCKQTY(0, " + Constz.Zone.Z01.ToString() + ", P.PRODUCTMASTER) STOCKQTY, P.NAME PRODUCTNAME ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN REQUISITION RQ ON RQ.LOID = RQI.REQUISITION ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetReqProductionItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM NO, RQI.PRODUCT, RQI.MASTER QTY, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, RQI.MASTER*P.PRICE AS NETPRICE, RQI.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, RQI.LOID REFLOID ";
            sql += "FROM REQMATERIAL RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionItemList(double product)
        {
            string sql = "SELECT B.LOID, ROWNUM NO, B.MATERIAL AS PRODUCT, B.MASTER QTY, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, B.MASTER*P.PRICE AS NETPRICE, B.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, B.LOID REFLOID ";
            sql += "FROM BOM B INNER JOIN PRODUCT P ON P.LOID = B.MATERIAL ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = B.UNIT ";
            sql += "WHERE B.MAINPRODUCT = " + product;
            return OracleDB.ExecListCmd(sql);
        }


        public DataTable GetProductStockList( double productBarcode,double warehouse)
        {
            double zone = Constz.Zone.Z04; 
            string sql = "SELECT PS.* ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.WAREHOUSE = " + warehouse.ToString() + " AND P.LOID = " + productBarcode.ToString() + " AND ZONE = " + zone.ToString() + " ORDER BY PS.LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStockListFG(double productBarcode, double warehouse)
        {
            double zone = Constz.Zone.Z01;
            string sql = "SELECT PS.* ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.WAREHOUSE = " + warehouse.ToString() + " AND P.LOID = " + productBarcode.ToString() + " AND ZONE = " + zone.ToString() + " ORDER BY PS.LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public string DoGetInvNo(string lotno,double warehouse,string product)
        {
            double zone = Constz.Zone.Z04;
            string sql = "SELECT INVNO ";
            sql += "FROM V_PRODUCT_SUPPLIER ";
            sql += "WHERE WAREHOUSE = " + warehouse.ToString() + " AND LOTNO = '" + lotno.ToString() + "' AND ZONE = " + zone.ToString() + " AND LOID = " + product.ToString() + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string invno = "";
            if (dt.Rows.Count > 0)
            {
                invno = dt.Rows[0]["INVNO"].ToString();
            }

            return invno;
        }

        public string DoGetPrice(string lotno, double warehouse, string product)
        {
            double zone = Constz.Zone.Z04;
            string sql = "SELECT PRICE ";
            sql += "FROM V_PRODUCT_SUPPLIER ";
            sql += "WHERE WAREHOUSE = " + warehouse.ToString() + " AND LOTNO = '" + lotno.ToString() + "' AND ZONE = " + zone.ToString() + " AND LOID = " + product.ToString() + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string price = "";
            if (dt.Rows.Count > 0)
            {
                price = dt.Rows[0]["PRICE"].ToString();
            }

            return price;
        }

        public string DoGetInvNoFG(string lotno, double warehouse, string product)
        {
            double zone = Constz.Zone.Z01;
            string sql = "SELECT INVNO ";
            sql += "FROM V_PRODUCT_SUPPLIER ";
            sql += "WHERE WAREHOUSE = " + warehouse.ToString() + " AND LOTNO = '" + lotno.ToString() + "' AND ZONE = " + zone.ToString() + " AND LOID = " + product.ToString()+ " " ;

            DataTable dt = OracleDB.ExecListCmd(sql);
            string invno = "";
            if (dt.Rows.Count > 0)
            {
                invno = dt.Rows[0]["INVNO"].ToString();
            }

            return invno;
        }

        public string DoGetPriceFG(string lotno, double warehouse, string product)
        {
            double zone = Constz.Zone.Z01;
            string sql = "SELECT PRICE ";
            sql += "FROM V_PRODUCT_SUPPLIER ";
            sql += "WHERE WAREHOUSE = " + warehouse.ToString() + " AND LOTNO = '" + lotno.ToString() + "' AND ZONE = " + zone.ToString() + " AND LOID = " + product.ToString() + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string price = "";
            if (dt.Rows.Count > 0)
            {
                price = dt.Rows[0]["PRICE"].ToString();
            }

            return price;
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
            string sql = "SELECT 0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT, ";
            sql += "0 NORMALDISCOUNT, 0 STOCKQTY, '' PRODUCTNAME ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
