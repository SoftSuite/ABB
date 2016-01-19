using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Search
{
    public class PopupStockoutBasketData
    {
        private string _BARCODE = "";
        private string _NAME = "";

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
    }
}
