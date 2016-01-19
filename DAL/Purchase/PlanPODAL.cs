using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.Data;
using ABB.Data.Purchase;

namespace ABB.DAL.Purchase
{
    public class PlanPODAL
    {

        public DataTable GetPlanList()
        {
            string sql = "SELECT DISTINCT 0 ORDERNO, YEAR ";
            sql += "FROM PLAN WHERE STATUS <> '" + Constz.Requisition.Status.Void.Code + "' ";
            sql += "AND PLANTYPE IN ('" + Constz.PlanType.FG + "', '" + Constz.PlanType.WH + "') ORDER BY YEAR DESC ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetPlanDetailList(PlanDetailSearchData data)
        {
            string sql = "SELECT V.* FROM V_PLANPURCHASE V ";
            string where = "";
            if (data.YEAR != 0)
                where += (where == "" ? "" : " AND ") + "V.YEAR = " + data.YEAR.ToString() + " ";

            if (data.MONTH != 0)
                where += (where == "" ? "" : " AND ") + "V.MONTH = " + data.MONTH.ToString() + " ";

            if (data.PRODUCTNAME != "")
                where += (where == "" ? "" : " AND ") + "UPPER(V.PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.ToUpper() + "%' ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : " AND ") + "V.PGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : " AND ") + "V.PTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            sql += (where == "" ? "" : "WHERE " + where) + " ORDER BY PLAN, PRODUCTNAME ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
