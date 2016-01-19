using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Admin
{
    public class MessageData
    {
        private double _LOID = 0;
        private string _MESSAGE = "";
        private DateTime _EFDATE = new DateTime(1, 1, 1);
        private DateTime _EPDATE = new DateTime(1, 1, 1);

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string MESSAGE
        {
            get { return _MESSAGE; }
            set { _MESSAGE = value; }
        }

        public DateTime EFDATE
        {
            get { return _EFDATE; }
            set { _EFDATE = value; }
        }

        public DateTime EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }
    }
}
