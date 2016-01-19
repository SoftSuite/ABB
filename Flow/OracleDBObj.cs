using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OracleClient;
using ABB.DAL;

namespace ABB.Flow
{
    public class OracleDBObj
    {

        private OracleConnection _Conn;
        private OracleTransaction _Trans;
        private string _error;

        public string ErrorMessage
        {
            get { return _error; }
        }
        public OracleConnection zConn
        {
            get { return _Conn; }
        }
        public OracleTransaction zTrans
        {
            get { return _Trans; }
        }

        public bool CreateConnection()
        {
            bool ret =true;
            try
            {
                _Conn = OracleDB.GetConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CreateSeverConnection()
        {
            bool ret = true;
            try
            {
                _Conn = OracleDB.GetServerConnection();
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CloseConnection()
        {
            bool ret = true;
            try
            {
                if (_Conn != null) { _Conn.Close(); }
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool CreateTransaction()
        {
            bool ret = true;
            try
            {
                if (_Conn == null) { _Conn = OracleDB.GetConnection(); }

                _Trans = _Conn.BeginTransaction(IsolationLevel.ReadCommitted);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            if (!ret)
            {
                try
                {
                    if (_Conn != null) { _Conn.Close(); }
                    _Conn.Open();
                    _Trans = _Conn.BeginTransaction(IsolationLevel.ReadCommitted);
                    ret = true;
                }
                catch (Exception ex)
                {
                    ret = false;
                    _error = ex.Message;
                }
            }
            return ret;
        }

    }
}
