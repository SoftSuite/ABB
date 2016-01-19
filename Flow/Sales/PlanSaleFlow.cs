using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public class PlanSaleFlow
    {
        private PlanForSaleDAL _pDAL;
        private PlanDAL _DAL;
        private PlanOrderDAL _DALitem;
        private PlanMarketDAL _DALitemMK;
        private string _error = "";
        private double _LOID = 0;

        private PlanForSaleDAL SaleDAL
        {
            get { if (_pDAL == null) { _pDAL = new PlanForSaleDAL(); } return _pDAL; }
        }

        private PlanDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new PlanDAL(); } return _DAL; }
        }

        private PlanOrderDAL DALItemObj
        {
            get { if (_DALitem == null) { _DALitem = new PlanOrderDAL(); } return _DALitem; }
        }

        private PlanMarketDAL DALItemObjMK
        {
            get { if (_DALitemMK == null) { _DALitemMK = new PlanMarketDAL(); } return _DALitemMK; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public DataTable GetPlanMarketList(PlanOrderSearchData data)
        {
            DataTable dt = SaleDAL.GetPlanMarketList(data);
            double rowIndex = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = rowIndex;
                rowIndex += 1;
            }
            return dt;
        }
        public DataTable GetPlanSaleList(PlanOrderSearchData data)
        {
            DataTable dt = SaleDAL.GetPlanSaleList(data);
            double rowIndex = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = rowIndex;
                rowIndex += 1;
            }
            return dt;
        }

        public DataTable GetPlanSaleItemList(PlanItemSearchData data)
        {
            return SaleDAL.GetPlanSaleItemList(data);
        }

        public DataTable GetPlanMKItemList(PlanItemSearchData data)
        {
            return SaleDAL.GetPlanMKItemList(data);
        }

        public PlanData GetPlanData(double plan)
        {
            PlanData data = new PlanData();
            DALObj.GetDataByLOID(plan, null);
            data.ACTIVE = DALObj.ACTIVE;
            data.CODE = DALObj.CODE;
            data.CONFIRMDATE = DALObj.CONFIRMDATE;
            data.CREATEBY = DALObj.CREATEBY;
            data.CREATEON = DALObj.CREATEON;
            data.DESCRIPTION = DALObj.DESCRIPTION;
            data.LOID = plan;
            data.PLANTYPE = DALObj.PLANTYPE;
            data.STATUS = DALObj.STATUS;
            data.YEAR = DALObj.YEAR;
            return data;
        }

        public bool CancelPlan(string userID, double plan)
        {
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PlanOrderSaleDAL orderSaleDAL = new PlanOrderSaleDAL();
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(plan, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                {
                    DALObj.CancelPlanSale(userID, DALObj.LOID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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
        private void UpdatePlanMarketing(string userID, double loid,PlanMarketingData data, OracleTransaction trans)
        {
            bool ret = true;
            DALItemObjMK.OnDB = false;
            DALItemObjMK.GetDataByLOID(loid, trans);

            //PlanMarketingData PMdata = new PlanMarketingData();
            DALItemObjMK.PERCENT = data.PERCENT;
            DALItemObjMK.M1 = data.M1;
            DALItemObjMK.M2 = data.M2;
            DALItemObjMK.M3 = data.M3;
            DALItemObjMK.M4 = data.M4;
            DALItemObjMK.M5 = data.M5;
            DALItemObjMK.M6 = data.M6;
            DALItemObjMK.M7 = data.M7;
            DALItemObjMK.M8 = data.M8;
            DALItemObjMK.M9 = data.M9;
            DALItemObjMK.M10 = data.M10;
            DALItemObjMK.M11 = data.M11;
            DALItemObjMK.M12 = data.M12;

            ret = DALItemObjMK.UpdateCurrentData(userID, trans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);            
        }

        private void CommitPlan(string userID, double plan, OracleTransaction trans)
        {
            bool ret = true;
            DALObj.OnDB = false;
            DALObj.GetDataByLOID(plan, trans);
            if (DALItemObj.GetDataByPlan(plan, trans).Rows.Count == 0)
                throw new ApplicationException("กรุณาระบุรายละเอียดแผนการผลิตปี " + DALObj.YEAR);
            else if (SaleDAL.HasUnplanedProduct(plan, trans))
                throw new ApplicationException("สินค้าบางรายการไม่ได้ประมาณการจำหน่าย");
            else
            {

                PlanOrderSaleDAL orderSaleDAL = new PlanOrderSaleDAL();
                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                {
                    DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                    DALObj.ACTIVE = Constz.ActiveStatus.Active;
                    DALObj.CONFIRMDATE = DateTime.Today;
                    ret = DALObj.UpdateCurrentData(userID, trans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALItemObj.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = orderSaleDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(orderSaleDAL.ErrorMessage);
                }
            }
        }

        public bool CommitPlan(string userID, ArrayList arr)
        {
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arr.Count; ++i)
                {
                    CommitPlan(userID, Convert.ToDouble(arr[i]), obj.zTrans);
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

        public bool CommitPlan(string userID, double plan)
        {
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                CommitPlan(userID, plan, obj.zTrans);

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

        public bool UpdateData(string userID, double loid, PlanMarketingData data)
        {
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                UpdatePlanMarketing(userID, loid, data, obj.zTrans);

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


        public bool DeletePlan(ArrayList arr)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PlanOrderDAL orderDAL = new PlanOrderDAL();
                PlanOrderSaleDAL orderSaleDAL = new PlanOrderSaleDAL();

                for (int i = 0; i < arr.Count; ++i)
                {
                    DALObj.OnDB = false;
                    double plan = Convert.ToDouble(arr[i]);
                    if (DALObj.GetDataByLOID(plan, obj.zTrans))
                    {
                        if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                        {
                            ret = DALObj.DeleteCurrentData(obj.zTrans);
                            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                            orderDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            orderSaleDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                        }

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

        public bool InsertPlan(string userID, PlanData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!DALObj.IsDuplicateData(data.YEAR, data.PLANTYPE, obj.zTrans))
                {
                    _DAL = new PlanDAL();
                    DALObj.OnDB = false;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.DESCRIPTION = data.DESCRIPTION;
                    DALObj.PLANTYPE = data.PLANTYPE;
                    DALObj.STATUS = data.STATUS;
                    DALObj.YEAR = data.YEAR;

                    ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    _LOID = DALObj.LOID;

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
            return ret;
        }

        public bool CopyPlan(string userID, double year, string description, double planSource)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.GetDataByLOID(planSource, obj.zTrans);
                if (DALObj.OnDB)
                {
                    if (!DALObj.IsDuplicateData(year.ToString(), DALObj.PLANTYPE, obj.zTrans))
                    {
                        PlanDAL pDAL = new PlanDAL();
                        pDAL.OnDB = false;
                        pDAL.ACTIVE = Constz.ActiveStatus.InActive;
                        pDAL.DESCRIPTION = description;
                        pDAL.PLANTYPE = DALObj.PLANTYPE;
                        pDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                        pDAL.YEAR = year.ToString();

                        ret = pDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(pDAL.ErrorMessage);

                        ret = pDAL.CopyPlan(userID, planSource, pDAL.LOID, obj.zTrans);
                        if (!ret) throw new ApplicationException(pDAL.ErrorMessage);

                        _LOID = pDAL.LOID;

                        obj.zTrans.Commit();
                        obj.CloseConnection();
                    }
                    else
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }
                }
                else
                    throw new ApplicationException("ไม่พบข้อมูลแผนการจำหน่ายที่ต้องการคัดลอก");
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

        private bool AddProduct(double plan, string userID, string where)
        {
            ProductDAL pDAL = new ProductDAL();
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DataTable dtProduct = SaleDAL.GetProductPlanList(where, obj.zTrans);
                foreach (DataRow dRow in dtProduct.Rows)
                {
                    DALItemObj.OnDB = false;
                    DALItemObj.PLAN = plan;
                    DALItemObj.PRODUCT = Convert.ToDouble(dRow["LOID"]);
                    DALItemObj.STATUS = Constz.Requisition.Status.Waiting.Code;
                    DALItemObj.UNIT = Convert.ToDouble(dRow["UNIT"]);
                    DALItemObj.PRODUCTMASTER = Convert.ToDouble(dRow["PRODUCTMASTER"]);
                    ret = DALItemObj.InsertCurrentData(userID, obj.zTrans);
                    if (!ret)
                    {
                        _error = DALItemObj.ErrorMessage;
                        break;
                    }
                }
                if (ret)
                {
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
            return ret;
        }

        private bool AddCustomer(double plan, string userID, string where)
        {
            //ProductDAL pDAL = new ProductDAL();
            bool ret = true;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DataTable dtCustomer = SaleDAL.GetAllCustomerList(where, obj.zTrans);
                foreach (DataRow dRow in dtCustomer.Rows)
                {
                    DALItemObjMK.OnDB = false;
                    DALItemObjMK.PLAN = plan;
                    DALItemObjMK.CUSTOMER = Convert.ToDouble(dRow["LOID"]);
                    DALItemObjMK.STATUS = Constz.Requisition.Status.Waiting.Code;
                    DALItemObjMK.RANK = Convert.ToDouble(dRow["RANK"]);
                    //   DALItemObjMK.PERCENT = Convert.ToDouble(dRow["PERCENT"]);
                    ret = DALItemObjMK.InsertCurrentData(userID, obj.zTrans);
                    if (!ret)
                    {
                        _error = DALItemObj.ErrorMessage;
                        break;
                    }
                }
                if (ret)
                {
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
            return ret;
        }


        public bool AddSomeProduct(double plan, string userID, string product)
        {
            bool ret = true;
            string where = "P.LOID IN (" + product.Substring(0, product.Length - 1) + ") AND P.LOID NOT IN (SELECT PRODUCT FROM PLANORDER WHERE PLAN = " + plan.ToString() + ") ";
            ret = AddProduct(plan, userID, where);
            return ret;
        }

        public bool AddAllProduct(double plan, string userID)
        {
            bool ret = true;
            string where = "P.LOID NOT IN (SELECT PRODUCT FROM PLANORDER WHERE PLAN = " + plan.ToString() + ") ";
            ret = AddProduct(plan, userID, where);
            return ret;
        }

        public bool AddAllCustomer(double plan, string userID)
        {
            bool ret = true;
            string where = "LOID NOT IN (SELECT CUSTOMER FROM PLANMARKET WHERE PLAN = " + plan.ToString() + ") ";
            ret = AddCustomer(plan, userID, where);
            return ret;
        }

        public bool DeletePlanOrder(ArrayList arr)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PlanOrderSaleDAL sDAL = new PlanOrderSaleDAL();
                for (int i = 0; i < arr.Count; ++i)
                {
                    DALItemObj.OnDB = false;
                    double planOrder = Convert.ToDouble(arr[i]);
                    if (DALItemObj.GetDataByLOID(planOrder, obj.zTrans))
                    {
                        if (DALItemObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                            ret = DALItemObj.DeleteCurrentData(obj.zTrans);

                        if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                        sDAL.DeleteDataByPlanAndProduct(DALItemObj.PLAN, DALItemObj.PRODUCT, obj.zTrans);
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

        public PlanMarketingData DoGetValueFront(int year)
        {
            return DALItemObjMK.DoGetValueFront(year);
        }

        public PlanMarketingData DoGetValueOther(int year)
        {
            return DALItemObjMK.DoGetValueOther(year);
        }

        public PlanMarketingData DoGetValue(int year, double customer)
        {
            return DALItemObjMK.DoGetValue(year,customer);
        }

        //public bool UpdatePM(double M1, double M2, double M3, double M4, double M5, double M6, double M7, double M8, double M9, double M10, double M11, double M12, string userID)
        //{
        //    bool ret = true;
        //    DALItemObjMK.M1 = M1;
        //    DALItemObjMK.M2 = M2;
        //    DALItemObjMK.M3 = M3;
        //    DALItemObjMK.M4 = M4;
        //    DALItemObjMK.M5 = M5;
        //    DALItemObjMK.M6 = M6;
        //    DALItemObjMK.M7 = M7;
        //    DALItemObjMK.M8 = M8;
        //    DALItemObjMK.M9 = M9;
        //    DALItemObjMK.M10 = M10;
        //    DALItemObjMK.M11 = M11;
        //    DALItemObjMK.M12 = M12;

        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {
        //        ret = DALItemObjMK.UpdateCurrentData(userID, obj.zTrans);

        //        if (ret)
        //        {
        //            obj.zTrans.Commit();
        //            obj.CloseConnection();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.zTrans.Rollback();
        //        obj.CloseConnection();
        //        ret = false;
        //        _error = ex.Message;
        //    }
        //    return ret;
        //}


    }
}
