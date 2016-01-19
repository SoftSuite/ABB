using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Purchase;
using System.Data.OracleClient;
using ABB.DAL;

/// <summary>
/// Create by: Ta
/// Create Date: 9 Jan 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 
namespace ABB.Flow.Purchase
{
    public class SupplierSearchFlow
    {
        private string _error = "";
        public string ErrorMessage { get { return _error; } }

        public ArrayList GetSearchSupplier(SupplierSearchData supData)
        {
            ArrayList arrResult = new ArrayList();
            string whStr = "";
            string sql = "";

            whStr += (supData.CODE == "" ? "" : " S.CODE = '" + supData.CODE + "'");
            whStr += (supData.SUPPLIERNAME == "" ? "" : (whStr == "" ? "" : " AND ") + " UPPER(S.SUPPLIERNAME) LIKE UPPER('%" + supData.SUPPLIERNAME + "%')");

            sql = "SELECT S.LOID, S.CODE, S.SUPPLIERNAME, S.TAXID, T.NAME || S.CNAME || ' ' || S.CLASTNAME AS CONNAME, S.TEL";
            sql += " FROM SUPPLIER S LEFT JOIN TITLE T ON S.CTITLE = T.LOID";
            sql += (whStr == "" ? "" : " WHERE" + whStr);
            sql += " ORDER BY S.SUPPLIERNAME";
            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(sql);
                arrResult.Clear();
                int i = 1;

                while (zRd.Read())
                {
                    SupplierResultData irData = new SupplierResultData();
                    irData.ORDERNO = i;
                    irData.LOID = zRd["LOID"].ToString();
                    irData.CODE = zRd["CODE"].ToString();
                    irData.SUPPLIERNAME = zRd["SUPPLIERNAME"].ToString();
                    irData.TAXID = zRd["TAXID"].ToString();
                    irData.CNAME = zRd["CONNAME"].ToString();
                    irData.TEL = zRd["TEL"].ToString();

                    arrResult.Add(irData);
                    i += 1;
                }
            }
            catch (Exception ex)
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
                    SupplierDAL oDAL = new SupplierDAL();
                    oDAL.GetDataByLOID(LOID, objDB.zTrans);
                    oDAL.DeleteCurrentData(objDB.zTrans);
                }
                objDB.zTrans.Commit();
                ret = true;
            }
            catch (Exception ex)
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
