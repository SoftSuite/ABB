<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductPlan.aspx.cs" Inherits="Search_ProductPlan" %>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ค้นหาสินค้า</title>
    <link href="../Template/BaseStyle.css" rel="stylesheet" type="text/css" /> 
    <script language="javascript" src="../Template/BaseScript.js"></script> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" width="545">
            <tr height="5px">
                <td width="5px">
                </td>
                <td width="540px">
                </td>
            </tr> 
            <tr height="25px">
                <td width="5px">
                </td>
                <td class="headtext">
                    &nbsp;ค้นหาสินค้า</td>
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
                                ประเภทสินค้า</td>  
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProductType" runat="server" CssClass="zCombobox" Width="390px" AutoPostBack="True" OnSelectedIndexChanged="cmbProductType_SelectedIndexChanged">
                                </asp:DropDownList></td> 
                            <td>
                            </td>  
                        </tr>
                        <tr height="25">
                            <td width="10">
                            </td>
                            <td width="90">
                                กลุ่มสินค้า</td>
                            <td colspan="3">
                                <asp:DropDownList ID="cmbProductGroup" runat="server" CssClass="zCombobox" Width="390px">
                                </asp:DropDownList></td>
                            <td>
                            </td>
                        </tr>
                        <tr height="25px">
                            <td width="10px">
                            </td> 
                            <td width="90px">
                                ชื่อสินค้า</td>  
                            <td colspan="3">
                                <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="390px"></asp:TextBox></td> 
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
                    <asp:GridView ID="grvProduct" runat="server" Width="540px" AutoGenerateColumns="False" CssClass="t_tablestyle" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowDataBound="grvProduct_RowDataBound">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server" />
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="25px" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkItem" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:BoundField DataField="LOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="BARCODE" HeaderText="บาร์โค้ด">
                                <ItemStyle Width="100px"/>
                                <HeaderStyle Width="100px"/>
                            </asp:BoundField> 
                            <asp:BoundField DataField="NAME" HeaderText="ชื่อสินค้า">
                            </asp:BoundField>
                            <asp:BoundField DataField="PRODUCTGROUP" HeaderText="กลุ่มสินค้า">
                                <ItemStyle Width="140px"/>
                                <HeaderStyle Width="140px"/>
                            </asp:BoundField> 
                            <asp:BoundField DataField="PRODUCTTYPE" HeaderText="ประเภทสินค้า">
                                <ItemStyle Width="140px"/>
                                <HeaderStyle Width="140px"/>
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
                <td align="center">
                    <asp:TextBox ID="txtProduct" runat="server" Width="100px" CssClass="zHidden"></asp:TextBox>
                     <asp:Button ID="btnOK" runat="server" CssClass="zButton" Text="เลือก" Width="80px" />
                    <asp:Button ID="btnClose" runat="server" CssClass="zButton" Text="ปิดหน้าต่าง" Width="80px" />
                </td>
            </tr>
            <tr>
                <td height="20" width="5px">
                </td>
                <td height="20">
                </td>
            </tr>
        </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
