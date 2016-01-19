using System;
using System.Collections.Generic;
using System.Text;

namespace ABB.Data
{
    public class Constz
    {
        public static string IntFormat { get { return "#,##0"; } }
        public static string ABBSERV { get { return "@ABBSERV"; } }
        public static string DateFormat { get { return "dd/MM/yyyy"; } }
        public static string DblFormat { get { return "#,##0.00"; } }
        public static string Dbl5Format { get { return "#,##0.00000"; } }

        public static string HomeFolder { get { return System.Web.HttpContext.Current.Request.ApplicationPath + "/"; } }

        public static string ImageFolder { get { return HomeFolder + "Images/"; } }

        /// <summary>
        /// ชื่อ config ที่กำหนดใน web.config
        /// </summary>
        public partial class WebConfigKey
        {
            public const string WEB_ADMIN = "WEB_ADMIN";
            public const string WEB_EIS = "WEB_EIS";
            public const string WEB_INVENTORY = "WEB_INVENTORY";
            public const string WEB_PRODUCTION = "WEB_PRODUCTION";
            public const string WEB_PURCHASE = "WEB_PURCHASE";
            public const string WEB_REPORTS = "WEB_REPORTS";
            public const string WEB_SALES = "WEB_SALES";
        }

        /// <summary>
        /// ฝ่ายการผลิต
        /// </summary>
        public partial class ProductionDepartment
        {
            public const double LOID = -1;
            public const string Code = "";
            public const string Name = "ฝ่ายการผลิต";
        }

        public partial class PurchaseDepartment
        {
            public const double LOID = 3;
            public const string Code = "DIV003";
            public const string Name = "ฝ่ายจัดซื้อ";
        }

        public partial class AdminDepartment
        {
            public const double LOID = 1;
            public const string Code = "DIV001";
            public const string Name = "ศูนย์ข้อมูลสารสนเทศ";
        }

        /// <summary>
        /// คลังผลิตภัณฑ์สำเร็จรูป
        /// </summary>
        public partial class ReadyMadeDepartment
        {
            public const double LOID = 1;
            public const string Code = "";
            public const string Name = "คลังผลิตภัณฑ์สำเร็จรูป";
        }

