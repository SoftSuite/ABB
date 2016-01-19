using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient; 
using ABB.Data;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.DAL.Sales
{
    public class StockInReturnDAL
    {
        private string V_PRODUCTSTOCKINRETURN
        {
            get
            {
                string view = "(SELECT 0 ORDERNO, RQI.LOID REFLOID, RQI.REQUISITION, RQ.CUSTOMER, RQI.PRODUCT, RQI.UNIT, C.CODE CUSTOMERCODE, ";
                view += "TITLE.NAME || C.NAME || ' ' || C.LASTNAME CUSTOMERNAME, RQ.CODE, P.BARCODE, P.NAME PRODUCTNAME, RQI.QTY INVQTY, RQI.QTY, RQI.PRICE, ";
                view += "UNIT.NAME UNITNAME, (RQI.PRICE-RQI.DISCOUNT)*RQI.QTY TOTAL,RQI.DISCOUNT TOTALDIS, RQ.REQDATE, RQ.WAREHOUSE ";
                view += "FROM REQUISITIONITEM RQI INNER JOIN REQUISITION RQ ON RQ.LOID = RQI.REQUISITION AND RQ.REQUISITIONTYPE = " + Constz.Requisition.RequisitionType.REQ13.ToString() + " ";
                view += "INNER JOIN CUSTOMER C ON C.LOID = RQ.CUSTOMER ";
                view += "INNER JOIN PRODUCT P ON P.LOID = RQI.PRODUCT ";
                view += "INNER JOIN UNIT ON UNIT.LOID = RQI.UNIT ";
                view += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
                view += "WHERE RQ.LOID NOT IN (SELECT REFLOID FROM STOCKIN WHERE STATUS <> '" + Constz.Requisition.Status.Void.Code + "' ";
                view += "AND REFTABLE = 'REQUISITION' AND DOCTYPE =" + Constz.DocType.RetShop.LOID.ToString() + ") ) ";
                return view;
            }
        }

        public DataTable GetStockInList(StockInReturnSearchData data)
        {
            string where = "S.DOCTYPE = " + Constz.DocType.RetShop.LOID.ToString() + " ";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(S.CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(S.RECEIVEDATE, 'DD/MM/YYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(S.RECEIVEDATE, 'DD/MM/YYYY')  <= " + OracleDB.QRDate(data.DATETO) + " ";

            string sql = "SELECT 0 ORDERNO, S.CODE, S.RECEIVEDATE, R.CODE INVOICECODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME CUSTOMERNAME, S.TOTAL, S.LOID, ";
            sql += "CASE S.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' END STATUSNAME ";
            sql += "FROM STOCKIN S LEFT JOIN REQUISITION R ON R.LOID = S.REFLOID AND S.REFTABLE = 'REQUISITION' ";
            sql += "LEFT JOIN CUSTOMER C ON C.LOID = R.CUSTOMER ";
            sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY S.CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockInProductList(double stockIn)
        {
            string sql = "SELECT 0 ORDERNO, STI.REFLOID, STI.PRODUCT, STI.UNIT, P.BARCODE, P.NAME PRODUCTNAME, RQI.QTY INVQTY, STI.QTY, STI.PRICE, ";
            sql += "UNIT.NAME UNITNAME, STI.PRICE*STI.QTY TOTAL ";
            sql += "FROM STOCKINITEM STI INNER JOIN PRODUCT P ON P.LOID = STI.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = STI.UNIT ";
            sql += "INNER JOIN REQUISITIONITEM RQI ON STI.REFLOID = RQI.LOID AND STI.REFTABLE = 'REQUISITIONITEM' ";
            sql += "WHERE STI.STOCKIN = " + stockIn.ToString() + " ";
            sql += "ORDER BY P.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetInvoiceProductList(double requisition)
        {
            string sql = "SELECT 0 ORDERNO, V.REFLOID, V.PRODUCT, V.UNIT, V.BARCODE, V.PRODUCTNAME, V.INVQTY,V.TOTALDIS, V.QTY, V.PRICE, V.UNITNAME, V.TOTAL ";
            sql += "FROM " + V_PRODUCTSTOCKINRETURN + " V ";
            sql += "WHERE REQUISITION = " + requisition.ToString() + " ";
            sql += "ORDER BY V.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetDiscount(string customer)
        {
            string sql = "SELECT DISTINCT DISCOUNT FROM DISCOUNTSTEP DS ";
            sql += "INNER JOIN CUSTOMER C ON DS.MEMBERTYPE = C.MEMBERTYPE ";
            sql += "WHERE C.CODE = '" + customer.ToString() + "' ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetInvoiceList(ABB.Data.Search.InvoiceForReturnSearchData data)
        {
            string where = "";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.CODE) >= '" + data.CODEFROM.Trim().ToUpper() + "' ";
            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.CODE) <= '" + data.CODETO.Trim().ToUpper() + "' ";
            if (data.CUSTOMERCODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(V.CUSTOMERCODE) >= '" + data.CUSTOMERCODE.Trim().ToUpper() + "' ";
            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "V.REQDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "V.REQDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PRODUCT != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCT = '" + data.PRODUCT.ToString() + "' ";

            string sql = sql_select + (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY V.CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        #region Public Method

        /// <summary>
        /// Get Data From DB to Object by LOID
        /// </summary>
        /// <param name="zLOID"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public bool GetDataByLOID(double zLOID, OracleTransaction zTrans)
        {
            return doGetdata(" REQUISITION = " + zLOID.ToString() + " ", zTrans);
        }

        public bool GetDataByCODE(string Code, OracleTransaction zTrans)
        {
            return doGetdata(" UPPER(CODE) = '" + Code.Trim().ToUpper() + "' ", zTrans);
        }

        #endregion

        #region Private Variable
        string _error = "";
        bool _OnDB = false;
        double _ORDERNO = 0;
        double _REQUISITION = 0;
        string _INVOICECODE = "";
        DateTime _INVOICEDATE = new DateTime(1, 1, 1);
        string _CUSTOMERCODE = "";
        string _CUSTOMERNAME = "";
        double _WAREHOUSE = 0;
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
        public double ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }
        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }
        public string INVOICECODE
        {
            get { return _INVOICECODE; }
            set { _INVOICECODE = value; }
        }
        public DateTime INVOICEDATE
        {
            get { return _INVOICEDATE; }
            set { _INVOICEDATE = value; }
        }
        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }
        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }
        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
        #endregion

        #region Query String
        private string sql_select
        {
            get
            {
                string sqlz = " SELECT DISTINCT 0 ORDERNO, V.REQUISITION, V.CODE INVOICECODE, V.REQDATE INVOICEDATE, V.CUSTOMERCODE, V.CUSTOMERNAME, ";
                sqlz += "V.WAREHOUSE ";
                sqlz += "FROM " + V_PRODUCTSTOCKINRETURN + " V ";
                return sqlz;
            }
        }
        #endregion

        #region Internal Method
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
                        if (!Convert.IsDBNull(zRdr["ORDERNO"])) _ORDERNO = Convert.ToDouble(zRdr["ORDERNO"]);
                        if (!Convert.IsDBNull(zRdr["REQUISITION"])) _REQUISITION = Convert.ToDouble(zRdr["REQUISITION"]);
                        if (!Convert.IsDBNull(zRdr["INVOICECODE"])) _INVOICECODE = zRdr["INVOICECODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["INVOICEDATE"])) _INVOICEDATE = OracleDB.DBDate(zRdr["INVOICEDATE"]);
                        if (!Convert.IsDBNull(zRdr["CUSTOMERCODE"])) _CUSTOMERCODE = zRdr["CUSTOMERCODE"].ToString();
                        if (!Convert.IsDBNull(zRdr["CUSTOMERNAME"])) _CUSTOMERNAME = zRdr["CUSTOMERNAME"].ToString();
                        if (!Convert.IsDBNull(zRdr["WAREHOUSE"])) _WAREHOUSE = Convert.ToDouble(zRdr["WAREHOUSE"]);
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


    }
}
