using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class InvoiceReturnData
    {
        private string _INVCODE = "";
        private string _CUSTOMERNAME = "";
        private string _PRODUCTNAME = "";
        private string _CUSTOMERCODE = "";

        public string INVCODE
        {
            get { return _INVCODE; }
            set { _INVCODE = value; }
        }

        public string CUSTOMERNAME
        {
            get { return _CUSTOMERNAME; }
            set { _CUSTOMERNAME = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string CUSTOMERCODE
        {
            get { return _CUSTOMERCODE; }
            set { _CUSTOMERCODE = value; }
        }
        
    }
}
