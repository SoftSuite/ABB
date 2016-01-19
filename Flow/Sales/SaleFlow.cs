using System;
using System.Collections.Generic;
using System.Text;
using ABB.DAL;
using ABB.DAL.Sales;
using ABB.Data;

namespace ABB.Flow.Sales
{
    public class SaleFlow
    {
        private SaleDAL _DAL;
        private SaleCustomerDAL _cDAL;
        private double _DISCOUNT = 0;
        private string _error = "";

        private SaleDAL DALObj
        {
            get { if (_DAL == null) { _DAL = new SaleDAL(); } return _DAL; }
        }

        private SaleCustomerDAL CustDALObj
        {
            get { if (_cDAL == null) { _cDAL = new SaleCustomerDAL(); } return _cDAL; }
        }

        public double DISCOUNT
        {
            get { return _DISCOUNT; }
        }

        public string ErrorMessage
        {
            get { return _error; }
        }

        public System.Data.DataTable GetProductPromotionList(string productList, double warehouse)
        {
            return DALObj.GetDataList(productList, warehouse);
        }

        /// <summary>
        ///  ค้นหาข้อมูลสินค้า และส่วนลดสินค้า ตามโปรโมชั่นสำหรับคลังสินค้าที่ระบุ
        /// </summary>
        /// <param name="product"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public ProductSaleData GetProductPromotion(double product, double warehouse)
        {
            return GetProductPromotion(DALObj.GetProductPromotion(warehouse, product, null));
        }

        /// <summary>
        /// ค้นหาข้อมูลสินค้า และส่วนลดสินค้า ตามโปรโมชั่นสำหรับคลังสินค้าที่ระบุ
        /// </summary>
        /// <param name="barcode"></param>
        /// <param name="warehouse"></param>
        /// <returns></returns>
        public ProductSaleData GetProductPromotion(string barcode, double warehouse)
        {
            return GetProductPromotion(DALObj.GetProductPromotion(warehouse, barcode, null));
        }

        private ProductSaleData GetProductPromotion(bool isValid)
        {
            ProductSaleData data = new ProductSaleData();
            if (isValid)
            {
                data.BARCODE = DALObj.BARCODE;
                data.DISCOUNT = DALObj.DISCOUNT;
                data.ISVAT = DALObj.ISVAT;
                data.PRODUCT = DALObj.PRODUCT;
                data.PRODUCTNAME = DALObj.PRODUCTNAME;
                data.UNIT = DALObj.UNIT;
                data.UNITNAME = DALObj.UNITNAME;
                data.UNITPRICE = DALObj.UNITPRICE;
                data.STOCKQTY = DALObj.STOCKQTY;
                data.ISDISCOUNT = DALObj.ISDISCOUNT;
                data.ISEDIT = DALObj.ISEDIT;
            }
            return data;
        }

        /// <summary>
        /// คำนวณราคาต่อหน่วยของสินค้า ถ้าไม่รวม vat จะส่งค่าราคาต่อหน่วยที่รวม vat
        /// </summary>
        /// <param name="unitPrice"></param>
        /// <param name="vatPercent"></param>
        /// <param name="isVat"></param>
        /// <returns></returns>
        public double CalculateUnitPrice(double unitPrice, double vatPercent, string isVat)
        {
            if (isVat != Constz.VAT.Included.Code)
            {
                unitPrice = Math.Round(unitPrice + ((unitPrice * vatPercent) / 100), 2);
            }
            return unitPrice;
        }

        /// <summary>
        /// คำนวณราคารวมสุทธิของสินค้า
        /// </summary>
        /// <param name="unitPrice"></param>
        /// <param name="quantity"></param>
        /// <param name="discount"></param>
        /// <returns></returns>
        public double CalcucateProductTotalItem(double unitPrice, double quantity, double discount)
        {
            return Math.Round((unitPrice - discount) * quantity,2);
        }

        /// <summary>
        /// คำนวณส่วนลดของสินค้า
        /// </summary>
        /// <param name="unitPrice"></param>
        /// <param name="discountPercent"></param>
        /// <returns></returns>
        public double CalcucateDiscount(double unitPrice, double discountPercent)
        {
            return Math.Round(unitPrice * discountPercent / 100, 2);
        }

        /// <summary>
        /// คำนวณ vat ทั้งหมด (จากราคาวินค้าที่รวมภาษีแล้ว)
        /// </summary>
        /// <param name="grandTotal"></param>
        /// <param name="vatPercent"></param>
        /// <returns></returns>
        public double CalculateTotalVat(double grandTotal, double vatPercent)
        {
            return Math.Round((grandTotal * vatPercent) / (100 + vatPercent), 2);
        }

