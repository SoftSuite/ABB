<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ReturnProduct.aspx.cs" Inherits="Transaction_ReturnProduct" Title="ใบรับคืนสินค้าฝากขาย" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบรับคืนสินค้าฝากขาย</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="true"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="ส่งคลัง"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr>
        <tr height="10px">
            <td style="width: 615px">
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr height="25px" width="800px">
            <td valign="top" style="width: 615px">
                <table border="0" cellpadding="0" cellspacing="0" width="600">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width: 460px;">
                            <table border="0" cellspacing="0" cellpadding="0" width="440" class="zCombobox">
                                 <tr style="height: 5px;">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px;">
                                        </td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomer" runat="server" Width="30px" CssClass="zHidden"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        รหัสลูกค้า</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="100px" CssClass="zTextbox"></asp:TextBox><asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" />
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="229px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbTitle" runat="server" Width="105px"></asp:DropDownList> 
                                        <asp:TextBox ID="txtName" runat="server" Width="100px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="140px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="358px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px">
                                        โทรศัพท์</td>
                                    <td style="width: 158px">
                                        <asp:TextBox ID="txtTel" runat="server" Width="140px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td style="width: 60px">
                                        โทรสาร</td>
                                    <td style="width: 152px">
                                        <asp:TextBox ID="txtFax" runat="server" Width="140px" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                </tr> 
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 65px"></td>
                                    <td style="width: 158px"></td>
                                    <td style="width: 60px"></td>
                                    <td style="width: 152px"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="10">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid; width: 220px;">
                            <table border="0" cellpadding="0" cellspacing="0" width="215">
                                <tr height="5px">
                                    <td style="width: 5px; height: 5px;"></td>
                                    <td style="width: 74px; height: 5px;"></td>
                                    <td style="height: 5px;"></td>
                                    <td style="height: 5px"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 74px">
                                        เลขที่</td>
                                    <td>
                                        <asp:TextBox ID="txtRequisitionCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 74px">
                                        ลงวันที่</td>
                                    <td><uc2:DatePickerControl ID="ctlReserveDate" runat="server" Enabled="false" /></td>
                                    <td></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 74px">
                                        วันที่ส่งสินค้า</td>
                                    <td>
                                        <uc2:DatePickerControl ID="ctlDueDate" runat="server" /><asp:Label ID="lblRemark" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
                                    <td></td>
                                </tr> 
                            </table> 
                        </td>
                    </tr> 
                    <tr height="5px">
                        <td colspan="4" style="width: 700px">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="width: 700px">
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
<asp:ImageButton id="imbEdit" runat="server" ImageUrl="~/Images/icn_edit.gif" AlternateText="แก้ไข" CommandName="Edit" CausesValidation="False"></asp:ImageButton> <asp:ImageButton id="imbDelete" runat="server" ImageUrl="~/Images/icn_delete.gif" AlternateText="ลบ" CommandName="Delete" CausesValidation="False"></asp:ImageButton> 
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
<asp:TemplateField HeaderText="บาร์โค้ด"><EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" Text='<%# Bind("BARCODE") %>' OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1" ></asp:TextBox>
                                        
</FooterTemplate>

<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="95px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า"><EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged"></asp:DropDownList>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged1"></asp:DropDownList>
                                        
</FooterTemplate>

<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:DropDownList ID="cmbProductView" runat="server" CssClass="zCombobox" Enabled="false" Width="145px"></asp:DropDownList>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวนที่มี"><EditItemTemplate>
                                            <asp:TextBox ID="txtPDQTY" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("PDQTY") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewPDQTY" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtPDQTYView" runat="server" Width="45px" Text='<%# Bind("PDQTY") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวน"><EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="1"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="45px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย"><EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="100px" Text='<%# Bind("UNITNAME") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="100px" Text="0"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>

<HeaderStyle Width="100px" HorizontalAlign="Center"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtUnitView" runat="server" Width="100px" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ราคา"><EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtPriceView" runat="server" Width="45px" Text='<%# Bind("PRICE") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="รวมเงิน"><EditItemTemplate>
                                            <asp:TextBox ID="txtNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                        
</EditItemTemplate>
<FooterTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        
</FooterTemplate>

<ItemStyle HorizontalAlign="Right"></ItemStyle>

<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:Label ID="txtNetPriceView" runat="server" Width="45px" Text='<%# Bind("NETPRICE") %>'></asp:Label>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="NETPRICE">
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
<asp:BoundField DataField="PRICE">
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
</Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteRequisitionItem" SelectMethod="GetRequisitionItem" 
                                TypeName="ReturnproductItem" UpdateMethod="UpdateRequisitionItem" InsertMethod="InsertRequisitionItem">
                                <DeleteParameters>
<asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
</DeleteParameters>
                                <UpdateParameters>
<asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PDQTY"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="UNITNME"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PRICE"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="NETPRICE"></asp:Parameter>
<asp:Parameter Type="String" Name="BARCODE"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
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
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        
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
<asp:TemplateField HeaderText="บาร์โค้ด">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า">
<HeaderStyle Width="150px"></HeaderStyle>
<ItemTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="145px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged"></asp:DropDownList>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวนที่มี" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewPDQTY" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวน">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="1"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="100px" Text="0"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ราคา" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="รวมเงิน" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="NETPRICE">
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
<asp:BoundField DataField="PRICE">
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
</Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetRequisitionItemBlank" TypeName="ReturnproductItem" DeleteMethod="DeleteRequisitionItem" UpdateMethod="UpdateRequisitionItem">
                                <deleteparameters>
<asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
</deleteparameters>
                                <updateparameters>
<asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PDQTY"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="UNITNAME"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="PRICE"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="NETPRICE"></asp:Parameter>
<asp:Parameter Type="String" Name="BARCODE"></asp:Parameter>
<asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
</updateparameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4" style="width: 700px">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4" style="width: 700px">
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
            <td valign="top" style="border-left: black 1px dotted; border-right: black 1px dotted; border-top: black 1px dotted; border-bottom: black 1px dotted;">
                <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr height="3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr height="3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            พนักงานขาย</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            สถานะ</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
                <table border="0" cellpadding="0" cellspacing="0" width="190px">
                    <tr height="25px">
                        <td width="5px"></td>
                        <td colspan="2" style="width: 66px">
                            &nbsp;จำนวนเงิน</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25">
                        <td width="5">
                        </td>
                        <td colspan="2" style="width: 66px">
                        </td>
                        <td>
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="80px"></asp:TextBox></td>
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>

