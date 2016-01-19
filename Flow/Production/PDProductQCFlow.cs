using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Inventory.FG;
using ABB.Data.Production;
using ABB.DAL;
using ABB.DAL.Production;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;
using ABB.Flow.Sales;

namespace ABB.Flow.Production
{
    public class PDProductQCFlow
    {
        string _error = "";
        double _LOID = 0;
        PDProductDAL _dal;
        PDOrderDAL _PDOrderdal;
        QCAnalysisPDDAL search;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PDProductDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDProductDAL(); } return _dal; }
        }

        public PDOrderDAL PDOrderDALObj
        {
            get { if (_PDOrderdal == null) { _PDOrderdal = new PDOrderDAL(); } return _PDOrderdal; }
        }

        public QCAnalysisPDDAL SearchDAL
        {
            get { if (search == null) search = new QCAnalysisPDDAL(); return search; }
        }



        public PDProductData GetData(double loid)
        {
            PDProductData data = new PDProductData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.PRODSTATUS = DALObj.PRODSTATUS;
                data.LOTNO = DALObj.LOTNO;
                data.CREATEBY = DALObj.CREATEBY;
                data.SENDQCDATE = DALObj.SENDQCDATE;
                data.PDORDER = DALObj.PDORDER;
                PDOrderDAL PDDAL = new PDOrderDAL();
                PDDAL.GetDataByLOID(data.PDORDER, null);
                data.ANACODE = PDDAL.ANACODE;
                data.ANADATE = PDDAL.ANADATE;
            }
            return data;
        }



        public DataTable GetPDProductItem(double pdorder)
        {
            DataTable dt = SearchDAL.GetPDProductItem(pdorder);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }






        public bool UpdateQCResult(double loid, DateTime duedate, double qcqty1, double qcqty2, double qcqty3, string qcresult, string qcremark, string userID, string status)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = SearchDAL.UpdateQCResult(loid, duedate, qcqty1, qcqty2, qcqty3, qcresult, qcremark, userID, status, obj.zTrans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                else
                {
                    double pdorder = SearchDAL.GetPDOrder(loid);
                    ret = SearchDAL.UpdatePDOrder(pdorder, userID, status, obj.zTrans);
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

        public bool UpdateQCStockin(double loid, string anacode, DateTime anadate, string userID, string status)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                ret = SearchDAL.UpdateQCStockin(loid, anacode, anadate, userID, status, obj.zTrans);
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


        public SupplierData GetSenderData(double loid)
        {
            StockFGDAL pFlow = new StockFGDAL();
            return pFlow.DoGetSenderData(loid);
        }

        public StockinQCData GetPDOrderData(double pdorder)
        {
            return SearchDAL.DoGetPDOrderData(pdorder);
        }

        public string GetDivision(string createBy)
        {
            return SearchDAL.GetDivision(createBy);
        }
        public bool ValidateData(StockinQCData data)
        {
            bool ret = true;
            if (data.ANACODE == "")
            {
                ret = false;
                _error = "กรุณาระบุเลขที่วิเคราะห์ ";
            }
            else if (data.ANADATE.Year == 1)
            {
                ret = false;
                _error = "กรุณาระบุวันที่วิเคราะห์ ";
            }

            return ret;
        }
        public bool UpdateData(string userID, StockinQCData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                    data.PDORDERLOID= DALObj.PDORDER ;

                    PDOrderDAL PDDAL = new PDOrderDAL();
                    PDDAL.GetDataByLOID(data.PDORDERLOID, null);

                    PDDAL.ANACODE = data.ANACODE;
                    PDDAL.ANADATE = data.ANADATE;

                    if (PDDAL.OnDB)
                        ret = PDDAL.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = PDDAL.InsertCurrentData(userID, obj.zTrans);

                    _LOID = PDOrderDALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(PDOrderDALObj.ErrorMessage);
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



    }
}
