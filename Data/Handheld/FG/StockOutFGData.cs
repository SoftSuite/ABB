using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Handheld.FG
{
    public class StockOutFGData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _REQCODE = "";
        private string _DOCNAME = "";

        public double STOCKOUT
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string REQCODE
        {
            get { return _REQCODE; }
            set { _REQCODE = value; }
        }

        public string DOCNAME
        {
            get { return _DOCNAME; }
            set { _DOCNAME = value; }
        }

    }
}
