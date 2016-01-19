using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Data.Sales ;

namespace ABB.Flow.Sales
{
    public  class ControlStockFlow
    {
        private string _error = "";
        private double _LOID = 0;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double GetLoid
        {
            get { return _LOID; }
        }

        public static DataTable GetStockList(double wharehouse)
        {
            ControlStockDAL dal = new ControlStockDAL();
            DataTable dt = dal.GetStockList(wharehouse);
            dt.Columns.Add("ORDERNO");
            int index = 0;
            foreach (DataRow dRow in dt.Rows)
            {
                index +=1;
                dRow["ORDERNO"] = index;
            }
            return dt;
        }

        public static DataTable GetProduct(double loid)
        {
            string sql = "";
            sql = "SELECT PD.BARCODE, PD.NAME, U.NAME UNIT, UM.NAME UNITMASTER FROM PRODUCT PD INNER JOIN UNIT U ON PD.UNIT = U.LOID INNER JOIN UNIT UM ON PD.UNITMASTER = UM.LOID";
            sql += " WHERE PD.PRODUCTMASTER ='" + loid + "' ORDER BY BARCODE, PD.NAME ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public static DataTable GetMinMax(double loid, double warehouse)
        {
            string sql = "";
            sql = "SELECT * FROM PRODUCTMINMAX";
            sql += " WHERE PRODUCT =" + loid + " AND WAREHOUSE = " + warehouse.ToString();

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }

        public bool UpdateData(string userID, ControlStockData data)
        {
            bool ret = true;

            ProductMinMaxDAL oDAL = new ProductMinMaxDAL();

                oDAL.GetDataByLOID(data.LOID, null);
 
                oDAL.STANDARD = Convert.ToDouble(data.STANDARD);
                oDAL.MINIMUM = Convert.ToDouble(data.MINIMUM);
                oDAL.MAXIMUM = Convert.ToDouble(data.MAXIMUM);

                ret = oDAL.UpdateCurrentData(userID, null);

                if (!ret)
                {
                    _error = oDAL.ErrorMessage;
                }
         
            return ret;
        }
        public bool InsertData(string userID, ControlStockData data)
        {
            Boolean ret = true;
            ProductMinMaxDAL oDAL = new ProductMinMaxDAL();

            oDAL.PRODUCT = Convert.ToInt32(data.PRODUCT);
            oDAL.WAREHOUSE = Convert.ToInt32(data.WAREHOUSE);
            oDAL.STANDARD = Convert.ToDouble(data.STANDARD);
            oDAL.MINIMUM = Convert.ToDouble(data.MINIMUM);
            oDAL.MAXIMUM = Convert.ToDouble(data.MAXIMUM);
            oDAL.ACTIVE = Convert.ToInt32(data.ACTIVE);

            ret = oDAL.InsertCurrentData(userID, null);

            if (ret == false)
                _error = oDAL.ErrorMessage;
            else
                _LOID = oDAL.LOID;

            return ret;

        }

        public static string GetProductName(string Barcode) 
        {
            ProductDAL pDAL = new ProductDAL();
            pDAL.GetDataByBarCode(Barcode, null);
            return pDAL.NAME;
        }

        public static string GetProductLoid(string Barcode)
        {
            ProductDAL pDAL = new ProductDAL();
            pDAL.GetDataByBarCode(Barcode, null);
            return pDAL.PRODUCTMASTER.ToString();
        }

        public static string GetWarehouseName(string WHLoid)
        {
            WarehouseDAL wDAL = new WarehouseDAL();
            wDAL.GetDataByLOID(Convert.ToDouble(WHLoid == "" ? "0" : WHLoid), null);
            return wDAL.NAME;
        }

        public bool DeleteData(string userID, ControlStockData data)
        {
            bool ret = true;
            ProductMinMaxDAL oDAL = new ProductMinMaxDAL();

            oDAL.LOID = Convert.ToDouble(data.LOID);

            ret = oDAL.DeleteCurrentData(null);

            if (ret == false)
                _error = oDAL.ErrorMessage;

            return ret;

        }
    }
}