        public CustomerSaleData GetCustomerData(double customer)
        {
            CustomerSaleData data = new CustomerSaleData();
            if (CustDALObj.doGetdata(customer, null))
            {
                data.CODE = CustDALObj.CODE;
                data.CUSTOMER = CustDALObj.CUSTOMER;
                data.CUSTOMERNAME = CustDALObj.CUSTOMERNAME;
                data.EFDATE = CustDALObj.EFDATE;
                data.EPDATE = CustDALObj.EPDATE;
                data.MEMBERTYPE = CustDALObj.MEMBERTYPE;
                data.CADDRESS = CustDALObj.CADDRESS;
                data.CFAX = CustDALObj.CFAX;
                data.CLASTNAME = CustDALObj.CLASTNAME;
                data.CNAME = CustDALObj.CNAME;
                data.CTEL = CustDALObj.CTEL;
                data.CTITLE = CustDALObj.CTITLE;
                data.PAYMENT = CustDALObj.PAYMENT;
                data.CREDITDAY = CustDALObj.CREDITDAY;
            }
            return data;
        }
        
        public CustomerSaleData GetCustomerData(string customerCode)
        {
            CustomerSaleData data = new CustomerSaleData();
            if (CustDALObj.doGetdata(customerCode, null))
            {
                data.CODE = CustDALObj.CODE;
                data.CUSTOMER = CustDALObj.CUSTOMER;
                data.CUSTOMERNAME = CustDALObj.CUSTOMERNAME;
                data.EFDATE = CustDALObj.EFDATE;
                data.EPDATE = CustDALObj.EPDATE;
                data.MEMBERTYPE = CustDALObj.MEMBERTYPE;
                data.CADDRESS = CustDALObj.CADDRESS;
                data.CFAX = CustDALObj.CFAX;
                data.CLASTNAME = CustDALObj.CLASTNAME;
                data.CNAME = CustDALObj.CNAME;
                data.CTEL = CustDALObj.CTEL;
                data.CTITLE = CustDALObj.CTITLE;
                data.PAYMENT = CustDALObj.PAYMENT;
                data.CREDITDAY = CustDALObj.CREDITDAY;
            }
            return data;
        }

        public bool GetCustomerDiscount(double customer, double totalPrice)
        {
            bool ret = true;
            _DISCOUNT = 0;
            
            OracleDBObj obj = new OracleDBObj();
            obj.CreateConnection();
            obj.CreateTransaction();
            try
            {
                CustomerDAL cDAL = new CustomerDAL();
                cDAL.GetDataByLOID(customer, obj.zTrans);
                if (cDAL.OnDB)
                {
                    if (cDAL.EFDATE.Date <= DateTime.Today && DateTime.Today <= cDAL.EPDATE.Date)
                        _DISCOUNT = DALObj.GetMemberDiscount(cDAL.MEMBERTYPE, totalPrice, obj.zTrans);
                }

                obj.zTrans.Commit();
                obj.CloseConnection();
            }
            catch (Exception ex)
            {
                obj.zTrans.Rollback();
                obj.CloseConnection();
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        public bool GetPromotionDiscount(double warehouse, double totalPrice)
        {
            bool ret = true;
            _DISCOUNT = 0;
            try
            {
                _DISCOUNT = DALObj.GetPromotionDiscount(warehouse, totalPrice, null);
            }
            catch (Exception ex)
            {
                ret = false;
                _error = ex.Message;
            }
            return ret;
        }

        //public bool GetDiscount(double warehouse, double customer, double totalPrice, string useMemberDiscount)
        //{
        //    bool ret = true;
        //    _DISCOUNT = 0;

        //    OracleDBObj obj = new OracleDBObj();
        //    obj.CreateConnection();
        //    obj.CreateTransaction();
        //    try
        //    {
        //        CustomerDAL cDAL = new CustomerDAL();
        //        cDAL.GetDataByLOID(customer, obj.zTrans);
        //        if (cDAL.OnDB)
        //        {
        //            if (cDAL.EFDATE.Date <= DateTime.Today && DateTime.Today <= cDAL.EPDATE.Date)
        //                _DISCOUNT = DALObj.GetDiscount(warehouse, cDAL.MEMBERTYPE, totalPrice, useMemberDiscount, obj.zTrans);
        //        }

        //        obj.zTrans.Commit();
        //        obj.CloseConnection();
        //    }
        //    catch (Exception ex)
        //    {
        //        obj.zTrans.Rollback();
        //        obj.CloseConnection();
        //        ret = false;
        //        _error = ex.Message;
        //    }
        //    return ret;
        //}

    }
}
