using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.Data;
using ABB.Data.Inventory.WH;

namespace ABB.DAL.Inventory.WH
{
    public class PlanWHDAL
    {
        public DataTable GetPlanList(PlanSearchData data)
        {
            string sql = "SELECT * FROM (SELECT 0 ORDERNO, LOID, CREATEBY, CREATEON, CODE, PLANTYPE, CONFIRMDATE, YEAR, ";
            sql += "CASE STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "ELSE '' END AS STATUSNAME, ";
            sql += "CASE STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Rank + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Rank + "' ";
            sql += "ELSE '0' END AS RANK ";
            sql += "FROM PLAN) A ";
            string where = "PLANTYPE = '" + Constz.PlanType.WH + "' ";
            if (data.YEARFROM != "")
                where += (where == "" ? "" : "AND ") + "YEAR >= '" + data.YEARFROM + "' ";

            if (data.YEARTO != "")
                where += (where == "" ? "" : "AND ") + "YEAR <= '" + data.YEARFROM + "' ";

            if (data.CREATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(CREATEON, 'YYYYMMDD') >= TO_CHAR(" + OracleDB.QRDate(data.CREATEFROM) + ", 'YYYYMMDD') ";

            if (data.CREATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(CREATEON, 'YYYYMMDD') <= TO_CHAR(" + OracleDB.QRDate(data.CREATETO) + ", 'YYYYMMDD') ";

            if (data.CONFIRMFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(CONFIRMDATE, 'YYYYMMDD') >= TO_CHAR(" + OracleDB.QRDate(data.CONFIRMFROM) + ", 'YYYYMMDD') ";

            if (data.CONFIRMTO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(CONFIRMDATE, 'DD/MM/YYYY') <= TO_CHAR(" + OracleDB.QRDate(data.CONFIRMTO) + ", 'YYYYMMDD') ";

            if (data.STATUSFROM != "")
                where += (where == "" ? "" : "AND ") + "RANK >= '" + data.STATUSFROM + "' ";

            if (data.STATUSTO != "")
                where += (where == "" ? "" : "AND ") + "RANK <= '" + data.STATUSTO + "' ";

            sql += (where == "" ? "" : "WHERE " + where) + " ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanDetailList(PlanDetailSearchData data)
        {
            string sql = "SELECT V.* FROM V_PLANWH V LEFT JOIN PLANREMAIN R ON R.PLAN = V.PLAN AND R.PRODUCT = V.PRODUCT AND R.MONTH = V.MONTH ";
            sql += "INNER JOIN PLANPRODUCE P ON P.PLAN = V.PLAN AND P.PRODUCT = V.PRODUCT AND P.MONTH = V.MONTH ";
            sql += "INNER JOIN PLANPURCHASE S ON S.PLAN = V.PLAN AND S.PRODUCT = V.PRODUCT AND S.MONTH = V.MONTH ";
            string where = "";
            if (data.PLAN != 0)
                where += (where == "" ? "" : " AND ") + "V.PLAN = " + data.PLAN.ToString() + " ";

            if (data.MONTH != 0)
                where += (where == "" ? "" : " AND ") + "V.MONTH = " + data.MONTH.ToString() + " ";

            if (data.PRODUCTNAME != "")
                where += (where == "" ? "" : " AND ") + "UPPER(V.PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : " AND ") + "V.PGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : " AND ") + "V.PTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            switch (data.PRODUCTSTATUS)
            {
                case Constz.PlanProductStatus.Minimum:
                    where += (where == "" ? "" : " AND ") + "";
                    where += "(R.DAY1 < NVL(MINIMUM,0) OR R.DAY2 < NVL(MINIMUM,0) OR R.DAY3 < NVL(MINIMUM,0) OR R.DAY4 < NVL(MINIMUM,0) OR R.DAY5 < NVL(MINIMUM,0) OR ";
                    where += "R.DAY6 < NVL(MINIMUM,0) OR R.DAY7 < NVL(MINIMUM,0) OR R.DAY8 < NVL(MINIMUM,0) OR R.DAY9 < NVL(MINIMUM,0) OR R.DAY10 < NVL(MINIMUM,0) OR ";
                    where += "R.DAY11 < NVL(MINIMUM,0) OR R.DAY12 < NVL(MINIMUM,0) OR R.DAY13 < NVL(MINIMUM,0) OR R.DAY14 < NVL(MINIMUM,0) OR R.DAY15 < NVL(MINIMUM,0) OR ";
                    where += "R.DAY16 < NVL(MINIMUM,0) OR R.DAY17 < NVL(MINIMUM,0) OR R.DAY18 < NVL(MINIMUM,0) OR R.DAY19 < NVL(MINIMUM,0) OR R.DAY20 < NVL(MINIMUM,0) OR ";
                    where += "R.DAY21 < NVL(MINIMUM,0) OR R.DAY22 < NVL(MINIMUM,0) OR R.DAY23 < NVL(MINIMUM,0) OR R.DAY24 < NVL(MINIMUM,0) OR R.DAY25 < NVL(MINIMUM,0) OR ";
                    where += "R.DAY26 < NVL(MINIMUM,0) OR R.DAY27 < NVL(MINIMUM,0) OR R.DAY28 < NVL(MINIMUM,0) OR R.DAY29 < NVL(MINIMUM,0) OR R.DAY30 < NVL(MINIMUM,0) OR ";
                    where += "R.DAY31 < NVL(MINIMUM,0) ) ";
                    break;

                case Constz.PlanProductStatus.Produce:
                    where += (where == "" ? "" : " AND ") + "";
                    where += "(P.DAY1>0 OR P.DAY2>0 OR P.DAY3>0 OR P.DAY4>0 OR P.DAY5>0 OR ";
                    where += "P.DAY6>0 OR P.DAY7>0 OR P.DAY8>0 OR P.DAY9>0 OR P.DAY10>0 OR ";
                    where += "P.DAY11>0 OR P.DAY12>0 OR P.DAY13>0 OR P.DAY14>0 OR P.DAY15>0 OR ";
                    where += "P.DAY16>0 OR P.DAY17>0 OR P.DAY18>0 OR P.DAY19>0 OR P.DAY20>0 OR ";
                    where += "P.DAY21>0 OR P.DAY22>0 OR P.DAY23>0 OR P.DAY24>0 OR P.DAY25>0 OR ";
                    where += "P.DAY26>0 OR P.DAY27>0 OR P.DAY28>0 OR P.DAY29>0 OR P.DAY30>0 OR ";
                    where += "P.DAY31>0 ) ";
                    break;

                case Constz.PlanProductStatus.Purchase:
                    where += (where == "" ? "" : " AND ") + "";
                    where += "(S.DAY1>0 OR S.DAY2>0 OR S.DAY3>0 OR S.DAY4>0 OR S.DAY5>0 OR ";
                    where += "S.DAY6>0 OR S.DAY7>0 OR S.DAY8>0 OR S.DAY9>0 OR S.DAY10>0 OR ";
                    where += "S.DAY11>0 OR S.DAY12>0 OR S.DAY13>0 OR S.DAY14>0 OR S.DAY15>0 OR ";
                    where += "S.DAY16>0 OR S.DAY17>0 OR S.DAY18>0 OR S.DAY19>0 OR S.DAY20>0 OR ";
                    where += "S.DAY21>0 OR S.DAY22>0 OR S.DAY23>0 OR S.DAY24>0 OR S.DAY25>0 OR ";
                    where += "S.DAY26>0 OR S.DAY27>0 OR S.DAY28>0 OR S.DAY29>0 OR S.DAY30>0 OR ";
                    where += "S.DAY31>0 ) ";
                    break;
            }

            sql += (where == "" ? "" : "WHERE " + where) + " ORDER BY PLAN, PRODUCTNAME, RANK ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanDetailData(double plan, int month, int day, double product)
        {
            string sql = "SELECT PLAN.YEAR-543 YEAR, R.MONTH, " + day + " DAY, R.PLAN, R.PRODUCT, RI.PDLOID, RI.POLOID, ";
            sql += "PD.NAME PRODUCTNAME, UNIT.NAME UNITNAME, PLAN.STATUS, NVL(M.MINIMUM, 0) MINIMUM, NVL(M.MAXIMUM,0) MAXIMUM, ";
            sql += "PD.LOTSIZE POLOTSIZE, PD.LOTSIZEPD PDLOTSIZE, R.LOID RECEIVELOID, PD.LEADTIME POLEADTIME, PD.LEADTIMEPD PDLEADTIME, RI.PDQTY, RI.POQTY ";
            sql += "FROM PLAN INNER JOIN PLANRECEIVE R ON R.PLAN = PLAN.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PD.LOID = R.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = R.UNIT ";
            sql += "LEFT JOIN PLANRECEIVEITEM RI ON RI.PLAN = PLAN.LOID AND RI.PRODUCT = R.PRODUCT ";
            sql += "AND TO_CHAR(RI.RECEIVEDATE, 'YYYYMMDD') = TRIM(TO_CHAR(PLAN.YEAR-543)) || TRIM(TO_CHAR(R.MONTH, '00')) || '" + day.ToString("00") + "' ";
            sql += "LEFT JOIN PRODUCTMINMAX M ON M.PRODUCT = PD.PRODUCTMASTER ";
            sql += "WHERE PLAN.LOID = " + plan.ToString() + " AND R.MONTH = " + month.ToString() + " AND R.PRODUCT = " + product.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public static DataTable GetProductList(double plan, double product, int month, int day)
        {
            string sql = "SELECT 0 RANK, PRODUCTNAME, DAY" + day.ToString() + " QTY FROM V_PLANWHPRODUCT ";
            sql += "WHERE PLAN = " + plan.ToString() + " AND MATERIAL = " + product.ToString() + " AND MONTH = " + month.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
