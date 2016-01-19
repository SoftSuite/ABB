using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ABB.DAL.Sales
{
    public class ControlStockDAL
    {
        public DataTable GetStockList(double Warehouse)
        {
            string where = "";
            if (Warehouse == 0)
                where = "";
            else
                where = " WHERE WH.LOID =" + Warehouse;

            string sql = "SELECT PM.LOID PMLOID,PD.BARCODE,PD.NAME,WH.NAME WAREHOUSE ,WH.LOID WHLOID, U.NAME UNIT, UM.NAME UNITMASTER,";
            sql += " CASE WHEN PM.STANDARD IS NULL THEN 0 ELSE PM.STANDARD END STANDARD,";
            sql += " CASE WHEN PM.MINIMUM IS NULL THEN 0 ELSE PM.MINIMUM END MINIMUM,";
            sql += " CASE WHEN PM.MAXIMUM IS NULL THEN 0 ELSE PM.MAXIMUM END MAXIMUM, PM.PRODUCT ";
            sql += " FROM PRODUCT PD INNER JOIN PRODUCTMINMAX PM ON PM.PRODUCT = PD.PRODUCTMASTER ";
            sql += " INNER JOIN WAREHOUSE WH ON WH.LOID = PM.WAREHOUSE";
            sql += " INNER JOIN UNIT U ON PD.UNIT = U.LOID";
            sql += " INNER JOIN UNIT UM ON PD.UNITMASTER = UM.LOID";
            sql += (where == "" ? "" : where);

            return OracleDB.ExecListCmd(sql);
        }
    }
}
