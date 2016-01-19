using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class CustomerSaleData
    {
        private double _CUSTOMER = 0;
        private string _CODE = "";
        private string _CUSTOMERNAME = "";
        private double _MEMBERTYPE = 0;
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private double _CREDITDAY = 0;
        private string _PAYMENT = "";
        private DateTime _EFDATE = new DateTime(1, 1, 1);
        private DateTime _EPDATE = new DateTime(1, 1, 1);

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public double MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }

        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }

        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }

        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
        }

        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }

        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }

        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }

        public double CREDITDAY
        {
            get { return _CREDITDAY; }
            set { _CREDITDAY = value; }
        }

        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }

        public DateTime EFDATE
        {
            get { return _EFDATE; }
            set { _EFDATE = value; }
        }

        public DateTime EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }

    }
}
