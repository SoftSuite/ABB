using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class StockOutFGSearchData
    {
        private double _DOCTYPE = 0;
        private string _STOCKOUTCODE = "";
        private DateTime _CREATEFROM = new DateTime(1, 1, 1);
        private DateTime _CREATETO = new DateTime(1, 1, 1);
        private string _CREATEBY = "";
        private double _CUSTOMER = 0;
        private string _REQUISITIONCODE = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";
        private DateTime _RESERVEDATEFROM = new DateTime(1, 1, 1);
        private DateTime _RESERVEDATETO = new DateTime(1, 1, 1);

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public string STOCKOUTCODE
        {
            get { return _STOCKOUTCODE; }
            set { _STOCKOUTCODE = value; }
        }

        public DateTime CREATEFROM
        {
            get { return _CREATEFROM; }
            set { _CREATEFROM = value; }
        }

        public DateTime CREATETO
        {
            get { return _CREATETO; }
            set { _CREATETO = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public string REQUISITIONCODE
        {
            get { return _REQUISITIONCODE; }
            set { _REQUISITIONCODE = value; }
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

        public DateTime RESERVEDATEFROM
        {
            get { return _RESERVEDATEFROM; }
            set { _RESERVEDATEFROM = value; }
        }

        public DateTime RESERVEDATETO
        {
            get { return _RESERVEDATETO; }
            set { _RESERVEDATETO = value; }
        }
    }
}
