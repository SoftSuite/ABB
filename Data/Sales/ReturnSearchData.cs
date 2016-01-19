using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ReturnSearchData
    {
        string _SICODE = "";
        DateTime _APPROVEDATE = new DateTime(1, 1, 1);
        string _CUSNAME = "";
        string _RQCODE = "";
        double _GRANDTOT = 0;
        double _SILOID = 0;
        double _WAREHOUSE = 0;
        int _ORDERNO = 0;
        string _INVCODE = "";
        DateTime _REQDATE = new DateTime(1, 1, 1);


        public string SICODE
        {
            get { return _SICODE; }
            set { _SICODE = value; }
        }

        public DateTime APPROVEDATE
        {
            get { return _APPROVEDATE; }
            set { _APPROVEDATE = value; }
        }

        public string CUSNAME
        {
            get { return _CUSNAME; }
            set { _CUSNAME = value; }
        }

        public string RQCODE
        {
            get { return _RQCODE; }
            set { _RQCODE = value; }
        }

        public double GRANDTOT
        {
            get { return _GRANDTOT; }
            set { _GRANDTOT = value; }
        }

        public double SILOID
        {
            get { return _SILOID; }
            set { _SILOID = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }
    }
}
