using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class SaleDAL
    {
        #region Variables

        private double _PRODUCT = 0;
        private string _PRODUCTNAME = "";
        private double _UNIT = 0;
        private string _UNITNAME = "";
        private double _UNITPRICE = 0;
        private double _DISCOUNT = 0;
        private string _ISVAT = "";
        private string _BARCODE = "";
        private double _STOCKQTY = 0;
        private string _ISDISCOUNT = "";
        private string _ISEDIT = "";
        private string _error = "";

        #endregion

        #region Property

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

        public double UNITPRICE
        {
            get { return _UNITPRICE; }
            set { _UNITPRICE = value; }
        }

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
            set { _DISCOUNT = value; }
        }

        public string ISVAT
        {
            get { return _ISVAT; }
            set { _ISVAT = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public double STOCKQTY
        {
            get { return _STOCKQTY; }
            set { _STOCKQTY = value; }
        }

        public string ISDISCOUNT
        {
            get { return _ISDISCOUNT; }
            set { _ISDISCOUNT = value; }
        }

        public string ISEDIT
        {
            get { return _ISEDIT; }
            set { _ISEDIT = value; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        #endregion

        private string SelectStatement(double warehouse)
        {
            string sql = "SELECT 0 RANK, 0 LOID, PD.LOID PRODUCT, PD.BARCODE, PD.NAME PRODUCTNAME, PD.UNIT, UNIT.NAME UNITNAME, PD.PRICE, 1 QTY, ";
            sql += "PD.PRICE NETPRICE, PD.ISVAT, PD.ISDISCOUNT, FN_GETPRODUCTDISCOUNT(PD.LOID, " + warehouse.ToString() + ", PD.ISDISCOUNT) DISCOUNT, ";
            sql += "FN_GETPRODUCTDISCOUNT(PD.LOID, " + warehouse.ToString() + ", PD.ISDISCOUNT) NORMALDISCOUNT, ";
            //sql += "FN_GETPRODUCTSTOCKQTY(" + warehouse.ToString() + ", " + Constz.Zone.Z01.ToString() + ", PD.PRODUCTMASTER) STOCKQTY ";
            sql += "FN_GETPRODUCTSTOCKQTY(1, " + Constz.Zone.Z01.ToString() + ", PD.PRODUCTMASTER) STOCKQTY, PD.ISEDIT ";
            sql += "FROM PRODUCT PD INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
            return sql;
        }

        public DataTable GetDataList(string productList, double warehouse)
        {
            string sql = SelectStatement(warehouse);
            sql += "WHERE PD.LOID IN (" + (productList.Trim() == "" ? "0" : productList.Trim()) + ") ";
            sql += "ORDER BY PD.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        private bool doGetdata(double warehouse, double product, string barcode, OracleTransaction zTrans)
        {
            bool ret = true;
            string where = "";

            if (product != 0)
                where += (where == "" ? "" : "AND ") + "PD.LOID = " + product.ToString() + " ";

            if (barcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PD.BARCODE) = '" + barcode + "' ";

            string sql = SelectStatement(warehouse);
            sql += (where == "" ? "" : "WHERE ") + where;

            OracleDataReader zRdr = null;
            try
            {
                zRdr = OracleDB.ExecQueryCmd(sql, zTrans);
                if (zRdr.Read())
                {
                    if (!Convert.IsDBNull(zRdr["PRODUCT"])) _PRODUCT = Convert.ToDouble(zRdr["PRODUCT"]);
                    if (!Convert.IsDBNull(zRdr["BARCODE"])) _BARCODE = zRdr["BARCODE"].ToString();
                    if (!Convert.IsDBNull(zRdr["PRODUCTNAME"])) _PRODUCTNAME = zRdr["PRODUCTNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["UNIT"])) _UNIT = Convert.ToDouble(zRdr["UNIT"]);
                    if (!Convert.IsDBNull(zRdr["UNITNAME"])) _UNITNAME = zRdr["UNITNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["PRICE"])) _UNITPRICE = Convert.ToDouble(zRdr["PRICE"]);
                    if (!Convert.IsDBNull(zRdr["DISCOUNT"])) _DISCOUNT = Convert.ToDouble(zRdr["DISCOUNT"]);
                    if (!Convert.IsDBNull(zRdr["ISVAT"])) _ISVAT = zRdr["ISVAT"].ToString();
                    if (!Convert.IsDBNull(zRdr["STOCKQTY"])) _STOCKQTY = Convert.ToDouble(zRdr["STOCKQTY"]);
                    if (!Convert.IsDBNull(zRdr["ISDISCOUNT"])) _ISDISCOUNT = zRdr["ISDISCOUNT"].ToString();
                    if (!Convert.IsDBNull(zRdr["ISEDIT"])) _ISEDIT = zRdr["ISEDIT"].ToString();
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
            return ret;
        }

        public bool GetProductPromotion(double warehouse, double product, OracleTransaction zTrans)
        {
            return doGetdata(warehouse, product, "", zTrans);
        }

        public bool GetProductPromotion(double warehouse, string barcode, OracleTransaction zTrans)
        {
            return doGetdata(warehouse, 0, barcode, zTrans);
        }

        public double GetProductStockQty(double productBarcode, double warehouse, double zone)
        {
            double quantity = 0;
            string sql = "SELECT SUM(PS.QTY/P.MULTIPLY) ";
            sql += "FROM PRODUCTSTOCK PS INNER JOIN PRODUCTBARCODE P ON P.PRODUCTMASTER = PS.PRODUCT ";
            sql += "WHERE P.LOID = " + productBarcode.ToString() + " AND PS.ZONE = " + zone.ToString() + " AND PS.WAREHOUSE =  " + warehouse.ToString() + " ";
            try
            {
                quantity = Convert.ToDouble(OracleDB.ExecSingleCmd(sql));
            }
            catch (Exception ex)
            {
                quantity = 0;
            }
            return quantity;
        }

        public double GetMemberDiscount(double memberType, double totalPrice, OracleTransaction trans)
        {
            double discount = 0;
            string sql = "SELECT DISCOUNT ";
            sql += "FROM DISCOUNTSTEP ";
            sql += "WHERE MEMBERTYPE = " + memberType.ToString() + " AND NVL(LOWERPRICE,0) = (";
            sql += "SELECT NVL(MAX(LOWERPRICE),0) ";
            sql += "FROM DISCOUNTSTEP ";
            sql += "WHERE MEMBERTYPE = " + memberType.ToString() + " AND NVL(LOWERPRICE,0) <= " + totalPrice.ToString() + ") ";
            DataTable dt = OracleDB.ExecListCmd(sql, trans);
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["DISCOUNT"])) discount = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
            }
            return discount;
        }

        public double GetPromotionDiscount(double warehouse, double totalPrice, OracleTransaction trans)
        {
            double discount = 0;
            string sql = "SELECT DISCOUNT ";
            sql += "FROM PROMOTION ";
            sql += "WHERE TO_CHAR(EPDATE, 'YYYYMMDD') >= TO_CHAR(SYSDATE,'YYYYMMDD') ";
            sql += "AND TO_CHAR(EFDATE, 'YYYYMMDD') <= TO_CHAR(SYSDATE,'YYYYMMDD') ";
            sql += "AND WAREHOUSE = " + warehouse.ToString() + " AND NVL(LOWERPRICE,0) = (";
            sql += "SELECT NVL(MAX(LOWERPRICE),0) ";
            sql += "FROM PROMOTION ";
            sql += "WHERE TO_CHAR(EPDATE, 'YYYYMMDD') >= TO_CHAR(SYSDATE,'YYYYMMDD') ";
            sql += "AND TO_CHAR(EFDATE, 'YYYYMMDD') <= TO_CHAR(SYSDATE,'YYYYMMDD') ";
            sql += "AND WAREHOUSE = " + warehouse.ToString() + " AND NVL(LOWERPRICE,0) <= " + totalPrice.ToString() + ")";
            DataTable dt = OracleDB.ExecListCmd(sql, trans);
            if (dt.Rows.Count > 0)
            {
                if (!Convert.IsDBNull(dt.Rows[0]["DISCOUNT"])) discount = Convert.ToDouble(dt.Rows[0]["DISCOUNT"]);
            }
            return discount;
        }

    }
}
