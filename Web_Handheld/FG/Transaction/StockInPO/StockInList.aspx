<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="StockInList.aspx.cs" Inherits="FG_Transaction_StockInPO_StockInList" Title="รับสินค้าจากผู้จำหน่าย" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="false" BtnEditShow="false" 
                    BtnNewShow="true" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" BtnHelpShow="true" BtnViewShow="false" 
                    OnNewClick="NewClick" OnHelpClick="HelpClick"/>
            </td> 
        </tr>
        <tr>
            <td valign="middle" height="20px">
                สถานะ :
                <asp:DropDownList ID="cmbStatus" runat="server" CssClass="zcombobox" Width="120px" OnSelectedIndexChanged="cmbStatus_SelectedIndexChanged" AutoPostBack="true">
                </asp:DropDownList></td> 
        </tr> 
        <tr>
            <td height="130px" valign="top">
                <asp:Panel ID="pnlData" runat="server">
                    <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                        DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnSelectedIndexChanged="grvData_SelectedIndexChanged"
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
                            <asp:BoundField DataField="LOID">
                                <ControlStyle CssClass="zHidden" />
                                <ItemStyle CssClass="zHidden" />
                                <HeaderStyle CssClass="zHidden" />
                                <FooterStyle CssClass="zHidden" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CODE" HeaderText="เลขที่ใบตรวจรับ">
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RECEIVEDATE" DataFormatString="{0 : dd/MM/yyyy}" HtmlEncode="False" HeaderText="วันที่ตรวจรับ">
                                <ItemStyle HorizontalAlign="Center" Width="70px" />
                                <HeaderStyle Width="70px" />
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
                <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox></td> 
        </tr> 
    </table>
</asp:Content>