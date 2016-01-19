using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class ToDoListMinimumStockRequestData
    {
        private double _REQUESTBY = 0;
        private double _DIVISION = 0;
        private string _ORDERTYPE = "";
        private string _ACTIVE = "";
        private string _STATUS = "";
        private double _WAREHOUSE = 0;
        private double _REQUISITIONTYPE = 0;
        private ArrayList _ITEM = new ArrayList();

        public double REQUESTBY
        {
            get { return _REQUESTBY; }
            set { _REQUESTBY = value; }
        }

        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }

        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

    }
}
