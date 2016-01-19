using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Flow.Admin;

namespace ABB.Flow.Sales
{
    public class ProductReserveFlow
    {
        string _error = "";
        double _LOID = 0;
        RequisitionDAL _dal;
        PurchaseDAL search;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public RequisitionDAL DALObj
        {
            get { if (_dal == null) { _dal = new RequisitionDAL(); } return _dal; }
        }

        public PurchaseDAL SearchDAL
        {
            get { if (search == null) search = new PurchaseDAL(); return search; }
        }

        public DataTable GetRequisitionList(ProductReserveSearchData data, string sortField)
        {
            DataTable dt = SearchDAL.GetReserveList(data, sortField);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetProductStockList(double product)
        {
            return SearchDAL.GetProductStockList(product);
        }

        public ProductReserveData GetData(double loid)
        {
            ProductReserveData data = new ProductReserveData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.ACTIVE = DALObj.ACTIVE;
                data.CADDRESS = DALObj.CADDRESS;
                data.CEMAIL = DALObj.CEMAIL;
                data.CFAX = DALObj.CFAX;
                data.CLASTNAME = DALObj.CLASTNAME;
                data.CNAME = DALObj.CNAME;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.CTEL = DALObj.CTEL;
                data.CTITLE = DALObj.CTITLE;
                data.CUSTOMER = DALObj.CUSTOMER;
                data.DUEDATE = DALObj.DUEDATE;
                data.REQDATE = DALObj.REQDATE;
                data.GRANDTOT = DALObj.GRANDTOT;
                data.REMARK = DALObj.REMARK;
                data.RESERVEDATE = DALObj.RESERVEDATE;
                data.REQUISITIONTYPE = DALObj.REQUISITIONTYPE;
                data.STATUS = DALObj.STATUS;
                data.TOTAL = DALObj.TOTAL;
                data.TOTDIS = DALObj.TOTDIS;
                data.TOTVAT = DALObj.TOTVAT;
                data.VAT = DALObj.VAT;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.INVCODE = DALObj.INVCODE;
            }
            return data;
        }

