using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class PurchaseOrderSearchData
    {
        private string _POCODE = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _PRCODE = "";
        private double _PURCHASETYPE = 0;
        private double _PRODUCT = 0;
        private double _DIVISION = 0;
        private string _STATUSFROM = "";
        private string _STATUSTO = "";

        public string POCODE
        {
            get { return _POCODE; }
            set { _POCODE = value; }
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

        public string PRCODE
        {
            get { return _PRCODE; }
            set { _PRCODE = value; }
        }

        public double PURCHASETYPE
        {
            get { return _PURCHASETYPE; }
            set { _PURCHASETYPE = value; }
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
