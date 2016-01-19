using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ControlStockData
    {
            int _ORDERNO = 0;
            string _PRODUCTCODE = "";
            string _PRODUCTNAME = "";
            double _LOID = 0;
            double _PRODUCT = 0;
            double _WAREHOUSE = 0;
            string _STANDARD = "";
            string _MINIMUM = "";
            string _MAXIMUM = "";
            double _ACTIVE = 0;
            string _CREATEBY = "";
            DateTime _CREATEON = new DateTime(1, 1, 1);
            string _UPDATEBY = "";
            DateTime _UPDATEON = new DateTime(1, 1, 1);

            public int ORDERNO
            {
                get { return _ORDERNO; }
                set { _ORDERNO = value; }
            }

            public string PRODUCTCODE
            {
                get { return _PRODUCTCODE; }
                set { _PRODUCTCODE = value; }
            }

            public string PRODUCTNAME
            {
                get { return _PRODUCTNAME; }
                set { _PRODUCTNAME = value; }
            }

            public double LOID
            {
                get { return _LOID; }
                set { _LOID = value; }
            }

            public double PRODUCT
            {
                get { return _PRODUCT; }
                set { _PRODUCT = value; }
            }

            public double WAREHOUSE
            {
                get { return _WAREHOUSE; }
                set { _WAREHOUSE = value; }
            }

            public string STANDARD
            {
                get { return _STANDARD; }
                set { _STANDARD = value; }
            }

            public string MINIMUM
            {
                get { return _MINIMUM; }
                set { _MINIMUM = value; }
            }

            public string MAXIMUM
            {
                get { return _MAXIMUM; }
                set { _MAXIMUM = value; }
            }

            public double ACTIVE
            {
                get { return _ACTIVE; }
                set { _ACTIVE = value; }
            }

            public string CREATEBY
            {
                get { return _CREATEBY; }
                set { _CREATEBY = value; }
            }

            public DateTime CREATEON
            {
                get { return _CREATEON; }
                set { _CREATEON = value; }
            }

            public string UPDATEBY
            {
                get { return _UPDATEBY; }
                set { _UPDATEBY = value; }
            }

            public DateTime UPDATEON
            {
                get { return _UPDATEON; }
                set { _UPDATEON = value; }
            }
        
    }
}
