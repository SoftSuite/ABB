using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class SearchInvoiceData
    {
        private double _REQUISITIONTYPE = 0;
        private string _INVCODEFROM = "";
        private string _INVCODETO = "";
        private string _CUSTOMER = "";

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public string INVCODEFROM
        {
            get { return _INVCODEFROM; }
            set { _INVCODEFROM = value; }
        }

        public string INVCODETO
        {
            get { return _INVCODETO; }
            set { _INVCODETO = value; }
        }

        public string CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }
    }
}
