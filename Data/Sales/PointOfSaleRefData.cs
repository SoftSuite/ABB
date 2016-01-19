using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PointOfSaleRefData
    {
        private double _REQUISITION = 0;
        private string _REQUISITIONCODE = "";
        private double _CUSTOMER = 0;
        private string _CUSTOMERCODE = "";
        private double _CUSTOMERDISCOUNT = 0;
        private ArrayList _REFITEM = new ArrayList();

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public string REQUISITIONCODE
        {
            get { return _REQUISITIONCODE; }
            set { _REQUISITIONCODE = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }

        public double CUSTOMERDISCOUNT
        {
            get { return _CUSTOMERDISCOUNT; }
            set { _CUSTOMERDISCOUNT = value; }
        }

        public ArrayList REFITEM
        {
            get { return _REFITEM; }
            set { _REFITEM = value; }
        }
    }
}
