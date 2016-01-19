using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ProcessData
    {
        private double _LOID = 0;
        private string _PROCESS = "";
        private double _PRODUCT = 0;
        private string _ACTIVE = "";
        private string _RADIATION = "";


        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string PROCESS
        {
            get { return _PROCESS; }
            set { _PROCESS = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }

        public string RADIATION
        {
            get { return _RADIATION; }
            set { _RADIATION = value; }
        }
    }
}
