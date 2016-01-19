using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ABB.DAL.Sales
{
    public class ProductSearchDAL
    {
        public DataTable GetProductList(string where)
        {
            string sql = "SELECT P.LOID, P.NAME, P.CODE, P.BARCODE, UNIT.NAME UNIT, PG.NAME PRODUCTGROUP, PT.NAME PRODUCTTYPE, P.LOTSIZE, P.COST, P.PRICE ";
            sql += "FROM PRODUCT P INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += (where == "" ? "" : " WHERE " + where + " ");
            sql += "ORDER BY PT.NAME, PG.NAME, P.CODE ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
