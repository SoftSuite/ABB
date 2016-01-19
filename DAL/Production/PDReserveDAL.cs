using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.Data;

namespace ABB.DAL.Production
{
    public class PDReserveDAL : RequisitionDAL
    {
        /// <summary>
        /// Get Data List of This Table
        /// </summary>
        /// <param name="whereCause"></param>
        /// <param name="zTrans">Transaction, set to null if no transaction provided</param>
        /// <returns></returns>
        public new DataTable GetDataList(string whereCause, OracleTransaction zTrans)
        {
            return OracleDB.ExecListCmd(sql_select + whereCause);
        }

        private string sql_select
        {
            get
            {
                string sqlz = "SELECT RQ.*,VP.PDPLOID PDLOID FROM REQUISITION RQ INNER JOIN V_PRODUCT_PDPRODUCT VP ON RQ.REFTABLE='PDPRODUCT' AND RQ.REFLOID=VP.PDLOID ";
                return sqlz;
            }
        }

    }
}