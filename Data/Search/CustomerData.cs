using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class CustomerData
    {
        private string _CODE = "";
        private string _FULLNAME = "";
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
