using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PackageSearchData
    {
        int _ORDERNO = 0;
        string _BARCODE = "";
        string _PNAME = "";
        double _COST = 0;
        double _STDPRICE = 0;


        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string PNAME
        {
            get { return _PNAME; }
            set { _PNAME = value; }
        }

        public double COST
        {
            get { return _COST; }
            set { _COST = value; }
        }

        public double STDPRICE
        {
            get { return _STDPRICE; }
            set { _STDPRICE = value; }
        }

    }
}
