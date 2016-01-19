using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL
{
    public class PromotionSalesItemDAL
    {
        //#region Public Method

        ///// <summary>
        ///// Insert Data From Object to DB
        ///// </summary>
        ///// <param name="UserID"></param>
        ///// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        ///// <returns></returns>
        //public bool InsertCurrentData(string UserID, OracleTransaction zTrans)
        //{
        //    _CREATEBY = UserID;
        //    _CREATEON = DateTime.Now;
        //    return doInsert(zTrans);
        //}

        ///// <summary>
        ///// Update Data From Object to DB
        ///// </summary>
        ///// <param name="UserID"></param>
        ///// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        ///// <returns></returns>
        //public bool UpdateCurrentData(string UserID, OracleTransaction zTrans)
        //{
        //    _UPDATEBY = UserID;
        //    _UPDATEON = DateTime.Now;
        //    return doUpdate(" P.LOID = " + _LOID.ToString() + " ", zTrans);
        //}

        ///// <summary>
        ///// Get Data From DB to Object by LOID
        ///// </summary>
        ///// <param name="zID"></param>
        ///// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        ///// <returns></returns>
        //public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        //{
        //    return doGetdata(" P.LOID = " + zLOID.ToString() + " ", zTrans);
        //}

        ///// <summary>
        ///// Delete Current Data From DB
        ///// </summary>
        ///// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        ///// <returns></returns>
        //public bool DeleteCurrentData(OracleTransaction zTrans)
        //{
        //    return doDelete(" P.LOID = " + _LOID.ToString() + " ", zTrans);
        //}

        ///// <summary>
        ///// Get Data List of This Table
        ///// </summary>
        ///// <param name="whereCause"></param>
        ///// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        ///// <returns></returns>
        //public DataTable GetDataList(string whereCause, OracleTransaction zTrans)
        //{
        //    return OracleDB.ExecListCmd(sql_selectdataitem + whereCause);
        //}

        //#endregion

        //#region Constant

        //private string tableName = "PROMOTION";

        //#endregion

        //#region Private Variable
        //string _error = "";
        //bool _OnDB = false;
        //double _LOID = 0;
        //string _BARCODE = "";
        //string _CREATEBY = "";
        //DateTime _CREATEON = new DateTime(1, 1, 1);
        //string _UPDATEBY = "";
        //DateTime _UPDATEON = new DateTime(1, 1, 1);
        //string _NAME = "";
        //string _UNAME = "";
        //string _PRICEOLD = "";
        //string _PRICENEW = "";
        //#endregion

        //#region Public Property
        //public string TableName
        //{
        //    get { return tableName; }
        //}
        //public string ErrorMessage
        //{
        //    get { return _error; }
        //    set { _error = value; }
        //}
        //public bool OnDB
        //{
        //    get { return _OnDB; }
        //    set { _OnDB = value; }
        //}
        //public double LOID
        //{
        //    get { return _LOID; }
        //    set { _LOID = value; }
        //}
        //public string BARCODE
        //{
        //    get { return _BARCODE; }
        //    set { _BARCODE = value; }
        //}
        //public string CREATEBY
        //{
        //    get { return _CREATEBY; }
        //}
        //public DateTime CREATEON
        //{
        //    get { return _CREATEON; }
        //}
        //public string UPDATEBY
        //{
        //    get { return _UPDATEBY; }
        //}
        //public DateTime UPDATEON
        //{
        //    get { return _UPDATEON; }
        //}
        //public string NAME
        //{
        //    get { return _NAME; }
        //    set { _NAME = value; }
        //}
        //public string UNAME
        //{
        //    get { return _UNAME; }
        //    set { _UNAME = value; }
        //}
        //public string PRICEOLD
        //{
        //    get { return _PRICEOLD; }
        //    set { _PRICEOLD = value; }
        //}
        //public string PRICENEW
        //{
        //    get { return _PRICENEW; }
        //    set { _PRICENEW = value; }
        //}
        //#endregion

        //#region Query String
        //private string sql_insertdataitem
        //{
        //    get
        //    {
        //        string sqlz = "INSERT INTO " + tableName + " (LOID, BARCODE, CREATEBY, CREATEON, NAME, UNAME, PRICEOLD, PRICENEW) VALUES (";
        //        sqlz += "  " + _LOID.ToString() + ",";
        //        sqlz += " '" + OracleDB.QRText(_BARCODE) + "',";
        //        sqlz += " '" + OracleDB.QRText(_CREATEBY) + "',";
        //        sqlz += " " + OracleDB.QRDateTime(_CREATEON) + ",";
        //        sqlz += " " + OracleDB.QRText(_NAME) + ",";
        //        sqlz += " '" + OracleDB.QRText(_UNAME) + "',";
        //        sqlz += " '" + OracleDB.QRText(_PRICEOLD) + "',";
        //        sqlz += " '" + OracleDB.QRText(_PRICENEW) + "' ";
        //        sqlz += " ) ";
        //        return sqlz;
        //    }
        //}
        //private string sql_updatedataitem
        //{
        //    get
        //    {
        //        string sqlz = " UPDATE " + tableName + " SET ";
        //        sqlz += " LOID  = " + _LOID.ToString() + ", ";
        //        sqlz += " BARCODE  = '" + OracleDB.QRText(_BARCODE) + "', ";
        //        sqlz += " UPDATEBY  = '" + OracleDB.QRText(_UPDATEBY) + "', ";
        //        sqlz += " UPDATEON  = '" + OracleDB.QRDate(_UPDATEON) + "', ";
        //        sqlz += " NAME  = '" + OracleDB.QRText(_NAME) + "', ";
        //        sqlz += " UNAME  = '" + OracleDB.QRText(_UNAME) + "', ";
        //        sqlz += " PRICEOLD  = '" + OracleDB.QRText(_PRICEOLD) + "', ";
        //        sqlz += " PRICENEW  = " + OracleDB.QRText(_PRICENEW) + " ";
        //        sqlz += "  ";
        //        return sqlz;
        //    }
        //}
        //private string sql_deletedataitem
        //{
        //    get
        //    {
        //        string sqlz = " DELETE FROM " + tableName + " ";
        //        return sqlz;
        //    }
        //}
        //private string sql_selectdataitem
        //{
        //    get
        //    {
        //        string sqlz = "SELECT P.LOID,PD.BARCODE,PD.NAME,PD.UNAME,PI.PRICEOLD,PI.PRICENEW ";
        //        sqlz += "FROM PROMOTION P INNER JOIN V_PRODUCT_LIST PD ON PD.LOID = P.LOID ";
        //        sqlz += "INNER JOIN PROMOTIONITEM PI ON P.LOID = PI.PROMOTION ";
        //        return sqlz;
        //    }
        //}
        //#endregion

        //#region Internal Method
        //private bool doInsert(OracleTransaction zTrans)
        //{
        //    bool ret = true;
        //    if (!_OnDB)
        //    {
        //        try
        //        {
        //            _LOID = OracleDB.GetLOID(tableName, zTrans);
        //            ret = (OracleDB.ExecNonQueryCmd(sql_insertdataitem, zTrans) > 0);
        //            if (!ret) _error = OracleDB.Err_NoInsert;
        //            else _OnDB = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            ret = false;
        //            _error = ex.Message;
        //        }
        //    }

        //    return ret;
        //}

        //private bool doUpdate(string whText, OracleTransaction zTrans)
        //{
        //    bool ret = true;
        //    if (_OnDB)
        //    {
        //        if (whText.Trim() != "")
        //        {
        //            string tmpWhere = " WHERE " + whText;
        //            try
        //            {
        //                ret = (OracleDB.ExecNonQueryCmd(sql_updatedataitem + tmpWhere, zTrans) > 0);
        //                if (!ret) _error = OracleDB.Err_NoUpdate;
        //            }
        //            catch (Exception ex)
        //            {
        //                ret = false;
        //                _error = ex.Message;
        //            }
        //        }
        //        else
        //        {
        //            ret = false;
        //            _error = OracleDB.Err_UpdateNoWhere;
        //        }
        //    }
        //    else
        //    {
        //        ret = false;
        //        _error = OracleDB.Err_NoExistUpdate;
        //    }
        //    return ret;

        //}

        //private bool doDelete(string whText, OracleTransaction zTrans)
        //{
        //    bool ret = true;
        //    if (whText.Trim() != "")
        //    {
        //        string tmpWhere = " WHERE " + whText;
        //        try
        //        {
        //            ret = (OracleDB.ExecNonQueryCmd(sql_deletedataitem + tmpWhere, zTrans) > 0);
        //            if (!ret) _error = OracleDB.Err_NoDelete;
        //            else _OnDB = false;
        //        }
        //        catch (Exception ex)
        //        {
        //            ret = false;
        //            _error = ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        ret = false;
        //        _error = OracleDB.Err_DeleteNoWhere;
        //    }

        //    return ret;
        //}

        //private bool doGetdata(string whText, OracleTransaction zTrans)
        //{
        //    bool ret = true;
        //    if (whText.Trim() != "")
        //    {
        //        string tmpWhere = " WHERE " + whText;
        //        OracleDataReader zRdr = null;
        //        try
        //        {
        //            zRdr = OracleDB.ExecQueryCmd(sql_selectdataitem + tmpWhere, zTrans);
        //            if (zRdr.Read())
        //            {
        //                _OnDB = true;
        //                if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
        //                if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
        //                if (!Convert.IsDBNull(zRdr["CREATEBY"])) _CREATEBY = zRdr["CREATEBY"].ToString();
        //                if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = OracleDB.DBDate(zRdr["CREATEON"]);
        //                if (!Convert.IsDBNull(zRdr["UPDATEBY"])) _UPDATEBY = zRdr["UPDATEBY"].ToString();
        //                if (!Convert.IsDBNull(zRdr["UPDATEON"])) _UPDATEON = OracleDB.DBDate(zRdr["UPDATEON"]);
        //                if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
        //                if (!Convert.IsDBNull(zRdr["UNAME"])) _UNAME = zRdr["UNAME"].ToString();
        //                if (!Convert.IsDBNull(zRdr["PRICEOLD"])) _PRICEOLD = zRdr["PRICEOLD"].ToString();
        //                if (!Convert.IsDBNull(zRdr["PRICENEW"])) _PRICENEW = zRdr["PRICENEW"].ToString();
        //            }
        //            else
        //            {
        //                ret = false;
        //                _error = OracleDB.Err_NoSelect;
        //            }
        //            zRdr.Close();
        //        }
        //        catch (Exception ex)
        //        {
        //            ret = false;
        //            _error = ex.Message;
        //            if (zRdr != null && !zRdr.IsClosed)
        //                zRdr.Close();
        //        }
        //    }
        //    else
        //    {
        //        ret = false;
        //        _error = "No data found.";
        //    }
        //    return ret;
        //}

        //#endregion

        public DataTable GetPromotionList(PromotionSalesDAL whereData)
        {
            string whereString = "";
            if (whereData.LOID != 0)
                whereString += (whereString == "" ? "" : "AND ") + "LOID = " + whereData.LOID.ToString() + " ";
            if (whereData.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE = '" + OracleDB.QRText(whereData.CODE.Trim()) + "' ";

            string sql = "SELECT P.LOID,PD.BARCODE,PD.NAME,PD.UNAME,PI.PRICEOLD,PI.PRICENEW ";
            sql += "FROM PROMOTION P INNER JOIN V_PRODUCT_LIST PD ON PD.LOID = P.LOID ";
            sql += "INNER JOIN PROMOTIONITEM PI ON P.LOID = PI.PROMOTION ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY BARCODE ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPromotionItemList(double loid)
        {
            string sql = "SELECT P.LOID, ROWNUM RANK,PD.BARCODE,PD.NAME,PD.UNAME,PI.PRICEOLD,PI.PRICENEW,PI.PROMOTION,PI.PRODUCT ";
            sql += " FROM PROMOTIONITEM PI ";
            sql += " INNER JOIN PROMOTION P ON P.LOID = PI.PROMOTION ";
            sql += " INNER JOIN V_PRODUCT_LIST PD ON PD.LOID = PI.PRODUCT ";
            sql += " WHERE P.LOID = " + loid;
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPromotionItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 NAME, 0 UNAME, 0 UNIT, 0 BARCODE, 0 PRICEOLD, 0 PRICENEW, 0 PROMOTION, 0 PRODUCT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
