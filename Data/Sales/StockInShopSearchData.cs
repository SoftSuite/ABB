using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data.Sales
{
    public class StockInShopSearchData
    {
        private string _STOCKINCODE = "";
        private string _REQUISITIONCODE = "";
        private DateTime _DATEFROM = new DateTime(1, 1, 1);
        private DateTime _DATETO = new DateTime(1, 1, 1);

        public string STOCKINCODE
        {
            get { return _STOCKINCODE; }
            set { _STOCKINCODE = value; }
        }

        public string REQUISITIONCODE
        {
            get { return _REQUISITIONCODE; }
            set { _REQUISITIONCODE = value; }
        }

        public DateTime DATEFROM
        {
            get { return _DATEFROM; }
            set { _DATEFROM = value; }
        }

        public DateTime DATETO
        {
            get { return _DATETO; }
            set { _DATETO = value; }
        }
    }
}
