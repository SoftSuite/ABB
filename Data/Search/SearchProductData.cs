using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class SearchProductData
    {
        private string _CODE = "";
        private string _NAME = "";
        private double _PRODUCTTYPE = 0;
        private double _PRODUCTGROUP = 0;
        private double _WAREHOUSE = 0;
        private string _TYPE = "";
        private double _ZONE = 0;
        private string _LOIDLIST = "";

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }

        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public string TYPE
        {
            get { return _TYPE; }
            set { _TYPE = value; }
        }

        public double ZONE
        {
            get { return _ZONE; }
            set { _ZONE = value; }
        }

        public string LOIDLIST
        {
            get { return _LOIDLIST; }
            set { _LOIDLIST = value; }
        }

    }
}
