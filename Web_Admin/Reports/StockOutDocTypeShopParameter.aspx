<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockOutDocTypeShopParameter.aspx.cs" Inherits="Reports_StockOutDocTypeParameter" Title="��§ҹ����͡�͡��á�è���" %>
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
                    &nbsp;��§ҹ����͡�͡��á�è���</td>
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
                                &nbsp;��§ҹ����͡�͡��á�è���</td> 
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
                                ������</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbRequisition" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px" style="height: 25px">
                            </td> 
                            <td width="90px" style="height: 25px">
                                ��Ш��ѹ���</td>  
                            <td width="150px" style="height: 25px">
                                <uc1:DatePickerControl ID="dpFrom" runat="server" />
                            </td> 
                            <td width="100px" style="height: 25px">
                                �֧</td>  
                            <td width="150px" style="height: 25px">
                                <uc1:DatePickerControl ID="dpTo" runat="server" />
                            </td> 
                            <td style="height: 25px">
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                �Ţ����͡���&nbsp;
                            </td>  
                            <td width="150px">
                                <asp:TextBox ID="txtInvcodeFrom" runat="server" CssClass="zTextbox" Width="134px"></asp:TextBox></td> 
                            <td width="100px">
                                �֧</td>  
                            <td width="150px">
                                <asp:TextBox ID="txtInvcodeTo" runat="server" CssClass="zTextbox" Width="134px"></asp:TextBox></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px" style="height: 25px">
                            </td> 
                            <td width="90px" style="height: 25px">
                                �����١���</td>  
                            <td colspan="3" style="height: 25px">
                                <asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td> 
                            <td style="height: 25px">
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
                                <asp:Button ID="btnReport" runat="server" Text="�ʴ���§ҹ" Width="130px" OnClick="btnReport_Click" /></td> 
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

