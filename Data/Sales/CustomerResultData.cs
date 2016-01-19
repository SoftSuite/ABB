using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Pom
/// Create Date: 13 Dec 2007
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>

namespace ABB.Data.Sales
{
    public class CustomerResultData
    {
        private string _LOID = "";
        private int _ORDERNO = 0;
        private string _CODE = "";
        private string _CUSNAME = "";
        private string _MEMBERTYPE = "";
        private string _CUSTOMERTYPE = "";
        private string _PAYMENT = "";
        private string _EPDATE = "";


        public string LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string CODE
        {
            get { return _CODE; }
            set { _CODE = value; }
        }

        public string CUSNAME
        {
            get { return _CUSNAME; }
            set { _CUSNAME = value; }
        }

        public string MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }

        public string CUSTOMERTYPE
        {
            get { return _CUSTOMERTYPE; }
            set { _CUSTOMERTYPE = value; }
        }

        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }

        public string EPDATE
        {
            get { return _EPDATE; }
            set { _EPDATE = value; }
        }
    }
}
