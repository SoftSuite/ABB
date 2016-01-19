using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ABB.DAL.Common
{
    public class ComboSourceDAL
    {
        public DataTable GetSource(string TableName, string TextField, string ValueField, string SortField, string WhereStr)
        {
            DataTable dt;
            string sql = "SELECT " + TextField + ", " + ValueField + " FROM " + TableName + " ";
            sql += (WhereStr == "" ? "" : " WHERE " + WhereStr);
            sql += " ORDER BY " + (SortField == "" ? TextField : SortField) + " ";
            try
            {
                dt = OracleDB.ExecListCmd(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return dt;
        }

        public DataTable GetSourceDistinct(string TableName, string TextField, string ValueField, string SortField, string WhereStr)
        {
            DataTable dt;
            string sql = "SELECT DISTINCT " + TextField + (ValueField == "" ? "" : ", " + ValueField) + " FROM " + TableName + " ";
            sql += (WhereStr == "" ? "" : " WHERE " + WhereStr);
            sql += " ORDER BY " + (SortField == "" ? TextField : SortField) + " ";
            try
            {
                dt = OracleDB.ExecListCmd(sql);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
            return dt;
        }
    }
}
