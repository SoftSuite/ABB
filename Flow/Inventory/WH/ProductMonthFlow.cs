using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.Data.Inventory.WH;

namespace ABB.Flow.Inventory.WH
{
    public class ProductMonthFlow
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        ProductMonthDAL _dal;
        public ProductMonthDAL DALObj
        {
            get { if (_dal == null) { _dal = new ProductMonthDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", "LOID", null);
        }

        public DataTable GetDataList(string sWhere)
        {
            return DALObj.GetDataList(sWhere, "LOID", null);
        }


        public ProductMonthData GetData(double PRODUCT)
        {
            ProductMonthData data = new ProductMonthData();
            if (DALObj.GetDataByPRODUCT(PRODUCT, null))
            {
                data.LOID = DALObj.LOID;
                data.PRODUCT = DALObj.PRODUCT;
                data.Month[0] = DALObj.M1;
                data.Month[1] = DALObj.M2;
                data.Month[2] = DALObj.M3;
                data.Month[3] = DALObj.M4;
                data.Month[4] = DALObj.M5;
                data.Month[5] = DALObj.M6;
                data.Month[6] = DALObj.M7;
                data.Month[7] = DALObj.M8;
                data.Month[8] = DALObj.M9;
                data.Month[9] = DALObj.M10;
                data.Month[10] = DALObj.M11;
                data.Month[11] = DALObj.M12;

            }
            return data;
        }

        public bool InsertData(string userID, ProductMonthData data)
        {
            bool ret = true;

            DALObj.PRODUCT = DALObj.doGetProduct(data.CODE);
            DALObj.M1 = data.Month[0];
            DALObj.M2 = data.Month[1];
            DALObj.M3 = data.Month[2];
            DALObj.M4 = data.Month[3];
            DALObj.M5 = data.Month[4];
            DALObj.M6 = data.Month[5];
            DALObj.M7 = data.Month[6];
            DALObj.M8 = data.Month[7];
            DALObj.M9 = data.Month[8];
            DALObj.M10 = data.Month[9];
            DALObj.M11 = data.Month[10];
            DALObj.M12 = data.Month[11];
         
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }
                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                throw new ApplicationException(ex.Message);
            }
            return ret;
        }

        public bool UpdateData(string userID, ProductMonthData data)
        {
            bool ret = true;
            DALObj.OnDB = false;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByPRODUCT(data.PRODUCT, obj.zTrans);
                DALObj.LOID = data.LOID;
                DALObj.PRODUCT = data.PRODUCT;
                DALObj.M1 = data.Month[0];
                DALObj.M2 = data.Month[1];
                DALObj.M3 = data.Month[2];
                DALObj.M4 = data.Month[3];
                DALObj.M5 = data.Month[4];
                DALObj.M6 = data.Month[5];
                DALObj.M7 = data.Month[6];
                DALObj.M8 = data.Month[7];
                DALObj.M9 = data.Month[8];
                DALObj.M10 = data.Month[9];
                DALObj.M11 = data.Month[10];
                DALObj.M12 = data.Month[11];

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                else
                    ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }
                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                throw new ApplicationException(ex.Message);
            }
            return ret;
        }

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.PRODUCT = Convert.ToDouble(arrData[i]);
                    ret = DALObj.DeleteCurrentData(obj.zTrans);
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }
                }
                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                throw new ApplicationException(ex.Message);
            }
            return ret;
        }

    }
}
