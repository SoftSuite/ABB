<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="Stockout.aspx.cs" Inherits="FG_Transaction_Stockout" Title="ใบเบิกสินค้าออก" %>
<%@ Register Src="../../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบเบิกสินค้าออก</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="อนุมัติ"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick" />
            </td> 
        </tr>
        <tr height="10px">
            <td>
            </td>
            <td>
            </td> 
            <td style="width: 184px">
            </td> 
        </tr> 
        <tr height="25px" width="900px">
            <td valign="top" width="715">
                <table border="0" cellpadding="0" cellspacing="0" width="715px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" width="405">
                            <table border="0" cellspacing="0" cellpadding="0" width="505" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px"></td>
                                    <td style="width: 135px"></td>
                                    <td></td>
                                    <td style="width: 135px"></td>
                                    <td width="5">
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        ประเภท</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtDocTypeName" runat="server" CssClass="zTextbox-View" Width="185px" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtRefNo" runat="server" Width="100px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                                    <td colspan="1" width="5">
                                    </td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        รหัสลูกค้า</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="80px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="208px"></asp:TextBox>
                                </td>
                                    <td colspan="1" width="5">
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbTitle" runat="server" Width="80px"></asp:DropDownList> 
                                        <asp:TextBox ID="txtName" runat="server" Width="80px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="140px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td colspan="1" width="5">
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="315px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td colspan="1" width="5">
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        โทรศัพท์</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtTel" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox></td>
                                    <td style="width: 50px">
                                        โทรสาร</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtFax" runat="server" Width="130px" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                    <td width="5">
                                    </td>
                                </tr> 
                                <tr height="5px">
                                    <td style="width: 5px;" height="5"></td>
                                    <td height="5" colspan="4">
                                        <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtSender" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtReceiver" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtDocType" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtWarehouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtRefWarehouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox></td>
                                    <td height="5" width="5">
                                    </td>
                                </tr> 
                            </table> 
                        </td>
                        <td width="5">
                            &nbsp;</td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" width="205">
                            <table border="0" cellpadding="0" cellspacing="0" width="205">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 120px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        เลขที่ใบเบิก</td>
                                    <td style="width: 120px">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View"
                                            Width="110px" ReadOnly="True"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        ลงวันที่</td>
                                    <td style="width: 120px"><uc2:DatePickerControl ID="ctlCreateOn" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width:75px">
                                        วันที่ขอเบิก</td>
                                    <td style="width: 120px">
                                        <uc2:DatePickerControl ID="ctlReserveDate" runat="server" Enabled="false" />
                                    </td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        กำหนดส่ง</td>
                                    <td style="width: 120px">
                                        <uc2:DatePickerControl ID="ctlDueDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
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
                            <asp:GridView ID="grvItem" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                DataKeyNames="NO" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" 
                                OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating" ShowFooter="True" Width="715px" DataSourceID="ItemDataSource" >
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="บันทึก" CausesValidation="True"
                                                CommandName="Update" ImageUrl="~/Images/icn_save.gif" />
                                            <asp:ImageButton ID="imbCancel" runat="server" AlternateText="ยกเลิก" CausesValidation="False"
                                                CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif" />
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" AlternateText="แก้ไข" CausesValidation="False"
                                                CommandName="Edit" ImageUrl="~/Images/icn_edit.gif" />
                                            <asp:ImageButton ID="imbDelete" runat="server" AlternateText="ลบ" CausesValidation="False"
                                                CommandName="Delete" ImageUrl="~/Images/icn_delete.gif" />
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("NO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextbox-View" ReadOnly="true"
                                                MaxLength="20" OnTextChanged="txtBarcode_TextChanged" Text='<%# Bind("BARCODE") %>'
                                                Width="95px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtBarcodeNew" runat="server" AutoPostBack="true" CssClass="zTextbox"
                                                MaxLength="20" OnTextChanged="txtBarcodeNew_TextChanged1" Width="95px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>' ></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Enabled="false"
                                                OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged" Width="240px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbProductNew" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                                OnSelectedIndexChanged="cmbProductNew_SelectedIndexChanged1" Width="240px">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                        <asp:Label ID="lblProductView" runat="server" Width="240px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="245px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot No">
										<EditItemTemplate>
                                            <asp:DropDownList ID="cmbLotNo" runat="server" CssClass="zCombobox"
                                                Width="80px" AutoPostBack="True" OnSelectedIndexChanged="cmbLotNo_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbLotNoNew" runat="server" CssClass="zCombobox"
                                               Width="80px"  AutoPostBack="True" OnSelectedIndexChanged="cmbLotNoNew_SelectedIndexChanged1">
                                            </asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="85px" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblLotNoView" runat="server" Text='<%# Bind("LOTNO") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="คงเหลือ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtRemainQty" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Text='<%# Bind("REMAINQTY") %>'
                                                Width="45px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRemainQtyNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Text="0" Width="45px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtRemainQtyView" runat="server" Text='<%# Bind("REMAINQTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Text='<%# Bind("QTY") %>'
                                                Width="45px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtQtyNew" runat="server" CssClass="zTextboxR" Text="0" Width="45px"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zHidden" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOID" runat="server" CssClass="zHidden" Text='<%# Bind("REFLOID") %>'></asp:TextBox>
                                            <asp:Label ID="lblUnitName" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtPriceNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOIDNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:Label ID="lblUnitNameNew" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitNameView" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                   
                                    <asp:BoundField DataField="UNIT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PRICE">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="REFLOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteStockOutItem"
                                OldValuesParameterFormatString="{0}" SelectMethod="GetStockOutItem"
                                TypeName="StockOutFGItem" UpdateMethod="UpdateStockOutItem">
                                 <DeleteParameters>
                                    <asp:Parameter Name="No" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="NO" Type="Decimal" />
                                    <asp:Parameter Name="REFLOID" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" />
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="LOTNO" Type="String" />
                                    <asp:Parameter Name="REMAINQTY" Type="Decimal" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="UNITNAME" Type="String" />
                                    <asp:Parameter Name="REQUISITION" Type="Decimal" />
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="stockOut" PropertyName="Text" Type="Double" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle"
                                DataKeyNames="NO" EmptyDataText="<center>***ไม่พบข้อมูล***</center>"
                                OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound" Width="715px" DataSourceID="NewItemDataSource">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" AlternateText="เพิ่มรายการใหม่" CausesValidation="True"
                                                CommandName="Insert" ImageUrl="~/Images/icn_save.gif" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                        <FooterStyle HorizontalAlign="Center" Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="50px" />
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="รหัสสินค้า">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBarcodeNew" runat="server" AutoPostBack="true" CssClass="zTextbox"
                                                MaxLength="20" OnTextChanged="txtBarcodeNew_TextChanged" Width="95px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbProductNew" runat="server" AutoPostBack="True" CssClass="zCombobox"
                                                OnSelectedIndexChanged="cmbProductNew_SelectedIndexChanged" Width="240px">
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle Width="245px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Lot No">
                             <ItemTemplate>
                                            <asp:DropDownList ID="cmbLotNoNew" runat="server" AutoPostBack="True" CssClass="zCombobox" Width="80px" OnSelectedIndexChanged="cmbLotNoNew_SelectedIndexChanged"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="85px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="คงเหลือ">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtRemainQtyNew" runat="server" CssClass="zTextboxR-View" ReadOnly = "true" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQtyNew" runat="server" CssClass="zTextboxR" Width="45px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitNameNew" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtUnitNew" runat="server" CssClass="zHidden"></asp:TextBox>
                                            <asp:TextBox ID="txtPriceNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                            <asp:TextBox ID="txtRefLOIDNew" runat="server" CssClass="zHidden" Text=""></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                    </asp:TemplateField>

                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px" />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="{0}"
                                SelectMethod="GetStockOutItemBlank" TypeName="StockOutFGItem"></asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr height="5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="3">
                            <table border="0" cellspacing="0" cellpadding="0" width="700">
                                <tr>
  
                                    <td>
                                    <table border="0" cellspacing="0" cellpadding="0" width="715">
                                    <tr>
                                    <td valign="top" style="width: 80px">
                                        &nbsp;หมายเหตุ </td>
										<td>
                                        <asp:TextBox ID="txtRemark" runat="server" Height="80px" TextMode="MultiLine" Width="540px" CssClass="zTextbox"></asp:TextBox></td> 
                                    </tr>
                                    </table>
                                </td>
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
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 80px">
                            พนักงาน</td>
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
                        <td width="80" >
                            ยอดสุทธิ</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>
