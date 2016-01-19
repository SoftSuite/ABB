<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ToDoListInvList.aspx.cs" Inherits="ToDoList_ToDoListInvList" Title="ToDoList" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;To Do List</td> 
        </tr> 
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="810px">
                    <tr style="height:32">
                        <td width="1px" style="background-image: url(../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                        </td>
                        <td valign="top" align="center" width="1px">
                            <asp:image id="imgBD1L" ImageUrl="~/Images/pMenuSL.PNG" runat="server" />
                        </td>
                        <td style="background-image: url(../Images/pMenuS.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center" width="172px">
                            <asp:Label ID="btnAllSource" runat="server" Text="รายการรับจองที่รอออก Invoice" Width="170px"></asp:Label>
                        </td> 
                        <td valign="top" align="center" width="1px">
                            <asp:image id="imgBD1R" ImageUrl="~/Images/pMenuSR.PNG" runat="server"/>
                        </td>
                        <td width="635px" style="background-image: url(../Images/pMenuB.png); background-repeat: repeat-x; background-color: transparent;" valign="middle" align="center">
                        </td> 
                    </tr>
                    <tr>
                        <td colspan="5">
                            <table border="0" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td width="5px"></td>  
                                    <td>
                                        <table border="0" cellpadding="0" cellspacing="0"  Width="800px" style="border-right: #cbdaa9 1px solid; border-top: #cbdaa9 1px solid; border-left: #cbdaa9 1px solid; border-bottom: #cbdaa9 1px solid">
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
                                                <td width="260px"></td>
                                            </tr> 
                                            <tr height="25px">
                                                <td width="50px"></td>
                                                <td width="150px">
                                                    เลขที่ใบจอง</td>
                                                <td width="150px" colspan="3"><asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox" Width="320px"></asp:TextBox></td>
                                                <td></td>
                                            </tr> 
                                            <tr height="25px">
                                                <td width="50px"></td>
                                                <td width="150px">
                                                    วันที่รับจอง</td>
                                                <td width="150px">
                                                    <uc2:DatePickerControl ID="ctlDateFrom" runat="server" />
                                                </td>
                                                <td width="20px" align="center">
                                                    ถึง</td>
                                                <td style="width: 170px"><uc2:DatePickerControl ID="ctlDateTo" runat="server" /></td>
                                                <td></td>
                                            </tr> 
                                            <tr height="25px">
                                                <td width="50px"></td>
                                                <td width="150px">
                                                    ชื่อลูกค้า</td>
                                                <td colspan="3"><asp:DropDownList ID="cmbCustomer" runat="server" CssClass="zComboBox" Width="320px">
                                                </asp:DropDownList></td>
                                                <td>
                                                    <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click"/></td>
                                            </tr> 
                                            <tr height="25px" class="zHidden">
                                                <td width="50px"></td>
                                                <td width="150px">
                                                    สถานะ</td>
                                                <td width="150px"><asp:DropDownList ID="cmbStatus" runat="server" CssClass="zComboBox" Width="150px" Enabled="False">
                                                </asp:DropDownList></td>
                                                <td width="20px" align="center"></td>
                                                <td style="width: 170px"></td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr> 
                                            <tr height="10px">
                                                <td width="50px"></td>
                                                <td width="150px"></td>
                                                <td width="150px"></td>
                                                <td width="20px"></td>
                                                <td style="width: 170px"></td>
                                                <td></td>
                                            </tr> 
                                        </table>
                                    </td> 
                                    <td width="5px"></td>  
                                </tr> 
                                <tr>
                                    <td width="5px"></td>
                                    <td class="toolbarplace">
                                        <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                                            BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                                            BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnNew="สร้างใบ Invoice" OnNewClick="NewClick"/>
                                    </td> 
                                    <td width="5px"></td>  
                                </tr> 
                                <tr>
                                    <td width="5px"></td>
                                    <td>
                                        <asp:GridView ID="grvRequisition" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                            EmptyDataText="<center>***ไม่พบข้อมูล***</center>" Width="800px">
                                            <PagerSettings Visible="False" />
                                            <Columns>
                                                <asp:BoundField DataField="LOID">
                                                    <ControlStyle CssClass="zHidden" />
                                                    <ItemStyle CssClass="zHidden" />
                                                    <HeaderStyle CssClass="zHidden" />
                                                    <FooterStyle CssClass="zHidden" />
                                                </asp:BoundField>
                                                <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="../Transaction/ProductInvoice.aspx?loid={0}"
                                                    DataTextField="CODE" DataTextFormatString="{0}" HeaderText="เลขที่ใบจอง">
                                                    <ItemStyle HorizontalAlign="center" Width="110px" />
                                                    <HeaderStyle Width="110px" />
                                                </asp:HyperLinkField>
                                                <asp:BoundField DataField="RESERVEDATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันที่รับจอง"
                                                    HtmlEncode="False" SortExpression="RESERVEDATE">
                                                    <ItemStyle HorizontalAlign="center" Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CONFIRMDATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="กำหนดยืนยัน"
                                                    HtmlEncode="False" SortExpression="CONFIRMDATE">
                                                    <ItemStyle HorizontalAlign="center" Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="DUEDATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="กำหนด<br>ส่งสินค้า"
                                                    HtmlEncode="False" SortExpression="DUEDATE">
                                                    <ItemStyle HorizontalAlign="center" Width="80px" />
                                                    <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="CUSTOMERNAME" HeaderText="ลูกค้า" SortExpression="CUSTOMERNAME">
                                                    <ItemStyle Width="170px" />
                                                    <HeaderStyle Width="170px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="GRANDTOT" HeaderText="จำนวนเงิน" DataFormatString="{0:#,##0.00}" HtmlEncode="False" SortExpression="CREATEBY">
                                                    <ItemStyle Width="100px" HorizontalAlign="right" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="STATUSNAME" HeaderText="สถานะ" SortExpression="STATUSNAME">
                                                    <ItemStyle Width="100px" />
                                                    <HeaderStyle Width="100px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="t_headtext" />
                                            <AlternatingRowStyle CssClass="t_alt_bg" />
                                        </asp:GridView>
                                    </td> 
                                    <td width="5px"></td>  
                                </tr>
                            </table>
                        </td> 
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>