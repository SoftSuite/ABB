using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ProductStockinQuarantineSearchData
    {
        private DateTime _MFGDATE = new DateTime(1, 1, 1);
        private double _PRODUCT = 0;
        private string _LOTNO = "";

        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

    }
}
