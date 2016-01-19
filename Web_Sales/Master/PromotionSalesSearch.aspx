<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PromotionSalesSearch.aspx.cs" Inherits="Master_PromotionSalesSearch" Title="กำหนดราคาสินค้าส่งเสริมการขาย" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;กำหนดราคาสินค้าส่งเสริมการขาย</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="false" BtnCancelShow="false"
                    BtnDeleteShow="true" BtnEditShow="false" BtnNewShow="true" BtnPrintShow="false"
                    BtnReturnShow="false" BtnSaveShow="false" BtnSubmitShow="false" OnNewClick="NewClick" OnDeleteClick="DeleteClick" />
            </td> 
        </tr> 
        <tr height="10px">
            <td>
            </td>
        </tr>
       
        <tr height="25px">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800" class="searchTable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;ค้นหา</td>
                    </tr>
                    <tr height="10">
                        <td colspan="3" height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            คลังสินค้า</td> 
                        <td style="width: 600px">
                             <asp:DropDownList ID="cmbWarehouse" runat="server" CssClass="zComboBox" Width="290px" OnSelectedIndexChanged="cmbWarehouse_SelectedIndexChanged">
                            </asp:DropDownList></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                           วันที่เริ่มใช้</td> 
                        <td style="width: 600px">
                            <uc2:DatePickerControl ID="ctlEFDateFrom" runat="server" />
                            &nbsp;ถึง&nbsp;
                            <uc2:DatePickerControl ID="ctlEFDateTo" runat="server" />
                        </td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            วันที่สิ้นสุด</td> 
                        <td style="width: 600px">
                            <uc2:DatePickerControl ID="ctlEPDateFrom" runat="server" />
                            &nbsp;ถึง&nbsp;
                            <uc2:DatePickerControl ID="ctlEPDateTo" runat="server" />
                            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                            <asp:ImageButton id="btnSearch" runat="server" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" ImageAlign="AbsMiddle">
                            </asp:ImageButton></td> 
                    </tr>
                    <tr height="10">
                        <td height="10" style="width: 50px">
                        </td>
                        <td height="10" style="width: 150px">
                        </td>
                        <td height="10" style="width: 600px">
                        </td>
                    </tr>
                </table>
            </td> 
        </tr> 
        <tr height="10">
            <td height="10">
            </td>
        </tr>
         <tr>
            <td>
                <asp:GridView ID="grvPromotionSale" runat="server" CssClass="t_tablestyle" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"  AutoGenerateColumns="False" Width="800px" OnRowDataBound="grvPromotionSale_RowDataBound">
                <Columns>
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
                       <asp:TemplateField HeaderText="ลำดับที่">
                            <ItemTemplate>
                                <asp:Label ID="lblNo" runat="server" Text=""></asp:Label> 
                            </ItemTemplate> 
                            <ItemStyle HorizontalAlign="Center"/>
                            <HeaderStyle Width="50px" /> 
                       </asp:TemplateField> 
                       <asp:BoundField DataField="LOID" >
                            <ControlStyle CssClass="zHidden" />
                            <ItemStyle CssClass="zHidden" />
                            <HeaderStyle CssClass="zHidden" />
                            <FooterStyle CssClass="zHidden" />
                        </asp:BoundField>
                     <asp:HyperLinkField DataNavigateUrlFields="LOID" DataNavigateUrlFormatString="PromotionSales.aspx?loid={0}"
                            DataTextField="CODE" HeaderText="รหัส" DataTextFormatString="{0}" >
                            <ItemStyle Width="120px" />
                            <HeaderStyle Width="120px" /> 
                        </asp:HyperLinkField>
                    <asp:BoundField DataField="NAME" HeaderText="ชื่อ">
                    </asp:BoundField>
                    <asp:BoundField DataField="EFDATE" HeaderText="วันที่เริ่มใช้" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" >
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="EPDATE" HeaderText="วันที่สิ้นสุด" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="False" >
                        <ItemStyle Width="80px" HorizontalAlign="Center" />
                        <HeaderStyle Width="80px" />
                    </asp:BoundField>
                     <asp:BoundField DataField="LOWERPRICE" HeaderText="ราคาซื้อขั้นต่ำ" DataFormatString="{0:#,##0}" HtmlEncode="False" >
                        <ItemStyle Width="90px" HorizontalAlign="right" />
                        <HeaderStyle Width="90px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="DISCOUNT" HeaderText="ส่วนลด(%)">
                        <ItemStyle Width="70px" HorizontalAlign="Center" />
                        <HeaderStyle Width="70px" />
                    </asp:BoundField>
                    </Columns> 
                        <HeaderStyle CssClass="t_headtext" />
                        <AlternatingRowStyle CssClass="t_alt_bg" />
                        <PagerSettings Visible="False" />            
                </asp:GridView>
            </td> 
        </tr>
    </table>
</asp:Content>

