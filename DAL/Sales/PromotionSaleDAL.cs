using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;

namespace ABB.DAL.Sales
{
    public class PromotionSaleDAL
    {
        #region Private Variable        
        string _error = "";
        bool _OnDB = false;
        double _LOID = 0;
        double _PROMOTION = 0;
        double _PRODUCT = 0;
        string _BARCODE = "";
        string _NAME = "";
        string _UNAME = "";
        double _PRICEOLD = 0;
        double _PRICENEW = 0;
        DateTime _CREATEON = new DateTime(1, 1, 1);
        #endregion

        #region Public Property
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
        public double PROMOTION
        {
            get { return _PROMOTION; }
            set { _PROMOTION = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }
        public double PRICEOLD
        {
            get { return _PRICEOLD; }
            set { _PRICEOLD = value; }
        }
        public double PRICENEW
        {
            get { return _PRICENEW; }
            set { _PRICENEW = value; }
        }
        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }
        #endregion

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        {
            return doGetdata(" P.LOID = " + zLOID.ToString() + " ", zTrans);
        }

        private string sql_selectdataitem
        {
            get
            {
                string sqlz = "SELECT P.LOID,PD.BARCODE,PD.NAME,PD.UNAME,PI.PRICEOLD,PI.PRICENEW,PI.PROMOTION,PI.PRODUCT, PI.CREATEON ";
                sqlz += "FROM PROMOTION P INNER JOIN PROMOTIONITEM PI ON P.LOID = PI.PROMOTION ";
                sqlz += "INNER JOIN V_PRODUCT_LIST PD ON PD.LOID = PI.PRODUCT ";
                return sqlz;
            }
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
                    zRdr = OracleDB.ExecQueryCmd(sql_selectdataitem + tmpWhere, zTrans);
                    if (zRdr.Read())
                    {
                        _OnDB = true;
                        if (!Convert.IsDBNull(zRdr["LOID"])) _LOID = Convert.ToDouble(zRdr["LOID"]);
                        if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["NAME"])) _NAME = zRdr["NAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["UNAME"])) _UNAME = zRdr["UNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                        if (!Convert.IsDBNull(zRdr["PROMOTION"])) _PROMOTION = Convert.ToDouble(zRdr["PROMOTION"]);
                        if (!Convert.IsDBNull(zRdr["PRICEOLD"])) _PRICEOLD = Convert.ToDouble(zRdr["PRICEOLD"]);
                        if (!Convert.IsDBNull(zRdr["PRICENEW"])) _PRICENEW = Convert.ToDouble(zRdr["PRICENEW"]);
                        if (!Convert.IsDBNull(zRdr["CREATEON"])) _CREATEON = Convert.ToDateTime(zRdr["CREATEON"]);
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

        public DataTable GetPromotionList(PromotionDAL whereData)
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
            string sql = "SELECT PI.LOID, ROWNUM RANK, PD.BARCODE, PD.NAME, UNIT.NAME UNAME, PI.PRICEOLD, PI.PRICENEW, PI.PROMOTION, PI.PRODUCT ";
            sql += " FROM PROMOTIONITEM PI INNER JOIN PRODUCT PD ON PD.LOID = PI.PRODUCT ";
            sql += " INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
            sql += " WHERE PI.PROMOTION = " + loid.ToString() + " ORDER BY PD.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPromotionItemListBlank()
        {
            string sql = "SELECT 0 LOID, 0 NAME, 0 UNAME, 0 UNIT, 0 BARCODE, 0 PRICEOLD, 0 PRICENEW, 0 PROMOTION, 0 PRODUCT ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetAllProductList(double discountPercent)
        {
            string sql = "SELECT LOID, 0 RANK, BARCODE, NAME, UNAME, PRICE PRICEOLD, ROUND(PRICE-(PRICE*" + discountPercent.ToString() + "/100),0) PRICENEW, 1, LOID PRODUCT ";
            sql += " FROM V_PRODUCT_LIST ";
            sql += " ORDER BY BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
