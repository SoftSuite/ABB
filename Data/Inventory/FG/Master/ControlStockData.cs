using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Pom
/// Create Date: 19 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 

namespace ABB.Data.Inventory.FG.Master
{
    public class ControlStockData
    {
        private double _LOID = 0;
        private double _WAREHOUSE = 0;
        private double _PRODUCT = 0;
        private double _STANDARD = 0;
        private double _MINIMUM = 0;
        private double _MAXIMUM = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double STANDARD
        {
            get { return _STANDARD; }
            set { _STANDARD = value; }
        }

        public double MINIMUM
        {
            get { return _MINIMUM; }
            set { _MINIMUM = value; }
        }

        public double MAXIMUM
        {
            get { return _MAXIMUM; }
            set { _MAXIMUM = value; }
        }
    }
}
