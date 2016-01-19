using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data.Sales;
using ABB.Data.Inventory.WH;
using ABB.Data;

namespace ABB.DAL.Inventory.WH
{
    public class StockWHDAL
    {
        string _error = "";
        public string ErrorMessage
        {
            get { return _error; }
            set { _error = value; }
        }
        public DataTable GetProductionList(ProductReserveSearchData whereData)
        {
            string whereString = "DOCLOID IN (" + Constz.DocType.ReqRawPD.LOID.ToString() + "," + Constz.DocType.ReqRawPO.LOID.ToString() + ") ";
            if (whereData.REQUISITIONTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "DOCLOID = '" + whereData.REQUISITIONTYPE.ToString() + "' ";
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
            if (whereData.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(whereData.STATUSFROM.Trim()) + "' ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(whereData.STATUSTO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT ST.LOID LOID,ST.DOCTYPE DOCLOID,DT.DOCNAME DOCTYPE,NVL(VPL.POCODE,VRP.RQCODE) REQCODE,NVL(VPL.ORDERDATE,VRP.REQDATE) REQDATE,ST.CREATEBY ,ST.CREATEON, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, ST.CODE STOCKCODE,NVL(VPL.PD_LOID,VRP.PDLOID) PRODUCT,S.SUPPLIERNAME AS CUSTOMERNAME  ";
            sql += "FROM STOCKOUT ST  LEFT JOIN DOCTYPE DT ON ST.DOCTYPE = DT.LOID  ";
            sql += "LEFT JOIN V_PDORDER_LIST VPL ON ST.REFTABLE = 'PDORDER' AND VPL.PO_LOID = ST.REFLOID AND ST.PRODUCTREF = 'POITEM' AND ST.PRODUCTLOID = VPL.POI_LOID ";
            sql += "LEFT JOIN V_REQUISITION_PROD_LIST VRP ON ST.REFTABLE = 'REQUISITION' AND VRP.RQLOID = ST.REFLOID ";
            sql += "LEFT JOIN SUPPLIER S ON S.LOID = ST.RECEIVER ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }
        public DataTable GetReqProductionItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM NO, RQI.PRODUCT, P.NAME PRODUCTNAME, RQI.MASTER QTY, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, RQI.MASTER*P.PRICE AS NETPRICE, RQI.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, RQI.LOID REFLOID ";
            sql += "FROM REQMATERIAL RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionItemList(double product)
        {
            string sql = "SELECT B.LOID, ROWNUM NO, B.MATERIAL AS PRODUCT, P.NAME PRODUCTNAME, B.MASTER QTY, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, B.MASTER*P.PRICE AS NETPRICE, B.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, B.LOID REFLOID ";
            sql += "FROM BOM B INNER JOIN PRODUCT P ON P.LOID = B.MATERIAL ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = B.UNIT ";
            sql += "WHERE B.MAINPRODUCT = " + product;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDItemList(double product)
        {
            string sql = "SELECT ps.LOID, ROWNUM NO, ps.PRODUCT AS PRODUCT, P.NAME PRODUCTNAME, ps.qty QTY, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, ps.qty*P.PRICE AS NETPRICE, ps.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, ps.LOID REFLOID ";
            sql += "FROM productstock ps INNER JOIN PRODUCT P ON P.LOID = ps.product ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = p.UNIT ";
            sql += "WHERE ps.PRODUCT = " + product;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStock(double warehouse, double productBarcode)
        {
            double zone = Constz.Zone.Z31;
            string sql = "SELECT PS.* ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PS.WAREHOUSE = " + warehouse.ToString() + " AND P.LOID = " + productBarcode.ToString() + " AND ZONE = " + zone.ToString() + " ORDER BY PS.LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetRequisitionItemList(double requisition, double product, string barcode, OracleTransaction zTrans)
        {
            string where = "RQI.REQUISITION = " + requisition.ToString() + " ";
            if (product != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID = " + product.ToString() + " ";

            if (barcode != "")
                where += (where == "" ? "" : "AND ") + "P.BARCODE = '" + barcode + "' ";

            string sql = "SELECT RQI.LOID, 1 NO, RQI.PRODUCT, RQI.MASTER QTY, '' LOTNO, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, RQI.MASTER*P.PRICE AS NETPRICE, RQI.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, RQI.LOID REFLOID ";
            sql += "FROM REQMATERIAL RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";
            return OracleDB.ExecListCmd(sql, zTrans);
        }

        public DataTable GetBomItemList(double mainproduct, double product, string barcode, OracleTransaction zTrans)
        {
            string where = "B.MAINPRODUCT = " + mainproduct.ToString() + " ";
            if (product != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID = " + product.ToString() + " ";

            if (barcode != "")
                where += (where == "" ? "" : "AND ") + "P.BARCODE = '" + barcode + "' ";

            string sql = "SELECT B.LOID, 1 NO, B.MATERIAL AS PRODUCT, B.MASTER QTY, '' LOTNO, P.UNIT, UNIT.NAME UNITNAME, P.PRICE, 0 AS DISCOUNT, B.MASTER*P.PRICE AS NETPRICE, B.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME, P.ISVAT, B.LOID REFLOID ";
            sql += "FROM BOM B INNER JOIN PRODUCT P ON P.LOID = B.MATERIAL ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = B.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";
            return OracleDB.ExecListCmd(sql, zTrans);
        }
        public DataTable GetPDItemList(double product, string barcode, OracleTransaction zTrans)
        {
            string where = "ps.qty > 0 ";
            if (product != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID = " + product.ToString() + " ";

            if (barcode != "")
                where += (where == "" ? "" : "AND ") + "P.BARCODE = '" + barcode + "' ";

            string sql = "SELECT ps.LOID, 1 NO, ps.PRODUCT, ps.qty QTY, '' LOTNO, P.UNIT, UNIT.NAME UNITNAME,P.PRICE, 0 AS DISCOUNT, ps.ACTIVE, P.BARCODE, UNIT.NAME UNITNAME,P.ISVAT, ps.LOID REFLOID  ";
            sql += "FROM productstock ps INNER JOIN PRODUCT P ON P.LOID = ps.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";
            return OracleDB.ExecListCmd(sql, zTrans);
        }

        public DataTable GetPDOtherItemList(double product, string barcode, OracleTransaction zTrans)
        {
            string where = "";
            if (product != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID = " + product.ToString() + " ";

            if (barcode != "")
                where += (where == "" ? "" : "AND ") + "P.BARCODE = '" + barcode + "' ";

            string sql = "SELECT P.LOID PDLOID, 1 NO,P.PRODUCTNAME PDNAME, P.LOID PRODUCT, 0 QTY, '' LOTNO, P.UNIT UNIT,U.NAME UNITNAME,P.PRICE PRICE,P.BARCODE BARCODE  ";
            sql += "FROM PRODUCT P INNER JOIN UNIT U ON U.LOID=P.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";
            return OracleDB.ExecListCmd(sql, zTrans);
        }

        public double GetApprover(string userid)
        {
            string sql = "SELECT LOID FROM OFFICER WHERE USERID = '" + userid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            double approver = 0;
            if (dt.Rows.Count > 0)
            {
                approver = Convert.ToDouble(dt.Rows[0]["LOID"]);
            }

            return approver;
        }

        public bool UpdatePDProductStatus(double loid, string status, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE PDPRODUCT SET PRODSTATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid.ToString() + " ";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) _error = OracleDB.Err_NoUpdate;
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool UpdatePDOrderStatus(double loid, string status, string userID, OracleTransaction zTrans)
        {
            string sql = "SELECT PDORDER FROM PDPRODUCT WHERE LOID = " + loid;
            DataTable dt = OracleDB.ExecListCmd(sql);
            double PDORDER = 0;
            if (dt.Rows.Count > 0)
            {
                PDORDER = Convert.ToDouble(dt.Rows[0]["PDORDER"]);
            }

            sql = "UPDATE PDORDER SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + PDORDER.ToString() + " ";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) _error = OracleDB.Err_NoUpdate;
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }


    }
}
