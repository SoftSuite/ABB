using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;

namespace ABB.DAL.Production
{
    public class QCAnalysisDAL
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
            set { _error = value; }
        }

        public DataTable GetStockInItem(double stockIn)
        {
            string sql = "SELECT 1 NO, SII.LOID,PD.BARCODE,PD.NAME PDNAME,U.NAME UNAME,SII.REFTABLE,PO.CODE,SII.QCQTY,SII.QCRESULT,SII.QCREMARK,SII.QCDUEDATE DUEDATE ";
            sql += "FROM STOCKINITEM SII INNER JOIN POITEM POI ON SII.REFLOID = POI.LOID AND SII.REFTABLE = 'POITEM' ";
            sql += "INNER JOIN PDORDER PO ON POI.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT PD ON SII.PRODUCT = PD.LOID INNER JOIN UNIT U ON SII.UNIT = U.LOID ";
            sql += "WHERE SII.STOCKIN = " + stockIn + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public bool UpdateQCResult(double loid, DateTime duedate, string qcresult, string qcremark, string qcqty, string userID, OracleTransaction zTrans)
        {
            string sql = "UPDATE STOCKINITEM SET QCRESULT = '" + qcresult + "', ";
            sql += "QCREMARK = '" + qcremark + "', ";
            sql += "QCQTY = " + qcqty + ", ";
            sql += "QCDUEDATE = " + OracleDB.QRDateTime(duedate) + ", ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid + " ";
            //+ (status == Constz.Requisition.Status.Approved.Code ? "AND STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' " : (status == Constz.Requisition.Status.Void.Code ? "AND STATUS = '" + Constz.Requisition.Status.Approved.Code + "' " : ""));
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) throw new ApplicationException(OracleDB.Err_NoUpdate);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool UpdateQCStockin(double loid, string anacode, DateTime anadate, string userID, string status, OracleTransaction zTrans)
        {
            string sql = "UPDATE STOCKIN SET ANACODE = '" + anacode + "', ";
            sql += "STATUS = '" + status + "', ";
            sql += "ANADATE = " + OracleDB.QRDateTime(anadate) + ", ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE LOID = " + loid + " ";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) throw new ApplicationException(OracleDB.Err_NoUpdate);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }
        public bool UpdateQCStockinItem(double stockin, string userID, string status, OracleTransaction zTrans)
        {
            string sql = "UPDATE STOCKINITEM ";
            sql += "SET STATUS = '" + status + "', ";
            sql += "UPDATEBY = '" + userID + "', ";
            sql += "UPDATEON = " + OracleDB.QRDateTime() + " ";
            sql += "WHERE STOCKIN = " + stockin + " ";
            bool ret = true;
            try
            {
                ret = (OracleDB.ExecNonQueryCmd(sql, zTrans) > 0);
                if (!ret) throw new ApplicationException(OracleDB.Err_NoUpdate);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }
        public string GetDivision(string createBy)
        {
            string sql = "SELECT D.TNAME FROM OFFICER F INNER JOIN DIVISION D ON F.DIVISION = D.LOID WHERE F.USERID = '" + createBy + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string division = "";
            if (dt.Rows.Count > 0)
            {
                division = dt.Rows[0]["TNAME"].ToString();


            }

            return division;
        }



  }
}
