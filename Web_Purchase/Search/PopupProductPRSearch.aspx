<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupProductPRSearch.aspx.cs" Inherits="Search_PopupProductPRSearch" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>�����Թ����㺢ͫ���</title>
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
                    &nbsp;�����Թ����㺢ͫ���</td>
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
                                �Ţ���㺢ͫ���</td>  
                            <td width="180px">
                                <asp:TextBox ID="txtFromPRCode" runat="server" CssClass="zTextbox" Width="155px"></asp:TextBox>
                            </td>
                            <td width="20px">
                                �֧</td>  
                            <td width="180px">
                                <asp:TextBox ID="txtToPRCode" runat="server" CssClass="zTextbox" Width="155px"></asp:TextBox>
                            </td>  
                        </tr>
                        <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                ��˹���</td>  
                            <td width="180px">
                                <uc2:DatePickerControl ID="ctlFromDueDate" runat="server" Enabled="true" />
                            </td>
                            <td width="20px">
                                �֧</td>                              
                            <td width="180px">
                                <uc2:DatePickerControl ID="ctlToDueDate" runat="server" Enabled="true" />
                            </td>  
                        </tr>
                         <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                ��������âͫ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbPurchaseType" runat="server" CssClass="zCombobox" Width="95%"></asp:DropDownList>
                            </td> 
                        </tr>
                         <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                �Թ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="95%"></asp:DropDownList>
                            </td> 
                        </tr>
                         <tr style="height:25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                ���ͫ���</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbDivision" runat="server" CssClass="zCombobox" Width="95%"></asp:DropDownList>
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
                            
                            <asp:BoundField DataField="PCODE">
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
                            
                            <asp:BoundField DataField="ISVAT">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>  
                            
                            <asp:BoundField DataField="PRITEM">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>                                                           
                           
                            <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "�����Թ���" DataField="PDNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="QTY" HeaderText = "�ӹǹ" DataField="QTY">
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="UNITNAME" HeaderText = "˹���" DataField="UNAME">
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="DUEDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "��˹���" DataField="DUEDATE" >
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="PRCODE" HeaderText = "�Ţ���㺢ͫ���" DataField="PRCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="OLDPRICE" HeaderText = "�Ҥ����" DataField="OLDPRICE">
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="CURPRICE" HeaderText = "�ҤһѨ�غѹ" DataField="CURPRICE">
                            <ItemStyle Width="50px"/>
                            <HeaderStyle Width="50px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="MINPRICE" HeaderText = "�Ҥҵ���ش" DataField="MINPRICE">
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
