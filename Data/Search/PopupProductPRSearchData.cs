using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class PopupProductPRSearchData
    {
        private string _PRCODEFROM = "";
        private string _PRCODETO = "";
        private DateTime _DUEDATEFROM = new DateTime(1, 1, 1);
        private DateTime _DUEDATETO = new DateTime(1, 1, 1);
        private string _PURCHASETYPE = "";
        private string _PRODUCT = "";
        private string _DIVISION = "";

        public string PRCODEFROM
        {
            get { return _PRCODEFROM; }
            set { _PRCODEFROM = value; }
        }

        public string PRCODETO
        {
            get { return _PRCODETO; }
            set { _PRCODETO = value; }
        }

        public DateTime DUEDATEFROM
        {
            get { return _DUEDATEFROM; }
            set { _DUEDATEFROM = value; }
        }

        public DateTime DUEDATETO
        {
            get { return _DUEDATETO; }
            set { _DUEDATETO = value; }
        }

        public string PURCHASETYPE
        {
            get { return _PURCHASETYPE; }
            set { _PURCHASETYPE = value; }
        }

        public string PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }
    }
}
