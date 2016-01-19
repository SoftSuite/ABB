<%@ Page Language="C#"  MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductionLostSearch.aspx.cs" Inherits="Transaction_ProductionLostSearch" Title="บันทึกความสูญเสียวัตถุดิบและบรรจุภัณฑ์" %>

<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
   <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext">
                &nbsp;บันทึกความสูญเสียวัตถุดิบและบรรจุภัณฑ์</td> 
        </tr> 
        <tr>
            <td class="toolbarplace">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false"  />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td> 
        </tr> 
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800px" class="searchTable">
                    <tr>
                        <td colspan="6" class="subheadertext">
                            &nbsp;ค้นหา</td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px"></td>
                        <td style="width: 130px"></td> 
                        <td width="20px"></td>
                        <td width="155px"></td>
                    </tr>
                   
                     <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            วันที่บันทึก</td>
                        <td style="width: 130px">
                            <uc2:DatePickerControl ID="ctlOrderDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td width="155px"><uc2:DatePickerControl ID="ctlOrderDateTo" runat="server"/></td>
                        <td></td>
                    </tr> 
                     <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            วันที่เริ่มผลิต</td>
                        <td style="width: 130px">
                            <uc2:DatePickerControl ID="ctlMfgDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td width="155px"><uc2:DatePickerControl ID="ctlMfgDateTo" runat="server"/></td>
                        <td></td>
                    </tr> 
                     <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            วันที่ผลิตเสร็จ</td>
                        <td style="width: 130px">
                            <uc2:DatePickerControl ID="ctlSendFGDateFrom" runat="server" />
                        </td>
                        <td width="20px" align="center">
                            ถึง</td>
                        <td width="155px"><uc2:DatePickerControl ID="ctlSendFGDateTo" runat="server"/></td>
                        <td></td>
                    </tr> 
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px">
                            เลขที่การผลิต</td>
                         <td colspan = 3>
                             <asp:TextBox ID="txtLotNo" runat="server" Width="285px"></asp:TextBox></td>
                            <td width="50px">
                                &nbsp;</td>

                    </tr>  
                    <tr height="25px">
                        <td width="50px" style="height: 25px"></td>
                        <td style="width: 80px; height: 25px">
                            ชื่อผลิตภัณฑ์</td>
                        <td colspan = 3 style="height: 25px">
                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zComboBox" Width="292px">
                            </asp:DropDownList></td>
                        <td width="50px" style="height: 25px"><asp:ImageButton ID="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                           
                    </tr>                      
                    <tr height="25px">
                        <td width="50px"></td>
                        <td style="width: 80px"></td>
                        <td style="width: 130px"></td> 
                        <td width="20px"></td>
                        <td width="155px"></td>
                    </tr>
                </table>
            </td> 
        </tr>
        <tr height="10px">
            <td></td> 
        </tr> 
        <tr>
            <td>
                <asp:GridView ID="grvPDOrder" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                    EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                    Width="800px" OnRowCommand="grvPDOrder_RowCommand" OnRowDataBound="grvPDOrder_RowDataBound">
                    <PagerSettings Visible="False" />
                    <Columns>
<asp:TemplateField ShowHeader="False"><ItemTemplate>
<asp:ImageButton id="btnPrintProduction" runat="server" ImageUrl="~/Images/icn_print.gif" AlternateText="พิมพ์ความสูญเสียบรรจุภัณฑ์" CommandName="print" CausesValidation="False" __designer:wfdid="w1"></asp:ImageButton> 
<asp:ImageButton id="btnPrintMaterial" runat="server" ImageUrl="~/Images/icn_print.gif" AlternateText="พิมพ์ความสูญเสียวัตถุดิบ" CommandName="print" CausesValidation="False" __designer:wfdid="w2"></asp:ImageButton>
</ItemTemplate>

<FooterStyle Width="60px"></FooterStyle>

<HeaderStyle Width="60px"></HeaderStyle>

<ItemStyle Width="60px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTNAME" HeaderText="ชื่อผลิตภัณฑ์" SortExpression="PRODUCTNAME">
<HeaderStyle Width="200px"></HeaderStyle>

<ItemStyle Width="200px"></ItemStyle>
</asp:BoundField>
<asp:TemplateField HeaderText="เลขที่การผลิต"><ItemTemplate>
                                 <asp:HyperLink ID="hplLotNo" runat="server" Text='<%# Bind("LOTNO") %>'></asp:HyperLink>
                            
</ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle Width="100px"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="MFGDATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันที่เริ่มผลิต" HtmlEncode="False" SortExpression="MFGDATE">
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="SENDFGDATE" DataFormatString="{0:dd/MM/yyyy}" HeaderText="วันที่ผลิตเสร็จ" HtmlEncode="False" SortExpression="SENDFGDATE">
<HeaderStyle Width="100px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="STDQTY" HeaderText="ผลผลิตตามทฤษฎี" SortExpression="STDQTY">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="PDQTY" HeaderText="ผลผลิตได้จริง" SortExpression="PDQTY">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="YIELD" HeaderText="% Yield สูญเสีย" SortExpression="YIELD">
<HeaderStyle Width="80px"></HeaderStyle>

<ItemStyle Width="80px"></ItemStyle>
</asp:BoundField>
</Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
            </td> 
        </tr> 
    </table>
</asp:Content>


