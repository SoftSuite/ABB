<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupStockinReturnWH.aspx.cs" Inherits="Search_PopupStockinReturnWH" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาใบแจ้งคืนวัตถุดิบ</title>
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
                &nbsp;ค้นหาใบแจ้งคืนวัตถุดิบ</td> 
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
                            รหัสผลิตภัณฑ์</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtBarcode" runat="server" CssClass="zTextbox" Width="298px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อผลิตภัณฑ์</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtPDName" runat="server" CssClass="zTextbox" Width="298px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่การผลิต</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLot" runat="server" CssClass="zTextbox" Width="298px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    
                    <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                วันที่แจ้งคืน</td>  
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
                <asp:GridView ID="grvItem" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvItem_RowDataBound">
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
                            
                            <asp:BoundField SortExpression="LOTNO" HeaderText = "เลขที่การผลิต" DataField="LOTNO">
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="60px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="PRODUCTCODE" HeaderText = "รหัสผลิตภัณฑ์" DataField="PRODUCTCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "ชื่อผลิตภัณฑ์" DataField="PRODUCTNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                                                  
                            <asp:BoundField SortExpression="PDQTY" HeaderText = "จำนวนที่ผลิตได้" DataField="PDQTY">
                            <ItemStyle Width="60px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="60px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="UNITNAME" HeaderText = "หน่วย" DataField="UNITNAME">
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="60px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="REQCODE" HeaderText = "เลขที่แจ้งคืน" DataField="REQCODE">
                            <ItemStyle Width="120px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="120px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="REQDATE" HeaderText = "วันที่แจ้งคืน" DataField="REQDATE">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                            </asp:BoundField>
                            
                        </Columns>
                        <SelectedRowStyle CssClass="t_selectstyle" /> 
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

