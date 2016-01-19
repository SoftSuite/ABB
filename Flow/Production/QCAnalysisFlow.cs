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
     public class QCAnalysisFlow
     {
         string _error = "";
         double _STLOID = 0;
         QCAnalysisSearchDAL _dal;
         PopupProductDAL search;
         StockInDAL _sDAL;
         PDOrderDAL _pDAL;

         public double STLOID
         {
             get { return _STLOID; }
         }

         public string ErrorMessage
         {
             get { return _error; }
         }

         public QCAnalysisSearchDAL DALObj
         {
             get { if (_dal == null) { _dal = new QCAnalysisSearchDAL(); } return _dal; }
         }
         public PopupProductDAL SearchDAL
         {
             get { if (search == null) search = new PopupProductDAL(); return search; }
         }
         private StockInDAL StDAL
         {
             get { if (_sDAL == null) { _sDAL = new StockInDAL(); } return _sDAL; }
         }
         private PDOrderDAL PoDAl
         {
             get { if (_pDAL == null) { _pDAL = new PDOrderDAL(); } return _pDAL; }
         }
         public DataTable GetPDRequestList(QCAnalysisSearchData data)
         {
             string whereString = "";

             if (data.QCCODE.Trim() != "")
                 whereString += (whereString == "" ? "" : "AND ") + "QCCODE = '" + OracleDB.QRText(data.QCCODE.Trim()) + "' ";
             if (data.DATEFROM.Year != 1)
                 whereString += (whereString == "" ? "" : "AND ") + "QCDATE >= " + OracleDB.QRDate(data.DATEFROM) + " ";
             if (data.DATETO.Year != 1)
                 whereString += (whereString == "" ? "" : "AND ") + "QCDATE <= " + OracleDB.QRDate(data.DATETO) + " ";
             if (data.CODE.Trim() != "")
                 whereString += (whereString == "" ? "" : "AND ") + "CODE = '" + OracleDB.QRText(data.CODE.Trim()) + "' ";
             if (data.STATUSFROM.Trim() != "")
                 whereString += (whereString == "" ? "" : "AND ") + "RANK >= '" + OracleDB.QRText(data.STATUSFROM.Trim()) + "' ";
             if (data.STATUSTO.Trim() != "")
                 whereString += (whereString == "" ? "" : "AND ") + "RANK <= '" + OracleDB.QRText(data.STATUSTO.Trim()) + "' ";

             string sql = "select ROWNUM NO,A.* from (SELECT STLOID, QCCODE, QCDATE, CODE, PDLOID, PDNAME, QTY, UNAME, APPROVER, DVNAME, STATUSVAL,TABLENAME, ";
             sql += "CASE STATUSVAL WHEN '" + Constz.Requisition.Status.SendQC.Name + "' THEN '" + Constz.Requisition.Status.SendQC.Name + "' ";
             sql += "WHEN '" + Constz.Requisition.Status.ReturnQC.Name + "' THEN '" + Constz.Requisition.Status.ReturnQC.Name + "' ";
             sql += "ELSE '' END AS STATUSNAME, ";
             sql += "CASE STATUSVAL WHEN '" + Constz.Requisition.Status.SendQC.Name + "' THEN '" + Constz.Requisition.Status.SendQC.Rank + "' ";
             sql += "WHEN '" + Constz.Requisition.Status.ReturnQC.Name + "' THEN '" + Constz.Requisition.Status.ReturnQC.Rank + "' ";
             sql += "ELSE '' END AS RANK ";
             sql += "FROM V_TODOLIST_QC ) A ";
             sql += (whereString == "" ? "" : "WHERE " + whereString);

             DataTable dt = OracleDB.ExecListCmd(sql);
             
             return dt;
         }

         #region Get Detaiil of PDReservePage

         public PDReserveData GetAllData(double loid)
         {
             // Get Old Data From Requisition 

             PDReserveData data = new PDReserveData();
             if (DALObj.GetDataByLOID(loid, null))
             {
                 //data.REQDATE = DALObj.REQDATE;
                 //data.CODE = DALObj.CODE;
                 //data.CREATEBY = DALObj.CREATEBY;
                 //data.STATUS = DALObj.STATUS;
                 //data.REMARK = DALObj.REMARK;
                 //data.LOTNO = DALObj.LOTNO;
                 //data.WAREHOUSE = DALObj.WAREHOUSE;
                 //data.LOTNO = DALObj.LOTNO;

                 PDReserveData data2 = GetPDDataFromLOT(data.LOTNO);
                 data.PDBARCODE = data2.PDBARCODE;
                 data.PDNAME = data2.PDNAME;
                 data.BATCHSIZE = data2.BATCHSIZE;
                 data.BATCHSIZEUNITNAME = data2.BATCHSIZEUNITNAME;


             }
             return data;
         }

         public PDReserveData GetPDDataFromLOT(string LotNo)
         {
             // Get Master Data From BOM List..

             PDReserveData data = new PDReserveData();
             if (SearchDAL.GetDataByLOTNO(LotNo, null))
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

         public UnitSearchData GetUnitData(double loid)
         {
             UnitFlow uFlow = new UnitFlow();
             return uFlow.GetData(loid);
         }

       

         public QCAnalysisData GetData(double loid)
         {
             QCAnalysisData data = new QCAnalysisData();
             if (DALObj.GetDataByLOID(loid, null))
             {
                 data.STLOID = DALObj.STLOID;
                 data.CODE = DALObj.CODE;
                 data.QCCODE = DALObj.QCCODE;
                 data.QCDATE = DALObj.QCDATE;
                 data.PDNAME = DALObj.PDNAME;
                 data.QTY = DALObj.QTY;
                 data.STATUSVAL = DALObj.STATUSVAL;
                 data.UNAME = DALObj.UNAME;
                 data.DVNAME = DALObj.DVNAME;
             }
             return data;
         }

         public bool ValidateData(QCAnalysisData data)
         {
             bool ret = true;
             if (data.QCCODE == "")
             {
                 ret = false;
                 _error = "กรุณาระบุเลขที่ส่งตรวจ";
             }
             else if (data.ITEM.Count == 0)
             {
                 ret = false;
                 _error = "กรุณาระบุรายการสินค้า";
             }
             return ret;
         }

         public bool UpdateData(string userID, QCAnalysisData data)
         {

             // ####### UPDATE REQUISITION
             bool ret = true;
             if (ValidateData(data))
             {
                 OracleDBObj obj = new OracleDBObj();
                 obj.CreateConnection();
                 obj.CreateTransaction();

                 if (data.STLOID != 0)
                 {
                     // update old requisition
                     DALObj.GetDataByLOID(data.STLOID, obj.zTrans);
                 }
                 else
                 {
                     DALObj.STLOID = data.STLOID;
                 }

                 try
                 {
                     DALObj.OnDB = false;
                     DALObj.GetDataByLOID(data.STLOID, obj.zTrans);

                     //DALObj.CODE = data.CODE;
                     //DALObj.REQDATE = data.REQDATE;
                     //DALObj.REMARK = data.REMARK;


                     if (DALObj.OnDB)
                         ret = DALObj.UpdateCurrentData(userID, obj.zTrans);
                     else
                         ret = DALObj.InsertCurrentData(userID, obj.zTrans);

                     _STLOID = DALObj.STLOID;
                     if (!ret)
                     {
                         throw new ApplicationException(DALObj.ErrorMessage);
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
                 PRItemDAL itemDAL = new PRItemDAL();
                 for (int i = 0; i < arrData.Count; i++)
                 {
                     DALObj.GetDataByLOID(Convert.ToDouble(arrData[i]), obj.zTrans);
                     itemDAL.DeleteDataByPDRequest(Convert.ToDouble(arrData[i]), obj.zTrans);
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

         public bool SubmitQCStockIn(ArrayList arrData, string userID)
         {
             bool ret = true;
             OracleDBObj obj = new OracleDBObj();
             obj.CreateConnection();
             obj.CreateTransaction();
             try
             {
                 for (int i =0; i<arrData.Count; ++i)
                 {
                     string[] str = arrData[i].ToString().Split('#');
                     if (str[1] == "STOCKIN")
                     {
                         StDAL.OnDB = false;
                         StDAL.GetDataByLOID(Convert.ToDouble(str[0]), obj.zTrans);
                         StockInItemDAL _itemDAL = new StockInItemDAL();
                         if (_itemDAL.GetDataList("WHERE STOCKIN = " + StDAL.LOID.ToString() + " AND QCRESULT NOT IN ('" + Constz.QCResult.Fail.Code + "', '" + Constz.QCResult.Pass.Code + "') ", obj.zTrans).Rows.Count == 0)
                             throw new ApplicationException("ระบุผลการตรวจสอบรายการเลขที่ " + StDAL.CODE + " ให้ครบทุกรายการ");
                         if (StDAL.STATUS == Constz.Requisition.Status.QC.Code)
                         {
                             StDAL.STATUS = Constz.Requisition.Status.Approved.Code;
                             ret = StDAL.UpdateCurrentData(userID, obj.zTrans);
                             if (!ret) throw new ApplicationException(StDAL.ErrorMessage);
                         }
                     }
                     else
                     {
                         PoDAl.OnDB = false;
                         PoDAl.GetDataByLOID(Convert.ToDouble(str[0]), obj.zTrans);
                         PDProductDAL _itemDAL = new PDProductDAL();
                         if (_itemDAL.GetDataList("WHERE PDPRODUCT = " + PoDAl.LOID.ToString() + " AND QCRESULT NOT IN ('" + Constz.QCResult.Fail.Code + "', '" + Constz.QCResult.Pass.Code + "') ", obj.zTrans).Rows.Count == 0)
                             throw new ApplicationException("ระบุผลการตรวจสอบรายการเลขที่ " + PoDAl.CODE + " ให้ครบทุกรายการ");
                         if (PoDAl.STATUS == Constz.Requisition.Status.QC.Code)
                         {
                             PoDAl.STATUS = Constz.Requisition.Status.Approved.Code;
                             ret = PoDAl.UpdateCurrentData(userID, obj.zTrans);
                             if (!ret) throw new ApplicationException(PoDAl.ErrorMessage);

                             ret = PoDAl.CutStock(Convert.ToDouble(str[0]), userID, obj.zTrans);
                             if (!ret) throw new ApplicationException(PoDAl.ErrorMessage);
                         }
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

     }
 }

