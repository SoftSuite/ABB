<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupStockinReturnSearch.aspx.cs" Inherits="Search_PopupStockinReturnSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>เลือกสินค้าที่รับคืน</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
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
                            <asp:TemplateField HeaderText="ลำดับ">
                                <ItemTemplate>
                                    <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                                </ItemTemplate> 
                                <ItemStyle HorizontalAlign="Center"/>
                                <HeaderStyle Width="50px" /> 
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="PDLOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            
                            <asp:BoundField DataField="REFLOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="PDNAME" HeaderText = "ชื่อสินค้า" DataField="PDNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                            </asp:BoundField>
                                                  
                            <asp:BoundField SortExpression="LOTNO" HeaderText = "Lot" DataField="LOTNO">
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="60px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="QTY" HeaderText = "จำนวน" DataField="QTY">
                            <ItemStyle Width="60px" HorizontalAlign="Right"/>
                            <HeaderStyle Width="60px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="UNAME" HeaderText = "หน่วย" DataField="UNAME">
                            <ItemStyle Width="60px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="60px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="DOCCODE" HeaderText = "เลขที่เอกสาร" DataField="DOCCODE">
                            <ItemStyle Width="120px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="120px" />  
                            </asp:BoundField>
                            
                            <asp:BoundField SortExpression="DOCNAME" HeaderText = "ชื่อเอกสาร" DataField="DOCNAME">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
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

