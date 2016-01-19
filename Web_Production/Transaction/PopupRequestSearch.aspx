<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PopupRequestSearch.aspx.cs" Inherits="Transaction_PopupRequestSearch" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาเลขที่การผลิต</title>
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
                &nbsp;ค้นหาเลขที่การผลิต</td> 
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
                        <td width="20px"></td>
                        <td style="width: 158px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่การผลิต</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtLotno" runat="server" CssClass="zTextbox" Width="100px"></asp:TextBox>
                            <asp:TextBox ID="txtRef" runat="server" CssClass="zHidden" Width="100px"></asp:TextBox></td>
                        <td></td>
                    </tr> 
                    
                     <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                             ชื่อผลิตภัณฑ์</td>
                        <td colspan="3">
                            <asp:DropDownList ID="cmbProduct" runat="server" Width="320px">
                            </asp:DropDownList></td>
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
                <asp:GridView ID="grvRequisition" runat="server" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="600px" OnRowDataBound="grvRequisition_RowDataBound">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                 <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_Submit.gif" />
                            </ItemTemplate>
                        </asp:TemplateField>
 
                       <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                        <asp:BoundField DataField="PRODUCT">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>  
                         <asp:BoundField SortExpression="LOTNO" HeaderText = "เลขที่การผลิต" DataField="LOTNO">
                            <ItemStyle Width="100px" HorizontalAlign="center"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="BARCODE" HeaderText = "บาร์โค้ด" DataField="BARCODE">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField> 
                        <asp:BoundField SortExpression="PRODUCTNAME" HeaderText = "ชื่อผลิตภัณฑ์" DataField="PRODUCTNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
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
                                <asp:Button ID="btnClose" runat="server" CssClass="zButton" Text="ปิดหน้าต่าง" Width="80px" />&nbsp;</td> 
                        </tr>
                    </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
