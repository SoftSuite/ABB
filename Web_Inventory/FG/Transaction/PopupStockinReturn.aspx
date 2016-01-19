<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupStockinReturn.aspx.cs" Inherits="FG_Transaction_PopupStockinReturn" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>เลือกประเภทการรับคืน</title>
    <link href="../../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
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
                &nbsp;เลือกประเภทการรับคืน</td> 
        </tr>  <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="600px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td style="width: 153px"></td>
                        <td style="width: 14px"></td>
                        <td style="width: 158px"></td>
                        <td></td>
                    </tr>
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ประเภทเอกสาร</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbRefType" runat="server" CssClass="zCombobox" Enabled="false" Width="305px"></asp:DropDownList>
                        </td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อลูกค้า</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox" Width="298px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                            เลขที่</td>  
                            <td style="width: 153px">
                                <asp:TextBox ID="txtCodeFrom" runat="server" CssClass="zTextbox" Width="130px"></asp:TextBox></td>
                            <td style="width: 14px">
                                ถึง</td>                              
                            <td style="width: 158px">
                                <asp:TextBox ID="txtCodeTo" runat="server" CssClass="zTextbox" Width="130px"></asp:TextBox></td>  
                        </tr>
                    <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                วันที่</td>  
                            <td style="width: 153px">
                                <uc1:DatePickerControl ID="ctlDateFrom" runat="server" />
                            </td>
                            <td style="width: 14px">
                                ถึง</td>                              
                            <td style="width: 158px">
                                <uc1:DatePickerControl ID="ctlDateTo" runat="server" />
                            </td> 
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
                <asp:GridView ID="grvStockinReturn" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="500px" OnRowDataBound="grvStickinReturn_RowDataBound">
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
                         <asp:TemplateField HeaderText="ลำดับ">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                        <asp:BoundField DataField="REFLOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CUSTOMERLOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField> 
                         <asp:BoundField SortExpression="DOCCODE" HeaderText = "เลขที่" DataField="DOCCODE">
                            <ItemStyle Width="100px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="DOCDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่ผลิต" DataField="DOCDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "ลูกค้า" DataField="CUSTOMERNAME">
                            <ItemStyle Width="200px"/>
                            <HeaderStyle Width="200px" />  
                        </asp:BoundField> 
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView></td> 
        </tr> 
       
           
                    </table>
                </td>
            </tr>
        </table>
        
                    <table border="0" cellpadding="0" cellspacing="0" width="600px">
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnClose" runat="server" CssClass="zButton" Text="ปิดหน้าต่าง" Width="80px" />
                            </td> 
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>

