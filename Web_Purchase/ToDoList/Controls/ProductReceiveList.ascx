<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ProductReceiveList.ascx.cs" Inherits="ToDoList_Controls_ProductReceiveList" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>

<table border="0" width="800px" cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <table border="0" cellpadding="0" cellspacing="0" style="border-right: #cbdaa9 1px solid;
                border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid"
                width="900">
                <tr>
                    <td class="subheadertext" colspan="6">
                        &nbsp;ค้นหา</td>
                </tr>
                <tr style="height:10">
                    <td width="50">
                    </td>
                    <td width="150">
                    </td>
                    <td width="150">
                    </td>
                    <td width="20">
                    </td>
                    <td style="width: 170px">
                    </td>
                    <td width="360">
                    </td>
                </tr>
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        เลขที่ใบสั่งซื้อ</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        วันที่สั่งซื้อ</td>
                    <td width="150">
                        <uc1:DatePickerControl ID="dtpDateFrom" runat="server" />
                    </td>
                    <td align="center" width="20">
                        ถึง
                    </td>
                    <td style="width: 170px">
                        <uc1:DatePickerControl ID="dtpDateTo" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>                
                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        สินค้าที่สั่งซื้อ</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>

                <tr style="height:25">
                    <td width="50">
                    </td>
                    <td width="150">
                        ผู้จำหน่าย</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbSupplier" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr style="height:10">
                    <td width="50">
                    </td>
                    <td width="150">
                    </td>
                    <td width="150">
                    </td>
                    <td width="20">
                    </td>
                    <td style="width: 170px">
                    </td>
                    <td>
                    </td>
                </tr>
            </table>        
        </td> 
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grvProductReceive" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="900px" OnRowDataBound="grvProductReceive_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="POLOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DUEDATE" HeaderText="กำหนดส่ง" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="DUEDATE">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:HyperLinkField DataNavigateUrlFields="POLOID" DataNavigateUrlFormatString="../../Transaction/PurchaseOrder.aspx?loid={0}"
                        DataTextField="POCODE" HeaderText="เลขที่ใบสั่งซื้อ" DataTextFormatString="{0}" >
                        <ItemStyle Width="110px" HorizontalAlign="center"/>
                        <HeaderStyle Width="110px" /> 
                    </asp:HyperLinkField>                    
                    <asp:BoundField DataField="ORDERDATE" HeaderText="วันที่ขอซื้อ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="ORDERDATE">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUPPLIERNAME" HeaderText="ผู้จำหน่าย" SortExpression="SUPPLIERNAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>                        
                    <asp:BoundField DataField="PRODUCTNAME" HeaderText="ชื่อสินค้า" SortExpression="PRODUCTNAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QTY" HeaderText="จำนวนสั่งซื้อ" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="QTY">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="RECEIVEQTY" HeaderText="จำนวนรับ" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="RECEIVEQTY">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="UNITNAME" HeaderText="หน่วย" SortExpression="UNITNAME">
                        <ItemStyle Width="70px" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STATUSPRODUCT" HeaderText="สถานะจัดซื้อ" SortExpression="STATUSPRODUCT">
                        <ItemStyle Width="70px" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>