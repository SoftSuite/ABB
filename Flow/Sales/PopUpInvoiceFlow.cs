using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Sales;
using System.Data.OracleClient;
using System.Data;
using ABB.DAL;
using ABB.Data.Search;

namespace ABB.Flow.Sales
{
    public class PopUpInvoiceFlow
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public ArrayList GetSearchUnit(ProductReserveSearchData uSearch)
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

        PopUpInvoiceDAL _dal;
        public PopUpInvoiceDAL DALObj
        {
            get { if (_dal == null) { _dal = new PopUpInvoiceDAL(); } return _dal; }
        }

        public DataTable GetDataList(string sWhere)
        {
            return DALObj.GetDataList(sWhere, null);
        }

        private bool VeridateData(ProductReserveSearchData data)
        {
            bool ret = true;
            if (data.INVCODE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุเลขที่ใบเสร็จ";
            }
            else if (data.CODE.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุรหัสสมาชิก";
            }
            return ret;
        }
        public InvoiceReturnData GetCustomerList(string code)
        {
            InvoiceReturnData data = new InvoiceReturnData();
            if (DALObj.GetDataByCODE(code, null))
            {
                //data.LOID = DALObj.LOID;
                data.INVCODE = DALObj.INVCODE;
                data.CUSTOMERCODE = DALObj.CUSTOMERCODE;
                data.CUSTOMERNAME = DALObj.CUSTOMERNAME;
                data.PRODUCTNAME = DALObj.PRODUCTNAME;
            }
            return data;
        }
        public ProductReserveSearchData GetData(double loid)
        {
            ProductReserveSearchData data = new ProductReserveSearchData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.CUSTOMERNAME = DALObj.CUSTOMERNAME;
                //data.ACTIVE = DALObj.ACTIVE;
                data.CODE = DALObj.CODE;
                data.LOID = DALObj.LOID;
                data.INVCODE = DALObj.INVCODE;
            }
            return data;
        }

        public bool UpdateData(string userID, ProductReserveSearchData data)
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
                    //DALObj.NAME = data.NAME.Trim();
                    //DALObj.ENAME = data.ENAME.Trim();
                    //DALObj.TYPE = data.TYPE.Trim();
                    DALObj.CODE = data.CODE.Trim();
                    //DALObj.ACTIVE = data.ACTIVE.Trim();

                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

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
