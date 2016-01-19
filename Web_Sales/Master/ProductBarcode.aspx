<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductBarcode.aspx.cs" Inherits="Master_ProductBarcode" Title="สินค้าสำเร็จรูป - หน่วยย่อย" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellspacing="0" cellpadding="0" width="100%">
        <tr class="headtext">
            <td>
                &nbsp;สินค้าสำเร็จรูป - หน่วยย่อย</td> 
        </tr> 
        <tr class = "toolbarplace">
            <td>
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="false"
                    BtnReturnShow="true" BtnSaveShow="true" BtnSubmitShow="false" NameBtnReturn="กลับหน้าสินค้าหลัก"
                    OnSaveClick="SaveClick" OnBackClick="BackClick" OnReturnClick="ReturnClick" />
            </td> 
        </tr> 
        <tr>
            <td height="10">
            </td>
        </tr>
        <tr height="25">
            <td>
                <table border="0" cellpadding="0" cellspacing="0" width="800" class="searchTable">
                    <tr height="25">
                        <td class="subheadertext" colspan="3">
                            &nbsp;ค้นหา</td>
                    </tr>
                    <tr height="10">
                        <td height="10" style="width: 50px">
                        </td>
                        <td height="10" style="width: 150px">
                        </td>
                        <td height="10">
                        </td>
                    </tr>
                    <tr height="25px">
                        <td style="width: 50px">
                        </td>
                        <td style="width: 150px">
                            ชื่อสินค้าภาษาไทย</td> 
                        <td>
                            <asp:TextBox ID="txtName" runat="server" CssClass="zTextbox-View" MaxLength="100" Width="205px"></asp:TextBox>
                            <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden"></asp:TextBox>
                            <asp:TextBox ID="txtMasterUnit" runat="server" CssClass="zHidden"></asp:TextBox>
                            <asp:TextBox ID="txtPackSizeUint" runat="server" CssClass="zHidden"></asp:TextBox>
                        </td> 
                    </tr>
                    <tr height="10">
                        <td height="10" style="width: 50px">
                        </td>
                        <td height="10" style="width: 150px">
                        </td>
                        <td height="10">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td height="10">
            </td>
        </tr>
         <tr>
            <td>
                <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="True"
                    Width="800px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" 
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
<ItemTemplate>
<asp:ImageButton id="imbEdit" runat="server" ImageUrl="~/Images/icn_edit.gif" AlternateText="แก้ไข" CommandName="Edit" CausesValidation="False" __designer:wfdid="w5"></asp:ImageButton>&nbsp; 
</ItemTemplate>

<FooterStyle HorizontalAlign="Center" Width="40px"></FooterStyle>

<HeaderStyle HorizontalAlign="Center" Width="40px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="Barcode"><EditItemTemplate>
                                <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox" Width="95px" MaxLength="20" Text='<%# Bind("BARCODE") %>'></asp:TextBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextbox" Width="95px" MaxLength="20"></asp:TextBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:Label ID="txtBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                            
</ItemTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ชื่อย่อ"><EditItemTemplate>
                                <asp:TextBox ID="txtAbbname" runat="server" CssClass="zTextbox" Width="45px" MaxLength="20" Text='<%# Bind("ABBNAME") %>'></asp:TextBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:TextBox ID="txtNewAbbname" runat="server" CssClass="zTextbox" Width="45px" MaxLength="20"></asp:TextBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:Label ID="txtAbbnameView" runat="server" Text='<%# Bind("ABBNAME") %>'></asp:Label>
                            
</ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวน"><EditItemTemplate>
                                <asp:TextBox ID="txtMultiply" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Bind("MULTIPLY") %>'></asp:TextBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:TextBox ID="txtNewMultiply" runat="server" CssClass="zTextboxR" Width="45px"></asp:TextBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:Label ID="txtMultiplyView" runat="server" Text='<%# Bind("MULTIPLY") %>'></asp:Label>
                            
</ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วยเริ่มต้น" InsertVisible="False"><ItemTemplate>
                                <asp:Label ID="lblUnitMaster" runat="server" Text='<%# Bind("UNITMASTER") %>'></asp:Label>
                            
</ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย"><EditItemTemplate>
                                <asp:DropDownList ID="cmbUnit" runat="server" CssClass="zCombobox" Enabled="true" Width="85px"></asp:DropDownList>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:DropDownList ID="cmbNewUnit" runat="server" CssClass="zCombobox" Enabled="true" Width="85px"></asp:DropDownList>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:DropDownList ID="cmbUnitView" runat="server" CssClass="zCombobox" Enabled="false" Width="85px"></asp:DropDownList>
                            
</ItemTemplate>

<HeaderStyle Width="90px"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ราคาทุน"><EditItemTemplate>
                                <asp:TextBox ID="txtCost" runat="server" CssClass="zTextboxR" Width="50px" Text='<%# Bind("COST") %>'></asp:TextBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:TextBox ID="txtNewCost" runat="server" CssClass="zTextboxR" Width="50px" Text="0"></asp:TextBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:Label ID="txtCostView" runat="server" Text='<%# Bind("COST") %>'></asp:Label>
                            
