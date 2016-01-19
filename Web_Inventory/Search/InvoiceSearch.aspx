<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InvoiceSearch.aspx.cs" Inherits="Search_InvoiceSearch" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาใบขอเบิกวัตถุดิบ</title>
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
                    &nbsp;ค้นหาใบเสร็จรับเงิน</td>
            </tr> 
            <tr height="20px">
                <td width="5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td width="5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="640px" style="border-right: #cbdaa9 1px solid; border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid">
                        <tr class="subheadertext">
                            <td colspan="6" height="20px">
                                &nbsp;ค้นหา</td> 
                        </tr>
                        <tr height="5px">
                            <td style="width: 10px">                            </td> 
                            <td style="width: 100px">                            </td>  
                            <td style="width: 150px">                            </td> 

                            <td style="width: 24px" colspan="3">                            </td>  
                        </tr>
                        <tr height="25px">
                            <td style="width: 10px">                            </td> 
                            <td style="width: 100px">
                                ประเภท</td>  
                            <td colspan="3" style="width: 182px">
                                <asp:DropDownList ID="cmbRequisitionType" runat="server" CssClass="zCombobox" Width="220px">
                                </asp:DropDownList></td> 
                          <td style="width: 180px"><span style="width: 182px">
                            </span></td>  
                        </tr>
                        <tr height="25px">
                            <td style="width: 10px">                            </td> 
                            <td style="width: 100px">
                                เลขที่ขอเบิก</td>  
                            <td style="width: 150px">
                                <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="120px"></asp:TextBox></td>
                                  <td style="width: 40px">ถึง</td>
                                  <td style="width: 150px">
                                <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="120px"></asp:TextBox></td>
                            
                            <td style="width: 180px"> </td>  
                        </tr>
                        <tr height="25">
                            <td style="width: 10px">
                            </td>
                            <td style="width: 100px">
                                ลูกค้า</td>
                            <td colspan="3">
                                <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox" Width="312px"></asp:TextBox></td>
                            <td style="width: 180px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                        </tr>

                        <tr height="5px">
                            <td style="width: 10px">                            </td> 
                            <td style="width: 100px">                            </td>  
                            <td style="width: 150px">                            </td> 
                           
                            <td colspan="3">                            </td>  
                        </tr>
                    </table>
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
                    <asp:GridView ID="grvReserve" runat="server" Width="640px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvReserve_RowDataBound">
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
                           <asp:BoundField SortExpression="TYPENAME" HeaderText = "ประเภท" DataField="TYPENAME">
                            <ItemStyle Width="200px"/>
                            <HeaderStyle Width="200px" />  
                        </asp:BoundField>
                         <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "ลูกค้า" DataField="CUSTOMERNAME">
                        </asp:BoundField>  
                           <asp:BoundField SortExpression="INVCODE" HeaderText = "เลขที่ใบเสร็จ" DataField="INVCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
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
                                &nbsp;<asp:Button ID="btnClose" runat="server" CssClass="zButton" Text="ปิดหน้าต่าง" Width="80px" />
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