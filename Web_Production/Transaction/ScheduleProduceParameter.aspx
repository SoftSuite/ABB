<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ScheduleProduceParameter.aspx.cs" Inherits="Transaction_ScheduleProduceParameter"  Title="���ҧ��ü�Ե" %>

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
                                ��͹</td>  
                            <td width="150px">
                                <asp:DropDownList ID="cmbMonth" runat="server" Width="122px">
                                    <asp:ListItem Value="1">���Ҥ�</asp:ListItem>
                                    <asp:ListItem Value="2">����Ҿѹ��</asp:ListItem>
                                    <asp:ListItem Value="3">�չҤ�</asp:ListItem>
                                    <asp:ListItem Value="4">����¹</asp:ListItem>
                                    <asp:ListItem Value="5">����Ҥ�</asp:ListItem>
                                    <asp:ListItem Value="6">�Զع�¹</asp:ListItem>
                                    <asp:ListItem Value="7">�á�Ҥ�</asp:ListItem>
                                    <asp:ListItem Value="8">�ԧ�Ҥ�</asp:ListItem>
                                    <asp:ListItem Value="9">�ѹ��¹</asp:ListItem>
                                    <asp:ListItem Value="10">���Ҥ�</asp:ListItem>
                                    <asp:ListItem Value="11">��Ȩԡ�¹</asp:ListItem>
                                    <asp:ListItem Value="12">�ѹ�Ҥ�</asp:ListItem>
                                </asp:DropDownList>&nbsp;</td> 
                           <td width="100px">�� �.�.
                            </td>  
                            <td width="150px">
                                <asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="59px"></asp:TextBox>
                            </td> 
                            <td width="40px">
                            </td>   
                        </tr>
                                                <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                �����Թ���</td>  
                            <td colspan="3">
                                <asp:TextBox ID="txtProduct" runat="server" Width="263px"></asp:TextBox></td> 
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
