using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Production
{
    public class ReturnRequestSearchData
    {
        private string _CODE = "";
        private DateTime _REQDATEFROM = new DateTime(1, 1, 1);
        private DateTime _REQDATETO = new DateTime(1, 1, 1);
        private DateTime _MFGDATEFROM = new DateTime(1, 1, 1);
        private DateTime _MFGDATETO = new DateTime(1, 1, 1);
        private string _PRODUCT = "";
        private string _LOTNO = "";
        private string _STATUSFROM = "";
        private string _STATUSTO = "";

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public DateTime REQDATEFROM
        {
            get { return _REQDATEFROM; }
            set { _REQDATEFROM = value; }
        }

        public DateTime REQDATETO
        {
            get { return _REQDATETO; }
            set { _REQDATETO = value; }
        }

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

        public string PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
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
