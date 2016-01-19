using System;
using System.Collections.Generic;
using System.Text;
using ABB.DAL;
using System.Collections;
using ABB.Data.Inventory.FG.Master;
using System.Data.OracleClient;

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
    public class ControlStockSearchFlow
    {
        private string _error = "";
        public string ErrorMessage { get { return _error; } }

        public static string GetWarehouseName(double whLOID)
        {
            string sql = "SELECT NAME FROM WAREHOUSE WHERE LOID = " + whLOID;
            return OracleDB.ExecSingleCmd(sql).ToString();
        }

        public ArrayList GetSearchProduct(ControlStockSearchData data)
        {
            ArrayList arrResult = new ArrayList();
            string whStr = "";
            string sql = "";

            if (data.WAREHOUSE.Trim() != "")
                whStr += (data.WAREHOUSE == "" ? "" : " PM.WAREHOUSE = " + data.WAREHOUSE + "");
            if (data.BARCODE.Trim() != "")
                whStr += (data.BARCODE == "" ? "" : (whStr == "" ? "" : " AND ") + " PD.BARCODE >= '" + data.BARCODE + "'");
            if (data.BARCODETO.Trim() != "")
                whStr += (data.BARCODETO == "" ? "" : (whStr == "" ? "" : " AND ") + " PD.BARCODE <= '" + data.BARCODETO + "'");
            if (data.PRODUCTNAME.Trim() != "")
                whStr += (data.PRODUCTNAME == "" ? "" : (whStr == "" ? "" : " AND ") + " UPPER(PD.NAME) LIKE UPPER('%" + data.PRODUCTNAME + "%')");
            
            sql = "SELECT PD.BARCODE, PD.NAME, PM.LOID AS PDMINMAXLOID, PM.STANDARD, PM.MINIMUM, PM.MAXIMUM";
            sql += " FROM PRODUCTMINMAX PM INNER JOIN PRODUCT PD ON PM.PRODUCT = PD.PRODUCTMASTER AND PD.ISDEFAULT = 'Y' ";
            sql += (whStr == "" ? "" : " WHERE" + whStr);
            sql += " ORDER BY PD.BARCODE"; 

            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(sql);
                arrResult.Clear();
                int i = 1;

                while (zRd.Read())
                {
                    ControlStockResultData irData = new ControlStockResultData();
                    irData.ORDERNO = i;
                    irData.PDMINMAXLOID = zRd["PDMINMAXLOID"].ToString();
                    irData.BARCODE = zRd["BARCODE"].ToString();
                    irData.PRODUCTNAME = zRd["NAME"].ToString();
                    irData.STANDARD = Convert.ToDouble(zRd["STANDARD"]).ToString("#,##0.00");
                    irData.MINIMUM = Convert.ToDouble(zRd["MINIMUM"]).ToString("#,##0.00");
                    irData.MAXIMUM = Convert.ToDouble(zRd["MAXIMUM"]).ToString("#,##0.00");
                   
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
                    ProductMinMaxDAL oDAL = new ProductMinMaxDAL();
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
