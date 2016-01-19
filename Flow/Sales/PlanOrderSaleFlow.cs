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
    public class PlanOrderSaleFlow
    {
        private PlanForSaleDAL _pDAL;
        private PlanOrderSaleDAL _DAL;
        private PlanOrderDAL _DALitem;
        private string _error = "";
        private double _LOID = 0;

        private PlanForSaleDAL SaleDAL
        {
            get { if (_pDAL == null) { _pDAL = new PlanForSaleDAL(); } return _pDAL; }
        }

        private PlanOrderSaleDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new PlanOrderSaleDAL(); } return _DAL; }
        }

        private PlanOrderDAL OrderDALObj
        {
            get { if (_DALitem == null) { _DALitem = new PlanOrderDAL(); } return _DALitem; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public DataTable GetPLanOrderSaleList(double plan, double product, int month)
        {
            DataTable dt = SaleDAL.GetPlanOrderSaleList(plan, product, month);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["RANK"] = i;
                ++i;
            }
            return dt;
        }

        public DataTable GetPLanOrderSaleListBlank()
        {
            DataTable dt = SaleDAL.GetPlanOrderSaleList(0, 0, 1);
            DataRow dRow = dt.NewRow();
            dt.Rows.Add(dRow);
            return dt;
        }

        public PlanOrderData GetPlanOrderData(double planOrder, int month)
        {
            PlanOrderData data = new PlanOrderData();
            DataTable dt = SaleDAL.GetPlanOrderData(planOrder);
            if (dt.Rows.Count == 1)
            {
                DataRow dRow = dt.Rows[0];
                data.PLAN = Convert.ToDouble(dRow["PLAN"]);
                data.PRODUCTNAME = dRow["PRODUCTNAME"].ToString();
                data.STATUS = dRow["STATUS"].ToString();
                data.UNITNAME = dRow["UNITNAME"].ToString();
                switch (month.ToString())
                {
                    case "1" :
                        data.MONTHNAME = "มกราคม";
                        break;

                    case "2":
                        data.MONTHNAME = "กุมภาพันธ์";
                        break;

                    case "3":
                        data.MONTHNAME = "มีนาคม";
                        break;

                    case "4":
                        data.MONTHNAME = "เมษายน";
                        break;

                    case "5":
                        data.MONTHNAME = "พฤษภาคม";
                        break;

                    case "6":
                        data.MONTHNAME = "มิถุนายน";
                        break;

                    case "7":
                        data.MONTHNAME = "กรกฎาคม";
                        break;

                    case "8":
                        data.MONTHNAME = "สิงหาคม";
                        break;

                    case "9":
                        data.MONTHNAME = "กันยายน";
                        break;

                    case "10":
                        data.MONTHNAME = "ตุลาคม";
                        break;

                    case "11":
                        data.MONTHNAME = "พฤศจิกายน";
                        break;

                    case "12":
                        data.MONTHNAME = "ธันวาคม";
                        break;
                }
            }
            return data;
        }

        private void UpdateData(string userID, int month, ArrayList arrData, OracleTransaction trans)
        {
            bool ret = true;
            double total = 0;
            DALObj.ResetQuantityByPlanAndProductAndMonth(OrderDALObj.PLAN, OrderDALObj.PRODUCT, month, userID, trans);

            for (int i = 0; i < arrData.Count; ++i)
            {
                PlanOrderSaleData data = (PlanOrderSaleData)arrData[i];
                DALObj.OnDB = false;
                DALObj.GetDataByPlanAndProductAndSale(OrderDALObj.PLAN, OrderDALObj.PRODUCT, data.SALEMAN, trans);
                DALObj.PLAN = OrderDALObj.PLAN;
                DALObj.PRODUCT = OrderDALObj.PRODUCT;
                DALObj.PRODUCTMASTER = OrderDALObj.PRODUCTMASTER;
                DALObj.SALEMAN = data.SALEMAN;
                DALObj.UNIT = OrderDALObj.UNIT;
                DALObj.STATUS = OrderDALObj.STATUS;
                total += data.QTY;

                switch (month.ToString())
                {
                    case "1":
                        DALObj.M1 = data.QTY;
                        break;

                    case "2":
                        DALObj.M2 = data.QTY;
                        break;

                    case "3":
                        DALObj.M3 = data.QTY;
                        break;

                    case "4":
                        DALObj.M4 = data.QTY;
                        break;

                    case "5":
                        DALObj.M5 = data.QTY;
                        break;

                    case "6":
                        DALObj.M6 = data.QTY;
                        break;

                    case "7":
                        DALObj.M7 = data.QTY;
                        break;

                    case "8":
                        DALObj.M8 = data.QTY;
                        break;

                    case "9":
                        DALObj.M9 = data.QTY;
                        break;

                    case "10":
                        DALObj.M10 = data.QTY;
                        break;

                    case "11":
                        DALObj.M11 = data.QTY;
                        break;

                    case "12":
                        DALObj.M12 = data.QTY;
                        break;
                }

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, trans);
                else
                    ret = DALObj.InsertCurrentData(userID, trans);

                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            }

            switch (month.ToString())
            {
                case "1":
                    OrderDALObj.M1 = total;
                    break;

                case "2":
                    OrderDALObj.M2 = total;
                    break;

                case "3":
                    OrderDALObj.M3 = total;
                    break;

                case "4":
                    OrderDALObj.M4 = total;
                    break;

                case "5":
                    OrderDALObj.M5 = total;
                    break;

                case "6":
                    OrderDALObj.M6 = total;
                    break;

                case "7":
                    OrderDALObj.M7 = total;
                    break;

                case "8":
                    OrderDALObj.M8 = total;
                    break;

                case "9":
                    OrderDALObj.M9 = total;
                    break;

                case "10":
                    OrderDALObj.M10 = total;
                    break;

                case "11":
                    OrderDALObj.M11 = total;
                    break;

                case "12":
                    OrderDALObj.M12 = total;
                    break;
            }

            ret = OrderDALObj.UpdateCurrentData(userID, trans);
            if (!ret) throw new ApplicationException(OrderDALObj.ErrorMessage);
        }

        private void UpdateDataAll(string userID, ArrayList arrData, OracleTransaction trans)
        {
            bool ret = true;
            double total = 0;
            string exceptLOID = "0";
            //DALObj.ResetQuantityByPlanAndProduct(OrderDALObj.PLAN, OrderDALObj.PRODUCT, userID, trans);

            for (int i = 0; i < arrData.Count; ++i)
            {
                PlanOrderSaleData data = (PlanOrderSaleData)arrData[i];
                DALObj.OnDB = false;
                DALObj.GetDataByPlanAndProductAndSale(OrderDALObj.PLAN, OrderDALObj.PRODUCT, data.SALEMAN, trans);
                DALObj.PLAN = OrderDALObj.PLAN;
                DALObj.PRODUCT = OrderDALObj.PRODUCT;
                DALObj.PRODUCTMASTER = OrderDALObj.PRODUCTMASTER;
                DALObj.SALEMAN = data.SALEMAN;
                DALObj.STATUS = OrderDALObj.STATUS;
                DALObj.UNIT = OrderDALObj.UNIT;
                total += data.QTY;
                if (DateTime.Now.Month <=1) DALObj.M1 = data.QTY;
                if (DateTime.Now.Month <= 2) DALObj.M2 = data.QTY;
                if (DateTime.Now.Month <= 3) DALObj.M3 = data.QTY;
                if (DateTime.Now.Month <= 4) DALObj.M4 = data.QTY;
                if (DateTime.Now.Month <= 5) DALObj.M5 = data.QTY;
                if (DateTime.Now.Month <= 6) DALObj.M6 = data.QTY;
                if (DateTime.Now.Month <= 7) DALObj.M7 = data.QTY;
                if (DateTime.Now.Month <= 8) DALObj.M8 = data.QTY;
                if (DateTime.Now.Month <= 9) DALObj.M9 = data.QTY;
                if (DateTime.Now.Month <= 10) DALObj.M10 = data.QTY;
                if (DateTime.Now.Month <= 11) DALObj.M11 = data.QTY;
                if (DateTime.Now.Month <= 12) DALObj.M12 = data.QTY;

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, trans);
                else
                    ret = DALObj.InsertCurrentData(userID, trans);

                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                exceptLOID += (exceptLOID == "" ? "" : ", ") + DALObj.LOID.ToString();
            }

            ret = DALObj.DeleteDataByPlanAndProductExceptLOID(OrderDALObj.PLAN, OrderDALObj.PRODUCT, exceptLOID, trans);

            if (DateTime.Now.Month <= 1) OrderDALObj.M1 = total;
            if (DateTime.Now.Month <= 2) OrderDALObj.M2 = total;
            if (DateTime.Now.Month <= 3) OrderDALObj.M3 = total;
            if (DateTime.Now.Month <= 4) OrderDALObj.M4 = total;
            if (DateTime.Now.Month <= 5) OrderDALObj.M5 = total;
            if (DateTime.Now.Month <= 6) OrderDALObj.M6 = total;
            if (DateTime.Now.Month <= 7) OrderDALObj.M7 = total;
            if (DateTime.Now.Month <= 8) OrderDALObj.M8 = total;
            if (DateTime.Now.Month <= 9) OrderDALObj.M9 = total;
            if (DateTime.Now.Month <= 10) OrderDALObj.M10 = total;
            if (DateTime.Now.Month <= 11) OrderDALObj.M11 = total;
            if (DateTime.Now.Month <= 12) OrderDALObj.M12 = total;

            ret = OrderDALObj.UpdateCurrentData(userID, trans);
            if (!ret) throw new ApplicationException(OrderDALObj.ErrorMessage);
        }

        public bool UpdateData(string userID, double planOrder, int month, ArrayList arrData, bool copyAll)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                //if (arrData.Count == 0) throw new ApplicationException("กรุณาระบุประมาณการขายของผู้ขาย");
                OrderDALObj.GetDataByLOID(planOrder, obj.zTrans);
                if (OrderDALObj.OnDB)
                {
                    if (OrderDALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        if (copyAll)
                            UpdateDataAll(userID, arrData, obj.zTrans);
                        else
                            UpdateData(userID, month, arrData, obj.zTrans);
                    }
                }
                else
                    throw new ApplicationException("ไม่พบข้อมูลแผนการจำหน่ายสินค้านี้");

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
