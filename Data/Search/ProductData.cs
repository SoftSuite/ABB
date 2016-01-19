using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class ProductData
    {
        private string _CODE = "";
        private string _NAME = "";
        private double _PRODUCTTYPE = 0;
        private double _PRODUCTGROUP = 0;
        private double _WAREHOUSE = 0;

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
    }
}
