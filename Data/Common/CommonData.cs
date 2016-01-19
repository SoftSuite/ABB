using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Common
{
    public class CommonData
    {
        private string _CREATEBY = "";
        private DateTime _CREATEON = new DateTime(1, 1, 1);
        private string _UPDATEBY = "";
        private DateTime _UPDATEON = new DateTime(1, 1, 1);

        public string CREATEBY
        {
            get { return _CREATEBY; }
            set { _CREATEBY = value; }
        }

        public DateTime CREATEON
        {
            get { return _CREATEON; }
            set { _CREATEON = value; }
        }

        public string UPDATEBY
        {
            get { return _UPDATEBY; }
            set { _UPDATEBY = value; }
        }

        public DateTime UPDATEON
        {
            get { return _UPDATEON; }
            set { _UPDATEON = value; }
        }
    }
}
