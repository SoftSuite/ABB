using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ProductionQCSearchData
    {
        private DateTime _MFGDATEFROM = new DateTime(1, 1, 1);
        private DateTime _MFGDATETO = new DateTime(1, 1, 1);
        private DateTime _SENDQCDATEFROM = new DateTime(1, 1, 1);
        private DateTime _SENDQCDATETO = new DateTime(1, 1, 1);
        private DateTime _EXPDATEFROM = new DateTime(1, 1, 1);
        private DateTime _EXPDATETO = new DateTime(1, 1, 1);
        private double _PRODUCT = 0;
        private string _LOTNO = "";

        public DateTime MFGDATEFROM
        {
            get { return _MFGDATEFROM; }
            set { _MFGDATEFROM = value; }
        }

        public DateTime MFGDATETO
        {
            get { return _MFGDATETO; }
            set { _MFGDATETO = value; }
        }

        public DateTime SENDQCDATEFROM
        {
            get { return _SENDQCDATEFROM; }
            set { _SENDQCDATEFROM = value; }
        }

        public DateTime SENDQCDATETO
        {
            get { return _SENDQCDATETO; }
            set { _SENDQCDATETO = value; }
        }

        public DateTime EXPDATEFROM
        {
            get { return _EXPDATEFROM; }
            set { _EXPDATEFROM = value; }
        }

        public DateTime EXPDATETO
        {
            get { return _EXPDATETO; }
            set { _EXPDATETO = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }
    }
}
