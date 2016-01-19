using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory
{
    public class StockinProductSearchData
    {
        private string _CODEFROM = "";
        private string _CODETO = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _PRODUCTNAME = "";
        private string _LOTNOFROM = "";
        private string _LOTNOTO = "";
        private DateTime _CREATEONFROM = new DateTime(1, 1, 1);
        private DateTime _CREATEONTO = new DateTime(1, 1, 1);
        private double _WAREHOUSE = 0;
        private string _PRODUCETYPE = "";

        public string CODEFROM
        {
            get { return _CODEFROM; }
            set { _CODEFROM = value; }
        }

        public string CODETO
        {
            get { return _CODETO; }
            set { _CODETO = value; }
        }

        public DateTime DATEFROM
        {
            get { return _DATEFROM; }
            set { _DATEFROM = value; }
        }

        public DateTime DATETO
        {
            get { return _DATETO; }
            set { _DATETO = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string LOTNOFROM
        {
            get { return _LOTNOFROM; }
            set { _LOTNOFROM = value; }
        }

        public string LOTNOTO
        {
            get { return _LOTNOTO; }
            set { _LOTNOTO = value; }
        }

        public DateTime CREATEONFROM
        {
            get { return _CREATEONFROM; }
            set { _CREATEONFROM = value; }
        }

        public DateTime CREATEONTO
        {
            get { return _CREATEONTO; }
            set { _CREATEONTO = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public string PRODUCETYPE
        {
            get { return _PRODUCETYPE; }
            set { _PRODUCETYPE = value; }
        }

    }
}

