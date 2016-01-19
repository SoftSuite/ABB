using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL
{
     public class PopUpInvoiceDAL
     {
         #region Public Method

         /// <summary>
         /// Insert Data From Object to DB
         /// </summary>
         /// <param name="UserID"></param>
         /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
         /// <returns></returns>
         public bool InsertCurrentData(string UserID, OracleTransaction zTrans)
         {
             _CREATEBY = UserID;
             _CREATEON = DateTime.Now;
             return doInsert(zTrans);
         }

         /// <summary>
         /// Update Data From Object to DB
         /// </summary>
         /// <param name="UserID"></param>
         /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
         /// <returns></returns>
         public bool UpdateCurrentData(string UserID, OracleTransaction zTrans)
         {
             _UPDATEBY = UserID;
             _UPDATEON = DateTime.Now;
             return doUpdate(" RQ.LOID = " + _LOID.ToString() + " ", zTrans);
         }

         /// <summary>
         /// Get Data From DB to Object by LOID
         /// </summary>
         /// <param name="zID"></param>
         /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
         /// <returns></returns>
         public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
         {
             return doGetdata(" RQ.LOID = " + zLOID.ToString() + " ", zTrans);
         }
         public bool GetDataByCODE(string zcode, OracleTransaction zTrans)
         {
             return doGetdataInv("INVCODE = '" + zcode.ToString() + "' ", zTrans);
         }
         /// <summary>
         /// Delete Current Data From DB
         /// </summary>
         /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
         /// <returns></returns>
         public bool DeleteCurrentData(OracleTransaction zTrans)
         {
             return doDelete(" RQ.LOID = " + _LOID.ToString() + " ", zTrans);
         }

         /// <summary>
         /// Get Data List of This Table
         /// </summary>
         /// <param name="whereCause"></param>
         /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
         /// <returns></returns>
         public DataTable GetDataList(string whereCause, OracleTransaction zTrans)
         {
             //whereCause += (whereCause == "" ? "" : " AND ") + "INVCODE IS NOT NULL AND STATUS = 'FN' ";
             whereCause += (whereCause == "" ? "" : " AND ");
             return OracleDB.ExecListCmd(sql_select + (whereCause == "" ? "" : " WHERE LOID NOT IN (SELECT REFLOID FROM REQUISITION WHERE REFTABLE = 'REQUISITION')") + whereCause);
         }

         #endregion

         #region Constant

         private string tableName = "REQUISITION";

         #endregion

         #region Private Variable
         string _error = "";
         bool _OnDB = false;
         double _LOID = 0;
         string _CODE = "";
         DateTime _REQDATE = new DateTime(1, 1, 1);
         //string _NAME = "";
         string _INVCODE = "";
         string _CUSTOMERNAME = "";
         string _CUSTOMERCODE = "";
         string _PRODUCTNAME = "";
         //string _LASTNAME = "";
         string _ACTIVE = "";
         string _CREATEBY = "";
         DateTime _CREATEON = new DateTime(1, 1, 1);
         string _UPDATEBY = "";
         DateTime _UPDATEON = new DateTime(1, 1, 1);
         #endregion

         #region Public Property
         public string TableName
         {
             get { return tableName; }
         }
         public string ErrorMessage
         {
             get { return _error; }
             set { _error = value; }
         }
         public bool OnDB
         {
             get { return _OnDB; }
             set { _OnDB = value; }
         }
         public double LOID
         {
             get { return _LOID; }
             set { _LOID = value; }
         }
         public string CODE
         {
             get { return _CODE; }
             set { _CODE = value; }
         }
         public DateTime REQDATE
         {
             get { return _REQDATE; }
             set { _REQDATE = value; }
         }
         public string INVCODE
         {
             get { return _INVCODE; }
             set { _INVCODE = value; }
         }
         public string CUSTOMERNAME
         {
             get { return _CUSTOMERNAME; }
             set { _CUSTOMERNAME = value; }
         }
         public string CUSTOMERCODE
         {
             get { return _CUSTOMERCODE; }
             set { _CUSTOMERCODE = value; }
         }
         public string PRODUCTNAME
         {
             get { return _PRODUCTNAME; }
             set { _PRODUCTNAME = value; }
         }
         public string ACTIVE
         {
             get { return _ACTIVE; }
             set { _ACTIVE = value; }
         }
         public string CREATEBY
         {
             get { return _CREATEBY; }
         }
         public DateTime CREATEON
         {
             get { return _CREATEON; }
         }
         public string UPDATEBY
         {
             get { return _UPDATEBY; }
         }
         public DateTime UPDATEON
         {
             get { return _UPDATEON; }
         }
         #endregion

         #region Query String
         private string sql_insert
         {
             get
             {
                 string sqlz = "INSERT INTO " + tableName + " (RQ.LOID, RQ.CODE, RQ.REQDATE, RQ.INVCODE,CU.NAME || ' ' || CU.LASTNAME CUSTOMERNAME,PD.NAME PRODUCTNAME) VALUES (";
                 sqlz += "  " + LOID.ToString() + ",";
                 sqlz += " '" + OracleDB.QRText(CODE) + "',";
                 sqlz += " '" + OracleDB.QRDateTime(REQDATE) + "',";
                 sqlz += " " + OracleDB.QRText(INVCODE) + ",";
                 sqlz += " " + OracleDB.QRText(_CUSTOMERNAME) + ",";
                 sqlz += " '" + OracleDB.QRText(_PRODUCTNAME) + "' ";
                 sqlz += " ) ";
                 return sqlz;
             }
         }
         private string sql_update
         {
             get
             {
                 string sqlz = " UPDATE " + tableName + " SET ";
                 sqlz += " RQ.LOID  = " + _LOID.ToString() + ", ";
                 sqlz += " RQ.CODE  = '" + OracleDB.QRText(_CODE) + "', ";
                 sqlz += " RQ.REQDATE  = '" + OracleDB.QRDateTime(_REQDATE) + "', ";
                 sqlz += " RQ.INVCODE  = '" + OracleDB.QRText(_INVCODE) + "', ";
                 sqlz += " CUSTOMERNAME  = '" + OracleDB.QRText(_CUSTOMERNAME) + "', ";
                 sqlz += " PRODUCTNAME  = " + OracleDB.QRText(_PRODUCTNAME) + " ";
                 sqlz += "  ";
                 return sqlz;
             }
         }
         private string sql_delete
         {
             get
             {
                 string sqlz = " DELETE FROM " + tableName + " ";
                 return sqlz;
             }
         }
         private string sql_select
         {
             get
             {
                 string sqlz = "SELECT DISTINCT LOID,CODE,REQDATE,INVCODE,CUSTOMERNAME FROM V_INVOICE_RETURNREQUEST ";
                 //string sqlz = "SELECT RQ.LOID,RQ.CODE,RQ.REQDATE,RQ.INVCODE,RQ.CNAME || ' ' || RQ.CLASTNAME RQCUSNAME,PD.NAME PRODUCTNAME, ";
                 //sqlz += "CU.NAME || ' ' || CU.LASTNAME CUNAME, CU.CODE CUSTOMERCODE, ";
                 //sqlz += "CASE WHEN RQ.CNAME IS NULL THEN CU.NAME || ' ' || CU.LASTNAME ";
                 //sqlz += "WHEN RQ.CNAME IS NOT NULL THEN RQ.CNAME || ' ' || RQ.CLASTNAME END AS CUSTOMERNAME ";
                 //sqlz += "FROM REQUISITION RQ INNER JOIN CUSTOMER CU ON RQ.CUSTOMER=CU.LOID ";
                 //sqlz += "INNER JOIN REQUISITIONITEM RQI ON  RQ.LOID=RQI.REQUISITION ";
                 //sqlz += "INNER JOIN PRODUCT PD ON RQI.PRODUCT= PD.LOID ";
                 //sqlz += "WHERE RQ.STATUS='AP' ";
                 sqlz += "ORDER BY INVCODE ";
                 return sqlz;
             }
         }
         #endregion

         #region Internal Method
         private bool doInsert(OracleTransaction zTrans)
         {
             bool ret = true;
             if (!_OnDB)
             {
                 try
                 {
                     _LOID = OracleDB.GetLOID(tableName, zTrans);
                     ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0);
                     if (!ret) _error = OracleDB.Err_NoInsert;
                     else _OnDB = true;
                 }
                 catch (Exception ex)
                 {
                     ret = false;
                     _error = ex.Message;
                 }
             }

             return ret;
         }

         private bool doUpdate(string whText, OracleTransaction zTrans)
         {
             bool ret = true;
             if (_OnDB)
             {
                 if (whText.Trim() != "")
                 {
                     string tmpWhere = " WHERE " + whText;
                     try
                     {
                         ret = (OracleDB.ExecNonQueryCmd(sql_update + tmpWhere, zTrans) > 0);
                         if (!ret) _error = OracleDB.Err_NoUpdate;
                     }
                     catch (Exception ex)
                     {
                         ret = false;
                         _error = ex.Message;
                     }
                 }
                 else
                 {
                     ret = false;
                     _error = OracleDB.Err_UpdateNoWhere;
                 }
             }
             else
             {
                 ret = false;
                 _error = OracleDB.Err_NoExistUpdate;
             }
             return ret;

         }

         private bool doDelete(string whText, OracleTransaction zTrans)
         {
             bool ret = true;
             if (whText.Trim() != "")
             {
                 string tmpWhere = " WHERE " + whText;
                 try
                 {
                     ret = (OracleDB.ExecNonQueryCmd(sql_delete + tmpWhere, zTrans) > 0);
                     if (!ret) _error = OracleDB.Err_NoDelete;
                     else _OnDB = false;
                 }
                 catch (Exception ex)
                 {
                     ret = false;
                     _error = ex.Message;
                 }
             }
             else
             {
                 ret = false;
                 _error = OracleDB.Err_DeleteNoWhere;
             }

             return ret;
         }

         private bool doGetdata(string whText, OracleTransaction zTrans)
         {
             bool ret = true;
             if (whText.Trim() != "")
             {
                 string tmpWhere = " WHERE " + whText;
                 OracleDataReader zRdr = null;
                 try
                 {
                     zRdr = OracleDB.ExecQueryCmd(sql_select + tmpWhere, zTrans);
                     if (zRdr.Read())
                     {
                         _OnDB = true;
                         if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                         if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                         if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
                         if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                         //if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                         //if (!Convert.IsDBNull(zRdr["UNAME"])) _UNAME = zRdr["UNAME"].ToString();
                         //if (!Convert.IsDBNull(zRdr["PRICEOLD"])) _PRICEOLD = zRdr["PRICEOLD"].ToString();
                         if (!Convert.IsDBNull(zRdr["INVCODE"])) _INVCODE = zRdr["INVCODE"].ToString();
                         if (!Convert.IsDBNull(zRdr["CUSTOMERNAME"])) _CUSTOMERNAME = zRdr["CUSTOMERNAME"].ToString();
                         if (!Convert.IsDBNull(zRdr["CUSTOMERCODE"])) _CUSTOMERCODE = zRdr["CUSTOMERCODE"].ToString();
                         if (!Convert.IsDBNull(zRdr["PRODUCTNAME"])) _PRODUCTNAME = zRdr["PRODUCTNAME"].ToString();
                      }
                     else
                     {
                         ret = false;
                         _error = OracleDB.Err_NoSelect;
                     }
                     zRdr.Close();
                 }
                 catch (Exception ex)
                 {
                     ret = false;
                     _error = ex.Message;
                     if (zRdr != null && !zRdr.IsClosed)
                         zRdr.Close();
                 }
             }
             else
             {
                 ret = false;
                 _error = "No data found.";
             }
             return ret;
         }
         private bool doGetdataInv(string whText, OracleTransaction zTrans)
         {
             bool ret = true;
             if (whText.Trim() != "")
             {
                 string tmpWhere = " WHERE " + whText;
                 OracleDataReader zRdr = null;
                 try
                 {
                     zRdr = OracleDB.ExecQueryCmd(sql_select + tmpWhere, zTrans);
                     if (zRdr.Read())
                     {
                         _OnDB = true;
                         if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                         if (!Convert.IsDBNull(zRdr["INVCODE"])) _INVCODE = zRdr["INVCODE"].ToString();
                         if (!Convert.IsDBNull(zRdr["CUSTOMERNAME"])) _CUSTOMERNAME = zRdr["CUSTOMERNAME"].ToString();
                         if (!Convert.IsDBNull(zRdr["CUSTOMERCODE"])) _CUSTOMERCODE = zRdr["CUSTOMERCODE"].ToString();
                         if (!Convert.IsDBNull(zRdr["PRODUCTNAME"])) _PRODUCTNAME = zRdr["PRODUCTNAME"].ToString();
                         //if (!Convert.IsDBNull(zRdr["REQDATE"])) _REQDATE = OracleDB.DBDate(zRdr["REQDATE"]);
                     }
                     else
                     {
                         ret = false;
                         _error = OracleDB.Err_NoSelect;
                     }
                     zRdr.Close();
                 }
                 catch (Exception ex)
                 {
                     ret = false;
                     _error = ex.Message;
                     if (zRdr != null && !zRdr.IsClosed)
                         zRdr.Close();
                 }
             }
             else
             {
                 ret = false;
                 _error = "No data found.";
             }
             return ret;
         }
         #endregion

     }
 }
