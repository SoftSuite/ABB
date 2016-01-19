using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.WH
{
    public class StockOutItemWHData : ABB.Data.Handheld.WH.StockOutWHData
    {
        private string _PRODUCTNAME = "";
        private double _QTY = 0;
        private string _LOTNO = "";
        private string _UNITNAME = "";

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public string UNITNAME
        {
            get { return _UNITNAME; }
            set { _UNITNAME = value; }
        }

    }
}
