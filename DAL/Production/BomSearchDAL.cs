using System;
using System.Collections.Generic;
using System.Text;
using ABB.Data.Production;
using System.Collections;
using System.Data;
using ABB.Data;
using ABB.DAL;
using System.Data.OracleClient;

namespace ABB.DAL.Production
{
    
    public class BomSearchDAL
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetBomProductList(BomSearchData data)
        {
            string where = "P.LOID IN (SELECT MAINPRODUCT FROM BOM) ";
            if (data.PRODUCTTYPE != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTTYPE = " + data.PRODUCTTYPE.ToString() + " ";

            if (data.PRODUCTGROUP != 0)
                where += (where == "" ? "" : "AND ") + "PRODUCTGROUP = " + data.PRODUCTGROUP.ToString() + " ";

            if (data.PRODUCTNAME.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(P.NAME) LIKE '%" + data.PRODUCTNAME.Trim() + "%' ";

            string sql = "SELECT 0 RANK, P.BARCODE, P.NAME, UNIT.NAME UNITNAME, PG.NAME PRODUCTGROUP, PT.NAME PRODUCTTYPE, P.LOTSIZE, P.LOID ";
            sql += "FROM PRODUCT P INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE AND PT.TYPE = '" + Constz.ProductType.Type.FG.Code + "' ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where;
            sql += "ORDER BY P.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetBomList(double productbarcode)
        {
            string sql = "SELECT 0 RANK, P.BARCODE, P.NAME, BOM.MASTER, PT.NAME PRODUCTTYPE, UNIT.NAME UNITNAME, BOM.UNIT, P.LOID ";
            sql += "FROM BOM INNER JOIN PRODUCT P ON BOM.MATERIAL = P.LOID ";
            sql += "INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = BOM.UNIT ";
            sql += "WHERE BOM.MAINPRODUCT = " + productbarcode.ToString() + " ";
            sql += "ORDER BY P.BARCODE ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetBomProductData(double productBarcode)
        {
            string sql = "SELECT DISTINCT P.BARCODE, BOM.MAINPRODUCT, P.PRODUCTGROUP, PG.PRODUCTTYPE, BOM.RADIATION, BOM.ACTIVE, PS.PROCESS ";
            sql += "FROM BOM INNER JOIN PRODUCT P ON BOM.MAINPRODUCT = P.LOID ";
            sql += "INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN PROCESS PS ON PS.PRODUCT = P.PRODUCTMASTER ";
            sql += "WHERE BOM.MAINPRODUCT = " + productBarcode.ToString() + " ";
            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductData(double productBarcode, string barcode)
        {
            string where = "";
            if (productBarcode != 0)
                where += (where == "" ? "" : "AND ") + "P.LOID = " + productBarcode.ToString() + " ";

            if (barcode.Trim() != "")
                where += (where == "" ? "" : "AND ") + "UPPER(P.BARCODE) = '" + barcode.Trim().ToUpper() + "' ";

            string sql = "SELECT P.LOID, P.BARCODE, P.PRODUCTGROUP, PG.PRODUCTTYPE, PT.NAME PRODUCTTYPENAME, P.LOTSIZE, P.UNIT, UNIT.NAME UNITNAME ";
            sql += "FROM PRODUCT P INNER JOIN PRODUCTGROUP PG ON PG.LOID = P.PRODUCTGROUP ";
            sql += "INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            sql += "INNER JOIN UNIT ON UNIT.LOID = P.UNIT ";
            sql += (where == "" ? "" : "WHERE ") + where;
            return OracleDB.ExecListCmd(sql);
        }

        #region OLD

        public static DataTable GetProductData(string  data,string  pcloid)
        {
            DataTable dt = new DataTable();
            string str = "";
            str = "SELECT DISTINCT BO.MAINPRODUCT PDLOID, PG.LOID PGLOID, PT.LOID PTLOID,PC.PROCESS, ";
            str += " PD.BARCODE,PC.ACTIVE, PC.LOID PCLOID ,PC.RADIATION ";
            str += " FROM BOM BO INNER JOIN PRODUCT PD on BO.MAINPRODUCT= PD.LOID ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID = PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            str += " INNER JOIN PROCESS PC ON PC.PRODUCT = BO.MAINPRODUCT AND BO.PROCESS = PC.LOID ";
            str += " WHERE BO.MAINPRODUCT =" + data + " AND PC.LOID ="+ pcloid ;
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable  GetBomData(string  PDLoid,string  PCLOID)
        {
            DataTable dt = new DataTable();
            string str = "";
            str = " SELECT BO.LOID BOLOID,NVL(PD.BARCODE,'') BARCODE,BO.MATERIAL PDLOID,PT.LOID PTLOID,PC.LOID PCLOID ,";
            str +=  " NVL(BO.MASTER,0) MASTER,U.LOID ULOID,PD.NAME PDNAME, PT.NAME PTNAME, U.NAME UNAME ";
            str += " FROM BOM BO INNER JOIN PRODUCT PD ON PD.LOID = BO.MATERIAL ";
            str += " INNER JOIN PRODUCTGROUP PG ON PG.LOID = PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID = BO.UNIT ";
            str += " INNER JOIN PROCESS PC ON PC.PRODUCT = BO.MAINPRODUCT AND BO.PROCESS = PC.LOID ";
            str += " WHERE BO.MAINPRODUCT =" + PDLoid + " AND PC.LOID ="+ PCLOID +"";
            dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProduct(string LOID)
        {
            string str = "";
            str = " SELECT '' BOLOID,NVL(PD.BARCODE,'') BARCODE,PD.LOID PDLOID,PT.LOID PTLOID,";
            str += " 1 MASTER,U.LOID ULOID,PD.NAME PDNAME, PT.NAME PTNAME, U.NAME UNAME,'' PCLOID ";
            str += " FROM PRODUCT PD INNER JOIN PRODUCTGROUP PG ON PG.LOID = PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.UNIT ";
            str += " WHERE PD.LOID =" + LOID + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProductBarcode(string BARCODE,string PDLoid)
        {
            string str = "";
            str = " SELECT PD.LOID PDLOID, PG.LOID PGLOID ,PT.LOID PTLOID,PD.NAME PDNAME, PT.NAME PTNAME, ";
            str += " PG.NAME PGNAME, PD.BARCODE, PD.ACTIVE ";
            str += " FROM PRODUCT PD INNER JOIN PRODUCTGROUP PG ON PG.LOID = PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE ";
            if (BARCODE != "")
                str += " WHERE PD.BARCODE ='" + BARCODE + "'";
            if (PDLoid != "")
                str += " WHERE PD.LOID =" + PDLoid + "";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        public static DataTable GetProductByBarcode(string Barcode)
        {
            string str = "";
            str = " SELECT '' BOLOID,NVL(PD.BARCODE,'') BARCODE,PD.LOID PDLOID,PT.LOID PTLOID,";
            str += " 1 MASTER,U.LOID ULOID,PD.NAME PDNAME, PT.NAME PTNAME, U.NAME UNAME,'' PCLOID ";
            str += " FROM PRODUCT PD INNER JOIN PRODUCTGROUP PG ON PG.LOID = PD.PRODUCTGROUP ";
            str += " INNER JOIN PRODUCTTYPE PT ON PT.LOID = PG.PRODUCTTYPE AND PT.TYPE = 'WH' ";
            str += " INNER JOIN UNIT U ON U.LOID = PD.UNIT ";
            str += " WHERE PD.BARCODE ='" + Barcode + "'";
            DataTable dt = OracleDB.ExecListCmd(str);
            return dt;
        }

        #endregion
    }
}
