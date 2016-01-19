using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.DAL;
using ABB.DAL.Inventory.FG;
using ABB.Data;
using ABB.Data.Inventory.FG;

namespace ABB.Flow.Inventory.FG
{
    public class PlanInventoryFlow
    {
        private PlanInventoryDAL _pDAL;
        private PlanDAL _DAL;
        private string _error = "";
        private double _LOID = 0;

        private PlanInventoryDAL PlanInvDAL
        {
            get { if (_pDAL == null) { _pDAL = new PlanInventoryDAL(); } return _pDAL; }
        }

        private PlanDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new PlanDAL(); } return _DAL; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public DataTable GetPlanList(PlanSearchData data)
        {
            DataTable dt = PlanInvDAL.GetPlanList(data);
            double rowIndex = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = rowIndex;
                rowIndex += 1;
            }
            return dt;
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
                PlanProduceDAL produceDAL = new PlanProduceDAL();
                PlanPurchaseDAL purchaseDAL = new PlanPurchaseDAL();
                PlanReceiveDAL receiveDAL = new PlanReceiveDAL();
                PlanReceiveItemDAL receiveItemDAL = new PlanReceiveItemDAL();
                PlanRemainDAL remainDAL = new PlanRemainDAL();
                PlanSaleDAL saleDAL = new PlanSaleDAL();
                PlanUseDAL useDAL = new PlanUseDAL();

                DALObj.OnDB = false;
                DALObj.GetDataByLOID(plan, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                {
                    DALObj.STATUS = Constz.Requisition.Status.Void.Code;
                    DALObj.ACTIVE = Constz.ActiveStatus.InActive;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    produceDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
                    purchaseDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
                    receiveDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
                    receiveItemDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
                    remainDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
                    saleDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
                    useDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, obj.zTrans);
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

        private void CommitPlan(string userID, double plan, OracleTransaction trans)
        {
            bool ret = true;
            DALObj.OnDB = false;
            if (!PlanInvDAL.HaveProductNotReceive(plan, trans))
            {
                DALObj.GetDataByLOID(plan, trans);

                PlanProduceDAL produceDAL = new PlanProduceDAL();
                PlanPurchaseDAL purchaseDAL = new PlanPurchaseDAL();
                PlanReceiveDAL receiveDAL = new PlanReceiveDAL();
                PlanReceiveItemDAL receiveItemDAL = new PlanReceiveItemDAL();
                PlanRemainDAL remainDAL = new PlanRemainDAL();
                PlanSaleDAL saleDAL = new PlanSaleDAL();
                PlanUseDAL useDAL = new PlanUseDAL();

                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                {
                    DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                    DALObj.ACTIVE = Constz.ActiveStatus.Active;
                    DALObj.CONFIRMDATE = DateTime.Today;
                    ret = DALObj.UpdateCurrentData(userID, trans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = produceDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(produceDAL.ErrorMessage);
                    ret = purchaseDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(purchaseDAL.ErrorMessage);
                    ret = receiveDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(receiveDAL.ErrorMessage);
                    ret = receiveItemDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(receiveItemDAL.ErrorMessage);
                    ret = remainDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(remainDAL.ErrorMessage);
                    ret = saleDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(saleDAL.ErrorMessage);
                    ret = useDAL.UpdateStatusByPlan(plan, DALObj.STATUS, userID, trans);
                    if (!ret) throw new ApplicationException(useDAL.ErrorMessage);
                }
            }
            else
                throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากมีสินค้าบางรายการยังไม่ได้สั่งซื้อ/สั่งผลิต");
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

        public bool DeletePlan(ArrayList arr)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PlanProduceDAL produceDAL = new PlanProduceDAL();
                PlanPurchaseDAL purchaseDAL = new PlanPurchaseDAL();
                PlanReceiveDAL receiveDAL = new PlanReceiveDAL();
                PlanRemainDAL remainDAL = new PlanRemainDAL();
                PlanSaleDAL saleDAL = new PlanSaleDAL();
                PlanUseDAL useDAL = new PlanUseDAL();
                PlanReceiveItemDAL receiveItemDAL = new PlanReceiveItemDAL();

                for (int i = 0; i < arr.Count; ++i)
                {
                    DALObj.OnDB = false;
                    double plan = Convert.ToDouble(arr[i]);
                    if (DALObj.GetDataByLOID(plan, obj.zTrans))
                    {
                        if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                        {
                            produceDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            purchaseDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            receiveDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            receiveItemDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            remainDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            saleDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);
                            useDAL.DeleteDataByPlan(DALObj.LOID, obj.zTrans);

                            ret = DALObj.DeleteCurrentData(obj.zTrans);
                            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        private void ValidateData(string year, OracleTransaction trans)
        {
            bool ret = true;
            if (DALObj.IsDuplicateData(year, Constz.PlanType.FG, trans))
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }
            else if (!DALObj.HasPlanOrder(year, trans))
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }
        }

        public bool InsertPlan(string userID, PlanData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ValidateData(data.YEAR, obj.zTrans);
                _DAL = new PlanDAL();
                DALObj.OnDB = false;
                DALObj.ACTIVE = data.ACTIVE;
                DALObj.DESCRIPTION = data.DESCRIPTION;
                DALObj.PLANTYPE = data.PLANTYPE;
                DALObj.STATUS = data.STATUS;
                DALObj.YEAR = data.YEAR;

                ret = DALObj.InsertCurrentData(userID, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = DALObj.InsertFGPlan(userID, DALObj.LOID, data.YEAR, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                _LOID = DALObj.LOID;

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

        public DataTable GetPlanDetailList(PlanDetailSearchData data)
        {
            return PlanInvDAL.GetPlanDetailList(data);
        }

        public bool CalculatePlanUseAndRemain(string userID, double plan)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ret = DALObj.CalculateFGPlanUseAndRemain(userID, plan, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

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
