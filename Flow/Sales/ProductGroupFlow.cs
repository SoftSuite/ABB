using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.DAL.Sales;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    public class ProductGroupFlow
    {
        string _error = "";
        ProductGroupDAL _dal;
        double _LOID = 0;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }
        public ProductGroupDAL DALObj
        {
            get { if (_dal == null) { _dal = new ProductGroupDAL(); } return _dal; }
        }

        public DataTable GetDataList()
        {
            ProductGroupSearchDAL pgDAL = new ProductGroupSearchDAL();
            return pgDAL.GetProductGourpList();
        }

        private bool ValidateData(ProductGroupData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุรหัสกลุ่มสินค้า";
            }
            else if (data.PRODUCTTYPE == 0)
            {
                ret = false;
                _error = "กรุณาเลือกประเภทสินค้า";
            }
            else if (data.NAME.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุชื่อกลุ่มสินค้า";
            }
            else if (DALObj.GetDataByCode(data.CODE, data.LOID, null))
            {
                ret = false;
                _error = "รหัสกลุ่มสินค้าซ้ำ";
            }
            else if (DALObj.GetDataByName(data.NAME, data.PRODUCTTYPE, data.LOID, null))
            {
                ret = false;
                _error = "ชื่อกลุ่มสินค้าซ้ำ ในประเภทสินค้าเดียวกัน";
            }
            return ret;
        }

        public ProductGroupData GetData(double loid)
        {
            ProductGroupData data = new ProductGroupData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.PRODUCTTYPE = DALObj.PRODUCTTYPE;
            }
            return data;
        }

        public bool UpdateData(string userID, ProductGroupData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    DALObj.LOID = data.LOID;
                    DALObj.NAME = data.NAME.Trim();
                    DALObj.PRODUCTTYPE = data.PRODUCTTYPE;
                    DALObj.ACTIVE = data.ACTIVE.Trim();
                    DALObj.CODE = data.CODE.Trim();

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
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
                   _error = ex.Message;
                }
            }
            else
                ret = false;
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
                    DALObj.LOID = Convert.ToDouble(arrData[i]);
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
                _error = ex.Message;
            }
            return ret;
        }

    }
}
