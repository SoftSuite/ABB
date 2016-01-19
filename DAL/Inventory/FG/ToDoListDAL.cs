using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data;
using ABB.Data.Inventory.FG;

namespace ABB.DAL.Inventory.FG
{
    public class ToDoListDAL
    {
        public DataTable GetMinimumStockList(ToDoListMinimumStockData data)
        {
            string where = "";
            if (data.ORDERTYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "ORDERTYPE = '" + data.ORDERTYPE + "' ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(NAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            if (data.STATUS.Trim() != "")
                where += (where == "" ? "" : "AND ") + "STATUS = '" + data.STATUS + "' ";

            if (data.WAREHOUSE != 0)
                where += (where == "" ? "" : "AND ") + "WAREHOUSE = " + data.WAREHOUSE.ToString() + " ";

            string sql = "SELECT WAREHOUSE, LOID, BARCODE, NAME, NVL(QTY,0) QTY, UNITNAME, LEADTIME, NVL(LOTSIZE,0) LOTSIZE, MINIMUM, REQUESTID, REQUESTCODE, ORDERTYPE, UNIT, REFTABLE, ";
            sql += "CASE ORDERTYPE WHEN '" + Constz.OrderType.PD.Code + "' THEN '" + Constz.OrderType.PD.Name + "' ";
            sql += "WHEN '" + Constz.OrderType.PO.Code + "' THEN '" + Constz.OrderType.PO.Name + "' ";
            sql += "ELSE '' END ORDERTYPENAME, CASE STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "ELSE '' END STATUS ";
            sql += "FROM V_TODOLIST_FG_MINSTOCK ";
            sql += (where == "" ? "" : " WHERE ") + where;
            sql += "ORDER BY BARCODE,  REQUESTCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockInList(ToDoListStockInData data)
        {
            string where = "";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(REQUESTCODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.DUEDATE.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(DUEDATE, 'YYYYMMDD') = TO_CHAR(" + OracleDB.QRDate(data.DUEDATE) + ", 'YYYYMMDD') ";

            if (data.ORDERTYPE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "ORDERTYPE = '" + data.ORDERTYPE + "' ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(NAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            if (data.STATUS.Trim() != "")
                where += (where == "" ? "" : "AND ") + "STATUS = '" + data.STATUS + "' ";

            //if (data.WAREHOUSE != 0)
            //    where += (where == "" ? "" : "AND ") + "WAREHOUSE = " + data.WAREHOUSE.ToString() + " ";

            string sql = "SELECT LOID, PRODUCT, DUEDATE, NAME, CODE, QTY, REMAIN, UNIT, UNITNAME, PRICE, RECEIVE, ORDERTYPE, SUPPLIER, SUPPLIERNAME, REQUESTCODE, ";
            sql += "REQUESTID, CASE ORDERTYPE WHEN '" + Constz.OrderType.PD.Code + "' THEN '" + Constz.OrderType.PD.Name + "' ";
            sql += "WHEN '" + Constz.OrderType.PO.Code + "' THEN '" + Constz.OrderType.PO.Name + "' ";
            sql += "ELSE '' END ORDERTYPENAME, CASE STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "ELSE '' END STATUS ";
            sql += "FROM V_TODOLIST_FG_STOCKIN ";
            sql += (where == "" ? "" : " WHERE ") + where;
            sql += "ORDER BY DUEDATE, NAME, LOID, CODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetStockOutList(ToDoListStockOutData data)
        {
            string where = "";
            if (data.CODE.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) LIKE '%" + data.CODE.Trim().ToUpper() + "%' ";

            if (data.REQDATE.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_CHAR(REQDATE, 'YYYYMMDD') = TO_CHAR(" + OracleDB.QRDate(data.REQDATE) + ", 'YYYYMMDD') ";

            if (data.REQUISITIONTYPE != 0)
                where += (where == "" ? "" : "AND ") + "REQUISITIONTYPE = " + data.REQUISITIONTYPE.ToString() + " ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            if (data.STATUS.Trim() != "")
                where += (where == "" ? "" : "AND ") + "STATUS = '" + data.STATUS + "' ";

            //if (data.WAREHOUSE != 0)
            //    where += (where == "" ? "" : "AND ") + "WAREHOUSE = " + data.WAREHOUSE.ToString() + " ";

            string sql = "SELECT DISTINCT LOID, REQUISITIONTYPE, TYPENAME, CODE, REQDATE, WAREHOUSE, CREATEON, STOID, STOCODE, SENDERNAME, SENDER, ";
            sql += "CASE STATUS WHEN '" + Constz.Requisition.Status.Approved.Code + "' THEN '" + Constz.Requisition.Status.Approved.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Finish.Code + "' THEN '" + Constz.Requisition.Status.Finish.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Void.Code + "' THEN '" + Constz.Requisition.Status.Void.Name + "' ";
            sql += "WHEN '" + Constz.Requisition.Status.Waiting.Code + "' THEN '" + Constz.Requisition.Status.Waiting.Name + "' ";
            sql += "ELSE '' END STATUS ";
            sql += "FROM V_TODOLIST_FG_STOCKOUT ";
            sql += (where == "" ? "" : " WHERE ") + where;
            sql += "ORDER BY TYPENAME, CODE, STOCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetExpireList(ToDoListExpireData data)
        {
            string where = "";
            
            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            if (data.TIME != 0)
                where += (where == "" ? "" : "AND ") + "EXPDATE <= ADD_MONTHS(sysdate, '" + data.TIME + "') ";

            string sql = "SELECT LOID, BARCODE, PRODUCTNAME, LOTNO, QTY, UNITNAME, EXPDATE ";
            sql += "FROM V_TODOLIST_FG_EXPDATE ";
            sql += (where == "" ? "" : " WHERE ") + where;
            sql += "ORDER BY PRODUCTNAME ";
            return OracleDB.ExecListCmd(sql);
        }

    }
}
