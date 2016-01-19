using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    public class MemberTypeFlow
    {
        string _error = "";
        double _LOID = 0;
        MemberTypeDAL _dal;
        DiscountStepDAL _dalItem;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public double LOID
        {
            get { return _LOID; }
        }
        public MemberTypeDAL DALObj
        {
            get { if (_dal == null) { _dal = new MemberTypeDAL(); } return _dal; }
        }
        public DiscountStepDAL DALItemObj
        {
            get { if (_dalItem == null) { _dalItem = new DiscountStepDAL(); } return _dalItem; }
        }

        public DataTable GetDataList()
        {
            return DALObj.GetDataList("", null);
        }

        public DataTable GetDiscountStepItem(double memberType)
        {
            return DALItemObj.GetDataList("WHERE MEMBERTYPE = " + memberType.ToString(), null);
        }

        public DataTable GetDiscountStepItemBlank()
        {
            DataTable dt = DALItemObj.GetDataList("WHERE MEMBERTYPE = 0 ", null);
            DataRow dRow = dt.NewRow();
            dt.Rows.Add(dRow);
            return dt;
        }

        private bool VeridateData(MemberTypeData data)
        {
            bool ret = true;
            if (data.CODE.Trim() == "")
            {
                _error = "กรุณาระบุรหัสประเภทลูกค้า";
                ret = false;
            }
            else if (data.NAME.Trim() == "")
            {
                _error = "กรุณาระบุชื่อประเภทลูกค้า";
                ret = false;
            }
            return ret;
        }

        public MemberTypeData GetData(double loid)
        {
            MemberTypeData data = new MemberTypeData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.DISCOUNT = DALObj.DISCOUNT;
                data.LOWERPRICE = DALObj.LOWERPRICE;
                data.DESCRIPTION = DALObj.DESCRIPTION;
            }
            return data;
        }

        public bool UpdateData(string userID, MemberTypeData data)
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
                    DALObj.DESCRIPTION = data.DESCRIPTION.Trim();
                    DALObj.ACTIVE = data.ACTIVE.Trim();
                    DALObj.CODE = data.CODE.Trim();
                    DALObj.DISCOUNT = data.DISCOUNT;
                    DALObj.LOWERPRICE = data.LOWERPRICE;

                    int check = Convert.ToInt32(OracleDB.ExecSingleCmd("SELECT COUNT(*) FROM MEMBERTYPE WHERE NAME ='" + data.NAME.Trim() + "' AND LOID != " + data.LOID));
                    if (check > 0)
                    {
                        ret = false;
                        _error = "ประเภทลูกค้าซ้ำ";
                    }
                    else
                    {
                        DALItemObj.DeleteDataByMemberType(data.LOID, obj.zTrans);

                        if (DALObj.OnDB)
                            ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        else
                            ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                        _LOID = DALObj.LOID;
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        for (int i = 0; i < data.ITEM.Count; ++i)
                        {
                            DiscountStepData itemData = (DiscountStepData)data.ITEM[i];
                            DALItemObj.OnDB = false;
                            DALItemObj.MEMBERTYPE = DALObj.LOID;
                            DALItemObj.LOWERPRICE = itemData.LOWERPRICE;
                            DALItemObj.DISCOUNT = itemData.DISCOUNT;
                            ret = DALItemObj.InsertCurrentData(userID, obj.zTrans);
                            if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);
                        }

                        obj.zTrans.Commit();
                        obj.CloseConnection();
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
                    DALItemObj.DeleteDataByMemberType(DALObj.LOID, obj.zTrans);

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
