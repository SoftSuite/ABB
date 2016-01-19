using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Sales
{
    public class SaleCustomerDAL
    {
        private double _CUSTOMER = 0;
        private string _CODE = "";
        private string _CUSTOMERNAME = "";
        private double _MEMBERTYPE = 0;
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private double _CREDITDAY = 0;
        private string _PAYMENT = "";
        private DateTime _EFDATE = new DateTime(1, 1, 1);
        private DateTime _EPDATE = new DateTime(1, 1, 1);
        private string _error = "";

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public double MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }

        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }

        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }

        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }

        public double CREDITDAY
        {
            get { return _CREDITDAY; }
            set { _CREDITDAY = value; }
        }

        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }

        public DateTime EFDATE
        {
            get { return _EFDATE; }
            set { _EFDATE = value; }
        }

        public DateTime EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        private bool doGetdata(double customer, string code, OracleTransaction zTrans)
        {
            bool ret = true;
            string where = "";
            if (customer != 0 || (customer ==0 && code.Trim() == "")) where += (where == "" ? "" : "AND ") + "C.LOID = " + customer.ToString() + " ";
            if (code.Trim() != "") where += (where == "" ? "" : "AND") + " UPPER(C.CODE) = '" + code.Trim().ToUpper() + "' ";
            string sql = "SELECT C.LOID, C.CODE, TITLE.NAME || C.NAME || ' ' || C.LASTNAME AS CUSTOMERNAME, C.MEMBERTYPE, ";
            sql += "C.CTITLE, C.CNAME, C.CLASTNAME, C.CADDRESS || ' ' || CASE WHEN  NVL(C.CROAD,'X') = 'X' THEN '' ELSE  '¶.' || C.CROAD || ' ' END || ";
            sql += "CASE WHEN C.CTAMBOL IS NULL THEN '' ELSE 'µ.' || T.NAME || ' ' END || ";
            sql += "CASE WHEN C.CAMPHUR IS NULL THEN '' ELSE 'Í.' || A.NAME || ' ' END || ";
            sql += "CASE WHEN C.CPROVINCE IS NULL THEN '' ELSE '¨.' || P.NAME || ' ' END || C.CZIPCODE CADDRESS, ";
            sql += "C.CTEL, C.CFAX, C.EFDATE, C.EPDATE, C.CREDITDAY, C.PAYMENT ";
            sql += "FROM CUSTOMER C INNER JOIN MEMBERTYPE MT ON MT.LOID = C.MEMBERTYPE ";
            sql += "LEFT JOIN TAMBOL T ON T.LOID = C.CTAMBOL ";
            sql += "LEFT JOIN AMPHUR A ON A.LOID = C.CAMPHUR ";
            sql += "LEFT JOIN PROVINCE P ON P.LOID = C.CPROVINCE ";
            sql += "LEFT JOIN TITLE ON TITLE.LOID = C.TITLE ";
            sql += (where == "" ? "" : "WHERE ") + where;

            OracleDataReader zRdr = null;
            try
            {
                zRdr = OracleDB.ExecQueryCmd(sql, zTrans);
                if (zRdr.Read())
                {
                    if (!Convert.IsDBNull(zRdr["CODE"])) _CODE = zRdr["CODE"].ToString();
                    if (!Convert.IsDBNull(zRdr["LOID"])) _CUSTOMER = Convert.ToDouble(zRdr["LOID"]);
                    if (!Convert.IsDBNull(zRdr["CUSTOMERNAME"])) _CUSTOMERNAME = zRdr["CUSTOMERNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["MEMBERTYPE"])) _MEMBERTYPE = Convert.ToDouble(zRdr["MEMBERTYPE"]);
                    if (!Convert.IsDBNull(zRdr["CTITLE"])) _CTITLE = Convert.ToDouble(zRdr["CTITLE"]);
                    if (!Convert.IsDBNull(zRdr["CNAME"])) _CNAME = zRdr["CNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["CLASTNAME"])) _CLASTNAME = zRdr["CLASTNAME"].ToString();
                    if (!Convert.IsDBNull(zRdr["CADDRESS"])) _CADDRESS = zRdr["CADDRESS"].ToString();
                    if (!Convert.IsDBNull(zRdr["CTEL"])) _CTEL = zRdr["CTEL"].ToString();
                    if (!Convert.IsDBNull(zRdr["CFAX"])) _CFAX = zRdr["CFAX"].ToString();
                    if (!Convert.IsDBNull(zRdr["CREDITDAY"])) _CREDITDAY = Convert.ToDouble(zRdr["CREDITDAY"]);
                    if (!Convert.IsDBNull(zRdr["PAYMENT"])) _PAYMENT = zRdr["PAYMENT"].ToString();
                    if (!Convert.IsDBNull(zRdr["EFDATE"])) _EFDATE = Convert.ToDateTime(zRdr["EFDATE"]);
                    if (!Convert.IsDBNull(zRdr["EPDATE"])) _EPDATE = Convert.ToDateTime(zRdr["EPDATE"]);
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

        public bool doGetdata(string customerCode, OracleTransaction zTrans)
        {
            if (customerCode == "")
                return true;
            else
                return doGetdata(0, customerCode, zTrans);
        }

        public bool doGetdata(double customer, OracleTransaction zTrans)
        {
            return doGetdata(customer, "", zTrans);
        }

    }
}
