using System;
using System.Collections.Generic;
using System.Text;


namespace ABB.Data.Sales
{
    public class ProductStockInShopSearchData
    {
        int _ORDERNO = 0;
        Double _LOID = 0;
        string _SICODE = "";
        DateTime _RECEIVEDATE = new DateTime(1, 1, 1);
        string _RQCODE = "";
        string _REQDATE = "";
        double _TOTAL = 0;
        double _WAREHOUSE = 0;
        bool _CHKAPPROVE = false;
        double _STILOID = 0;
        DateTime _CREATEON = new DateTime(1, 1, 1);
        string _REMARK = "";
        string _CREATEBY = "";
        string _SISTATUS = "";
        string  _SISTATUSNAME = "";

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public Double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public string SICODE
        {
            get { return _SICODE; }
            set { _SICODE = value; }
        }

        public DateTime RECEIVEDATE
        {
            get { return _RECEIVEDATE; }
            set { _RECEIVEDATE = value; }
        }

        public string RQCODE
        {
            get { return _RQCODE; }
            set { _RQCODE = value; }
        }

        public String REQDATE
        {
            get { return _REQDATE; }
            set { _REQDATE = value; }
        }

        public double TOTAL
        {
            get { return _TOTAL; }
            set { _TOTAL = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }

        public bool CHKAPPROVE
        {
            get { return _CHKAPPROVE; }
            set { _CHKAPPROVE = value; }
        }

        public double STILOID
        {
            get { return _STILOID; }
            set { _STILOID = value; }
        }

        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }

        public String REMARK
        {
            get {return _REMARK;}
            set { REMARK = value; }
        }

        public String CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public String SISTATUS
        {
            get { return _SISTATUS; }
            set { _SISTATUS = value; }
        }

        public String SISTATUSNAME
        {
            get { return _SISTATUSNAME; }
            set { _SISTATUSNAME = value; }
        }
    }
}
