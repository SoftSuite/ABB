<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockcheckParameter.aspx.cs" Inherits="Reports_StockcheckParameter" Title="��§ҹ��õ�Ǩ�Ѻ�Թ���" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style ="height :25px" >
                <td style ="width:5px"></td>
                <td class="headtext">&nbsp;��§ҹ��û�Ѻ��ا�ʹ</td>
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
                            <td style="width:73px">
                                Batch No:</td>  
                            <td style ="width:200px">
                                <asp:DropDownList id="cmbBatchNo" runat="server" Width="195px">
                                </asp:DropDownList></td>   
                        </tr>
                         <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td style="width:73px">
                                ��§ҹ</td>  
                            <td style ="width:200px">
                            <asp:RadioButton ID="RdIncrease" runat="server" Text="㺻�Ѻ��ا����" GroupName="radiation" AutoPostBack="True" Checked="True"  />
                            <asp:RadioButton ID="RdDecrease" runat="server" Text="㺻�Ѻ��اŴ" GroupName="radiation" AutoPostBack="True"   />
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

