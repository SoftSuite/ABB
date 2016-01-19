using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class ToDoListStockInData
    {
        private string _ORDERTYPE = "";
        private string _PRODUCTNAME = "";
        private string _CODE = "";
        private DateTime _DUEDATE = new DateTime(1,1,1);
        private string _STATUS = "";
        private double _WAREHOUSE = 0;

        public string ORDERTYPE
        {
            get { return _ORDERTYPE; }
            set { _ORDERTYPE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
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

    }
}
