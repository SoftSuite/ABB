using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data;
using ABB.Data.Sales;
using ABB.DAL;

/// <summary>
/// Create by: Pom
/// Create Date: 13 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

namespace ABB.Flow.Sales
{
    public class CustomerFlow
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public static DataTable GetCustomer(string LOID)
        {
            string sql = "SELECT * FROM CUSTOMER WHERE LOID = '" + LOID + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static string GenerateCusCode()
        {
            string sql = "SELECT MAX(to_number(CODE)) FROM CUSTOMER";
            object cusCode = OracleDB.ExecSingleCmd(sql);
            if (Convert.IsDBNull(cusCode) == true)
                return Convert.ToString(1);
            else
                return Convert.ToString(Convert.ToInt32(cusCode) + 1);
        }

        public bool InsertData(string UserID, CustomerData cusData)
        {
            Boolean ret = true;
            CustomerDAL oDAL = new CustomerDAL();

            SetDataToDAL(oDAL, cusData);
            
            ret = oDAL.InsertCurrentData(UserID, null);

            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;
        }

        public bool UpdateData(string UserID, CustomerData cusData)
        {
            Boolean ret = true;
            CustomerDAL oDAL = new CustomerDAL();
            //double LOID = 0;

            //LOID = GetLOID(cusData.CODE);
            oDAL.GetDataByLOID(cusData.LOID, null);
            SetDataToDAL(oDAL, cusData);

            ret = oDAL.UpdateCurrentData(UserID, null);

            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;
        }

        public double GetLOID(string CusCode)
        {
            string sql = "SELECT LOID FROM CUSTOMER WHERE CODE = '" + CusCode + "'";
            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public double GetLOIDbyIDENTITY(string Identity)
        {
            string sql = "SELECT LOID FROM CUSTOMER WHERE IDENTITY = '" + Identity + "'";
            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public CustomerData GetData(double loid)
        {
            CustomerData data = new CustomerData();
            CustomerDAL _DAL = new CustomerDAL();
            _DAL.GetDataByLOID(loid, null);
            data.ACTIVE = _DAL.ACTIVE;
            data.BILLADDRESS = _DAL.BILLADDRESS;
            data.BILLAMPHUR = _DAL.BILLAMPHUR;
            data.BILLEMAIL = _DAL.BILLEMAIL;
            data.BILLFAX = _DAL.BILLFAX;
            data.BILLPROVINCE = _DAL.BILLPROVINCE;
            data.BILLROAD = _DAL.BILLROAD;
            data.BILLTAMBOL = _DAL.BILLTAMBOL;
            data.BILLTEL = _DAL.BILLTEL;
            data.BILLZIPCODE = _DAL.BILLZIPCODE;
            data.CADDRESS = _DAL.CADDRESS;
            data.CAMPHUR = _DAL.CAMPHUR;
            data.CEMAIL = _DAL.CEMAIL;
            data.CFAX = _DAL.CFAX;
            data.CLASTNAME = _DAL.CLASTNAME;
            data.CMOBILE = _DAL.CMOBILE;
            data.CNAME = _DAL.CNAME;
            data.CODE = _DAL.CODE;
            data.CPROVINCE = _DAL.CPROVINCE;
            data.CREDITAMOUNT = _DAL.CREDITAMOUNT;
            data.CREDITDAY = _DAL.CREDITDAY;
            data.CROAD = _DAL.CROAD;
            data.CTAMBOL = _DAL.CTAMBOL;
            data.CTEL = _DAL.CTEL;
            data.CTITLE = _DAL.CTITLE;
            data.CUSTOMERTYPE = _DAL.CUSTOMERTYPE;
            data.CZIPCODE = _DAL.CZIPCODE;
            data.DELIVERTYPE = _DAL.DELIVERTYPE;
            data.EFDATE = _DAL.EFDATE;
            data.EPDATE = _DAL.EPDATE;
            data.IDENTITY = _DAL.IDENTITY;
            data.LASTNAME = _DAL.LASTNAME;
            data.LOID = _DAL.LOID;
            data.MEMBERTYPE = _DAL.MEMBERTYPE;
            data.NAME = _DAL.NAME;
            data.PAYMENT = _DAL.PAYMENT;
            data.REMARK = _DAL.REMARK;
            data.SENDADDRESS = _DAL.SENDADDRESS;
            data.SENDAMPHUR = _DAL.SENDAMPHUR;
            data.SENDEMAIL = _DAL.SENDEMAIL;
            data.SENDFAX = _DAL.SENDFAX;
            data.SENDPLACE = _DAL.SENDPLACE;
            data.SENDPROVINCE = _DAL.SENDPROVINCE;
            data.SENDROAD = _DAL.SENDROAD;
            data.SENDTAMBOL = _DAL.SENDTAMBOL;
            data.SENDTEL = _DAL.SENDTEL;
            data.SENDZIPCODE = _DAL.SENDZIPCODE;
            data.TITLE = _DAL.TITLE;
            return data;
        }

        private void SetDataToDAL(CustomerDAL oDAL, CustomerData cusData)
        {
            oDAL.ACTIVE = "1";

            //-------------------- ชื่อบริษัท/ลูกค้า ---------------------------------------
            oDAL.CODE = cusData.CODE;
            oDAL.CUSTOMERTYPE = cusData.CUSTOMERTYPE;
            oDAL.IDENTITY = cusData.IDENTITY;
            oDAL.TITLE = cusData.TITLE;
            oDAL.NAME = cusData.NAME;
            oDAL.LASTNAME = cusData.LASTNAME;
            oDAL.MEMBERTYPE = cusData.MEMBERTYPE;
            oDAL.EFDATE = cusData.EFDATE;
            oDAL.EPDATE = cusData.EPDATE;

            //-------------------- เงื่อนไขการชำระเงิน ---------------------------------------
            oDAL.PAYMENT = cusData.PAYMENT;
            oDAL.CREDITDAY = cusData.CREDITDAY;
            oDAL.CREDITAMOUNT = cusData.CREDITAMOUNT;

            //-------------------- ที่อยู่บริษัท/ลูกค้า/สมาชิก ---------------------------------------
            oDAL.BILLADDRESS = cusData.BILLADDRESS;
            oDAL.BILLROAD = cusData.BILLROAD;
            oDAL.BILLPROVINCE = cusData.BILLPROVINCE;
            oDAL.BILLAMPHUR = cusData.BILLAMPHUR;
            oDAL.BILLTAMBOL = cusData.BILLTAMBOL;
            oDAL.BILLZIPCODE = cusData.BILLZIPCODE;
            oDAL.BILLTEL = cusData.BILLTEL;
            oDAL.BILLFAX = cusData.BILLFAX;
            oDAL.BILLEMAIL = cusData.BILLEMAIL;

            //-------------------- ชื่อผู้ติดต่อ ---------------------------------------
            oDAL.CTITLE = cusData.CTITLE;
            oDAL.CNAME = cusData.CNAME;
            oDAL.CLASTNAME = cusData.CLASTNAME;
            oDAL.CTEL = cusData.CTEL;
            oDAL.CMOBILE = cusData.CMOBILE;
            oDAL.CEMAIL = cusData.CEMAIL;
            oDAL.CADDRESS = cusData.CADDRESS;
            oDAL.CROAD = cusData.CROAD;
            oDAL.CPROVINCE = cusData.CPROVINCE;
            oDAL.CAMPHUR = cusData.CAMPHUR;
            oDAL.CTAMBOL = cusData.CTAMBOL;
            oDAL.CZIPCODE = cusData.CZIPCODE;

            //-------------------- สถานที่ส่งสินค้า ---------------------------------------
            oDAL.SENDPLACE = cusData.SENDPLACE;
            oDAL.DELIVERTYPE = cusData.DELIVERTYPE;
            oDAL.SENDADDRESS = cusData.SENDADDRESS;
            oDAL.SENDROAD = cusData.SENDROAD;
            oDAL.SENDPROVINCE = cusData.SENDPROVINCE;
            oDAL.SENDAMPHUR = cusData.SENDAMPHUR;
            oDAL.SENDTAMBOL = cusData.SENDTAMBOL;
            oDAL.SENDZIPCODE = cusData.SENDZIPCODE;
            oDAL.SENDTEL = cusData.SENDTEL;
            oDAL.SENDFAX = cusData.SENDFAX;
            oDAL.SENDEMAIL = cusData.SENDEMAIL;

            //-------------------- หมายเหตุ ---------------------------------------
            oDAL.REMARK = cusData.REMARK;
        }
    }
}
