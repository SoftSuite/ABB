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
using ABB.DAL.Inventory.WH;
using ABB.Flow.Admin;

namespace ABB.Flow.Inventory.WH
{
    public class StockoutWHFlow
    {
        string _error = "";
        double _LOID = 0;
        //RequisitionDAL _dal;
        StockOutDAL _dal;
        StockWHDAL search;
        StockOutItemDAL _itemDAL;
        private StockOutWHItemData _data;
        public StockOutWHItemData ReqItemProductData
        {
            get { if (_data == null) { _data = new StockOutWHItemData(); } return _data; }
        }

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

        public StockWHDAL SearchDAL
        {
            get { if (search == null) search = new StockWHDAL(); return search; }
        }

        public DataTable GetProductionList(ProductReserveSearchData data)
        {
            return SearchDAL.GetProductionList(data);
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
                data.DIVISION = DALObj.DIVISION;
                data.SUPPORTCAUSE = DALObj.SUPPORTCAUSE;
                data.SUPPORTREFCODE = DALObj.SUPPORTREFCODE;


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

        public bool ValidateData(StockoutWHData data)
        {
            bool ret = true;
            if (data.DOCTYPE == Constz.DocType.RetSMaterial.LOID && data.DIVISION == 0)
            {
                ret = false;
                _error = "กรุณาเลือกหน่วยงาน";
            }
            else if (data.DOCTYPE == Constz.DocType.RetSMaterial.LOID && data.SUPPORTCAUSE == "")
            {
                ret = false;
                _error = "กรุณาระบุสาเหตุการสนับสนุน";
            }
            else if (data.REFLOID == 0 && data.DOCTYPE != Constz.DocType.RetSMaterial.LOID)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            else if (data.STOCKOUTITEM.Rows.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการวัตถุดิบ";
            }

            return ret;
        }

        public DataTable GetProductLotWH(double product, double warehouse)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return CompareLot(SearchDAL.GetProductionItemList(product),warehouse);
        }

        public DataTable GetReqProductLotWH(double requisition, double warehouse)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return CompareLot(SearchDAL.GetReqProductionItemList(requisition),warehouse);
        }

        public bool GetReqItemProductData(double requisition, double mainproduct, double product, string type)
        {
            bool ret = true;
            DataTable dt = new DataTable();
            if (type == Constz.DocType.ReqRawPO.LOID.ToString())
            {
                dt = SearchDAL.GetBomItemList(mainproduct, product, "", null);
            }
            else if (type == "24")
            {
                dt = SearchDAL.GetPDItemList(product, "", null);
            }
            else
            {
                dt = SearchDAL.GetRequisitionItemList(requisition, product, "", null);
            }
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

        public bool GetReqItemProductOtherData(double requisition, double mainproduct, double product, string type)
        {
            bool ret = true;
            DataTable dt = new DataTable();
            //if (type == Constz.DocType.ReqRawPO.LOID.ToString())
            //{
            //    dt = SearchDAL.GetBomItemList(mainproduct, product, "", null);
            //}
            //else if (type == "24")
            //{
            //    dt = SearchDAL.GetPDItemList(product, "", null);
            //}
            //else
            //{
            //    dt = SearchDAL.GetRequisitionItemList(requisition, product, "", null);
            //}
            dt = SearchDAL.GetPDOtherItemList(product, "", null);
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
                //ReqItemProductData.REFLOID = Convert.ToDouble(dRow["REFLOID"]);
                ReqItemProductData.UNIT = Convert.ToDouble(dRow["UNIT"]);
                ReqItemProductData.UNITNAME = dRow["UNITNAME"].ToString();
            }
            return ret;
        }

        public bool GetReqItemProductData(double requisition, double mainproduct, string barcode, string type)
        {
            bool ret = true;
            DataTable dt = new DataTable();
            if (type == Constz.DocType.ReqRawPO.LOID.ToString())
            {
                dt = SearchDAL.GetBomItemList(mainproduct, 0, barcode, null);
            }
            else
            {
                dt = SearchDAL.GetRequisitionItemList(requisition, 0, barcode, null);
            }
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

        private DataTable CompareLot(DataTable dtReqItem, double warehouse)
        {
            DataTable dtReqWithLot = new DataTable();
            dtReqWithLot.Columns.Add("LOID", typeof(double));
            dtReqWithLot.Columns.Add("RANK", typeof(double));
            dtReqWithLot.Columns.Add("NO", typeof(double));
            dtReqWithLot.Columns.Add("PRODUCT", typeof(double));
            dtReqWithLot.Columns.Add("PRODUCTNAME", typeof(string));
            dtReqWithLot.Columns.Add("BARCODE", typeof(string));
            dtReqWithLot.Columns.Add("LOTNO", typeof(string));
            dtReqWithLot.Columns.Add("REMAINQTY", typeof(double));
            dtReqWithLot.Columns.Add("QTY", typeof(double));
            dtReqWithLot.Columns.Add("UNIT", typeof(string));
            dtReqWithLot.Columns.Add("UNITNAME", typeof(string));
            dtReqWithLot.Columns.Add("PRICE", typeof(string));
            dtReqWithLot.Columns.Add("NETPRICE", typeof(double));
            dtReqWithLot.Columns.Add("REFLOID", typeof(double));

            int Rank = 0;
            for (int iReq = 0; iReq < dtReqItem.Rows.Count; iReq++)
            {
                double Product = Convert.ToDouble(dtReqItem.Rows[iReq]["PRODUCT"]);
                DataTable dtLot = SearchDAL.GetProductStock(warehouse,Product);
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
                    newRow["NO"] = Rank;
                    newRow["PRODUCT"] = Product;
                    newRow["PRODUCTNAME"] = dtReqItem.Rows[iReq]["PRODUCTNAME"].ToString();
                    newRow["BARCODE"] = dtReqItem.Rows[iReq]["BARCODE"].ToString();
                    newRow["LOTNO"] = dtLot.Rows[iLot]["LOTNO"].ToString();
                    newRow["REMAINQTY"] = ItemDALObj.GetRemainQTYStock(dtLot.Rows[iLot]["LOTNO"].ToString(), Product); 
                    newRow["QTY"] = QTY;
                    newRow["UNIT"] = dtReqItem.Rows[iReq]["UNIT"].ToString();
                    newRow["UNITNAME"] = dtReqItem.Rows[iReq]["UNITNAME"].ToString();
                    newRow["PRICE"] = dtReqItem.Rows[iReq]["PRICE"].ToString();
                    newRow["NETPRICE"] = Convert.ToDouble(dtReqItem.Rows[iReq]["PRICE"]) * QTY;
                    newRow["REFLOID"] = Convert.ToDouble(dtReqItem.Rows[iReq]["REFLOID"]);
                    dtReqWithLot.Rows.Add(newRow);

                    if (allQTY <= 0)
                        break;
                }//for lot
            }//for item

            return dtReqWithLot;
        }

