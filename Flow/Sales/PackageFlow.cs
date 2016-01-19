using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using System.Data.OracleClient;
using ABB.DAL;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public  class PackageFlow
    {
        public static ArrayList GetSearchPackage(string WHLoid, string PCode,string PName)
        {
            string str = "";
            ArrayList arrResult = new ArrayList();

            str = " SELECT DISTINCT PD.BARCODE,PD.NAME PNAME,PD.COST,PD.STDPRICE ";
            str += " FROM PRODUCT PD INNER JOIN PACKAGE PK ON PK.MAINPRODUCT = PD.LOID ";
            str += " INNER JOIN PRODUCTSTOCK PS ON PS.PRODUCT = PD.LOID ";
            str += " INNER JOIN ZONE ON ZONE.LOID = PS.ZONE ";
            str += " WHERE ZONE.WAREHOUSE = " + WHLoid + "";

            if (PCode != "")
            {
                str += " AND BARCODE = '" + PCode + "'";
            }

            if (PName != "")
            {
                str += " AND UPPER(PD.NAME) LIKE UPPER('%" + PName + "%')";
            }

            try
            {
                OracleDataReader zRd = OracleDB.ExecQueryCmd(str);
                arrResult.Clear();
                int i = 1;
                while (zRd.Read())
                {
                    PackageSearchData irData = new PackageSearchData();
                    irData.ORDERNO = i;
                    irData.BARCODE = zRd["BARCODE"].ToString();
                    irData.PNAME = zRd["PNAME"].ToString();
                    irData.COST = Convert.ToDouble(zRd["COST"]);
                    irData.STDPRICE = Convert.ToDouble(zRd["STDPRICE"]);
                    arrResult.Add(irData);
                    i = i + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return arrResult;
        }
    }
}
