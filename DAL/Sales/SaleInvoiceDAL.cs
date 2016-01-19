using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.DAL.Sales
{
    public class SaleInvoiceDAL
    {
        public DataTable GetRequisitionData(double requisition)
        {
            string sql = "SELECT R.LOID, R.CODE, R.REQDATE, RF.CODE REFCODE, NVL(R.CCODE, C.CODE) CCODE, NVL(R.CNAME, T.NAME || C.NAME || ' ' || C.LASTNAME) CNAME, NVL(R.CTEL, C.CTEL) CTEL, NVL(R.CFAX, C.CFAX) CFAX, R.TOTAL, R.TOTDIS, R.VAT, ";
            sql += "NVL(R.CADDRESS, C.CADDRESS || CASE NVL(C.CROAD,'-') WHEN '-' THEN '' ELSE ' ¶.' || C.CROAD END || CASE NVL(TA.LOID,0) WHEN 0 THEN '' ELSE ' µ.' || TA.NAME END || ";
            sql += "CASE NVL(AM.LOID,0) WHEN 0 THEN '' ELSE ' Í.' || AM.NAME END || CASE NVL(PR.LOID,0) WHEN 0 THEN '' ELSE ' ¨.' || PR.NAME END || ' ' || C.CZIPCODE) CADDRESS, R.PAYMENT, R.CHEQUE,R.CREDITCARDID, R.CHEQUEDATE, R.BANKNAME, R.BANKBRANCH,";
            sql += "R.RECEIVEBY, R.RECEIVEDATE, R.TOTVAT, R.GRANDTOT, R.CREATEBY, R.INVCODE ";
            sql += "FROM REQUISITION R INNER JOIN CUSTOMER C ON C.LOID = R.CUSTOMER ";
            sql += "LEFT JOIN TITLE T ON T.LOID = C.TITLE ";
            sql += "LEFT JOIN REQUISITION RF ON R.REFLOID = RF.LOID AND R.REFTABLE = 'REQUISITION' ";
            sql += "LEFT JOIN TAMBOL TA ON TA.LOID = C.CTAMBOL ";
            sql += "LEFT JOIN AMPHUR AM ON AM.LOID = C.CAMPHUR ";
            sql += "LEFT JOIN PROVINCE PR ON PR.LOID = C.CPROVINCE ";
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
