using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class ToDoListStockinOrderData
    {
        private string _ACCCODE = "";
        private double _DOCTYPE = 0;
        private double _SENDER = 0;
        private double _RECEIVER = 0;
        private DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        private string _STATUS = "";
        private ArrayList _ITEM = new ArrayList();

        public string ACCCODE
        {
            get { return _ACCCODE; }
            set { _ACCCODE = value; }
        }

        public double DOCTYPE
        {
            get { return _DOCTYPE; }
            set { _DOCTYPE = value; }
        }

        public double SENDER
        {
            get { return _SENDER; }
            set { _SENDER = value; }
        }

        public double RECEIVER
        {
            get { return _RECEIVER; }
            set { _RECEIVER = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }

    }
}
