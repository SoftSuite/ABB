using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Production;
using ABB.Data.Sales;
using ABB.DAL;
using ABB.DAL.Production;
using ABB.Flow.Admin;
using ABB.Flow.Sales;
using ABB.Flow.Production;

namespace ABB.Flow.Production
{
    public class ReturnRequestFlow
    {
        string _error = "";
        double _LOID = 0;
        ReturnRequestDAL _dal;
        PopupProductDAL search;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public ReturnRequestDAL DALObj
        {
            get { if (_dal == null) { _dal = new ReturnRequestDAL(); } return _dal; }
        }
        public PopupProductDAL SearchDAL
        {
            get { if (search == null) search = new PopupProductDAL(); return search; }
        }
        public DataTable GetReturnList(ReturnRequestSearchData data)
        {
            string whereString = "";

            if (data.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE = '" + OracleDB.QRText(data.CODE.Trim()) + "' ";
            if (data.REQDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE >= " + OracleDB.QRDate(data.REQDATEFROM) + " ";
            if (data.REQDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE <= " + OracleDB.QRDate(data.REQDATETO) + " ";
            if (data.MFGDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE >= " + OracleDB.QRDate(data.MFGDATEFROM) + " ";
            if (data.MFGDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE <= " + OracleDB.QRDate(data.MFGDATETO) + " ";
            if (data.PRODUCT.Trim() != "0")
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = '" + OracleDB.QRText(data.PRODUCT.Trim()) + "' ";
            if (data.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(data.LOTNO.Trim()) + "' ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

            string sql = "select * from (SELECT ROWNUM NO, RQ.LOID, RQ.CODE, RQ.REQDATE,RQ.REQUISITIONTYPE, VP.PDLOID PRODUCT, VP.PDNAME, VP.LOTNO,VP.BATCHSIZE,VP.BATCHSIZEUNITNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '' END AS RANK, VP.MFGDATE ";
            sql += "FROM REQUISITION RQ INNER JOIN V_PRODUCT_PDPRODUCT VP ON RQ.REFTABLE='PDPRODUCT' ";
            sql += "AND RQ.REFLOID=VP.PDPLOID ";
            sql += "INNER JOIN REQUISITIONTYPE RT ON RQ.REQUISITIONTYPE=RT.LOID AND RT.LOID=14) ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY CODE ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            for (int i = 0; i < dt.Rows.Count; ++i)
            {
                dt.Rows[i]["NO"] = i + 1;
            }
            return dt;
        }

        public DataTable GetPopUpList(ReturnRequestData data)
        {
            string whereString = "";

           if (data.PRODUCT.Trim() != "0")
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = '" + OracleDB.QRText(data.PRODUCT.Trim()) + "' ";
            if (data.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(data.LOTNO.Trim()) + "' ";

            string sql = "select * from (SELECT ROWNUM NO, PDP.LOID, PD.LOID PRODUCT, PD.BARCODE, PD.NAME PRODUCTNAME, PDP.LOTNO ";
            sql += "FROM PDPRODUCT PDP ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID WHERE PDP.PRODSTATUS IN ('RD','RR','QB','QC','QS','AP')) ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            //for (int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    dt.Rows[i]["NO"] = i + 1;
            //}
            return dt;
        }

        #region Get List For DataGridView
        public DataTable GetRQMItem(double requisition)
        {
            string sql = "SELECT RQM.LOID, ROWNUM RANK,PD.LOID PRODUCT, PD.NAME PRODUCTNAME,RQM.MASTER, RQM.USEQTY,RQM.WASTEQTYMAT,RQM.RETURNQTY,RQM.UNIT ";
            sql += "FROM REQMATERIAL RQM INNER JOIN PRODUCT PD ON RQM.PRODUCT=PD.LOID ";
            //sql += "LEFT JOIN REQMATERIAL RQM ON RQM.PRODUCT= VP.PDLOID LEFT JOIN REQUISITION RQ ON RQM.REQUISITION=RQ.LOID AND RQ.REQUISITIONTYPE='11'  ";
            sql += "WHERE RQM.REQUISITION = '" + requisition + "'";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDItem(double pdploid)
        {
            string sql = "SELECT VR.PDMLOID LOID, ROWNUM RANK,VR.PDMLOID PRODUCT, VR.PDMNAME PRODUCTNAME,VR.MASTER, VR.USEQTY,VR.WASTEQTYMAT,0 RETURNQTY,VR.UNMLOID UNIT ";
            sql += "FROM V_REQMATERIAL VR ";
            sql += "WHERE VR.LOID = '" + pdploid + "'";
            return OracleDB.ExecListCmd(sql);
        }
        #endregion

        #region Get Detaiil of PDReservePage

        public ReturnRequestData GetAllData(double loid)
        {
            // Get Old Data From Requisition 

            ReturnRequestData data = new ReturnRequestData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.REQDATE = DALObj.REQDATE;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.STATUS = DALObj.STATUS;
                data.REMARK = DALObj.REMARK;
                data.LOTNO = DALObj.LOTNO;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.LOTNO = DALObj.LOTNO;
                data.REFLOID = DALObj.REFLOID;
                data.LOID = DALObj.LOID;
                //ReturnRequestData data2 = GetPDDataFromLOT(data.LOTNO);
                //data.PDBARCODE = data2.PDBARCODE;
                //data.PDNAME = data2.PDNAME;
                //data.BATCHSIZE = data2.BATCHSIZE;
                //data.BATCHSIZEUNITNAME = data2.BATCHSIZEUNITNAME;


            }
            return data;
        }

        public PDReserveData GetPDDataFromLOT(string LotNo)
        {
            // Get Master Data From BOM List..

            PDReserveData data = new PDReserveData();
            if (_dal.GetDataByLOTNO(LotNo, null))
            {
                data.VPLOID = SearchDAL.PDLOID;
                data.LOTNO = SearchDAL.LOTNO;
                data.PDBARCODE = SearchDAL.PDBARCODE;
                data.PDNAME = SearchDAL.PDNAME;
                data.BATCHSIZE = SearchDAL.BATCHSIZE;
                data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
            }

            return data;
        }


        #endregion

        public DataTable GetPDItemBlank()
        {
            string sql = "SELECT 0 LOID, 0 PDLOID, 0 MASTER, 0 USEQTY, 0 WASTEQTYMAT, 0 RETURNQTY, '' PRODUCTNAME ";
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

        public DataTable GetPDReserveItem(double requisition)
        {
            ProductReserveItemDAL itemDAL = new ProductReserveItemDAL();
            return GetPDItem(requisition);
        }

        public DataTable GetPDReserveItemBlank()
        {
            ProductReserveItemDAL itemDAL = new ProductReserveItemDAL();
            return GetPDItemBlank();
        }

        public PDReserveData GetDataLotNo(string code)
        {
            PDReserveData data = new PDReserveData();
            if (DALObj.GetDataByLOTNO(code, null))
            {
                data.REQDATE = DALObj.REQDATE;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                //data.STATUS = DALObj.STATUS;
                data.REMARK = DALObj.REMARK;
                data.LOTNO = DALObj.LOTNO;
                data.WAREHOUSE = DALObj.WAREHOUSE;
            }
            return data;
        }
        public PDReserveData GetDataLotNo1(string code)
        {
            PDReserveData data = new PDReserveData();
            if (SearchDAL.GetDataByLOTNO(code, null))
            {
                data.LOTNO = SearchDAL.LOTNO;
                data.PDBARCODE = SearchDAL.PDBARCODE;
                data.PDNAME = SearchDAL.PDNAME;
                data.BATCHSIZE = SearchDAL.BATCHSIZE;
                data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
            }
            return data;
        }

        public ReturnRequestData GetRefData(double loid)
        {
            return DALObj.DoGetRefData(loid);
        }

        public UnitSearchData GetUnitData(double loid)
        {
            UnitFlow uFlow = new UnitFlow();
            return uFlow.GetData(loid);
        }

        //public OfficerData GetOfficerData(double loid)
        //{
        //    OfficerDAL dal = new OfficerDAL();
        //    OfficerData data = new OfficerData();
        //    dal.GetDataByLOID(loid, null);
        //    data.TNAME = dal.TNAME;
        //    data.LASTNAME = dal.LASTNAME;
        //    data.DIVISION = dal.DIVISION;
        //    data.USERID = dal.USERID;
        //    data.PASSWORD = dal.PASSWORD;
        //    data.EFDATE = dal.EFDATE;
        //    data.EPDATE = dal.EPDATE;
        //    return data;
        //}

        public PDReserveData GetData(double loid)
        {
            PDReserveData data = new PDReserveData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.LOID = DALObj.LOID;
                data.CODE = DALObj.CODE;
                //data.LOTNO = DALObj.LOTNO;
                data.REQDATE = DALObj.REQDATE;
                //data.PURCHASETYPE = DALObj.PURCHASETYPE;
                //data.REQUESTBY = DALObj.REQUESTBY;
                //data.DIVISION = DALObj.DIVISION;
                data.STATUS = DALObj.STATUS;
                data.CREATEBY = DALObj.CREATEBY;
                data.REMARK = DALObj.REMARK;
            }
            return data;
        }

        public PDReserveData GetData1(double loid)
        {
            PDReserveData data = new PDReserveData();
            if (SearchDAL.GetDataByLOID(loid, null))
            {
                data.LOTNO = SearchDAL.LOTNO;
                data.PDBARCODE = SearchDAL.PDBARCODE;
                data.PDNAME = SearchDAL.PDNAME;
                data.BATCHSIZE = SearchDAL.BATCHSIZE;
                data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
            }
            return data;
        }
        public bool ValidateData(ReturnRequestData data)
        {
            bool ret = true;
            if (data.LOTNO.Trim() == "")
            {
                ret = false;
                _error = "กรุณาระบุเลขที่การผลิต";
            }
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
            else if (data.ITEM.Count == 0)
            {
                ret = false;
                _error = "กรุณาระบุรายการสินค้า";
            }
            return ret;
        }

        public bool UpdateData(string userID, ReturnRequestData data)
        {

            // ####### UPDATE REQUISITION
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();

                //if (data.LOID != 0)
                //{
                //    // update old requisition
                //    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                //}
                //else
                //{
                //    DALObj.STATUS = data.STATUS;
                //    DALObj.ACTIVE = data.ACTIVE;
                //    DALObj.REFTABLE = "PDPRODUCT";
                //    DALObj.REFLOID = data.VPLOID;
                //    DALObj.LOTNO = data.LOTNO;
                //    DALObj.WAREHOUSE = data.WAREHOUSE;
                //}

                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);

                    DALObj.CODE = data.CODE;
                    DALObj.REQDATE = data.REQDATE;
                    DALObj.STATUS = data.STATUS;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.REFTABLE = data.REFTABLE;
                    DALObj.REFLOID = data.REFLOID;
                    DALObj.LOTNO = data.LOTNO;
                    DALObj.WAREHOUSE = data.WAREHOUSE;
                    DALObj.REMARK = data.REMARK;
                    DALObj.REQUISITIONTYPE = data.REQUISITIONTYPE;


                    if (DALObj.OnDB)
                        ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    else
                        ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                    _LOID = DALObj.LOID;
                    if (!ret)
                    {
                        throw new ApplicationException(DALObj.ErrorMessage);
                    }


                    // ############ UPDATE ITEM
                    ProductMaterial itemDAL = new ProductMaterial();
                    itemDAL.DeleteCurrentDataByREQ(DALObj.LOID, obj.zTrans);
                    for (Int16 i = 0; i < data.ITEM.Count; ++i)
                    {
                        itemDAL = new ProductMaterial();
                        ReturnItemData item = (ReturnItemData)data.ITEM[i];
                        itemDAL.REQUISITION = DALObj.LOID;
                        itemDAL.PRODUCT = item.PDLOID;
                        itemDAL.MASTER = item.MASTER;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.USEQTY = item.USEQTY;
                        itemDAL.WASTEQTYMAT = item.WASTEQTYMAT;
                        itemDAL.RETURNQTY = item.RETURNQTY;
                        itemDAL.ACTIVE = Constz.ActiveStatus.Active;

                        //                        itemDAL.OnDB = false;
                        ret = itemDAL.InsertCurrentData(userID, obj.zTrans);
                        if (!ret) throw new ApplicationException(itemDAL.ErrorMessage);
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

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ProductMaterial itemDAL = new ProductMaterial();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByReturn(Convert.ToDouble(arrData[i]), obj.zTrans);
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

        public bool UpdateReturnStatus(ArrayList arrData, string status, string userID)
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
                        if (GetRQMItem(Convert.ToDouble(arrData[i])).Rows.Count == 0)
                        {
                            throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุสินค้า");
                        }
                        PDReserveData data = GetData(Convert.ToDouble(arrData[i]));
                        //if (data.REASON == "") throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุเหตุผลในการขอซื้อ");
                    }
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    DALObj.STATUS = status;
                    ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                    if (!ret) throw new ApplicationException(DALObj.ErrorMessage);

                    ret = DALObj.CutStockRequisition(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
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

        //public bool CopyPDRequest(string userID, double loidSource)
        //{
        //    PDReserveData data = GetData(loidSource);
        //    DataTable itemList = GetPDItem(data.LOID);
        //    ArrayList arr = new ArrayList();
        //    foreach (DataRow dRow in itemList.Rows)
        //    {
        //        PDReserveItemData idata = new PDReserveItemData();
        //        idata.MASTER = Convert.ToDouble(dRow["MASTER"]);
        //        idata.ACTIVE = Constz.ActiveStatus.Active;
        //        arr.Add(idata);
        //    }
        //    data.ITEM = arr;
        //    DALObj.OnDB = false;
        //    data.LOID = 0;
        //    data.CODE = "";
        //    data.STATUS = Constz.Requisition.Status.Waiting.Code;
        //    //data.ACTIVE = Constz.ActiveStatus.Active;
        //    //data.ORDERTYPE = Constz.OrderType.PO.Code;
        //    return UpdateData(userID, data);
        //}

        public bool CommitData(string userID, ReturnRequestData data)
        {
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();
                try
                {
                    UpdateData(userID, data);

                    ret = DALObj.CutStockRequisition(_LOID, userID, obj.zTrans);
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
    }
}

