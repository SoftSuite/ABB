using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.Data;
using ABB.Data.Sales;
using ABB.DAL.Sales;

namespace ABB.Flow.Sales
{
    public class ReturnSearchFlow
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }
        public static ArrayList GetReturnSearch(string WHLoid,string SiCode, string DateFrom, string DateTo)
        {
            string str = "";
            ArrayList arrResult = new ArrayList();

            str = " SELECT SICODE, APPROVEDATE, CUSNAME, RQCODE, GRANDTOT, SILOID ";
            str += " FROM V_RETURN_LIST " ;
            str += " WHERE WAREHOUSE = " + WHLoid + " ";

            if (SiCode != "")
            {
                str += " AND UPPER(SICODE) = UPPER ('" + SiCode + "')";
            }

            if (DateFrom != null && DateTo != null)
            {
                str += " AND APPROVEDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY')";
            }
            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    ReturnSearchData irData = new ReturnSearchData();
                    irData.SICODE = zRd["SICODE"].ToString();
                    irData.APPROVEDATE = Convert.ToDateTime(zRd["APPROVEDATE"]);
                    irData.CUSNAME = zRd["CUSNAME"].ToString();
                    irData.RQCODE = zRd["RQCODE"].ToString();
                    irData.GRANDTOT = Convert.ToDouble(zRd["GRANDTOT"]);
                    irData.SILOID = Convert.ToDouble(zRd["SILOID"]);
                    arrResult.Add(irData);
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arrResult;
        }

        public bool DeleteStockIn_StockInitemData(string userID, ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (string loid in arrLOID)
                {

                    string sqlStockInItem = "DELETE FROM STOCKINITEM WHERE STOCKIN = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlStockInItem, DBObj.zTrans);

                    string sqlStockIn = "DELETE FROM STOCKIN  WHERE LOID = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlStockIn, DBObj.zTrans);
                }
                ret = true;
                DBObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                ret = false;
                DBObj.zTrans.Rollback();
                _error = ex.Message;
            }

            DBObj.CloseConnection();
            return ret;
        }

        public double InsertStockIn(string UserId, StockInData data)
        {
            Boolean ret = true;
            StockInDAL oDAL = new StockInDAL();
            oDAL.SENDER = Convert.ToDouble(data.SENDER);
            oDAL.DOCTYPE = Constz.DocType.RetShop.LOID;
            oDAL.RECEIVEDATE = DateTime.Now.Date;
            oDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
            oDAL.GRANDTOT = 0.00;
            oDAL.RECEIVER = data.RECEIVER;
            
            

            ret = oDAL.InsertCurrentData(UserId, null);

            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return 0;
            }
            else
                return oDAL.LOID;
        }

        public static DataTable GetReturnData(string WHLoid, string StockIn)
        {
            string str = "";
            str = " SELECT PD.CODE PDCODE, PD.NAME PDNAME, RQI.QTY RQ_QTY, SII.QTY RECEIVE_QTY, ";
            str += " UN.NAME UNITNAME, SII.PRICE,SI.GRANDTOT TOTAL,SI.LOID SILOID,SII.LOID SIILOID, ";
            str += " PD.LOID PDLOID, RQI.LOID RQILOID,SI.STATUS SISTATUS, SII.STATUS SIISTATUS ";
            str += " FROM STOCKIN SI INNER JOIN STOCKINITEM SII ON SII.STOCKIN = SI.LOID ";
            str += " INNER JOIN REQUISITIONITEM RQI ON RQI.LOID = SII.REFLOID AND SII.REFTABLE = 'REQUISITIONITEM'";
            str += " INNER JOIN REQUISITION RQ ON RQI.REQUISITION = RQ.LOID";
            str += " INNER JOIN REQUISITIONTYPE RQT ON RQT.LOID = RQ.REQUISITIONTYPE AND RQT.NAME = 'ใบรับคำสั่งซื้อ'";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = SII.PRODUCT ";
            str += " INNER JOIN UNIT UN ON UN.LOID = PD.UNIT ";
            str += " WHERE WAREHOUSE = " + WHLoid + " AND SI.LOID = " + StockIn + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetStockInData(string SILoid)
        {
            string str = "";
            str = " SELECT CODE SICODE , RECEIVEDATE, CREATEBY,GRANDTOT ";
            str += " FROM STOCKIN ";
            str += " WHERE LOID = " + SILoid + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProductStockInShopData(string WHLoid, string StockIn)
        {
            string str = "";
            str = " SELECT PDCODE, PDNAME, QTY, UNAME, PRICE, TOTAL, SILOID,SIILOID,SISTATUS, SIISTATUS ";
            str += " FROM V_RETURNDETAIL_LIST ";
            str += " WHERE WAREHOUSE = " + WHLoid + " AND SILOID = " + StockIn + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetRequisition_StockInData(string WHLoid, string StockIn)
        {
            string str = "";

            str = " SELECT DISTINCT RQ.CODE RQCODE, RQ.REQDATE, CU.CUCODE CUSCODE, ";
            str += " CU.TNAME||CU.CUNAME||' '||CU.CULNAME CUSNAME, SI.CODE SICODE, ";
            str += " SI.RECEIVEDATE,SI.REMARK, SI.REASON, ";
            str += " SI.CREATEBY, SI.GRANDTOT, SI.LOID SILOID, SII.LOID SIILOID ";
            str += " FROM STOCKIN SI INNER JOIN STOCKINITEM SII ON SII.STOCKIN = SI.LOID ";
            str += " INNER JOIN DOCTYPE DT ON DT.LOID = SI.DOCTYPE ";
            str += " INNER JOIN REQUISITIONITEM RQI ON RQI.LOID = SII.REFLOID AND SII.REFTABLE = 'REQUISITIONITEM'";
            str += " INNER JOIN REQUISITION RQ ON RQ.LOID = RQI.REQUISITION";
            str += " INNER JOIN (SELECT CU.LOID,NVL(TT.NAME,'') TNAME,CU.NAME CUNAME,CU.LASTNAME CULNAME,CU.CODE CUCODE  ";
            str += " FROM   CUSTOMER CU LEFT JOIN TITLE TT ON TT.LOID = CU.TITLE) CU  ";
            str += " ON CU.LOID = RQ.CUSTOMER ";
            str += " WHERE RQ.WAREHOUSE = " + WHLoid + " AND SI.LOID = " + StockIn + "";
            str += " AND DT.DOCNAME LIKE '%ใบรับคืน%' ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public bool UpdateTemptable(string UserID, ArrayList arr)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (StockInItemData data in arr)
                {
                    string sqlStockInItem = "UPDATE STOCKINITEM SET QTY  = " + data.QTY.ToString() + "";
                    sqlStockInItem += " ,UPDATEBY = '" + UserID + "', UPDATEON = SYSDATE ";
                    sqlStockInItem += " WHERE LOID = " + data.LOID.ToString();
                    OracleDB.ExecNonQueryCmd(sqlStockInItem, DBObj.zTrans);
                }
                ret = true;
                DBObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                ret = false;
                DBObj.zTrans.Rollback();
                _error = ex.Message;
            }

            DBObj.CloseConnection();
            return ret;
        }

        public bool UpdateStockIn_GrandTot(string UserID, ArrayList arr)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (StockInData data in arr)
                {
                    string sqlStockIn = "UPDATE STOCKIN SET GRANDTOT  = " + data.GRANDTOT.ToString() + "";
                    sqlStockIn += " ,UPDATEBY = '" + UserID + "', UPDATEON = SYSDATE  ,STATUS = 'AP' " ;
                    sqlStockIn += " ,APPROVEDATE = TO_DATE(SYSDATE,'DD/MM/YYYY')";
                    sqlStockIn += " ,REMARK ='" + data.REMARK + "', REASON = '" + data.REASON + "' ";
                    sqlStockIn += " WHERE LOID = " + data.LOID.ToString();
                    OracleDB.ExecNonQueryCmd(sqlStockIn, DBObj.zTrans);
                }
                ret = true;
                DBObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                ret = false;
                DBObj.zTrans.Rollback();
                _error = ex.Message;
            }

            DBObj.CloseConnection();
            return ret;
        }

        public static int CheckStockInitemData(string SILoid)
        {
            int ret = -1;
            string str = "";
            str = " SELECT STOCKIN FROM STOCKINITEM WHERE STOCKIN = " + SILoid + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            ret = dt.Rows.Count;
            return ret;

        }

        public bool DeleteStockInData(string SILOID)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                string sqlStockIn = "DELETE FROM STOCKIN  WHERE LOID = " + SILOID;
                OracleDB.ExecNonQueryCmd(sqlStockIn, DBObj.zTrans);

                ret = true;
                DBObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                ret = false;
                DBObj.zTrans.Rollback();
                _error = ex.Message;
            }

            DBObj.CloseConnection();
            return ret;
        }

        public bool InsertStockInitem(string UserId, ArrayList arr)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (StockInItemData data in arr)
                {
                    StockInItemDAL oDAL = new StockInItemDAL();

                    oDAL.QTY = Convert.ToDouble(data.QTY);
                    oDAL.STOCKIN = Convert.ToDouble(data.STOCKIN);
                    oDAL.PRODUCT = Convert.ToDouble(data.PRODUCT);
                    oDAL.STATUS = "AP";
                    oDAL.REFLOID = Convert.ToDouble(data.REFLOID);
                    oDAL.REFTABLE = "REQUISITIONITEM";
                    oDAL.PRICE = Convert.ToDouble(data.PRICE);
                    oDAL.UNIT = Convert.ToDouble(data.UNIT);


                    oDAL.InsertCurrentData(UserId, DBObj.zTrans);
                }
                ret = true;
                DBObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                ret = false;
                DBObj.zTrans.Rollback();
                _error = ex.Message;
            }

            DBObj.CloseConnection();
            return ret;
        }

        public static ArrayList GetReturnPopup(string WHLoid, string RQCODE_FROM, string RQCODE_TO, string DATEFROM, string DATETO,string CUSCODE, string PDLOID )
        {
            string str = "";
            ArrayList arrResult = new ArrayList();

            str = " SELECT DISTINCT CODE RQCODE ,INVCODE, REQDATE, CFULLNAME ";
            str += " FROM V_INVRETURN_LIST ";
            str += " WHERE WAREHOUSE =" + WHLoid + "";

            if ((RQCODE_FROM != "") && (RQCODE_TO != ""))
                str += " AND CODE BETWEEN '" + RQCODE_FROM + "' AND '" + RQCODE_TO + "'";

            if ((DATEFROM != "") && (DATETO != ""))
                str += " AND TO_DATE(REQDATE,'DD/MM/YYYY') BETWEEN TO_DATE('" + DATEFROM + "','DD/MM/YYYY') AND TO_DATE('" + DATETO + "','DD/MM/YYYY')";

            if (CUSCODE != "")
                str += " AND CCODE ='" + CUSCODE + "'";

            if (PDLOID != "0")
                str += " AND PRODUCT =" + PDLOID + "";


            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    ReturnSearchData irData = new ReturnSearchData();
                    irData.ORDERNO = i;
                    irData.RQCODE = zRd["RQCODE"].ToString();
                    irData.INVCODE = zRd["INVCODE"].ToString();
                    irData.REQDATE = Convert.ToDateTime(zRd["REQDATE"]);
                    irData.CUSNAME = zRd["CFULLNAME"].ToString();
                    arrResult.Add(irData);
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arrResult;
        }

        public static DataTable GetRequisitionItemData(string WHLoid, string RQCode)
        {
            string str = "";
            str = " SELECT DISTINCT PD.BARCODE PDCODE, PD.NAME PDNAME, RQI.QTY RQ_QTY,UN.LOID ULOID, ";
            str += " UN.NAME UNITNAME, PD.PRICE,RQI.QTY*PD.PRICE TOTAL,PD.LOID PDLOID,RQI.LOID RQILOID ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION = RQ.LOID";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            str += " INNER JOIN UNIT UN ON UN.LOID = PD.UNIT ";
            str += " WHERE WAREHOUSE = " + WHLoid + " AND UPPER(RQ.CODE) = UPPER('" + RQCode + "')";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public bool UpdateRequisition_Choose(string UserID, ArrayList arr)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (RequisitionData data in arr)
                {
                    string sqlStockIn = "UPDATE REQUISITION SET ACTIVE  = '"+ data.ACTIVE +"'";
                    sqlStockIn += " ,UPDATEBY = '" + UserID + "', UPDATEON = SYSDATE ";
                    sqlStockIn += " ,STATUS = '" + data.STATUS + "'";
                    sqlStockIn += " WHERE CODE = '" + data.CODE + "'";
                    OracleDB.ExecNonQueryCmd(sqlStockIn, DBObj.zTrans);
                }
                ret = true;
                DBObj.zTrans.Commit();
            }
            catch (Exception ex)
            {
                _error = ex.Message;
                ret = false;
                DBObj.zTrans.Rollback();
            }

            DBObj.CloseConnection();
            return ret;
        }

        public static DataTable GetRequisitionItem_CopyData(string WHLoid, string Rqcode)
        {
            string str = "";
            str = " SELECT DISTINCT RQ.CODE RQCODE, RQ.REQDATE, CUS.CODE CUSCODE, ";
            str += " TT.NAME||CUS.NAME||' '||CUS.LASTNAME CUSNAME ";
            str += " FROM   REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQ.LOID = RQI.REQUISITION";
            str += " INNER JOIN CUSTOMER CUS ON CUS.LOID = RQ.CUSTOMER ";
            str += " LEFT JOIN TITLE TT ON TT.LOID = CUS.TITLE ";
            str += " WHERE RQ.WAREHOUSE = " + WHLoid + " AND RQ.CODE = '" + Rqcode + "'";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }
    }
}
