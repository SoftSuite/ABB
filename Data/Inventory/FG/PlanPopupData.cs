using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Inventory.FG
{
    public class PlanPopupData
    {
        private double _PLAN = 0;
        private int _DAY = 0;
        private double _PDLOID = 0;
        private double _PDQTY = 0;
        private double _POLOID = 0;
        private double _POQTY = 0;
        private double _RECEIVELOID = 0;

        public double PLAN
        {
            get { return _PLAN; }
            set { _PLAN = value; }
        }

        public int DAY
        {
            get { return _DAY; }
            set { _DAY = value; }
        }

        public double PDLOID
        {
            get { return _PDLOID; }
            set { _PDLOID = value; }
        }

        public double POLOID
        {
            get { return _POLOID; }
            set { _POLOID = value; }
        }

        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }

        public double POQTY
        {
            get { return _POQTY; }
            set { _POQTY = value; }
        }

        public double RECEIVELOID
        {
            get { return _RECEIVELOID; }
            set { _RECEIVELOID = value; }
        }

    }
}
