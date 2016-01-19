<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="SaleSummaryBillParameter.aspx.cs" Inherits="Reports_SaleSummaryBillParameter" Title="รายงานสรุปยอดขายตามบิล" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
       <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr height="5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr height="25px">
                <td width="5px">
                </td>
                <td class="headtext">
                    &nbsp;รายงานสรุปยอดขายตามบิล</td>
            </tr> 
            <tr height="10px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="500px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" height="20px">
                                &nbsp;รายงานสรุปยอดขายตามบิล</td> 
                        </tr>
                        <tr height="5px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td width="100px">
                            </td>  
                            <td width="150px">
                            </td> 
                            <td width="40px">
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                ประจำวันที่</td>  
                            <td width="150px">
                                <uc1:DatePickerControl ID="dpFrom" runat="server" />
                            </td> 
                            <td width="100px">
                                ถึง</td>  
                            <td width="150px">
                                <uc1:DatePickerControl ID="dpTo" runat="server" />
                            </td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                เลขที่เอกสาร&nbsp;
                            </td>  
                            <td width="150px">
                                <asp:TextBox ID="txtInvcodeFrom" runat="server" CssClass="zTextbox" Width="134px"></asp:TextBox></td> 
                            <td width="100px">
                                ถึง</td>  
                            <td width="150px">
                                <asp:TextBox ID="txtInvcodeTo" runat="server" CssClass="zTextbox" Width="134px"></asp:TextBox></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                ชื่อลูกค้า</td>  
                            <td colspan="3">
                                <asp:TextBox id="txtCustomer" runat="server" CssClass="zTextbox" Width="349px">
                                </asp:TextBox></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px" id="trCustomer" runat="server">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                ชื่อลูกค้า</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                </td>  
                            <td width="150px">
                                </td> 
                            <td width="100px">
                                </td>  
                            <td width="150px">
                                </td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                </td>  
                            <td width="150px">
                                <asp:Button ID="btnReport" runat="server" Text="แสดงรายงาน" Width="130px" OnClick="btnReport_Click" /></td> 
                            <td width="100px">
                                </td>  
                            <td width="150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                    </table>
                </td>
            </tr> 
        </table>
</asp:Content>

