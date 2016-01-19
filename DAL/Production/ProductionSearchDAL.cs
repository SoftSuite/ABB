using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.Data.Production;
using ABB.Data;

namespace ABB.DAL.Production
{
    public class ProductionSearchDAL
    {
        public DataTable GetProductionStockinQuarantineList(ProductStockinQuarantineSearchData whereData)
        {
            string whereString = "";

            if (whereData.MFGDATE.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE = " + OracleDB.QRDate(whereData.MFGDATE) + " ";
            if (whereData.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + whereData.PRODUCT + " ";
            if (whereData.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(whereData.LOTNO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO,A.* FROM (SELECT PDP.LOID,PDP.MFGDATE,PDP.PRODUCT,PD.NAME PRODUCTNAME,PDP.LOTNO,PDP.QUARANTINEQTY,U.NAME UNITNAME,PDP.QUARANTINEREMARK ";
            sql += "FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PDP.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID)A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionStockoutQuarantineList(ProductStockoutQuarantineSearchData whereData)
        {
            string whereString = "";

            if (whereData.MFGDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE >= " + OracleDB.QRDate(whereData.MFGDATEFROM) + " ";
            if (whereData.MFGDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE <= " + OracleDB.QRDate(whereData.MFGDATETO) + " ";
            if (whereData.SENDFGDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "SENDFGDATE >= " + OracleDB.QRDate(whereData.SENDFGDATEFROM) + " ";
            if (whereData.SENDFGDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "SENDFGDATE <= " + OracleDB.QRDate(whereData.SENDFGDATETO) + " ";
            if (whereData.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + whereData.PRODUCT + " ";
            if (whereData.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(whereData.LOTNO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO,A.* FROM (SELECT PDP.LOID,PDP.MFGDATE,PDP.QUARANTINEDATE,PDP.PRODUCT, ";
            sql += "PD.NAME PRODUCTNAME,PDP.LOTNO,PDP.PDQTY,PDP.QCQTY1,PDP.QCQTY2,PDP.QCQTY3,U.NAME UNITNAME,PDP.QUARANTINEREMARK, ";
            sql += "PDP.STDQTY-PDP.PDQTY AS BADQTY,PDP.SENDFGQTY,PDP.SENDFGDATE,PDP.SENDFGREMARK ";
            sql += "FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PDP.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID)A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionLostList(ProductionLostSearchData whereData)
        {
            string whereString = "";

            if (whereData.MFGDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE >= " + OracleDB.QRDate(whereData.MFGDATEFROM) + " ";
            if (whereData.MFGDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE <= " + OracleDB.QRDate(whereData.MFGDATETO) + " ";
            if (whereData.SENDFGDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "SENDFGDATE >= " + OracleDB.QRDate(whereData.SENDFGDATEFROM) + " ";
            if (whereData.SENDFGDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "SENDFGDATE <= " + OracleDB.QRDate(whereData.SENDFGDATETO) + " ";
            if (whereData.ORDERDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "ORDERDATE >= " + OracleDB.QRDate(whereData.ORDERDATEFROM) + " ";
            if (whereData.ORDERDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "ORDERDATE <= " + OracleDB.QRDate(whereData.ORDERDATETO) + " ";
            if (whereData.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + whereData.PRODUCT + " ";
            if (whereData.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(whereData.LOTNO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO,A.* FROM (SELECT PDP.LOID,PDP.MFGDATE,PDP.QUARANTINEDATE,PDP.PRODUCT, ";
            sql += "PD.NAME PRODUCTNAME,PDP.LOTNO,PDP.PDQTY,U.NAME UNITNAME,PDP.QUARANTINEREMARK, ";
            sql += "PDP.STDQTY,PDP.YIELD,PDP.SENDFGDATE,PO.ORDERDATE ";
            sql += "FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PDP.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID)A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }

        public DataTable GetProductionQCList(ProductionQCSearchData whereData)
        {
            string whereString = "";

            if (whereData.MFGDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE >= " + OracleDB.QRDate(whereData.MFGDATEFROM) + " ";
            if (whereData.MFGDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "MFGDATE <= " + OracleDB.QRDate(whereData.MFGDATETO) + " ";
            if (whereData.SENDQCDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "SENDQCDATE >= " + OracleDB.QRDate(whereData.SENDQCDATEFROM) + " ";
            if (whereData.SENDQCDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "SENDQCDATE <= " + OracleDB.QRDate(whereData.SENDQCDATETO) + " ";
            if (whereData.EXPDATEFROM.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "EXPDATE >= " + OracleDB.QRDate(whereData.EXPDATEFROM) + " ";
            if (whereData.EXPDATETO.Year != 1)
                whereString += (whereString == "" ? "" : "AND ") + "EXPDATE <= " + OracleDB.QRDate(whereData.EXPDATETO) + " ";
            if (whereData.PRODUCT != 0)
                whereString += (whereString == "" ? "" : "AND ") + "PRODUCT = " + whereData.PRODUCT + " ";
            if (whereData.LOTNO.Trim() != "")
                whereString += (whereString == "" ? "" : "AND ") + "LOTNO = '" + OracleDB.QRText(whereData.LOTNO.Trim()) + "' ";

            string sql = "SELECT ROWNUM NO,A.* FROM (SELECT PDP.LOID,PDP.MFGDATE,PDP.QUARANTINEDATE,PDP.PRODUCT, ";
            sql += "PD.NAME PRODUCTNAME,PDP.LOTNO,PDP.QCQTY1,U.NAME UNITNAME,PDP.QUARANTINEREMARK, ";
            sql += "PDP.STDQTY,PDP.QCREMARK,PDP.SENDQCDATE,PO.ORDERDATE ";
            sql += "FROM PDPRODUCT PDP INNER JOIN PDORDER PO ON PDP.PDORDER = PO.LOID ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID INNER JOIN UNIT U ON PD.UNIT = U.LOID)A ";
            sql += (whereString == "" ? "" : "WHERE " + whereString);
            sql += "ORDER BY NO ";

            return OracleDB.ExecListCmd(sql);
        }
    }
}