        public partial class DocType
        {
            /// <summary>
            /// ใบแสดงยกยอดรับ
            /// </summary>
            public partial class RecShop
            {
                public const double LOID = 1;
                public const string NAME = "ใบแสดงยกยอดรับ";
            }
            /// <summary>
            /// ใบรับคืน
            /// </summary>
            public partial class RetShop
            {
                public const double LOID = 2;
                public const string NAME = "ใบรับคืน";
            }
            /// <summary>
            /// ใบแจ้งคืนสินค้าตัวอย่าง
            /// </summary>
            public partial class RetSample
            {
                public const double LOID = 3;
                public const string NAME = "ใบแจ้งคืนสินค้าตัวอย่าง";
            }
            /// <summary>
            /// ใบเบิกสินค้าฝากขาย
            /// </summary>
            public partial class ReqDistribute
            {
                public const double LOID = 4;
                public const string NAME = "ใบเบิกสินค้าฝากขาย";
            }
            /// <summary>
            /// ใบเบิกขายนอกสถานที่
            /// </summary>
            public partial class ReqFair
            {
                public const double LOID = 5;
                public const string NAME = "ใบเบิกขายนอกสถานที่";
            }
            /// <summary>
            /// ใบเบิกหน่วยงานสนับสนุน
            /// </summary>
            public partial class ReqOrgSupport
            {
                public const double LOID = 6;
                public const string NAME = "ใบเบิกหน่วยงานสนับสนุน";
            }
            /// <summary>
            /// ใบเบิกสนับสนุนสินค้า
            /// </summary>
            public partial class ReqSupport
            {
                public const double LOID = 7;
                public const string NAME = "ใบเบิกสนับสนุนสินค้า";
            }
            /// <summary>
            /// ใบเบิกรับคำสั่งซื้อ
            /// </summary>
            public partial class Reserve
            {
                public const double LOID = 8;
                public const string NAME = "ใบเบิกรับคำสั่งซื้อ";
            }
            /// <summary>
            /// ใบแจ้งส่งคืนสินค้า
            /// </summary>
            public partial class RetProduct
            {
                public const double LOID = 9;
                public const string NAME = "ใบแจ้งส่งคืนสินค้า";
            }
            /// <summary>
            /// ใบแจ้งส่งคืนวัตถุดิบ
            /// </summary>
            public partial class RetRaw
            {
                public const double LOID = 10;
                public const string NAME = "ใบแจ้งส่งคืนวัตถุดิบ";
            }
            /// <summary>
            /// ใบเบิกวัตถุดิบเพื่อผลิต
            /// </summary>
            public partial class ReqRawPD
            {
                public const double LOID = 11;
                public const string NAME = "ใบเบิกวัตถุดิบเพื่อผลิต";
            }
            /// <summary>
            /// ใบเบิกวัตถุดิบเพื่อจ้างผลิต
            /// </summary>
            public partial class ReqRawPO
            {
                public const double LOID = 12;
                public const string NAME = "ใบเบิกวัตถุดิบเพื่อจ้างผลิต";
            }
            /// <summary>
            /// ใบนำส่งสินค้าเข้าคลัง
            /// </summary>
            public partial class DelProduct
            {
                public const double LOID = 13;
                public const string NAME = "ใบนำส่งผลิตภัณฑ์เข้าคลัง";
            }
            /// <summary>
            /// ใบตรวจรับสินค้า
            /// </summary>
            public partial class RecProduct
            {
                public const double LOID = 14;
                public const string NAME = "ใบตรวจรับสินค้า";
            }
            /// <summary>
            /// ใบนำส่งวัตถุดิบเข้าคลัง
            /// </summary>
            public partial class DelRaw
            {
                public const double LOID = 15;
                public const string NAME = "ใบนำส่งวัตถุดิบเข้าคลัง";
            }
            /// <summary>
            /// ใบตรวจรับวัตถุดิบ
            /// </summary>
            public partial class RecRaw
            {
                public const double LOID = 16;
                public const string NAME = "ใบตรวจรับวัตถุดิบ";
            }
            /// <summary>
            /// ใบเบิกออกจากคลัง
            /// </summary>
            public partial class ReqProduct
            {
                public const double LOID = 17;
                public const string NAME = "ใบเบิกออกจากคลัง";
            }
            /// <summary>
            /// ใบคืนใบยืมสินค้าออก
            /// </summary>
            public partial class RetFair
            {
                public const double LOID = 18;
                public const string NAME = "ใบคืนใบยืมสินค้าออก";
            }
            /// <summary>
            /// ใบรับคืนสินค้าฝากขาย
            /// </summary>
            public partial class RetDistribute
            {
                public const double LOID = 19;
                public const string NAME = "ใบรับคืนสินค้าฝากขาย";
            }
            /// <summary>
            /// ใบรับคืนสินค้าตัวอย่าง
            /// </summary>
            public partial class RetInSample
            {
                public const double LOID = 20;
                public const string NAME = "ใบรับคืนสินค้าตัวอย่าง";
            }
            /// <summary>
            /// ใบรับคืนสินค้าลดหนี้
            /// </summary>
            public partial class RetInReduce
            {
                public const double LOID = 21;
                public const string NAME = "ใบรับคืนสินค้าลดหนี้";
            }
            /// <summary>
            /// ใบรับคืนวัตถุดิบจากฝ่ายผลิต
            /// </summary>
            public partial class RetMaterial
            {
                public const double LOID = 22;
                public const string NAME = "ใบรับคืนวัตถุดิบจากฝ่ายผลิต";
            }
            /// <summary>
            /// ใบเบิกสนับสนุนวัตถุดิบ
            /// </summary>
            public partial class RetSMaterial
            {
                public const double LOID = 24;
                public const string NAME = "ใบเบิกสนับสนุนวัตถุดิบ";
            }
            /// <summary>
            /// ใบเบิกวัสดุ
            /// </summary>
            public partial class RetSOther
            {
                public const double LOID = 25;
                public const string NAME = "ใบเบิกวัสดุ";
            }
        }

        public partial class ActiveStatus
        {
            public const string Active = "1";
            public const string InActive = "0";
        }

        public partial class CustomerType
        {
            /// <summary>
            /// บุคคลทั่วไป
            /// </summary>
            public partial class Personal
            {
                public const string Code = "P";
                public const string Name = "บุคคลทั่วไป";
            }
            /// <summary>
            /// บริษัทเอกชน
            /// </summary>
            public partial class Company
            {
                public const string Code = "C";
                public const string Name = "บริษัทเอกชน";
            }
            /// <summary>
            /// องค์กร/หน่วยงานของรัฐ
            /// </summary>
            public partial class Government
            {
                public const string Code = "G";
                public const string Name = "องค์กร/หน่วยงานของรัฐ";
            }
        }

        public partial class Warehouse
        {
            public partial class Type
            {
                public partial class FG
                {
                    public const string Code = "FG";
                    public const string Name = "สินค้าสำเร็จรูป";
                }
                public partial class WH
                {
                    public const string Code = "WH";
                    public const string Name = "วัตถุดิบ";
                }
            }
        }

        public partial class IsFGStockOut
        {
            public const string No = "N";
            public const string Yes = "Y";
        }

