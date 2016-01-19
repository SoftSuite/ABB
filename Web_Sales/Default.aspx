<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
   <link href="Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <div>
          <tr>
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="600" class="searchtable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;Master</td>
                    </tr>
                    <tr height="25">
                        <td colspan="3" height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px; height: 18px;">
                        </td>
                       <td style="width: 600px; height: 18px;">
                            <asp:HyperLink ID="ProductTypeSearch" runat="server" Font-Bold="True" NavigateUrl="~/Master/ProductTypeSearch.aspx" Target="_blank">ประเภทสินค้า</asp:HyperLink></td> 
                    </tr> 
                   
                    <tr height="25px">
                        <td style="width: 50px" height="25">
                        </td>
                        <td style="width: 600px" height="25">
                            <asp:HyperLink ID="ProductGroupSearch" runat="server" Font-Bold="True" 
                                NavigateUrl="~/Master/ProductGroupSearch.aspx" Target="_blank">กลุ่มสินค้า</asp:HyperLink></td> 
                    </tr>
                    <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            <asp:HyperLink ID="UnitSearch" runat="server" Font-Bold="True" NavigateUrl="~/Master/UnitSearch.aspx" Target="_blank">หน่วยนับ</asp:HyperLink></td>
                    </tr>
                     <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            <asp:HyperLink ID="ProductSearch" runat="server" Font-Bold="True" NavigateUrl="~/Master/ProductSearch.aspx" Target="_blank">สินค้าสำเร็จรูป</asp:HyperLink></td>
                    </tr>
                     <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            <asp:HyperLink ID="MemberTypeSearch" runat="server" Font-Bold="True" 
                                NavigateUrl="~/Master/MemberTypeSearch.aspx" Target="_blank">ประเภทลูกค้า</asp:HyperLink></td>
                    </tr>
                     <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                           <asp:HyperLink ID="CustomerSearch" runat="server" Font-Bold="True"
                               NavigateUrl="~/Master/CustomerSearch.aspx" Target="_blank">ค้นหาบริษัท/ลูกค้า </asp:HyperLink></td>
                    </tr>
                    <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            <asp:HyperLink ID="PromotionSalesSearch" runat="server" Font-Bold="True" NavigateUrl="~/Master/PromotionSalesSearch.aspx" Target="_blank">กำหนดราคาสินค้าส่งเสริมการขาย</asp:HyperLink></td>
                    </tr>
                    <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            </td>
                    </tr>
                </table>
                          <table border="0" cellpadding="0" cellspacing="0" width="600" class="searchtable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;Transaction</td>
                    </tr>
                    <tr height="25">
                        <td colspan="3" height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px; height: 25px;">
                        </td>
                       <td style="width: 600px; height: 25px;">
                            <asp:HyperLink ID="ProductReserveSearch" runat="server" Font-Bold="True" 
                                NavigateUrl="~/Transaction/ProductReserveSearch.aspx" Target="_blank">ใบรับคำสั่งซื้อ/สั่งจอง</asp:HyperLink></td> 
                    </tr> 
                   
                    <tr height="25px">
                        <td style="width: 50px" height="25">
                        </td>
                        <td style="width: 600px" height="25">
                           <asp:HyperLink ID="ProductInvoiceSearch" runat="server" Font-Bold="True"
                               NavigateUrl="~/Transaction/ProductInvoiceSearch.aspx" Target="_blank">ใบเสร็จรับเงิน </asp:HyperLink></td> 
                    </tr>
                    <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            <asp:HyperLink ID="ReturnRequestSearch" runat="server" Font-Bold="True" NavigateUrl="~/Transaction/ReturnRequestSearch.aspx" Target="_blank">ใบลดหนี้</asp:HyperLink></td>
                    </tr>
                     <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            <asp:HyperLink ID="HyperLink1" runat="server" Font-Bold="True" 
                                NavigateUrl="~/Transaction/ProductOrderSearch.aspx" Target="_blank">บันทึกสั่งผลิตสินค้า</asp:HyperLink></td>
                    </tr>
                     <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            </td>
                    </tr>
                  </table>
                    <table border="0" cellpadding="0" cellspacing="0" width="600" class="searchtable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;Web_POS</td>
                    </tr>
                    <tr height="25">
                        <td colspan="3" height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px; height: 25px;">
                        </td>
                       <td style="width: 600px; height: 25px;">
                           <asp:HyperLink ID="ProductRequestInShopSearch" runat="server" Font-Bold="True"
                               NavigateUrl="~/Web_POS/ProductRequestInShopSearch.aspx" Target="_blank">ใบขอเบิกภายใน</asp:HyperLink></td> 
                    </tr> 
                     <tr height="25">
                        <td style="width: 50px; height: 10px;">
                        </td>
                        <td style="width: 600px; height: 10px;">
                            </td>
                    </tr>
                  </table>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>  
        </div>
    </form>
</body>
</html>
