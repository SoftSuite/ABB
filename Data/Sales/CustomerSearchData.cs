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
    public class CustomerSearchData
    {
        private string _CUSCODE = "";
        private string _CUSTYPE = "";
        private string _CUSNAME = "";
        private string _LASTNAME = "";
        private double _MEMBERTYPE = 0;
        private double _PROVINCE = 0;
        
        public string CUSCODE
        {
            get { return _CUSCODE; }
            set { _CUSCODE = value; }
        }

        public string CUSTYPE
        {
            get { return _CUSTYPE; }
            set { _CUSTYPE = value; }
        }

        public string CUSNAME
        {
            get { return _CUSNAME; }
            set { _CUSNAME = value; }
        }

        public string LASTNAME 
        {
            get { return _LASTNAME; }
            set { _LASTNAME=value; }
        }

        public double MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }

        public double PROVINCE
        {
            get { return _PROVINCE; }
            set { _PROVINCE = value; }
        }
    }
}
