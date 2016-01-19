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
    public class PlanDetailPopupFlow
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

        public PlanDetailData GetPlanDetailData(double plan, int month, int day, double product)
        {
            PlanDetailData data = new PlanDetailData();
            DataTable dt = PlanInvDAL.GetPlanDetailData(plan, month, day, product);
            if (dt.Rows.Count ==1)
            {
                DataRow dRow = dt.Rows[0];
                if (!Convert.IsDBNull(dRow["MAXIMUM"])) data.MAXIMUM = Convert.ToDouble(dRow["MAXIMUM"]);
                if (!Convert.IsDBNull(dRow["MINIMUM"])) data.MINIMUM = Convert.ToDouble(dRow["MINIMUM"]);
                if (!Convert.IsDBNull(dRow["YEAR"])) data.YEAR = Convert.ToInt32(dRow["YEAR"]);
                if (!Convert.IsDBNull(dRow["MONTH"])) data.MONTH = Convert.ToInt32(dRow["MONTH"]);
                if (!Convert.IsDBNull(dRow["DAY"])) data.DAY = Convert.ToInt32(dRow["DAY"]);
                if (!Convert.IsDBNull(dRow["PDLEADTIME"])) data.PDLEADTIME = Convert.ToDouble(dRow["PDLEADTIME"]);
                if (!Convert.IsDBNull(dRow["PDLOID"])) data.PDLOID = Convert.ToDouble(dRow["PDLOID"]);
                if (!Convert.IsDBNull(dRow["PDLOTSIZE"])) data.PDLOTSIZE = Convert.ToDouble(dRow["PDLOTSIZE"]);
                if (!Convert.IsDBNull(dRow["PDQTY"])) data.PDQTY = Convert.ToDouble(dRow["PDQTY"]);
                if (!Convert.IsDBNull(dRow["PLAN"])) data.PLAN = Convert.ToDouble(dRow["PLAN"]);
                if (!Convert.IsDBNull(dRow["POLEADTIME"])) data.POLEADTIME = Convert.ToDouble(dRow["POLEADTIME"]);
                if (!Convert.IsDBNull(dRow["POLOID"])) data.POLOID = Convert.ToDouble(dRow["POLOID"]);
                if (!Convert.IsDBNull(dRow["POLOTSIZE"])) data.POLOTSIZE = Convert.ToDouble(dRow["POLOTSIZE"]);
                if (!Convert.IsDBNull(dRow["POQTY"])) data.POQTY = Convert.ToDouble(dRow["POQTY"]);
                if (!Convert.IsDBNull(dRow["PRODUCT"])) data.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                if (!Convert.IsDBNull(dRow["PRODUCTNAME"])) data.PRODUCTNAME = dRow["PRODUCTNAME"].ToString();
                if (!Convert.IsDBNull(dRow["UNITNAME"])) data.UNITNAME = dRow["UNITNAME"].ToString();
                if (!Convert.IsDBNull(dRow["STATUS"])) data.STATUS = dRow["STATUS"].ToString();
                if (!Convert.IsDBNull(dRow["RECEIVELOID"])) data.RECEIVELOID = Convert.ToDouble(dRow["RECEIVELOID"]);
            }
            return data;
        }

        public DataTable getMaterialList(double product, double qty)
        {
            DataTable dt = PlanInvDAL.GetMaterialList(product, qty);
            int i = 1;
            foreach(DataRow dRow in dt.Rows)
            {
                dRow["RANK"] = i;
                i += 1;
            }
            return dt;
        }

        public bool UpdateData(string userID, PlanPopupData data)
        {
            bool ret = true;
            double leadtimePO = 0;
            double leadtimePD = 0;
            double produceLOID = 0;
            double purchaseLOID = 0;

            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                PlanProduceDAL produceDAL = new PlanProduceDAL();
                PlanPurchaseDAL purchaseDAL = new PlanPurchaseDAL();
                PlanReceiveDAL receiveDAL = new PlanReceiveDAL();
                PlanReceiveItemDAL receiveItemDAL = new PlanReceiveItemDAL();

                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.PLAN, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                {
                    #region PlanReceive

                    receiveDAL.GetDataByLOID(data.RECEIVELOID, obj.zTrans);
                    switch (data.DAY.ToString())
                    {
                        case "1": receiveDAL.DAY1 = data.PDQTY + data.POQTY; break;
                        case "2": receiveDAL.DAY2 = data.PDQTY + data.POQTY; break;
                        case "3": receiveDAL.DAY3 = data.PDQTY + data.POQTY; break;
                        case "4": receiveDAL.DAY4 = data.PDQTY + data.POQTY; break;
                        case "5": receiveDAL.DAY5 = data.PDQTY + data.POQTY; break;
                        case "6": receiveDAL.DAY6 = data.PDQTY + data.POQTY; break;
                        case "7": receiveDAL.DAY7 = data.PDQTY + data.POQTY; break;
                        case "8": receiveDAL.DAY8 = data.PDQTY + data.POQTY; break;
                        case "9": receiveDAL.DAY9 = data.PDQTY + data.POQTY; break;
                        case "10": receiveDAL.DAY10 = data.PDQTY + data.POQTY; break;
                        case "11": receiveDAL.DAY11 = data.PDQTY + data.POQTY; break;
                        case "12": receiveDAL.DAY12 = data.PDQTY + data.POQTY; break;
                        case "13": receiveDAL.DAY13 = data.PDQTY + data.POQTY; break;
                        case "14": receiveDAL.DAY14 = data.PDQTY + data.POQTY; break;
                        case "15": receiveDAL.DAY15 = data.PDQTY + data.POQTY; break;
                        case "16": receiveDAL.DAY16 = data.PDQTY + data.POQTY; break;
                        case "17": receiveDAL.DAY17 = data.PDQTY + data.POQTY; break;
                        case "18": receiveDAL.DAY18 = data.PDQTY + data.POQTY; break;
                        case "19": receiveDAL.DAY19 = data.PDQTY + data.POQTY; break;
                        case "20": receiveDAL.DAY20 = data.PDQTY + data.POQTY; break;
                        case "21": receiveDAL.DAY21 = data.PDQTY + data.POQTY; break;
                        case "22": receiveDAL.DAY22 = data.PDQTY + data.POQTY; break;
                        case "23": receiveDAL.DAY23 = data.PDQTY + data.POQTY; break;
                        case "24": receiveDAL.DAY24 = data.PDQTY + data.POQTY; break;
                        case "25": receiveDAL.DAY25 = data.PDQTY + data.POQTY; break;
                        case "26": receiveDAL.DAY26 = data.PDQTY + data.POQTY; break;
                        case "27": receiveDAL.DAY27 = data.PDQTY + data.POQTY; break;
                        case "28": receiveDAL.DAY28 = data.PDQTY + data.POQTY; break;
                        case "29": receiveDAL.DAY29 = data.PDQTY + data.POQTY; break;
                        case "30": receiveDAL.DAY30 = data.PDQTY + data.POQTY; break;
                        case "31": receiveDAL.DAY31 = data.PDQTY + data.POQTY; break;
                    }
                    ret = receiveDAL.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(receiveDAL.ErrorMessage);

                    #endregion

                    ProductDAL pDAL = new ProductDAL();
                    pDAL.GetDataByLOID(receiveDAL.PRODUCT, obj.zTrans);
                    leadtimePO = pDAL.LEADTIME;
                    leadtimePD = pDAL.LEADTIMEPD;
                    DateTime purchaseDate = new DateTime(Convert.ToInt32(DALObj.YEAR) - 543, Convert.ToInt32(receiveDAL.MONTH), data.DAY).AddDays(-leadtimePO);
                    DateTime produceDate = new DateTime(Convert.ToInt32(DALObj.YEAR) - 543, Convert.ToInt32(receiveDAL.MONTH), data.DAY).AddDays(-leadtimePD);

                    #region PlanPurchase

                    purchaseDAL.GetData(DALObj.LOID, purchaseDate.Month, receiveDAL.PRODUCT, DALObj.STATUS, obj.zTrans);
                    switch (purchaseDate.Day.ToString())
                    {
                        case "1": purchaseDAL.DAY1 = data.POQTY; break;
                        case "2": purchaseDAL.DAY2 = data.POQTY; break;
                        case "3": purchaseDAL.DAY3 = data.POQTY; break;
                        case "4": purchaseDAL.DAY4 = data.POQTY; break;
                        case "5": purchaseDAL.DAY5 = data.POQTY; break;
                        case "6": purchaseDAL.DAY6 = data.POQTY; break;
                        case "7": purchaseDAL.DAY7 = data.POQTY; break;
                        case "8": purchaseDAL.DAY8 = data.POQTY; break;
                        case "9": purchaseDAL.DAY9 = data.POQTY; break;
                        case "10": purchaseDAL.DAY10 = data.POQTY; break;
                        case "11": purchaseDAL.DAY11 = data.POQTY; break;
                        case "12": purchaseDAL.DAY12 = data.POQTY; break;
                        case "13": purchaseDAL.DAY13 = data.POQTY; break;
                        case "14": purchaseDAL.DAY14 = data.POQTY; break;
                        case "15": purchaseDAL.DAY15 = data.POQTY; break;
                        case "16": purchaseDAL.DAY16 = data.POQTY; break;
                        case "17": purchaseDAL.DAY17 = data.POQTY; break;
                        case "18": purchaseDAL.DAY18 = data.POQTY; break;
                        case "19": purchaseDAL.DAY19 = data.POQTY; break;
                        case "20": purchaseDAL.DAY20 = data.POQTY; break;
                        case "21": purchaseDAL.DAY21 = data.POQTY; break;
                        case "22": purchaseDAL.DAY22 = data.POQTY; break;
                        case "23": purchaseDAL.DAY23 = data.POQTY; break;
                        case "24": purchaseDAL.DAY24 = data.POQTY; break;
                        case "25": purchaseDAL.DAY25 = data.POQTY; break;
                        case "26": purchaseDAL.DAY26 = data.POQTY; break;
                        case "27": purchaseDAL.DAY27 = data.POQTY; break;
                        case "28": purchaseDAL.DAY28 = data.POQTY; break;
                        case "29": purchaseDAL.DAY29 = data.POQTY; break;
                        case "30": purchaseDAL.DAY30 = data.POQTY; break;
                        case "31": purchaseDAL.DAY31 = data.POQTY; break;
                    }
                    ret = purchaseDAL.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(purchaseDAL.ErrorMessage);
                    purchaseLOID = purchaseDAL.LOID;

                    #endregion

                    #region PlanProduce

                    produceDAL.GetData(DALObj.LOID, produceDate.Month, receiveDAL.PRODUCT, DALObj.STATUS, obj.zTrans);
                    switch (produceDate.Day.ToString())
                    {
                        case "1": produceDAL.DAY1 = data.PDQTY; break;
                        case "2": produceDAL.DAY2 = data.PDQTY; break;
                        case "3": produceDAL.DAY3 = data.PDQTY; break;
                        case "4": produceDAL.DAY4 = data.PDQTY; break;
                        case "5": produceDAL.DAY5 = data.PDQTY; break;
                        case "6": produceDAL.DAY6 = data.PDQTY; break;
                        case "7": produceDAL.DAY7 = data.PDQTY; break;
                        case "8": produceDAL.DAY8 = data.PDQTY; break;
                        case "9": produceDAL.DAY9 = data.PDQTY; break;
                        case "10": produceDAL.DAY10 = data.PDQTY; break;
                        case "11": produceDAL.DAY11 = data.PDQTY; break;
                        case "12": produceDAL.DAY12 = data.PDQTY; break;
                        case "13": produceDAL.DAY13 = data.PDQTY; break;
                        case "14": produceDAL.DAY14 = data.PDQTY; break;
                        case "15": produceDAL.DAY15 = data.PDQTY; break;
                        case "16": produceDAL.DAY16 = data.PDQTY; break;
                        case "17": produceDAL.DAY17 = data.PDQTY; break;
                        case "18": produceDAL.DAY18 = data.PDQTY; break;
                        case "19": produceDAL.DAY19 = data.PDQTY; break;
                        case "20": produceDAL.DAY20 = data.PDQTY; break;
                        case "21": produceDAL.DAY21 = data.PDQTY; break;
                        case "22": produceDAL.DAY22 = data.PDQTY; break;
                        case "23": produceDAL.DAY23 = data.PDQTY; break;
                        case "24": produceDAL.DAY24 = data.PDQTY; break;
                        case "25": produceDAL.DAY25 = data.PDQTY; break;
                        case "26": produceDAL.DAY26 = data.PDQTY; break;
                        case "27": produceDAL.DAY27 = data.PDQTY; break;
                        case "28": produceDAL.DAY28 = data.PDQTY; break;
                        case "29": produceDAL.DAY29 = data.PDQTY; break;
                        case "30": produceDAL.DAY30 = data.PDQTY; break;
                        case "31": produceDAL.DAY31 = data.PDQTY; break;
                    }
                    ret = produceDAL.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(produceDAL.ErrorMessage);
                    produceLOID = produceDAL.LOID;

                    #endregion

                    receiveItemDAL.GetData(data.PLAN, receiveDAL.PRODUCT, new DateTime(Convert.ToInt32(DALObj.YEAR) - 543, Convert.ToInt32(receiveDAL.MONTH), data.DAY), obj.zTrans);
                    if (receiveItemDAL.OnDB)
                    {
                        if (receiveItemDAL.PODATE != purchaseDate)
                        {
                            #region PlanPurchase

                            purchaseDAL.OnDB = false;
                            purchaseDAL.GetData(DALObj.LOID, receiveItemDAL.PODATE.Month, receiveDAL.PRODUCT, DALObj.STATUS, obj.zTrans);
                            switch (receiveItemDAL.PODATE.Day.ToString())
                            {
                                case "1": purchaseDAL.DAY1 = 0; break;
                                case "2": purchaseDAL.DAY2 = 0; break;
                                case "3": purchaseDAL.DAY3 = 0; break;
                                case "4": purchaseDAL.DAY4 = 0; break;
                                case "5": purchaseDAL.DAY5 = 0; break;
                                case "6": purchaseDAL.DAY6 = 0; break;
                                case "7": purchaseDAL.DAY7 = 0; break;
                                case "8": purchaseDAL.DAY8 = 0; break;
                                case "9": purchaseDAL.DAY9 = 0; break;
                                case "10": purchaseDAL.DAY10 = 0; break;
                                case "11": purchaseDAL.DAY11 = 0; break;
                                case "12": purchaseDAL.DAY12 = 0; break;
                                case "13": purchaseDAL.DAY13 = 0; break;
                                case "14": purchaseDAL.DAY14 = 0; break;
                                case "15": purchaseDAL.DAY15 = 0; break;
                                case "16": purchaseDAL.DAY16 = 0; break;
                                case "17": purchaseDAL.DAY17 = 0; break;
                                case "18": purchaseDAL.DAY18 = 0; break;
                                case "19": purchaseDAL.DAY19 = 0; break;
                                case "20": purchaseDAL.DAY20 = 0; break;
                                case "21": purchaseDAL.DAY21 = 0; break;
                                case "22": purchaseDAL.DAY22 = 0; break;
                                case "23": purchaseDAL.DAY23 = 0; break;
                                case "24": purchaseDAL.DAY24 = 0; break;
                                case "25": purchaseDAL.DAY25 = 0; break;
                                case "26": purchaseDAL.DAY26 = 0; break;
                                case "27": purchaseDAL.DAY27 = 0; break;
                                case "28": purchaseDAL.DAY28 = 0; break;
                                case "29": purchaseDAL.DAY29 = 0; break;
                                case "30": purchaseDAL.DAY30 = 0; break;
                                case "31": purchaseDAL.DAY31 = 0; break;
                            }
                            if (purchaseDAL.OnDB)
                            {
                                ret = purchaseDAL.UpdateCurrentData(userID, obj.zTrans);
                                if (!ret) throw new ApplicationException(purchaseDAL.ErrorMessage);
                            }

                            #endregion
                        }
                        if (receiveItemDAL.PDDATE != produceDate)
                        {
                            #region PlanProduce

                            produceDAL.OnDB = false;
                            produceDAL.GetData(DALObj.LOID, receiveItemDAL.PDDATE.Month, receiveDAL.PRODUCT, DALObj.STATUS, obj.zTrans);
                            switch (receiveItemDAL.PDDATE.Day.ToString())
                            {
                                case "1": produceDAL.DAY1 = 0; break;
                                case "2": produceDAL.DAY2 = 0; break;
                                case "3": produceDAL.DAY3 = 0; break;
                                case "4": produceDAL.DAY4 = 0; break;
                                case "5": produceDAL.DAY5 = 0; break;
                                case "6": produceDAL.DAY6 = 0; break;
                                case "7": produceDAL.DAY7 = 0; break;
                                case "8": produceDAL.DAY8 = 0; break;
                                case "9": produceDAL.DAY9 = 0; break;
                                case "10": produceDAL.DAY10 = 0; break;
                                case "11": produceDAL.DAY11 = 0; break;
                                case "12": produceDAL.DAY12 = 0; break;
                                case "13": produceDAL.DAY13 = 0; break;
                                case "14": produceDAL.DAY14 = 0; break;
                                case "15": produceDAL.DAY15 = 0; break;
                                case "16": produceDAL.DAY16 = 0; break;
                                case "17": produceDAL.DAY17 = 0; break;
                                case "18": produceDAL.DAY18 = 0; break;
                                case "19": produceDAL.DAY19 = 0; break;
                                case "20": produceDAL.DAY20 = 0; break;
                                case "21": produceDAL.DAY21 = 0; break;
                                case "22": produceDAL.DAY22 = 0; break;
                                case "23": produceDAL.DAY23 = 0; break;
                                case "24": produceDAL.DAY24 = 0; break;
                                case "25": produceDAL.DAY25 = 0; break;
                                case "26": produceDAL.DAY26 = 0; break;
                                case "27": produceDAL.DAY27 = 0; break;
                                case "28": produceDAL.DAY28 = 0; break;
                                case "29": produceDAL.DAY29 = 0; break;
                                case "30": produceDAL.DAY30 = 0; break;
                                case "31": produceDAL.DAY31 = 0; break;
                            }
                            if (produceDAL.OnDB)
                            {
                                ret = produceDAL.UpdateCurrentData(userID, obj.zTrans);
                                if (!ret) throw new ApplicationException(produceDAL.ErrorMessage);
                            }

                            #endregion
                        }
                    }
                    receiveItemDAL.PDDATE = produceDate;
                    receiveItemDAL.PLAN = data.PLAN;
                    receiveItemDAL.PODATE = purchaseDate;
                    receiveItemDAL.PRODUCT = receiveDAL.PRODUCT;
                    receiveItemDAL.PRODUCTMASTER = receiveDAL.PRODUCTMASTER;
                    receiveItemDAL.RECEIVEDATE = new DateTime(Convert.ToInt32(DALObj.YEAR) - 543, Convert.ToInt32(receiveDAL.MONTH), data.DAY);
                    receiveItemDAL.STATUS = DALObj.STATUS;
                    receiveItemDAL.UNIT = receiveDAL.UNIT;
                    receiveItemDAL.POLOID = purchaseLOID;
                    receiveItemDAL.PDLOID = produceLOID;
                    receiveItemDAL.POQTY = data.POQTY;
                    receiveItemDAL.PDQTY = data.PDQTY;

                    if (receiveItemDAL.OnDB)
                    {
                        if (data.PDQTY == 0 && data.POQTY == 0)
                            ret = receiveItemDAL.DeleteCurrentData(obj.zTrans);
                        else
                            ret = receiveItemDAL.UpdateCurrentData(userID, obj.zTrans);
                    }
                    else
                        ret = receiveItemDAL.InsertCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(receiveItemDAL.ErrorMessage);

                    ret = DALObj.CalculateFGPlanRemainByProduct(userID, DALObj.LOID, purchaseDAL.PRODUCT, purchaseDAL.PRODUCTMASTER, obj.zTrans);
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

    }
}
