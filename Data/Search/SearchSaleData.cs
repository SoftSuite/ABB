using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class SearchSaleData
    {
        private DateTime _REQDATEFROM = new DateTime(1, 1, 1);
        private DateTime _REQDATETO = new DateTime(1, 1, 1);
        private string _CODEFROM = "";
        private string _CODETO = "";
        private double _CUSTOMER = 0;
        private string _CUSTOMERNAME = "";
        private double _PRODUCT = 0;
        private string _INVCODE = "";
        private string _CUSTOMERCODE = "";
        private string _PRODUCTNAME = "";
        private string _STATUS = "";

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

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
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
        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }
        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }
        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }
    }
}
