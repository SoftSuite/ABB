using System; 
using System.Collections.Generic; 
using System.Text; 
using System.Data; 
using System.Data.OracleClient; 
using ABB.Data;
using System.Collections;
using ABB.Data.Sales;




namespace ABB.DAL.Sales 
{
    public class V_SUPPORT_LIST_DAL
    {
        public string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetSupportSearch(string WHLoid, string RqCode, string DateFrom, string DateTo, string Customer, string StatusFrom)
        {
            DataTable dt = new DataTable();
            string str = "";
            str = " SELECT DISTINCT RQCODE, RESERVEDATE,FIRSTNAME CUSNAME, LASTNAME, GRANDTOT, RQLOID, RQSTATUSNAME, ";
            str += " RQSTATUS, CUSLOID ";
            str += " FROM V_SUPPORT_LIST ";
            str += " WHERE WAREHOUSE = " + WHLoid + " ";

            //เลขที่
            if (RqCode != "")
                str += " AND UPPER(RQCODE) = UPPER('" + RqCode + "') ";

            // วันที่
            if (DateFrom != null && DateFrom != "" && DateTo != null && DateTo != "")
                str += " AND RESERVEDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY')";

            //ลูกค้า
            if (Customer != "0")
                str += " AND CUSLOID = " + Customer + " ";

            //สถานะ
            if (StatusFrom != "ALL")
                str += " AND RQSTATUS = '" + StatusFrom + "'";
                
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public DataTable GetRequisitionData(string RQLoid, String WHLoid)
        {
            DataTable dt = new DataTable();
            string str = "";
            str = " SELECT RQ.LOID RQLOID,RQ.CODE RQCODE,RQ.REQDATE, RQ.CREATEBY, ";
            str += " RQ.TOTAL,RQ.STATUS RQSTATUS, CU.LOID CUSLOID, ";
            str += " CASE WHEN RQ.STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "'";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' END  RQSTATUSNAME,CU.CODE CUSCODE, ";
            str += " NVL(TT.NAME,'')||CU.NAME||' '||CU.LASTNAME CUSNAME, ";
            str += " RQ.REASON, RQ.REMARK ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQ.LOID = RQI.REQUISITION ";
            str += " INNER JOIN CUSTOMER CU ON CU.LOID = RQ.CUSTOMER ";
            str += " LEFT JOIN TITLE TT ON TT.LOID = CU.TITLE ";
            str += " WHERE RQ.WAREHOUSE = " + WHLoid + " AND RQ.LOID = " + RQLoid + "";
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public DataTable GetRequisitionItemData(string RQLoid, String WHLoid)
        {
            DataTable dt = new DataTable();
            string str = "";
            str = " SELECT RQ.LOID RQLOID,RQI.LOID RQILOID,RQ.CODE RQCODE,RQ.REQDATE, RQ.CREATEBY, ";
            str += " PD.BARCODE PDCODE,PD.NAME PDNAME, PD.LOID PRODUCT, RQI.QTY,U.NAME UNAME,RQI.PRICE, ";
            str += " RQI.NETPRICE TOTAL,RQ.STATUS RQSTATUS,U.LOID ULOID,  ";
            str += " CASE WHEN RQ.STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "'";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' END  RQSTATUSNAME,CU.CODE CUSCODE, ";
            str += " NVL(TT.NAME,'')||CU.NAME||' '||CU.LASTNAME CUSNAME,RQI.DISCOUNT ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQ.LOID = RQI.REQUISITION ";
            str += " INNER JOIN PRODUCT PD ON RQI.PRODUCT = PD.LOID ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.UNIT ";
            str += " INNER JOIN CUSTOMER CU ON CU.LOID = RQ.CUSTOMER ";
            str += " LEFT JOIN TITLE TT ON TT.LOID = CU.TITLE ";
            str += " WHERE RQ.WAREHOUSE = " + WHLoid + " AND RQ.LOID = " + RQLoid + "";
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProductWithPromotion(double warehouse, DateTime pDate, double product, string barcode)
        {
            string where = "";
            if (warehouse != 0)
                where += (where == "" ? "" : "AND ") + "(P.WAREHOUSE IS NULL OR P.WAREHOUSE = " + warehouse.ToString() + ") ";

            if (product != 0)
                where += (where == "" ? "" : "AND ") + "PD.LOID = " + product.ToString() + " ";

            if (barcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PD.BARCODE) = '" + product.ToString() + "' ";

            string sql = "SELECT PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME UNITNAME, PD.PRICE, MAX(CASE NVL(P.LOID,0) WHEN 0 THEN 0 ELSE PI.PRICEOLD - PI.PRICENEW END) DISCOUNT, PD.ISVAT ";
            sql += "FROM PRODUCT PD INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
            sql += "LEFT JOIN PROMOTIONITEM PI ON PI.PRODUCT = PD.LOID ";
            sql += "LEFT JOIN PROMOTION P ON P.LOID = PI.PROMOTION ";
            sql += (pDate.Year == 1 ? "" : "AND (P.EPDATE IS NULL OR P.EPDATE <= " + OracleDB.QRDate(pDate) + ") AND (P.EFDATE IS NULL OR P.EFDATE >= " + OracleDB.QRDate(pDate) + ") ");
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "GROUP BY PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME, PD.PRICE, PD.ISVAT ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetCustomerDetail(double customer)
        {
            string sql = "SELECT C.LOID, C.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME AS CUSTOMERNAME, MT.DISCOUNT ";
            sql += "FROM CUSTOMER C INNER JOIN MEMBERTYPE MT ON MT.LOID = C.MEMBERTYPE ";
            sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
            sql += "WHERE C.LOID = " + customer.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetItemList(double requisition)
        {
            string sql = "SELECT RQI.REQUISITION, RQI.LOID, RQI.PRODUCT, RQI.UNIT, ROWNUM ORDERNO, PD.BARCODE, PD.NAME, RQI.QTY, UNIT.NAME UNITNAME, RQI.PRICE, ";
            sql += "RQI.DISCOUNT, RQI.NETPRICE, ";
            sql += "CASE RQI.ISVAT WHEN '" + Constz.VAT.Included.Code + "' THEN " + Constz.VAT.Included.Name + " ELSE " + Constz.VAT.NotIncluded.Name + " END AS ISVAT, PD.PRICE UNITPRICE, 0 NORMALDISCOUNT ";
            sql += "FROM REQUISITIONITEM RQI INNER JOIN PRODUCT PD ON RQI.PRODUCT = PD.LOID ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
            sql += "WHERE RQI.REQUISITION = " + requisition.ToString();
            return OracleDB.ExecListCmd(sql);
        }

        public double UpdateRequsitionItemData(string UserID, RequisitionItemData data)
        {
            Boolean ret = true;
            RequisitionItemDAL oDAL = new RequisitionItemDAL();
            oDAL.GetDataByLOID(data.LOID, null);
            oDAL.QTY = Convert.ToDouble(data.QTY);
            oDAL.NETPRICE = Convert.ToDouble(data.NETPRICE);
            oDAL.DISCOUNT = Convert.ToDouble(data.DISCOUNT);

            ret = oDAL.UpdateCurrentData(UserID, null);

            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return 0;
            }
            else
                return oDAL.LOID;
        }

        public bool InsertRequsitionItemData(string userID, RequisitionItemData data)
        {
            Boolean ret = true;
            RequisitionItemDAL oDAL = new RequisitionItemDAL();

            oDAL.REQUISITION = Convert.ToDouble(data.REQUISITION);
            oDAL.PRODUCT = Convert.ToDouble(data.PRODUCT);
            oDAL.UNIT = Convert.ToDouble(data.UNIT);
            oDAL.PRICE = Convert.ToDouble(data.PRICE);
            oDAL.QTY = Convert.ToDouble(data.QTY);
            oDAL.NETPRICE = Convert.ToDouble(data.NETPRICE);
            oDAL.DISCOUNT = Convert.ToDouble(data.DISCOUNT);
            oDAL.ACTIVE = data.ACTIVE.ToString();
           
            ret = oDAL.InsertCurrentData(userID, null);
            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;
        }

        public DataTable GetRequisitionNewData(string RQLoid,string WHLoid)
        {
            DataTable dt = new DataTable();
            string str = "";
            str = " SELECT RQ.LOID RQLOID,RQ.CODE RQCODE,RQ.REQDATE, RQ.CREATEBY, ";
            str += " RQ.TOTAL,RQ.STATUS RQSTATUS, ";
            str += " CASE WHEN RQ.STATUS = '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "'";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            str += " WHEN RQ.STATUS = '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' END  RQSTATUSNAME, ";
            str += " RQ.REASON, RQ.REMARK ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONTYPE RT ON RT.LOID = RQ.REQUISITIONTYPE AND RT.LOID = 4 ";
            str += " WHERE RQ.LOID = " + RQLoid + " AND WAREHOUSE = "+ WHLoid +"";
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public bool UpdateRequsitionData(string UserID, RequisitionData data)
        {
            bool ret = true;
            RequisitionDAL oDAL = new RequisitionDAL();
            oDAL.GetDataByLOID(data.LOID,null);
            oDAL.CUSTOMER = Convert.ToDouble(data.CUSTOMER);
            oDAL.TOTAL = Convert.ToDouble(data.TOTAL);
            oDAL.REASON = data.REASON.ToString();
            oDAL.REMARK = data.REMARK.ToString();
            
            ret = oDAL.UpdateCurrentData(UserID, null);

            if (ret == false)
            {
                _error = oDAL.ErrorMessage;
                return ret;
            }
            else
                return ret;
        }

        public  bool DeleteReq_ReqItemData(ArrayList arr)
        {
            bool ret = true;

            OracleConnection conn = OracleDB.GetConnection();
            OracleTransaction zTran = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                foreach (string loid in arr)
                {

                    string sqlStockInItem = "DELETE FROM REQUISITIONITEM WHERE REQUISITION = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlStockInItem, zTran);

                    string sqlStockIn = "DELETE FROM REQUISITION  WHERE LOID = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlStockIn, zTran);
                }
                ret = true;
                zTran.Commit();
            }
            catch (Exception ex)
            {
                ret = false;
                zTran.Rollback();
                _error = ex.Message;
                return false;
            }

            conn.Close();
            return ret;
        }

        public  bool RequisitionApprove(string userID, ArrayList arr)
        {
            bool ret = true;
            OracleConnection conn = OracleDB.GetConnection();
            OracleTransaction zTran = conn.BeginTransaction(IsolationLevel.ReadCommitted);

            try
            {
                foreach (string loid in arr)
                {
                    string sqlRequisition = "UPDATE REQUISITION SET STATUS = '" + Constz.Requisition.Status.Approved.Code + "' ";
                    sqlRequisition += " WHERE LOID = " + loid;
                    OracleDB.ExecNonQueryCmd(sqlRequisition, zTran);
                    RequisitionDAL rDAL = new RequisitionDAL();
                    bool rr = rDAL.CutStockRequisition(Convert.ToDouble(loid), userID, zTran);
                }
                ret = true;
                zTran.Commit();
                
            }           
            catch (Exception ex)
            {
                ret = false;
                zTran.Rollback();
                _error = ex.Message;
                return false;
                
            }

            conn.Close();
            return ret;
        }

        public static int CheckRequisitionItemData(string RQLoid)
        {
            int ret = -1;
            string str = "";
            str = " SELECT REQUISITION FROM REQUISITIONITEM WHERE REQUISITION = " + RQLoid + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            ret = dt.Rows.Count;
            return ret;

        }

        public static bool DeleteRequisition(RequisitionData data)
        {
            Boolean ret = true;
            RequisitionDAL oDAL = new RequisitionDAL();
            oDAL.GetDataByLOID(data.LOID, null);
            ret = oDAL.DeleteCurrentData(null);
            return ret;
        }

        public static DataTable GetProductByBarcode(double warehouse, DateTime pDate, string barcode)
        {
            string where = "";
            if (warehouse != 0)
                where += (where == "" ? "" : "AND ") + "(P.WAREHOUSE IS NULL OR P.WAREHOUSE = " + warehouse.ToString() + ") ";

            if (barcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PD.BARCODE) = '" + barcode.ToString() + "' ";

            string sql = "SELECT PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME UNITNAME, PD.PRICE, MAX(CASE NVL(P.LOID,0) WHEN 0 THEN 0 ELSE PI.PRICEOLD - PI.PRICENEW END) DISCOUNT, PD.ISVAT ";
            sql += "FROM PRODUCT PD INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
            sql += "LEFT JOIN PROMOTIONITEM PI ON PI.PRODUCT = PD.LOID ";
            sql += "LEFT JOIN PROMOTION P ON P.LOID = PI.PROMOTION ";
            sql += (pDate.Year == 1 ? "" : "AND (P.EPDATE IS NULL OR P.EPDATE <= " + OracleDB.QRDate(pDate) + ") AND (P.EFDATE IS NULL OR P.EFDATE >= " + OracleDB.QRDate(pDate) + ") ");
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "GROUP BY PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME, PD.PRICE, PD.ISVAT ";
            return OracleDB.ExecListCmd(sql);
        }


    }
}