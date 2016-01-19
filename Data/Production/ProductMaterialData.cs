using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ProductMaterialData
    {
        private double _LOID = 0;
        private double _PRODUCT = 0;
        private double _UNIT = 0;
        private double _REQUISITION = 0;
        private double _MASTER = 0;
        private double _USEQTY = 0;
        private double _WASTEQTYMAT = 0;
        private double _RETURNQTY = 0;
        private string _REMARK = "";
        private string _ACTIVE = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public double MASTER
        {
            get { return _MASTER; }
            set { _MASTER = value; }
        }

        public double USEQTY
        {
            get { return _USEQTY; }
            set { _USEQTY = value; }
        }

        public double WASTEQTYMAT
        {
            get { return _WASTEQTYMAT; }
            set { _WASTEQTYMAT = value; }
        }

        public double RETURNQTY
        {
            get { return _RETURNQTY; }
            set { _RETURNQTY = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
    }
}

