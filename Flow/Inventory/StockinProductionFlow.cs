using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Data.Admin;
using ABB.Data.Inventory;
using ABB.Data.Inventory.FG;
using ABB.DAL;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;
using ABB.Flow.Sales;

namespace ABB.Flow.Inventory
{
    public class StockinProductionFlow
    {
        string _error = "";
        double _LOID = 0;
        StockInDAL _SIDAL;
        StockFGDAL search;
        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        private StockInDAL SDAL
        {
            get { if (_SIDAL == null) { _SIDAL = new StockInDAL(); } return _SIDAL; }
        }
        public StockFGDAL SearchDAL
        {
            get { if (search == null) search = new StockFGDAL(); return search; }
        }
        public DataTable GetStockInListWH(StockinProductSearchData data)
        {
            string whereString = " RECEIVER = " + data.WAREHOUSE + " AND DOCTYPE=15 ";

            if (data.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE >= '" + OracleDB.QRText(data.CODEFROM.Trim()) + "' ";
            if (data.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE <= '" + OracleDB.QRText(data.CODETO.Trim()) + "' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCTNAME = '" + OracleDB.QRText(data.PRODUCTNAME.Trim()) + "' ";
            if (data.LOTNOFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO >= '" + OracleDB.QRText(data.LOTNOFROM.Trim()) + "' ";
            if (data.LOTNOTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO <= '" + OracleDB.QRText(data.LOTNOTO.Trim()) + "' ";
            if (data.CREATEONFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(data.CREATEONFROM) + " ";
            if (data.CREATEONTO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(data.CREATEONTO) + " ";
            if (data.PRODUCETYPE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCETYPE = '" + OracleDB.QRText(data.PRODUCETYPE.Trim()) + "' ";

            string sql = "select ROWNUM NO, a.* from (SELECT DISTINCT ST.LOID LOID,ST.CODE CODE,ST.CREATEON CREATEON,ST.RECEIVER,PPD.PRODUCETYPE,PPD.LOTNO,PD.NAME PRODUCTNAME,ST.DOCTYPE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM STOCKIN ST INNER JOIN STOCKINITEM SI ON SI.STOCKIN=ST.LOID ";
            sql += "INNER JOIN PDPRODUCT PPD ON PPD.LOID=SI.REFLOID AND SI.REFTABLE ='PDPRODUCT' ";
            sql += "INNER JOIN PRODUCT PD ON PPD.PRODUCT=PD.LOID) a ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }
        public DataTable GetStockInListFG(StockinProductSearchData data)
        {
            string whereString = " RECEIVER = " + data.WAREHOUSE + " AND DOCTYPE=13 ";

            if (data.CODEFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE >= '" + OracleDB.QRText(data.CODEFROM.Trim()) + "' ";
            if (data.CODETO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE <= '" + OracleDB.QRText(data.CODETO.Trim()) + "' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCTNAME = '" + OracleDB.QRText(data.PRODUCTNAME.Trim()) + "' ";
            if (data.LOTNOFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO >= '" + OracleDB.QRText(data.LOTNOFROM.Trim()) + "' ";
            if (data.LOTNOTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO <= '" + OracleDB.QRText(data.LOTNOTO.Trim()) + "' ";
            if (data.CREATEONFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON >= " + OracleDB.QRDate(data.CREATEONFROM) + " ";
            if (data.CREATEONTO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "CREATEON <= " + OracleDB.QRDate(data.CREATEONTO) + " ";
            if (data.PRODUCETYPE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCETYPE = '" + OracleDB.QRText(data.PRODUCETYPE.Trim()) + "' ";

            string sql = "select ROWNUM NO, a.* from (SELECT DISTINCT ST.LOID LOID,ST.CODE CODE,ST.CREATEON CREATEON,ST.RECEIVER,PPD.PRODUCETYPE,PPD.LOTNO,PD.NAME PRODUCTNAME,ST.DOCTYPE, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE ST.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM STOCKIN ST INNER JOIN STOCKINITEM SI ON SI.STOCKIN=ST.LOID ";
            sql += "INNER JOIN PDPRODUCT PPD ON PPD.LOID=SI.REFLOID AND SI.REFTABLE ='PDPRODUCT' ";
            sql += "INNER JOIN PRODUCT PD ON PPD.PRODUCT=PD.LOID) a ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }
        #region Get List For DataGridView
        public DataTable GetPDItem(string LOTNO)
        {
            string sql = "SELECT ROWNUM as LOID, ROWNUM RANK, VB.RWBARCODE,VB.RWNAME,VB.RWGROUPNAME, 0 as MASTER,VB.UNAME, VB.RWLOID as PDLOID, VB.RWUNIT AS UNIT ";
            sql += "FROM V_BOM_LIST VB LEFT JOIN V_PRODUCT_PDPRODUCT VP ON VB.PDLOID=VP.PDLOID ";
            //sql += "LEFT JOIN REQMATERIAL RQM ON RQM.PRODUCT= VP.PDLOID LEFT JOIN REQUISITION RQ ON RQM.REQUISITION=RQ.LOID AND RQ.REQUISITIONTYPE='11'  ";
            sql += "WHERE VB.PDLOTNO = '" + LOTNO + "'";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDItem(double LOID)
        {
            string sql = "SELECT RQM.LOID as LOID, ROWNUM RANK, VR.BARCODE as RWBARCODE, VR.NAME as RWNAME, VR.GNAME as RWGROUPNAME, RQM.MASTER, VR.UNAME, RQM.PRODUCT as PDLOID, VR.PUNIT as UNIT ";
            sql += " FROM REQMATERIAL RQM ";
            sql += " INNER JOIN V_RAW_LIST VR ON VR.LOID = RQM.PRODUCT ";
            sql += " WHERE RQM.REQUISITION = " + LOID.ToString();

            return OracleDB.ExecListCmd(sql);
        }
        #endregion

        #region Get Detaiil of PDReservePage

        public StockinProductData GetAllData(double loid)
        {
            // Get Old Data From Requisition 

            StockinProductData data = new StockinProductData();
            if (SDAL.GetDataByLOID(loid, null))
            {
                //data.STCREATEON = DALObj.;
                //data.CODE = DALObj.CODE;
                //data.CREATEBY = DALObj.CREATEBY;
                //data.STATUS = DALObj.STATUS;
                //data.REMARK = DALObj.REMARK;
                //data.LOTNO = DALObj.LOTNO;
                //data.WAREHOUSE = DALObj.WAREHOUSE;
                //data.LOTNO = DALObj.LOTNO;

                //PDReserveData data2 = GetPDDataFromLOT(data.LOTNO);
                //data.PDBARCODE = data2.PDBARCODE;
                //data.PDNAME = data2.PDNAME;
                //data.BATCHSIZE = data2.BATCHSIZE;
                //data.BATCHSIZEUNITNAME = data2.BATCHSIZEUNITNAME;


            }
            return data;
        }

        public StockinProductData GetPDDataFromLOT(string LotNo)
        {
            // Get Master Data From BOM List..

            StockinProductData data = new StockinProductData();
            //if (SearchDAL.GetDataByLOTNO(LotNo, null))
            //{
            //    data.VPLOID = SearchDAL.PDLOID;
            //    data.LOTNO = SearchDAL.LOTNO;
            //    data.PDBARCODE = SearchDAL.PDBARCODE;
            //    data.PDNAME = SearchDAL.PDNAME;
            //    data.BATCHSIZE = SearchDAL.BATCHSIZE;
            //    data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
            //}

            return data;
        }


        #endregion

        public DataTable GetPDItemBlank()
        {
            string sql = "SELECT 0 LOID, 0 RWBARCODE, 0 RWNAME, 0 RWGROUPNAME, 0 MASTER, 0 UNAME ";
            sql += "FROM DUAL ";
            return OracleDB.ExecListCmd(sql);
        }

        public ProductSearchData GetProductData(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductData(string barcode)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(barcode);
        }

        public StockinProductData GetDataLotNo(string code)
        {
            StockinProductData data = new StockinProductData();
            //if (SDAL.GetDataByLOTNO(code, null))
            //{
            //    data.REQDATE = DALObj.REQDATE;
            //    data.CODE = DALObj.CODE;
            //    data.CREATEBY = DALObj.CREATEBY;
            //    //data.STATUS = DALObj.STATUS;
            //    data.REMARK = DALObj.REMARK;
            //    data.LOTNO = DALObj.LOTNO;
            //    data.WAREHOUSE = DALObj.WAREHOUSE;
            //}
            return data;
        }
        public StockinProductData GetDataLotNo1(string code)
        {
            StockinProductData data = new StockinProductData();
            //if (SearchDAL.GetDataByLOTNO(code, null))
            //{
            //    data.LOTNO = SearchDAL.LOTNO;
            //    data.PDBARCODE = SearchDAL.PDBARCODE;
            //    data.PDNAME = SearchDAL.PDNAME;
            //    data.BATCHSIZE = SearchDAL.BATCHSIZE;
            //    data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
            //}
            return data;
        }



        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        public string GetProduceType(double loid)
        {
            return SearchDAL.GetProduceType(loid);
        }


        public StockinProductData GetData(double loid)
        {
            StockinProductData data = new StockinProductData();
            if (SDAL.GetDataByLOID(loid, null))
            {
                data.LOID = SDAL.LOID;
                data.CODE = SDAL.CODE;
                data.SENDER = SDAL.SENDER;
                data.REMARK = SDAL.REMARK;
                data.STCREATEON = SDAL.CREATEON;
                data.STATUS = SDAL.STATUS;
            }
            return data;
        }

        public StockinProductData GetData1(double loid)
        {
            StockinProductData data = new StockinProductData();
            //if (SearchDAL.GetDataByLOID(loid, null))
            //{
            //    data.LOTNO = SearchDAL.LOTNO;
            //    data.PDBARCODE = SearchDAL.PDBARCODE;
            //    data.PDNAME = SearchDAL.PDNAME;
            //    data.BATCHSIZE = SearchDAL.BATCHSIZE;
            //    data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
            //}
            return data;
        }
        public bool ValidateData(StockinProductData data)
        {
            bool ret = true;
            //if (Convert.ToDouble(data.LOTNO) == 0)
            //{
            //    ret = false;
            //    _error = "กรุณาระบุเลขที่การผลิต";
            //}
            //else if (data.REQDATE.Year == 1)
            //{
            //    ret = false;
            //    _error = "กรุณาวันที่บันทึกรายการ";
            //}
            //else if (data.REASON == "")
            //{
            //    ret = false;
            //    _error = "กรุณาระบุเหตุผลในการขอซื้อ";
            //}
            if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }
        public bool UpdateData(string userID, StockinProductData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    UpdateData(userID, data, obj.zTrans);

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
        // GetReceiveItemList !!!
        public DataTable GetStockInItem(double requisition)
        {
            return SearchDAL.GetProductItemList(requisition);
        }

        public DataTable GetStockInItemBlank()
        {
            StockInItemDAL itemDAL = new StockInItemDAL();
            return SearchDAL.GetProductItemListBlank();
        }
        private void UpdateData(string userID, StockinProductData data, System.Data.OracleClient.OracleTransaction zTrans)
        {

            bool ret = true;
            SDAL.OnDB = false;
            SDAL.GetDataByLOID(data.LOID, zTrans);

            SDAL.SENDER = data.SENDER;
            SDAL.RECEIVER = data.RECEIVER;
            if (SDAL.RECEIVEDATE.Year == 1) SDAL.RECEIVEDATE = DateTime.Today;
            //SDAL.CREATEON = data.STCREATEON;
            SDAL.REMARK = data.REMARK;
            SDAL.STATUS = data.STATUS;
            SDAL.DOCTYPE = data.DOCTYPE;


            if (SDAL.OnDB)
                ret = SDAL.UpdateCurrentData(userID, zTrans);
            else
                ret = SDAL.InsertCurrentData(userID, zTrans);

            _LOID = SDAL.LOID;
            if (!ret)
            {
                throw new ApplicationException(SDAL.ErrorMessage);
            }

            StockInItemDAL itemDAL = new StockInItemDAL();
            itemDAL.DeleteDataByStockIn(data.LOID, zTrans);
            for (Int16 i = 0; i < data.ITEM.Count; ++i)
            {
                StockinProductData item = (StockinProductData)data.ITEM[i];
                itemDAL.PRODUCT = item.PRODUCT;
                itemDAL.QTY = item.QTY;
                itemDAL.STOCKIN = SDAL.LOID;
                itemDAL.REFLOID = item.REFLOID;
                itemDAL.REFTABLE = "PDPRODUCT";
                itemDAL.LOTNO = item.LOTNO;
                itemDAL.ACTIVE = Constz.ActiveStatus.Active;
                itemDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                itemDAL.UNIT = item.UNIT;


                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(SDAL.ErrorMessage);
            }
        }

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                StockInItemDAL itemDAL = new StockInItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    SDAL.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByStockIn(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = SDAL.DeleteCurrentData(obj.zTrans);
                    if (!ret) throw new ApplicationException(SDAL.ErrorMessage);
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

        public bool UpdatePDRequestStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    if (status == Constz.Requisition.Status.Approved.Code)
                    {
                        if (GetPDItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        StockinProductData data = GetData(Convert.ToDouble(arrData[i]));
                        //if (data.REASON == "") throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุเหตุผลในการขอซื้อ");
                    }
                    //ret = DALObj.UpdatePDRequestStatus(Convert.ToDouble(arrData[i]), status, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(SDAL.ErrorMessage);
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
        public SupplierData GetSenderData(double loid)
        {
            StockFGDAL pFlow = new StockFGDAL();
            return pFlow.DoGetSenderData(loid);
        }

        public bool SubmitPDStockin(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; ++i)
                {
                    SDAL.OnDB = false;
                    SDAL.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (SDAL.STATUS == Constz.Requisition.Status.DoWaiting.Code)
                    {
                        SDAL.STATUS = Constz.Requisition.Status.Finish.Code;
                        ret = SDAL.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(SDAL.ErrorMessage);

                        ret = SDAL.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(SDAL.ErrorMessage);
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
        public bool CommitQCData(string userID, StockinProductData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    UpdateData(userID, data, obj.zTrans);

                    ret = SDAL.CutStock(_LOID, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(SDAL.ErrorMessage);

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
        //public bool CommitData(string userID, StockinProductData data)
        //{
        //    bool ret = true;
        //    if (ValidateData(data))
        //    {
        //        OracleDBObj obj = new OracleDBObj();
        //        obj.CreateConnection();
        //        obj.CreateTransaction();
        //        try
        //        {
        //            UpdateData(userID, data);

        //            //ret = SDAL.CutStockRequisition(_LOID, userID, obj.zTrans);
        //            if (!ret) throw new ApplicationException(SDAL.ErrorMessage);

        //            obj.zTrans.Commit();
        //            obj.CloseConnection();
        //        }
        //        catch (Exception ex)
        //        {
        //            obj.zTrans.Rollback();
        //            obj.CloseConnection();
        //            ret = false;
        //            _error = ex.Message;
        //        }
        //    }
        //    else
        //        ret = false;
        //    return ret;
        //}
    }
}

