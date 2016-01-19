using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ReqMaterialData
    {
        private double _LOID = 0;
        private string _CREATEBY = "";
        private DateTime _CREATEON = new DateTime(1, 1, 1);
        private string _UPDATEBY = "";
        private DateTime _UPDATEON = new DateTime(1, 1, 1);
        private double _REQUISITION = 0;
        private double _PRODUCT = 0;
        private double _MASTER = 0;
        private double _USEQTY = 0;
        private double _WASTEQTYMAT = 0;
        private string _WASTEQTYMAN = "";
        private double _RETURNQTY = 0;
        private double _CHANGEQTY = 0;
        private double _UNIT = 0;
        private string _ACTIVE = "";
        private string _REMARK = "";
        private double _YIELDMAT = 0;
        private double _YIELDMAM = 0;
        private double _BOM = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }

        public string UPDATEBY
        {
            get { return _UPDATEBY; }
            set { _UPDATEBY = value; }
        }

        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
            set { _UPDATEON = value; }
        }

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
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

        public string WASTEQTYMAN
        {
            get { return _WASTEQTYMAN; }
            set { _WASTEQTYMAN = value; }
        }

        public double RETURNQTY
        {
            get { return _RETURNQTY; }
            set { _RETURNQTY = value; }
        }

        public double CHANGEQTY
        {
            get { return _CHANGEQTY; }
            set { _CHANGEQTY = value; }
        }

        public double UNIT
        {
            get { return _UNIT; }
            set { _UNIT = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public double YIELDMAT
        {
            get { return _YIELDMAT; }
            set { _YIELDMAT = value; }
        }

        public double YIELDMAM
        {
            get { return _YIELDMAM; }
            set { _YIELDMAM = value; }
        }

        public double BOM
        {
            get { return _BOM; }
            set { _BOM = value; }
        }
    }
}
