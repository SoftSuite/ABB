using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Production;
using System.Data.OracleClient;

namespace ABB.DAL.Production
{
    public class QCAnalysisPDDAL
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
            set { _error = value; }
        }

        public DataTable GetPDProductItem(double pdorder)
        {
            string sql = "SELECT 1 NO, PDP.LOID,PD.BARCODE,PD.NAME PDNAME,U.NAME UNAME,PDP.PDQTY,PDP.QCRESULT,PDP.QCREMARK,PDP.QCQTY1,PDP.QCQTY2,PDP.QCQTY3,PDP.QCDUEDATE DUEDATE";
            sql += " FROM PDPRODUCT PDP ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID ";
            sql += "WHERE PDP.LOID = " + pdorder + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public bool UpdateQCResult(double loid, DateTime duedate, double qcqty1, double qcqty2, double qcqty3, string qcresult, string qcremark, string userID, string status, OracleTransaction zTrans)
        {
            string sql = "UPDATE PDPRODUCT SET QCRESULT = '" + qcresult + "', ";
            sql += "QCQTY1 = " + qcqty1 + ", ";
            sql += "QCQTY2 = " + qcqty2 + ", ";
            sql += "QCQTY3 = " + qcqty3 + ", ";
            sql += "QCREMARK = '" + qcremark + "', ";
            sql += "PRODSTATUS = '" + status + "', ";
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

        public bool UpdatePDOrder(double loid, string userID, string status, OracleTransaction zTrans)
        {
            string sql = "UPDATE PDORDER SET ";
            sql += "STATUS = '" + status + "', ";
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

        public double GetPDOrder(double loid)
        {
            string sql = "SELECT PDORDER FROM PDPRODUCT WHERE LOID = " + loid + " ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            double pdorder = 0;
            if (dt.Rows.Count > 0)
            {
                pdorder = Convert.ToDouble(dt.Rows[0]["PDORDER"].ToString().Trim());

            }

            return pdorder;
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

        public StockinQCData DoGetPDOrderData(double pdorder)
        {
            string sql = "SELECT PO.CREATEBY,PO.CODE FROM PDORDER PO WHERE PO.LOID = '" + pdorder + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            StockinQCData data = new StockinQCData();
            if (dt.Rows.Count > 0)
            {
                data.INVNO = dt.Rows[0]["CODE"].ToString();
                data.CREATEBY = dt.Rows[0]["CREATEBY"].ToString();
            }

            return data;
        }

  }
}
