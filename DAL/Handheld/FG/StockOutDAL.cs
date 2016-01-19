using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;

namespace ABB.DAL.Handheld.FG
{
    public class StockOutDAL
    {
        public DataTable GetStockOutList()
        {
            string sql = "SELECT RQ.LOID REQUISITION, RQ.CODE REQCODE, RQ.REQDATE, ST.LOID STOCKOUT ";
            sql += "FROM REQUISITION RQ INNER JOIN REQUISITIONTYPE RT ON RT.LOID = RQ.REQUISITIONTYPE ";
            sql += "LEFT JOIN STOCKOUT ST ON ST.REFLOID = RQ.LOID AND ST.REFTABLE = 'REQUISITION' ";
            sql += "RT.ISFGSTOCKOUT = '" + Constz.IsFGStockOut.Yes + "' AND RQ.STATUS = '" + Constz.Requisition.Status.Finish.Code + "' AND RQ.ACTIVE = '" + Constz.ActiveStatus.Active + "' ";
            sql += "ORDER BY REQCODE, REQDATE, ST.LOID ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductDetail(double requisition, string barcode)
        {
            string sql = "SELECT RQI.LOID REFLOID, P.LOID PRODUCT, P.BARCODE, P.NAME, RQI.UNIT, UNIT.NAME UNITNAME ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE P.BARCODE = '" + barcode + "' AND RQI.REQUISITION = " + requisition.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStock(double product, double stockout)
        {
            string sql = "SELECT LOID, NVL(LOTNO, 'ไม่ระบุ') LOTNO ";
            sql += "FROM PRODUCTSTOCK ";
            sql += "WHERE PRODUCT = '" + product.ToString() + "' AND ZONE = ";
            sql +="( SELECT Z.FROMZONE, ST.LOID ";
            sql += "FROM STOCKOUT ST INNER JOIN ZONEMOVEMENT Z ON Z.REFLOID = ST.DOCTYPE AND Z.REFTABLE = 'DOCTYPE' AND Z.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
            sql += "WHERE ST.LOID = " + stockout.ToString() + ") ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
