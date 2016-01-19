using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Admin
{
    public class OfficerData
    {
        private double _LOID = 0;
        private string _TNAME = "";
        private string _LASTNAME = "";
        private double _DIVISION = 0;
        private string _USERID = "";
        private string _PASSWORD = "";
        private string _PASSCONFIRM = "";
        private DateTime _EFDATE = new DateTime(1, 1, 1);
        private DateTime _EPDATE = new DateTime(1, 1, 1);
        private string _NICKNAME = "";
        private DateTime _BIRTHDATE = new DateTime(1, 1, 1);
        private string _TEL = "";
        private string _EMAIL = "";
        private string _ADDRESS = "";
        private string _ROAD = "";
        private double _PROVINCE = 0;
        private double _AMPHUR = 0;
        private double _TAMBOL = 0;
        private string _ZIPCODE = "";
        private string _REMARK = "";
        private double _TITLE = 0;

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }
        public string TNAME
        {
            get { return _TNAME; }
            set { _TNAME = value; }
        }
        public string LASTNAME
        {
            get { return _LASTNAME; }
            set { _LASTNAME = value; }
        }
        public double DIVISION
        {
            get { return _DIVISION; }
            set { _DIVISION = value; }
        }
        public string USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        public string PASSWORD
        {
            get { return _PASSWORD; }
            set { _PASSWORD = value; }
        }
        public string PASSCONFIRM
        {
            get { return _PASSCONFIRM; }
            set { _PASSCONFIRM = value; }
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
        public string NICKNAME
        {
            get { return _NICKNAME; }
            set { _NICKNAME = value; }
        }
        public DateTime BIRTHDATE
        {
            get { return _BIRTHDATE; }
            set { _BIRTHDATE = value; }
        }
        public string TEL
        {
            get { return _TEL; }
            set { _TEL = value; }
        }
        public string EMAIL
        {
            get { return _EMAIL; }
            set { _EMAIL = value; }
        }
        public string ADDRESS
        {
            get { return _ADDRESS; }
            set { _ADDRESS = value; }
        }
        public string ROAD
        {
            get { return _ROAD; }
            set { _ROAD = value; }
        }
        public double PROVINCE
        {
            get { return _PROVINCE; }
            set { _PROVINCE = value; }
        }
        public double AMPHUR
        {
            get { return _AMPHUR; }
            set { _AMPHUR = value; }
        }
        public double TAMBOL
        {
            get { return _TAMBOL; }
            set { _TAMBOL = value; }
        }
        public string ZIPCODE
        {
            get { return _ZIPCODE; }
            set { _ZIPCODE = value; }
        }
        public string REMARK
        {
            get { return _REMARK; }
            set { _REMARK = value; }
        }
        public double TITLE
        {
            get { return _TITLE; }
            set { _TITLE = value; }
        }
    }
}
