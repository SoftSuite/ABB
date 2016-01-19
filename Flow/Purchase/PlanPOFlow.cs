using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.DAL;
using ABB.DAL.Purchase;
using ABB.Data;
using ABB.Data.Purchase;

namespace ABB.Flow.Purchase
{
    public class PlanPOFlow
    {
        private PlanPODAL _pDAL;
        private PlanDAL _DAL;
        private string _error = "";

        private PlanPODAL PlanObj
        {
            get { if (_pDAL == null) { _pDAL = new PlanPODAL(); } return _pDAL; }
        }

        private PlanDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new PlanDAL(); } return _DAL; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public DataTable GetPlanList()
        {
            DataTable dt = PlanObj.GetPlanList();
            double rowIndex = 1;
            foreach (DataRow dRow in dt.Rows)
            {
                dRow["ORDERNO"] = rowIndex;
                rowIndex += 1;
            }
            return dt;
        }

        public DataTable GetPlanDetailList(PlanDetailSearchData data)
        {
            return PlanObj.GetPlanDetailList(data);
        }

    }
}
