using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public  class PDProductSearchData
    {
        private int _ORDERNO = 0;
        private double  _PDPLOID = 0;
        private double _PDLOID = 0;
        private string _LOTNO = "";
        private DateTime _MFGDATE = new DateTime(1, 1, 1);
        private string _PDNAME = "";
        private double _BATCHSIZE = 0;
        private double _STDQTY = 0;
        private double _PDQTY = 0;
        private string _UNAME = "";
        private string _PRODSTATUS = "";
        private string _PRODSTATUSNAME = "";
        private double _RANK = 0;

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public double PDPLOID
        {
            get { return _PDPLOID; }
            set { _PDPLOID = value; }
        }

        public double PDLOID
        {
            get { return _PDLOID; }
            set { _PDLOID = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
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

        public double BATCHSIZE
        {
            get { return _BATCHSIZE; }
            set { _BATCHSIZE = value; }
        }

        public double STDQTY
        {
            get { return _STDQTY; }
            set { _STDQTY = value; }
        }

        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }

        public string UNAME
        {
            get { return _UNAME; }
            set { _UNAME = value; }
        }

        public string PRODSTATUS
        {
            get { return _PRODSTATUS; }
            set { _PRODSTATUS = value; }
        }

        public string PRODSTATUSNAME
        {
            get { return _PRODSTATUSNAME; }
            set { _PRODSTATUSNAME = value; }
        }

        public double RANK
        {
            get { return _RANK; }
            set { _RANK = value; }
        }
    }
}
