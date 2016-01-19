using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class ControlStockSearchData
    {
        int _ORDERNO = 0;
        string _BARCODE = "";
        string _PRODUCTNAME = "";
        string _STANDARD = "";
        string _MINIMUM = "";
        string _MAXIMUM = "";
        string _PMLOID = "";
        string _WAREHOUSE = "";
        string _WHLOID = "";
        string _PRODUCT = "";

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string STANDARD
        {
            get { return _STANDARD; }
            set { _STANDARD = value; }
        }

        public string MINIMUM
        {
            get { return _MINIMUM; }
            set { _MINIMUM = value; }
        }

        public string MAXIMUM
        {
            get { return _MAXIMUM; }
            set { _MAXIMUM = value; }
        }

        public string PMLOID
        {
            get { return _PMLOID; }
            set { _PMLOID = value; }
        }

        public string WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public string WHLOID
        {
            get { return _WHLOID; }
            set { _WHLOID = value; }
        }

        public string PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
    }


}
