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
    public class CustomerData
    {
        private double _LOID = 0;
        private string _CODE = "";
        private double _TITLE = 0;
        private string _NAME = "";
        private string _LASTNAME = "";
        private double _MEMBERTYPE = 0;
        private string _CUSTOMERTYPE = "";
        private string _IDENTITY = "";
        private string _PAYMENT = "";
        private double _CREDITDAY = 0;
        private double _CREDITAMOUNT = 0;
        private string _BILLADDRESS = "";
        private string _BILLROAD = "";
        private double _BILLTAMBOL = 0;
        private double _BILLAMPHUR = 0;
        private double _BILLPROVINCE = 0;
        private string _BILLZIPCODE = "";
        private string _BILLTEL = "";
        private string _BILLFAX = "";
        private string _BILLEMAIL = "";
        private double _CTITLE = 0;
        private string _CNAME = "";
        private string _CLASTNAME = "";
        private string _CADDRESS = "";
        private string _CROAD = "";
        private double _CTAMBOL = 0;
        private double _CAMPHUR = 0;
        private double _CPROVINCE = 0;
        private string _CZIPCODE = "";
        private string _CTEL = "";
        private string _CFAX = "";
        private string _CEMAIL = "";
        private string _CMOBILE = "";
        private string _SENDPLACE = "";
        private string _SENDADDRESS = "";
        private string _SENDROAD = "";
        private double _SENDTAMBOL = 0;
        private double _SENDAMPHUR = 0;
        private double _SENDPROVINCE = 0;
        private string _SENDZIPCODE = "";
        private string _SENDTEL = "";
        private string _SENDFAX = "";
        private string _SENDEMAIL = "";
        private string _DELIVERTYPE = "";
        private string _ACTIVE = "";
        private DateTime _EFDATE = new DateTime(1, 1, 1);
        private DateTime _EPDATE = new DateTime(1, 1, 1);
        private string _REMARK = "";

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

        public double TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
        }

        public string NAME
        {
            get { return _NAME; }
            set { _NAME = value; }
        }

        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; }
        }

        public double MEMBERTYPE
        {
            get { return _MEMBERTYPE; }
            set { _MEMBERTYPE = value; }
        }
        public string CUSTOMERTYPE
        {
            get { return _CUSTOMERTYPE; }
            set { _CUSTOMERTYPE = value; }
        }
        public string IDENTITY
        {
            get { return _IDENTITY; }
            set { _IDENTITY = value; }
        }
        public string PAYMENT
        {
            get { return _PAYMENT; }
            set { _PAYMENT = value; }
        }
        public double CREDITDAY
        {
            get { return _CREDITDAY; }
            set { _CREDITDAY = value; }
        }
        public double CREDITAMOUNT
        {
            get { return _CREDITAMOUNT; }
            set { _CREDITAMOUNT = value; }
        }
        public string BILLADDRESS
        {
            get { return _BILLADDRESS; }
            set { _BILLADDRESS = value; }
        }
        public string BILLROAD
        {
            get { return _BILLROAD; }
            set { _BILLROAD = value; }
        }
        public double BILLTAMBOL
        {
            get { return _BILLTAMBOL; }
            set { _BILLTAMBOL = value; }
        }
        public double BILLAMPHUR
        {
            get { return _BILLAMPHUR; }
            set { _BILLAMPHUR = value; }
        }
        public double BILLPROVINCE
        {
            get { return _BILLPROVINCE; }
            set { _BILLPROVINCE = value; }
        }
        public string BILLZIPCODE
        {
            get { return _BILLZIPCODE; }
            set { _BILLZIPCODE = value; }
        }
        public string BILLTEL
        {
            get { return _BILLTEL; }
            set { _BILLTEL = value; }
        }
        public string BILLFAX
        {
            get { return _BILLFAX; }
            set { _BILLFAX = value; }
        }
        public string BILLEMAIL
        {
            get { return _BILLEMAIL; }
            set { _BILLEMAIL = value; }
        }
        public double CTITLE
        {
            get { return _CTITLE; }
            set { _CTITLE = value; }
        }
        public string CNAME
        {
            get { return _CNAME; }
            set { _CNAME = value; }
        }
        public string CLASTNAME
        {
            get { return _CLASTNAME; }
            set { _CLASTNAME = value; }
        }
        public string CADDRESS
        {
            get { return _CADDRESS; }
            set { _CADDRESS = value; }
        }
        public string CROAD
        {
            get { return _CROAD; }
            set { _CROAD = value; }
        }
        public double CTAMBOL
        {
            get { return _CTAMBOL; }
            set { _CTAMBOL = value; }
        }
        public double CAMPHUR
        {
            get { return _CAMPHUR; }
            set { _CAMPHUR = value; }
        }
        public double CPROVINCE
        {
            get { return _CPROVINCE; }
            set { _CPROVINCE = value; }
        }
        public string CZIPCODE
        {
            get { return _CZIPCODE; }
            set { _CZIPCODE = value; }
        }
        public string CTEL
        {
            get { return _CTEL; }
            set { _CTEL = value; }
        }
        public string CFAX
        {
            get { return _CFAX; }
            set { _CFAX = value; }
        }
        public string CEMAIL
        {
            get { return _CEMAIL; }
            set { _CEMAIL = value; }
        }
        public string CMOBILE
        {
            get { return _CMOBILE; }
            set { _CMOBILE = value; }
        }
        public string SENDPLACE
        {
            get { return _SENDPLACE; }
            set { _SENDPLACE = value; }
        }
        public string SENDADDRESS
        {
            get { return _SENDADDRESS; }
            set { _SENDADDRESS = value; }
        }
        public string SENDROAD
        {
            get { return _SENDROAD; }
            set { _SENDROAD = value; }
        }
        public double SENDTAMBOL
        {
            get { return _SENDTAMBOL; }
            set { _SENDTAMBOL = value; }
        }
        public double SENDAMPHUR
        {
            get { return _SENDAMPHUR; }
            set { _SENDAMPHUR = value; }
        }
        public double SENDPROVINCE
        {
            get { return _SENDPROVINCE; }
            set { _SENDPROVINCE = value; }
        }
        public string SENDZIPCODE
        {
            get { return _SENDZIPCODE; }
            set { _SENDZIPCODE = value; }
        }
        public string SENDTEL
        {
            get { return _SENDTEL; }
            set { _SENDTEL = value; }
        }
        public string SENDFAX
        {
            get { return _SENDFAX; }
            set { _SENDFAX = value; }
        }
        public string SENDEMAIL
        {
            get { return _SENDEMAIL; }
            set { _SENDEMAIL = value; }
        }
        public string DELIVERTYPE
        {
            get { return _DELIVERTYPE; }
            set { _DELIVERTYPE = value; }
        }
        public string ACTIVE
        {
            get { return _ACTIVE; }
            set { _ACTIVE = value; }
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
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
    }
}