        public DataTable GetReqmaterialItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetReqProductionItemList(requisition);
        }

        public DataTable GetBOMItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetProductionItemList(requisition);
        }

        public DataTable GetPDItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetPDItemList(requisition);
        }
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
            DALObj.SENDER = data.SENDER;
            DALObj.DELIVERYDATE = data.DUEDATE;
            DALObj.REMARK = data.REMARK;
            DALObj.REQDATE = data.REQDATE;
            DALObj.DOCTYPE = data.REQUISITIONTYPE;
            DALObj.STATUS = data.STATUS;
            DALObj.REFLOID = data.REFLOID;
            DALObj.PRODUCTLOID = data.REFPROD;
            DALObj.PRODUCTREF = data.PRODUCTREF;
            DALObj.INVCODE = data.INVCODE;
            DALObj.APPROVER = data.APPROVER;
            DALObj.APPROVEDATE = data.APPROVEDATE;
            DALObj.DIVISION = data.DIVISION;
            DALObj.SUPPORTCAUSE = data.SUPPORTCAUSE;
            DALObj.SUPPORTREFCODE = data.SUPPORTREFCODE;

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
                if (!Convert.IsDBNull(data.STOCKOUTITEM.Rows[i]["LOID"])) itemDAL.REFLOID = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["LOID"]);
                itemDAL.QTY = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["QTY"]);
                itemDAL.PRICE = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["PRICE"]);
                itemDAL.ACTIVE = Constz.ActiveStatus.Active;
                itemDAL.STATUS = Constz.Requisition.Status.Waiting.Code;
                itemDAL.UNIT = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["UNIT"]);
                if (data.REQUISITIONTYPE == Constz.DocType.ReqRawPO.LOID)
                {
                    itemDAL.REFTABLE = "BOM";
                    //itemDAL.REFLOID = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["LOID"]);
                }
                else if (data.REQUISITIONTYPE == Constz.DocType.ReqRawPD.LOID)
                {
                    itemDAL.REFTABLE = "REQMATERIAL";
                    //itemDAL.REFLOID = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["LOID"]);
                }

                itemDAL.STOCKOUT = DALObj.LOID;
                itemDAL.REMAIN = Convert.ToDouble(data.STOCKOUTITEM.Rows[i]["REMAINQTY"]);
                itemDAL.OnDB = false;
                ret = itemDAL.InsertCurrentData(userID, zTrans);
                if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
            }
        }

        public bool CommitData(string userID, StockoutWHData data)
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
                    DALObj.OnDB = false;
                    if (data.REQUISITIONTYPE == Constz.DocType.ReqRawPD.LOID)
                    {
                        ret = SearchDAL.UpdatePDProductStatus(Convert.ToDouble(data.REFPROD), Constz.Requisition.Status.RW.Code, userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                        else
                        {
                            ret = SearchDAL.UpdatePDOrderStatus(Convert.ToDouble(data.REFPROD), Constz.Requisition.Status.RW.Code, userID, obj.zTrans);
                            if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                        }
                    }
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
            }
            else
                ret = false;
            return ret;
        }

        public bool UpdateData(string userID, StockoutWHData data)
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
        public DataTable GetStockOutItemBlank()
        {
            return ItemDALObj.GetStockOutItemListBlank();
        }

        public DataTable GetProductStock(double warehouse, double product)
        {
            return ItemDALObj.GetProductStock(warehouse, product);
        }

        public double GetQTYStock(string lotno, double product)
        {
            return ItemDALObj.GetQTYStock(lotno, product);
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
        public double GetApprover(string userid)
        {
            return SearchDAL.GetApprover(userid);
        }
        public double GetRemainQTYStock(string lotno, double product)
        {
            return ItemDALObj.GetRemainQTYStock(lotno, product);
        }

    }
}