        public partial class ProductType
        {
            public partial class Type
            {
                public partial class FG
                {
                    public const string Code = "FG";
                    public const string Name = "สินค้าสำเร็จรูป";
                    public const string Rank = "1";
                }
                public partial class WH
                {
                    public const string Code = "WH";
                    public const string Name = "วัตถุดิบ";
                    public const string Rank = "2";
                }
                public partial class Others
                {
                    public const string Code = "OT";
                    public const string Name = "อื่นๆ";
                    public const string Rank = "3";
                }
            }
        }

        public partial class DeliveryType
        {
            public partial class Undefined
            {
                public const string Code = "ZZ";
                public const string Name = "ไม่ระบุ";
            }
            public partial class Customer
            {
                public const string Code = "CU";
                public const string Name = "รับเอง";
            }
            public partial class Car
            {
                public const string Code = "CA";
                public const string Name = "ส่งโดยรถบริษัท";
            }
            public partial class TransferCompany
            {
                public const string Code = "TR";
                public const string Name = "ส่งโดยบริษัทรับจ้างขนส่ง";
            }
            public partial class Mail
            {
                public const string Code = "MA";
                public const string Name = "ส่งทางไปรษณีย์";
            }
            public partial class Other
            {
                public const string Code = "OT";
                public const string Name = "อื่นๆ";
            }
        }

        public partial class Payment
        {
            public partial class Cash
            {
                public const string Code = "CA";
                public const string Name = "เงินสด";
            }
            public partial class CreditCard
            {
                public const string Code = "CC";
                public const string Name = "บัตรเครดิต";
            }
            public partial class Credit
            {
                public const string Code = "CR";
                public const string Name = "สินเชื่อ";
            }
            public partial class Cheque
            {
                public const string Code = "CH";
                public const string Name = "เช็ค";
            }
            public partial class Others
            {
                public const string Code = "OT";
                public const string Name = "อื่นๆ";
            }
        }

        public partial class Basket
        {
            public partial class Status
            {
                public partial class Waiting
                {
                    public const string Code = "WA";
                    public const string Name = "กรอกข้อมูล";
                    public const string Rank = "1";
                }
                public partial class Approved
                {
                    public const string Code = "AP";
                    public const string Name = "ส่งคลังกระเช้า";
                    public const string Rank = "7";
                }
                public partial class Finish
                {
                    public const string Code = "FN";
                    public const string Name = "เสร็จสิ้น";
                    public const string Rank = "8";
                }
            }

            public partial class Type
            {
                public partial class New
                {
                    public const string Code = "NEW";
                    public const string Name = "จัดกระเช้าใหม่";
                }
                public partial class Return
                {
                    public const string Code = "RET";
                    public const string Name = "คืนสินค้าในกระเช้า";
                }
            }
        }

        public partial class StockinReturn
        {
            public partial class Status
            {
                public partial class Waiting
                {
                    public const string Code = "WA";
                    public const string Name = "กรอกข้อมูล";
                    public const string Rank = "1";
                }
                public partial class QC
                {
                    public const string Code = "QC";
                    public const string Name = "ส่งตรวจ";
                    public const string Rank = "6";
                }
                public partial class Approved
                {
                    public const string Code = "AP";
                    public const string Name = "เสร็จสิ้น";
                    public const string Rank = "7";
                }
                public partial class Void
                {
                    public const string Code = "VO";
                    public const string Name = "ยกเลิก";
                    public const string Rank = "9";
                }
            }
        }

        public partial class Requisition
        {
            public partial class Status
            {
                /// <summary>
                /// กำลังดำเนินการ
                /// </summary>
                public partial class Waiting
                {
                    public const string Code = "WA";
                    public const string Name = "กำลังดำเนินการ";
                    public const string Rank = "1";
                }
                /// <summary>
                /// สั่งจอง
                /// </summary>
                public partial class Reserve
                {
                    public const string Code = "RS";
                    public const string Name = "สั่งจอง";
                    public const string Rank = "2";
                }
                /// <summary>
                /// รอผลิต
                /// </summary>
                public partial class RW
                {
                    public const string Code = "RW";
                    public const string Name = "รอผลิต";
                    public const string Rank = "3";
                }
                /// <summary>
                /// ฉายรังสี
                /// </summary>
                public partial class XRay
                {
                    public const string Code = "RD";
                    public const string Name = "ฉายรังสี";
                    public const string Rank = "4";
                }
                /// <summary>
                /// กักกันสินค้า
                /// </summary>
                public partial class QS
                {
                    public const string Code = "QS";
                    public const string Name = "กักกันสินค้า";
                    public const string Rank = "5";
                }
                /// <summary>
                /// รอรับเข้า
                /// </summary>
                public partial class QC
                {
                    public const string Code = "QC";
                    public const string Name = "รอรับเข้า";
                    public const string Rank = "6";
                }
                public partial class SP
                {
                    public const string Code = "SP";
                    public const string Name = "ส่งให้จัดซื้อ";
                    public const string Rank = "7";
                }
                /// <summary>
                /// อนุมัติ
                /// </summary>
                public partial class Approved
                {
                    public const string Code = "AP";
                    public const string Name = "อนุมัติ";
                    public const string Rank = "8";
                }
                /// <summary>
                /// เสร็จสิ้น
                /// </summary>
                public partial class Finish
                {
                    public const string Code = "FN";
                    public const string Name = "เสร็จสิ้น";
                    public const string Rank = "9";
                }
                /// <summary>
                /// ยกเลิก
                /// </summary>
                public partial class Void
                {
                    public const string Code = "VO";
                    public const string Name = "ยกเลิก";
                    public const string Rank = "10";
                }

