<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PurchaseOrderParameter.aspx.cs" Inherits="Reports_PurchaseOrderParameter" Title="��§ҹ���觫���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
       <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height:5px">
                <td style ="width :5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style ="height:25px">
                <td style ="width :5px">
                </td>
                <td class="headtext">
                    &nbsp;��§ҹ���觫���</td>
            </tr> 
            <tr style ="height:10px">
                <td style ="width :5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td style ="width :5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="500px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" style ="height :20px">
                                &nbsp;��§ҹ���觫���</td> 
                        </tr>
                        <tr style ="height:5px">
                            <td style ="width :10px">
                            </td> 
                            <td style="width :103px">
                            </td>  
                            <td style ="width :150px">
                            </td> 
                            <td style="width :80px">
                            </td>  
                            <td style ="width :150px">
                            </td> 
                            <td style ="width :40px">
                            </td>  
                        </tr>
                        <tr style ="height:25px">
                            <td style ="width :10px">
                            </td> 
                            <td style="width :103px">
                                ��Ш��ѹ���</td>  
                            <td style ="width :150px">
                                <uc1:DatePickerControl ID="dpFrom" runat="server" />
                            </td> 
                            <td style="width :80px">
                                �֧</td>  
                            <td style ="width :150px">
                                <uc1:DatePickerControl ID="dpTo" runat="server" />
                            </td> 
                            <td>
                            </td>  
                        </tr>
                         <tr style ="height:25px">
                            <td style ="width :10px">
                            </td> 
                            <td style="width :103px">
                                �Ţ������觫���&nbsp;
                            </td>  
                            <td style ="width :150px">
                                <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="122px"></asp:TextBox></td> 
                            <td style="width :80px">
                                �֧</td>  
                            <td style ="width :150px">
                                <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="122px"></asp:TextBox></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr style ="height:25px">
                            <td style ="width :10px">
                            </td> 
                            <td style="width :103px">
                                ����˹���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbSupplier" runat="server" CssClass="zCombobox" Width="356px">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr style ="height:25px">
                            <td style ="width :10px">
                            </td> 
                            <td style="width :103px">
                                </td>  
                            <td style ="width :150px">
                                </td> 
                            <td style="width :80px">
                                </td>  
                            <td  style ="width :150px">
                                </td> 
                            <td>
                            </td>  
                        </tr>
                         <tr style ="height:25px">
                            <td style ="width :10px">
                            </td> 
                            <td style="width :103px">
                                </td>  
                            <td style ="width :150px">
                                <asp:Button ID="btnReport" runat="server" Text="�ʴ���§ҹ" Width="130px" OnClick="btnReport_Click" /></td> 
                            <td style="width :80px">
                                </td>  
                            <td style ="width :150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                    </table>
                </td>
            </tr> 
        </table>
</asp:Content>

