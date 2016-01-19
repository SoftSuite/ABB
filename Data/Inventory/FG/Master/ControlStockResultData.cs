using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Create by: Pom
/// Create Date: 18 Dec 2007
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
namespace ABB.Data.Inventory.FG.Master
{
    public class ControlStockResultData
    {
        private int _ORDERNO = 0;
        private string _PDMINMAXLOID = "";
        private string _BARCODE = "";
        private string _PRODUCTNAME = "";
        private string _STANDARD = "";
        private string _MINIMUM = "";
        private string _MAXIMUM = "";

        public int ORDERNO
        {
            get { return _ORDERNO; }
            set { _ORDERNO = value; }
        }

        public string PDMINMAXLOID
        {
            get { return _PDMINMAXLOID; }
            set { _PDMINMAXLOID = value; }
        }

        public string BARCODE
        {
            get { return _BARCODE; }
            set { _BARCODE = value; }
        }

        public string PRODUCTNAME
        {
            get { return _PRODUCTNAME; }
            set { _PRODUCTNAME = value; }
        }

        public string STANDARD
        {
            get { return _STANDARD; }
            set { _STANDARD = value; }
        }

        public string MINIMUM
        {
            get { return _MINIMUM; }
            set { _MINIMUM = value; }
        }

        public string MAXIMUM
        {
            get { return _MAXIMUM; }
            set { _MAXIMUM = value; }
        }
    }
}
