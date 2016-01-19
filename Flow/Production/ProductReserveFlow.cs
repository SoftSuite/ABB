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
using ABB.DAL.Sales;
using ABB.DAL.Production;
using ABB.Flow.Admin;
using ABB.Flow.Sales;
using ABB.Flow.Production;

namespace ABB.Flow.Production
{
    public class ProductReserveFlow
    {
        string _error = "";
        double _LOID = 0;
        PDReserveDAL _dal;
        PopupProductDAL search;
        RequisitionDAL _RDAL;

        public double LOID
        {
            get { return _LOID; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public PDReserveDAL DALObj
        {
            get { if (_dal == null) { _dal = new PDReserveDAL(); } return _dal; }
        }
        public PopupProductDAL SearchDAL
        {
            get { if (search == null) search = new PopupProductDAL(); return search; }
        }
        private RequisitionDAL RQDAL
        {
            get { if (_RDAL == null) { _RDAL = new RequisitionDAL(); } return _RDAL; }
        }
        public DataTable GetPDRequestList(PDReserveSearchData data)
        {
            string whereString = "";

            if (data.CODE.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "CODE = '" + OracleDB.QRText(data.CODE.Trim()) + "' ";
            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "REQDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "PDNAME LIKE '%" + OracleDB.QRText(data.PRODUCTNAME.Trim()) + "%' ";
            if (data.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(data.LOTNO.Trim()) + "' ";
            if (data.REFWAREHOUSE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "REFWAREHOUSE = '" + OracleDB.QRText(data.REFWAREHOUSE.ToString()) + "' ";
            if (data.STATUSFROM.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
            if (data.STATUSTO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

            string sql = "select ROWNUM NO,A.* from (SELECT RQ.LOID, RQ.CODE, RQ.REQDATE,RQ.REQUISITIONTYPE, VP.PDNAME, RQ.REFWAREHOUSE,VP.LOTNO,VP.BATCHSIZE,VP.BATCHSIZEUNITNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.DoWaiting.Code + "' THEN '" + Constz.Requisition.Status.DoWaiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.SendWareHouse.Code + "' THEN '" + Constz.Requisition.Status.SendWareHouse.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE RQ.STATUS WHEN '" + Constz.Requisition.Status.DoWaiting.Code + "' THEN '" + Constz.Requisition.Status.DoWaiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.SendWareHouse.Code + "' THEN '" + Constz.Requisition.Status.SendWareHouse.Rank + "' ";
            sql += "ELSE '' END AS RANK ";
            sql += "FROM REQUISITION RQ INNER JOIN V_PRODUCT_PDPRODUCT VP ON RQ.REFTABLE='PDPRODUCT' ";
            sql += "AND RQ.REFLOID=VP.PDPLOID ";
            sql += "INNER JOIN REQUISITIONTYPE RT ON RQ.REQUISITIONTYPE=RT.LOID AND RT.LOID=8) A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            DataTable dt = OracleDB.ExecListCmd(sql);
            //for (int i = 0; i < dt.Rows.Count; ++i)
            //{
            //    dt.Rows[i]["NO"] = i + 1;
            //}
            return dt;
        }
        #region Get List For DataGridView
        //public DataTable GetPDItem(string LOTNO) { return GetPDItem(LOTNO, 1); }
        //public DataTable GetPDItem(string LOTNO, double ConvertNo)
        //{
        //    string sql = "SELECT ROWNUM as LOID, ROWNUM RANK, VB.RWBARCODE,VB.RWNAME,VB.RWGROUPNAME, VB.RWMASTER * " + ConvertNo.ToString() + " as MASTER,VB.RWUNITNAME UNAME, VB.RWLOID as PDLOID, VB.RWUNIT AS UNIT ";
        //    sql += "FROM V_BOM_LIST VB LEFT JOIN V_PRODUCT_PDPRODUCT VP ON VB.PDLOID=VP.PDLOID AND VB.PDLOTNO = VP.LOTNO ";
        //    //sql += "LEFT JOIN REQMATERIAL RQM ON RQM.PRODUCT= VP.PDLOID LEFT JOIN REQUISITION RQ ON RQM.REQUISITION=RQ.LOID AND RQ.REQUISITIONTYPE='11'  ";
        //    sql += "WHERE VB.PCACTIVE=1 AND VB.PDLOTNO = '" + LOTNO + "'";
        //    return OracleDB.ExecListCmd(sql);
        //}

        public DataTable GetPDItem(string LOTNO)
        {
            //string sql = "SELECT ROWNUM as LOID, ROWNUM RANK, VB.RWBARCODE,VB.RWNAME,VB.RWGROUPNAME, VB.RWMASTER * VP.STDQTY as MASTER, ";
            //sql += "VB.RWUNITNAME UNAME, VB.RWLOID as PDLOID, VB.RWUNIT AS UNIT, FN_GETPRODUCTSTOCKQTY(2, " + Constz.Zone.Z04.ToString() + ", VB.PRODUCTMASTER) STOCKQTY ";
            //sql += "FROM V_BOM_LIST VB LEFT JOIN V_PRODUCT_PDPRODUCT VP ON VB.PDLOID=VP.PDLOID AND VB.PDLOTNO = VP.LOTNO ";
            //sql += "WHERE VB.PCACTIVE=1 AND VB.PDLOTNO = '" + LOTNO + "'";
            string sql = "SELECT ROWNUM as LOID, ROWNUM RANK, VB.RWBARCODE,VB.RWNAME,VB.PTLOID, VB.RWGROUPNAME, ";
            sql += "CASE  VB.RWGLOID ";
            sql += "WHEN 439 THEN "; // บรรจุภัณฑ์
            sql += "VB.RWMASTER * VP.STDQTY ELSE (VP.BATCHSIZE*1000*VB.RWMASTER)/100 END MASTER, ";
            sql += "VB.RWUNITNAME UNAME, VB.RWLOID as PDLOID, VB.RWUNIT AS UNIT, ";
            sql += "FN_GETPRODUCTSTOCKQTY(2, 4, VB.PRODUCTMASTER) STOCKQTY ";
            sql += "FROM V_BOM_LIST VB ";
            sql += "LEFT JOIN V_PRODUCT_PDPRODUCT VP ON VB.PDLOID=VP.PDLOID AND VB.PDLOTNO = VP.LOTNO ";
            sql += "WHERE VB.PCACTIVE=1 AND VB.PDLOTNO = '" + LOTNO + "'";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPDItem(double LOID)
        {
            string sql = "SELECT RQM.LOID as LOID, ROWNUM RANK, VR.BARCODE as RWBARCODE, VR.NAME as RWNAME, VR.GNAME as RWGROUPNAME, RQM.MASTER, ";
            sql += "VR.UNAME, RQM.PRODUCT as PDLOID,RQM.UNIT as UNIT, VR.STOCKQTY ";
            sql += " FROM REQMATERIAL RQM ";
            sql += " INNER JOIN V_RAW_LIST VR ON VR.LOID = RQM.PRODUCT ";
            sql += " WHERE RQM.REQUISITION = " + LOID.ToString();

            return OracleDB.ExecListCmd(sql);
        }
        #endregion

        #region Get Detaiil of PDReservePage

        public PDReserveData GetAllData(double loid)
        {
            // Get Old Data From Requisition 

            PDReserveData data = new PDReserveData();
            if (DALObj.GetDataByLOID(loid, null))
            {
                data.MFGDATE = DALObj.DUEDATE;
                data.REQDATE = DALObj.REQDATE;
                data.CODE = DALObj.CODE;
                data.CREATEBY = DALObj.CREATEBY;
                data.STATUS = DALObj.STATUS;
                data.REMARK = DALObj.REMARK;
                data.LOTNO = DALObj.LOTNO;
                data.WAREHOUSE = DALObj.WAREHOUSE;
                data.LOTNO = DALObj.LOTNO;
                data.LOID = DALObj.LOID;

                PDReserveData data2 = GetPDDataFromLOT(data.LOTNO);
                data.PDBARCODE = data2.PDBARCODE;
                data.PDNAME = data2.PDNAME;
                data.PDUNITNAME = data2.PDUNITNAME;
                data.STDQTY = data2.STDQTY;
                data.BATCHSIZE = data2.BATCHSIZE;
                data.BATCHSIZEUNITNAME = data2.BATCHSIZEUNITNAME;
                data.BATCHSIZEUNIT = data2.BATCHSIZEUNIT;
                data.PACKSIZE = data2.PACKSIZE;
                data.PACKSIZEUNIT = data2.PACKSIZEUNIT;
                data.MFGDATE = data2.MFGDATE;
            }
            return data;
        }

        public PDReserveData GetPDDataFromLOT(string LotNo)
        {
            // Get Master Data From BOM List..

            PDReserveData data = new PDReserveData();
            if (SearchDAL.GetDataByLOTNO(LotNo, null))
            {
                data.VPLOID = SearchDAL.PDPLOID;
                data.LOTNO = SearchDAL.LOTNO;
                data.PDBARCODE = SearchDAL.PDBARCODE;
                data.PDNAME = SearchDAL.PDNAME;
                data.STDQTY = SearchDAL.STDQTY;
                data.PDUNITNAME = SearchDAL.PDUNITNAME;
                data.BATCHSIZE = SearchDAL.BATCHSIZE;
                data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
                data.BATCHSIZEUNIT = SearchDAL.BATCHSIZEUNIT;
                data.PACKSIZE = SearchDAL.PACKSIZE;
                data.PACKSIZEUNIT = SearchDAL.PACKSIZEUNIT;
                data.MFGDATE = SearchDAL.MFGDATE;
            }

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
                data.MFGDATE = DALObj.DUEDATE;
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
                data.MFGDATE = SearchDAL.MFGDATE;
                data.LOTNO = SearchDAL.LOTNO;
                data.PDBARCODE = SearchDAL.PDBARCODE;
                data.PDNAME = SearchDAL.PDNAME;
                data.STDQTY = SearchDAL.STDQTY;
                data.PDUNITNAME = SearchDAL.PDUNITNAME;
                data.BATCHSIZE = SearchDAL.BATCHSIZE;
                data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
                data.BATCHSIZEUNIT = SearchDAL.BATCHSIZEUNIT;
                data.PACKSIZE = SearchDAL.PACKSIZE;
                data.PACKSIZEUNIT = SearchDAL.PACKSIZEUNIT;
            }
            return data;
        }

        public static double ConvertUnit(string UFrom, string UTo, string QtyFrom, string QtyTo)
        {
            string str = "";
            double multiply = 0;
            double stdqty = 0;
            str = " SELECT MULTIPLY FROM UNITTRANSFORM ";
            str += " WHERE UNITFROM =" + UFrom + " AND UNITTO =" + UTo + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            if (dt.Rows.Count > 0)
            {
                multiply = Convert.ToDouble(dt.Rows[0]["MULTIPLY"]);
                stdqty = (Convert.ToDouble(QtyFrom) * multiply) / Convert.ToDouble(QtyTo);
                return stdqty;
            }
            else
            {
                stdqty = Convert.ToDouble(QtyFrom) * Convert.ToDouble(QtyTo);
                return stdqty;
            }
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
                data.REQDATE = DALObj.REQDATE;
                data.MFGDATE = DALObj.DUEDATE;
                data.REQUISITIONTYPE = DALObj.REQUISITIONTYPE;
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
                data.MFGDATE = SearchDAL.MFGDATE;
                data.LOTNO = SearchDAL.LOTNO;
                data.PDBARCODE = SearchDAL.PDBARCODE;
                data.PDNAME = SearchDAL.PDNAME;
                data.STDQTY = SearchDAL.STDQTY;
                data.PDUNITNAME = SearchDAL.PDUNITNAME;
                data.BATCHSIZE = SearchDAL.BATCHSIZE;
                data.BATCHSIZEUNITNAME = SearchDAL.BATCHSIZEUNITNAME;
                data.BATCHSIZEUNIT = SearchDAL.BATCHSIZEUNIT;
                data.PACKSIZE = SearchDAL.PACKSIZE;
                data.PACKSIZEUNIT = SearchDAL.PACKSIZEUNIT;
            }
            return data;
        }
        public bool ValidateData(PDReserveData data)
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
                _error = "ยังไม่มีข้อมูลใน Bom ของสินค้าประเภทนี้";
            }
            return ret;
        }

        public bool UpdateData(string userID, PDReserveData data)
        {

            // ####### UPDATE REQUISITION
            bool ret = true;
            if (ValidateData(data))
            {
                OracleDBObj obj = new OracleDBObj();
                obj.CreateConnection();
                obj.CreateTransaction();

                if (data.LOID != 0)
                {
                    // update old requisition
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);
                }
                else
                {
                    DALObj.STATUS = data.STATUS;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.REFTABLE = "PDPRODUCT";
                    DALObj.REFLOID = data.VPLOID;
                    DALObj.LOTNO = data.LOTNO;
                    DALObj.CODE = data.CODE;
                    DALObj.WAREHOUSE = data.WAREHOUSE;
                    DALObj.REQUISITIONTYPE =data.REQUISITIONTYPE ;
                }

                try
                {
                    DALObj.OnDB = false;
                    DALObj.GetDataByLOID(data.LOID, obj.zTrans);

                    //DALObj.CODE = data.CODE;
                    DALObj.REQDATE = data.REQDATE;
                    DALObj.DUEDATE = data.MFGDATE;
                    DALObj.REMARK = data.REMARK;
                    DALObj.STATUS = data.STATUS;
                    DALObj.ACTIVE = data.ACTIVE;
                    DALObj.LOTNO = data.LOTNO;
                    DALObj.WAREHOUSE = data.WAREHOUSE;
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
                        ProductMaterialData item = (ProductMaterialData)data.ITEM[i];
                        itemDAL.REQUISITION = DALObj.LOID;
                        itemDAL.PRODUCT = item.PRODUCT;
                        itemDAL.UNIT = item.UNIT;
                        itemDAL.MASTER = item.MASTER;
                        itemDAL.ACTIVE = item.ACTIVE;

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

        //public bool DeleteData(ArrayList arrData)
        //{
        //    bool ret = true;
        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {
        //        PRItemDAL itemDAL = new PRItemDAL();
        //        for (int i = 0; i < arrData.Count; i++)
        //        {
        //            DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
        //            itemDAL.DeleteDataByPDRequest(Convert.ToDouble(arrData[i]), obj.zTrans);
        //            ret = DALObj.DeleteCurrentData(obj.zTrans);
        //            if (!ret)
        //            {
        //                if (DALObj.ErrorMessage.Contains("integrity constraint"))
        //                    throw new Exception(GlobalErrorMessage.ConstaintDelete);
        //                else
        //                    throw new ApplicationException(DALObj.ErrorMessage);
        //            }
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

        public bool DeleteData(ArrayList arrData)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                ReqMaterialDAL itemDAL = new ReqMaterialDAL();
                for (int i = 0; i < arrData.Count; i++)
                {
                    DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                    itemDAL.DeleteDataByRequisition(Convert.ToDouble(arrData[i]), obj.zTrans);
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
                        PDReserveData data = GetData(Convert.ToDouble(arrData[i]));
                        //if (data.REASON == "") throw new ApplicationException("ไม่สามารถอนุมัติรายการได้ เนื่องจากบางรายการไม่ได้ระบุเหตุผลในการขอซื้อ");
                    }
                    //ret = DALObj.UpdatePDRequestStatus(Convert.ToDouble(arrData[i]), status, userID, obj.zTrans);
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

        public bool CopyPDRequest(string userID, double loidSource)
        {
            PDReserveData data = GetData(loidSource);
            DataTable itemList = GetPDItem(data.LOID);
            ArrayList arr = new ArrayList();
            foreach (DataRow dRow in itemList.Rows)
            {
                PDReserveItemData idata = new PDReserveItemData();
                idata.MASTER = Convert.ToDouble(dRow["MASTER"]);
                idata.ACTIVE = Constz.ActiveStatus.Active;
                arr.Add(idata);
            }
            data.ITEM = arr;
            DALObj.OnDB = false;
            data.LOID = 0;
            data.CODE = "";
            data.STATUS = Constz.Requisition.Status.Waiting.Code;
            //data.ACTIVE = Constz.ActiveStatus.Active;
            //data.ORDERTYPE = Constz.OrderType.PO.Code;
            return UpdateData(userID, data);
        }

        public bool SubmitPDRequisition(ArrayList arrData, string userID)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                for (int i = 0; i < arrData.Count; ++i)
                {
                        RQDAL.OnDB = false;
                        RQDAL.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                        if (RQDAL.STATUS == Constz.Requisition.Status.DoWaiting.Code)
                        {
                            RQDAL.STATUS = Constz.Requisition.Status.Approved.Code;
                            ret = RQDAL.UpdateCurrentData(userID, obj.zTrans);
                            if (!ret) throw new ApplicationException(RQDAL.ErrorMessage);

                            ret = RQDAL.CutStockRequisition(Convert.ToDouble(arrData[i]), userID, obj.zTrans);
                            if (!ret) throw new ApplicationException(RQDAL.ErrorMessage);
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
        public bool CommitData(string userID, PDReserveData data)
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
