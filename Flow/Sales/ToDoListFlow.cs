using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ABB.DAL.Sales;
using ABB.Data.Sales;

namespace ABB.Flow.Sales
{
    public class ToDoListFlow
    {
        private ToDoListDAL _dal;

        private ToDoListDAL DALObj
        {
            get { if (_dal == null) { _dal = new ToDoListDAL(); } return _dal; }
        }

        public DataTable GetRequisitionList(ToDoListData data)
        {
            return DALObj.GetRequisitionList(data);
        }

    }
}
