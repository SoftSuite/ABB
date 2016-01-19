using System;
using System.Collections.Generic;
using System.Text;

namespace DIP.Data.Admin
{
    public class SysMenuSearchData
    {
        private double _SYSMENUGROUP = 0;
        private double _PARENT = 0;
        private string _TITLE = "";

        public double SYSMENUGROUP
        {
            get { return _SYSMENUGROUP; }
            set { _SYSMENUGROUP = value; }
        }
        public double PARENT
        {
            get { return _PARENT; }
            set { _PARENT = value; }
        }
        public string TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
        }
    }
}
