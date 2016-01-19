using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Sales;
using ABB.Data.Inventory.FG;
using ABB.DAL;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;

namespace ABB.Flow.Inventory.FG
{
    public class StockOutFGFlow
    {
        private string _error = "";
        private double _LOID = 0;
        private RequisitionDAL _reqDAL;
        private StockOutDAL _DAL;
        private StockOutItemDAL _itemDAL;
        private StockFGDAL _sDAL;
        private StockOutFGItemData _data;

        public string ErrorMessage
        {
            get { return _error; }
        }

        public double LOID
        {
            get { return _LOID; }
        }

        public RequisitionDAL ReqDAL
        {
            get { if (_reqDAL == null) { _reqDAL = new RequisitionDAL(); } return _reqDAL; }
        }

        public StockOutDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new StockOutDAL(); } return _DAL; }
        }

        public StockOutItemDAL DALItemObj
        {
            get { if (_itemDAL == null) { _itemDAL = new StockOutItemDAL(); } return _itemDAL; }
        }

        public StockFGDAL StockSearchDAL
        {
            get { if (_sDAL == null) { _sDAL = new StockFGDAL(); } return _sDAL; }
        }

        public StockOutFGItemData ReqItemProductData
        {
            get { if (_data == null) { _data = new StockOutFGItemData(); } return _data; }
        }

        public DataTable GetProductStock(double warehouse, double product)
        {
            return StockSearchDAL.GetProductStock(warehouse, product);
        }

        public DataTable GetStockOutItemList(double stockOut)
        {
            DataTable dt = StockSearchDAL.GetStockOutItemList(stockOut);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }

        public DataTable GetStockOutItemBlank()
        {
            return StockSearchDAL.GetStockOutItemBlank();
        }

        public DataTable GetReserveItemList(double requisition)
        {
            return StockSearchDAL.GetReserveItemList(requisition);
        }

        public bool GetReqItemProductData(double requisition, double product)
        {
            bool ret = true;
            DataTable dt = StockSearchDAL.GetRequisitionItemList(requisition, product, "", null);
            if (dt.Rows.Count == 0)
                ret = false;
            else
            {
                DataRow dRow = dt.Rows[0];
                ReqItemProductData.BARCODE = dRow["BARCODE"].ToString();
                ReqItemProductData.LOTNO = dRow["LOTNO"].ToString();
                ReqItemProductData.PRICE = Convert.ToDouble(dRow["PRICE"]);
                ReqItemProductData.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                ReqItemProductData.QTY = Convert.ToDouble(dRow["QTY"]);
                ReqItemProductData.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                ReqItemProductData.UNIT = Convert.ToDouble(dRow["UNIT"]);
                ReqItemProductData.UNITNAME = dRow["UNITNAME"].ToString();
            }
            return ret;
        }

        public bool GetReqItemProductData(double requisition, string barcode)
        {
            bool ret = true;
            DataTable dt = StockSearchDAL.GetRequisitionItemList(requisition, 0, barcode, null);
            if (dt.Rows.Count == 0)
                ret = false;
            else
            {
                DataRow dRow = dt.Rows[0];
                ReqItemProductData.BARCODE = dRow["BARCODE"].ToString();
                ReqItemProductData.LOTNO = dRow["LOTNO"].ToString();
                ReqItemProductData.PRICE = Convert.ToDouble(dRow["PRICE"]);
                ReqItemProductData.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                ReqItemProductData.QTY = Convert.ToDouble(dRow["QTY"]);
                ReqItemProductData.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                ReqItemProductData.UNIT = Convert.ToDouble(dRow["UNIT"]);
                ReqItemProductData.UNITNAME = dRow["UNITNAME"].ToString();
            }
            return ret;
        }

        public DataTable GetStockOutList(StockOutFGSearchData data)
        {
            DataTable dt = StockSearchDAL.GetStockOutList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }

        public StockOutFGReqData GetcustomerData(double requisition)
        { 
                    StockOutFGReqData data = new StockOutFGReqData();
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ReqDAL.GetDataByLOID(requisition, obj.zTrans);
                data.CADDRESS = ReqDAL.CADDRESS;
                data.CFAX = ReqDAL.CFAX;
                data.CLASTNAME = ReqDAL.CLASTNAME;
                data.CNAME = ReqDAL.CNAME;
                data.CTEL = ReqDAL.CTEL;
                data.CTITLE = ReqDAL.CTITLE;
                data.CUSTOMER = ReqDAL.CUSTOMER;
                data.WAREHOUSE = ReqDAL.WAREHOUSE;
                data.REFWAREHOUSE = ReqDAL.REFWAREHOUSE == 0 ? ReqDAL.WAREHOUSE : ReqDAL.REFWAREHOUSE;
                WarehouseDAL wDAL = new WarehouseDAL();
                wDAL.GetDataByLOID(data.WAREHOUSE, obj.zTrans);
                data.WAREHOUSECODE = wDAL.CODE;
                data.WAREHOUSENAME = wDAL.NAME;
                CustomerDAL cDAL = new CustomerDAL();
                cDAL.GetDataByLOID(data.CUSTOMER, obj.zTrans);
                data.CUSTOMERCODE = cDAL.CODE;
                data.CUSTOMERNAME = (cDAL.NAME + " " + cDAL.LASTNAME).Trim();
                data.REQUISITION = ReqDAL.LOID;
                data.REQUISITIONCODE = ReqDAL.CODE;
                data.REQUISITIONTYPE = ReqDAL.REQUISITIONTYPE;
                data.RESERVEDATE = ReqDAL.RESERVEDATE;
                data.REQDATE = ReqDAL.REQDATE;

            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                _error = ex.Message;
            }
            return data;
        }

        public StockOutFGReqData GetRequisitionData(double requisition)
        {
            StockOutFGReqData data = new StockOutFGReqData();
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ReqDAL.GetDataByLOID(requisition, obj.zTrans);
                data.CADDRESS = ReqDAL.CADDRESS;
                data.CFAX = ReqDAL.CFAX;
                data.CLASTNAME = ReqDAL.CLASTNAME;
                data.CNAME = ReqDAL.CNAME;
                data.CTEL = ReqDAL.CTEL;
                data.CTITLE = ReqDAL.CTITLE;
                data.CUSTOMER = ReqDAL.CUSTOMER;
                data.WAREHOUSE = ReqDAL.WAREHOUSE;
                data.REFWAREHOUSE = ReqDAL.REFWAREHOUSE == 0 ? ReqDAL.WAREHOUSE : ReqDAL.REFWAREHOUSE;
                WarehouseDAL wDAL = new WarehouseDAL();
                wDAL.GetDataByLOID(data.WAREHOUSE, obj.zTrans);
                data.WAREHOUSECODE = wDAL.CODE;
                data.WAREHOUSENAME = wDAL.NAME;
                CustomerDAL cDAL = new CustomerDAL();
                cDAL.GetDataByLOID(data.CUSTOMER, obj.zTrans);
                data.CUSTOMERCODE = cDAL.CODE;
                data.CUSTOMERNAME = (cDAL.NAME + " " + cDAL.LASTNAME).Trim();
                data.REQUISITION = ReqDAL.LOID;
                data.REQUISITIONCODE = ReqDAL.CODE;
                data.REQUISITIONTYPE = ReqDAL.REQUISITIONTYPE;
                data.RESERVEDATE = ReqDAL.RESERVEDATE;
                data.REQDATE = ReqDAL.REQDATE;
                DocTypeDAL dDAL = new DocTypeDAL();
                dDAL.GetDataByRequisitionType(data.REQUISITIONTYPE, obj.zTrans);
                data.DOCTYPE = dDAL.LOID;
                RequisitionItemDAL reqItemDAL = new RequisitionItemDAL();
                DataTable dt = GetProductLot(data.REQUISITION, data.REFWAREHOUSE);
                int i = 1;
                foreach (DataRow dRow in dt.Rows)
                {
                    dRow["NO"] = i;
                    i += 1;
                }
                data.REQUISITIONITEM = dt;

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                _error = ex.Message;
            }
            return data;
        }

        public StockoutFGData GetData(double loid)
        {
            StockoutFGData data = new StockoutFGData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                data.REFLOID = DALObj.REFLOID;
                data.CADDRESS = DALObj.CADDRESS;
                data.CFAX = DALObj.CFAX;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CNAME = DALObj.CNAME;
                data.CODE = DALObj.CODE;
                data.CREATEON = DALObj.CREATEON;
                data.CREATEBY = DALObj.CREATEBY;
                data.CTEL = DALObj.CTEL;
                data.CTITLE = DALObj.CTITLE;
                data.RECEIVER = DALObj.RECEIVER;
                data.DELIVERYDATE = DALObj.DELIVERYDATE;
                data.INVCODE = DALObj.INVCODE;
                data.REMARK = DALObj.REMARK;
                data.REQDATE = DALObj.REQDATE;
                data.DOCTYPE = DALObj.DOCTYPE;
                data.SENDER = DALObj.SENDER;
                data.STATUS = DALObj.STATUS;
                data.TOTAL = DALObj.SumPrice(DALObj.LOID, null);
                StockOutFGReqData reqData = GetcustomerData(data.REFLOID);
                data.RESERVEDATE = reqData.RESERVEDATE;
                data.REQUISITIONCODE = reqData.REQUISITIONCODE;
                data.CUSTOMERCODE = reqData.CUSTOMERCODE;
                data.CUSTOMERNAME = reqData.CUSTOMERNAME;
                data.WAREHOUSECODE = reqData.WAREHOUSECODE;
                data.WAREHOUSENAME = reqData.WAREHOUSENAME;
            }
            return data;
        }

        private bool VerifyData(StockoutFGData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            if (data.REFLOID == 0)
            {
                ret = false;
                _error = "กรุณาเลือกรายการขอเบิก";
            }
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุการสินค้าขอเบิก";
            }
            else if (DALObj.GetStockOutByReference(data.REFLOID, "REQUISITION", data.LOID, zTrans) > 0)
            {
                ret = false;
                _error = "ไม่สามารถอลือกใบคำขอนี้ได้ เนื่องจากสร้างใบเบิกแล้ว";
            }
            return ret;
        }

        private void UpdateData(string userID, StockoutFGData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            if (!VerifyData(data, zTrans)) throw new ApplicationException(_error);
            DALObj.ACTIVE = data.ACTIVE;
            DALObj.CADDRESS = data.CADDRESS;
            DALObj.CFAX = data.CFAX;
            DALObj.CLASTNAME = data.CLASTNAME;
            DALObj.CNAME = data.CNAME;
            DALObj.CTEL = data.CTEL;
            DALObj.CTITLE = data.CTITLE;
            DALObj.RECEIVER = data.RECEIVER;
            DALObj.REMARK = data.REMARK;
            DALObj.REQDATE = data.REQDATE;
            DALObj.DOCTYPE = data.DOCTYPE;
            DALObj.STATUS = data.STATUS;
            DALObj.REFTABLE = data.REFTABLE;
            DALObj.REFLOID = data.REFLOID;
            DALObj.SENDER = data.SENDER;

            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, zTrans);
            else
                ret = DALObj.InsertCurrentData(userID, zTrans);

            _LOID = DALObj.LOID;
            if (!ret)
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }

            DALItemObj.DeleteDataByStockOut(_LOID, zTrans);
            for (int i = 0; i < data.ITEM.Count; i++)
            {
                StockOutItemData itemData = (StockOutItemData)data.ITEM[i];
                _itemDAL = new StockOutItemDAL();
                DALItemObj.ACTIVE = itemData.ACTIVE;
                DALItemObj.LOTNO = itemData.LOTNO;
                DALItemObj.PRICE = itemData.PRICE;
                DALItemObj.PRODUCT = itemData.PRODUCT;
                DALItemObj.QTY = itemData.QTY;
                DALItemObj.REFLOID = itemData.REFLOID;
                DALItemObj.REFTABLE = itemData.REFTABLE;
                DALItemObj.STATUS = itemData.STATUS;
                DALItemObj.STOCKOUT = DALObj.LOID;
                DALItemObj.UNIT = itemData.UNIT;
                DALItemObj.REMAIN = itemData.REMAIN;
                ret = DALItemObj.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);
            }
        }

        public bool CommitData(string userID, StockoutFGData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                _LOID = data.LOID;
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                {
                    if (Convert.ToDouble(OracleDB.ExecSingleCmd("SELECT COUNT(LOID) FROM STOCKOUTITEM WHERE LOTNO IS NULL AND STOCKOUT = " + data.LOID.ToString(), obj.zTrans)) > 0)
                        throw new ApplicationException("มีบางรายการที่ไม่ได้ระบุ lot No.");
                    data.STATUS = Constz.Requisition.Status.Approved.Code;
                    UpdateData(userID, data, obj.zTrans);
                    ret = DALItemObj.UpdateStatusByStockOut(_LOID, data.STATUS, userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);
                    ret = DALObj.CutStock(_LOID, userID, obj.zTrans);
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

        public bool CommitData(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        if (Convert.ToDouble(OracleDB.ExecSingleCmd("SELECT COUNT(LOID) FROM STOCKOUTITEM WHERE LOTNO IS NULL AND STOCKOUT = " + arrData[i].ToString(), obj.zTrans)) > 0)
                            throw new ApplicationException("มีบางรายการที่ไม่ได้ระบุ lot No.");

                        DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        ret = DALItemObj.UpdateStatusByStockOut(_LOID, DALObj.STATUS, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALItemObj.ErrorMessage);

                        ret = DALObj.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

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

        public bool UpdateData(string userID, StockoutFGData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                _LOID = data.LOID;
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code || DALObj.STATUS == "")
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
                StockOutItemDAL itemDAL = new StockOutItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
                    {
                        _itemDAL = new StockOutItemDAL();
                        DALItemObj.DeleteDataByStockOut(Convert.ToDouble(arrData[i]), obj.zTrans);
                        ret = DALObj.DeleteCurrentData(obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
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

        private DataTable GetProductLot(double requisition, double warehouse)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return CompareLot(StockSearchDAL.GetReserveItemList(requisition), warehouse);
        }

        private DataTable CompareLot(DataTable dtReqItem, double warehouse)
        {
            DataTable dtReqWithLot = new DataTable();
            dtReqWithLot.Columns.Add("LOID", typeof(double));
            dtReqWithLot.Columns.Add("RANK", typeof(double));
            dtReqWithLot.Columns.Add("NO", typeof(double));
            dtReqWithLot.Columns.Add("PRODUCT", typeof(string));
            dtReqWithLot.Columns.Add("PRODUCTNAME", typeof(string));
            dtReqWithLot.Columns.Add("BARCODE", typeof(string));
            dtReqWithLot.Columns.Add("LOTNO", typeof(string));
            dtReqWithLot.Columns.Add("REMAINQTY", typeof(double));
            dtReqWithLot.Columns.Add("QTY", typeof(double));
            dtReqWithLot.Columns.Add("UNIT", typeof(string));
            dtReqWithLot.Columns.Add("UNITNAME", typeof(string));
            dtReqWithLot.Columns.Add("PRICE", typeof(string));
            dtReqWithLot.Columns.Add("REFLOID", typeof(double));

            int Rank = 0;
            for (int iReq = 0; iReq < dtReqItem.Rows.Count; iReq++)
            {
                double Product = Convert.ToDouble(dtReqItem.Rows[iReq]["PRODUCT"]);
                DataTable dtLot = StockSearchDAL.GetProductStock(warehouse, Product);
                double allQTY = Convert.ToDouble(dtReqItem.Rows[iReq]["QTY"]);
                for (int iLot = 0; iLot < dtLot.Rows.Count; iLot++)
                {
                    double QTY = Convert.ToDouble(dtLot.Rows[iLot]["QTY"]);
                    if (QTY <= allQTY)
                        allQTY = allQTY - QTY;
                    else
                    {
                        QTY = allQTY;
                        allQTY = 0;
                    }

                    DataRow newRow = dtReqWithLot.NewRow();
                    newRow["LOID"] = Convert.ToDouble(dtReqItem.Rows[iReq]["LOID"]);
                    Rank = Rank + 1;
                    newRow["RANK"] = Rank;
                    newRow["NO"] = 1;
                    newRow["PRODUCT"] = Product;
                    newRow["PRODUCTNAME"] = dtReqItem.Rows[iReq]["PRODUCTNAME"].ToString();
                    newRow["BARCODE"] = dtReqItem.Rows[iReq]["BARCODE"].ToString();
                    newRow["LOTNO"] = dtLot.Rows[iLot]["LOTNO"].ToString();
                    newRow["REMAINQTY"] = DALItemObj.GetRemainQTYStockFG(dtLot.Rows[iLot]["LOTNO"].ToString(), Product);
                    newRow["QTY"] = QTY;
                    newRow["UNIT"] = dtReqItem.Rows[iReq]["UNIT"].ToString();
                    newRow["UNITNAME"] = dtReqItem.Rows[iReq]["UNITNAME"].ToString();
                    newRow["PRICE"] = dtReqItem.Rows[iReq]["PRICE"].ToString();
                    newRow["REFLOID"] = Convert.ToDouble(dtReqItem.Rows[iReq]["REFLOID"]);
                    dtReqWithLot.Rows.Add(newRow);

                    if (allQTY <= 0)
                        break;
                }//for lot
            }//for item

            return dtReqWithLot;
        }

        public double GetQTYStock(string lotno, double product)
        {
            return DALItemObj.GetQTYStockFG(lotno, product);
        }
        public double GetRemainQTYStock(string lotno, double product)
        {
            return DALItemObj.GetRemainQTYStockFG(lotno, product);
        }
    }

}
