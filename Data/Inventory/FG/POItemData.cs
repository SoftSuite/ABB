using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class POItemData
    {
        double _LOID = 0;
        string _CODE = "";
        double _QTY = 0;
        double _UNIT = 0;
        string _BARCODE = "";
        string _NAME = "";

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

        public double QTY
        {
            get { return _QTY; }
            set { _QTY = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

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
