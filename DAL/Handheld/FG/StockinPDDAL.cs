using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;

namespace ABB.DAL.Handheld.FG
{
    public class StockInPDDAL
    {
        public virtual DataTable GetStockInPDList(double docType)
        {
            string where = "STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' ";
            if (docType != 0)
                where += (where == "" ? "" : " AND ") + "DOCTYPE = " + docType.ToString() + " ";
            string sql = "SELECT LOID, CODE, RECEIVEDATE FROM STOCKIN " + (where == "" ? "" : " WHERE " ) + where + "ORDER BY CODE, RECEIVEDATE";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductDataByBarcode(string barcode, string produceType)
        {
            string sql = "SELECT DISTINCT P.LOID, P.BARCODE, P.NAME, P.UNIT, UNIT.NAME UNITNAME, P.PRICE ";
            sql += "FROM PRODUCT P INNER JOIN UNIT ON P.UNIT = UNIT.LOID ";
            sql += "INNER JOIN PDPRODUCT PD ON PD.PRODUCT = P.LOID ";
            sql += "INNER JOIN PDORDER PO ON PO.LOID = PD.PDORDER ";
            sql += "LEFT JOIN STOCKINITEM STI ON PD.LOID = STI.REFLOID AND STI.REFTABLE = 'PDPRODUCT' ";
            sql += "WHERE STI.LOID IS NULL AND PO.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND UPPER(P.BARCODE) = '" + barcode.ToUpper().Trim() + "' AND PD.PRODUCETYPE = '" + produceType.Trim() + "' ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDProduct(double product, string lotno)
        {
            string sql = "SELECT LOID FROM PDPRODUCT ";
            sql += "WHERE UPPER(LOTNO) = '" + lotno.ToUpper().Trim() + "' AND PRODUCT = '" + product + "' ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductList(double stockIn)
        {
            string sql = "SELECT STI.LOID, STI.LOTNO, P.NAME, STI.QTY ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "WHERE STI.STOCKIN = " + stockIn.ToString() + " ";
            sql += "ORDER BY STI.LOTNO, P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public virtual DataTable GetProductDetail(double stockInItem)
        {
            string sql = "SELECT STI.STOCKIN, ST.CODE, PD.MFGDATE, STI.LOTNO, P.NAME, STI.QTY, UNIT.NAME UNITNAME ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN STOCKIN ST ON ST.LOID = STI.STOCKIN ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += "LEFT JOIN PDPRODUCT PD ON PD.LOID = STI.REFLOID AND STI.REFTABLE = 'PDPRODUCT' ";
            sql += "WHERE STI.LOID = " + stockInItem.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
