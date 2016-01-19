<%@ Page Language="C#"   MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockoutOtherSearch.aspx.cs" Inherits="WH_Transaction_StockoutSearch" Title="ใบเบิกวัสดุอื่นๆ" %>

<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;ใบเบิกวัสดุอื่นๆ</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="true" NameBtnSubmit="อนุมัติ"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" style="border-right: #cbdaa9 1px solid; border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td style="width: 243px"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ชื่อหน่วยงาน</td>
                        <td colspan="3"><asp:DropDownList ID="cmbDivision" runat="server" CssClass="zCombobox"  Width="322px">
                        </asp:DropDownList></td>
                        <td style="width: 243px"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            ผู้ขอเบิก</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtCreateby" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td style="width: 243px"></td>
                    </tr> 

                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            เลขที่เบิก</td>
                        <td colspan="3">
                            <asp:TextBox ID="txtStockCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                        <td style="width: 243px"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            วันที่เบิก</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlApproveDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlApproveDateTo" runat="server" /></td>
                        <td style="width: 243px"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td width="150px">
                            สถานะ</td>
                        <td width="150px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="150px">
                        </asp:DropDownList></td>
                        <td style="width: 243px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td width="150px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td style="width: 243px"></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvRequisition_RowCommand" OnRowDataBound="grvRequisition_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
<asp:TemplateField><HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            
</HeaderTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="25px"></HeaderStyle>
<ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ShowHeader="False">
<ItemStyle Width="60px"></ItemStyle>

<HeaderStyle Width="60px"></HeaderStyle>
<ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="พิมพ์"
                                    ImageUrl="~/Images/icn_print.gif"/>
                            
</ItemTemplate>

<FooterStyle Width="60px"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ลำดับ">
<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                            
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:HyperLinkField HeaderText="เลขที่ใบเบิก" DataTextField="STOCKOUTCODE" DataTextFormatString="{0}" DataNavigateUrlFormatString="StockoutOther.aspx?loid={0}" DataNavigateUrlFields="LOID">
<ItemStyle Width="120px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="120px"></HeaderStyle>
</asp:HyperLinkField>
<asp:BoundField HtmlEncode="False" DataFormatString="{0:dd/MM/yyyy}" DataField="CREATEON" SortExpression="CREATEON" HeaderText="วันที่เบิก">
<ItemStyle Width="100px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="100px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="DIVISIONNAME" SortExpression="DIVISIONNAME" HeaderText="หน่วยงานที่ขอเบิก">
<ItemStyle Width="120px"></ItemStyle>

<HeaderStyle Width="120px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="SUPPORTREFCODE" SortExpression="SUPPORTREFCODE" HeaderText="อ้างอิง">
<ItemStyle Width="100px"></ItemStyle>

<HeaderStyle Width="100px"></HeaderStyle>
</asp:BoundField>
<asp:BoundField DataField="STATUSNAME" SortExpression="STATUSNAME" HeaderText="สถานะ">
<ItemStyle Width="120px"></ItemStyle>

<HeaderStyle Width="120px"></HeaderStyle>
</asp:BoundField>
</Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>
