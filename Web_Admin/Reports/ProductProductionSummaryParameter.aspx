<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductProductionSummaryParameter.aspx.cs" Inherits="Reports_ProductProductionSummaryParameter" Title="��§ҹ��ü�Ե�Թ��� ���¼�Ե" %>
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
                    &nbsp;��§ҹ��ü�Ե�Թ��� ���¼�Ե</td>
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
                                &nbsp;��§ҹ��ü�Ե�Թ��� ���¼�Ե</td> 
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
                                ��Ш��ѹ���</td>  
                            <td width="150px">
                                <uc1:DatePickerControl ID="dpFrom" runat="server" />
                            </td> 
                            <td width="100px">
                                �֧</td>  
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
                                ������Թ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProduceGroup" runat="server" CssClass="zCombobox" Width="356px" AutoPostBack="True">
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

