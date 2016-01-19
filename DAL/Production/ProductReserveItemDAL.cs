using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data; 

namespace ABB.DAL.Production
{
    public class ProductReserveItemDAL
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
            return doUpdate(" PDLOID = " + _PDLOID.ToString() + " ", zTrans);
        }
 
        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        {
            return doGetdata(" PDLOID = " + zLOID.ToString() + " ", zTrans);
        }
  
        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteCurrentData(OracleTransaction zTrans)
        {
            return doDelete(" PDLOID = " + _PDLOID.ToString() + " ", zTrans);
        }
          
        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataList(string whereCause, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause);
        }

        #endregion
 
        #region Constant

        private string tableName = "V_BOM_LIST";

        #endregion
 
        #region Private Variable
        string _error = "";
        bool _OnDB = false; 
        double _PDLOID = 0;
        string _CREATEBY = "";
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _UPDATEBY = "";
        DateTime _UPDATEON = new DateTime(1, 1, 1);
        double _MASTER = 0;
        string _RWBARCODE = "";
        string _RWNAME = "";
        string _RWGROUPNAME = "";
        string _UNAME = "";
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
        public double PDLOID 
        {
           get { return _PDLOID; } 
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
        public double MASTER 
        {
           get { return _MASTER; } 
           set { _MASTER= value; } 
        }
        public string RWBARCODE 
        {
           get { return _RWBARCODE; } 
           set { _RWBARCODE= value; } 
        }
        public string RWNAME 
        {
           get { return _RWNAME; } 
           set { _RWNAME= value; } 
        }
        public string RWGROUPNAME
        {
           get { return _RWGROUPNAME; } 
           set { _RWGROUPNAME= value; } 
        }
         public string UNAME
        {
           get { return _UNAME; } 
           set { _UNAME= value; } 
        }
        #endregion

        #region Query String 
        private string sql_insert 
        { 
           get
           {
               string sqlz =  "INSERT INTO " + tableName + " (PDLOID,CREATEBY,CREATEON,UPDATEBY,UPDATEON,MASTER,RWBARCODE,RWNAME,RWGROUPNAME,UNAME)VALUES(";
                sqlz += "  "+_PDLOID.ToString() + ",";// LOID";
                sqlz += " '"+ OracleDB.QRText(_CREATEBY)+ "',";// CREATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_CREATEON)+ ",";// CREATEON";
                sqlz += " '"+ OracleDB.QRText(_UPDATEBY)+ "',";// UPDATEBY";
                sqlz += " "+ OracleDB.QRDateTime(_UPDATEON)+ ",";// UPDATEON";
                sqlz += "  "+_MASTER.ToString() + ",";// MASTER";
                sqlz += " '"+ OracleDB.QRText(_RWBARCODE)+ "',";// RWBARCODE";
                sqlz += " '"+ OracleDB.QRText(_RWNAME)+ "',";// RWNAME";
                sqlz += " '"+ OracleDB.QRText(_RWGROUPNAME)+ "',";// RWGROUPNAME";
                sqlz += " '"+ OracleDB.QRText(_UNAME)+ "',";// UNAME";
                sqlz += " ) "; 
                return sqlz; 
           } 
        }
        private string sql_update 
        { 
           get
           {
               string sqlz = " UPDATE " + tableName + " SET ";
                sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY)+ "', ";
                sqlz += " UPDATEON  = " + OracleDB.QRDateTime(_UPDATEON)+ ", ";
                sqlz += " MASTER  = " + _MASTER.ToString()+", ";
                sqlz += " RWBARCODE  = '" + OracleDB.QRText(_RWBARCODE)+ "', ";
                sqlz += " RWNAME  = '" + OracleDB.QRText(_RWNAME)+ "', ";
                sqlz += " RWGROUPNAME  = '" + OracleDB.QRText(_RWGROUPNAME)+ "', ";
                sqlz += " UNAME  = '" + OracleDB.QRText(_UNAME)+ "' ";
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
                 string sqlz = " SELECT * FROM " + tableName + " "; 
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
                     _PDLOID = OracleDB.GetLOID(tableName, zTrans); 
                     ret = (OracleDB.ExecNonQueryCmd(sql_insert, zTrans) > 0); 
                     if (!ret) _error = OracleDB.Err_NoInsert; 
                     else _OnDB = true;
                 }
                 catch (OracleException ex)
                 {
                     ret = false;
                     _error = OracleDB.GetOracleExceptionText(ex);
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
                     catch (OracleException ex)
                     {
                         ret = false;
                         _error = OracleDB.GetOracleExceptionText(ex);
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
                 catch (OracleException ex)
                 {
                     ret = false;
                     _error = OracleDB.GetOracleExceptionText(ex);
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
                     zRdr = OracleDB.ExecQueryCmd(sql_select +tmpWhere , zTrans);
                     if (zRdr.Read())
                     {
                         _OnDB = true;
                            if (!Convert.IsDBNull(zRdr["LOID"]))  _PDLOID = Convert.ToDouble(zRdr["LOID"]);
                            if (!Convert.IsDBNull(zRdr["CREATEBY"]))  _CREATEBY = zRdr["CREATEBY"].ToString();
                            if (!Convert.IsDBNull(zRdr["CREATEON"]))  _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
                            if (!Convert.IsDBNull(zRdr["UPDATEBY"]))  _UPDATEBY = zRdr["UPDATEBY"].ToString();
                            if (!Convert.IsDBNull(zRdr["UPDATEON"]))  _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
                            if (!Convert.IsDBNull(zRdr["MASTER"]))  _MASTER = Convert.ToDouble(zRdr["MASTER"]);
                            if (!Convert.IsDBNull(zRdr["RWBARCODE"]))  _RWBARCODE = zRdr["RWBARCODE"].ToString();
                            if (!Convert.IsDBNull(zRdr["RWNAME"]))  _RWNAME = zRdr["RWNAME"].ToString();
                            if (!Convert.IsDBNull(zRdr["RWGROUPNAME"]))  _RWNAME = zRdr["RWGROUPNAME"].ToString();
                            if (!Convert.IsDBNull(zRdr["UNAME"]))  _RWNAME = zRdr["UNAME"].ToString();
                    }
                    else
                    {
                         ret = false;
                         _error = OracleDB.Err_NoSelect;
                    }
                    zRdr.Close();
                }
                catch (OracleException ex)
                {
                    ret = false;
                    _error = OracleDB.GetOracleExceptionText(ex);
                    if (zRdr != null && !zRdr.IsClosed)
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

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool DeleteDataByRequisition(double requisition, OracleTransaction zTrans)
        {
            return doDelete("PDLOID = " + requisition.ToString() + " ", zTrans);
        }
        //public bool DeleteCurrentData(string where, OracleTransaction zTrans)
        //{
        //    return doDelete(where, zTrans);
        //}

        /// <summary>
        /// Delete Current Data From DB
        /// </summary>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public DataTable GetDataByRequisition(double requisition, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + " WHERE PDLOID = " + requisition, zTrans);
        }

    }
}