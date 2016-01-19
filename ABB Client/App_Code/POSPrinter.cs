using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using ABB.Data;
using ABB.Data.Sales;
using ABB.Flow.Sales;
using System.Configuration;
using System.IO.Ports;
using System.Threading;

namespace ABBClient.App_Code
{
    public class POSPrinter
    {
        private double _REQUISITION = 0;
        private PointOfSaleFlow _flow;
        private SaleFlow _sFlow;
        private Font printFont;
        private Font fixFont;
        private Brush printBrush;

        public double REQUISITION
        {
            get { return _REQUISITION; }
            set { _REQUISITION = value; }
        }

        public PointOfSaleFlow FlowObj
        {
            get { if (_flow == null) { _flow = new PointOfSaleFlow(); } return _flow; }
        }

        public SaleFlow sFlow
        {
            get { if (_sFlow == null) { _sFlow = new SaleFlow(); } return _sFlow; }
        }

        private void OpenDrawer()
        {
            try
            {
                SerialPort sp = new SerialPort(ConfigurationManager.AppSettings["PORT"].ToString(), 9600);
                sp.Open();
                sp.ReadTimeout = 500;
                sp.WriteLine("NOP");
                sp.Close();
            }
            catch
            {
            }
        }

        private void CutPaper()
        {
            try
            {
                SerialPort sp = new SerialPort(ConfigurationManager.AppSettings["PRINTERPORT"].ToString(), 9600);
                sp.Open();
                sp.ReadTimeout = 500;
                sp.WriteLine("F");
                sp.Close();
            }
            catch
            {
            }
        }

        public void Print(double requisition)
        {
            _REQUISITION = requisition;
            string font = "";
            try
            {
                font = ConfigurationManager.AppSettings["FONT"].ToString();
            }
            catch (Exception ex)
            {
                font = "Arial";
            }
            fixFont = new Font(font, 10);
            printFont = new Font(font, Convert.ToInt64(ConfigurationManager.AppSettings["SIZE"]));  //new Font("Tahoma", 8);
            printBrush = Brushes.Black;
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(this.pd_PrintPage);
            try
            {
                pd.DefaultPageSettings.Margins.Bottom = 1;
                pd.DefaultPageSettings.Margins.Left = 1;
                pd.DefaultPageSettings.Margins.Right = 1;
                pd.DefaultPageSettings.Margins.Top = 0;
                pd.DefaultPageSettings.PaperSize = new System.Drawing.Printing.PaperSize("POS", 245, 200); //200
                pd.Print();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }
        }

        private void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            OpenDrawer();
            PointOfSaleData data = FlowObj.GetData(REQUISITION);
            string POSCode = "";
            string customerCode = "";
            customerCode = sFlow.GetCustomerData(data.CUSTOMER).CODE;
            POSCode = ConfigurationManager.AppSettings["POSCODE"].ToString();
            int count = -1;
            int leftMargin = e.MarginBounds.Left;
            int rightMargin = e.MarginBounds.Right;
            int topMargin = e.MarginBounds.Top;
            int recHeight = 25;
            StringFormat sf = new StringFormat(); sf.Alignment = System.Drawing.StringAlignment.Far; sf.FormatFlags = StringFormatFlags.NoWrap; sf.LineAlignment = StringAlignment.Center;
            String msg = null;

            msg = Appz.GetSysConfig(Constz.ConfigName.ORGNAME);
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            ++count;