</ItemTemplate>

<HeaderStyle Width="55px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ราคาขาย"><EditItemTemplate>
                                <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR" Width="50px" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR" Width="50px" Text="0"></asp:TextBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:Label ID="txtPriceView" runat="server" Text='<%# Bind("PRICE") %>'></asp:Label>
                            
</ItemTemplate>

<HeaderStyle Width="55px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="รวมVAT"><EditItemTemplate>
                                <asp:CheckBox ID="chkIsVAT" runat="server" Enabled="true"></asp:CheckBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:CheckBox ID="chkNewIsVAT" runat="server" Enabled="true"></asp:CheckBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:CheckBox ID="chkIsVATView" runat="server" Enabled="false"></asp:CheckBox>
                            
</ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="คิดส่วนลด"><EditItemTemplate>
                                <asp:CheckBox ID="chkIsDiscount" runat="server" Enabled="true"></asp:CheckBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:CheckBox ID="chkNewIsDiscount" runat="server" Enabled="true"></asp:CheckBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:CheckBox ID="chkIsDiscountView" runat="server" Enabled="false"></asp:CheckBox>
                            
</ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ขนาดบรรจุ"><EditItemTemplate>
                                <asp:TextBox ID="txtPackSize" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Bind("PACKSIZE") %>'></asp:TextBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:TextBox ID="txtNewPackSize" runat="server" CssClass="zTextboxR" Width="45px" Text="0"></asp:TextBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:Label ID="txtPackSizeView" runat="server" Width="45px" Text='<%# Bind("PACKSIZE") %>'></asp:Label>
                            
</ItemTemplate>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วยปริมาณ"><EditItemTemplate>
                                <asp:DropDownList ID="cmbUnitPack" runat="server" CssClass="zCombobox" Enabled="true" Width="85px"></asp:DropDownList>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:DropDownList ID="cmbNewUnitPack" runat="server" CssClass="zCombobox" Enabled="true" Width="85px"></asp:DropDownList>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:DropDownList ID="cmbUnitPackView" runat="server" CssClass="zCombobox" Enabled="false" Width="85px"></asp:DropDownList>
                            
</ItemTemplate>

<HeaderStyle Width="90px"></HeaderStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="ใช้งาน"><EditItemTemplate>
                                <asp:CheckBox ID="chkIsActive" runat="server" Enabled="true"></asp:CheckBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:CheckBox ID="chkNewIsActive" runat="server" Enabled="false"></asp:CheckBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:CheckBox ID="chkIsActiveView" runat="server" Enabled="false"></asp:CheckBox>
                            
</ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:TemplateField HeaderText="เริ่มต้น"><EditItemTemplate>
                                <asp:CheckBox ID="chkIsDefault" runat="server" Enabled="false"></asp:CheckBox>
                            
</EditItemTemplate>
<FooterTemplate>
                                <asp:CheckBox ID="chkNewIsDefault" runat="server" Enabled="false"></asp:CheckBox>
                            
</FooterTemplate>
<ItemTemplate>
                                <asp:CheckBox ID="chkIsDefaultView" runat="server" Enabled="false"></asp:CheckBox>
                            
</ItemTemplate>

<FooterStyle HorizontalAlign="Center"></FooterStyle>

<HeaderStyle Width="50px"></HeaderStyle>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="UNIT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ISVAT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>
</asp:BoundField>
<asp:BoundField DataField="ISDISCOUNT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>
</asp:BoundField>
</Columns>
                    <HeaderStyle CssClass="t_headtext" />
                    <AlternatingRowStyle CssClass="t_alt_bg" />
                </asp:GridView>
                <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteItem" SelectMethod="GetItem" 
                    TypeName="ProductBarcodeItem" UpdateMethod="UpdateItem" OldValuesParameterFormatString="{0}">
                    <DeleteParameters>
                        <asp:Parameter Name="LOID" Type="Double" />
                    </DeleteParameters>
                    <UpdateParameters>
                        <asp:Parameter Name="LOID" Type="Decimal" />
                        <asp:Parameter Name="BARCODE" Type="String" />
                        <asp:Parameter Name="ABBNAME" Type="String" />
                        <asp:Parameter Name="MULTIPLY" Type="Decimal" />
                        <asp:Parameter Name="UNITMASTER" Type="String" />
                        <asp:Parameter Name="UNIT" Type="Decimal" />
                        <asp:Parameter Name="COST" Type="Decimal" />
                        <asp:Parameter Name="PRICE" Type="Decimal" />
                        <asp:Parameter Name="ISVAT" Type="String" />
                        <asp:Parameter Name="ISDISCOUNT" Type="String" />
                        <asp:Parameter Name="PACKSIZE" Type="Decimal" />
                        <asp:Parameter Name="UNITPACK" Type="Decimal" />
                    </UpdateParameters>
                    <SelectParameters>
                        <asp:ControlParameter ControlID="txtLOID" Name="ProductMaster" PropertyName="Text"
                            Type="Double" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </td> 
        </tr>  
        <tr>
            <td></td> 
        </tr> 
    </table>    
</asp:Content>