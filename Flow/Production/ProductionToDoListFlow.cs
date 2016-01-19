using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.Data.Production;
using ABB.DAL;

namespace ABB.Flow.Production
{
    public class ProductionToDoListFlow
    {
        double _LOID = 0;
        public double LOID
        {
            get { return _LOID; }
        }
        string _error = "";
        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetProductionWaitList(ProductionWaitListSearchData data)
        {
            string where = "";

            if (data.CODEFROM.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) >= '" + OracleDB.QRText(data.CODEFROM.Trim()).ToUpper() + "' ";

            if (data.CODETO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(CODE) <= '" + OracleDB.QRText(data.CODETO.Trim()).ToUpper() + "' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(REQDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PDNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PDNAME) LIKE '%" + data.PDNAME.Trim().ToUpper() + "%' ";

            string sql = "SELECT RQILOID, RQLOID, PDLOID, CODE, PDNAME, REQDATE, QTY, UNAME, OFFICER ";
            sql += "FROM V_TODOLIST_PD_WAIT ";
            sql += (where == "" ? "" : "WHERE " + where);
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionDuringList(ProductionDuringListSearchData data)
        {
            string where = "";

            if (data.LOTNO.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(LOTNO) LIKE '%" + data.LOTNO.Trim().ToUpper() + "%' ";

            if (data.DATEFROM.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(MFGDATE, 'DDMMYYYY') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                where += (where == "" ? "" : "AND ") + "TO_DATE(MFGDATE, 'DDMMYYYY') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PDNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(PDNAME) LIKE '%" + data.PDNAME.Trim().ToUpper() + "%' ";

            string sql = "SELECT PDPLOID, POLOID, LOTNO, PDNAME, MFGDATE, STDQTY, PDQTY, UNAME, DUEDATE, SUPPLIERNAME, QUARANTINEDATE, SENDQCDATE, QCDUEDATE, QCRESULT, SENDFGDATE ";
            sql += "FROM V_TODOLIST_PD_DURING ";
            sql += (where == "" ? "" : "WHERE " + where);
            return OracleDB.ExecListCmd(sql);
        }
    }
}
