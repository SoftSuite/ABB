<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupStockoutSearch.aspx.cs" Inherits="Search_PopupStockoutSearch" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>������駤׹�Թ���/�ѵ�شԺ</title>
            <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
   <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr style="height:5px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style="height:25px">
                <td width="5px">
                </td>
                <td class="headtext">
                    &nbsp;������駤׹�Թ���/�ѵ�شԺ</td>
            </tr> 
            <tr style="height:10px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" style="height:20px">
                                &nbsp;����</td> 
                        </tr>
                        <tr style="height:5px">
                            <td colspan="6">
                            </td> 
                        </tr>
                        <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                �Ţ�����駤׹</td>  
                            <td width="180px">
                                <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="155px"></asp:TextBox>
                            </td>
                            <td width="20px">
                                �֧</td>  
                            <td width="180px">
                                <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="155px"></asp:TextBox>
                            </td>  
                        </tr>
                        <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                �ѹ�����駤׹</td>  
                            <td width="180px">
                                <uc2:DatePickerControl ID="ctlDateFrom" runat="server" Enabled="true" />
                            </td>
                            <td width="20px">
                                �֧</td>                              
                            <td width="180px">
                                <uc2:DatePickerControl ID="ctlDateTo" runat="server" Enabled="true" />
                            </td>  
                        </tr>
                         
                         <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                �����Թ���/�ѵ�شԺ</td>  
                            <td colspan="3">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="355px"></asp:TextBox>
                            </td> 
                            <td>
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                            </td>  
                        </tr>                            
                        </tr>                        
                        <tr style="height:5px">
                            <td colspan="6">
                            </td> 
                        </tr>
                    </table>
                </td>
            </tr> 
            <tr>
                <td style="height:20" width="5px">
                </td>
                <td style="height:20">
                </td>
            </tr>
            <tr>
                <td width="5px">
                </td>
                <td>
                    <asp:GridView ID="grvReserve" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" OnRowDataBound="grvReserve_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="True" CommandName="Select" ImageUrl="~/Images/icn_Submit.gif" />
                                </ItemTemplate> 
                                <ItemStyle Width="20px" HorizontalAlign="Center" />
                                <HeaderStyle Width="20px" HorizontalAlign="Center" />
                            </asp:TemplateField> 
                            
                            <asp:BoundField DataField="LOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="UNIT">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>                            
    
                            <asp:BoundField SortExpression="CODE" HeaderText = "�Ţ�����駤׹" DataField="CODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>                                                         
                           
                            <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "�����Թ���" DataField="PRODUCTNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="QTY" HeaderText = "�ӹǹ" DataField="QTY">
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="UNITNAME" HeaderText = "�ӹǹ" DataField="UNITNAME">
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px" />  
                            </asp:BoundField>                                                                           
                            
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" /> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height:20" width="5px">
                </td>
                <td style="height:20">
                </td>
            </tr>
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540px">
                        <tr>
                            <td align="center">
                                 <asp:Button ID="btnClose" runat="server" CssClass="zButton" Text="�Դ˹�ҵ�ҧ" Width="80px" />
                            </td> 
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>