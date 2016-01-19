<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="Transaction_PurchaseOrder" Title="ใบสั่งซื้อ/จ้าง" %>

<%@ Register Src="../Controls/ToolbarControlPO.ascx" TagName="ToolbarControlPO" TagPrefix="uc3" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl" TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบสั่งซื้อ/จ้าง</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc3:ToolbarControlPO ID="ToolbarControlPO1" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="อนุมัติจัดซื้อ"
                    BtnSentShow="false" NameBtnSent="ส่งให้ผู้จำหน่าย" OnSentClick="SentClick"
                    OnBackClick="BackClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick"/>
            </td> 
        </tr>
        <tr style="height:10px">
            <td>
            </td>
            <td>
            </td> 
            <td>
            </td> 
        </tr> 
        <tr style="height:25px; width:800px">
            <td valign="top">
                <table border="0" cellpadding="0" cellspacing="0" style="width:615px">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:100%" class="zCombobox">
                                <tr style="height:5px">
                                    <td colspan="5"></td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px">
                                        ประเภทใบสั่งซื้อ</td>
                                    <td style="width: 135px">
                                        <asp:DropDownList ID="cmbType" runat="server" Width="90%" Enabled="false" AutoPostBack="True" OnSelectedIndexChanged="cmbType_SelectedIndexChanged">
                                            <asp:ListItem Value="N">สั่งซื้อปกติ</asp:ListItem>
                                            <asp:ListItem Value="B">สั่งซื้อย้อนหลัง</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px">
                                        ผู้จำหน่าย</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbSupplier" runat="server" AutoPostBack="true" Width="90%" Enabled="true" OnSelectedIndexChanged="cmbSupplier_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:Label ID="lblRemark" runat="server" Text="*" CssClass="zRemark"></asp:Label>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="0px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtRefLOID" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                        <asp:TextBox ID="txtRefTable" runat="server" CssClass="zHidden" Width="0px">0</asp:TextBox>
                                </tr>
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCName" runat="server" CssClass="zTextbox" Width="90%"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="90%" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px">
                                        โทรศัพท์</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtTel" runat="server" Width="85%" CssClass="zTextbox"></asp:TextBox></td>
                                    <td style="width: 50px">
                                        โทรสาร</td>
                                    <td style="width: 135px">
                                        <asp:TextBox ID="txtFax" runat="server" Width="80%" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px">
                                        เงื่อนไขในการชำระ</td>
                                    <td style="width: 135px">
                                        <asp:DropDownList ID="cmbPaymentType" runat="server" Width="90%" Enabled="true">
                                            <asp:ListItem Value="CA">เงินสด</asp:ListItem>
                                            <asp:ListItem Value="CC">บัตรเครดิต</asp:ListItem>
                                            <asp:ListItem Value="CR">สินเชื่อ</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtPaymentDesc" runat="server" CssClass="zTextbox" Width="85%"></asp:TextBox></td>
                                </tr>
                                <tr style="height:5px">
                                    <td colspan="5"></td>
                                </tr> 
                            </table> 
                        </td>
                        <td style="width:3px">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid">
                            <table border="0" cellpadding="0" cellspacing="0" style="width:217px">
                                <tr style="height:5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px;"></td>
                                    <td style="width: 132px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px; height: 25px;"></td>
                                    <td style="width: 75px; height: 25px;">
                                        เลขที่ใบสั่งซื้อ</td>
                                    <td style="width: 132px; height: 25px;">
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        ลงวันที่</td>
                                    <td style="width: 132px"><uc2:DatePickerControl ID="ctlOrderDate" runat="server" Enabled="true" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width:75px">
                                        เลขที่ใบสั่งซื้อเดิม</td>
                                    <td style="width: 132px; height: 25px;">
                                        <asp:TextBox ID="txtPOOldCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px; height: 25px;"></td>
                                </tr> 
                                <tr style="height:25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 75px">
                                        เลขที่บันทึกแก้ไข</td>
                                    <td style="width: 132px; height: 25px;">
                                        <asp:TextBox ID="txtPOEditCode" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                            Width="110px"></asp:TextBox></td>
                                    <td style="width: 5px; height: 25px;"></td>
                                </tr> 
                            </table> </td>
                    </tr> 
                    <tr style="height:5px">
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
                                
                                    <asp:TemplateField ShowHeader="False">
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update" AlternateText="บันทึก"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel" AlternateText="ยกเลิก"
                                                runat="server" CausesValidation="False" CommandName="Cancel" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbEdit" runat="server" CausesValidation="False" CommandName="Edit" AlternateText="แก้ไข"
                                                ImageUrl="~/Images/icn_edit.gif"/>
                                            <asp:ImageButton ID="imbDelete" AlternateText="ลบ"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNo" runat="server" Text='<%# Bind("RANK") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="65px" Text='<%# Bind("BARCODE") %>'></asp:TextBox>
                                            <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="btnNewSearch" runat="server" CommandName="Search" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" />                                            
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtBarcodeView" runat="server" Width="95px" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="65px" Text="" ></asp:TextBox>
                                            <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
                                            <asp:ImageButton ID="btnNewSearch" runat="server" CommandName="Search" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" />
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtProduct" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px" Text='<%# Bind("PRODUCTNAME") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtProductView" runat="server" Width="95px" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNetPrice_TextChanged" Text='<%# Bind("QTY") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtQtyView" runat="server" Width="45px" Text='<%# Bind("QTY") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNetPrice_TextChanged1"  Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("UNITNAME") %>' ></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtUnitView" runat="server" Width="45px" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคา">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNetPrice_TextChanged" Text='<%# Bind("PRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPriceView" runat="server" Width="45px" Text='<%# Bind("PRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNetPrice_TextChanged1"  Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ส่วนลด">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtDiscount" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNetPrice_TextChanged" Text='<%# Bind("DISCOUNT") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtDiscountView" runat="server" Width="45px" Text='<%# Bind("DISCOUNT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewDiscount" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNetPrice_TextChanged1"  Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมเงิน">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text='<%# Bind("NETPRICE") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtNetPriceView" runat="server" Width="45px" Text='<%# Bind("NETPRICE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                        <HeaderStyle Width="50px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="กำหนดส่ง">
                                        <EditItemTemplate>
                                            <uc2:DatePickerControl ID="ctlDueDate" DateValue='<%# Bind("DUEDATE") %>' runat="Server"></uc2:DatePickerControl>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <uc2:DatePickerControl ID="ctlNewDueDate" DateValue='' runat="Server"></uc2:DatePickerControl>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <uc2:DatePickerControl ID="ctlDueDateView" DateValue='<%# Bind("DUEDATE" ) %>' Enabled="false" runat="Server"></uc2:DatePickerControl>
                                        </ItemTemplate>
                                        <HeaderStyle Width="125px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="เลขที่ใบขอซื้อ">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPRItemCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px" MaxLength="20" Text='<%# Bind("CODE") %>' ></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="txtPRItemCodeView" runat="server" Width="95px" Text='<%# Bind("CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPRItemCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px" Text="" ></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Width="100px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="100px"></HeaderStyle>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมVAT">
                                        <FooterTemplate>
                                            <asp:CheckBox ID="chkVat" runat="server" />
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkVat" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="LOID" >
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="ISVAT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PRODUCT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="UNIT">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                    <asp:BoundField DataField="PRITEM">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>
                                    
                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
                            
                            <asp:ObjectDataSource ID="ItemDataSource" runat="server" SelectMethod="GetPOItem" 
                                TypeName="POItem" UpdateMethod="UpdatePOItem" OldValuesParameterFormatString="{0}" DeleteMethod="DeletePOItem">
                                <deleteparameters>
                                    <asp:Parameter Type="Double" Name="LOID"></asp:Parameter>
                                </deleteparameters>
                                <updateparameters>
                                    <asp:Parameter Type="Decimal" Name="LOID"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="RANK"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="PRODUCT"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="QTY"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="UNIT"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="PRICE"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="DISCOUNT"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="NETPRICE"></asp:Parameter>
                                    <asp:Parameter Type="DateTime" Name="DUEDATE"></asp:Parameter>
                                    <asp:Parameter Type="Decimal" Name="PRITEM"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="BARCODE"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="ISVAT"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="PRODUCTNAME"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="UNITNAME"></asp:Parameter>
                                    <asp:Parameter Type="String" Name="CODE"></asp:Parameter>
                                    <asp:Parameter Type="Double" Name="PDORDER"></asp:Parameter>
                                </updateparameters>
                                <selectparameters>
                                    <asp:ControlParameter PropertyName="Text" Type="Double" Name="PDOrder" ControlID="txtLOID"></asp:ControlParameter>
                                    <asp:ControlParameter PropertyName="Text" Type="String" Name="status" ControlID="txtStatus"></asp:ControlParameter>
                                </selectparameters>
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
                                            <asp:TextBox ID="txtNewBarCode" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="65px"></asp:TextBox>
                                            <asp:TextBox ID="txtGetData" runat="server" CssClass="zHidden" Width="95px" ></asp:TextBox>
					                        <asp:ImageButton ID="btnNewSearch" runat="server" CommandName="Search" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" />
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="สินค้า">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewProduct" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="95px"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="จำนวน">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNewNetPrice_TextChanged" Text="0"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ราคา" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNewNetPrice_TextChanged" Text="0"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ส่วนลด" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewDiscount" runat="server" CssClass="zTextboxR" Width="45px" AutoPostBack="True" OnTextChanged="txtNewNetPrice_TextChanged" Text="0"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="รวมเงิน" InsertVisible="False">
<HeaderStyle Width="50px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" CssClass="zTextboxR-View" ReadOnly="true" Width="45px" Text="0"></asp:TextBox>
                                    
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="กำหนดส่ง" InsertVisible="False">
<HeaderStyle Width="125px"></HeaderStyle>
<ItemTemplate>
                                            <uc2:DatePickerControl ID="ctlNewDueDate" DateValue='' runat="Server"></uc2:DatePickerControl>
                                        
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="เลขที่ใบขอซื้อ">
<HeaderStyle Width="100px"></HeaderStyle>
<ItemTemplate>
                                            <asp:TextBox ID="txtNewPRItemCode" runat="server" CssClass="zTextboxR-View" Width="95px"></asp:TextBox>
                                        
                                    
