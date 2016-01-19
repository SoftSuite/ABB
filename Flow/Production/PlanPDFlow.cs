using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
using ABB.DAL;
using ABB.DAL.Production;
using ABB.Data;
using ABB.Data.Production;

namespace ABB.Flow.Production
{
    public class PlanPDFlow
    {
        private PlanPDDAL _pDAL;
        private PlanDAL _DAL;
        private string _error = "";

        private PlanPDDAL PlanObj
        {
            get { if (_pDAL == null) { _pDAL = new PlanPDDAL(); } return _pDAL; }
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
