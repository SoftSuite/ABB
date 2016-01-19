using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.DAL.Sales
{
    public class PlanForSaleDAL
    {
        public DataTable GetPlanSaleList(PlanOrderSearchData data)
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
            string where = "PLANTYPE = '" + Constz.PlanType.SA + "' ";
            if (data.YEARFROM != "")
                where += (where == "" ? "" : "AND ") + "YEAR >= '" + data.YEARFROM + "' ";

            if (data.YEARTO != "" )
                where += (where == "" ? "" : "AND ") + "YEAR <= '" + data.YEARFROM + "' ";

            if (data.CREATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CREATEON, 'DD/MM/YYYY') >= " + OracleDB.QRDate(data.CREATEFROM) + " ";

            if (data.CREATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CREATEON, 'DD/MM/YYYY') <= " + OracleDB.QRDate(data.CREATETO) + " ";

            if (data.CONFIRMFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CONFIRMDATE, 'DD/MM/YYYY') >= " + OracleDB.QRDate(data.CONFIRMFROM) + " ";

            if (data.CONFIRMTO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CONFIRMDATE, 'DD/MM/YYYY') <= " + OracleDB.QRDate(data.CONFIRMTO) + " ";

            if (data.STATUSFROM != "")
                where += (where == "" ? "" : "AND ") + "RANK >= '" + data.STATUSFROM + "' ";

            if (data.STATUSTO != "")
                where += (where == "" ? "" : "AND ") + "RANK <= '" + data.STATUSTO + "' ";

            sql += (where == "" ? "" : "WHERE " + where) + " ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanMarketList(PlanOrderSearchData data)
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
            string where = "PLANTYPE = '" + Constz.PlanType.MK + "' ";
            if (data.YEARFROM != "")
                where += (where == "" ? "" : "AND ") + "YEAR >= '" + data.YEARFROM + "' ";

            if (data.YEARTO != "")
                where += (where == "" ? "" : "AND ") + "YEAR <= '" + data.YEARFROM + "' ";

            if (data.CREATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CREATEON, 'DD/MM/YYYY') >= " + OracleDB.QRDate(data.CREATEFROM) + " ";

            if (data.CREATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CREATEON, 'DD/MM/YYYY') <= " + OracleDB.QRDate(data.CREATETO) + " ";

            if (data.CONFIRMFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CONFIRMDATE, 'DD/MM/YYYY') >= " + OracleDB.QRDate(data.CONFIRMFROM) + " ";

            if (data.CONFIRMTO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(CONFIRMDATE, 'DD/MM/YYYY') <= " + OracleDB.QRDate(data.CONFIRMTO) + " ";

            if (data.STATUSFROM != "")
                where += (where == "" ? "" : "AND ") + "RANK >= '" + data.STATUSFROM + "' ";

            if (data.STATUSTO != "")
                where += (where == "" ? "" : "AND ") + "RANK <= '" + data.STATUSTO + "' ";

            sql += (where == "" ? "" : "WHERE " + where) + " ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanSaleItemList(PlanItemSearchData data)
        {
            string where = "";
            if (data.PLAN != 0)
                where += (where == "" ? "" : "AND ") + "PI.PLAN = " + data.PLAN.ToString() + " ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "P.PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PG.PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.PRODUCTNAME != "")
                where += (where == "" ? "" : "AND ") + "upper(P.NAME) LIKE '%" + data.PRODUCTNAME.ToUpper() + "%' ";

            string sql = "SELECT PI.LOID, PI.PRODUCT, P.NAME PRODUCTNAME, P.PRODUCTGROUP, PG.NAME GROUPNAME, PT.NAME TYPENAME, ";
            sql += "PI.M1, PI.M2, PI.M3, PI.M4, PI.M5, PI.M6, PI.M7, PI.M8, PI.M9, PI.M10, PI.M11, PI.M12, PI.STATUS ";
            sql += "FROM PLANORDER PI INNER JOIN PRODUCT P ON P.LOID = PI.PRODUCT ";
            sql += "INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            sql += (where == "" ? "" : "WHERE " + where) + "ORDER BY P.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanMKItemList(PlanItemSearchData data)
        {
            string where = "";
            if (data.PLAN != 0)
                where += (where == "" ? "" : "AND ") + "PM.PLAN = " + data.PLAN.ToString() + " ";

            string sql = "SELECT PM.LOID,PM.PLAN, PM.CUSTOMER,CASE WHEN PM.CUSTOMER = '-1' THEN 'Í×è¹æ' ELSE C.NAME END AS CUSTOMERNAME,PM.PERCENT, ";
            sql += "PM.M1,PM.M2,PM.M3,PM.M4,PM.M5,PM.M6,PM.M7,PM.M8,PM.M9,PM.M10,PM.M11,PM.M12,PM.RANK,PM.STATUS ";
            sql += "FROM PLANMARKET PM ";
            sql += "LEFT JOIN  CUSTOMER C ON PM.CUSTOMER = C.LOID ";
            sql += (where == "" ? "" : "WHERE " + where) + "ORDER BY PM.RANK ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductPlanList(string where, System.Data.OracleClient.OracleTransaction trans)
        {
            string sql = "SELECT P.LOID, P.UNIT, P.PRODUCTMASTER ";
            sql += "FROM PRODUCT P INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP AND P.ACTIVE = '" + Constz.ActiveStatus.Active + "' ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE AND PT.TYPE = '" + Constz.ProductType.Type.FG.Code + "' ";
            sql += (where == "" ? "" : "WHERE " + where) + " ";
            return OracleDB.ExecListCmd(sql, trans);
        }

        public DataTable GetAllCustomerList(string where, System.Data.OracleClient.OracleTransaction trans)
        {
            string sql = "SELECT * FROM (SELECT ROWNUM RANK, LOID, NAME FROM (SELECT LOID, NAME FROM CUSTOMER WHERE MEMBERTYPE = 71 ORDER BY NAME)A ";
            sql += "UNION SELECT 0 RANK, 1 LOID, 'ÃéÒ¹ÊÁØ¹ä¾ÃÍÀÑÂÀÙàºÈÃ' NAME FROM DUAL ";
            sql += "UNION SELECT 999 RANK, -1 LOID, 'Í×è¹æ' NAME FROM DUAL)A ";
            sql += (where == "" ? "" : "WHERE " + where) + " ";
            return OracleDB.ExecListCmd(sql, trans);
        }

        public DataTable GetPlanOrderData(double planOrder)
        {
            string sql = "SELECT PO.LOID, PO.PLAN, PO.STATUS, PO.PRODUCT, PO.UNIT, PO.STATUS, P.NAME PRODUCTNAME, UNIT.NAME UNITNAME ";
            sql += "FROM PLANORDER PO INNER JOIN PRODUCT P ON P.LOID = PO.PRODUCT ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = PO.UNIT ";
            sql += "WHERE PO.LOID =  " + planOrder.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanOrderSaleList(double plan, double product, int month)
        {
            string sql = "SELECT 0 AS RANK, PS.SALEMAN, S.NAME SALENAME, PS.M" + month.ToString() + " QTY ";
            sql += "FROM PLANORDERSALE PS INNER JOIN SALEMAN S ON S.LOID = PS.SALEMAN ";
            sql += "WHERE PS.M" + month.ToString() + " > 0 AND PS.PLAN = " + plan.ToString() + " AND PRODUCT = " + product.ToString() + " ";
            sql += "ORDER BY S.NAME ";
            return OracleDB.ExecListCmd(sql);
        }

        public bool HasUnplanedProduct(double plan, System.Data.OracleClient.OracleTransaction trans)
        {
            bool ret = false;
            string sql = "SELECT COUNT(LOID) FROM PLANORDER ";
            sql += "WHERE M1+M2+M3+M4+M5+M6+M7+M8+M9+M10+M11+M12 <=0 AND PLAN = " + plan.ToString() + " ";
            ret = Convert.ToDouble(OracleDB.ExecSingleCmd(sql, trans)) > 0;
            return ret;
        }

    }
}
