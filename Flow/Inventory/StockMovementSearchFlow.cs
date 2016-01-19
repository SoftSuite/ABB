using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.DAL;
using ABB.Flow.Inventory;
using ABB.Data.Inventory;

namespace ABB.Flow.Inventory
{
    public class StockMovementSearchFlow
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetStockMovementItemList(StockMovementSearchData data)
        {
            string whereString = "";

            if (data.DATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "TO_DATE(CDATE,'YYYY-MM-DD') >= " + OracleDB.QRDate(data.DATEFROM) + " ";

            if (data.DATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "TO_DATE(CDATE,'YYYY-MM-DD') <= " + OracleDB.QRDate(data.DATETO) + " ";

            if (data.PRODUCTTYPE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PTLOID = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.PRODUCTGROUP != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PGLOID = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTNAME.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "UPPER(PRODUCTNAME) LIKE '%" + data.PRODUCTNAME.Trim().ToUpper() + "%' ";

            if (data.ZONE != 0)
                whereString += (whereString == "" ? "" : "AND ") + "(FROMWAREHOUSE = " + data.ZONE.ToString() + " OR TOWAREHOUSE = " + data.ZONE.ToString() + ") ";

            if (data.ZONEFROM != 0)
                whereString += (whereString == "" ? "" : "AND ") + "FROMZONE = " + data.ZONEFROM.ToString() + " ";

            if (data.ZONETO != 0)
                whereString += (whereString == "" ? "" : "AND ") + "TOZONE = " + data.ZONETO.ToString() + " ";

            string sql = "SELECT ROWNUM NO, CREATEON, FROMWAREHOUSENAME, FROMZONENAME, TOWAREHOUSENAME, TOZONENAME, PRODUCTNAME, QTY, ";
            sql += "UNITNAME, DOCCODE, TYPENAME ";
            sql += "FROM V_STOCKMOVEMENT_SEARCH ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);

            DataTable dt = OracleDB.ExecListCmd(sql);
            return dt;
        }
    }
}
