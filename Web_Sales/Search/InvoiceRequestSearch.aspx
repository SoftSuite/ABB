<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceRequestSearch.aspx.cs" Inherits="Search_InvoiceRequestSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาใบเสร็จรับเงิน</title>
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
        <tr>
            <td class="headtext">
                &nbsp;ค้นหาใบเสร็จรับเงิน</td> 
        </tr>  <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="500px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่ใบเสร็จ</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtInvcode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            รหัสสมาชิก</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemberCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อสมาชิก</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtMemberName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สินค้า</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtProduct" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td>
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvRequisition" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="500px" OnRowDataBound="grvRequisition_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="imbSelect" runat="server" ImageUrl="~/Images/icn_submit.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                         <asp:BoundField SortExpression="INVCODE" HeaderText = "เลขที่ใบเสร็จ" DataField="INVCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="REQDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่ออกใบเสร็จ" DataField="REQDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "ลูกค้า" DataField="CUSTOMERNAME">
                        </asp:BoundField> 
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
                <asp:TextBox ID="txtCurrentInvoice" runat="server" CssClass="zHidden" Width="56px"></asp:TextBox></td> 
        </tr> 
       
           
                    </table>
                </td>
            </tr>
        </table>
        
                    <table border="0" cellpadding="0" cellspacing="0" width="600px">
                        <tr>
                            <td align="center">
                                &nbsp;</td> 
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
