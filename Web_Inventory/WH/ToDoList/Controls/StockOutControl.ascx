<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockOutControl.ascx.cs" Inherits="WH_ToDoList_Controls_StockOutControl" %>
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
                        ประเภทการเบิกวัตถุดิบ</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbRequisitionType" runat="server" CssClass="zComboBox" Width="320px">
                        </asp:DropDownList></td>
                    <td>
                    </td>
                </tr>
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        ชื่อวัตถุดิบ</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtProductName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        เลขที่เอกสารขอเบิก</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        วันที่ขอเบิก</td>
                    <td colspan="3" width="150">
                        <uc1:DatePickerControl ID="dtpReqDate" runat="server" />
                    </td>
                    <td>
                    </td>
                </tr>
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        สถานะ</td>
                    <td width="150">
                        <asp:DropDownList ID="cmbStatus" runat="server" CssClass="zComboBox"
                            Width="150px" Enabled="False">
                        </asp:DropDownList></td>
                    <td align="center" width="20">
                    </td>
                    <td style="width: 170px">
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                    </td>
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
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:ImageButton ID="btnNew" runat="server" CausesValidation="False" CommandName="new" AlternateText="สร้างใบเบิกวัตถุดิบ" CommandArgument = ""
                                    ImageUrl="~/Images/icn_new.gif"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                        <HeaderStyle Width="30px" /> 
                    </asp:TemplateField>
                    <asp:BoundField DataField="TYPENAME" HeaderText="ประเภทการเบิก" SortExpression="TYPENAME">
                        <ItemStyle Width="150px" />
                        <HeaderStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CODE" HeaderText="เลขที่ขอเบิก" SortExpression="CODE">
                        <ItemStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REQDATE" HeaderText="วันที่ขอเบิก" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="REQDATE">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SENDERNAME" HeaderText="ผู้เบิกวัตถุดิบ" SortExpression="SENDERNAME">
                        <ItemStyle Width="225px" />
                        <HeaderStyle Width="225px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STATUS" HeaderText="สถานะ" SortExpression="STATUS">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="เลขที่เบิก" SortExpression="">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkStockOut" Text="" runat="server"></asp:HyperLink> 
                        </ItemTemplate>
                        <ItemStyle Width="120px" Height="25px"/>
                        <HeaderStyle Width="120px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CREATEON" HeaderText="วันที่เบิก" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="CREATEON">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SENDER">
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