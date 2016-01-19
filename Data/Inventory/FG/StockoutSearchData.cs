using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class StockoutSearchData
    {
        private double _REQUISITIONTYPE = 0;
        private string _REQCODE = "";
        private string _REQCODEFROM = "";
        private string _REQCODETO = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private DateTime _REQUESTDATEFROM = new DateTime(1, 1, 1);
        private DateTime _REQUESTDATETO = new DateTime(1, 1, 1);
        private DateTime _APPROVEFROM = new DateTime(1, 1, 1);
        private DateTime _APPROVETO = new DateTime(1, 1, 1);
        private string _CREATEBY = "";
        private string _STOCKCODE = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";
        private double _PRODUCT = 0;
        private double _SUPPLIER = 0;
        private string _CUSTOMERNAME = "";
        private string _CUSTOMERCODE = "";

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public string REQCODE
        {
            get { return _REQCODE; }
            set { _REQCODE = value; }
        }

        public string REQCODEFROM
        {
            get { return _REQCODEFROM; }
            set { _REQCODEFROM = value; }
        }

        public string REQCODETO
        {
            get { return _REQCODETO; }
            set { _REQCODETO = value; }
        }

        public DateTime REQUESTDATEFROM
        {
            get { return _REQUESTDATEFROM; }
            set { _REQUESTDATEFROM = value; }
        }

        public DateTime REQUESTDATETO
        {
            get { return _REQUESTDATETO; }
            set { _REQUESTDATETO = value; }
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

        public DateTime APPROVEFROM
        {
            get { return _APPROVEFROM; }
            set { _APPROVEFROM = value; }
        }

        public DateTime APPROVETO
        {
            get { return _APPROVETO; }
            set { _APPROVETO = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public string STOCKCODE
        {
            get { return _STOCKCODE; }
            set { _STOCKCODE = value; }
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

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }

    }
}

