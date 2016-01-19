using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class ToDoListExpireData
    {
        private string _PRODUCTNAME = "";
        private double _TIME = 0;

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double TIME
        {
            get { return _TIME; }
            set { _TIME = value; }
        }
    }
}