        public DataTable GetRequisitionItem(double requisition,double warehouse)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            DataTable dt = SearchDAL.GetReserveItemList(requisition, warehouse);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["RANK"] = (i + 1);
            }
            return dt;
        }

        public DataTable GetRequisitionItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetReserveItemList(requisition);
        }

        public DataTable GetRequisitionReturnItem(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetReserveItemList(requisition);
        }

        public DataTable GetProductLot(double requisition, double warehouse)
        {
            return CompareLot(GetRequisitionItem(requisition, warehouse));
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

        public DataTable GetProductLotWH(double product)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return CompareLot(SearchDAL.GetProductionItemList(product));
        }

        public DataTable GetReqProductLotWH(double requisition)
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return CompareLot(SearchDAL.GetReqProductionItemList(requisition));
        }

        public DataTable GetProductStockFG(double product, double warehouse)
        {
            return SearchDAL.GetProductStockListFG(product, warehouse);
        }

        public string GetInvNo(string lotno, double warehouse, string product)
        {
            return SearchDAL.DoGetInvNo(lotno, warehouse, product);
        }

        public string GetPrice(string lotno, double warehouse, string product)
        {
            return SearchDAL.DoGetPrice(lotno, warehouse, product);
        }

        public string GetInvNoFG(string lotno, double warehouse, string product)
        {
            return SearchDAL.DoGetInvNoFG(lotno, warehouse, product);
        }
        public string GetPriceFG(string lotno, double warehouse, string product)
        {
            return SearchDAL.DoGetPriceFG(lotno, warehouse, product);
        }

        public DataTable GetProductStock(double product, double warehouse)
        {
            return SearchDAL.GetProductStockList(product, warehouse);
        }

        private DataTable CompareLot(DataTable dtReqItem)
        {
            DataTable dtReqWithLot = new DataTable();
            dtReqWithLot.Columns.Add("LOID", typeof(double));
            dtReqWithLot.Columns.Add("RANK", typeof(double));
            dtReqWithLot.Columns.Add("PRODUCT", typeof(double));
            dtReqWithLot.Columns.Add("BARCODE", typeof(string));
            dtReqWithLot.Columns.Add("LOTNO", typeof(string));
            dtReqWithLot.Columns.Add("QTY", typeof(double));
            dtReqWithLot.Columns.Add("UNIT", typeof(string));
            dtReqWithLot.Columns.Add("PRICE", typeof(string));

            int Rank = 0;
            for (int iReq = 0; iReq < dtReqItem.Rows.Count; iReq++)
            {
                double Product = Convert.ToDouble(dtReqItem.Rows[iReq]["PRODUCT"]);
                DataTable dtLot = SearchDAL.GetProductStockList(Product);
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
                    newRow["PRODUCT"] = Product;
                    newRow["BARCODE"] = dtReqItem.Rows[iReq]["BARCODE"].ToString();
                    newRow["LOTNO"] = dtLot.Rows[iLot]["LOTNO"].ToString();
                    newRow["QTY"] = QTY;
                    newRow["UNIT"] = dtReqItem.Rows[iReq]["UNIT"].ToString();
                    newRow["PRICE"] = dtReqItem.Rows[iReq]["PRICE"].ToString();
                    dtReqWithLot.Rows.Add(newRow);

                    if (allQTY <= 0)
                        break;
                }//for lot
            }//for item

            return dtReqWithLot;
        }

        public DataTable GetRequisitionItemBlank()
        {
            RequisitionItemDAL itemDAL = new RequisitionItemDAL();
            return SearchDAL.GetReserveItemListBlank();
        }

        public bool ValidateData(ProductReserveData data)
        {
            bool ret = true;
            if (data.CUSTOMER == 0)
            {
                ret = false;
                _error = "��س��к��١���";
            }
            else if (data.DUEDATE.Year == 1)
            {
                ret = false;
                _error = "��سҡ�˹��ѹ������Թ���";
            }
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "��س��к���¡���Թ���";
            }
            return ret;
        }

        private void UpdateData(string userID, ProductReserveData data, System.Data.OracleClient.OracleTransaction zTrans)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(data.LOID, zTrans);

                DALObj.ACTIVE = data.ACTIVE;
                DALObj.CADDRESS = data.CADDRESS;
                DALObj.CEMAIL = data.CEMAIL;
                DALObj.CFAX = data.CFAX;
                DALObj.CLASTNAME = data.CLASTNAME;
                DALObj.CNAME = data.CNAME;
                DALObj.CODE = data.CODE;
                DALObj.CTEL = data.CTEL;
                DALObj.CTITLE = data.CTITLE;
                DALObj.CUSTOMER = data.CUSTOMER;
                DALObj.DUEDATE = data.DUEDATE;
                DALObj.GRANDTOT = data.GRANDTOT;
                DALObj.REMARK = data.REMARK;
                DALObj.RESERVEDATE = data.RESERVEDATE;
                DALObj.REQUISITIONTYPE = data.REQUISITIONTYPE;
                DALObj.STATUS = data.STATUS;
                DALObj.TOTAL = data.TOTAL;
                DALObj.TOTDIS = data.TOTDIS;
                DALObj.TOTVAT = data.TOTVAT;
                DALObj.VAT = data.VAT;
                DALObj.WAREHOUSE = data.WAREHOUSE;
                DALObj.REFTYPELOID = data.REQUISITIONTYPE;
                DALObj.REFTYPETABLE = "REQUISITIONTYPE";

                if (DALObj.OnDB)
                    ret = DALObj.UpdateCurrentData(userID, zTrans);
                else
                    ret = DALObj.InsertCurrentData(userID, zTrans);

                _LOID = DALObj.LOID;
                if (!ret)
                {
                    throw new ApplicationException(DALObj.ErrorMessage);
                }

                RequisitionItemDAL itemDAL = new RequisitionItemDAL();
                itemDAL.DeleteDataByRequisition(data.LOID, zTrans);
                for (Int16 i = 0; i < data.ITEM.Count; ++i)
                {
                    RequisitionItemData item = (RequisitionItemData)data.ITEM[i];
                    itemDAL.ACTIVE = item.ACTIVE;
                    itemDAL.DISCOUNT = item.DISCOUNT;
                    itemDAL.DUEDATE = item.DUEDATE;
                    itemDAL.NETPRICE = item.NETPRICE;
                    itemDAL.PRODUCT = item.PRODUCT;
                    itemDAL.QTY = item.QTY;
                    itemDAL.PRICE = item.PRICE;
                    itemDAL.REQUISITION = DALObj.LOID;
                    itemDAL.UNIT = item.UNIT;
                    itemDAL.DUEDATE = data.DUEDATE;
                    itemDAL.ISVAT = item.ISVAT;

                    itemDAL.OnDB = false;
                    ret = itemDAL.InsertCurrentData(userID, zTrans);
                    if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
                }
            }
        }           

        public bool CommitData(string userID, ProductReserveData data)
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
                    CommitData(_LOID, userID, obj.zTrans);

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

        public bool UpdateData(string userID, ProductReserveData data)
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
                RequisitionItemDAL itemDAL = new RequisitionItemDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    if (DALObj.STATUS != Constz.Requisition.Status.Approved.Code)
                    {
                        itemDAL.DeleteDataByRequisition(Convert.ToDouble(arrData[i]), obj.zTrans);
                        ret = DALObj.DeleteCurrentData(obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                    }
                    else throw new ApplicationException("�������öź��¡���Ţ��� " + DALObj.CODE + " ���ͧ�ҡ�����ʶҹ�͹��ѵ���¡������");
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

        public bool CopyRequisition(string userID, double loidSource)
        {
            ProductReserveData data = GetData(loidSource);
            data.CODE = "";
            DataTable itemList = GetRequisitionItem(data.LOID, data.WAREHOUSE);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                RequisitionItemData idata = new RequisitionItemData();
                idata.ACTIVE = dRow["ACTIVE"].ToString();
                idata.DISCOUNT = Convert.ToDouble(dRow["DISCOUNT"]);
                idata.DUEDATE = data.DUEDATE;
                idata.NETPRICE = Convert.ToDouble(dRow["NETPRICE"]);
                idata.PRICE = Convert.ToDouble(dRow["PRICE"]);
                idata.PRODUCT = Convert.ToDouble(dRow["PRODUCT"]);
                idata.QTY = Convert.ToDouble(dRow["QTY"]);
                idata.UNIT = Convert.ToDouble(dRow["UNIT"]);
                idata.ISVAT = dRow["ISVAT"].ToString();
                arr.Add(idata);
            }
            data.ITEM = arr;
            DALObj.OnDB = false;
            data.LOID = 0;
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            data.ACTIVE = Constz.ActiveStatus.Active;
            return UpdateData(userID, data);
        }

        private void CommitData(double requisition, string userID, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = true;
            DALObj.OnDB = false;
            DALObj.GetDataByLOID(requisition, trans);
            if (DALObj.STATUS == Constz.Requisition.Status.Waiting.Code)
            {
                if (DALObj.REQUISITIONTYPE == Constz.Requisition.RequisitionType.REQ01)
                    DALObj.STATUS = Constz.Requisition.Status.Reserve.Code;
                else
                    DALObj.STATUS = Constz.Requisition.Status.Approved.Code;

                ret = DALObj.UpdateCurrentData(userID, trans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                ret = DALObj.CutStockRequisition(requisition, userID, trans);
                if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
            }
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
                    CommitData(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
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

        public bool CancelData(double requisition, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                DALObj.OnDB = false;
                DALObj.GetDataByLOID(requisition, obj.zTrans);
                StockOutDAL sDAL = new StockOutDAL();

                if (DALObj.STATUS == Constz.Requisition.Status.Approved.Code)
                {
                    if (sDAL.TotalReference("REQUISITION", requisition, obj.zTrans) > 0)
                        throw new ApplicationException("�������ö¡��ԡ��¡���� ���ͧ�ҡ��¡�ù��١��ҧ�ԧ㹡�÷���¡�õ������");
                    else
                    {
                        DALObj.STATUS = Constz.Requisition.Status.Void.Code;

                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(DALObj.ErrorMessage);
                        ret = DALObj.CutStockRequisition(requisition, userID, obj.zTrans);
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

        public ProductSearchData GetProductData(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductBarcode(double loid)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(loid);
        }

        public ProductSearchData GetProductData(string barcode)
        {
            ProductFlow pFlow = new ProductFlow();
            return pFlow.GetData(barcode);
        }

        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        public MemberTypeData GetMemberTypeData(double loid)
        {
            MemberTypeFlow flow = new MemberTypeFlow();
            return flow.GetData(loid);
        }

        public TitleData GetTitleData(double loid)
        {
            TitleFlow flow = new TitleFlow();
            return flow.GetData(loid);
        }

        public CustomerData GetCustomerData(double loid)
        {
            CustomerDAL dal = new CustomerDAL();
            CustomerData data = new CustomerData();
            dal.GetDataByLOID(loid, null);
            data.ACTIVE = dal.ACTIVE;
            data.BILLADDRESS = dal.BILLADDRESS;
            data.BILLAMPHUR = dal.BILLAMPHUR;
            data.BILLEMAIL = dal.BILLEMAIL;
            data.BILLFAX = dal.BILLFAX;
            data.BILLPROVINCE = dal.BILLPROVINCE;
            data.BILLROAD = dal.BILLROAD;
            data.BILLTAMBOL = dal.BILLTAMBOL;
            data.BILLTEL = dal.BILLTEL;
            data.BILLZIPCODE = dal.BILLZIPCODE;
            data.CADDRESS = dal.CADDRESS;
            data.CAMPHUR = dal.CAMPHUR;
            data.CEMAIL = dal.CEMAIL;
            data.CFAX = dal.CFAX;
            data.CLASTNAME = dal.CLASTNAME;
            data.CMOBILE = dal.CMOBILE;
            data.CNAME = dal.CNAME;
            data.CODE = dal.CODE;
            data.CPROVINCE = dal.CPROVINCE;
            data.CREDITAMOUNT = dal.CREDITAMOUNT;
            data.CREDITDAY = dal.CREDITDAY;
            data.CROAD = dal.CROAD;
            data.CTAMBOL = dal.CTAMBOL;
            data.CTEL = dal.CTEL;
            data.CTITLE = dal.CTITLE;
            data.CUSTOMERTYPE = dal.CUSTOMERTYPE;
            data.CZIPCODE = dal.CZIPCODE;
            data.DELIVERTYPE = dal.DELIVERTYPE;
            data.EFDATE = dal.EFDATE;
            data.EPDATE = dal.EPDATE;
            data.IDENTITY = dal.IDENTITY;
            data.LASTNAME = dal.LASTNAME;
            data.MEMBERTYPE = dal.MEMBERTYPE;
            data.NAME = dal.NAME;
            data.PAYMENT = dal.PAYMENT;
            data.REMARK = dal.REMARK;
            data.SENDADDRESS = dal.SENDADDRESS;
            data.SENDAMPHUR = dal.SENDAMPHUR;
            data.SENDEMAIL = dal.SENDEMAIL;
            data.SENDFAX = dal.SENDFAX;
            data.SENDPLACE = dal.SENDPLACE;
            data.SENDPROVINCE = dal.SENDPROVINCE;
            data.SENDROAD = dal.SENDROAD;
            data.SENDTAMBOL = dal.SENDTAMBOL;
            data.SENDTEL = dal.SENDTEL;
            data.SENDZIPCODE = dal.SENDZIPCODE;
            data.TITLE = dal.TITLE;

            return data;
        }

    }
}
