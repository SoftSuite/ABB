using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ABB.Data.Production;
using ABB.DAL;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;


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

namespace ABB.DAL.Production
{
    public class PDProductSearchDAL
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public static ArrayList GetPDProductSearch(string LotNo, string DateFrom, string DateTo, string PDName)
        {
            ArrayList arrResult = new ArrayList();
            string str = "";
            string whr = "";
            str += " SELECT DISTINCT PDPLOID, PDLOID, LOTNO, TO_DATE(MFGDATE,'DD/MM/YYYY') MFGDATE, PDNAME, BATCHSIZE, STDQTY ,PDQTY, ";
            str += " UNAME, PRODSTATUS, ";
            str += " CASE WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.WA.Code + "' THEN '" + Constz.ProductionStatus.Status.WA.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.RW.Code + "' THEN '" + Constz.ProductionStatus.Status.RW.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.RD.Code + "' THEN '" + Constz.ProductionStatus.Status.RD.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.QS.Code + "' THEN '" + Constz.ProductionStatus.Status.QS.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.QC.Code + "' THEN '" + Constz.ProductionStatus.Status.QC.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.QB.Code + "' THEN '" + Constz.ProductionStatus.Status.QB.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.RR.Code + "' THEN '" + Constz.ProductionStatus.Status.RR.Name.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.AP.Code + "' THEN '" + Constz.ProductionStatus.Status.AP.Name.ToString() + "' ";
            str += " ELSE ' ' END PRODSTATUSNAME, ";
            str += " CASE WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.WA.Code + "' THEN '" + Constz.ProductionStatus.Status.WA.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.RW.Code + "' THEN '" + Constz.ProductionStatus.Status.RW.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.RD.Code + "' THEN '" + Constz.ProductionStatus.Status.RD.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.QS.Code + "' THEN '" + Constz.ProductionStatus.Status.QS.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.QC.Code + "' THEN '" + Constz.ProductionStatus.Status.QC.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.QB.Code + "' THEN '" + Constz.ProductionStatus.Status.QB.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.RR.Code + "' THEN '" + Constz.ProductionStatus.Status.RR.Rank.ToString() + "' ";
            str += " WHEN PRODSTATUS = '" + Constz.ProductionStatus.Status.AP.Code + "' THEN '" + Constz.ProductionStatus.Status.AP.Rank.ToString() + "' ";
            str += " ELSE ' ' END RANK ";
            str += " FROM V_PDPRODUCT_LIST ";

            if (DateFrom.Trim() != "1/1/1" && DateTo.Trim() != "1/1/1")
                whr += (whr == "" ? "WHERE " : "AND ") + " MFGDATE BETWEEN TO_DATE('" + DateFrom + "','DD/MM/YYYY') AND TO_DATE('" + DateTo + "','DD/MM/YYYY')";

            if (LotNo.Trim() != "")
                whr += (whr == "" ? "WHERE " : "AND ") + " LOTNO = '" + LotNo + "'";

            if (PDName.Trim() != "")
                whr += (whr == "" ? "WHERE " : "AND ") + " UPPER(PDNAME) LIKE UPPER('%" + PDName + "%')";

            str += whr;
            str += " ORDER BY MFGDATE,PDNAME,LOTNO ";
            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    PDProductSearchData irData = new PDProductSearchData();
                    irData.ORDERNO = i;
                    irData.PDPLOID = Convert.ToDouble(zRd["PDPLOID"]);
                    irData.PDLOID = Convert.ToDouble(zRd["PDLOID"]);
                    irData.LOTNO = zRd["LOTNO"].ToString();
                    if (!Convert.IsDBNull(zRd["MFGDATE"])) irData.MFGDATE = Convert.ToDateTime(zRd["MFGDATE"]);
                    irData.PDNAME = zRd["PDNAME"].ToString();
                    irData.BATCHSIZE = Convert.ToDouble(zRd["BATCHSIZE"]);
                    irData.STDQTY = Convert.ToDouble(zRd["STDQTY"]);
                    irData.PDQTY = Convert.ToDouble(zRd["PDQTY"]);
                    irData.UNAME = zRd["UNAME"].ToString();
                    irData.PRODSTATUS = zRd["PRODSTATUS"].ToString();
                    irData.PRODSTATUSNAME = zRd["PRODSTATUSNAME"].ToString();
                    irData.RANK = Convert.ToDouble(zRd["RANK"]);
                    arrResult.Add(irData);
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arrResult;
        }

