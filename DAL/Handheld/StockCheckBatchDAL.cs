using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;

namespace ABB.DAL.Handheld
{
    public class StockCheckBatchDAL
    {
        public DataTable GetStockCheckList(double warehouse)
        {
            string sql = "SELECT LOID, BATCHNO, CHECKDATE, WAREHOUSE FROM STOCKCHECK ";
            sql += "WHERE WAREHOUSE = " + warehouse.ToString() + " AND STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' ";
            sql += "ORDER BY WAREHOUSE, BATCHNO, CHECKDATE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockCheckItemList(double stockCheck, double location, string createBy)
        {
            string sql = "SELECT STI.LOID, P.NAME PRODUCTNAME, STI.LOTNO, STI.COUNTQTY ";
            sql += "FROM STOCKCHECKITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "WHERE STI.STOCKCHECK = " + stockCheck.ToString() + " AND STI.LOCATION = " + location.ToString() + " AND STI.CREATEBY = '" + createBy + "' ";
            sql += "ORDER BY P.NAME, STI.LOTNO, STI.COUNTQTY, STI.LOID ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductStock(string barcode, double warehouse, double location)
        {
            string sql = "SELECT DISTINCT P.LOID PRODUCT, P.BARCODE, P.NAME, P.UNIT, UNIT.NAME UNITNAME ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCT P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += "INNER JOIN ZONE ON ZONE.LOID = PS.ZONE ";
            sql += "WHERE P.BARCODE = '" + barcode + "' AND ZONE.LOCATION = " + location.ToString() + " AND PS.WAREHOUSE = " + warehouse.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockCheckData(double stockCheck)
        {
            string sql = "SELECT ST.LOID, ST.CHECKDATE, ST.BATCHNO, W.NAME WAREHOUSENAME ";
            sql += "FROM STOCKCHECK ST INNER JOIN WAREHOUSE W ON ST.WAREHOUSE = W.LOID ";
            sql += "WHERE ST.LOID = " + stockCheck.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockCheckItemData(double stockCheckItem)
        {
            string sql = "SELECT STI.STOCKCHECK, ST.CHECKDATE, ST.BATCHNO, W.NAME WAREHOUSENAME, STI.LOCATION, LOCATION.NAME LOCATIONNAME, ";
            sql += "P.NAME PRODUCTNAME, STI.LOTNO, STI.COUNTQTY, UNIT.NAME UNITNAME ";
            sql += "FROM STOCKCHECK ST INNER JOIN WAREHOUSE W ON ST.WAREHOUSE = W.LOID ";
            sql += "INNER JOIN STOCKCHECKITEM STI ON STI.STOCKCHECK = ST.LOID ";
            sql += "INNER JOIN LOCATION ON LOCATION.LOID = STI.LOCATION ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += "WHERE STI.LOID = " + stockCheckItem.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
