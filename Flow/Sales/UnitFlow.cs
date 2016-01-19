using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Sales;
using System.Data.OracleClient;
using System.Data;
using ABB.DAL;
using ABB.Data;

namespace ABB.Flow.Sales
{
    public class UnitFlow
    {
        private string _error = "";
        double _LOID = 0;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public ArrayList GetSearchUnit(UnitSearchData uSearch)
        {
            //string str = "";
            ArrayList arrResult = new ArrayList();

            //str = " SELECT * FROM UNIT ";
            //str += " WHERE NAME = '" + name + "'";
            //str += " AND E = " + Ename;

            //if (Barcode != "")
            //{
            //    str += " AND BARCODE  = '" + Barcode + "'";
            //}

            //if (PName != "")
            //{
            //    str += " AND PNAME LIKE '%" + PName + "%'";
            //}

            //try
            //{
            //    OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
            //    arrResult.Clear();
            //    int i = 1;
            //    while (zRd.Read())
            //    {
            //        V_Product_List_RequisitionData irData = new V_Product_List_RequisitionData();
            //        irData.ORDERNO = i;
            //        irData.BARCODE = zRd["BARCODE"].ToString();
            //        irData.PNAME = zRd["PNAME"].ToString();
            //        arrResult.Add(irData);
            //        i = i + 1;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return arrResult;
        }

        UnitDAL _dal;
        public UnitDAL DALObj
        {
            get { if (_dal == null) { _dal = new UnitDAL(); } return _dal; }
        }

        public DataTable GetDataList(string sWhere)
        {
            return DALObj.GetDataList(sWhere, null);
        }

        private bool VeridateData(UnitSearchData data)
        {
            bool ret = true;
            if (data.NAME.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุชื่อหน่วยนับภาษาไทย";
            }
            else if (data.ENAME.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุชื่อหน่วยนับภาษาอังกฤษ";
            }
            else if (data.TYPE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุเพื่อใช้สำหรับ";
            }
            return ret;
        }

        public UnitSearchData GetData(double loid)
        {
            UnitSearchData data = new UnitSearchData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.ACTIVE = DALObj.ACTIVE;
                data.TYPE = DALObj.TYPE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.NAME = DALObj.NAME;
                data.ENAME = DALObj.ENAME;
            }
            return data;
        }

        public bool UpdateData(string userID, UnitSearchData data)
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
                    DALObj.ENAME = data.ENAME.Trim();
                    DALObj.TYPE = data.TYPE.Trim();
                    DALObj.CODE = data.CODE.Trim();
                    DALObj.ACTIVE = data.ACTIVE.Trim();

                    int check = Convert.ToInt32(OracleDB.ExecSingleCmd("SELECT COUNT(*) FROM UNIT WHERE NAME ='" + data.NAME.Trim() + "' AND LOID != " + data.LOID));
                    int check1 = Convert.ToInt32(OracleDB.ExecSingleCmd("SELECT COUNT(*) FROM UNIT WHERE ENAME ='" + data.ENAME.Trim() + "' AND LOID != " + data.LOID));
                    if (check > 0)
                    {
                        ret = false;
                        _error = "ชื่อหน่วยนับภาษาไทยซ้ำ";
                    }
                    else if (check1 > 0)
                    {
                        ret = false;
                        _error = "ชื่อหน่วยนับภาษาอังกฤษซ้ำ";
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
                        if (DALObj.ErrorMessage.Contains("integrity constraint"))
                            throw new Exception(GlobalErrorMessage.ConstaintDelete);
                        else
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
