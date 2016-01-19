using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace ABB.Data
{
    public class PlanData : ABB.Data.Common.CommonData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private string _PLANTYPE = "";
        private string _DESCRIPTION = "";
        private DateTime _CONFIRMDATE = new DateTime(1, 1, 1);
        private string _ACTIVE = "";
        private string _STATUS = "";
        private string _YEAR = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string PLANTYPE
        {
            get { return _PLANTYPE; }
            set { _PLANTYPE = value; }
        }

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }

        public DateTime CONFIRMDATE
        {
            get { return _CONFIRMDATE; }
            set { _CONFIRMDATE = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string STATUS
        {
            get { return _STATUS; }
            set { _STATUS = value; }
        }

        public string YEAR
        {
            get { return _YEAR; }
            set { _YEAR = value; }
        }

    }
}
