using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.Data.Sales;
using ABB.DAL.Sales;
using ABB.Data;

namespace ABB.Flow.Sales
{
    public class ProductStockInShopFlow
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public static ArrayList GetSearchProductStockInShop(string WHLoid, string SICode, string DateFrom, string DateTo, string RQCode)
        {
            string str = "";
            ArrayList arrResult = new ArrayList();

            str = " SELECT DISTINCT SICODE,RECEIVEDATE, RQCODE, REQDATE, TOTAL, WAREHOUSE, LOID, SISTATUS, ";
            str += " CASE WHEN SISTATUS = 'AP' THEN 'ยืนยัน' WHEN SISTATUS = 'WA' THEN 'กำลังทำรายการ' ";
            str += " WHEN SISTATUS = 'VO' THEN 'ยกเลิก' END SISTATUSNAME ";
            str += " FROM V_PRODUCTSTOCKINSHOP ";
            str += " WHERE WAREHOUSE = " + WHLoid + "";

            if (SICode != "")
            {
                str += " AND UPPER(SICODE) = UPPER('" + SICode + "')";
            }

            if (DateFrom != null && DateTo != null)
            {
                str += " AND RECEIVEDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY')";
            }

            if (RQCode != "")
            {
                str += " AND RQCode = '" + RQCode + "'";
            }

            str += " ORDER BY SICODE DESC ";

            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    ProductStockInShopSearchData irData = new ProductStockInShopSearchData();
                    irData.ORDERNO = i;
                    irData.CHKAPPROVE = false;
                    irData.LOID = Convert.ToDouble(zRd["LOID"]);
                    irData.SICODE = zRd["SICODE"].ToString();
                    irData.RECEIVEDATE = Convert.ToDateTime(zRd["RECEIVEDATE"]);
                    irData.RQCODE = zRd["RQCODE"].ToString();
                    irData.REQDATE = zRd["REQDATE"].ToString();
                    irData.TOTAL = Convert.ToDouble(zRd["TOTAL"]);
                    irData.WAREHOUSE = Convert.ToDouble(zRd["WAREHOUSE"]);
                    irData.SISTATUS = zRd["SISTATUS"].ToString();
                    irData.SISTATUSNAME = zRd["SISTATUSNAME"].ToString();
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

        public bool UpdateStockInData(string userID, ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (string loid in arrLOID)
                {
                    string sqlStockIn = "UPDATE STOCKIN SET STATUS = 'AP', APPROVEDATE = TO_DATE(SYSDATE,'DD/MM/YYYY') "; 
                           sqlStockIn += " WHERE LOID = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlStockIn, DBObj.zTrans);

                    string sqlStockInItem = "UPDATE STOCKINITEM SET STATUS = 'AP' WHERE STOCKIN = " + loid;
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

        public static DataTable GetProductStockInShopData(string WHLoid, string StockIn)
        {
            string str = "";
            str = " SELECT PD.BARCODE PDCODE, PD.NAME PDNAME, RQI.QTY RQ_QTY, SII.QTY RECEIVE_QTY, ";
            str += " UN.NAME UNITNAME, SII.PRICE,SI.GRANDTOT TOTAL,SI.LOID SILOID,SII.LOID SIILOID, ";
            str += " PD.LOID PDLOID, RQI.LOID RQILOID,SI.STATUS SISTATUS, SII.STATUS SIISTATUS,UN.LOID ULOID, ";
            str += " SII.QTY * SII.PRICE SSUM ";
            str += " FROM STOCKIN SI INNER JOIN STOCKINITEM SII ON SII.STOCKIN = SI.LOID ";
            str += " INNER JOIN REQUISITIONITEM RQI ON RQI.LOID = SII.REFLOID AND SII.REFTABLE = 'REQUISITIONITEM'";
            str += " INNER JOIN REQUISITION RQ ON RQI.REQUISITION = RQ.LOID";
            str += " INNER JOIN REQUISITIONTYPE RQT ON RQT.LOID = RQ.REQUISITIONTYPE AND RQT.LOID = 6 ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = SII.PRODUCT ";
            str += " INNER JOIN UNIT UN ON UN.LOID = PD.UNIT ";
            str += " WHERE WAREHOUSE = " + WHLoid + " AND SI.LOID = " + StockIn + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetRequisition_StockInData(string WHLoid, string StockIn)
        {
            string str = "";

            str = " SELECT DISTINCT RQ.CODE RQCODE,SI.CODE SICODE,SI.RECEIVEDATE, SI.CREATEBY,RQ.REMARK, ";
            str += " SI.GRANDTOT TOTAL,SI.STATUS SISTATUS, STI.STATUS SIISTATUS,SI.LOID SILIOID ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION = RQ.LOID";
            str += " INNER JOIN STOCKINITEM STI ON STI.REFLOID = RQI.LOID AND STI.REFTABLE = 'REQUISITIONITEM' ";
            str += " INNER JOIN STOCKIN SI ON SI.LOID = STI.STOCKIN ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            str += " WHERE WAREHOUSE = " + WHLoid + " AND SI.LOID = " + StockIn + "";

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

        public double InsertStockIn(string UserId, StockInData data)
        {
            Boolean ret = true;
            StockInDAL oDAL = new StockInDAL();
            oDAL.SENDER = Convert.ToDouble(data.SENDER);
            oDAL.DOCTYPE = Constz.DocType.RecShop.LOID;
            oDAL.RECEIVEDATE = DateTime.Now.Date;
            oDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
            oDAL.RECEIVER = 999999999;

            ret = oDAL.InsertCurrentData(UserId, null);

            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return 0;
            }
            else
                return oDAL.LOID;
        }

        public static DataTable GetRequisitionItemData(string WHLoid, string RQCode)
        {
            string str = "";
            str = " SELECT PD.BARCODE PDCODE, PD.NAME PDNAME, RQI.QTY RQ_QTY, 0 AS RECEIVE_QTY, UN.LOID ULOID, ";
            str += " UN.NAME UNITNAME, PD.PRICE,RQI.QTY*PD.PRICE TOTAL,PD.LOID PDLOID,RQI.LOID RQILOID ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION = RQ.LOID";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            str += " INNER JOIN UNIT UN ON UN.LOID = PD.UNIT ";
            str += " WHERE WAREHOUSE = " + WHLoid + " AND UPPER(RQ.CODE) = UPPER('" + RQCode + "')";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
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
                    oDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                    oDAL.REFLOID = Convert.ToDouble(data.REFLOID);
                    oDAL.REFTABLE = "REQUISITIONITEM";
                    oDAL.PRICE  = Convert.ToDouble(data.PRICE);
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

        public static DataTable GetStockInData(string SILoid)
        {
            string str = "";
            str = " SELECT CODE , RECEIVEDATE, CREATEBY ";
            str += " FROM STOCKIN ";
            str += " WHERE LOID = "+ SILoid +"";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
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

        public static ArrayList GetProductStockInShopPopup(string WHLoid, string RQCODE_FROM, string RQCODE_TO, string DATEFROM, string DATETO)
        {
            string str = "";
            ArrayList arrResult = new ArrayList();

            str = " SELECT DISTINCT RQ.CODE RQCODE ,RQ.RESERVEDATE,RQ.CREATEBY ";
            str += " FROM STOCKOUT SO INNER JOIN STOCKOUTITEM SOI ON SOI.STOCKOUT = SO.LOID ";
            str += " INNER JOIN REQUISITIONITEM RQI ON RQI.LOID = SOI.REFLOID AND SOI.REFTABLE = 'REQUISITIONITEM' ";
            str += " INNER JOIN REQUISITION RQ ON RQI.REQUISITION = RQ.LOID" ;
            str += " INNER JOIN REQUISITIONTYPE RQT ON RQT.LOID = RQ.REQUISITIONTYPE AND RQT.LOID = 6 ";
            str += " WHERE SO.ACTIVE = '" + Constz.ActiveStatus.Active + "' AND SO.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND SOI.ACTIVE = '" + Constz.ActiveStatus.Active + "' AND SOI.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
            str += " AND RQ.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' AND RQ.WAREHOUSE = " + WHLoid + "";

            if ((RQCODE_FROM != "") && (RQCODE_TO != ""))
                str += " AND RQ.CODE BETWEEN '" + RQCODE_FROM + "' AND '" + RQCODE_TO + "'";

            if ((DATEFROM != "") && (DATETO != ""))
                str += " AND RESERVEDATE BETWEEN TO_DATE('" + DATEFROM + "','DD/MM/YYYY') AND TO_DATE('" + DATETO + "','DD/MM/YYYY')";

            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    ProductStockInShopPopupData irData = new ProductStockInShopPopupData();
                    irData.ORDERNO = i;
                    irData.RQCODE = zRd["RQCODE"].ToString();
                    irData.RECEIVEDATE = Convert.ToDateTime(zRd["RESERVEDATE"]);
                    irData.CREATEBY = zRd["CREATEBY"].ToString();
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
                    sqlStockIn += " ,UPDATEBY = '" + UserID + "', UPDATEON = SYSDATE  ";
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

        public bool UpdateStockInApproveData(string userID, ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj DBObj = new OracleDBObj();
            DBObj.CreateConnection();
            DBObj.CreateTransaction();

            try
            {
                foreach (string loid in arrLOID)
                {
                    string sqlStockIn = "UPDATE STOCKIN SET STATUS = '" + Constz.Requisition.Status.Approved.Code + "', ";
                           sqlStockIn += " RECEIVEDATE = TO_DATE(SYSDATE,'DD/MM/YYYY'), ";
                           sqlStockIn += " APPROVEDATE = TO_DATE(SYSDATE,'DD/MM/YYYY') ";
                           sqlStockIn += " WHERE LOID = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlStockIn, DBObj.zTrans);

                    string sqlStockInItem = "UPDATE STOCKINITEM SET STATUS = '" + Constz.Requisition.Status.Approved.Code + "' WHERE STOCKIN = " + loid;
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

    }
}
