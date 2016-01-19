using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.DAL.Sales
{
    public class BillDAL
    {
        public DataTable GetRequisitionData(double requisition)
        {
            string sql = "SELECT R.LOID, R.CODE, R.REQDATE, RF.CODE REFCODE, T.NAME || C.NAME || ' ' || C.LASTNAME CUSTOMERNAME, R.TOTAL, R.TOTDIS, R.VAT, ";
            sql += "R.TOTVAT, R.GRANDTOT, R.CREATEBY ";
            sql += "FROM REQUISITION R INNER JOIN CUSTOMER C ON C.LOID = R.CUSTOMER ";
            sql += "LEFT JOIN TITLE T ON T.LOID = C.TITLE ";
            sql += "LEFT JOIN REQUISITION RF ON R.REFLOID = RF.LOID AND R.REFTABLE = 'REQUISITION' ";
            sql += "WHERE R.LOID = " + requisition.ToString();
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetRequisitionItem(double requisition)
        {
            string sql = "SELECT ROWNUM RANK, P.BARCODE, P.NAME PRODUCTNAME, RI.QTY, UNIT.NAME UNITNAME, RI.PRICE, RI.DISCOUNT, RI.NETPRICE, ";
            sql += "NVL(RI.ISVAT, '0') ISVAT ";
            sql += "FROM REQUISITIONITEM RI INNER JOIN PRODUCT P ON P.LOID = RI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RI.UNIT ";
            sql += "WHERE RI.REQUISITION = " + requisition.ToString();
            return OracleDB.ExecListCmd(sql);
        }

    }
}
