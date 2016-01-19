using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ProductionLostSearchData
    {
        private DateTime _MFGDATEFROM = new DateTime(1, 1, 1);
        private DateTime _MFGDATETO = new DateTime(1, 1, 1);
        private DateTime _SENDFGDATEFROM = new DateTime(1, 1, 1);
        private DateTime _SENDFGDATETO = new DateTime(1, 1, 1);
        private DateTime _ORDERDATEFROM = new DateTime(1, 1, 1);
        private DateTime _ORDERDATETO = new DateTime(1, 1, 1);
        private double _PRODUCT = 0;
        private string _LOTNO = "";

        public DateTime MFGDATEFROM
        {
            get { return _MFGDATEFROM; }
            set { _MFGDATEFROM = value; }
        }

        public DateTime MFGDATETO
        {
            get { return _MFGDATETO; }
            set { _MFGDATETO = value; }
        }

        public DateTime SENDFGDATEFROM
        {
            get { return _SENDFGDATEFROM; }
            set { _SENDFGDATEFROM = value; }
        }

        public DateTime SENDFGDATETO
        {
            get { return _SENDFGDATETO; }
            set { _SENDFGDATETO = value; }
        }

        public DateTime ORDERDATEFROM
        {
            get { return _ORDERDATEFROM; }
            set { _ORDERDATEFROM = value; }
        }

        public DateTime ORDERDATETO
        {
            get { return _ORDERDATETO; }
            set { _ORDERDATETO = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
    }
}
