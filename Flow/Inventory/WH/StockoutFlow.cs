using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Inventory.WH;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Inventory;
using ABB.Flow.Admin;

namespace ABB.Flow.Inventory.WH
{
    public class StockoutFlow
    {
        string _error = "";
        double _LOID = 0;
        //RequisitionDAL _dal;
        StockOutDAL _dal;
        StockFGDAL search;
        StockOutItemDAL _itemDAL;


        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        //public RequisitionDAL DALObj
        //{
        //    get { if (_dal == null) { _dal = new RequisitionDAL(); } return _dal; }
        //}

        public StockOutDAL DALObj
        {
            get { if (_dal == null) { _dal = new StockOutDAL(); } return _dal; }
        }

        public StockOutItemDAL ItemDALObj
        {
            get { if (_itemDAL == null) { _itemDAL = new StockOutItemDAL(); } return _itemDAL; }
        }

        public StockFGDAL SearchDAL
        {
            get { if (search == null) search = new StockFGDAL(); return search; }
        }

        public DataTable GetProductionList(ProductReserveSearchData data)
        {
            DataTable dt = SearchDAL.GetProductionList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }

        public DataTable GetProductionOtherList(ProductReserveSearchData data)
        {
            DataTable dt = SearchDAL.GetProductionOtherList(data);
            int i = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["NO"] = i;
                i += 1;
            }
            return dt;
        }