                /// <summary>
                /// ส่งตรวจ QC
                /// </summary>
                public partial class SendQC
                {
                    public const string Code = "QC";
                    public const string Name = "ส่งตรวจ QC";
                    public const string Rank = "11";
                }
                /// <summary>
                /// แจ้งผลการตรวจ 
                /// </summary>
                public partial class ReturnQC
                {
                    public const string Code = "AP";
                    public const string Name = "แจ้งผลการตรวจ";
                    public const string Rank = "12";
                }
                /// <summary>
                /// ทำรายการ 
                /// </summary>
                public partial class DoWaiting
                {
                    public const string Code = "WA";
                    public const string Name = "ทำรายการ";
                    public const string Rank = "13";
                }
                /// <summary>
                /// ส่งคลังวัตถุดิบ
                /// </summary>
                public partial class SendWareHouse
                {
                    public const string Code = "AP";
                    public const string Name = "ส่งคลังวัตถุดิบ";
                    public const string Rank = "14";
                }
                /// <summary>
                /// ส่งคลังสำเร็จรูป
                /// </summary>
                public partial class ApproveWH
                {
                    public const string Code = "AP";
                    public const string Name = "ส่งคลังสำเร็จรูป";
                    public const string Rank = "15";
                }

                /// <summary>
                /// ทั้งหมด
                /// </summary>
                public partial class AllStatus
                {
                    public const string Code = "ALL";
                    public const string Name = "ทั้งหมด";
                    public const string Rank = "999999";
                }
            }

            public partial class RequisitionType
            {
                /// <summary>
                /// ใบสั่งซื้อ/สั่งจอง
                /// </summary>
                public const double REQ01 = 1;
                /// <summary>
                /// ใบขอเบิกสินค้าฝากขาย
                /// </summary>
                public const double REQ02 = 2;
                /// <summary>
                /// ใบขอเบิกขายนอกสถานที่
                /// </summary>
                public const double REQ03 = 3;
                /// <summary>
                /// ใบขอสนับสนุนสินค้า
                /// </summary>
                public const double REQ04 = 4;
                /// <summary>
                /// ใบลดหนี้
                /// </summary>
                public const double REQ05 = 5;
                /// <summary>
                /// ใบขอเบิกภายใน
                /// </summary>
                public const double REQ06 = 6;
                /// <summary>
                /// บันทึกสั่งผลิต
                /// </summary>
                public const double REQ07 = 7;
                /// <summary>
                /// ใบขอเบิกวัตถุดิบและบรรจุภัณฑ์เพื่อการผลิต
                /// </summary>
                public const double REQ08 = 8;
                /// <summary>
                /// ใบขอเบิกวัตถุดิบเพื่อจ้างผลิต
                /// </summary>
                public const double REQ09 = 9;
                /// <summary>
                /// ใบขอเบิกหน่วยงานสนับสนุน
                /// </summary>
                public const double REQ10 = 10;
                /// <summary>
                /// ใบเสร็จรับเงิน
                /// </summary>
                public const double REQ11 = 11;
                /// <summary>
                /// ใบรับคืนสินค้าฝากขาย
                /// </summary>
                public const double REQ12 = 12;
                /// <summary>
                /// POS
                /// </summary>
                public const double REQ13 = 13;
                /// <summary>
                /// ใบขอคืนวัตถุดิบและบรรจุภัณฑ์
                /// </summary>
                public const double REQ14 = 14;
                /// <summary>
                /// ใบขอเบิกวัตถุดิบเพื่อผลิต
                /// </summary>
                public const double REQ15 = 15;
                /// <summary>
                /// สนับสนุนวัตถุดิบ
                /// </summary>
                public const double REQ24 = 24;
            }

        }

