<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequestSearch.aspx.cs" Inherits="Search_RequestSearch" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาใบขอเบิกสินค้า</title>
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
                <td style="width: 5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr height="25px">
                <td style="width: 5px">
                </td>
                <td class="headtext">
                    &nbsp;ค้นหาใบขอเบิกสินค้า</td>
            </tr> 
            <tr height="20px">
                <td style="width: 5px">
                </td>
                <td>
                </td>
            </tr> 
            <tr>
                <td style="width: 5px"> &nbsp;
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540" style="border-right: #cbdaa9 1px solid; border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid">
                        <tr class="subheadertext">
                            <td colspan="6" height="20px">
                                &nbsp;ค้นหา</td> 
                        </tr>
                        <tr height="5px">
                            <td width="10" style="height: 5px">                            </td> 
                            <td width="120" style="height: 5px">                            </td>  
                            <td style="width: 182px; height: 5px;">                            </td> 

                            <td style="width: 24px; height: 5px;" >                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10">                            </td> 
                            <td width="120">
                                เลขที่ขอเบิก</td>  
                            <td style="width: 182px">
                                <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="95px"></asp:TextBox></td>
                                  <td style="width: 24px">ถึง</td>
                                  <td style="width: 182px">
                                <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="96px"></asp:TextBox></td>
                            
                            <td style="width: 120px"> &nbsp;</td>  
                        </tr>
                        <tr height="25px">
                            <td width="10">                            </td> 
                            <td width="120">
                                วันที่ขอเบิก</td>  
                            <td style="width: 182px">
                                <uc1:DatePickerControl ID="ctlDateFrom" runat="server" />                            </td>
                             <td style="width: 24px">ถึง</td>
  
                            <td style="width: 182px">
                                <uc1:DatePickerControl ID="ctlDateTo" runat="server" />                            </td>
                            <td style="width: 120px">                            </td>  
                        </tr>
                         <tr height="25px">
                            <td width="10">                            </td> 
                            <td width="120">
                                รหัสลูกค้า</td>  
                            <td colspan="3" style="width: 182px">
                                <asp:TextBox ID="txtCustCode" runat="server" CssClass="zTextbox" Width="306px"></asp:TextBox></td> 
                            <td style="width: 120px">                                </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10">                            </td> 
                            <td width="120">
                                ชื่อลูกค้า</td>  
                            <td colspan="3" style="width: 182px">
                                <asp:TextBox ID="txtCustName" runat="server" CssClass="zTextbox" Width="306px"></asp:TextBox></td> 
                          <td style="width: 120px"><span style="width: 182px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />                              
                          </span>                          </td>  
                        </tr>
                        <tr height="5px">
                            <td width="10">                            </td> 
                            <td width="120">                            </td>  
                            <td style="width: 182px">                            <asp:TextBox ID="txtRefNo" runat="server" CssClass="zHidden" Width="75px"></asp:TextBox></td> 
                           
                            <td style="width: 24px">                            </td>  
                            <td style="width: 24px">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr> 
            <tr>
                <td height="20" style="width: 5px">
                </td>
                <td height="20">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <asp:GridView ID="grvReserve" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvReserve_RowDataBound">
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
                           <asp:BoundField SortExpression="RTNAME" HeaderText = "ประเภทขอเบิก" DataField="RTNAME">
                            <ItemStyle Width="160px"/>
                            <HeaderStyle Width="160px" />  
                        </asp:BoundField>
                           <asp:BoundField SortExpression="CODE" HeaderText = "เลขที่ขอเบิก" DataField="CODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="REQDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่ขอเบิก" DataField="REQDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                         <asp:BoundField SortExpression="NAME" HeaderText = "ลูกค้า" DataField="NAME">
                        </asp:BoundField>  
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" /> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                        </asp:GridView>
                </td>
            </tr>
            <tr>
                <td height="20" style="width: 5px">
                </td>
                <td height="20">
                </td>
            </tr>
            <tr>
                <td style="width: 5px">
                </td>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" width="540px">
                        <tr>
                            <td align="center">
                                 <asp:Button ID="btnSelect" runat="server" CssClass="zButton" Text="เลือก" Width="80px" Visible="False" />
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
