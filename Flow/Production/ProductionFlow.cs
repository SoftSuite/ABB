using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using ABB.Data;
using ABB.Data.Admin;
using ABB.Data.Production;
using ABB.DAL;
using ABB.DAL.Production;


namespace ABB.Flow.Production
{
    public class ProductionFlow
    {
        private ProductionSearchDAL search;

        public ProductionSearchDAL SearchDAL
        {
            get { if (search == null) search = new ProductionSearchDAL(); return search; }
        }

        public DataTable GetProductionStockinQuarantineList(ProductStockinQuarantineSearchData data)
        {
            return SearchDAL.GetProductionStockinQuarantineList(data);
        }

        public DataTable GetProductionStockoutQuarantineList(ProductStockoutQuarantineSearchData data)
        {
            return SearchDAL.GetProductionStockoutQuarantineList(data);
        }

        public DataTable GetProductionLostList(ProductionLostSearchData data)
        {
            return SearchDAL.GetProductionLostList(data);
        }

        public DataTable GetProductionQCList(ProductionQCSearchData data)
        {
            return SearchDAL.GetProductionQCList(data);
        }
    }
}
