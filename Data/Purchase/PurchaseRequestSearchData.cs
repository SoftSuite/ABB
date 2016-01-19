using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class PurchaseRequestSearchData
    {
        private string _CODEFROM = "";
        private string _CODETO = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private double _PURCHASETYPE = 0;
        private double _DIVISION = 0;
        private string _PRODUCTNAME = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";
        private string _STATUSPRFROM = "";
        private string _STATUSPRTO = "";
        private string _STATUSPOFROM = "";
        private string _STATUSPOTO = "";

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

        public double PURCHASETYPE
        {
            get { return _PURCHASETYPE; }
            set { _PURCHASETYPE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }

        public string STATUSFROM
        {
            get { return _STATUSFROM; }
            set { _STATUSFROM = value; }
        }

        public string STATUSTO
        {
            get { return _STATUSTO; }
            set { _STATUSTO = value; }
        }

        public string STATUSPRFROM
        {
            get { return _STATUSPRFROM; }
            set { _STATUSPRFROM = value; }
        }

        public string STATUSPRTO
        {
            get { return _STATUSPRTO; }
            set { _STATUSPRTO = value; }
        }

        public string STATUSPOFROM
        {
            get { return _STATUSPOFROM; }
            set { _STATUSPOFROM = value; }
        }

        public string STATUSPOTO
        {
            get { return _STATUSPOTO; }
            set { _STATUSPOTO = value; }
        }
    }
}