        public partial class QCResult
        {
            public partial class Pass
            {
                public const string Code = "Y";
                public const string Name = "ผ่าน";
            }
            public partial class Fail
            {
                public const string Code = "N";
                public const string Name = "ไม่ผ่าน";
            }
        }

        public partial class OrderType
        {
            public partial class PO
            {
                public const string Code = "PO";
                public const string Name = "สั่งซื้อ";
            }
            public partial class PD
            {
                public const string Code = "PD";
                public const string Name = "สั่งผลิต";
            }
            public partial class AR
            {
                public const string Code = "AR";
                public const string Name = "ทั้งสอง";
            }
        }

        public partial class VAT
        {
            public partial class Included
            {
                public const string Code = "1";
                public const string Name = "รวมภาษี";
            }
            public partial class NotIncluded
            {
                public const string Code = "0";
                public const string Name = "ไม่รวมภาษี";
            }
        }

        public partial class Discount
        {
            public partial class Calculated
            {
                public const string Code = "1";
                public const string Name = "คิดส่วนลด";
            }
            public partial class NotCalculated
            {
                public const string Code = "0";
                public const string Name = "ไม่คิดส่วนลด";
            }
        }

        public partial class Edit
        {
            public partial class Editable
            {
                public const string Code = "Y";
                public const string Name = "แก้ไขราคาได้";
            }
            public partial class DisEditable
            {
                public const string Code = "N";
                public const string Name = "แก้ไขราคาไม่ได้";
            }
        }

        public partial class Refund
        {
            public partial class Yes
            {
                public const string Code = "Y";
                public const string Name = "คืนเงินให้สมาชิก";
            }
            public partial class No
            {
                public const string Code = "N";
                public const string Name = "ไม่คืนเงินให้สมาชิก";
            }
        }

        public partial class ConfigName
        {
            public const string VAT = "VAT";
            public const string PERIOD = "PERIOD";
            public const string ORGNAME = "ORGNAME";
            public const string ORGTEL = "ORGTEL";
            public const string SUPDISCOUNT = "SUPDISCOUNT";
        }

        public partial class ToDoListTab
        {
            public partial class Inventory
            {
                public partial class FG
                {
                    public partial class MinimumStock
                    {
                        public const string Index = "1";
                        public const string Name = "สินค้าที่ต่ำกว่า Minimum Stock";
                    }
                    public partial class StockIn
                    {
                        public const string Index = "2";
                        public const string Name = "สินค้าที่รอรับเข้า";
                    }
                    public partial class StockOut
                    {
                        public const string Index = "3";
                        public const string Name = "สินค้าที่รอจ่ายออก";
                    }
                    public partial class Expire
                    {
                        public const string Index = "4";
                        public const string Name = "สินค้าที่ใกล้หมดอายุ";
                    }
                }
                public partial class WH
                {
                    public partial class MinimumStock
                    {
                        public const string Index = "1";
                        public const string Name = "วัตถุดิบที่ต่ำกว่า Minimum Stock";
                    }
                    public partial class StockIn
                    {
                        public const string Index = "2";
                        public const string Name = "วัตถุดิบที่รอรับเข้า";
                    }
                    public partial class StockOut
                    {
                        public const string Index = "3";
                        public const string Name = "วัตถุดิบที่รอจ่ายออก";
                    }
                    public partial class Expire
                    {
                        public const string Index = "4";
                        public const string Name = "วัตถุดิบที่ใกล้หมดอายุ";
                    }
                }
            }
            public partial class Purchase
            {
                public partial class ProductReceiveList
                {
                    public const string Index = "1";
                    public const string Name = "รายการสินค้าที่รอทำใบสั่งซื้อ";
                }
                public partial class ProductPurchaseList
                {
                    public const string Index = "2";
                    public const string Name = "รายการสินค้าที่รอรับสินค้า";
                }
            }
            public partial class Production
            {
                public partial class ProductionWaitList
                {
                    public const string Index = "1";
                    public const string Name = "รายการสินค้าที่รอผลิต";
                }
                public partial class ProductionDuringList
                {
                    public const string Index = "2";
                    public const string Name = "รายการสินค้าที่อยู่ระหว่างการผลิต";
                }
            }
        }