</ItemTemplate>
</asp:TemplateField>

                                    <asp:TemplateField HeaderText="รวมVAT">
                                        <ItemStyle HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkVat" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>


<asp:BoundField DataField="LOID">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="ISVAT">
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
<asp:BoundField DataField="UNIT">
<ControlStyle CssClass="zHidden"></ControlStyle>

<ItemStyle CssClass="zHidden"></ItemStyle>

<HeaderStyle CssClass="zHidden"></HeaderStyle>

<FooterStyle CssClass="zHidden"></FooterStyle>
</asp:BoundField>
<asp:BoundField DataField="PRITEM">
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
                                SelectMethod="GetPOItemBlank" TypeName="POItem">
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr style="height:5px">
                        <td colspan="4">
                        </td>
                    </tr> 
                    <tr>
                        <td colspan="4">
                            <table border="0" cellspacing="0" cellpadding="0" style="width:600">
                               <tr style="height:25px">
                                    <td style="width: 70px">
                                        การขนส่ง</td>
                                    <td style="width: 135px">
                                        <asp:DropDownList ID="cmbDelivery" runat="server" Width="130px" Enabled="true">
                                            <asp:ListItem Value="OT">อื่นๆ</asp:ListItem>
                                            <asp:ListItem Value="CU">ลูกค้ามารับเอง</asp:ListItem>
                                            <asp:ListItem Value="MA">ไปรษณีย์</asp:ListItem>
                                            <asp:ListItem Value="TR">บริษัทรับจ้างขนส่ง</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td style="width: 395px">
                                        <asp:TextBox ID="txtOther" runat="server" CssClass="zTextbox" Width="390px"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td valign="top" style="width:70px">
                                        หมายเหตุ</td>
                                    <td colspan="2">
                                        <asp:TextBox ID="txtRemark" runat="server" Height="50px" TextMode="MultiLine" Width="530px" CssClass="zTextbox"></asp:TextBox></td> 
                                </tr> 
                            </table> 
                        </td>
                    </tr>
                </table>
            </td> 
            <td style="width:2px">
                &nbsp;
            </td> 
            <td valign="top" style="border-left: black 1px dotted; border-right: black 1px dotted; border-top: black 1px dotted; border-bottom: black 1px dotted;">
                <table border="0" cellpadding="0" cellspacing="0" style="width:190px">
                    <tr style="height:3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td style="width: 80px">
                            สถานะ</td>
                        <td style="width: 105px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="100px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
                <table border="0" cellpadding="0" cellspacing="0" style="width:190px">
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td colspan="2">
                            &nbsp;ยอดรวม</td>
                        <td>
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td style="width: 50px">
                            &nbsp;VAT</td>
                        <td style="width: 50px">
                            <asp:TextBox ID="txtVat" runat="server" CssClass="zTextboxR" Width="25px" MaxLength="2" AutoPostBack="True" OnTextChanged="txtVat_TextChanged" ></asp:TextBox>%</td>
                        <td>
                            <asp:TextBox ID="txtTotalVat" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr>                    
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td colspan="2">
                            &nbsp;ส่วนลดรวม</td>
                        <td>
                            <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr>
                    <tr style="height:25px">
                        <td style="width:5px"></td>
                        <td colspan="2">
                            &nbsp;ยอดสุทธิ</td>
                        <td>
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>

