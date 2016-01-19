using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ABB.DAL
{
    public class ProductStockAjustDAL
    {
        public DataTable GetStockList(double warehouse, double zone)
        {
            string sql = "SELECT 0 as NO, LOID, PRODUCTNAME, QTY, UNITNAME ";
            sql += "FROM VIEW_PRODUCTSTOCKSHOP ";
            sql += "WHERE WAREHOUSE = " + warehouse.ToString() + " AND ZONE = " + zone.ToString() + " ";
            sql += "ORDER BY PRODUCTNAME ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
