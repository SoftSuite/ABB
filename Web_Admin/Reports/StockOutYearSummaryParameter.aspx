<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockOutYearSummaryParameter.aspx.cs" Inherits="Reports_StockOutYearSummaryParameter" Title="��§ҹ��è����Թ����͡�ҡ��ѧ" %>
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
                    &nbsp;��§ҹ��è����Թ����͡�ҡ��ѧ</td>
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
                                &nbsp;��§ҹ��è����Թ����͡�ҡ��ѧ</td> 
                        </tr>
                        <tr height="5px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
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
                            <td style="width: 146px">
                                ��Шӻ�</td>  
                            <td width="150px">
                                <asp:DropDownList ID="cmbYear" runat="server" CssClass="zCombobox" Width="101px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td>  
                            <td width="100px"></td>  
                            <td width="150px"></td> 
                            <td></td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
                                �������Թ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
                                ������Թ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
                                �����Թ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                        
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
                                �ʴ��Ҥ�</td>  
                            <td colspan="3">
                            <asp:RadioButton ID="RdCost" runat="server" Text="�Ҥҷع" GroupName="radiation" AutoPostBack="True"  />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="RdPrice" runat="server" Text="�ҤҢ��" GroupName="radiation" AutoPostBack="True"   />


                            <td>
                            </td>  
                        </tr>

                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width: 146px">
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
                            <td style="width: 146px">
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

