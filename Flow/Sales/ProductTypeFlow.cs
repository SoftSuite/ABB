using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    public class ProductTypeFlow
    {
        string _error = "";
        ProductTypeDAL _dal;
        double _LOID = 0;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public ProductTypeDAL DALObj
        {
            get { if (_dal == null) { _dal = new ProductTypeDAL(); } return _dal; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", null);
        }

        private bool VeridateData(ProductTypeSearchData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุรหัสประเภทสินค้า";
            }
            else if (data.NAME.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุชื่อประเภทสินค้า";
            }
            return ret;
        }

        public ProductTypeSearchData GetData(double loid)
        {
            ProductTypeSearchData data = new ProductTypeSearchData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.TYPE = DALObj.TYPE;
            }
            return data;
        }

        public bool UpdateData(string userID, ProductTypeSearchData data)
        {
            bool ret = true;
            if (VeridateData(data))
            {

                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    DALObj.LOID = data.LOID;
                    DALObj.NAME = data.NAME.Trim();
                    DALObj.ACTIVE = data.ACTIVE.Trim();
                    DALObj.CODE = data.CODE.Trim();
                    DALObj.TYPE = data.TYPE.Trim();

                    int check = Convert.ToInt32(OracleDB.ExecSingleCmd("SELECT COUNT( *) FROM PRODUCTTYPE WHERE CODE ='" + data.CODE.Trim() + "' AND LOID != " + data.LOID));
                    int check1 = Convert.ToInt32(OracleDB.ExecSingleCmd("SELECT COUNT( *) FROM PRODUCTTYPE WHERE NAME ='" + data.NAME.Trim() + "' AND LOID != " + data.LOID));
                    if (check > 0)
                    {
                        // ซ้ำ
                        ret = false;
                        _error = "รหัสประเภทสินค้าซ้ำ";
                    }
                    else if (check1 > 0)
                    {
                        ret = false;
                        _error = "ชื่อประเภทสินค้ามีอยู่แล้ว";
                    }
                    else
                    {
                        // ไม่ซ้ำ ทำการ insert 
                        if (DALObj.OnDB)
                            ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        else
                            ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                        _LOID = DALObj.LOID;
                        if (ret)
                        {
                            obj.zTrans.Commit();
                            obj.CloseConnection();
                        }
                        else
                        {
                            throw new ApplicationException(DALObj.ErrorMessage);
                        }
                    }
                }
                catch (Exception ex)
                {
                    obj.zTrans.Rollback();
                    obj.CloseConnection();
                    ret = false;
                    _error = ex.Message;
                }
            }
            else ret = false;
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
