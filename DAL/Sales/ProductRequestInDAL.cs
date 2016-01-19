using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class ProductRequestInDAL
    {
        public DataTable GetOrderList(ProductOrderSearchData whereData)
        {
            string whereString = "REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ06.ToString() + " ";
            if (whereData.CODE.Trim() != "")
                whereString += "AND CODE >= '" + OracleDB.QRText(whereData.CODE.Trim()) + "' ";
            if (whereData.CODETO.Trim() != "")
                whereString += "AND CODE <= '" + OracleDB.QRText(whereData.CODETO.Trim()) + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += "AND RESERVEDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += "AND RESERVEDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            //if (whereData.PDNAME.Trim() != "0")
            //    whereString += "AND PDLOID = '" + OracleDB.QRText(whereData.PDNAME.Trim()) + "' ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += "AND RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += "AND RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.LOID, RQ.CODE, RQ.RESERVEDATE, RQ.REQUISITIONTYPE, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUS, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, RQ.CREATEBY, '" + Constz.ReadyMadeDepartment.Name + "' AS SELLER, RQ.TOTAL NETPRICE ";
            sql += "FROM REQUISITION RQ ";
            //sql += "LEFT JOIN (SELECT REQUISITION, SUM(GRANDTOT) NETPRICE ";
            //sql += "FROM REQUISITIONITEM GROUP BY REQUISITION) R ON RQ.LOID = R.REQUISITION "
            sql += ")A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetOrderItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.QTY, RQI.UNIT, RQI.ACTIVE, RQI.DUEDATE, PD.NAME PDNAME, PD.BARCODE, UNIT.NAME UNIT ,RQI.NETPRICE NETPRICE,RQI.PRICE ";
            sql += "FROM REQUISITIONITEM RQI LEFT JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            sql += "LEFT JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
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
        public DataTable GetReserveItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT, ";
            sql += "0 NORMALDISCOUNT, 0 STOCKQTY, '' PRODUCTNAME ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }
        public DataTable GetOrderItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 PRODUCT, 0 QTY, 0 UNIT, 0 PRICE, 0 DISCOUNT, 0 NETPRICE, '" + Constz.ActiveStatus.Active + "' ACTIVE, '' BARCODE, '' UNITNAME, '' ISVAT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}

