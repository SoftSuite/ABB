using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Sales;

namespace ABB.DAL.Sales
{
    public class ToDoListDAL
    {
        public DataTable GetRequisitionList(ToDoListData data)
        {
            string where = "";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.STATUS.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(STATUS) LIKE '%" + data.STATUS.Trim().ToUpper() + "%' ";

            if (data.RESERVEDATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(RESERVEDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.RESERVEDATEFROM) + " ";

            if (data.RESERVEDATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(RESERVEDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.RESERVEDATETO) + " ";

            if (data.CUSTOMER != 0)
                where += (where == "" ? "" : "AND ") + "CUSTOMER =" + data.CUSTOMER.ToString() + " ";

            string sql = "SELECT LOID, CODE, RESERVEDATE, CONFIRMDATE, DUEDATE, CUSTOMER, CUSTOMERNAME, GRANDTOT, STATUS, ";
            sql += "CASE STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Reserve.Code + "' THEN '" + Constz.Requisition.Status.Reserve.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "ELSE '' END STATUSNAME ";
            sql += "FROM V_TODOLIST_SALE ";
            sql += (where == "" ? "" : "WHERE ") + where + " ";
            sql += "ORDER BY CODE ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
