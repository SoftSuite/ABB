using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory
{
    public class StockMovementSearchData
    {
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _PRODUCTNAME = "";
        private double _PRODUCTTYPE = 0;
        private double _PRODUCTGROUP = 0;
        private string _LOTNO = "";
        private double _ZONE = 0;
        private double _ZONEFROM = 0;
        private double _ZONETO = 0;

        public DateTime DATEFROM
        {
            get { return _DATEFROM; }
            set { _DATEFROM = value; }
        }

        public DateTime DATETO
        {
            get { return _DATETO; }
            set { _DATETO = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public double PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }

        public double PRODUCTGROUP
        {
            get { return _PRODUCTGROUP; }
            set { _PRODUCTGROUP = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public double ZONE
        {
            get { return _ZONE; }
            set { _ZONE = value; }
        }

        public double ZONEFROM
        {
            get { return _ZONEFROM; }
            set { _ZONEFROM = value; }
        }

        public double ZONETO
        {
            get { return _ZONETO; }
            set { _ZONETO = value; }
        }
    }
}
