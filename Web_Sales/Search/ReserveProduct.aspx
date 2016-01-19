<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReserveProduct.aspx.cs" Inherits="Search_ReserveProduct" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>ค้นหาใบรับคำสั่งซื้อ/สั่งจอง</title>
        <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
            <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
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
                    &nbsp;ค้นหาใบรับคำสั่งซื้อ/สั่งจอง</td>
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
                                &nbsp;ค้นหา</td> 
                        </tr>
                        <tr height="5px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                            </td>  
                            <td width="300px">
                            </td> 
                            <td >
                            </td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                ประเภท</td>  
                            <td width="300px">
                                <asp:DropDownList ID="cmbRequisitionType" runat="server" CssClass="zCombobox" Width="250px" OnSelectedIndexChanged="cmbRequisitionType_SelectedIndexChanged" AutoPostBack="True">
                                </asp:DropDownList></td> 
                            
                            <td> <asp:TextBox ID="txtRefNo" runat="server" CssClass="zHidden" Width="40px"></asp:TextBox>
                                <asp:TextBox ID="txtPopup" runat="server" CssClass="zHidden" Width="40px"></asp:TextBox></td>  
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                                ชื่อลูกค้า</td>  
                            <td width="300px"><asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zCombobox" Width="250px" OnSelectedIndexChanged="cmbCustomer_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList></td> 
                            
                            <td>
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>  
                        </tr>
                        <tr height="5px">
                            <td width="10px">
                            </td> 
                            <td width="120px">
                            </td>  
                            <td width="300px">
                            </td> 
                           
                            <td>
                            </td>  
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
                    <asp:GridView ID="grvReserve" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="PDLOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvReserve_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" OnCheckedChanged="chk_CheckChanged" AutoPostBack="True" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" OnCheckedChanged="chk_CheckChanged" AutoPostBack="True" />
                            </ItemTemplate>
                        </asp:TemplateField>

                           <asp:BoundField SortExpression="NAME" HeaderText = "ชื่อสินค้า" DataField="NAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="QTY" HeaderText = "จำนวน" DataField="QTY">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="UNAME" HeaderText = "หน่วย" DataField="UNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                         <asp:BoundField SortExpression="CUSTOMERNAME" HeaderText = "ลูกค้า" DataField="CUSTOMERNAME">
                            <ItemStyle Width="140px"/>
                            <HeaderStyle Width="140px" />  
                        </asp:BoundField> 
                        
                        <asp:BoundField DataField="PDLOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
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
                                 <asp:Button ID="btnSelect" runat="server" CssClass="zButton" Text="เลือก" Width="80px" />
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
