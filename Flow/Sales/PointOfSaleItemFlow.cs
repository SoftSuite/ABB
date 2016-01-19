using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL;

namespace ABB.Flow.Sales
{
    [System.ComponentModel.DataObject()]
    public class PointOfSaleItemFlow
    {
        private RequisitionDAL _dal;

        public RequisitionDAL DALObj
        {
            get { if (_dal == null) { _dal = new RequisitionDAL(); } return _dal; }
        }

        [System.ComponentModel.DataObjectMethodAttribute(System.ComponentModel.DataObjectMethodType.Select, true)]
        public DataTable GetRequisitionItem(double requisition)
        {
            return ABB.DAL.Sales.PointOfSaleDAL.GetItemList(requisition);
        }
    }
}
