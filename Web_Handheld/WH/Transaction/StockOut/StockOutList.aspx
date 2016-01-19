<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="StockOutList.aspx.cs" Inherits="WH_Transaction_StockOut_StockOutList" Title="เบิกวัตถุดิบ" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="false" BtnEditShow="false" 
                    BtnNewShow="false" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" BtnHelpShow="true" BtnViewShow="false" 
                    OnHelpClick="HelpClick"/>
            </td> 
        </tr>
        <tr>
            <td height="150px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                        DataKeyNames="REQUISITION" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnSelectedIndexChanged="grvData_SelectedIndexChanged"
                        Width="220px">
                        <PagerSettings Visible="False" />
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelect" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_Submit.gif" CommandName="select" AlternateText="เลือกรายการ" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="20px" />
                                <HeaderStyle HorizontalAlign="Center" Width="20px" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="STOCKOUT">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REQCODE" HeaderText="เลขที่ขอเบิก">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REQDATE" DataFormatString="{0 : dd/MM/yyyy}" HtmlEncode="False" HeaderText="วันที่ขอเบิก">
                                <ItemStyle Width="60px" />
                                <HeaderStyle Width="60px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CODE" HeaderText="เลขที่ใบเบิก">
                                <ItemStyle HorizontalAlign="Center" Width="50px" />
                                <HeaderStyle Width="50px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="REQUISITION">
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
                </asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                <table border="0" id="test" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="กลับเมนู" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
                </td> 
        </tr> 
    </table>
</asp:Content>