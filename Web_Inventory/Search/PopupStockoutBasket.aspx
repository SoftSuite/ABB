<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupStockoutBasket.aspx.cs" Inherits="Search_PopupStockoutBasket" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>กระเช้า</title>
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
                <td style="width:5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr style="height:25px">
                <td style="width:5px">
                </td>
                <td class="headtext">
                    &nbsp;ค้นหากระเช้า</td>
            </tr> 
            <tr style="height:10px">
                <td style="width:5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td style="width:5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540px" class="searchTable">
                        <tr class="subheadertext">
                            <td colspan="4" style="height:20px">
                                &nbsp;ค้นหา</td> 
                        </tr>
                        <tr style="height:5px">
                            <td colspan="4">
                            </td> 
                        </tr>
                        <tr style="height:25px">
                            <td style="width:10px">
                            </td> 
                            <td style="width:120px">
                                บาร์โค้ด</td>  
                            <td>
                                <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" Width="175px" />
                            </td>
                            <td></td>
                        </tr>
                         
                         <tr style="height:25px">
                            <td style="width:10px">
                            </td> 
                            <td style="width:120px">
                                ชื่อกระเช้า</td>  
                            <td>
                                <asp:TextBox ID="txtBasketName" runat="server" CssClass="zTextbox" Width="95%" />
                            </td> 
                            <td>
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                            </td>  
                        </tr>                            
                        </tr>                        
                        <tr style="height:5px">
                            <td colspan="4">
                            </td> 
                        </tr>
                    </table>
                </td>
            </tr> 
            <tr>
                <td style="height:20; width:5px">
                </td>
                <td style="height:20">
                </td>
            </tr>
            <tr>
                <td style="width:5px">
                </td>
                <td>
                    <asp:GridView ID="grvReserve" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvReserve_RowDataBound">
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

                            <asp:BoundField SortExpression="BARCODE" HeaderText = "บาร์โค้ด" DataField="BARCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                                                  
                            <asp:BoundField SortExpression="NAME" HeaderText = "ชื่อกระเช้า" DataField="NAME">
                            <ItemStyle Width="300px"/>
                            <HeaderStyle Width="300px" />  
                            </asp:BoundField>
                            
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" /> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td style="height:20 ; width:5px">
                </td>
                <td style="height:20">
                </td>
            </tr>
            <tr>
                <td style="width:5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540px">
                        <tr>
                            <td align="center">
                                 <asp:Button ID="btnClose" runat="server" CssClass="zButton" Text="ปิดหน้าต่าง" Width="80px" />
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
