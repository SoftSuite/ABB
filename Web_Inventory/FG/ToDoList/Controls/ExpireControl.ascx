<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExpireControl.ascx.cs" Inherits="FG_ToDoList_Controls_ExpireControl" %>
<%@ Register Src="../../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc1" %>
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
                <tr height="10">
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
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        ชื่อสินค้า</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr height="10">
                    <td width="50">
                    </td>
                    <td width="150">
                        ระยะเวลา</td>
                    <td width="150">
                        <asp:TextBox ID="txtTime" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                    <td width="20">
                    </td>
                    <td style="width: 170px">
                        เดือน</td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                </tr>
                <tr height="10">
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
        <td class="toolbarplace">
        </td> 
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="900px" OnRowDataBound="grvRequisition_RowDataBound" OnRowCommand="grvRequisition_RowCommand">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="LOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="70px" /> 
                    </asp:TemplateField> 
                    <asp:BoundField DataField="BARCODE" HeaderText="รหัสสินค้า" SortExpression="BARCODE">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="PRODUCTNAME" HeaderText="ชื่อสินค้า" SortExpression="PRODUCTNAME">
                        <ItemStyle Width="180px" />
                        <HeaderStyle Width="180px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOTNO" HeaderText="Lot No" SortExpression="LOTNO">
                        <ItemStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QTY" HeaderText="คงเหลือ" SortExpression="QTY">
                        <ItemStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNITNAME" HeaderText="หน่วยนับ" SortExpression="UNITNAME">
                        <ItemStyle Width="80px" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EXPDATE" HeaderText="วันที่หมดอายุ" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="EXPDATE">
                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>