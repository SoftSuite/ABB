<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="StockinProduction.aspx.cs" Inherits="Transaction_StockinProduction" Title="ใบนำส่งผลิตภัณฑ์เข้าคลัง" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;<asp:Label ID="lblHeader" runat="server" Text=""></asp:Label></td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="ยืนยัน"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr height="25px" width="800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" width="615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" width="390px" class="zCombobox">
                                 <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        </td>
                                    <td colspan="3">
                                        &nbsp;<asp:TextBox ID="txtSender" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox id="txtCode" runat="server" CssClass="zHidden" Width="30px">
                                        </asp:TextBox></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ผู้ส่ง</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtSenderName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>
                                </td>
                                </tr>
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtSenderCode" runat="server" Width="80px" CssClass="zHidden"></asp:TextBox></td>
                                    <td></td>
                                    <td style="width: 152px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width: 283px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="217px">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 225px;"></td>
                                    <td></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        เลขที่</td>
                                    <td style="width: 225px">
                                        <asp:TextBox ID="txtStockinCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px; height: 25px;"></td>
                                    <td style="width: 75px; height: 25px;">
                                        ลงวันที่</td>
                                    <td style="width: 225px; height: 25px;"><uc2:DatePickerControl ID="ctlReserveDate" runat="server" Enabled="false" CssClass="zTextbox-View" /></td>
                                    <td style="height: 25px"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        สถานะ</td>
                                    <td style="width: 225px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="110px"></asp:TextBox></td>
                                    <td></td>
                                </tr> 
                            </table> 
                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" 
                                OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" 
                                OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
<asp:TemplateField ShowHeader="False"><EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                                    runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        
</FooterTemplate>

<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:ImageButton id="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข" ImageUrl="~/Images/icn_edit.gif"></asp:ImageButton> <asp:ImageButton id="imbDelete" runat="server" CausesValidation="False" CommandName="Delete" AlternateText="ลบ" ImageUrl="~/Images/icn_delete.gif"></asp:ImageButton> 
</ItemTemplate>

<FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="เลขที่การผลิต"><EditItemTemplate>
<asp:TextBox id="txtBarCode" runat="server" CssClass="zTextbox-View"  ReadOnly = "true" Text='<%# Bind("LOTNO") %>' Width="95px" OnTextChanged="txtBarCode_TextChanged" MaxLength="20" AutoPostBack="true"></asp:TextBox> 
</EditItemTemplate>
<FooterTemplate>
<asp:TextBox id="txtNewBarCode" runat="server" CssClass="zTextbox-View"  Width="60px" OnTextChanged="txtNewBarCode_TextChanged1" MaxLength="20" AutoPostBack="true"></asp:TextBox> <asp:ImageButton id="btnNewSearch" runat="server" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle"></asp:ImageButton> <asp:TextBox id="txtGetData" runat="server" CssClass="zHidden" Width="5px"></asp:TextBox>&nbsp; <asp:TextBox id="txtRefLoid" runat="server" CssClass="zHidden" Width="5px"></asp:TextBox> <asp:TextBox id="txtProductLOID" runat="server" CssClass="zHidden" Width="5px"></asp:TextBox> <asp:TextBox id="txtUnitLOID" runat="server" CssClass="zHidden" Width="5px"></asp:TextBox> <asp:TextBox id="txtPDPLOID" runat="server" CssClass="zHidden" Width="5px"></asp:TextBox>&nbsp; 
</FooterTemplate>

<ItemStyle Width="120px"></ItemStyle>

<HeaderStyle Width="120px"></HeaderStyle>
<ItemTemplate>
<asp:Label id="txtBarcodeView" runat="server" Text='<%# Bind("LOTNO") %>' Width="95px"></asp:Label> 
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="วันที่ผลิต"><EditItemTemplate>
                                           <asp:TextBox ID="txtMfgdate" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="150px" Text='<%# Bind("MFGDATE","{0:dd/MM/yyyy}") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewMfgdate" runat="server" CssClass="zTextboxR-View" Width="150px" Text=""></asp:TextBox>
                                        
</FooterTemplate>

<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtMfgdateView" runat="server" Width="45px" Text='<%# Bind("MFGDATE","{0:dd/MM/yyyy}") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า"><EditItemTemplate>
                                           <asp:TextBox ID="txtProduct" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="150px" Text='<%# Bind("PRODUCTNAME") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zTextboxR-View" Width="150px" Text=""></asp:TextBox>
                                        
</FooterTemplate>

