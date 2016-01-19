using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;

namespace ABB.DAL.Handheld.WH
{
    public class StockInPODAL
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        #region Before Send QC

        public bool UpdatePDOrderReceiveQty(double stockIn, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;
            try
            {
                string sql = "UPDATE POITEM SET RECEIVEQTY = (SELECT QCQTY FROM STOCKINITEM WHERE REFLOID = POITEM.LOID AND REFTABLE = 'POITEM' AND STOCKIN = " + stockIn.ToString() + ") ";
                sql += "WHERE PDORDER IN (SELECT POI.PDORDER FROM STOCKINITEM STI INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' WHERE STI.STOCKIN= " + stockIn.ToString() + ") ";
                ret = OracleDB.ExecNonQueryCmd(sql, trans) > 0;
                if (!ret) throw new ApplicationException(OracleDB.Err_NoUpdate);
            }
            catch (System.Data.OracleClient.OracleException ex)
            {
                ret = false;
                _error = OracleDB.GetOracleExceptionText(ex);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public DataTable GetStockInPOList(double docType, string status)
        {
            string where = "";
            if (docType != 0)
                where += (where == "" ? "" : " AND ") + "DOCTYPE = " + docType.ToString() + " ";

            if (status.Trim() != "")
                where += (where == "" ? "" : " AND ") + "STATUS = '" + status + "' ";

            string sql = "SELECT LOID, CODE, RECEIVEDATE FROM STOCKIN " + (where == "" ? "" : " WHERE ") + where + " ORDER BY CODE, RECEIVEDATE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockInPOData(double loid)
        {
            string sql = "SELECT ST.LOID, ST.CODE, ST.INVNO, SP.SUPPLIERNAME, ST.SENDER, ST.STATUS ";
            sql += "FROM STOCKIN ST INNER JOIN SUPPLIER SP ON ST.SENDER = SP.LOID ";
            sql += "WHERE ST.LOID = " + loid.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductDataByBarcode(string barcode, double PDOrder)
        {
            string sql = "SELECT PI.LOID REFLOID, P.LOID, P.BARCODE, P.NAME, PI.UNIT, UNIT.NAME UNITNAME, P.PRICE ";
            sql += "FROM PRODUCT P INNER JOIN POITEM PI ON PI.PRODUCT = P.LOID ";
            sql += "INNER JOIN UNIT ON PI.UNIT = UNIT.LOID ";
            sql += "WHERE UPPER(P.BARCODE) = '" + barcode.ToUpper().Trim() + "' AND PI.PDORDER = " + PDOrder.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductList(double stockIn, double PDOrder)
        {
            string sql = "SELECT STI.LOID, P.NAME, STI.QCQTY, UNIT.NAME UNITNAME ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = STI.UNIT ";
            sql += "INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' ";
            sql += "WHERE STI.STOCKIN = " + stockIn.ToString() + " AND POI.PDORDER = " + PDOrder.ToString() + " ";
            sql += "ORDER BY STI.LOTNO, P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductDetail(double stockInItem)
        {
            string sql = "SELECT ST.CODE, PO.CODE ORDERCODE, PO.ORDERDATE, P.NAME, POI.QTY ORDERQTY, UNIT.NAME UNITNAME, ST.INVNO, ";
            sql += "S.SUPPLIERNAME, STI.QTY, STI.QCQTY, STI.STOCKIN ";
            sql += "FROM STOCKINITEM STI INNER JOIN STOCKIN ST ON ST.LOID = STI.STOCKIN ";
            sql += "INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' ";
            sql += "INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = STI.UNIT ";
            sql += "INNER JOIN SUPPLIER S ON S.LOID = ST.SENDER ";
            sql += "WHERE STI.LOID=" + stockInItem.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        #endregion

        #region After Send QC

        public DataTable GetStockInQCData(double loid)
        {
            string sql = "SELECT ST.LOID, ST.CODE, ST.QCCODE, ST.RECEIVEDATE, ST.STATUS ";
            sql += "FROM STOCKIN ST ";
            sql += "WHERE ST.LOID = " + loid.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetQCProductByBarcode(string barcode, double stockIn)
        {
            string sql = "SELECT STI.LOID, P.BARCODE, P.NAME, P.UNIT, UNIT.NAME UNITNAME, STI.QTY ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON STI.PRODUCT = P.LOID ";
            //sql += "INNER JOIN POITEM PI ON PI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' ";
            sql += "INNER JOIN UNIT ON STI.UNIT = UNIT.LOID ";
            sql += "WHERE STI.QCRESULT = '" + Constz.QCResult.Pass.Code + "' AND UPPER(P.BARCODE) = '" + barcode.ToUpper().Trim() + "' AND STI.STOCKIN = " + stockIn.ToString() + " ";
            sql += "AND STI.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetQCProductList(double stockIn)
        {
            string sql = "SELECT STI.LOID, P.NAME, STI.QTY, UNIT.NAME UNITNAME ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += "WHERE STI.STOCKIN = " + stockIn.ToString() + " AND STI.QCRESULT = '" + Constz.QCResult.Pass.Code + "' AND STI.QTY >0 ";
            sql += "ORDER BY STI.LOTNO, P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetQCProductDetail(double stockInItem)
        {
            string sql = "SELECT STI.STOCKIN, ST.CODE, ST.RECEIVEDATE, P.NAME, STI.QCQTY, UNIT.NAME UNITNAME, ST.QCCODE, STI.QTY ";
            sql += "FROM STOCKINITEM STI INNER JOIN STOCKIN ST ON ST.LOID = STI.STOCKIN ";
            //sql += "INNER JOIN POITEM POI ON POI.LOID = STI.REFLOID AND STI.REFTABLE = 'POITEM' ";
            sql += "INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = STI.UNIT ";
            sql += "WHERE STI.LOID=" + stockInItem.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        #endregion
    }
}
