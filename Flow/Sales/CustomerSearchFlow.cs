using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data;
using ABB.Data.Sales;
using System.Data.OracleClient;
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
    public class CustomerSearchFlow
    {
        private string _error = "";
        public string ErrorMessage { get { return _error; } }

        public ArrayList GetSearchCustomer(CustomerSearchData cusData)
        { 
            ArrayList arrResult = new ArrayList();
            string whStr = "";
            string sql = "";

            whStr += (cusData.CUSCODE == "" ? "" : " C.CODE = '" + cusData.CUSCODE + "'");
            whStr += (cusData.CUSTYPE == "" ? "" : (whStr == "" ? "" : " AND ") + " C.CUSTOMERTYPE = '" + cusData.CUSTYPE + "'");
            whStr += (cusData.CUSNAME == "" ? "" : (whStr == "" ? "" : " AND ") + " UPPER(C.NAME) LIKE UPPER('%" + cusData.CUSNAME + "%')");
            whStr += (cusData.LASTNAME == "" ? "" : (whStr == "" ? "" : " AND " ) + " UPPER(C.LASTNAME) LIKE UPPER('%" + cusData.LASTNAME + "%')");
            whStr += (cusData.MEMBERTYPE == 0 ? "" : (whStr == "" ? "" : " AND ") + " C.MEMBERTYPE = " + cusData.MEMBERTYPE.ToString() + "");
            whStr += (cusData.PROVINCE == 0 ? "" : (whStr == "" ? "" : " AND ") + " C.BILLPROVINCE = " + cusData.PROVINCE.ToString() + "");

            sql = "SELECT C.LOID, C.CODE, C.NAME || ' ' || C.LASTNAME AS CUSNAME, M.NAME AS MEMBERTYPE, C.CUSTOMERTYPE, C.EPDATE, C.PAYMENT";
            sql += " FROM CUSTOMER C LEFT JOIN MEMBERTYPE M ON C.MEMBERTYPE = M.LOID";
            sql += (whStr == "" ? "" : " WHERE" + whStr);
            sql += " ORDER BY C.CODE";
            
            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(sql);
                arrResult.Clear();
                int i = 1;

                while (zRd.Read())
                {
                    CustomerResultData irData = new CustomerResultData();
                    irData.ORDERNO = i;
                    irData.LOID = zRd["LOID"].ToString();
                    irData.CODE = zRd["CODE"].ToString();
                    irData.CUSNAME = zRd["CUSNAME"].ToString();
                    irData.MEMBERTYPE = zRd["MEMBERTYPE"].ToString();

                    if (zRd["CUSTOMERTYPE"].ToString() == Constz.CustomerType.Personal.Code)
                        irData.CUSTOMERTYPE = Constz.CustomerType.Personal.Name;
                    else if (zRd["CUSTOMERTYPE"].ToString() == Constz.CustomerType.Company.Code)
                        irData.CUSTOMERTYPE = Constz.CustomerType.Company.Name;
                    else if (zRd["CUSTOMERTYPE"].ToString() == Constz.CustomerType.Government.Code)
                        irData.CUSTOMERTYPE = Constz.CustomerType.Government.Name;

                    irData.EPDATE = Convert.ToDateTime(zRd["EPDATE"]).ToString(Constz.DateFormat);

                    if (zRd["PAYMENT"].ToString() == Constz.Payment.Cash.Code)
                        irData.PAYMENT = Constz.Payment.Cash.Name;
                    else if (zRd["PAYMENT"].ToString() == Constz.Payment.CreditCard.Code)
                        irData.PAYMENT = Constz.Payment.CreditCard.Name;
                    else if (zRd["PAYMENT"].ToString() == Constz.Payment.Credit.Code)
                        irData.PAYMENT = Constz.Payment.Credit.Name;

                    arrResult.Add(irData);
                    i += 1;
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return arrResult;
        }

        public bool DeleteData(ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj objDB = new OracleDBObj();

            objDB.CreateConnection();
            objDB.CreateTransaction();

            try
            {
                foreach (double LOID in arrLOID)
                {
                    CustomerDAL oDAL = new CustomerDAL();
                    oDAL.GetDataByLOID(LOID, objDB.zTrans);
                    oDAL.DeleteCurrentData(objDB.zTrans);
                }
                objDB.zTrans.Commit();
                ret = true;
            }
            catch(Exception ex)
            {
                objDB.zTrans.Rollback();
                _error = ex.Message;
                ret = false;
            }

            objDB.CloseConnection();
            return ret;
        }
    }
}
