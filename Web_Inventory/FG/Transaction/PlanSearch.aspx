<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PlanSearch.aspx.cs" Inherits="FG_Transaction_PlanSearch" Title="แผนการสั่งสินค้า" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;แผนการสั่งสินค้า</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" NameBtnSubmit="ยืนยัน"
                    OnNewClick="NewClick" OnDeleteClick="DeleteClick"/>
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="700" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td style="width: 100px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 100px">
                            ปี พ.ศ.</td>
                        <td width="150px">
                            <asp:TextBox ID="txtYearFrom" runat="server" CssClass="zTextbox" MaxLength="4" Width="80px"></asp:TextBox></td>
                        <td width="20px">
                            ถึง</td>
                        <td style="width: 170px">
                            <asp:TextBox ID="txtYearTo" runat="server" CssClass="zTextbox" MaxLength="4" Width="80px"></asp:TextBox></td>
                        <td>
                            <asp:TextBox ID="txtNewLOID" runat="server" CssClass="zHidden" MaxLength="4" Width="60px"></asp:TextBox></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 100px">
                            วันที่สร้าง</td>
                        <td width="150px"><uc2:DatePickerControl ID="ctlCreateFrom" runat="server" />
                            </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlCreateTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 100px">
                            วันที่ยืนยัน</td>
                        <td width="150px">
                            <uc2:DatePickerControl ID="ctlConfirmFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td style="width: 170px"><uc2:DatePickerControl ID="ctlConfirmTo" runat="server" /></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px" style="height: 25px"></td>
                        <td style="width: 100px; height: 25px;">
                            สถานะ</td>
                        <td width="150px" style="height: 25px"><asp:DropDownList ID="cmbStatusFrom" runat="server" CssClass="zComboBox" Width="130px">
                        </asp:DropDownList></td>
                        <td width="20px" align="center" style="height: 25px">
                            ถึง</td>
                        <td style="width: 170px; height: 25px;"><asp:DropDownList ID="cmbStatusTo" runat="server" CssClass="zComboBox" Width="130px">
                        </asp:DropDownList></td>
                        <td style="height: 25px">
                            <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                    </tr> 
                    <tr height="10px">
                        <td width="50px"></td>
                        <td style="width: 100px"></td>
                        <td width="150px"></td>
                        <td width="20px"></td>
                        <td style="width: 170px"></td>
                        <td></td>
                    </tr> 
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPlan" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="700px" OnRowCommand="grvPlan_RowCommand" OnRowDataBound="grvPlan_RowDataBound" >
                    <PagerSettings Visible="False" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox ID="chkAll" runat="server" />
                            </HeaderTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="25px" />
                            <ItemTemplate>
                                <asp:CheckBox ID="chkItem" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:ImageButton ID="btnPrint" runat="server" CausesValidation="False" CommandName="print" AlternateText="พิมพ์"
                                    ImageUrl="~/Images/icn_print.gif"/>
                                <asp:ImageButton ID="btnCancel" AlternateText="ยกเลิก"
                                        runat="server" CausesValidation="False" CommandName="cancelItem" ImageUrl="~/Images/icn_cancel.gif"/>
                            </ItemTemplate>
                            <ItemStyle Width="60px"/>
                            <HeaderStyle Width="60px"/>
                            <FooterStyle Width="60px"/>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle Width="55px" />
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text='<%# Bind("ORDERNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="LOID">
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                        <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="Plan.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="เลขที่แผนงาน" DataTextFormatString="{0}" >
                            <ItemStyle HorizontalAlign="Center"/>
                        </asp:HyperLinkField> 
                        <asp:BoundField SortExpression="YEAR" HeaderText = "ปี พ.ศ." DataField="YEAR">
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CREATEON" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่สร้าง" DataField="CREATEON" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>
                        <asp:BoundField SortExpression="CONFIRMDATE" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" HeaderText = "วันที่ยืนยัน" DataField="CONFIRMDATE" >
                            <ItemStyle Width="80px" HorizontalAlign="Center"/>
                            <HeaderStyle Width="80px" />  
                        </asp:BoundField>  
                        <asp:BoundField SortExpression="STATUSNAME" HeaderText = "สถานะ" DataField="STATUSNAME">
                            <ItemStyle Width="100px"/>
                            <HeaderStyle Width="100px" />  
                        </asp:BoundField>
                    </Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>