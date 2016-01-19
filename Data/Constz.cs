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
        /// ���� config ����˹�� web.config
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
        /// ���¡�ü�Ե
        /// </summary>
        public partial class ProductionDepartment
        {
            public const double LOID = -1;
            public const string Code = "";
            public const string Name = "���¡�ü�Ե";
        }

        public partial class PurchaseDepartment
        {
            public const double LOID = 3;
            public const string Code = "DIV003";
            public const string Name = "���¨Ѵ����";
        }

        public partial class AdminDepartment
        {
            public const double LOID = 1;
            public const string Code = "DIV001";
            public const string Name = "�ٹ����������ʹ��";
        }

        /// <summary>
        /// ��ѧ��Ե�ѳ��������ٻ
        /// </summary>
        public partial class ReadyMadeDepartment
        {
            public const double LOID = 1;
            public const string Code = "";
            public const string Name = "��ѧ��Ե�ѳ��������ٻ";
        }

        public partial class DocType
        {
            /// <summary>
            /// ��ʴ�¡�ʹ�Ѻ
            /// </summary>
            public partial class RecShop
            {
                public const double LOID = 1;
                public const string NAME = "��ʴ�¡�ʹ�Ѻ";
            }
            /// <summary>
            /// ��Ѻ�׹
            /// </summary>
            public partial class RetShop
            {
                public const double LOID = 2;
                public const string NAME = "��Ѻ�׹";
            }
            /// <summary>
            /// ��駤׹�Թ��ҵ�����ҧ
            /// </summary>
            public partial class RetSample
            {
                public const double LOID = 3;
                public const string NAME = "��駤׹�Թ��ҵ�����ҧ";
            }
            /// <summary>
            /// ��ԡ�Թ��ҽҡ���
            /// </summary>
            public partial class ReqDistribute
            {
                public const double LOID = 4;
                public const string NAME = "��ԡ�Թ��ҽҡ���";
            }
            /// <summary>
            /// ��ԡ��¹͡ʶҹ���
            /// </summary>
            public partial class ReqFair
            {
                public const double LOID = 5;
                public const string NAME = "��ԡ��¹͡ʶҹ���";
            }
            /// <summary>
            /// ��ԡ˹��§ҹʹѺʹع
            /// </summary>
            public partial class ReqOrgSupport
            {
                public const double LOID = 6;
                public const string NAME = "��ԡ˹��§ҹʹѺʹع";
            }
            /// <summary>
            /// ��ԡʹѺʹع�Թ���
            /// </summary>
            public partial class ReqSupport
            {
                public const double LOID = 7;
                public const string NAME = "��ԡʹѺʹع�Թ���";
            }
            /// <summary>
            /// ��ԡ�Ѻ����觫���
            /// </summary>
            public partial class Reserve
            {
                public const double LOID = 8;
                public const string NAME = "��ԡ�Ѻ����觫���";
            }
            /// <summary>
            /// ����觤׹�Թ���
            /// </summary>
            public partial class RetProduct
            {
                public const double LOID = 9;
                public const string NAME = "����觤׹�Թ���";
            }
            /// <summary>
            /// ����觤׹�ѵ�شԺ
            /// </summary>
            public partial class RetRaw
            {
                public const double LOID = 10;
                public const string NAME = "����觤׹�ѵ�شԺ";
            }
            /// <summary>
            /// ��ԡ�ѵ�شԺ���ͼ�Ե
            /// </summary>
            public partial class ReqRawPD
            {
                public const double LOID = 11;
                public const string NAME = "��ԡ�ѵ�شԺ���ͼ�Ե";
            }
            /// <summary>
            /// ��ԡ�ѵ�شԺ���ͨ�ҧ��Ե
            /// </summary>
            public partial class ReqRawPO
            {
                public const double LOID = 12;
                public const string NAME = "��ԡ�ѵ�شԺ���ͨ�ҧ��Ե";
            }
            /// <summary>
            /// 㺹����Թ�����Ҥ�ѧ
            /// </summary>
            public partial class DelProduct
            {
                public const double LOID = 13;
                public const string NAME = "㺹��觼�Ե�ѳ����Ҥ�ѧ";
            }
            /// <summary>
            /// 㺵�Ǩ�Ѻ�Թ���
            /// </summary>
            public partial class RecProduct
            {
                public const double LOID = 14;
                public const string NAME = "㺵�Ǩ�Ѻ�Թ���";
            }
            /// <summary>
            /// 㺹����ѵ�شԺ��Ҥ�ѧ
            /// </summary>
            public partial class DelRaw
            {
                public const double LOID = 15;
                public const string NAME = "㺹����ѵ�شԺ��Ҥ�ѧ";
            }
            /// <summary>
            /// 㺵�Ǩ�Ѻ�ѵ�شԺ
            /// </summary>
            public partial class RecRaw
            {
                public const double LOID = 16;
                public const string NAME = "㺵�Ǩ�Ѻ�ѵ�شԺ";
            }
            /// <summary>
            /// ��ԡ�͡�ҡ��ѧ
            /// </summary>
            public partial class ReqProduct
            {
                public const double LOID = 17;
                public const string NAME = "��ԡ�͡�ҡ��ѧ";
            }
            /// <summary>
            /// 㺤׹�����Թ����͡
            /// </summary>
            public partial class RetFair
            {
                public const double LOID = 18;
                public const string NAME = "㺤׹�����Թ����͡";
            }
            /// <summary>
            /// ��Ѻ�׹�Թ��ҽҡ���
            /// </summary>
            public partial class RetDistribute
            {
                public const double LOID = 19;
                public const string NAME = "��Ѻ�׹�Թ��ҽҡ���";
            }
            /// <summary>
            /// ��Ѻ�׹�Թ��ҵ�����ҧ
            /// </summary>
            public partial class RetInSample
            {
                public const double LOID = 20;
                public const string NAME = "��Ѻ�׹�Թ��ҵ�����ҧ";
            }
            /// <summary>
            /// ��Ѻ�׹�Թ���Ŵ˹��
            /// </summary>
            public partial class RetInReduce
            {
                public const double LOID = 21;
                public const string NAME = "��Ѻ�׹�Թ���Ŵ˹��";
            }
            /// <summary>
            /// ��Ѻ�׹�ѵ�شԺ�ҡ���¼�Ե
            /// </summary>
            public partial class RetMaterial
            {
                public const double LOID = 22;
                public const string NAME = "��Ѻ�׹�ѵ�شԺ�ҡ���¼�Ե";
            }
            /// <summary>
            /// ��ԡʹѺʹع�ѵ�شԺ
            /// </summary>
            public partial class RetSMaterial
            {
                public const double LOID = 24;
                public const string NAME = "��ԡʹѺʹع�ѵ�شԺ";
            }
            /// <summary>
            /// ��ԡ��ʴ�
            /// </summary>
            public partial class RetSOther
            {
                public const double LOID = 25;
                public const string NAME = "��ԡ��ʴ�";
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
            /// �ؤ�ŷ����
            /// </summary>
            public partial class Personal
            {
                public const string Code = "P";
                public const string Name = "�ؤ�ŷ����";
            }
            /// <summary>
            /// ����ѷ�͡��
            /// </summary>
            public partial class Company
            {
                public const string Code = "C";
                public const string Name = "����ѷ�͡��";
            }
            /// <summary>
            /// ͧ���/˹��§ҹ�ͧ�Ѱ
            /// </summary>
            public partial class Government
            {
                public const string Code = "G";
                public const string Name = "ͧ���/˹��§ҹ�ͧ�Ѱ";
            }
        }

        public partial class Warehouse
        {
            public partial class Type
            {
                public partial class FG
                {
                    public const string Code = "FG";
                    public const string Name = "�Թ���������ٻ";
                }
                public partial class WH
                {
                    public const string Code = "WH";
                    public const string Name = "�ѵ�شԺ";
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
                    public const string Name = "�Թ���������ٻ";
                    public const string Rank = "1";
                }
                public partial class WH
                {
                    public const string Code = "WH";
                    public const string Name = "�ѵ�شԺ";
                    public const string Rank = "2";
                }
                public partial class Others
                {
                    public const string Code = "OT";
                    public const string Name = "����";
                    public const string Rank = "3";
                }
            }
        }

        public partial class DeliveryType
        {
            public partial class Undefined
            {
                public const string Code = "ZZ";
                public const string Name = "����к�";
            }
            public partial class Customer
            {
                public const string Code = "CU";
                public const string Name = "�Ѻ�ͧ";
            }
            public partial class Car
            {
                public const string Code = "CA";
                public const string Name = "����ö����ѷ";
            }
            public partial class TransferCompany
            {
                public const string Code = "TR";
                public const string Name = "���º���ѷ�Ѻ��ҧ����";
            }
            public partial class Mail
            {
                public const string Code = "MA";
                public const string Name = "�觷ҧ��ɳ���";
            }
            public partial class Other
            {
                public const string Code = "OT";
                public const string Name = "����";
            }
        }

        public partial class Payment
        {
            public partial class Cash
            {
                public const string Code = "CA";
                public const string Name = "�Թʴ";
            }
            public partial class CreditCard
            {
                public const string Code = "CC";
                public const string Name = "�ѵ��ôԵ";
            }
            public partial class Credit
            {
                public const string Code = "CR";
                public const string Name = "�Թ����";
            }
            public partial class Cheque
            {
                public const string Code = "CH";
                public const string Name = "��";
            }
            public partial class Others
            {
                public const string Code = "OT";
                public const string Name = "����";
            }
        }

        public partial class Basket
        {
            public partial class Status
            {
                public partial class Waiting
                {
                    public const string Code = "WA";
                    public const string Name = "��͡������";
                    public const string Rank = "1";
                }
                public partial class Approved
                {
                    public const string Code = "AP";
                    public const string Name = "�觤�ѧ������";
                    public const string Rank = "7";
                }
                public partial class Finish
                {
                    public const string Code = "FN";
                    public const string Name = "�������";
                    public const string Rank = "8";
                }
            }

            public partial class Type
            {
                public partial class New
                {
                    public const string Code = "NEW";
                    public const string Name = "�Ѵ����������";
                }
                public partial class Return
                {
                    public const string Code = "RET";
                    public const string Name = "�׹�Թ���㹡�����";
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
                    public const string Name = "��͡������";
                    public const string Rank = "1";
                }
                public partial class QC
                {
                    public const string Code = "QC";
                    public const string Name = "�觵�Ǩ";
                    public const string Rank = "6";
                }
                public partial class Approved
                {
                    public const string Code = "AP";
                    public const string Name = "�������";
                    public const string Rank = "7";
                }
                public partial class Void
                {
                    public const string Code = "VO";
                    public const string Name = "¡��ԡ";
                    public const string Rank = "9";
                }
            }
        }

        public partial class Requisition
        {
            public partial class Status
            {
                /// <summary>
                /// ���ѧ���Թ���
                /// </summary>
                public partial class Waiting
                {
                    public const string Code = "WA";
                    public const string Name = "���ѧ���Թ���";
                    public const string Rank = "1";
                }
                /// <summary>
                /// ��觨ͧ
                /// </summary>
                public partial class Reserve
                {
                    public const string Code = "RS";
                    public const string Name = "��觨ͧ";
                    public const string Rank = "2";
                }
                /// <summary>
                /// �ͼ�Ե
                /// </summary>
                public partial class RW
                {
                    public const string Code = "RW";
                    public const string Name = "�ͼ�Ե";
                    public const string Rank = "3";
                }
                /// <summary>
                /// ����ѧ��
                /// </summary>
                public partial class XRay
                {
                    public const string Code = "RD";
                    public const string Name = "����ѧ��";
                    public const string Rank = "4";
                }
                /// <summary>
                /// �ѡ�ѹ�Թ���
                /// </summary>
                public partial class QS
                {
                    public const string Code = "QS";
                    public const string Name = "�ѡ�ѹ�Թ���";
                    public const string Rank = "5";
                }
                /// <summary>
                /// ���Ѻ���
                /// </summary>
                public partial class QC
                {
                    public const string Code = "QC";
                    public const string Name = "���Ѻ���";
                    public const string Rank = "6";
                }
                public partial class SP
                {
                    public const string Code = "SP";
                    public const string Name = "�����Ѵ����";
                    public const string Rank = "7";
                }
                /// <summary>
                /// ͹��ѵ�
                /// </summary>
                public partial class Approved
                {
                    public const string Code = "AP";
                    public const string Name = "͹��ѵ�";
                    public const string Rank = "8";
                }
                /// <summary>
                /// �������
                /// </summary>
                public partial class Finish
                {
                    public const string Code = "FN";
                    public const string Name = "�������";
                    public const string Rank = "9";
                }
                /// <summary>
                /// ¡��ԡ
                /// </summary>
                public partial class Void
                {
                    public const string Code = "VO";
                    public const string Name = "¡��ԡ";
                    public const string Rank = "10";
                }

                /// <summary>
                /// �觵�Ǩ QC
                /// </summary>
                public partial class SendQC
                {
                    public const string Code = "QC";
                    public const string Name = "�觵�Ǩ QC";
                    public const string Rank = "11";
                }
                /// <summary>
                /// �駼š�õ�Ǩ 
                /// </summary>
                public partial class ReturnQC
                {
                    public const string Code = "AP";
                    public const string Name = "�駼š�õ�Ǩ";
                    public const string Rank = "12";
                }
                /// <summary>
                /// ����¡�� 
                /// </summary>
                public partial class DoWaiting
                {
                    public const string Code = "WA";
                    public const string Name = "����¡��";
                    public const string Rank = "13";
                }
                /// <summary>
                /// �觤�ѧ�ѵ�شԺ
                /// </summary>
                public partial class SendWareHouse
                {
                    public const string Code = "AP";
                    public const string Name = "�觤�ѧ�ѵ�شԺ";
                    public const string Rank = "14";
                }
                /// <summary>
                /// �觤�ѧ������ٻ
                /// </summary>
                public partial class ApproveWH
                {
                    public const string Code = "AP";
                    public const string Name = "�觤�ѧ������ٻ";
                    public const string Rank = "15";
                }

                /// <summary>
                /// ������
                /// </summary>
                public partial class AllStatus
                {
                    public const string Code = "ALL";
                    public const string Name = "������";
                    public const string Rank = "999999";
                }
            }

            public partial class RequisitionType
            {
                /// <summary>
                /// ���觫���/��觨ͧ
                /// </summary>
                public const double REQ01 = 1;
                /// <summary>
                /// 㺢��ԡ�Թ��ҽҡ���
                /// </summary>
                public const double REQ02 = 2;
                /// <summary>
                /// 㺢��ԡ��¹͡ʶҹ���
                /// </summary>
                public const double REQ03 = 3;
                /// <summary>
                /// 㺢�ʹѺʹع�Թ���
                /// </summary>
                public const double REQ04 = 4;
                /// <summary>
                /// �Ŵ˹��
                /// </summary>
                public const double REQ05 = 5;
                /// <summary>
                /// 㺢��ԡ����
                /// </summary>
                public const double REQ06 = 6;
                /// <summary>
                /// �ѹ�֡��觼�Ե
                /// </summary>
                public const double REQ07 = 7;
                /// <summary>
                /// 㺢��ԡ�ѵ�شԺ��к�è��ѳ�����͡�ü�Ե
                /// </summary>
                public const double REQ08 = 8;
                /// <summary>
                /// 㺢��ԡ�ѵ�شԺ���ͨ�ҧ��Ե
                /// </summary>
                public const double REQ09 = 9;
                /// <summary>
                /// 㺢��ԡ˹��§ҹʹѺʹع
                /// </summary>
                public const double REQ10 = 10;
                /// <summary>
                /// ������Ѻ�Թ
                /// </summary>
                public const double REQ11 = 11;
                /// <summary>
                /// ��Ѻ�׹�Թ��ҽҡ���
                /// </summary>
                public const double REQ12 = 12;
                /// <summary>
                /// POS
                /// </summary>
                public const double REQ13 = 13;
                /// <summary>
                /// 㺢ͤ׹�ѵ�شԺ��к�è��ѳ��
                /// </summary>
                public const double REQ14 = 14;
                /// <summary>
                /// 㺢��ԡ�ѵ�شԺ���ͼ�Ե
                /// </summary>
                public const double REQ15 = 15;
                /// <summary>
                /// ʹѺʹع�ѵ�شԺ
                /// </summary>
                public const double REQ24 = 24;
            }

        }

        public partial class QCResult
        {
            public partial class Pass
            {
                public const string Code = "Y";
                public const string Name = "��ҹ";
            }
            public partial class Fail
            {
                public const string Code = "N";
                public const string Name = "����ҹ";
            }
        }

        public partial class OrderType
        {
            public partial class PO
            {
                public const string Code = "PO";
                public const string Name = "��觫���";
            }
            public partial class PD
            {
                public const string Code = "PD";
                public const string Name = "��觼�Ե";
            }
            public partial class AR
            {
                public const string Code = "AR";
                public const string Name = "����ͧ";
            }
        }

        public partial class VAT
        {
            public partial class Included
            {
                public const string Code = "1";
                public const string Name = "�������";
            }
            public partial class NotIncluded
            {
                public const string Code = "0";
                public const string Name = "����������";
            }
        }

        public partial class Discount
        {
            public partial class Calculated
            {
                public const string Code = "1";
                public const string Name = "�Դ��ǹŴ";
            }
            public partial class NotCalculated
            {
                public const string Code = "0";
                public const string Name = "���Դ��ǹŴ";
            }
        }

        public partial class Edit
        {
            public partial class Editable
            {
                public const string Code = "Y";
                public const string Name = "����Ҥ���";
            }
            public partial class DisEditable
            {
                public const string Code = "N";
                public const string Name = "����Ҥ������";
            }
        }

        public partial class Refund
        {
            public partial class Yes
            {
                public const string Code = "Y";
                public const string Name = "�׹�Թ�����Ҫԡ";
            }
            public partial class No
            {
                public const string Code = "N";
                public const string Name = "���׹�Թ�����Ҫԡ";
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
                        public const string Name = "�Թ��ҷ���ӡ��� Minimum Stock";
                    }
                    public partial class StockIn
                    {
                        public const string Index = "2";
                        public const string Name = "�Թ��ҷ�����Ѻ���";
                    }
                    public partial class StockOut
                    {
                        public const string Index = "3";
                        public const string Name = "�Թ��ҷ���ͨ����͡";
                    }
                    public partial class Expire
                    {
                        public const string Index = "4";
                        public const string Name = "�Թ��ҷ������������";
                    }
                }
                public partial class WH
                {
                    public partial class MinimumStock
                    {
                        public const string Index = "1";
                        public const string Name = "�ѵ�شԺ����ӡ��� Minimum Stock";
                    }
                    public partial class StockIn
                    {
                        public const string Index = "2";
                        public const string Name = "�ѵ�شԺ������Ѻ���";
                    }
                    public partial class StockOut
                    {
                        public const string Index = "3";
                        public const string Name = "�ѵ�شԺ����ͨ����͡";
                    }
                    public partial class Expire
                    {
                        public const string Index = "4";
                        public const string Name = "�ѵ�شԺ�������������";
                    }
                }
            }
            public partial class Purchase
            {
                public partial class ProductReceiveList
                {
                    public const string Index = "1";
                    public const string Name = "��¡���Թ��ҷ���ͷ����觫���";
                }
                public partial class ProductPurchaseList
                {
                    public const string Index = "2";
                    public const string Name = "��¡���Թ��ҷ�����Ѻ�Թ���";
                }
            }
            public partial class Production
            {
                public partial class ProductionWaitList
                {
                    public const string Index = "1";
                    public const string Name = "��¡���Թ��ҷ���ͼ�Ե";
                }
                public partial class ProductionDuringList
                {
                    public const string Index = "2";
                    public const string Name = "��¡���Թ��ҷ�����������ҧ��ü�Ե";
                }
            }
        }

        public partial class ProductionTab
        {
            public partial class RawMaterialUsing
            {
                public const string Index = "1";
                public const string Row = "2";
                public const string Name = "������ѵ�شԺ";
            }
            public partial class Pack
            {
                public const string Index = "2";
                public const string Row = "2";
                public const string Name = "��ú�è�";
            }
            public partial class X_RaySending
            {
                public const string Index = "3";
                public const string Row = "2";
                public const string Name = "��仩���ѧ��";
            }
            public partial class X_RayReceiving
            {
                public const string Index = "4";
                public const string Row = "2";
                public const string Name = "�Ѻ�׹�ҡ����ѧ��";
            }
            public partial class Import
            {
                public const string Index = "5";
                public const string Row = "2";
                public const string Name = "�Ѻ��Ҥ�ѧ�ѡ�ѹ";
            }
            public partial class QC
            {
                public const string Index = "6";
                public const string Row = "1";
                public const string Name = "�觵�Ǩ��������س�Ҿ";
            }
            public partial class RawMaterialLoss
            {
                public const string Index = "7";
                public const string Row = "1";
                public const string Name = "�����٭�����ѵ�شԺ";
            }
            public partial class PackLoss
            {
                public const string Index = "8";
                public const string Row = "1";
                public const string Name = "�����٭���º�è��ѳ��";
            }
            public partial class Export
            {
                public const string Index = "9";
                public const string Row = "1";
                public const string Name = "�����͡�ҡ��ѧ�ѡ�ѹ";
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
            /// �Թ��һ���
            /// </summary>
            public const double Z01 = 1;
            /// <summary>
            /// �Թ���ʹѺʹع˹��§ҹ
            /// </summary>
            public const double Z02 = 2;
            /// <summary>
            /// �Թ��� Reject
            /// </summary>
            public const double Z03 = 3;
            /// <summary>
            /// �ѵ�شԺ
            /// </summary>
            public const double Z04 = 4;
            /// <summary>
            /// �ѵ�شԺ�� QC
            /// </summary>
            public const double Z05 = 5;
            /// <summary>
            /// �ѵ�شԺ Reject
            /// </summary>
            public const double Z06 = 6;
            /// <summary>
            /// �Թ���ʹѺʹع
            /// </summary>
            public const double Z07 = 7;
            /// <summary>
            /// �Թ���Ŵ˹��
            /// </summary>
            public const double Z08 = 8;
            /// <summary>
            /// �Թ������Ѻ���
            /// </summary>
            public const double Z10 = 10;
            /// <summary>
            /// �Թ���˹����ҹ
            /// </summary>
            public const double Z11 = 11;
            /// <summary>
            /// �Թ������觤׹����˹���
            /// </summary>
            public const double Z12 = 12;
            /// <summary>
            /// �Թ��Ң�¹͡ʶҹ���
            /// </summary>
            public const double Z13 = 13;
            /// <summary>
            /// �Թ��ҽҡ���
            /// </summary>
            public const double Z14 = 14;
            /// <summary>
            /// �Թ��Ң����
            /// </summary>
            public const double Z15 = 15;
            /// <summary>
            /// �Թ��Ң�»�ա
            /// </summary>
            public const double Z16 = 16;
            /// <summary>
            /// �ѵ�شԺ�ͼ�Ե
            /// </summary>
            public const double Z17 = 17;
            /// <summary>
            /// �ѵ�شԺ����Ҥ�ѧ
            /// </summary>
            public const double Z18 = 18;
            /// <summary>
            /// �Թ���������ٻ
            /// </summary>
            public const double Z19 = 19;
            /// <summary>
            /// �Թ��ҩ���ѧ��
            /// </summary>
            public const double Z20 = 20;
            /// <summary>
            /// �Թ��ҡѡ�ѹ
            /// </summary>
            public const double Z21 = 21;
            /// <summary>
            /// �Թ����� QC
            /// </summary>
            public const double Z22 = 22;
            /// <summary>
            /// �Թ������Ѻ��Ҥ�ѧ
            /// </summary>
            public const double Z23 = 23;
            /// <summary>
            /// �Թ����觤׹����˹���
            /// </summary>
            public const double Z24 = 24;
            /// <summary>
            /// �ѵ�شԺ���觤׹����˹���
            /// </summary>
            public const double Z25 = 25;
            /// <summary>
            /// �ѵ�شԺ�觤׹����˹���
            /// </summary>
            public const double Z26 = 26;
            /// <summary>
            /// �Թ��ҵ�����ҧ���Ѻ�׹
            /// </summary>
            public const double Z27 = 27;
            /// <summary>
            /// �Թ��ҨѴ������
            /// </summary>
            public const double Z28 = 28;
            /// <summary>
            /// �ѵ�شԺ��仨�ҧ��Ե
            /// </summary>
            public const double Z29 = 29;
            /// <summary>
            /// �Թ��Ҩͧ
            /// </summary>
            public const double Z30 = 30;
            /// <summary>
            /// �ѵ�شԺ���ͧ����
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
                public const string Name = "������";
            }
            public partial class FG
            {
                public const string Code = "FG";
                public const string Name = "�Թ���������ٻ";
            }
            public partial class WH
            {
                public const string Code = "WH";
                public const string Name = "�ѵ�شԺ";
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
            /// �Ѵ��ҧ
            /// </summary>
            public const double TYPE01 = 1;
            /// <summary>
            /// �Ѵ���;�ʴ�
            /// </summary>
            public const double TYPE02 = 2;
            /// <summary>
            /// �Ѵ���ͺ�è��ѳ��
            /// </summary>
            public const double TYPE03 = 3;
            /// <summary>
            /// �Ѵ�����ѵ�شԺ
            /// </summary>
            public const double TYPE04 = 4;
            /// <summary>
            /// ����
            /// </summary>
            public const double TYPE05 = 5;
            /// <summary>
            /// ������͹��ѧ
            /// </summary>
            public const double TYPE06 = 6;
        }

        public partial class ProductionStatus
        {
            public partial class Status
            {
                /// <summary>
                /// ���ѧ���Թ���
                /// </summary>
                public partial class WA
                {
                    public const string Code = "WA";
                    public const string Name = "���ѧ���Թ���";
                    public const string Rank = "1";
                }

                /// <summary>
                /// �ͼ�Ե
                /// </summary>
                public partial class RW
                {
                    public const string Code = "RW";
                    public const string Name = "�ԡ�ѵ�شԺ";
                    public const string Rank = "2";
                }
                /// <summary>
                /// ����ѧ��
                /// </summary>
                public partial class RD
                {
                    public const string Code = "RD";
                    public const string Name = "��仩���ѧ��";
                    public const string Rank = "3";
                }
                /// <summary>
                /// �ѡ�ѹ�Թ���
                /// </summary>
                public partial class QS
                {
                    public const string Code = "QS";
                    public const string Name = "�Ѻ��ҡѡ�ѹ�Թ���";
                    public const string Rank = "4";
                }
                /// <summary>
                /// ���Ѻ���
                /// </summary>
                public partial class QC
                {
                    public const string Code = "QC";
                    public const string Name = "�觵�Ǩ��������س�Ҿ";
                    public const string Rank = "5";
                }
                /// <summary>
                /// �駼š�õ�Ǩ
                /// </summary>
                public partial class QB
                {
                    public const string Code = "QB";
                    public const string Name = "�駼š�õ�Ǩ";
                    public const string Rank = "6";
                }
                /// <summary>
                /// ͹��ѵ�
                /// </summary>
                public partial class AP
                {
                    public const string Code = "AP";
                    public const string Name = "�����͡�ҡ��ѧ�ѡ�ѹ";
                    public const string Rank = "7";
                }

                public partial class RR
                {
                    public const string Code = "RR";
                    public const string Name = "�Ѻ�׹�ҡ��é���ѧ��";
                    public const string Rank = "8";
                }
                /// <summary>
                /// �������
                /// </summary>
                public partial class Finish
                {
                    public const string Code = "FN";
                    public const string Name = "�������";
                    public const string Rank = "9";
                }
                /// <summary>
                /// ¡��ԡ
                /// </summary>
                public partial class Void
                {
                    public const string Code = "VO";
                    public const string Name = "¡��ԡ";
                    public const string Rank = "10";
                }

                /// <summary>
                /// �觵�Ǩ QC
                /// </summary>
                public partial class SendQC
                {
                    public const string Code = "QC";
                    public const string Name = "�觵�Ǩ QC";
                    public const string Rank = "11";
                }
                /// <summary>
                /// �駼š�õ�Ǩ 
                /// </summary>
                public partial class ReturnQC
                {
                    public const string Code = "AP";
                    public const string Name = "�駼š�õ�Ǩ";
                    public const string Rank = "12";
                }
                /// <summary>
                /// ����¡�� 
                /// </summary>
                public partial class DoWaiting
                {
                    public const string Code = "WA";
                    public const string Name = "����¡��";
                    public const string Rank = "13";
                }
                /// <summary>
                /// �觤�ѧ�ѵ�شԺ
                /// </summary>
                public partial class SendWareHouse
                {
                    public const string Code = "AP";
                    public const string Name = "�觤�ѧ�ѵ�شԺ";
                    public const string Rank = "14";
                }
                /// <summary>
                /// �觤�ѧ������ٻ
                /// </summary>
                public partial class ApproveWH
                {
                    public const string Code = "AP";
                    public const string Name = "�觤�ѧ������ٻ";
                    public const string Rank = "15";
                }

                /// <summary>
                /// ������
                /// </summary>
                public partial class AllStatus
                {
                    public const string Code = "ALL";
                    public const string Name = "������";
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