        public partial class ProductionTab
        {
            public partial class RawMaterialUsing
            {
                public const string Index = "1";
                public const string Row = "2";
                public const string Name = "การใช้วัตถุดิบ";
            }
            public partial class Pack
            {
                public const string Index = "2";
                public const string Row = "2";
                public const string Name = "การบรรจุ";
            }
            public partial class X_RaySending
            {
                public const string Index = "3";
                public const string Row = "2";
                public const string Name = "ส่งไปฉายรังสี";
            }
            public partial class X_RayReceiving
            {
                public const string Index = "4";
                public const string Row = "2";
                public const string Name = "รับคืนจากฉายรังสี";
            }
            public partial class Import
            {
                public const string Index = "5";
                public const string Row = "2";
                public const string Name = "รับเข้าคลังกักกัน";
            }
            public partial class QC
            {
                public const string Index = "6";
                public const string Row = "1";
                public const string Name = "ส่งตรวจวิเคราะห์คุณภาพ";
            }
            public partial class RawMaterialLoss
            {
                public const string Index = "7";
                public const string Row = "1";
                public const string Name = "ความสูญเสียวัตถุดิบ";
            }
            public partial class PackLoss
            {
                public const string Index = "8";
                public const string Row = "1";
                public const string Name = "ความสูญเสียบรรจุภัณฑ์";
            }
            public partial class Export
            {
                public const string Index = "9";
                public const string Row = "1";
                public const string Name = "จ่ายออกจากคลังกักกัน";
            }
        }

        public partial class PlanType
        {
            public const string SA = "SA";
            public const string FG = "FG";
            public const string WH = "WH";
            public const string PD = "PD";
            public const string PO = "PO";
            public const string MK = "MK";
        }

        public partial class Report
        {
            public const string ControlTransport = "ControlTransport";
            public const string Invoice = "Invoice";
            public const string InvoiceFull = "InvoiceFull";
            public const string Productionherb = "Productionherb";
            public const string ProductionLost = "ProductionLost";
            public const string ProductionStockinDetain = "ProductionStockinDetain";
            public const string ProductMaterialReserve = "ProductMaterialReserve";
            public const string ProductOrder = "ProductOrder";
            public const string ProductOrderWH = "ProductOrderWH";
            //public const string ProductPO = "ProductPO";
            public const string ProductQC = "ProductQC";
            public const string ProductReceive = "ProductReceive";
            public const string ProductRequestInShop = "ProductRequestInShop";
            public const string ProductReserve = "ProductReserve";
            public const string ProductReserveSale = "ProductReserveSale";
            public const string ProductReturn = "ProductReturn";
            public const string ProductStockInShop = "ProductStockInShop";
            public const string ProductStockoutDetain = "ProductStockoutDetain";
            public const string ProductStockoutDetain1 = "ProductStockoutDetain1";
            public const string Promotion = "Promotion";
            public const string Purchase = "Purchase";
            public const string PurchaseOrder = "PurchaseOrder";
            public const string QCWH = "QCWH";
            public const string ReturnRequest = "ReturnRequest";
            public const string ReturnRequestPD = "ReturnRequestPD";
            public const string ReturnTester = "ReturnTester";
            public const string StockInProduction = "StockInProduction";
            public const string StockinProductWH = "StockinProductWH";
            public const string StockInReturn = "StockInReturn";
            public const string StockinReturnMaterial = "StockinReturnMaterial";
            public const string StockinReturnPDExam = "StockinReturnPDExam";
            public const string StockinReturnPDRequest = "StockinReturnPDRequest";
            public const string StockInReturnProduct = "StockInReturnProduct";
            public const string StockInSupplier = "StockInSupplier";
            public const string StockinSupplierWH = "StockinSupplierWH";
            public const string StockOut = "StockOut";
            public const string StockoutBasket = "StockoutBasket";
            public const string StockOutBorrow = "StockOutBorrow";
            public const string StockOutSupport = "StockOutSupport";
            public const string StockoutMaterialWH = "StockoutMaterialWH";
            public const string StockoutExportMaterial = "StockoutExportMaterial";
            public const string SendMoneyReport = "SendMoneyReport";
            public const string StockRemainReport = "StockRemainReport";
            public const string StockoutDoctypeReport = "StockoutDoctypeReport";
            public const string SaleSummaryBillReport = "SaleSummaryBillReport";
            public const string Support = "Support";
            public const string ProductSaleSummaryReport = "ProductSaleSummaryReport";
            public const string ProductReturnSummaryReport = "ProductReturnSummaryReport";
            public const string ControlTransportDetail = "ControlTransportDetail";
            public const string ProductionStockinQuarantine = "ProductionStockinQuarantine";
            public const string FGStockInReturnProduct = "FGStockInReturnProduct";
            public const string StockMovementRepot = "StockMovementRepot";
            public const string ProductReturnSearch = "ProductReturnSearch";
            public const string F_FG_16_R00 = "F-FG-16_R00";
            public const string repBarcodeProduct = "repBarcodeProduct";
            public const string repBarcodeMaterial = "repBarcodeMaterial";
            public const string MaterialLost = "MaterialLost";
            public const string StockinSupplierInitial = "StockinSupplierInitial";
            public const string StockoutOther = "StockoutOther";
            public const string Productionherb01 = "Productionherb01";
        }

