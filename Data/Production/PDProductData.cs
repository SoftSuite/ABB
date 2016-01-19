using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Nang
/// Create Date: 26 Feb 2008
/// ---------------------------------------
/// Modify By: -
/// Modify From: -
/// Modify Date: -
/// ---------------------------------------
/// Remark: -
/// Description: -
/// =======================================
/// </summary>
/// 

namespace ABB.Data.Production
{

    public class PDProductData
    {
        private double _PRICE = 0;
        private double _PROCESS = 0;
        private double _LOID = 0;
        private string _CREATEBY = "";
        private DateTime _CREATEON = new DateTime(1, 1, 1);
        private string _UPDATEBY = "";
        private DateTime _UPDATEON = new DateTime(1, 1, 1);
        private string _LOTNO = "";
        private double _PDORDER = 0;
        private double _PRODUCT = 0;
        private double _BOM = 0;
        private double _BATCHSIZE = 0;
        private double _BATCHSIZEUNIT = 0;
        private string _PRODSTATUS = "";
        private DateTime _DUEDATE = new DateTime(1, 1, 1);
        private DateTime _MFGDATE = new DateTime(1, 1, 1);
        private DateTime _EXPDATE = new DateTime(1, 1, 1);
        private double _STDQTY = 0;
        private double _LOSTQTY = 0;
        private double _PDQTY = 0;
        private double _YIELD = 0;
        private string _PACKING = "";
        private string _PACKAGE = "";

        private DateTime _RADIATEDATE = new DateTime(1, 1, 1);
        private double _RADIATEQTY = 0;
        private double _RADIATEUNIT = 0;
        private string _RADIATEREMARK = "";

        private DateTime _RADIATERETDATE = new DateTime(1, 1, 1);
        private double _RADIATERETQTY = 0;
        private double _RADIATERETUNIT = 0;
        private string _RADIATERETREMARK = "";

        private DateTime _QUARANTINEDATE = new DateTime(1, 1, 1);
        private double _QUARANTINEQTY = 0;
        private string _QUARANTINEREMARK = "";
        private double _QUARANTINEUNIT = 0;

        private DateTime _SENDQCDATE = new DateTime(1, 1, 1);
        private double _QCQTY1 = 0;
        private double _QCQTY2 = 0;
        private double _QCQTY3 = 0;
        private string _QCRESULT = "";
        private string _QCREMARK = "";

        private DateTime _SENDFGDATE = new DateTime(1, 1, 1);
        private double _SENDFGQTY = 0;
        private string _SENDFGREMARK = "";

        private string _REFTABLE = "";
        private double _REFLOID = 0;

        private string _PRODUCTTYPE = "";
        private double _TOWAREHOUSE = 0;

        private DateTime _ANADATE = new DateTime(1, 1, 1);
        private string _ANACODE = "";

        private double _WAREHOUSE = 0;

        public double PRICE
        {
            get { return _PRICE; }
            set { _PRICE = value; }
        }

        public double PROCESS
        {
            get { return _PROCESS; }
            set { _PROCESS = value; }
        }

        public double QUARANTINEUNIT
        {
            get { return _QUARANTINEUNIT; }
            set { _QUARANTINEUNIT = value; }
        }

        public double LOID
        {
            get { return _LOID; }
            set { _LOID = value; }
        }

        public string  CREATEBY
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

        public string LOTNO
        {
            get { return _LOTNO; }
            set { _LOTNO = value; }
        }

        public double PDORDER
        {
            get { return _PDORDER; }
            set { _PDORDER = value; }
        }

        public double PRODUCT
        {
            get { return _PRODUCT; }
            set { _PRODUCT = value; }
        }

        public double BOM
        {
            get { return _BOM; }
            set { _BOM = value; }
        }

        public double BATCHSIZE
        {
            get { return _BATCHSIZE; }
            set { _BATCHSIZE = value; }
        }

        public double BATCHSIZEUNIT
        {
            get { return _BATCHSIZEUNIT; }
            set { _BATCHSIZEUNIT = value; }
        }

        public string PRODSTATUS
        {
            get { return _PRODSTATUS; }
            set { _PRODSTATUS = value; }
        }

        public DateTime DUEDATE
        {
            get { return _DUEDATE; }
            set { _DUEDATE = value; }
        }

        public DateTime MFGDATE
        {
            get { return _MFGDATE; }
            set { _MFGDATE = value; }
        }

