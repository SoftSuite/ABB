using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class PlanOrderSearchData
    {
        private string _YEARFROM = "";
        private string _YEARTO = "";
        private DateTime _CREATEFROM = new DateTime(1, 1, 1);
        private DateTime _CREATETO = new DateTime(1, 1, 1);
        private DateTime _CONFIRMFROM = new DateTime(1, 1, 1);
        private DateTime _CONFIRMTO = new DateTime(1,1,1);
        private string _STATUSFROM = "";
        private string _STATUSTO = "";

        public string YEARFROM
        {
            get { return _YEARFROM; }
            set { _YEARTO = value; }
        }

        public string YEARTO
        {
            get { return _YEARTO; }
            set { _YEARTO = value; }
        }

        public DateTime CREATEFROM
        {
            get { return _CREATEFROM; }
            set { _CREATEFROM = value; }
        }

        public DateTime CREATETO
        {
            get { return _CREATETO; }
            set { _CREATETO = value; }
        }

        public DateTime CONFIRMFROM
        {
            get { return _CONFIRMFROM; }
            set { _CONFIRMFROM = value; }
        }

        public DateTime CONFIRMTO
        {
            get { return _CONFIRMTO; }
            set { _CONFIRMTO = value; }
        }

        public string STATUSFROM
        {
            get { return _STATUSFROM; }
            set { _STATUSFROM = value; }
        }

        public string STATUSTO
        {
            get { return _STATUSTO; }
            set { _STATUSTO = value; }
        }
    }
}
