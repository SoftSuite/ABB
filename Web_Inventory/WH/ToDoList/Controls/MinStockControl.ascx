<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MinStockControl.ascx.cs" Inherits="WH_ToDoList_Controls_MinStockControl" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc1" %>
<table border="0" width="800px" cellpadding="0" cellspacing="0">
    <tr>
        <td><div style="display:none"><uc1:DatePickerControl ID="dtpDueDate" runat="server" Visible="true" /></div>
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
                    <td style="width: 20px">
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
                        ประเภทการสั่งวัตถุดิบ</td>
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
                        สถานะ</td>
                    <td width="150">
                        <asp:DropDownList ID="cmbStatus" runat="server" CssClass="zComboBox"
                            Width="150px">
                        </asp:DropDownList></td>
                    <td align="center" style="width: 20px">
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
                    <td style="width: 20px">
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
            <asp:LinkButton ID="btnNewNorm" runat="server" CssClass="toolbarbutton" OnClick="btnNewNorm_Click" >LinkButton</asp:LinkButton>
            <uc2:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="สร้างใบสั่งผลิต" OnNewClick="NewClick"/>
        </td> 
    </tr>
    <tr>
        <td>
            <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="1200px" OnRowDataBound="grvRequisition_RowDataBound">
                <PagerSettings Visible="False" />
                <Columns>
                    <asp:BoundField DataField="LOID">
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
                    <asp:BoundField DataField="BARCODE" HeaderText="รหัสวัตถุดิบ" SortExpression="BARCODE">
                        <ItemStyle Width="100px" />
                        <HeaderStyle Width="100px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NAME" HeaderText="ชื่อวัตถุดิบ" SortExpression="NAME">
                        <ItemStyle Width="220px" />
                        <HeaderStyle Width="220px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="QTY" HeaderText="คงเหลือ" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="QTY">
                        <ItemStyle Width="110px" HorizontalAlign="right" Font-Bold=true ForeColor=Red/>
                        <HeaderStyle Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="MINIMUM" HeaderText="ปริมาณต่ำสุด" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="MINIMUM">
                        <ItemStyle Width="110px" HorizontalAlign="right"/>
                        <HeaderStyle Width="110px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="UNITNAME" HeaderText="หน่วยนับ" SortExpression="UNITNAME">
                        <ItemStyle Width="70px" HorizontalAlign="Center"/>
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="ORDERTYPENAME" HeaderText="ประเภทการสั่ง" SortExpression="ORDERTYPENAME">
                        <ItemStyle Width="50px" HorizontalAlign="Center"/>
                        <HeaderStyle Width="50px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LEADTIME" HeaderText="ระยะเวลา<br>ผลิต/สั่งซื้อ (วัน)" DataFormatString="{0:#,##0}" HtmlEncode="false" SortExpression="LEADTIME">
                        <ItemStyle Width="80px" HorizontalAlign="center" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LOTSIZE" HeaderText="จำนวนต่อ Lot" DataFormatString="{0:#,##0.00}" HtmlEncode="false" SortExpression="LOTSIZE">
                        <ItemStyle Width="70px" HorizontalAlign="right" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="เลขที่ขอ<br>จัดซื้อ/สั่งผลิต" SortExpression="">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkRequisition" Text="" runat="server" Target="_blank"></asp:HyperLink> 
                        </ItemTemplate>
                        <ItemStyle Width="120px" Height="25px" HorizontalAlign="Center"/>
                        <HeaderStyle Width="120px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="STATUS" HeaderText="สถานะ" SortExpression="STATUS">
                        <ItemStyle Width="100px" HorizontalAlign="Center"/>
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
                </Columns>
                <HeaderStyle CssClass="t_headtext" />
                <AlternatingRowStyle CssClass="t_alt_bg" />
            </asp:GridView>
        </td> 
    </tr>
</table>
