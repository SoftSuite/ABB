<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductStockinParameter.aspx.cs" Inherits="Reports_ProductStockinParameter" Title="รายงานสินค้าที่รับ" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
<table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height : 5px">
                <td style="width: 5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style ="height : 25px">
                <td style="width: 5px; height: 27px;">
                </td>
                <td class="headtext" style="height: 27px">
                    &nbsp;รายงานสินค้าที่รับ</td>
            </tr> 
            <tr style ="height : 10px">
                <td style="width: 5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="500px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" style ="height : 20px">
                                &nbsp;รายงานสินค้าที่รับ</td> 
                        </tr>
                        <tr style ="height : 5px">
                            <td style="width: 10px">
                            </td> 
                            <td style="width: 129px">
                            </td>  
                            <td style="width: 152px">
                            </td> 
                            <td style="width: 80px">
                            </td>  
                            <td style="width: 150px">
                            </td> 
                            <td style="width: 40px">
                            </td>  
                        </tr>
                        <tr>
                          <tr style ="height : 25px">
                            <td style="width: 10px">
                            </td> 
                            <td style="width: 129px">
                                ประเภทเอกสาร</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbDocType" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                        <tr style ="height : 25px">
                            <td style="width: 10px">
                            </td> 
                            <td style="width: 129px">
                                ประจำวันที่</td>  
                            <td style="width: 152px">
                                <uc1:DatePickerControl ID="dpFrom" runat="server" />
                            </td> 
                            <td style="width: 80px">
                                ถึง</td>  
                            <td style="width: 150px">
                                <uc1:DatePickerControl ID="dpTo" runat="server" />
                                </td> 
                            <td>
                            </td>  
                        </tr>
                        <tr style ="height : 25px">
                            <td style="width: 10px">
                            </td> 
                            <td style="width: 129px">
                                ประเภทสินค้า</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                        <tr style ="height : 25px">
                            <td style="width: 10px">
                            </td> 
                            <td style="width: 129px">
                                กลุ่มสินค้า</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr style ="height : 25px">
                            <td style="width: 10px; height: 25px;">
                            </td> 
                            <td style="width: 129px; height: 25px;">
                                ชื่อสินค้า</td>  
                            <td colspan="3" style="height: 25px">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True">
                                </asp:DropDownList></td> 
                            <td style="height: 25px">
                            </td>  
                        </tr>
                         <tr style ="height : 25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 129px">
                                </td>  
                            <td style="width: 152px">
                                </td> 
                            <td style="width: 80px">
                                </td>  
                            <td style="width: 150px">
                                </td> 
                            <td>
                            </td>  
                        </tr>
                         <tr style ="height : 25px">
                            <td style="width: 10px">
                            </td> 
                            <td style="width: 129px">
                                </td>  
                            <td style="width: 152px">
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click" /></td> 
                            <td style="width: 80px">
                                </td>  
                            <td style="width: 150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                    </table>
                </td>
            </tr> 
        </table>
</asp:Content>

