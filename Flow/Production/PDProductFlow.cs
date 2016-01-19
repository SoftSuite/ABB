using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.DAL.Production;
using System.Data;
using ABB.Data;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.Data.Production;



/// <summary>
/// Create by: Nang
/// Create Date: 20 Feb 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 

namespace ABB.Flow.Production
{
    public  class PDProductFlow
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public static ArrayList GetPDProductSearch(string LotNo, string DateFrom, string DateTo, string PDName)
        {
            ArrayList arr = new ArrayList();
            arr = PDProductSearchDAL.GetPDProductSearch(LotNo, DateFrom, DateTo, PDName);
            return arr;
        }

        public static DataTable GetPdProductData(string PdLoid, string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetPdProductData(PdLoid, PdpLoid);  
            return dt;
        }

        public static DataTable GetMaterialData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetMaterialData(PdpLoid);
            return dt;
        }

        public DataTable GetProduct(string type)
        {
            return PDProductSearchDAL.GetProduct(type);
        }

        public static DataTable GetProductionFillData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetProductionFillData(PdpLoid);
            return dt;
        }

        public static DataTable GetRadiationData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetRadiationData(PdpLoid);
            return dt;
        }

        public static DataTable GetRadiationReturnData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetRadiationReturnData(PdpLoid);
            return dt;
        }

        public static DataTable GetStockInDetailData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetStockInDetailData(PdpLoid);
            return dt;
        }

        public static DataTable GetSendQCData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetSendQCData(PdpLoid);
            return dt;
        }

        public static DataTable GetStockOutDetailData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetStockOutDetailData(PdpLoid);
            return dt;
        }

        public static DataTable GetMaterialLostData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetMaterialLostData(PdpLoid);
            return dt;
        }

        public static DataTable GetPackageLostData(string PdpLoid)
        {
            DataTable dt = new DataTable();
            dt = PDProductSearchDAL.GetPackageLostData(PdpLoid);
            return dt;
        }

        public bool UpdateMaterial(string UserID, string PdpLoid, DataTable tempTable)
        {
            bool ret = true;
            ProcessDAL oDAL = new ProcessDAL();
            OracleDBObj objDB = new OracleDBObj();
            objDB.CreateConnection();
            objDB.CreateTransaction();

            try
            {
                if (tempTable.Rows.Count > 0)
                {
                    if (UpdateMaterial(UserID, tempTable, objDB, Convert.ToDouble(PdpLoid)) == true)
                    {
                        objDB.zTrans.Commit();
                        ret = true;
                    }
                    else
                    {
                        objDB.zTrans.Rollback();
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message; ;
                objDB.zTrans.Rollback();
                ret = false;
            }
            objDB.CloseConnection();
            return ret;
        }

        private bool UpdateMaterial(string UserID, DataTable tempTable, OracleDBObj objDB, double PdpLoid)
        {
            bool ret = true;
            string sqlDelete = "DELETE FROM MATERIALITEM WHERE PDPRODUCT = " + PdpLoid + "";
            OracleDB.ExecNonQueryCmd(sqlDelete, objDB.zTrans);

            double mtrLoid = 0;
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                if (Convert.ToDouble(tempTable.Rows[i]["MTRLOID"])!=mtrLoid)
                {
                    MaterialItemDAL miDAL = new MaterialItemDAL();
                    miDAL.PDPRODUCT = PdpLoid;
                    miDAL.PRODUCT = Convert.ToDouble(tempTable.Rows[i]["MTRLOID"]);
                    mtrLoid = Convert.ToDouble(tempTable.Rows[i]["MTRLOID"]);
                    if (tempTable.Rows[i]["ALLQTY"].ToString() == "" || tempTable.Rows[i]["ALLQTY"] == null) 
                        miDAL.ALLQTY = 0; 
                    else 
                        miDAL.ALLQTY = Convert.ToDouble(tempTable.Rows[i]["ALLQTY"]);
                  
                    miDAL.USEQTY = Convert.ToDouble(tempTable.Rows[i]["USEQTY"]);
                    miDAL.WASTEQTYMAT = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAT"]);
                    miDAL.WASTEQTYMAN = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAN"]);
                    miDAL.RETURNQTY = Convert.ToDouble(tempTable.Rows[i]["RETURNQTY"]);
                    miDAL.CHANGEQTY = Convert.ToDouble(tempTable.Rows[i]["CHANGEQTY"]);
                    miDAL.UNIT = Convert.ToDouble(tempTable.Rows[i]["UNIT"]);

                    if (tempTable.Rows[i]["ACTIVE"].ToString() == "" || tempTable.Rows[i]["ACTIVE"] == null) 
                        miDAL.ACTIVE = "1"; 
                    else 
                        miDAL.ACTIVE = tempTable.Rows[i]["ACTIVE"].ToString();
                    
                    miDAL.REMARK = tempTable.Rows[i]["REMARK"].ToString();
                    miDAL.YIELDMAT = Convert.ToDouble(tempTable.Rows[i]["YIELDMAT"]);
                    miDAL.YIELDMAM = Convert.ToDouble(tempTable.Rows[i]["YIELDMAM"]);
                    miDAL.PGROUP = tempTable.Rows[i]["PGROUP"].ToString();
                    ret = miDAL.InsertCurrentData(UserID, objDB.zTrans);

                    if (ret == false)
                    {
                        _error = miDAL.ErrorMessage;
                        return false;
                    }
                }
            }
            return true;
        }

        public bool UpdateMaterialLost(string UserID, string PdpLoid, DataTable tempTable)
        {
            bool ret = true;
            ProcessDAL oDAL = new ProcessDAL();
            OracleDBObj objDB = new OracleDBObj();
            objDB.CreateConnection();
            objDB.CreateTransaction();

            try
            {
                if (tempTable.Rows.Count > 0)
                {
                    if (UpdateMaterialLost(UserID, tempTable, objDB, Convert.ToDouble(PdpLoid)) == true)
                    {
                        objDB.zTrans.Commit();
                        ret = true;
                    }
                    else
                    {
                        objDB.zTrans.Rollback();
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message; ;
                objDB.zTrans.Rollback();
                ret = false;
            }
            objDB.CloseConnection();
            return ret;
        }

        private bool UpdateMaterialLost(string UserID, DataTable tempTable, OracleDBObj objDB, double PdpLoid)
        {
            bool ret = true;
            double MILoid = 0;
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                MaterialItemDAL miDAL = new MaterialItemDAL();
                DataTable dtt = GetMILoid(PdpLoid, Convert.ToDouble(tempTable.Rows[i]["MTRLOID"]));
                if (dtt.Rows.Count > 0)
                {
                    MILoid = Convert.ToDouble(dtt.Rows[0]["LOID"]);
                    miDAL.GetDataByLOID(MILoid, null);
                    miDAL.WASTEQTYMAT = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAT"]);
                    miDAL.WASTEQTYMAN = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAN"]);
                    miDAL.RETURNQTY = Convert.ToDouble(tempTable.Rows[i]["RETURNQTY"]);
                    miDAL.CHANGEQTY = Convert.ToDouble(tempTable.Rows[i]["CHANGEQTY"]);
                    miDAL.REMARK = tempTable.Rows[i]["REMARK"].ToString();
                    miDAL.YIELDMAT = Convert.ToDouble(tempTable.Rows[i]["YIELDMAT"]);
                    miDAL.YIELDMAM = Convert.ToDouble(tempTable.Rows[i]["YIELDMAM"]);
                    ret = miDAL.UpdateCurrentData(UserID, objDB.zTrans);
                }

                if (ret == false)
                {
                    _error = miDAL.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        private DataTable GetMILoid(double PdpLoid, double PdLoid)
        {
            string str = "";
            str = " SELECT DISTINCT LOID FROM MATERIALITEM WHERE PDPRODUCT = " + PdpLoid + " AND PRODUCT =" + PdLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public bool UpdatePackageLost(string UserID, string PdpLoid, DataTable tempTable)
        {
            bool ret = true;
            ProcessDAL oDAL = new ProcessDAL();
            OracleDBObj objDB = new OracleDBObj();
            objDB.CreateConnection();
            objDB.CreateTransaction();

            try
            {
                if (tempTable.Rows.Count > 0)
                {
                    if (UpdatePackageLost(UserID, tempTable, objDB, Convert.ToDouble(PdpLoid)) == true)
                    {
                        objDB.zTrans.Commit();
                        ret = true;
                    }
                    else
                    {
                        objDB.zTrans.Rollback();
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message; ;
                objDB.zTrans.Rollback();
                ret = false;
            }
            objDB.CloseConnection();
            return ret;
        }

        private bool UpdatePackageLost(string UserID, DataTable tempTable, OracleDBObj objDB, double PdpLoid)
        {
            bool ret = true;
            double MILoid = 0;
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                MaterialItemDAL miDAL = new MaterialItemDAL();
                DataTable dtt = GetMILoid(PdpLoid, Convert.ToDouble(tempTable.Rows[i]["MTRLOID"]));
                if (dtt.Rows.Count > 0)
                    MILoid = Convert.ToDouble(dtt.Rows[0]["LOID"]); miDAL.WASTEQTYMAT = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAT"]);
                miDAL.GetDataByLOID(MILoid, null);
                miDAL.RETURNQTY = Convert.ToDouble(tempTable.Rows[i]["RETURNQTY"]);
                miDAL.CHANGEQTY = Convert.ToDouble(tempTable.Rows[i]["CHANGEQTY"]);
                miDAL.REMARK = tempTable.Rows[i]["REMARK"].ToString();
                miDAL.WASTEQTYMAT = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAT"]);
                miDAL.YIELDMAT = Convert.ToDouble(tempTable.Rows[i]["YIELDMAT"]);

                ret = miDAL.UpdateCurrentData(UserID, objDB.zTrans);

                if (ret == false)
                {
                    _error = miDAL.ErrorMessage;
                    return false;
                }
            }
            return true;
        }

        public static bool UpdatePDPrdouct(string UserID, PDProductData data)
        {
            bool ret = true;
            PDProductDAL pDAL = new PDProductDAL();
            //### ProductFill
            pDAL.GetDataByLOID(data.LOID, null);
            pDAL.PACKING = data.PACKING.ToString();
            pDAL.PACKAGE = data.PACKAGE.ToString();
            pDAL.PDQTY = Convert.ToDouble(data.PDQTY);
            pDAL.EXPDATE = Convert.ToDateTime(data.EXPDATE);
            pDAL.YIELD = Convert.ToDouble(data.YIELD);
            pDAL.STDQTY = Convert.ToDouble(data.STDQTY);
            pDAL.LOSTQTY = Convert.ToDouble(data.LOSTQTY);

            //### Radiate
            pDAL.RADIATEDATE = Convert.ToDateTime(data.RADIATEDATE);
            pDAL.RADIATEQTY = Convert.ToDouble(data.RADIATEQTY);
            pDAL.RADIATEUNIT = Convert.ToDouble(data.RADIATEUNIT);
            pDAL.RADIATEREMARK = data.RADIATEREMARK.ToString();

            //### RadiateReturn
            pDAL.RADIATERETDATE = Convert.ToDateTime(data.RADIATERETDATE);
            pDAL.RADIATERETQTY = Convert.ToDouble(data.RADIATERETQTY);
            pDAL.RADIATERETUNIT = Convert.ToDouble(data.RADIATERETUNIT);
            pDAL.RADIATERETREMARK = data.RADIATERETREMARK.ToString();

            //### StockInDetail
            pDAL.QUARANTINEDATE = Convert.ToDateTime(data.QUARANTINEDATE);
            pDAL.QUARANTINEQTY = Convert.ToDouble(data.QUARANTINEQTY);
            pDAL.QUARANTINEUNIT = Convert.ToDouble(data.QUARANTINEUNIT);
            pDAL.QUARANTINEREMARK = data.QUARANTINEREMARK.ToString();

            //### SendQC
            pDAL.SENDQCDATE = Convert.ToDateTime(data.SENDQCDATE);

            //### StockOutDetail
            pDAL.SENDFGDATE = Convert.ToDateTime(data.SENDFGDATE);
            pDAL.SENDFGQTY = Convert.ToDouble(data.SENDFGQTY);
            pDAL.SENDFGREMARK = data.SENDFGREMARK.ToString();

            //### Production
            pDAL.LOTNO = data.LOTNO;
            pDAL.MFGDATE = data.MFGDATE;
            pDAL.BATCHSIZE = Convert.ToDouble(data.BATCHSIZE);
            pDAL.BATCHSIZEUNIT = Convert.ToDouble(data.BATCHSIZEUNIT);
            pDAL.TOWAREHOUSE = Convert.ToDouble(data.TOWAREHOUSE);

            ret = pDAL.UpdateCurrentData(UserID, null);
            return ret;
        }

        public static DataTable InsertPdProduct(string UserID, PDProductData data)
        {
            double PdpLoid = 0;
            DataTable dt = new DataTable();
            PDProductDAL pDAL = new PDProductDAL();
            pDAL.LOTNO = data.LOTNO;
            pDAL.MFGDATE = data.MFGDATE;
            pDAL.PDORDER = data.PDORDER;
            pDAL.PRODUCT = data.PRODUCT;
            pDAL.BATCHSIZEUNIT = data.BATCHSIZEUNIT;
            pDAL.BATCHSIZE = data.BATCHSIZE;
            //pDAL.SENDFGDATE = DateTime.Today;
            pDAL.EXPDATE = data.EXPDATE;
            //pDAL.RADIATEDATE = DateTime.Today;
            //pDAL.RADIATERETDATE = DateTime.Today;
            //pDAL.QUARANTINEDATE = DateTime.Today;
            //pDAL.SENDFGDATE = DateTime.Today;
            //pDAL.SENDQCDATE = DateTime.Today;
            pDAL.RADIATEUNIT = data.RADIATEUNIT;
            pDAL.RADIATERETUNIT = data.RADIATEUNIT;
            pDAL.QUARANTINEUNIT = data.RADIATEUNIT;
            pDAL.STDQTY = data.STDQTY;
            pDAL.PRODSTATUS = "WA";
            pDAL.REFLOID = data.REFLOID;
            pDAL.REFTABLE = data.REFTABLE;
            pDAL.PRODUCTTYPE = data.PRODUCTTYPE;
            pDAL.TOWAREHOUSE = data.TOWAREHOUSE;
            
            bool ret = pDAL.InsertCurrentData(UserID, null);
            if (ret == true)
            {
                PdpLoid = pDAL.LOID;
                dt = GetPdProduct(PdpLoid);
                
            }
            return dt;
        }

        public bool InsertMaterialItem(string UserID, DataTable tempTable,double PdpLoid)
        {
            bool ret = true;
            ProcessDAL oDAL = new ProcessDAL();
            OracleDBObj objDB = new OracleDBObj();
            objDB.CreateConnection();
            objDB.CreateTransaction();

            try
            {
                if (tempTable.Rows.Count > 0)
                {
                    if (InsertMaterialItemData(UserID, tempTable, objDB, PdpLoid) == true)
                    {
                        objDB.zTrans.Commit();
                        ret = true;
                    }
                    else
                    {
                        objDB.zTrans.Rollback();
                        ret = false;
                    }
                }
            }
            catch (Exception ex)
            {
                _error = ex.Message; ;
                objDB.zTrans.Rollback();
                ret = false;
            }
            objDB.CloseConnection();
            return ret;
        }

        private  bool InsertMaterialItemData(string UserID, DataTable tempTable, OracleDBObj objDB, double PdpLoid)
        {
            bool ret = true;
            string sqlDelete = "DELETE FROM MATERIALITEM WHERE PDPRODUCT = " + PdpLoid + "";
            OracleDB.ExecNonQueryCmd(sqlDelete, objDB.zTrans);
            for (int i = 0; i < tempTable.Rows.Count; i++)
            {
                MaterialItemDAL miDAL = new MaterialItemDAL();
                miDAL.PDPRODUCT = PdpLoid;
                miDAL.PRODUCT = Convert.ToDouble(tempTable.Rows[i]["MTRLOID"]);
                miDAL.ALLQTY = Convert.ToDouble(tempTable.Rows[i]["ALLQTY"]);
                miDAL.USEQTY = Convert.ToDouble(tempTable.Rows[i]["USEQTY"]);
                miDAL.WASTEQTYMAT = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAT"]);
                miDAL.WASTEQTYMAN = Convert.ToDouble(tempTable.Rows[i]["WASTEQTYMAN"]);
                miDAL.RETURNQTY = Convert.ToDouble(tempTable.Rows[i]["RETURNQTY"]);
                miDAL.CHANGEQTY = Convert.ToDouble(tempTable.Rows[i]["CHANGEQTY"]);
                miDAL.UNIT = Convert.ToDouble(tempTable.Rows[i]["ULOID"]);
                miDAL.ACTIVE = tempTable.Rows[i]["ACTIVE"].ToString();
                miDAL.REMARK = tempTable.Rows[i]["REMARK"].ToString();
                miDAL.YIELDMAT = Convert.ToDouble(tempTable.Rows[i]["YIELDMAT"]);
                miDAL.YIELDMAM = Convert.ToDouble(tempTable.Rows[i]["YIELDMAM"]);
                miDAL.PGROUP = tempTable.Rows[i]["PGROUP"].ToString();

                ret = miDAL.InsertCurrentData(UserID, objDB.zTrans);
            }
            return ret;
        }

        public static DataTable GetPdProduct(double PdpLoid)
        {
            DataTable dt = PDProductSearchDAL.GetPdProduct(PdpLoid);
            return dt;  
        }

        public static double InsertPdOrder(string UserID,string Remark,string reftable, double refloid)
        {
            double POLoid = 0;
            PDOrderDAL poDAL = new PDOrderDAL();
            poDAL.REMARK = Remark.Trim();
            poDAL.ORDERTYPE = "PD";
            poDAL.ACTIVE = "1";
            poDAL.STATUS = "WA";
            poDAL.POTYPE = "N";
            poDAL.SUPPLIER = Constz.ProductionDepartment.LOID;
            poDAL.REFLOID = refloid;
            poDAL.REFTABLE = reftable;
            bool ret = poDAL.InsertCurrentData(UserID, null);
            if (ret == true)
            {
                POLoid = poDAL.LOID;
                return POLoid;
            }
            else
                return 0;     
        }

        public static DataTable GetProductDetail(string Pdloid)
        {
            DataTable dt = PDProductSearchDAL.GetProductDetail(Pdloid);
            return dt;
        }

        public static DataTable GetRequisitionItemDetail(string RqiLoid)
        {
            DataTable dt = PDProductSearchDAL.GetRequisitionItemDetail(RqiLoid);
            return dt;
        }

        public static DataTable GetRequisitionItemDetailByToDoList(string RqLoid, string pdloid)
        {
            DataTable dt = PDProductSearchDAL.GetRequisitionItemDetailByToDoList(RqLoid,pdloid);
            return dt;
        }

        public static DataTable GetMaterialReLoadData(double PdLoid, double PDPLOID)
        {
            DataTable dt = PDProductSearchDAL.GetMaterialReLoadData(PdLoid, PDPLOID);
            return dt;
        }

        public static DataTable GetMaterialLostReLoadData(double PdLoid,double PDPLOID)
        {
            DataTable dt = PDProductSearchDAL.GetMaterialLostReLoadData(PdLoid, PDPLOID );
            return dt;
        }

        public static DataTable CheckMaterialItem(double PdLoid)
        {
            DataTable dt = PDProductSearchDAL.CheckMaterialItem(PdLoid);
            return dt;
        }

        public static DataTable CheckMaterialItemWithReq(double PdLoid)
        {
            DataTable dt = PDProductSearchDAL.CheckMaterialItem(PdLoid);
            return dt;
        }

        public static DataTable GetPackageLostReLoadData(double PdLoid,double PDPLOID)
        {
            DataTable dt = PDProductSearchDAL.GetPackageLostReLoadData(PdLoid, PDPLOID);
            return dt;
        }

        public static double  ConvertUnit(string UFrom, string UTo,string QtyFrom, string QtyTo)
        {
            string str ="";
            double multiply = 0;
            double stdqty = 0;
            str =" SELECT MULTIPLY FROM UNITTRANSFORM ";
            str += " WHERE UNITFROM ="+ UFrom +" AND UNITTO ="+ UTo +"";
            DataTable dt = OracleDB.ExecListCmd(str);
            if (dt.Rows.Count > 0)
            {
                multiply = Convert.ToDouble(dt.Rows[0]["MULTIPLY"]);
                stdqty = (Convert.ToDouble(QtyFrom) * multiply) / Convert.ToDouble(QtyTo);
                return stdqty;
            }
            else if (Convert.ToDouble(UFrom) == Convert.ToDouble(UTo))
            {
                stdqty = Convert.ToDouble(QtyFrom) / Convert.ToDouble(QtyTo);
                return stdqty;
            }
            else
                return stdqty;
        }

        public static bool UpdatePdOrder(string UserID, PDOrderData data,double PoLoid)
        {
            PDOrderDAL poDAL = new PDOrderDAL();
            poDAL.GetDataByLOID(PoLoid, null);
            poDAL.REMARK = data.REMARK;
            poDAL.SUPPLIER = Constz.ProductionDepartment.LOID;
            bool ret = poDAL.UpdateCurrentData(UserID, null);
            return ret;
        }

        public static bool UpdateRequest(string UserID,double PdpLoid, double RqiLoid, double qty)
        {
            bool ret =true;
            PDProductDAL pdDAL = new PDProductDAL();
            pdDAL.GetDataByLOID(PdpLoid, null);
            pdDAL.REFLOID = RqiLoid;
            pdDAL.REFTABLE = "REQUISITIONITEM";
            pdDAL.STDQTY = qty;
            ret = pdDAL.UpdateCurrentData(UserID, null);
            return ret;
        }

        public static DataTable GetRequistionData(string ReqCode)
        {
            string str = "";
            str = " SELECT LOID,REQDATE, QTY, CODE , PDLOID ";
            str += " FROM V_PRODUCT_REQUEST WHERE CODE ='" + ReqCode + "'";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public bool DeleteData(ArrayList arrLOID)
        {
            bool ret = true;
            OracleDBObj objDB = new OracleDBObj();

            objDB.CreateConnection();
            objDB.CreateTransaction();

            try
            {
                foreach (double PDPLOID in arrLOID)
                {
                    //Delete MaterialItem
                    MaterialItemDAL oDAL = new MaterialItemDAL();
                    double  mtloid = GetMetrialItemLoid(PDPLOID);
                    oDAL.GetDataByLOID(mtloid, objDB.zTrans);
                    oDAL.DeleteCurrentData(objDB.zTrans);

                    //Delete PdProduct
                    PDProductDAL pDAL = new PDProductDAL();
                    pDAL.GetDataByLOID(PDPLOID, objDB.zTrans);
                    pDAL.DeleteCurrentData(objDB.zTrans);

                    //Delete PdOrder
                    PDOrderDAL poDAL = new PDOrderDAL();
                    double poloid = GetPdOrderLoid(PDPLOID);
                    poDAL.GetDataByLOID(poloid, objDB.zTrans);
                    poDAL.DeleteCurrentData(objDB.zTrans);
                }
                objDB.zTrans.Commit();
                ret = true;
            }
            catch (Exception ex)
            {
                objDB.zTrans.Rollback();
                _error = ex.Message;
                ret = false;
            }

            objDB.CloseConnection();
            return ret;
        }

        private double GetMetrialItemLoid(double pdploid)
        {
            string str = "SELECT LOID FROM MATERIALITEM WHERE PDPRODUCT = "+ pdploid ;
            object miloid = OracleDB.ExecSingleCmd(str);
            return Convert.ToDouble(miloid);
        }

        private double GetPdOrderLoid(double pdploid)
        {
            string str = "SELECT PDORDER FROM PDPRODUCT WHERE LOID = " + pdploid;
            object poloid = OracleDB.ExecSingleCmd(str);
            return Convert.ToDouble(poloid);
        }

        public static bool Update_StatusRD(string UserID, PDProductData PdpData, PDOrderData PoData, string PdpLoid, string PoLoid)
        {
            bool ret = true;
            bool rr = true;
            PDProductDAL pdDAL = new PDProductDAL();
            pdDAL.GetDataByLOID(Convert.ToDouble(PdpLoid), null);
            pdDAL.PRODSTATUS  = PdpData.PRODSTATUS;

            if (PdpData.PRODSTATUS == "QS")
            {
                pdDAL.QUARANTINEQTY = PdpData.QUARANTINEQTY;
                pdDAL.QUARANTINEREMARK = PdpData.QUARANTINEREMARK;
                if (PdpData.QUARANTINEDATE.Year.ToString() != "1" & PdpData.QUARANTINEDATE.ToString() != "")
                    pdDAL.QUARANTINEDATE = PdpData.QUARANTINEDATE;
            }
            else if (PdpData.PRODSTATUS == "RD")
            { 
                pdDAL.RADIATEQTY = PdpData.RADIATEQTY;
                pdDAL.RADIATEREMARK = PdpData.RADIATEREMARK;
                pdDAL.RADIATEUNIT = PdpData.RADIATEUNIT;
                if (PdpData.RADIATEDATE.Year.ToString() != "1" & PdpData.RADIATEDATE.ToString() != "")
                    pdDAL.RADIATEDATE = PdpData.RADIATEDATE;
            }
            else if (PdpData.PRODSTATUS == "AP")
            {
                pdDAL.SENDFGREMARK = PdpData.SENDFGREMARK;
                pdDAL.SENDFGQTY = PdpData.SENDFGQTY;
                if (PdpData.SENDFGDATE.Year.ToString() != "1" & PdpData.SENDFGDATE.ToString() != "")
                    pdDAL.SENDFGDATE = PdpData.SENDFGDATE;
            }
            else if (PdpData.PRODSTATUS == "QC")
            {
                if (PdpData.SENDQCDATE.Year.ToString() != "1" & PdpData.SENDQCDATE.ToString() != "")
                    pdDAL.SENDQCDATE = PdpData.SENDQCDATE;
            }
            


            ret = pdDAL.UpdateCurrentData(UserID, null);
            //=========================================================//
            PDOrderDAL PoDAL = new PDOrderDAL();
            PoDAL.GetDataByLOID(Convert.ToDouble(PoLoid), null);
            string status = PoData.STATUS;
            PoDAL.STATUS = PoData.STATUS;
            rr = PoDAL.UpdateCurrentData(UserID, null);
            if (status == Constz.Requisition.Status.RW.Code)
                rr = PoDAL.CutStockQS(Convert.ToDouble(PoLoid), UserID ,null);
            else if (status == Constz.Requisition.Status.XRay.Code)
                rr = PoDAL.CutStockItemQS(Convert.ToDouble(PoLoid), UserID, null);
            else if (status == Constz.Requisition.Status.QC.Code || status == Constz.Requisition.Status.QS.Code)
                rr = PoDAL.CutStock(Convert.ToDouble(PoLoid), UserID, null);

            if (ret == true && rr == true)
                return true;
            else
                return false;
        }

        public static bool Update_StatusRR(string UserID, PDProductData PdpData, PDOrderData PoData, string PdpLoid, string PoLoid)
        {
            bool ret = true;
            bool rr = true;
            PDProductDAL pdDAL = new PDProductDAL();
            pdDAL.GetDataByLOID(Convert.ToDouble(PdpLoid), null);
            pdDAL.PRODSTATUS = PdpData.PRODSTATUS;
            pdDAL.RADIATERETQTY = PdpData.RADIATERETQTY;
            pdDAL.RADIATERETREMARK = PdpData.RADIATERETREMARK;
            pdDAL.RADIATERETUNIT = PdpData.RADIATERETUNIT;

            if (PdpData.RADIATERETDATE.Year.ToString() != "1" || PdpData.RADIATERETDATE.ToString() != "")
                pdDAL.RADIATERETDATE = PdpData.RADIATERETDATE;

            ret = pdDAL.UpdateCurrentData(UserID, null);
            //=========================================================//
            PDOrderDAL PoDAL = new PDOrderDAL();
            PoDAL.GetDataByLOID(Convert.ToDouble(PoLoid), null);
            string status = PoData.STATUS;
            PoDAL.STATUS = PoData.STATUS;
            rr = PoDAL.UpdateCurrentData(UserID, null);
            if (ret == true && rr == true)
                return true;
            else
                return false;
        }

        public string GetReport(double loid)
        {
            string sql = "SELECT REPORTNAME FROM V_REPORT_PRODUCTION WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string report = "";
            if (dt.Rows.Count > 0)
            {
                report = dt.Rows[0]["REPORTNAME"].ToString();

            }

            return report;
        }
        public string GetReportLand(double loid)
        {
            string sql = "SELECT REPORTNAMELANDSCAPE FROM V_REPORT_PRODUCTION WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            string reportLand = "";
            if (dt.Rows.Count > 0)
            {
                reportLand = dt.Rows[0]["REPORTNAMELANDSCAPE"].ToString();

            }

            return reportLand;
        }
        public static double CheckRadiateQty(string loid)
        {
            string sql = "SELECT PDQTY FROM PDPRODUCT WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            double pdqty = 0;
            if (dt.Rows.Count > 0)
            {
                pdqty = Convert.ToDouble(dt.Rows[0]["PDQTY"]);

            }

            return pdqty;
        }
        public static double CheckRadiateReturnQty(string loid)
        {
            string sql = "SELECT RADIATEQTY FROM PDPRODUCT WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            double rqty = 0;
            if (dt.Rows.Count > 0)
            {
                rqty = Convert.ToDouble(dt.Rows[0]["RADIATEQTY"]);

            }

            return rqty;
        }
        public static double CheckQuarantineQty(string loid)
        {
            string sql = "SELECT (PDQTY+RADIATERETQTY-RADIATEQTY) QTY FROM PDPRODUCT WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            double qqty = 0;
            if (dt.Rows.Count > 0)
            {
                qqty = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return qqty;
        }
        public static double CheckSendFGQty(string loid)
        {
            string sql = "SELECT (QUARANTINEQTY-QCQTY1-QCQTY2-QCQTY3) QTY FROM PDPRODUCT WHERE LOID = '" + loid + "' ";

            DataTable dt = OracleDB.ExecListCmd(sql);
            double qqty = 0;
            if (dt.Rows.Count > 0)
            {
                qqty = Convert.ToDouble(dt.Rows[0]["QTY"]);

            }

            return qqty;
        }
    }
}
