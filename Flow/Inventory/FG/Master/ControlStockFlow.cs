using System;
using System.Collections.Generic;
using System.Text;
using ABB.DAL;
using System.Data;
using ABB.Data.Inventory.FG.Master;

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
    public class ControlStockFlow
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public static string GetWarehouseName(double whLOID)
        {
            string sql = "SELECT NAME FROM WAREHOUSE WHERE LOID = " + whLOID;
            return OracleDB.ExecSingleCmd(sql).ToString();
        }

        public static DataTable GetProduct(string barcode, string warehouse)
        {
            string type = "";
            if (warehouse == "1")
                type = "FG";
            else if (warehouse == "2")
                type = "WH";


            string sql = "";
            sql = "SELECT PD.NAME, U.NAME UNIT, UM.NAME UNITMASTER FROM PRODUCT PD INNER JOIN UNIT U ON PD.UNIT = U.LOID INNER JOIN UNIT UM ON PD.UNITMASTER = UM.LOID";
            sql += " WHERE BARCODE ='" + barcode + "' AND PD.TYPE = '" +type+ "'";

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static double GetProductLOID(string barcode)
        {
            string sql = "";
            sql = "SELECT PRODUCTMASTER FROM PRODUCT";
            sql += " WHERE BARCODE ='" + barcode + "'";

            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public static bool CheckProductExist(string barcode)
        {
            string sql = "";
            sql = "SELECT COUNT(*) FROM PRODUCT";
            sql += " WHERE BARCODE ='" + barcode + "'";

            object count = OracleDB.ExecSingleCmd(sql);
            if (Convert.ToInt32(count) > 0)
                return true;
            else
                return false;
        }

        public static DataTable GetProductMinMax(string barcode, string warehouse)
        {
            string type = "";
            if (warehouse == "1")
                type = "FG";
            else if (warehouse == "2")
                type = "WH";


            string sql = "";
            sql = "SELECT PM.LOID AS PDMINMAXLOID, PM.STANDARD, PM.MINIMUM, PM.MAXIMUM";
            sql += " FROM PRODUCTMINMAX PM INNER JOIN PRODUCT PD ON PM.PRODUCT = PD.PRODUCTMASTER";
            sql += " WHERE PM.WAREHOUSE = " + warehouse + " AND PD.BARCODE = '" + barcode + "' AND PD.TYPE = '" +type+ "'";

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static double GetProductMinMaxLOID(string barcode, string warehouse)
        {
            string sql = "";
            sql = "SELECT PM.LOID";
            sql += " FROM PRODUCTMINMAX PM INNER JOIN PRODUCT PD ON PM.PRODUCT = PD.PRODUCTMASTER";
            sql += " WHERE PM.WAREHOUSE = " + warehouse + " AND PD.BARCODE = '" + barcode + "'";

            object LOID = OracleDB.ExecSingleCmd(sql);
            return Convert.ToDouble(LOID);
        }

        public static bool CheckProductMinMaxExist(string barcode, string warehouse)
        {
            string sql = "";
            sql = "SELECT COUNT(*)";
            sql += " FROM PRODUCTMINMAX PM INNER JOIN PRODUCT PD ON PM.PRODUCT = PD.PRODUCTMASTER";
            sql += " WHERE PM.WAREHOUSE = " + warehouse + " AND PD.BARCODE = '" + barcode + "'";

            object count = OracleDB.ExecSingleCmd(sql);
            if (Convert.ToInt32(count) > 0)
                return true;
            else
                return false;
        }




        public bool InsertData(string UserID, ControlStockData csData)
        {
            bool ret = true;
            ProductMinMaxDAL oDAL = new ProductMinMaxDAL();

            oDAL.PRODUCT = Convert.ToInt32(csData.PRODUCT);
            oDAL.WAREHOUSE = Convert.ToInt32(csData.WAREHOUSE);
            oDAL.STANDARD = csData.STANDARD;
            oDAL.MINIMUM = csData.MINIMUM;
            oDAL.MAXIMUM = csData.MAXIMUM;
            oDAL.ACTIVE = 1;

            ret = oDAL.InsertCurrentData(UserID, null);

            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;
        }

        public bool UpdateData(string UserID, ControlStockData csData)
        {
            bool ret = true;
            ProductMinMaxDAL oDAL = new ProductMinMaxDAL();

            oDAL.GetDataByLOID(csData.LOID, null);
            oDAL.STANDARD = csData.STANDARD;
            oDAL.MINIMUM = csData.MINIMUM;
            oDAL.MAXIMUM = csData.MAXIMUM;
            oDAL.ACTIVE = 1;

            ret = oDAL.UpdateCurrentData(UserID, null);

            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;
        }
    }
}
