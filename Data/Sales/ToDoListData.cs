using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ToDoListData
    {
        private string _CODE = "";
        private DateTime _RESERVEDATEFROM = new DateTime(1, 1, 1);
        private DateTime _RESERVEDATETO = new DateTime(1, 1, 1);
        private double _CUSTOMER = 0;
        private string _STATUS = "";

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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

        public double CUSTOMER
        {
            get { return _CUSTOMER; }
            set { _CUSTOMER = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

    }
}
