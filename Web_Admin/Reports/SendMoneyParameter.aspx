<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SendMoneyParameter.aspx.cs" Inherits="Reports_SendMoneyParameter" Title="รายงานบันทึกการนำส่งเงินค่ายาสมุนไพร" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MenuContent" Runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
 <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height :5px">
                <td style="width:5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style ="height :25px">
                <td style="width:5px">
                </td>
                <td class="headtext">
                    &nbsp;รายงานบันทึกการนำส่งเงินค่ายาสมุนไพร</td>
            </tr> 
            <tr style ="height :10px">
                <td style="width:5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td style="width:5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="500px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6"  style ="height :20px">
                                &nbsp;รายงานบันทึกการนำส่งเงินค่ายาสมุนไพร</td> 
                        </tr>
                        <tr style ="height :5px">
                            <td style="width : 10px">
                            </td> 
                            <td style="width : 90px">
                            </td>  
                            <td style="width : 150px">
                            </td> 
                            <td style="width : 100px">
                            </td>  
                            <td style="width : 150px">
                            </td> 
                            <td style="width : 40px">
                            </td>  
                        </tr>
                        <tr style ="height :25px">
                            <td  style="width : 10px">
                            </td> 
                            <td style="width : 90px">
                                เลขที่ใบเสร็จ</td>  
                            <td style="width : 150px">
                                <asp:TextBox ID="txtCodeFrom" runat="server"></asp:TextBox></td> 
                            <td style="width : 100px">
                                ถึง</td>  
                            <td style="width : 150px">
                                <asp:TextBox ID="txtCodeTo" runat="server"></asp:TextBox></td> 
                            <td>
                            </td>  
                        </tr>
                       <tr style ="height :25px">
                            <td  style="width : 10px">
                            </td> 
                            <td style="width : 90px">
                                </td>  
                            <td style="width : 150px">
                                </td> 
                            <td style="width : 100px">
                                </td>  
                            <td style="width : 150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                        <tr style ="height :25px">
                            <td  style="width : 10px">
                            </td> 
                            <td style="width : 90px">
                                </td>  
                            <td style="width : 150px">
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click" /></td> 
                            <td style="width : 100px">
                                </td>  
                            <td style="width : 150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                      </table>
                </td>
            </tr> 
        </table>
</asp:Content>

