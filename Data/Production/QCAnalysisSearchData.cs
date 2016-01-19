using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class QCAnalysisSearchData
    {
        private double _STLOID = 0;
        private string _QCCODE = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);
        private string _CODE = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";

        public double STLOID
        {
            get { return _STLOID; }
            set { _STLOID = value; }
        }

        public string QCCODE
        {
            get { return _QCCODE; }
            set { _QCCODE = value; }
        }

        public DateTime DATEFROM
        {
            get { return _DATEFROM; }
            set { _DATEFROM = value; }
        }

        public DateTime DATETO
        {
            get { return _DATETO; }
            set { _DATETO = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
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