            msg = "... Payless, Higher Quality ...";
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = Appz.GetSysConfig(Constz.ConfigName.ORGTEL);
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            
            sf.Alignment = StringAlignment.Center;
            msg = "RECEIPT/TAX INVOICE";
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = "NO. " + data.CODE;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = "DATE:" + data.REQDATE.ToString("dd/MM/yy HH:mm");
            sf.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = "CASHIER:" + data.CREATEBY;
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = "COM: " + POSCode;
            sf.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = "CUST NO:" + customerCode;
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            ++count;
            /*
            msg = "PRODUCT";
            sf.Alignment = StringAlignment.Near;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(158, recHeight)), sf);

            msg = "QTY";
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin + 130, Convert.ToInt32(topMargin + ((count) * printFont.GetHeight(e.Graphics)))), new Size(40, recHeight)), sf);

            msg = "AMOUNT";
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            */
            for (int i = 0; i < data.REQUISITIONITEM.Count; ++i)
            {
                RequisitionItemData itemData = (RequisitionItemData)data.REQUISITIONITEM[i];
                msg = (itemData.ProductName.Length > 24 ? itemData.ProductName.Substring(0, 24) : itemData.ProductName);
                //msg = itemData.ProductName;
                sf.Alignment = StringAlignment.Near;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(140, recHeight)), sf);

                msg = itemData.QTY.ToString(Constz.IntFormat);
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin + 130, Convert.ToInt32(topMargin + ((count) * printFont.GetHeight(e.Graphics)))), new Size(40, recHeight)), sf);

                msg = itemData.NETPRICE.ToString(Constz.DblFormat);
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            }

            ++count;

            msg = "TOTAL";
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

            msg = (data.TOTAL + data.TOTVAT).ToString(Constz.DblFormat);
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = "DISCOUNT";
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

            //msg = data.TOTDIS.ToString(Constz.DblFormat);
            msg = (Convert.ToDouble(data.GRANDTOT.ToString(Constz.IntFormat)) - (data.TOTAL + data.TOTVAT)).ToString(Constz.DblFormat);
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            //if (data.VAT > 0)
            //{
            //    msg = "VAT " + data.VAT.ToString(Constz.IntFormat) + "%";
            //    sf.Alignment = StringAlignment.Far;
            //    e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

            //    msg = data.TOTVAT.ToString(Constz.DblFormat);
            //    sf.Alignment = StringAlignment.Far;
            //    e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            //}

            data.GRANDTOT = Convert.ToDouble(data.GRANDTOT.ToString(Constz.IntFormat));

            msg = "GRAND TOTAL";
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

            msg = data.GRANDTOT.ToString(Constz.IntFormat) + ".00";
            sf.Alignment = StringAlignment.Far;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            ++count;

            if (data.CASH > 0)
            {
                msg = "CASH";
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

                msg = data.CASH.ToString(Constz.DblFormat);
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            }

            if (data.CREDITCARDPAY > 0)
            {
                msg = "CREDIT CARD";
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

                msg = data.CREDITCARDPAY.ToString(Constz.DblFormat);
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            }

            if (data.COUPON > 0)
            {
                msg = "COUPON";
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

                msg = data.COUPON.ToString(Constz.DblFormat);
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            }

            if (data.COUPON < data.GRANDTOT && data.CASH + data.COUPON >0)
            {
                msg = "CHANGE";
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(180, recHeight)), sf);

                msg = (data.COUPON + data.CASH + data.CREDITCARDPAY - data.GRANDTOT).ToString(Constz.DblFormat);
                sf.Alignment = StringAlignment.Far;
                e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + (count * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            }

            ++count;

            msg = "***  THANK YOU  ***";
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            #region Additional
            /*
            msg = "F";
            e.Graphics.DrawString(msg, new Font("control", 10), printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            CutPaper();
            ++count;
            ++count;

            msg = Appz.GetSysConfig(Constz.ConfigName.ORGNAME);
            sf.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(msg, printFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            ++count;

            msg = "... Payless, Higher Quality ...";
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);

            msg = Appz.GetSysConfig(Constz.ConfigName.ORGTEL);
            e.Graphics.DrawString(msg, fixFont, printBrush, new RectangleF(new Point(leftMargin, Convert.ToInt32(topMargin + ((++count) * printFont.GetHeight(e.Graphics)))), new Size(e.PageSettings.PaperSize.Width - 4, recHeight)), sf);
            */
            #endregion

            e.HasMorePages = false;
        }

    }
}
