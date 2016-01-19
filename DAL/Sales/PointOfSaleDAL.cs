using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class PointOfSaleDAL
    {
        public static DataTable GetItemList(double requisition)
        {
            string sql = "SELECT RQI.REQUISITION, RQI.LOID, RQI.PRODUCT, RQI.UNIT, ROWNUM ORDERNO, PD.BARCODE, PD.NAME, RQI.QTY, UNIT.NAME UNITNAME, RQI.PRICE, ";
            sql += "RQI.DISCOUNT, RQI.NETPRICE, ";
            sql += "CASE RQI.ISVAT WHEN '" + Constz.VAT.Included.Code + "' THEN " + Constz.VAT.Included.Code + " ELSE " + Constz.VAT.NotIncluded.Code + " END AS ISVAT, PD.PRICE UNITPRICE, 0 NORMALDISCOUNT ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT PD ON RQI.PRODUCT = PD.LOID ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition.ToString();
            return OracleDB.ExecListCmd(sql);
        }

        //public static DataTable GetCustomerDetail(double customer)
        //{
        //    string sql = "SELECT C.LOID, C.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME AS CUSTOMERNAME, MT.DISCOUNT ";
        //    sql += "FROM CUSTOMER C INNER JOIN MEMBERTYPE MT ON MT.LOID = C.MEMBERTYPE ";
        //    sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
        //    sql += "WHERE C.LOID = " + customer.ToString() + " ";
        //    return OracleDB.ExecListCmd(sql);
        //}

        //public static DataTable GetProductWithPromotion(double warehouse, DateTime pDate, double product, string barcode)
        //{
        //    SaleDAL sDAL = new SaleDAL();
        //    return sDAL.GetProductPromotion(warehouse, pDate, product, barcode);
        //}

        public static DataTable GetRefRequisition(double loid)
        {
            string sql = "SELECT RQ.LOID, RQ.CODE REQUISITIONCODE, RQ.CUSTOMER, C.CODE, M.DISCOUNT CUSTOMERDISCOUNT ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON C.LOID = RQ.CUSTOMER ";
            sql += "INNER JOIN MEMBERTYPE M ON M.LOID = C.MEMBERTYPE ";
            sql += "WHERE RQ.LOID = " + loid.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetRefRequisitionItem(double refRequisition)
        {
            string sql = "SELECT RQI.REQUISITION, RQI.LOID, RQI.PRODUCT, PD.UNIT, PD.BARCODE, PD.NAME, RQI.QTY - NVL(STI.QTY,0) QTY, UNIT.NAME UNITNAME, PD.PRICE, ";
            sql += "MAX(CASE NVL(P.LOID,0) WHEN 0 THEN 0 ELSE PI.PRICEOLD - PI.PRICENEW END) DISCOUNT, PD.ISVAT, PD.ISDISCOUNT, PD.ISEDIT ";
            sql += "FROM REQUISITION RQ INNER JOIN CUSTOMER C ON C.LOID = RQ.CUSTOMER ";
            sql += "INNER JOIN MEMBERTYPE MT ON MT.LOID = C.MEMBERTYPE ";
            sql += "INNER JOIN REQUISITIONITEM RQI ON RQ.LOID = RQI.REQUISITION AND RQ.REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ13.ToString() + " ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            sql +="INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
            sql += "LEFT JOIN STOCKINITEM STI ON STI.REFLOID = RQI.LOID AND STI.REFTABLE = 'REQUISITIONITEM' AND STI.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
            sql += "LEFT JOIN PROMOTIONITEM PI ON PI.PRODUCT = PD.LOID ";
            sql += "LEFT JOIN PROMOTION P ON P.LOID = PI.PROMOTION AND  TO_CHAR(P.EPDATE, 'YYYYMMDD') >= TO_CHAR(SYSDATE,'YYYYMMDD') ";
            sql += "AND TO_CHAR(P.EFDATE, 'YYYYMMDD') <= TO_CHAR(SYSDATE,'YYYYMMDD') ";
            sql += "WHERE RQI.QTY - NVL(STI.QTY,0) >0 AND RQI.REQUISITION = " + refRequisition.ToString() + " ";
            sql += "GROUP BY RQI.REQUISITION, RQI.LOID, RQI.PRODUCT, PD.UNIT, PD.BARCODE, PD.NAME, RQI.QTY - NVL(STI.QTY,0), UNIT.NAME, PD.PRICE, PD.ISVAT , PD.ISDISCOUNT, PD.ISEDIT ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
