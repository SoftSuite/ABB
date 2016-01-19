<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockRemainParameter.aspx.cs" Inherits="Reports_StockRemainParameter" Title="รายงานสินค้าคงเหลือ" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height :25px" >
                <td style ="width:5px"></td>
                <td class="headtext">&nbsp;รายงานสินค้าคงเหลือ</td>
            </tr> 
            <tr>
                <td style ="width:5px"></td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 564px" >
                        <tr style ="height :5px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px"></td>  
                            <td style ="width:200px"></td> 
                        </tr>
                        <tr style ="height :25px">
                            <td style ="width:10px"></td> 
                            <td style="width:73px">ประเภทสินค้า</td>  
                            <td style ="width:200px">
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="365px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td>   
                        </tr>
                        <tr style ="height :25px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px">กลุ่มสินค้า</td>  
                            <td style ="width:200px">
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="365px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged" >
                                </asp:DropDownList></td> 
                        </tr>
                         <tr style="height:25px">
                            <td style ="width:10px"></td> 
                            <td style="width:73px">ชื่อสินค้า</td>  
                            <td style ="width:200px">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="365px" >
                                </asp:DropDownList></td> 
                        </tr>
                          <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
                                แสดงราคา</td>  
                            <td colspan="3">
                            <asp:RadioButton ID="RdCost" runat="server" Text="ราคาทุน" GroupName="radiation" AutoPostBack="True"  />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RdPrice" runat="server" Text="ราคาขาย" GroupName="radiation" AutoPostBack="True"   />


                            <td>
                            </td>  
                        </tr>
                         <tr style ="height:25px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px"></td>  
                            <td style ="width:200px"></td> 
                        </tr>
                         <tr style="height:25px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px"></td> 
                            <td  style="width:200px">
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" CssClass ="zButton" OnClick="btnReport_Click" > 
                                </asp:Button></td>  
                         </tr>
                     </table>
                 </td>
             </tr> 
    </table>
</asp:Content>