        public partial class QueryString
        {
            public const string ReportName = "reportname";
            public const string ReportKey = "reportkey";
            public const string Loid = "loid";
        }

        public partial class BasketType
        {
            public const string New = "NEW";
            public const string Return = "RET";
        }

        public partial class Zone
        {
            /// <summary>
            /// สินค้าปกติ
            /// </summary>
            public const double Z01 = 1;
            /// <summary>
            /// สินค้าสนับสนุนหน่วยงาน
            /// </summary>
            public const double Z02 = 2;
            /// <summary>
            /// สินค้า Reject
            /// </summary>
            public const double Z03 = 3;
            /// <summary>
            /// วัตถุดิบ
            /// </summary>
            public const double Z04 = 4;
            /// <summary>
            /// วัตถุดิบรอ QC
            /// </summary>
            public const double Z05 = 5;
            /// <summary>
            /// วัตถุดิบ Reject
            /// </summary>
            public const double Z06 = 6;
            /// <summary>
            /// สินค้าสนับสนุน
            /// </summary>
            public const double Z07 = 7;
            /// <summary>
            /// สินค้าลดหนี้
            /// </summary>
            public const double Z08 = 8;
            /// <summary>
            /// สินค้ารอรับเข้า
            /// </summary>
            public const double Z10 = 10;
            /// <summary>
            /// สินค้าหน้าร้าน
            /// </summary>
            public const double Z11 = 11;
            /// <summary>
            /// สินค้ารอส่งคืนผู้จำหน่าย
            /// </summary>
            public const double Z12 = 12;
            /// <summary>
            /// สินค้าขายนอกสถานที่
            /// </summary>
            public const double Z13 = 13;
            /// <summary>
            /// สินค้าฝากขาย
            /// </summary>
            public const double Z14 = 14;
            /// <summary>
            /// สินค้าขายส่ง
            /// </summary>
            public const double Z15 = 15;
            /// <summary>
            /// สินค้าขายปลีก
            /// </summary>
            public const double Z16 = 16;
            /// <summary>
            /// วัตถุดิบรอผลิต
            /// </summary>
            public const double Z17 = 17;
            /// <summary>
            /// วัตถุดิบรอเข้าคลัง
            /// </summary>
            public const double Z18 = 18;
            /// <summary>
            /// สินค้าสำเร็จรูป
            /// </summary>
            public const double Z19 = 19;
            /// <summary>
            /// สินค้าฉายรังสี
            /// </summary>
            public const double Z20 = 20;
            /// <summary>
            /// สินค้ากักกัน
            /// </summary>
            public const double Z21 = 21;
            /// <summary>
            /// สินค้ารอ QC
            /// </summary>
            public const double Z22 = 22;
            /// <summary>
            /// สินค้ารอรับเข้าคลัง
            /// </summary>
            public const double Z23 = 23;
            /// <summary>
            /// สินค้าส่งคืนผู้จำหน่าย
            /// </summary>
            public const double Z24 = 24;
            /// <summary>
            /// วัตถุดิบรอส่งคืนผู้จำหน่าย
            /// </summary>
            public const double Z25 = 25;
            /// <summary>
            /// วัตถุดิบส่งคืนผู้จำหน่าย
            /// </summary>
            public const double Z26 = 26;
            /// <summary>
            /// สินค้าตัวอย่างรอรับคืน
            /// </summary>
            public const double Z27 = 27;
            /// <summary>
            /// สินค้าจัดกระเช้า
            /// </summary>
            public const double Z28 = 28;
            /// <summary>
            /// วัตถุดิบส่งไปจ้างผลิต
            /// </summary>
            public const double Z29 = 29;
            /// <summary>
            /// สินค้าจอง
            /// </summary>
            public const double Z30 = 30;
            /// <summary>
            /// วัตถุดิบสำรองจ่าย
            /// </summary>
            public const double Z31 = 31;
        }

        public partial class PlanDetailType
        {
            public const string Produce = "1";
            public const string Purchase = "2";
            public const string Receive = "3";
            public const string Use = "4";
            public const string Remain = "5";
        }

        public partial class PlanProductStatus
        {
            public const string Purchase = "1";
            public const string Produce = "2";
            public const string Minimum = "3";
        }

        public partial class UnitType
        {
            public partial class ALL
            {
                public const string Code = "AL";
                public const string Name = "ทั้งหมด";
            }
            public partial class FG
            {
                public const string Code = "FG";
                public const string Name = "สินค้าสำเร็จรูป";
            }
            public partial class WH
            {
                public const string Code = "WH";
                public const string Name = "วัตถุดิบ";
            }
        }

        public partial class Radiation
        {
            public const string Yes = "Y";
            public const string No = "N";
        }

