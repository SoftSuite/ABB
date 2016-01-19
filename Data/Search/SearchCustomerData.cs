using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class SearchCustomerData
    {
        private string _CODE = "";
        private string _FULLNAME = "";
        private string _NAME = "";
        private string _LASTNAME = "";
        private double _MEMBERTYPE = 0;
        private string _CUSTOMERTYPE = "";

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string FULLNAME
        {
            get { return _FULLNAME; }
            set { _FULLNAME = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; }
        }

        public double MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }

        public string CUSTOMERTYPE
        {
            get { return _CUSTOMERTYPE; }
            set { _CUSTOMERTYPE = value; }
        }

    }
}
