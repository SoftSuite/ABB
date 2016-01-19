using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;
using ABB.Data.Production;

namespace ABB.DAL.Production
{
    public class ReturnRequestDAL : RequisitionDAL
    {

        public ReturnRequestData DoGetRefData(double loid)
        {
            string sql = "SELECT PDP.LOTNO,PD.NAME,PD.BARCODE FROM PDPRODUCT PDP ";
           // sql += "INNER JOIN REQUISITION RQ ON RQ.REFTABLE = 'PDPRODUCT' AND PDP.LOID = RQ.REFLOID ";
            sql += "INNER JOIN PRODUCT PD ON PDP.PRODUCT = PD.LOID ";
            sql += "WHERE PDP.LOID = '" + loid + "'";
            DataTable dt = OracleDB.ExecListCmd(sql);
            ReturnRequestData data = new ReturnRequestData();
            if (dt.Rows.Count > 0)
            {
                data.LOTNO = dt.Rows[0]["LOTNO"].ToString();
                data.PDNAME = dt.Rows[0]["NAME"].ToString();
                data.PDBARCODE = dt.Rows[0]["BARCODE"].ToString();
            }

            return data;
        }

    }
}