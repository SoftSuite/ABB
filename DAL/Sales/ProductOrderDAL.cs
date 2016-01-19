using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;


namespace ABB.DAL.Sales
{
    public class ProductOrderDAL
    {
        public DataTable GetOrderList(ProductOrderSearchData whereData)
        {
            string whereString = "REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ07.ToString() + " ";
            if (whereData.CODE.Trim() != "")
                whereString += "AND CODE = '" + OracleDB.QRText(whereData.CODE.Trim()) + "' ";
            if (whereData.DATEFROM.Year != 1)
                whereString += "AND REQDATE >= " + OracleDB.QRDate(whereData.DATEFROM) + " ";
            if (whereData.DATETO.Year != 1)
                whereString += "AND REQDATE <= " + OracleDB.QRDate(whereData.DATETO) + " ";
            if (whereData.PDNAME.Trim() != "0")
                whereString += "AND PDLOID = '" + OracleDB.QRText(whereData.PDNAME.Trim()) + "' ";
            if (whereData.STATUSFROM.Trim() != "")
                whereString += "AND RANK >= " + OracleDB.QRText(whereData.STATUSFROM.Trim()) + " ";
            if (whereData.STATUSTO.Trim() != "")
                whereString += "AND RANK <= " + OracleDB.QRText(whereData.STATUSTO.Trim()) + " ";

            string sql = "SELECT ROWNUM NO, A.* FROM (SELECT RQ.LOID, RQ.CODE, RQ.REQDATE, RQI.DUEDATE, RQ.REQUISITIONTYPE,";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUS, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, PD.LOID AS PDLOID, PD.NAME AS PDNAME, RQI.QTY, UN.NAME AS UNIT, RQ.CREATEBY ";
            sql += "FROM REQUISITION RQ LEFT JOIN (SELECT REQUISITION, MIN(LOID) LOID FROM REQUISITIONITEM GROUP BY REQUISITION) R ON RQ.LOID = R.REQUISITION ";
            sql += "LEFT JOIN REQUISITIONITEM RQI ON RQI.LOID = R.LOID ";
            sql += "LEFT JOIN UNIT UN ON RQI.UNIT = UN.LOID ";
            sql += "LEFT JOIN PRODUCT PD ON RQI.PRODUCT = PD.LOID ) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetOrderItemList(double requisition)
        {
            string sql = "SELECT RQI.LOID, ROWNUM RANK, RQI.PRODUCT, RQI.QTY, RQI.UNIT, RQI.ACTIVE, RQI.DUEDATE, PD.NAME PDNAME, PD.BARCODE, UNIT.NAME UNIT ";
            sql += "FROM REQUISITIONITEM RQI LEFT JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            sql += "LEFT JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition;
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

