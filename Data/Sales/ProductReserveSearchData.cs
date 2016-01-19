using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductReserveSearchData
    {
        private double _LOID = 0;
        private double _REQUISITIONTYPE = 0;
        private string _INVCODE = "";
        private string _CODE = "";
        private string _REQCODE = "";
        private string _CODEFROM = "";
        private string _CODETO = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private DateTime _RESERVEFROM = new DateTime(1, 1, 1);
        private DateTime _RESERVETO = new DateTime(1, 1, 1);
        private string _CUSTOMERNAME = "";
        private string _CUSTOMERCODE = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";
        private DateTime _CREATEFROM = new DateTime(1, 1, 1);
        private DateTime _CREATETO = new DateTime(1, 1, 1);
        private double _CUSTOMER = 0;
        private double _PRODUCT = 0;
        private double _WAREHOUSE = 0;
        private double _DIVISION = 0;


        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public string REQCODE
        {
            get { return _REQCODE; }
            set { _REQCODE = value; }
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

        public DateTime RESERVEFROM
        {
            get { return _RESERVEFROM; }
            set { _RESERVEFROM = value; }
        }

        public DateTime RESERVETO
        {
            get { return _RESERVETO; }
            set { _RESERVETO = value; }
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
           public DateTime CREATEFROM
        {
            get { return _CREATEFROM; }
            set { _CREATEFROM = value; }
        }

        public DateTime CREATETO
        {
            get { return _CREATETO; }
            set { _CREATETO = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

    }


}
