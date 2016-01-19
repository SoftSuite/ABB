using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Admin
{
    public class SysConfigData
    {
        private double _LOID = 0;
        private string _CONFIGNAME = "";
        private string _CONFIGvALUE = "";
        private string _DESCRIPTION = "";

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string CONFIGNAME
        {
            get { return _CONFIGNAME; }
            set { _CONFIGNAME = value; }
        }

        public string CONFIGVALUE
        {
            get { return _CONFIGvALUE; }
            set { _CONFIGvALUE = value; }
        }

        public string DESCRIPTION
        {
            get { return _DESCRIPTION; }
            set { _DESCRIPTION = value; }
        }
    }
}
