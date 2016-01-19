using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using ABB.DAL.Common;

namespace ABB.Flow.Common
{
    public class ComboSourceFlow
    {
        private ComboSourceDAL _dal;
        public ComboSourceDAL SourceDAL
        {
            get { if (_dal == null) {_dal = new ComboSourceDAL(); } return _dal; }
        }

        public DataTable GetSource(string TableName, string TextField, string ValueField, string SortField, string WhereStr)
        {
            return SourceDAL.GetSource(TableName, TextField, ValueField, SortField,WhereStr);
        }

        public DataTable GetSourceDistinct(string TableName, string TextField, string ValueField, string SortField, string WhereStr)
        {
            return SourceDAL.GetSourceDistinct(TableName, TextField, ValueField, SortField,WhereStr);
        }

    }
}