        public StockoutWHData GetData(double loid)
        {
            StockoutWHData data = new StockoutWHData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                data.REQUISITIONTYPE = DALObj.DOCTYPE;
                data.REQCODE = GetRequisitionCode(DALObj.REFLOID);
                data.REFLOID = DALObj.REFLOID;
                //data.TOTAL = GetRequisitionTotal(DALObj.REFLOID);
                data.CADDRESS = DALObj.CADDRESS;
                data.CFAX = DALObj.CFAX;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CNAME = DALObj.CNAME;
                data.CODE = DALObj.CODE;
                data.CREATEON = DALObj.CREATEON;
                data.CREATEBY = DALObj.CREATEBY;
                data.CTEL = DALObj.CTEL;
                data.CTITLE = DALObj.CTITLE;
                data.CUSTOMER = DALObj.RECEIVER;
                data.DUEDATE = DALObj.DELIVERYDATE;
                data.INVCODE = DALObj.INVCODE;
                data.REMARK = DALObj.REMARK;
                data.REQDATE = DALObj.REQDATE;
                data.STATUS = DALObj.STATUS;
                data.REFPROD = DALObj.PRODUCTLOID;
                data.REFTABLE = DALObj.PRODUCTREF;

                if (data.REQUISITIONTYPE == Constz.Requisition.RequisitionType.REQ12)
                    data.REQCODE = GetPDOrderCode(DALObj.REFLOID);
                else
                    data.REQCODE = GetRequisitionCode(DALObj.REFLOID);
            }
            return data;
        }

        public StockoutWHData GetProductData(double loid)
        {
            return DALObj.DoGetProduct(loid);
        }

        public StockoutWHData GetReqProductData(double loid)
        {
            return DALObj.DoGetReqProduct(loid);
        }

        //public DataTable GetRequisitionItem(double requisition)
        //{
        //    StockOutItemDAL itemDAL = new StockOutItemDAL();
        //    return SearchDAL.GetPurchaseItemList(requisition);
        //}

        //public DataTable GetRequisitionItemBlank()
        //{
        //    StockOutItemDAL itemDAL = new StockOutItemDAL();
        //    return SearchDAL.GetPurchaseItemListBlank();
        //}

        //public bool ValidateData(ProductReserveData data)
        //{
        //    bool ret = true;
        //    if (data.CUSTOMER == 0)
        //    {
        //        ret = false;
        //        _error = "กรุณาระบุลูกค้า";
        //    }
        //    else if (data.DUEDATE.Year == 1)
        //    {
        //        ret = false;
        //        _error = "กรุณากำหนดวันที่ส่งสินค้า";
        //    }
        //    else if (data.ITEM.Count == 0)
        //    {
        //        ret = false;
        //        _error = "กรุณาระบุรายการสินค้า";
        //    }
        //    return ret;
        //}

        private void UpdateData(string userID, StockoutWHData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            DALObj.GetDataByLOID(data.LOID, zTrans);

            DALObj.ACTIVE = data.ACTIVE;

            DALObj.CADDRESS = data.CADDRESS;
            DALObj.REFTABLE = data.REFTABLE;
            DALObj.CFAX = data.CFAX;
            DALObj.CLASTNAME = data.CLASTNAME;
            DALObj.CNAME = data.CNAME;
            DALObj.CTEL = data.CTEL;
            DALObj.CTITLE = data.CTITLE;
            DALObj.RECEIVER = data.CUSTOMER;
            DALObj.DELIVERYDATE = data.DUEDATE;
            DALObj.REMARK = data.REMARK;
            DALObj.REQDATE = data.REQDATE;
            DALObj.DOCTYPE = data.REQUISITIONTYPE;
            DALObj.STATUS = data.STATUS;
            DALObj.REFLOID = data.REFLOID;
            DALObj.PRODUCTLOID = data.REFPROD;
            DALObj.PRODUCTREF = data.PRODUCTREF;
            DALObj.INVCODE = data.INVCODE;

            if (DALObj.OnDB)
                ret = DALObj.UpdateCurrentData(userID, zTrans);
            else
                ret = DALObj.InsertCurrentData(userID, zTrans);

            _LOID = DALObj.LOID;
            if (!ret)
            {
                throw new ApplicationException(DALObj.ErrorMessage);
            }

            StockOutItemDAL itemDAL = new StockOutItemDAL();
            itemDAL.DeleteDataByStockOut(data.LOID, zTrans);
            for (int i = 0; i < data.STOCKOUTITEM.Rows.Count; i++)
            {
                itemDAL.PRODUCT = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["PRODUCT"]);
                itemDAL.LOTNO = data.STOCKOUTITEM.Rows[i]["LOTNO"].ToString();
                itemDAL.QTY = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["QTY"]);
                itemDAL.REFLOID = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["LOID"]);
                itemDAL.PRICE = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["PRICE"]);
                itemDAL.ACTIVE = Constz.ActiveStatus.Active;
                itemDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                itemDAL.UNIT = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["UNIT"]);
                if (data.REQUISITIONTYPE == Constz.DocType.ReqRawPO.LOID)
                    itemDAL.REFTABLE = "BOM";
                else
                    itemDAL.REFTABLE = "REQMATERIAL";

                itemDAL.STOCKOUT = data.LOID;
                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
            }
        }

        public bool CommitData(string userID, StockoutWHData data)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                UpdateData(userID, data, obj.zTrans);
                DALObj.OnDB = false;

                ret = DALObj.CutStock(_LOID, userID, obj.zTrans);
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

        public bool UpdateData(string userID, StockoutWHData data)
        {
            bool ret = true;
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
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByStockOut(Convert.ToDouble(arrData[i]), obj.zTrans);
                    ret = DALObj.DeleteCurrentData(obj.zTrans);
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

        public bool UpdateStockOutStatus(ArrayList arrData, string status, string userID)
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
                        if (GetStockOutItem(arrData[i].ToString()).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        StockoutWHData data = GetData(Convert.ToDouble(arrData[i]));
                        if (data.CUSTOMER == 0) throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุลูกค้า");
                        if (data.DUEDATE.Year == 1) throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้กำหนดวันที่ส่งสินค้า");
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);

                    if (DALObj.DOCTYPE == Constz.DocType.ReqRawPD.LOID)
                    {
                        ret = DALObj.UpdatePDProductStatus(Convert.ToDouble(DALObj.PRODUCTLOID), Constz.Requisition.Status.RW.Code, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                        else
                        {
                            ret = DALObj.UpdatePDOrderStatus(Convert.ToDouble(DALObj.PRODUCTLOID), Constz.Requisition.Status.RW.Code, userID, obj.zTrans);
                            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        }
                    }
                    DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);

                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    ret = DALObj.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);

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

        public bool UpdateStockOutOtherStatus(ArrayList arrData, string status, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; i++)
                {
                    //if (status == Constz.Requisition.Status.Approved.Code)
                    //{
                    //    if (GetStockOutItem(arrData[i].ToString()).Rows.Count == 0)
                    //    {
                    //        throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                    //    }
                    //    StockoutWHData data = GetData(Convert.ToDouble(arrData[i]));
                    //    if (data.CUSTOMER == 0) throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุลูกค้า");
                    //    if (data.DUEDATE.Year == 1) throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้กำหนดวันที่ส่งสินค้า");
                    //}
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);

                    if (DALObj.DOCTYPE == Constz.DocType.ReqRawPD.LOID)
                    {
                        ret = DALObj.UpdatePDProductStatus(Convert.ToDouble(DALObj.PRODUCTLOID), Constz.Requisition.Status.RW.Code, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                        else
                        {
                            ret = DALObj.UpdatePDOrderStatus(Convert.ToDouble(DALObj.PRODUCTLOID), Constz.Requisition.Status.RW.Code, userID, obj.zTrans);
                            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        }
                    }
                    DALObj.STATUS = Constz.Requisition.Status.Approved.Code;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);

                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    ret = DALObj.CutStock(Convert.ToDouble(arrData[i]), userID, obj.zTrans);

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
        //public bool UpdateRequisitionStatus2(double Loid, string status)
        //{
        //    bool ret = true;
        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {
        //        if (status == Constz.Requisition.Status.Approved.Code)
        //        {
        //            if (GetStockOutItem(Loid.ToString()).Rows.Count == 0)
        //            {
        //                throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
        //            }

        //            ret = DALObj.UpdateRequisitionStatus(Loid, status, obj.zTrans);
        //            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
        //        }
        //        obj.zTrans.Commit();
        //        obj.CloseConnection();
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

        public DataTable GetStockOutItem(string stockout)
        {
            return ItemDALObj.GetStockOutList(stockout);
        }

        public double GetDoctype(string requisitiontype)
        {
            return DALObj.GetDoctype(requisitiontype);
        }

        public double GetRequisitiontype(double doctype)
        {
            return DALObj.GetRequisitiontype(doctype);
        }

        public string GetRequisitionCode(double loid)
        {
            return DALObj.GetRequisitionCode(loid);
        }

        public string GetPDOrderCode(double loid)
        {
            return DALObj.GetPDOrderCode(loid);
        }

        public double GetRequisitionTotal(double loid)
        {
            return DALObj.GetRequisitionTotal(loid);
        }

    }
}

