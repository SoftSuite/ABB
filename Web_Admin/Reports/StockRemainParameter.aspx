<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockRemainParameter.aspx.cs" Inherits="Reports_StockRemainParameter" Title="��§ҹ�Թ��Ҥ������" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height :25px" >
                <td style ="width:5px"></td>
                <td class="headtext">&nbsp;��§ҹ�Թ��Ҥ������</td>
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
                            <td style="width:73px">�������Թ���</td>  
                            <td style ="width:200px">
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="365px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td>   
                        </tr>
                        <tr style ="height :25px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px">������Թ���</td>  
                            <td style ="width:200px">
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="365px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductGroup_SelectedIndexChanged" >
                                </asp:DropDownList></td> 
                        </tr>
                         <tr style="height:25px">
                            <td style ="width:10px"></td> 
                            <td style="width:73px">�����Թ���</td>  
                            <td style ="width:200px">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="365px" >
                                </asp:DropDownList></td> 
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
                         <tr style ="height:25px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px"></td>  
                            <td style ="width:200px"></td> 
                        </tr>
                         <tr style="height:25px">
                            <td style ="width :10px"></td> 
                            <td style="width:73px"></td> 
                            <td  style="width:200px">
                                <asp:Button ID="btnReport" runat="server" Text="�ʴ���§ҹ" CssClass ="zButton" OnClick="btnReport_Click" > 
                                </asp:Button></td>  
                         </tr>
                     </table>
                 </td>
             </tr> 
    </table>
</asp:Content>

