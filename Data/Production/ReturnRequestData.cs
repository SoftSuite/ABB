using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;


namespace ABB.Data.Production
{
    public class ReturnRequestData
    {
        private double _LOID = 0;
        private string _PRODUCT = "";
        private double _VPLOID = 0;
        private string _CODE = "";
        private string _PDBARCODE = "";
        private string _LOTNO = "";
        private DateTime _REQDATE = new DateTime(1, 1, 1);
        private DateTime _MFGDATE = new DateTime(1, 1, 1);
        private string _PDNAME = "";
        private string _PDUNITNAME = "";
        private double _BATCHSIZE = 0;
        private string _BATCHSIZEUNITNAME = "";
        private double _STDQTY = 0;
        private double _REQUISITIONTYPE = 0;
        private string _STATUS = "";
        private string _CREATEBY = "";
        private string _REMARK = "";
        private string _ACTIVE = "";
        private double _WAREHOUSE = 0;
        private string _REFTABLE = "";
        private double _REFLOID = 0;
        private ArrayList _ITEM = new ArrayList();

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double VPLOID
        {
            get { return _VPLOID; }
            set { _VPLOID = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string PDBARCODE
        {
            get { return _PDBARCODE; }
            set { _PDBARCODE = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public DateTime REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }

        public string PDNAME
        {
            get { return _PDNAME; }
            set { _PDNAME = value; }
        }

        public string PDUNITNAME
        {
            get { return _PDUNITNAME; }
            set { _PDUNITNAME = value; }
        }

        public double BATCHSIZE
        {
            get { return _BATCHSIZE; }
            set { _BATCHSIZE = value; }
        }

        public string BATCHSIZEUNITNAME
        {
            get { return _BATCHSIZEUNITNAME; }
            set { _BATCHSIZEUNITNAME = value; }
        }

        public double STDQTY
        {
            get { return _STDQTY; }
            set { _STDQTY = value; }
        }

        public double REQUISITIONTYPE
        {
            get { return _REQUISITIONTYPE; }
            set { _REQUISITIONTYPE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public ArrayList ITEM
        {
            get { return _ITEM; }
            set { _ITEM = value; }
        }
    }
}