        public partial class PurchaseType
        {
            /// <summary>
            /// จัดจ้าง
            /// </summary>
            public const double TYPE01 = 1;
            /// <summary>
            /// จัดซื้อพัสดุ
            /// </summary>
            public const double TYPE02 = 2;
            /// <summary>
            /// จัดซื้อบรรจุภัณฑ์
            /// </summary>
            public const double TYPE03 = 3;
            /// <summary>
            /// จัดซื้อวัตถุดิบ
            /// </summary>
            public const double TYPE04 = 4;
            /// <summary>
            /// อื่นๆ
            /// </summary>
            public const double TYPE05 = 5;
            /// <summary>
            /// ซื้อย้อนหลัง
            /// </summary>
            public const double TYPE06 = 6;
        }

        public partial class ProductionStatus
        {
            public partial class Status
            {
                /// <summary>
                /// กำลังดำเนินการ
                /// </summary>
                public partial class WA
                {
                    public const string Code = "WA";
                    public const string Name = "กำลังดำเนินการ";
                    public const string Rank = "1";
                }

                /// <summary>
                /// รอผลิต
                /// </summary>
                public partial class RW
                {
                    public const string Code = "RW";
                    public const string Name = "เบิกวัตถุดิบ";
                    public const string Rank = "2";
                }
                /// <summary>
                /// ฉายรังสี
                /// </summary>
                public partial class RD
                {
                    public const string Code = "RD";
                    public const string Name = "ส่งไปฉายรังสี";
                    public const string Rank = "3";
                }
                /// <summary>
                /// กักกันสินค้า
                /// </summary>
                public partial class QS
                {
                    public const string Code = "QS";
                    public const string Name = "รับเข้ากักกันสินค้า";
                    public const string Rank = "4";
                }
                /// <summary>
                /// รอรับเข้า
                /// </summary>
                public partial class QC
                {
                    public const string Code = "QC";
                    public const string Name = "ส่งตรวจวิเคราะห์คุณภาพ";
                    public const string Rank = "5";
                }
                /// <summary>
                /// แจ้งผลการตรวจ
                /// </summary>
                public partial class QB
                {
                    public const string Code = "QB";
                    public const string Name = "แจ้งผลการตรวจ";
                    public const string Rank = "6";
                }
                /// <summary>
                /// อนุมัติ
                /// </summary>
                public partial class AP
                {
                    public const string Code = "AP";
                    public const string Name = "จ่ายออกจากคลังกักกัน";
                    public const string Rank = "7";
                }

                public partial class RR
                {
                    public const string Code = "RR";
                    public const string Name = "รับคืนจากการฉายรังสี";
                    public const string Rank = "8";
                }
                /// <summary>
                /// เสร็จสิ้น
                /// </summary>
                public partial class Finish
                {
                    public const string Code = "FN";
                    public const string Name = "เสร็จสิ้น";
                    public const string Rank = "9";
                }
                /// <summary>
                /// ยกเลิก
                /// </summary>
                public partial class Void
                {
                    public const string Code = "VO";
                    public const string Name = "ยกเลิก";
                    public const string Rank = "10";
                }

                /// <summary>
                /// ส่งตรวจ QC
                /// </summary>
                public partial class SendQC
                {
                    public const string Code = "QC";
                    public const string Name = "ส่งตรวจ QC";
                    public const string Rank = "11";
                }
                /// <summary>
                /// แจ้งผลการตรวจ 
                /// </summary>
                public partial class ReturnQC
                {
                    public const string Code = "AP";
                    public const string Name = "แจ้งผลการตรวจ";
                    public const string Rank = "12";
                }
                /// <summary>
                /// ทำรายการ 
                /// </summary>
                public partial class DoWaiting
                {
                    public const string Code = "WA";
                    public const string Name = "ทำรายการ";
                    public const string Rank = "13";
                }
                /// <summary>
                /// ส่งคลังวัตถุดิบ
                /// </summary>
                public partial class SendWareHouse
                {
                    public const string Code = "AP";
                    public const string Name = "ส่งคลังวัตถุดิบ";
                    public const string Rank = "14";
                }
                /// <summary>
                /// ส่งคลังสำเร็จรูป
                /// </summary>
                public partial class ApproveWH
                {
                    public const string Code = "AP";
                    public const string Name = "ส่งคลังสำเร็จรูป";
                    public const string Rank = "15";
                }

                /// <summary>
                /// ทั้งหมด
                /// </summary>
                public partial class AllStatus
                {
                    public const string Code = "ALL";
                    public const string Name = "ทั้งหมด";
                    public const string Rank = "999999";
                }
            }
        }

        public partial class UseMemberDiscount
        {
            public const string Yes = "1";
            public const string No = "0";
        }

    }
}