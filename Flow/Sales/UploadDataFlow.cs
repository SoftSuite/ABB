using System;
using System.Collections.Generic;
using System.Text;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    public class UploadDataFlow
    {
        private string _error = "";

        public string ErrorMessage
        {
            get { return _error; }
        }

        public bool UploadData(DateTime dateFrom)
        {
            bool ret = true;
            OracleDBObj obj = new OracleDBObj();
            obj.CreateSeverConnection();
            obj.CreateTransaction();
            try
            {
                OracleDB.ExecNonQueryCmd("CALL SP_IMPORT_EXPORT(" + OracleDB.QRDate(dateFrom) + ")", obj.zTrans);

                obj.zTrans.Commit();
                obj.zConn.Close();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
                obj.zTrans.Rollback();
                obj.zConn.Close();
            }
            return ret;
        }
    }
}
