<%@ Page Language="C#" MasterPageFile="~/Template/MasterBody.master" AutoEventWireup="true" CodeFile="ProductList.aspx.cs" Inherits="FG_Transaction_StockCheck_ProductList" Title="ตรวจนับสินค้า" %>
<%@ Register Src="../../../Controls/ToolbarControl.ascx" TagName="ToolbarControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainForm" Runat="Server">
    <table border="0" cellpadding="0" cellspacing="0" width = "100%">
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false" BtnDeleteShow="false" 
                    BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false"  BtnViewShow="false"
                    BtnSubmitShow="false" BtnHelpShow="false" OnNewClick="NewClick" OnSaveClick="SaveClick" OnBackClick="BackClick" NameBtnNew="เพิ่มสินค้า" />
                <uc1:ToolbarControl ID="ctlToolbarZone" runat="server" BtnBackShow="false" BtnCancelShow="false" BtnDeleteShow="false" 
                    BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false" BtnReturnShow="false" BtnSaveShow="false"  BtnViewShow="false"
                    BtnSubmitShow="false" BtnHelpShow="false" OnNewClick="NewZoneClick" NameBtnNew="โซนใหม่" />
            </td> 
        </tr> 
        <tr>
            <td>
                <asp:Panel ID="pnlData" runat="server" Height="150px">
                    <table border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td height="150px" valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr height="20px">
                                        <td>
                                            &nbsp;<b>Batch No : </b><asp:Label ID="lblBatchNo" runat="server" ></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr height="20px">
                                        <td>
                                            &nbsp;<b>Location : </b><asp:Label ID="lblWarehouseName" runat="server"></asp:Label>
                                        </td>
                                    </tr> 
                                    <tr height="20px">
                                        <td>
                                            <b>&nbsp;โซน : </b>
                                                <asp:DropDownList ID="cmbLocation" runat="server" AutoPostBack="True" CssClass="zComboBox"
                                                    OnSelectedIndexChanged="cmbLocation_SelectedIndexChanged" Width="120px">
                                                </asp:DropDownList></td>
                                    </tr>
                                    <tr>
                                        <td height="90" valign="top">
                                            <asp:GridView ID="grvData" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                                DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                                                Width="220px" OnSelectedIndexChanged="grvData_SelectedIndexChanged" OnRowDataBound="grvData_RowDataBound" OnRowCommand="grvData_RowCommand">
                                                <PagerSettings Visible="False" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="btnDetail" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_View.gif" CommandName="view" AlternateText="รายละเอียด" CommandArgument="" />
                                                            <asp:ImageButton ID="btnDelete" runat="server" CausesValidation="True" ImageUrl="~/Images/icn_delete.gif" CommandName="select" AlternateText="ลบรายการ" />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="40px" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="LOID">
                                                        <ControlStyle CssClass="zHidden" />
                                                        <ItemStyle CssClass="zHidden" />
                                                        <HeaderStyle CssClass="zHidden" />
                                                        <FooterStyle CssClass="zHidden" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="productNAME" HeaderText="สินค้า">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="LOTNO" HeaderText="Lot No">
                                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                                        <HeaderStyle Width="40px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="COUNTQTY" HeaderText="จำนวน" DataFormatString="{0:#,##0}" HtmlEncode="false">
                                                        <ItemStyle HorizontalAlign="right" Width="40px" />
                                                        <HeaderStyle Width="40px" />
                                                    </asp:BoundField>
                                                </Columns>
                                                <SelectedRowStyle CssClass="t_selectstyle" />
                                                <HeaderStyle CssClass="t_headtext" />
                                                <AlternatingRowStyle CssClass="t_alt_bg" />
                                            </asp:GridView>
                                        </td>
                                    </tr> 
                                </table>
                            </td> 
                        </tr>
                    </table> 
                </asp:Panel>
                <asp:Panel ID="pnlMessage" runat="server" ScrollBars="None" Visible="false">
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td height="75px">
                            </td> 
                        </tr> 
                        <tr >
                            <td height="75px" class="messageLayer">
                                <asp:Panel ID="pnlSave" runat="server" ScrollBars="None" Width="220px" Visible="false">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                        <tr height="24">
                                            <td width="50" align="center" valign="top">
                                                <img src="../../../Images/msg_Inform.gif" /></td>
                                            <td>
                                                <b>บันทึกข้อมูลเรียบร้อย</b>
                                            </td> 
                                        </tr> 
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                    </table>
                                </asp:Panel> 
                                <asp:Panel ID="pnlDelete" runat="server" ScrollBars="None" Width="220px" Visible="false">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                        <tr height="18">
                                            <td width="50" align="center" valign="top">
                                                <img src="../../../Images/msg_Question.gif" /></td>
                                            <td>
                                                <b>ต้องการลบรายการสินค้า</b><br />
                                                &nbsp;&nbsp; "<asp:Label ID="lblProductName" runat="server"></asp:Label>"&nbsp;
                                                <b>ใช่หรือไม่ ?</b>
                                            </td> 
                                        </tr> 
                                        <tr height="3">
                                            <td colspan="2"></td> 
                                        </tr>
                                    </table>
                                </asp:Panel> 
                            </td> 
                        </tr> 
                    </table>
                </asp:Panel>
            </td> 
        </tr> 
        <tr>
            <td class="subheadertext">
                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                    <tr>
                        <td>
                            &nbsp;<asp:LinkButton ID="btnSave" runat="server" Text="ตกลง" CssClass="hButton" Width="80px" OnClick="btnSave_Click" /></td>
                        <td align="right">
                            <asp:LinkButton ID="btnCancel" runat="server" Text="ยกเลิก" CssClass="hButton" Width="80px" OnClick="btnCancel_Click" />&nbsp;</td>
                    </tr> 
                </table>
            </td> 
        </tr> 
    </table>
   <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="70px"></asp:TextBox> 
</asp:Content>