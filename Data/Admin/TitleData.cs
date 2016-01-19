using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Admin
{
    public class TitleData
    {
        double _LOID = 0;
        string _CODE = "";
        string _NAME = "";
        string _ACTIVE = "";

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
        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
        }
    }
}
