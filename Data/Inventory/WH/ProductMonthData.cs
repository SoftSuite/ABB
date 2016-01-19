using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.WH
{
    public class ProductMonthData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private double _PRODUCT = 0;
        private string[] _Month = new string[12];


        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }
        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }
        public string[] Month
        {
            get {return _Month;}
            set {_Month = value;}
        }
    }
}
