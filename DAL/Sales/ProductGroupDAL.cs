using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ABB.DAL.Sales
{
    public class ProductGroupSearchDAL
    {
        public DataTable GetProductGourpList()
        {
            string sql = "SELECT PG.LOID, PG.CODE, PG.NAME, PG.PRODUCTTYPE, PG.ACTIVE, PG.CREATEBY, PG.CREATEON, PG.UPDATEBY, PG.UPDATEON, ";
            sql += "PT.NAME PRODUCTTYPENAME ";
            sql += "FROM PRODUCTGROUP PG INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            sql += "ORDER BY PG.CODE, PG.NAME ";
            return OracleDB.ExecListCmd(sql);
        }
    }
}
