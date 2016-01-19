using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class SaleSearchData
    {
        private DateTime _REQDATEFROM = new DateTime(1, 1, 1);
        private DateTime _REQDATETO = new DateTime(1, 1, 1);
        private string _CODEFROM = "";
        private string _CODETO = "";
        private string _CUSTOMERNAME = "";
        private double _PRODUCT = 0;

        public DateTime REQDATEFROM
        {
            get { return _REQDATEFROM; }
            set { _REQDATEFROM = value; }
        }

        public DateTime REQDATETO
        {
            get { return _REQDATETO; }
            set { _REQDATETO = value; }
        }

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

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

    }
}
