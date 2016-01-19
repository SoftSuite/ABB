using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;

namespace ABB.DAL.Handheld.FG
{
    public class StockOutFGDAL
    {
        public DataTable GetStockOutList()
        {
            string sql = "SELECT RQ.LOID REQUISITION, RQ.CODE REQCODE, RQ.REQDATE, NVL(ST.LOID, 0) STOCKOUT, ST.CODE ";
            sql += "FROM REQUISITION RQ INNER JOIN REQUISITIONTYPE RT ON RT.LOID = RQ.REQUISITIONTYPE ";
            sql += "LEFT JOIN STOCKOUT ST ON ST.REFLOID = RQ.LOID AND ST.REFTABLE = 'REQUISITION' AND ST.STATUS <> '" + Constz.Requisition.Status.Void.Code + "' ";
            sql += "WHERE (ST.LOID IS NULL OR (ST.LOID IS NOT NULL AND ST.STATUS = '" + Constz.Requisition.Status.Waiting.Code + "')) AND RT.ISFGSTOCKOUT = '" + Constz.IsFGStockOut.Yes + "' AND RQ.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND RQ.ACTIVE = '" + Constz.ActiveStatus.Active + "' ";
            sql += "ORDER BY REQCODE, REQDATE, ST.LOID ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutItemList(double stockOut)
        {
            string sql = "SELECT STI.LOID, P.NAME, STI.QTY, STI.LOTNO ";
            sql += "FROM STOCKOUTITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "WHERE STI.STOCKOUT = " + stockOut.ToString() + " ";
            sql += "ORDER BY P.NAME, STI.LOTNO ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductDetail(double stockOut, string barcode)
        {
            string sql = "SELECT RQI.LOID REFLOID, P.LOID PRODUCT, P.BARCODE, P.NAME, RQI.UNIT, UNIT.NAME UNITNAME, P.PRICE ";
            sql += "FROM STOCKOUT ST INNER JOIN REQUISITION RQ ON RQ.LOID = ST.REFLOID AND ST.REFTABLE = 'REQUISITION' ";
            sql += "INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION = RQ.LOID ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE P.BARCODE = '" + barcode + "' AND ST.LOID = " + stockOut.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStock(double productBarcode, double stockout)
        {
            string sql = "SELECT PS.LOID, NVL(PS.LOTNO, 'ไม่ระบุ') LOTNO ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE PD ON PD.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE PD.LOID = '" + productBarcode.ToString() + "' AND ZONE = ";
            sql +="( SELECT Z.FROMZONE ";
            sql += "FROM STOCKOUT ST INNER JOIN ZONEMOVEMENT Z ON Z.REFLOID = ST.DOCTYPE AND Z.REFTABLE = 'DOCTYPE' AND Z.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
            sql += "WHERE ST.LOID = " + stockout.ToString() + ") ORDER BY PS.MFGDATE";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutData(double stockOut)
        {
            string sql = "SELECT ST.LOID, ST.CODE, RQ.CODE REQCODE, DT.DOCNAME ";
            sql += "FROM STOCKOUT ST INNER JOIN REQUISITION RQ ON RQ.LOID = ST.REFLOID AND ST.REFTABLE = 'REQUISITION' ";
            sql += "INNER JOIN DOCTYPE DT ON DT.LOID = ST.DOCTYPE ";
            sql += "WHERE ST.LOID = " + stockOut.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutItemData(double stockOutItem)
        {
            string sql = "SELECT STI.STOCKOUT, ST.CODE, RQ.CODE REQCODE, DT.DOCNAME, P.NAME, STI.QTY, STI.LOTNO, UNIT.NAME UNITNAME ";
            sql += "FROM STOCKOUT ST INNER JOIN REQUISITION RQ ON RQ.LOID = ST.REFLOID AND ST.REFTABLE = 'REQUISITION' ";
            sql += "INNER JOIN DOCTYPE DT ON DT.LOID = ST.DOCTYPE ";
            sql += "INNER JOIN STOCKOUTITEM STI ON STI.STOCKOUT = ST.LOID ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = STI.UNIT ";
            sql += "WHERE STI.LOID = " + stockOutItem.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
