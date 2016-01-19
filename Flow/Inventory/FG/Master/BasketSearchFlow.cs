using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Inventory.FG.Master;
using System.Data;
using System.Data.OracleClient;
using ABB.DAL;

/// <summary>
/// Create by: Pom
/// Create Date: 18 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>


namespace ABB.Flow.Inventory.FG.Master
{
    public class BasketSearchFlow
    {
        private string _error = "";
        public string ErrorMessage { get { return _error; } }

        public ArrayList GetSearchBasket(BasketSearchData data)
        {
            ArrayList arrResult = new ArrayList();
            string whStr = "";
            string sql = "";

            whStr += (data.BARCODE == "" ? "" : (whStr == "" ? "" : " AND ") + " PD.BARCODE = '" + data.BARCODE + "'");
            whStr += (data.PRODUCTNAME == "" ? "" : (whStr == "" ? "" : " AND ") + " PD.NAME LIKE '%" + data.PRODUCTNAME + "%'");

            sql = "SELECT DISTINCT PD.LOID, PD.BARCODE, PD.NAME, PD.COST, PD.PRICE";
            sql += " FROM PRODUCT PD INNER JOIN PACKAGE PK ON PK.MAINPRODUCT = PD.LOID";
            sql += (whStr == "" ? "" : " WHERE" + whStr);
            sql += " ORDER BY PD.BARCODE";

            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(sql);
                arrResult.Clear();
                int i = 1;

                while (zRd.Read())
                {
                    BasketSearchResultData irData = new BasketSearchResultData();
                    irData.ORDERNO = i;
                    irData.LOID = zRd["LOID"].ToString();
                    irData.BARCODE = zRd["BARCODE"].ToString();
                    irData.PRODUCTNAME = zRd["NAME"].ToString();
                    irData.COST = Convert.ToDouble(zRd["COST"]).ToString("#,##0.00");
                    irData.PRICE = Convert.ToDouble(zRd["PRICE"]).ToString("#,##0.00");

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

                    string sqlPackage = "DELETE FROM PACKAGE WHERE MAINPRODUCT = " + LOID.ToString();
                    ret = (OracleDB.ExecNonQueryCmd(sqlPackage, objDB.zTrans) > 0);
                    if (!ret) _error = OracleDB.Err_NoDelete;
                    //else _OnDB = false;

                    string sqlProduct = "DELETE FROM PRODUCT WHERE LOID = " + LOID.ToString();
                    ret = (OracleDB.ExecNonQueryCmd(sqlProduct, objDB.zTrans) > 0);
                    if (!ret) _error = OracleDB.Err_NoDelete;
                   // else _OnDB = false;
                }
                objDB.zTrans.Commit();
                ret = true;
            }
            catch (OracleException ex)
            {
                ret = false;
                _error = OracleDB.GetOracleExceptionText(ex);
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