        public DateTime EXPDATE
        {
            get { return _EXPDATE; }
            set { _EXPDATE = value; }
        }

        public double STDQTY
        {
            get { return _STDQTY; }
            set { _STDQTY = value; }
        }

        public double LOSTQTY
        {
            get { return _LOSTQTY; }
            set { _LOSTQTY = value; }
        }

        public double PDQTY
        {
            get { return _PDQTY; }
            set { _PDQTY = value; }
        }

        public double YIELD
        {
            get { return _YIELD; }
            set { _YIELD = value; }
        }

        public string PACKING
        {
            get { return _PACKING; }
            set { _PACKING = value; }
        }

        public string PACKAGE
        {
            get { return _PACKAGE; }
            set { _PACKAGE = value; }
        }

        public DateTime RADIATEDATE
        {
            get { return _RADIATEDATE; }
            set { _RADIATEDATE = value; }
        }

        public double RADIATEQTY
        {
            get { return _RADIATEQTY; }
            set { _RADIATEQTY = value; }
        }

        public double RADIATEUNIT
        {
            get { return _RADIATEUNIT; }
            set { _RADIATEUNIT = value; }
        }

        public string RADIATEREMARK
        {
            get { return _RADIATEREMARK; }
            set { _RADIATEREMARK = value; }
        }

        public DateTime RADIATERETDATE
        {
            get { return _RADIATERETDATE; }
            set { _RADIATERETDATE = value; }
        }

        public double RADIATERETQTY
        {
            get { return _RADIATERETQTY; }
            set { _RADIATERETQTY = value; }
        }

        public double RADIATERETUNIT
        {
            get { return _RADIATERETUNIT; }
            set { _RADIATERETUNIT = value; }
        }

        public string RADIATERETREMARK
        {
            get { return _RADIATERETREMARK; }
            set { _RADIATERETREMARK = value; }
        }

        public DateTime QUARANTINEDATE
        {
            get { return _QUARANTINEDATE; }
            set { _QUARANTINEDATE = value; }
        }

        public double QUARANTINEQTY
        {
            get { return _QUARANTINEQTY; }
            set { _QUARANTINEQTY = value; }
        }

        public string QUARANTINEREMARK
        {
            get { return _QUARANTINEREMARK; }
            set { _QUARANTINEREMARK = value; }
        }

        public DateTime SENDQCDATE
        {
            get { return _SENDQCDATE; }
            set { _SENDQCDATE = value; }
        }

        public double QCQTY1
        {
            get { return _QCQTY1; }
            set { _QCQTY1 = value; }
        }

        public double QCQTY2
        {
            get { return _QCQTY2; }
            set { _QCQTY2 = value; }
        }

        public double QCQTY3
        {
            get { return _QCQTY3; }
            set { _QCQTY3 = value; }
        }

        public string QCRESULT
        {
            get { return _QCRESULT; }
            set { _QCRESULT = value; }
        }

        public string QCREMARK
        {
            get { return _QCREMARK; }
            set { _QCREMARK = value; }
        }

        public DateTime SENDFGDATE
        {
            get { return _SENDFGDATE; }
            set { _SENDFGDATE = value; }
        }

        public double SENDFGQTY
        {
            get { return _SENDFGQTY; }
            set { _SENDFGQTY = value; }
        }

        public string SENDFGREMARK
        {
            get { return _SENDFGREMARK; }
            set { _SENDFGREMARK = value; }
        }

        public string REFTABLE
        {
            get { return _REFTABLE; }
            set { _REFTABLE = value; }
        }

        public double REFLOID
        {
            get { return _REFLOID; }
            set { _REFLOID = value; }
        }

        public string PRODUCTTYPE
        {
            get { return _PRODUCTTYPE; }
            set { _PRODUCTTYPE = value; }
        }

        public double TOWAREHOUSE
        {
            get { return _TOWAREHOUSE; }
            set { _TOWAREHOUSE = value; }
        }

        public DateTime ANADATE
        {
            get { return _ANADATE; }
            set { _ANADATE = value; }
        }

        public string ANACODE
        {
            get { return _ANACODE; }
            set { _ANACODE = value; }
        }

        public double WAREHOUSE
        {
            get { return _WAREHOUSE; }
            set { _WAREHOUSE = value; }
        }
    }
}
