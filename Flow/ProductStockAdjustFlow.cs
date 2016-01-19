using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.DAL;

namespace ABB.Flow
{
    public class ProductStockAdjustFlow
    {
        ProductStockAjustDAL _adjDAL;
        ProductStockDAL _DAL;
        string _error = "";

        public ProductStockAjustDAL AdjustDAL
        {
            get { if (_adjDAL == null) { _adjDAL = new ProductStockAjustDAL(); } return _adjDAL; }
        }

        private ProductStockDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new ProductStockDAL(); } return _DAL; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetStockList(double warehouse, double zone)
        {
            DataTable dt = AdjustDAL.GetStockList(warehouse, zone);
            double rowIndex = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = rowIndex;
                rowIndex += 1;
            }
            return dt;
        }

        public bool UpdateData(string userID, double loid, double qty)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(loid, obj.zTrans);
                DALObj.QTY = qty;
                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                obj.zTrans.Commit();
                obj.zConn.Close();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.zConn.Close();
            }
            return ret;
        }

        public bool UpdateData(string userID, ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; ++i)
                {
                    ProductStockAdjustData data = (ProductStockAdjustData)arrData[i];
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    DALObj.QTY = data.QTY;
                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                }

                obj.zTrans.Commit();
                obj.zConn.Close();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.zConn.Close();
            }
            return ret;
        }
    }
}
