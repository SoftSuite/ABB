<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SupplierSearch.aspx.cs" Inherits="Search_SupplierSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>���Ҽ���˹���</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
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
                    &nbsp;���Ҽ���˹���</td>
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
                    <table border="0" cellpadding="0" cellspacing="0" width="540px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="6" height="20px">
                                &nbsp;����</td> 
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
                                ���ʼ���˹���</td>  
                            <td width="150px">
                                <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="100px"></asp:TextBox></td> 
                            <td width="100px">
                                <asp:TextBox ID="txtCustomer" runat="server" CssClass="zHidden" Width="50px"></asp:TextBox></td>  
                            <td width="150px">
                            </td> 
                            <td>
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                ���ͼ���˹���</td>  
                            <td colspan="3">
                                <asp:TextBox ID="txtFullName" runat="server" CssClass="zTextbox" Width="390px"></asp:TextBox></td> 
                            <td>
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>  
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
                            <td>
                            </td>  
                        </tr>
                    </table>
                </td>
            </tr> 
            <tr>
                <td height="10" width="5px">
                </td>
                <td height="10">
                </td>
            </tr>
            <tr>
                <td width="5px">
                </td>
                <td>
                    <asp:GridView ID="grvCustomer" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***��辺������***</center>" OnRowDataBound="grvCustomer_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_Submit.gif" />
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
                            <asp:BoundField DataField="CODE" HeaderText="���ʼ���˹���">
                                <ItemStyle Width="150px" HorizontalAlign="CENTER"/>
                                <HeaderStyle Width="150px"/>
                            </asp:BoundField> 
                            <asp:BoundField DataField="SUPPLIERNAME" HeaderText="���ͼ���˹���">
                                <ItemStyle Width="150px"/>
                                <HeaderStyle Width="150px"/>
                            </asp:BoundField>
                             
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" /> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td height="20" width="5px">
                </td>
                <td height="20">
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
