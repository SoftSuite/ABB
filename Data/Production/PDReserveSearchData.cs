using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class PDReserveSearchData
    {
        private string _CODE = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _PRODUCTNAME = "";
        private string _LOTNO = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";
        private double _REFWAREHOUSE = 0;

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
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

        public double REFWAREHOUSE
        {
            get { return _REFWAREHOUSE; }
            set { _REFWAREHOUSE = value; }
        }
    }
}
