using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class SupplierData
    {
        private string _CODE = "";
        private string _NAME = "";

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
    }
}
