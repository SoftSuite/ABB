using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Sales;
using ABB.Data;

namespace ABB.DAL.Common
{
    public class CommonDAL
    {
        string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
            set { _error = value; }
        }

        //public static DataTable GetProductWithPromotion(double warehouse, DateTime pDate, double product, string barcode)
        //{
        //    string where = "";
        //    if (warehouse != 0)
        //        where += (where == "" ? "" : "AND ") + "(P.WAREHOUSE IS NULL OR P.WAREHOUSE = " + warehouse.ToString() + ") ";

        //    if (product != 0)
        //        where += (where == "" ? "" : "AND ") + "PD.LOID = " + product.ToString() + " ";

        //    if (barcode.Trim() != "")
        //        where += (where == "" ? "" : "AND ") + "UPPER(PD.BARCODE) = '" + product.ToString() + "' ";

        //    string sql = "SELECT PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME UNITNAME, PD.PRICE, MAX(CASE NVL(P.LOID,0) WHEN 0 THEN 0 ELSE PI.PRICEOLD - PI.PRICENEW END) DISCOUNT, PD.ISVAT ";
        //    sql += "FROM PRODUCT PD INNER JOIN UNIT ON UNIT.LOID = PD.UNIT ";
        //    sql += "LEFT JOIN PROMOTIONITEM PI ON PI.PRODUCT = PD.LOID ";
        //    sql += "LEFT JOIN PROMOTION P ON P.LOID = PI.PROMOTION ";
        //    sql += (pDate.Year == 1 ? "" : "AND (P.EPDATE IS NULL OR P.EPDATE <= " + OracleDB.QRDate(pDate) + ") AND (P.EFDATE IS NULL OR P.EFDATE >= " + OracleDB.QRDate(pDate) + ") ");
        //    sql += (where == "" ? "" : "WHERE ") + where;
        //    sql += "GROUP BY PD.LOID, PD.BARCODE, PD.NAME, PD.UNIT, UNIT.NAME, PD.PRICE, PD.ISVAT ";
        //    return OracleDB.ExecListCmd(sql);
        //}

        public double GetProductStockQty(double product, double warehouse, double zone)
        {
            double quantity = 0;
            string sql = "SELECT SUM(QTY) FROM PRODUCTSTOCK WHERE PRODUCT = " + product.ToString() + " AND ZONE = " + zone.ToString() + " AND WAREHOUSE =  " + warehouse.ToString() + " ";
            try
            {
                quantity = Convert.ToDouble(OracleDB.ExecSingleCmd(sql));
            }
            catch (Exception ex)
            {
                quantity = 0;
            }
            return quantity;
        }


    }
}
