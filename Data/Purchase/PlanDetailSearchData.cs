using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Purchase
{
    public class PlanDetailSearchData : Inventory.FG.PlanDetailSearchData
    {
        double _YEAR = 0;

        public double YEAR
        {
            get { return _YEAR; }
            set { _YEAR = value; }
        }
    }
}
