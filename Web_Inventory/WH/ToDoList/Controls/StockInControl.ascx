<%@ Control Language="C#" AutoEventWireup="true" CodeFile="StockInControl.ascx.cs" Inherits="WH_ToDoList_Controls_StockInControl" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc2" %>
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
                        ประเภทการรับวัตถุดิบ</td>
                    <td colspan="3">
                        <asp:DropDownList ID="cmbOrderType" runat="server" CssClass="zComboBox" Width="320px">
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
                        <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        เลขที่เอกสาร</td>
                    <td colspan="3" width="150">
                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="150px"></asp:TextBox></td>
                    <td>
                    </td>
                </tr>
                <tr height="25">
                    <td width="50">
                    </td>
                    <td width="150">
                        วันที่กำหนดส่ง</td>
                    <td colspan="3" width="150">
                        <uc1:DatePickerControl ID="dtpDueDate" runat="server" />
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
                            Width="150px">
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
            <uc2:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="สร้างใบรับวัตถุดิบ" OnNewClick="NewClick"/>
        </td> 
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="900px" OnRowDataBound="grvRequisition_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="LOID">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REQUESTCODE">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkAll" runat="server" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="chkItem" runat="server"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center"/>
                        <HeaderStyle Width="30px" /> 
                    </asp:TemplateField>
                    <asp:BoundField DataField="DUEDATE" HeaderText="กำหนดส่ง" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="DUEDATE">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NAME" HeaderText="ชื่อวัตถุดิบ" SortExpression="NAME">
                        <ItemStyle Width="130px" />
                        <HeaderStyle Width="130px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="เลขที่การผลิต/ใบสั่งซื้อ" SortExpression="">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkRequisition" Text="" runat="server" Target="_blank"></asp:HyperLink> 
                        </ItemTemplate>
                        <ItemStyle Width="100px"/>
                        <HeaderStyle Width="100px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="QTY" HeaderText="จำนวนผลิต/สั่งซื้อ" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="QTY">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="REMAIN" HeaderText="จำนวนค้างรับ" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="REMAIN">
                        <ItemStyle Width="70px" HorizontalAlign="right"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNITNAME" HeaderText="หน่วยนับ" SortExpression="UNITNAME">
                        <ItemStyle Width="70px" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUPPLIERNAME" HeaderText="ผู้ผลิต" SortExpression="SUPPLIERNAME">
                        <ItemStyle Width="80px" Wrap="true" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ORDERTYPENAME" HeaderText="ประเภทการสั่ง" SortExpression="ORDERTYPENAME">
                        <ItemStyle Width="50px" />
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="CODE" HeaderText="เลขที่รับ" SortExpression="CODE">
                        <ItemStyle Width="120px" />
                        <HeaderStyle Width="120px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="STATUS" HeaderText="สถานะ" SortExpression="STATUS">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ORDERTYPE">
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNIT">
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
                    <asp:BoundField DataField="PRICE" DataFormatString="{0:#,##0.00}" HtmlEncode="false" >
                        <ControlStyle CssClass="zHidden" />
                        <ItemStyle CssClass="zHidden" />
                        <HeaderStyle CssClass="zHidden" />
                        <FooterStyle CssClass="zHidden" />
                    </asp:BoundField>
                    <asp:BoundField DataField="SUPPLIER">
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