        public static DataTable GetPdProductData(string PdLoid, string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.LOTNO, PDP.MFGDATE, PDP.PRODUCT, PD.PACKSIZE, UPZ.LOID UNITPZ, ";
            str += " UBZ.LOID UNITBZ, PO.REMARK , PDP.BATCHSIZE ,PO.LOID POLOID,NVL(RQ.REQDATE,'') REQDATE, ";
            str += " NVL(RQ.CODE,'') RQCODE , PDP.PRODSTATUS, PO.STATUS POSTATUS, ";
            str += " PDP.PRODSTATUS, PO.STATUS POSTATUS , PD.UNIT PDUNIT, PDP.PRODUCETYPE, PDP.TOWAREHOUSE ";
            str += " FROM PDPRODUCT PDP INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN UNIT UBZ ON UBZ.LOID = PDP.BATCHSIZEUNIT ";
            str += " LEFT JOIN REQUISITIONITEM RQI ON RQI.LOID = PDP.REFLOID ";
            str += " LEFT JOIN REQUISITION RQ ON RQ.LOID = RQI.REQUISITION ";
            str += " LEFT JOIN UNIT UPZ ON UPZ.LOID = PD.PACKSIZEUNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid + " AND PDP.PRODUCT =" + PdLoid + "";

            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetMaterialData(string PdpLoid)
        {
            string str = "";
            str = " SELECT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME,PRODSTATUS, POSTATUS ,ALLQTY USEQTY2,MLOTNO ";
            str += " FROM V_PRODUCT_MATERIAL ";
            str += " WHERE PDPLOID =" + PdpLoid + "";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProduct(string type)
        {
            string str = "";
            str = " SELECT PD.LOID LOID, PD.NAME NAME";
            str += " FROM V_MAINPRODUCT_BOM PD INNER JOIN PRODUCTGROUP PG ON PD.PRODUCTGROUP = PG.LOID ";
            str += " INNER JOIN PRODUCTTYPE PT ON PG.PRODUCTTYPE = PT.LOID ";
            str += " WHERE PT.TYPE ='" + type + "' ";
            str += " ORDER BY PD.NAME ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProductionFillData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.PACKING, PDP.PACKAGE, PD.PACKSIZE,U.NAME UNAME, PDP.STDQTY, ";
            str += " PDP.PDQTY, PDP.LOSTQTY LOST, NVL(PDP.YIELD,0) YIELD,U1.NAME PDUNAME, ";
            str += " PDP.MFGDATE, PDP.EXPDATE,PDP.LOID PDPLOID, PO.LOID POLOID, ";
            str += " PD.LOID PDLOID , U.LOID ULOID, PDP.PRODSTATUS, PO.STATUS POSTATUS ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.PACKSIZEUNIT ";
            str += " INNER JOIN UNIT U1 ON U1.LOID = PD.UNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetRadiationData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.RADIATEDATE,PDP.RADIATEQTY,PDP.RADIATEUNIT,PDP.RADIATEREMARK, ";
            str += " PDP.LOID PDPLOID, PD.LOID PDLOID, U.LOID ULOID,PO.LOID POLOID, PDP.PRODSTATUS, ";
            str += " PO.STATUS POSTATUS ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID = PDP.BATCHSIZEUNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetRadiationReturnData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.RADIATERETDATE,PDP.RADIATERETQTY,PDP.RADIATERETUNIT, ";
            str += " PDP.RADIATERETREMARK, PDP.LOID PDPLOID, PD.LOID PDLOID, U.LOID ULOID,PO.LOID POLOID, ";
            str += " PDP.PRODSTATUS, PO.STATUS POSTATUS ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID = PDP.BATCHSIZEUNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetStockInDetailData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.QUARANTINEDATE, PDP.QUARANTINEQTY, PDP.QUARANTINEUNIT,PDP.QUARANTINEREMARK, ";
            str += " PDP.LOID PDPLOID, PD.LOID PDLOID, U.LOID ULOID,PO.LOID POLOID, ";
            str += " PDP.PRODSTATUS, PO.STATUS POSTATUS , PDP.RADIATERETDATE ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.PACKSIZEUNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetSendQCData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.SENDQCDATE, PDP.QCQTY1, U.LOID UNIT1, PDP.QCQTY2,PDP.QCQTY3, PDP.QCRESULT, ";
            str += " PDP.QCREMARK,PDP.LOID PDPLOID, PO.LOID POLOID, PD.LOID PDLOID, U.LOID ULOID, ";
            str += " PDP.PRODSTATUS, PO.STATUS POSTATUS ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.UNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetStockOutDetailData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDP.SENDFGDATE,PDP.LOSTQTY LOST, U.LOID ULOID, ";
            str += " PDP.SENDFGQTY,PDP.SENDFGREMARK, PDP.LOID PDPLOID, PO.LOID POLOID, ";
            str += " PD.LOID PDLOID, U.LOID ULOID , PDP.PRODSTATUS, PO.STATUS POSTATUS,QCRESULT ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER AND PO.ORDERTYPE = 'PD' ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.UNIT ";
            str += " WHERE PDP.LOID = " + PdpLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetMaterialLostData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME ,BMASTER, RQMASTER ,PRODSTATUS , POSTATUS ,ALLQTY USEQTY2 ";
            str += " FROM V_PRODUCT_MATERIALLOST ";
            str += " WHERE PDPLOID =" + PdpLoid + " AND PGROUPNAME = 'WH' ";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetPackageLostData(string PdpLoid)
        {
            string str = "";
            str = " SELECT DISTINCT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME ,BMASTER, RQMASTER ,PRODSTATUS , POSTATUS , ALLQTY USEQTY2";
            str += " FROM V_PRODUCT_MATERIALLOST ";
            str += " WHERE PDPLOID =" + PdpLoid + " AND PGROUPNAME = 'PC' ";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetPdProduct(double PdpLoid)
        {
            string str = "";
            str = "SELECT PDP.LOID PDPLOID,PDP.LOTNO, PDP.MFGDATE,PDP.PRODUCT ,PD.PACKSIZE,  ";
            str += " UPZ.LOID UNITPZ,PDP.BATCHSIZE, PDP.BATCHSIZEUNIT,PO.LOID POLOID,RQ.CODE RQCODE, ";
            str += " RQ.REQDATE, PD.UNIT PDUNIT, PDP.PRODUCETYPE, PDP.TOWAREHOUSE ";
            str += " FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PO.LOID = PDP.PDORDER ";
            str += " AND PO.ORDERTYPE = 'PD'  ";
            str += " LEFT JOIN REQUISITIONITEM RQI ON RQI.LOID = PDP.REFLOID AND PDP.REFTABLE = 'REQUISITIONITEM' ";
            str += " LEFT JOIN REQUISITION RQ ON RQ.LOID = RQI.REQUISITION ";
            str += " LEFT JOIN PRODUCT PD ON PD.LOID = PDP.PRODUCT ";
            str += " LEFT JOIN UNIT UPZ ON UPZ.LOID = PD.PACKSIZEUNIT ";
            str += " WHERE PDP.LOID =" + PdpLoid + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProductDetail(string PdLoid)
        {
            string str = "";
            str = " SELECT PACKSIZE, NVL(U.LOID,0) ULOID, PRODUCT.UNIT PDUNIT,NVL(AGE,0) AGE  ";
            str += " FROM PRODUCT  LEFT JOIN UNIT U ON U.LOID = PRODUCT.PACKSIZEUNIT ";
            str += " WHERE PRODUCT.LOID = " + PdLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetRequisitionItemDetail(string RqiLoid)
        {
            string str = "";
            str = " SELECT DISTINCT RQ.CODE RQCODE,TO_CHAR(RQ.REQDATE,'DD/MM/YYYY') REQDATE, VM.LOID PRODUCT ,PD.NAME ,";
            str += " RQI.QTY * PD.PACKSIZE BATCHQTY, PD.PACKSIZEUNIT "; 
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION = RQ.LOID ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            str += " LEFT JOIN V_MAINPRODUCT_BOM VM ON PD.LOID = VM.LOID ";
            str += " WHERE RQI.LOID = " + RqiLoid;
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetRequisitionItemDetailByToDoList(string RqLoid,string pdloid)
        {
            string str = "";
            str = " SELECT DISTINCT RQ.CODE RQCODE,TO_CHAR(RQ.REQDATE,'DD/MM/YYYY') REQDATE, VM.LOID PRODUCT ,PD.NAME,";
            str += " RQI.QTY * PD.PACKSIZE BATCHQTY, PD.PACKSIZEUNIT ,RQI.QTY ,RQI.UNIT RQIUNIT ,RQI.LOID RQILOID ";
            str += " FROM REQUISITION RQ INNER JOIN REQUISITIONITEM RQI ON RQI.REQUISITION = RQ.LOID ";
            str += " INNER JOIN PRODUCT PD ON PD.LOID = RQI.PRODUCT ";
            str += " LEFT JOIN V_MAINPRODUCT_BOM VM ON PD.LOID = VM.LOID ";
            str += " WHERE RQ.LOID = " + RqLoid + " AND PD.LOID = " + pdloid + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetMaterialReLoadData(double PdLoid, double PDPLOID)
        {
            string str = "";
            str = " SELECT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME, PRODSTATUS, POSTATUS ,ALLQTY USEQTY2,MLOTNO ";
            str += " FROM V_PRODUCT_MATERIAL ";
            str += " WHERE PDLOID =" + PdLoid + " AND PDPLOID = " + PDPLOID + "";
            str += " ORDER BY MTRLOID ";

            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetMaterialLostReLoadData(double PdLoid, double PDPLOID)
        {
            string str = "";
            str = " SELECT DISTINCT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME ,BMASTER, RQMASTER,PRODSTATUS, POSTATUS ,ALLQTY USEQTY2 ";
            str += " FROM V_PRODUCT_MATERIALLOST ";
            str += " WHERE PDLOID =" + PdLoid + " AND PDPLOID = " + PDPLOID + " AND PGROUPNAME = 'WH' ";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable CheckMaterialItem(double PdLoid)
        {
            string str = "";
            str = " SELECT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME ";
            str += " FROM V_MATERIALCTRL ";
            str += " WHERE PDLOID =" + PdLoid + "";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable CheckMaterialItemWithReq(double PdLoid)
        {
            string str = "";
            str = " SELECT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME ";
            str += " FROM V_MATERIALCTRLWITHREQ ";
            str += " WHERE PDLOID =" + PdLoid + "";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetPackageLostReLoadData(double PdLoid, double PDPLOID)
        {
            string str = "";
            str = " SELECT DISTINCT PDPLOID, LOTNO, PROCESS, MTRLOID, MTRCODE, BARCODE, ABBNAME, MTRNAME, MASTER, UNAME, ";
            str += " MTRCODE, BARCODE, ABBNAME, MTRNAME, PCACTIVE , ALLQTY, PGLOID, ULOID, USEQTY, BLOID,UNIT,  ";
            str += " WASTEQTYMAT, WASTEQTYMAN, RETURNQTY,CHANGEQTY, ACTIVE, REMARK, YIELDMAT, YIELDMAM, PGROUP,";
            str += " PGROUPNAME ,BMASTER, RQMASTER,PRODSTATUS, POSTATUS,ALLQTY USEQTY2  ";
            str += " FROM V_PRODUCT_MATERIALLOST ";
            str += " WHERE PDLOID =" + PdLoid + " AND PDPLOID = " + PDPLOID + " AND PGROUPNAME <> 'WH' ";
            str += " ORDER BY MTRLOID ";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

    }
}
