using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class PopupPOSearchData
    {
        private string _CODEFROM = "";
        private string _CODETO = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _SUPPLIER = "";


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

        public string SUPPLIER
        {
            get { return _SUPPLIER; }
            set { _SUPPLIER = value; }
        }


    }
}

