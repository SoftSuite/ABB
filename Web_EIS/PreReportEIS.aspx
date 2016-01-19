<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PreReportEIS.aspx.cs" Inherits="PreReportEIS" Title="Untitled Page" %>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height:5px">
                <td style="width:5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style="height:25px">
                <td style="width:5px">
                </td>
                <td class="headtext">
                    &nbsp;รายงานสรุป</td>
            </tr> 
            <tr style="height:10px">
                <td style="width:5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" >รายงานจำนวนสินค้าที่ขายได้ เปรียบเทียบปี</asp:LinkButton></td>
            </tr> 
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">รายงานจำนวนสินค้าที่ขายได้ เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr> 
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">รายงานจำนวนสินค้าที่รับเข้า เปรียบเทียบปี</asp:LinkButton></td>
            </tr> 
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">รายงานจำนวนสินค้าที่รับเข้า เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr> 
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">รายงานจำนวนสินค้าที่จ่ายออก เปรียบเทียบปี</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click">รายงานจำนวนสินค้าที่จ่ายออก เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click">รายงานจำนวนสินค้าที่สนับสนุน เปรียบเทียบปี</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton8" runat="server" OnClick="LinkButton8_Click">รายงานจำนวนสินค้าที่สนับสนุน เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton9" runat="server" OnClick="LinkButton9_Click">รายงานจำนวนสินค้าที่รับคืน เปรียบเทียบปี</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton10" runat="server" OnClick="LinkButton10_Click">รายงานจำนวนสินค้าที่รับคืน เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton11" runat="server" OnClick="LinkButton11_Click">รายงานจำนวนสินค้าที่ส่งคืน เปรียบเทียบปี</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton12" runat="server" OnClick="LinkButton12_Click">รายงานจำนวนสินค้าที่ส่งคืน เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr>
            <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton13" runat="server" OnClick="LinkButton13_Click">รายงานยอดขายเปรียบเทียบปี</asp:LinkButton></td>
            </tr>
             <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton14" runat="server" OnClick="LinkButton14_Click">รายงานยอดขายเปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr>
                         <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton15" runat="server" OnClick="LinkButton15_Click">รายงานจำนวนสินค้าที่ผลิตได้เปรียบเทียบปี</asp:LinkButton></td>
            </tr>
                                     <tr style="height:20px">
                <td style="width:5px">
                </td>
                <td>
                    <asp:LinkButton ID="LinkButton16" runat="server" OnClick="LinkButton16_Click">รายงานจำนวนสินค้าที่ผลิตได้เปรียบเทียบสินค้า</asp:LinkButton></td>
            </tr>
        </table>
</asp:Content>