<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtProductView" runat="server" Width="150px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จ่ายออกจากคลังกักกัน"><EditItemTemplate>
                                            <asp:TextBox ID="txtPDQTY" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="90px" Text='<%# Bind("PDQTY") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewPDQTY" runat="server" CssClass="zTextboxR-View" Width="90px" Text="0"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="90px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtPDQTYView" runat="server" Width="85px" Text='<%# Bind("PDQTY") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวนรับ"><EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="85px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="85px" Text="0"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="90px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="90px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย"><EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="100px" Text='<%# Bind("UNITNAME") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" Width="100px" Text=""></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtUnitView" runat="server" Width="100px" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PDPLOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTNAME">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="MFGDATE">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PDQTY">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="QTY">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="UNITNAME">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="UNIT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="REFLOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
</Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteStockInItem" SelectMethod="GetStockInItem" 
                                TypeName="StockinProductItem" UpdateMethod="UpdateStockInItem" InsertMethod="InsertStokInItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
<asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
</DeleteParameters>
                                <UpdateParameters>
<asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PDQTY"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="MFGDATE"></asp:Parameter>
<asp:Parameter Type="String" Name="UNITNAME"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="UNIT"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
<asp:Parameter Type="String" Name="PRODUCTNAME"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PDPLOID"></asp:Parameter>
<asp:Parameter Type="String" Name="LOTNO"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="REFLOID"></asp:Parameter>
</UpdateParameters>
                                <SelectParameters>
<asp:ControlParameter PropertyName="Text" Type="Double" Name="requisition" ControlID="txtLOID"></asp:ControlParameter>
<asp:ControlParameter PropertyName="Text" Type="String" Name="status" ControlID="txtStatus"></asp:ControlParameter>
</SelectParameters>
                            </asp:ObjectDataSource>
                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                Width="600px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <Columns>
<asp:TemplateField ShowHeader="False">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
<asp:ImageButton id="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่" ImageUrl="~/Images/icn_save.gif"></asp:ImageButton> 
</ItemTemplate>

<FooterStyle Width="50px" HorizontalAlign="Center"></FooterStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
<ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text=""></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="เลขที่การผลิต"> 
<HeaderStyle Width="120px"></HeaderStyle>
<ItemTemplate>
<asp:TextBox id="txtNewBarCode" runat="server" CssClass="zTextbox-View" Width="80px" OnTextChanged="txtNewBarCode_TextChanged" MaxLength="20" AutoPostBack="true"></asp:TextBox> <asp:ImageButton id="btnNewSearch" runat="server" ImageUrl="~/Images/view.gif" ImageAlign="AbsMiddle"></asp:ImageButton> <asp:TextBox id="txtGetData" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox> <asp:TextBox id="txtRefLoid" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox> <asp:TextBox id="txtProductLOID" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox> <asp:TextBox id="txtUnitLOID" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox> <asp:TextBox id="txtPDPLOID" runat="server" CssClass="zHidden" Width="1px"></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="วันที่ผลิต">
<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewMfgdate" runat="server" CssClass="zTextboxR-View" Width="150px" Text=""></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า">
<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zTextboxR-View" Width="150px" Text=""></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จ่ายออกจากคลังกักกัน" InsertVisible="False">
<HeaderStyle Width="90px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewPDQTY" runat="server" CssClass="zTextboxR-View" Width="90px" Text="0"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวนรับ">
<HeaderStyle Width="90px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="85px" Text="0"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" Width="100px" Text=""></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PDPLOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PRODUCTNAME">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="MFGDATE">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PDQTY">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="QTY">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="UNITNAME">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="UNIT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="REFLOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
</Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}"
                                SelectMethod="GetStockInItemBlank" TypeName="StockinProductItem" DeleteMethod="DeleteStockInItem" UpdateMethod="UpdateStockInItem">
                                <deleteparameters>
<asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
</deleteparameters>
                                <updateparameters>
<asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PDQTY"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
<asp:Parameter Type="DateTime" Name="MFGDATE"></asp:Parameter>
<asp:Parameter Type="String" Name="UNITNAME"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="UNIT"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
<asp:Parameter Type="String" Name="PRODUCTNAME"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PDPLOID"></asp:Parameter>
<asp:Parameter Type="String" Name="LOTNO"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="REFLOID"></asp:Parameter>
</updateparameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" cellpadding="0" width="600">
                                <tr>
                                    <td valign="top" width="70px">
                                        &nbsp;หมายเหตุ</td>
                                    <td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="530px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                </table>
            </td> 
            <td width="2px">
                &nbsp;
            </td> 
         </tr>
    </table>
</asp:Content>

