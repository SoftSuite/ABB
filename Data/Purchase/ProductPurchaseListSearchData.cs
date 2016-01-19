using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class ProductPurchaseListSearchData
    {
        private string _CODE = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private double _PURCHASETYPE = 0;
        private double _PRODUCT = 0;

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
    }
}
