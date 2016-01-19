using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ProductOrderSearchData
    {

        private string _CODE = "";
        private string _CODETO = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _PDNAME = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";



        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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

        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
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
    }
}
