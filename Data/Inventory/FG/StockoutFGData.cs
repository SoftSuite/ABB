using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;

namespace ABB.Data.Inventory.FG
{
    public class StockoutFGData : StockOutData
    {
        private string _REQUISITIONCODE = "";
        private DateTime _RESERVEDATE = new DateTime(1, 1, 1);
        private string _CUSTOMERCODE = "";
        private string _CUSTOMERNAME = "";
        private string _WAREHOUSECODE = "";
        private string _WAREHOUSENAME = "";

        private double _TOTAL = 0;
        private ArrayList _ITEM = new ArrayList();

        public string REQUISITIONCODE
        {
            get { return _REQUISITIONCODE; }
            set { _REQUISITIONCODE = value; }
        }

        public DateTime RESERVEDATE
        {
            get { return _RESERVEDATE; }
            set { _RESERVEDATE = value; }
        }

        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public string WAREHOUSECODE
        {
            get { return _WAREHOUSECODE; }
            set { _WAREHOUSECODE = value; }
        }

        public string WAREHOUSENAME
        {
            get { return _WAREHOUSENAME; }
            set { _WAREHOUSENAME = value; }
        }

        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

    }
}

