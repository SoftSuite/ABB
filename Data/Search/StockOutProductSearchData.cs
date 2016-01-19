using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class StockOutProductSearchData
    {
        private string _REQUISITIONCODEFROM = "";
        private string _REQUISITIONCODETO = "";
        private DateTime _RESERVEDATEFROM = new DateTime(1, 1, 1);
        private DateTime _RESERVEDATETO = new DateTime(1, 1, 1);

        public string REQUISITIONCODEFROM
        {
            get { return _REQUISITIONCODEFROM; }
            set { _REQUISITIONCODEFROM = value; }
        }

        public string REQUISITIONCODETO
        {
            get { return _REQUISITIONCODETO; }
            set { _REQUISITIONCODETO = value; }
        }

        public DateTime RESERVEDATEFROM
        {
            get { return _RESERVEDATEFROM; }
            set { _RESERVEDATEFROM = value; }
        }

        public DateTime RESERVEDATETO
        {
            get { return _RESERVEDATETO; }
            set { _RESERVEDATETO = value; }
        }
    }
}
