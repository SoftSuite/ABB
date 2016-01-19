<%@ Page Language="C#" MasterPageFile="~/Template/Page1.master" AutoEventWireup="true" CodeFile="ProductInvoice.aspx.cs" Inherits="Transaction_ProductInvoice" Title="ใบเสร็จรับเงิน" %>
<%@ Register Src="../Controls/DatePickerControl.ascx" TagName="DatePickerControl"
    TagPrefix="uc2" %>
<%@ Register Src="../Controls/ToolbarControl.ascx" TagName="ToolbarControl" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <script language="javascript" src="../Template/BaseScript.js" type="text/javascript"></script>
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td class="headtext" colspan="3">
                &nbsp;ใบเสร็จรับเงิน</td> 
        </tr> 
        <tr>
            <td class="toolbarplace" colspan="3">
                <uc1:ToolbarControl ID="ctlToolbar" runat="server" BtnBackShow="true" BtnCancelShow="false"
                    BtnDeleteShow="false" BtnEditShow="false" BtnNewShow="false" BtnPrintShow="true"
                    BtnReturnShow="false" BtnSaveShow="true" BtnSubmitShow="true" NameBtnSubmit="บันทึกและส่งคลังสำเร็จรูป"
                    OnBackClick="BackClick" OnCancelClick="CancelClick" OnSaveClick="SaveClick" OnSubmitClick="SubmitClick" />
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
                <table border="0" cellpadding="0" cellspacing="0" width="665">
                    <tr>
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" width="400">
                            <table border="0" cellspacing="0" cellpadding="0" width="390px" class="zCombobox">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px"></td>
                                    <td style="width: 135px"></td>
                                    <td style="width: 50px"></td>
                                    <td style="width: 135px"></td>
                                </tr> 
                                <tr height="25">
                                    <td style="width: 5px; height: 26px;">
                                    </td>
                                    <td style="width: 75px; height: 26px;">
                                    </td>
                                    <td colspan="3" style="height: 26px">
                                        <asp:TextBox ID="txtRefNo" runat="server" Width="120px" OnTextChanged="txtRefNo_TextChanged" AutoPostBack="True" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton
                                            ID="btnSearch" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/Images/view.gif" OnClick="btnSearch_Click" /></td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        ประเภท</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbRefType" runat="server" Width="323px" CssClass="zcombobox" Enabled="False"></asp:DropDownList>
                                        </td>
                                </tr>
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        รหัสลูกค้า</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtCustomerCode" runat="server" Width="80px" CssClass="zTextbox-View" ReadOnly="True"></asp:TextBox>
                                        <asp:TextBox ID="txtCustomerName" runat="server" CssClass="zTextbox-View" ReadOnly="True" Width="235px"></asp:TextBox>
                                </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">
                                        ชื่อผู้ติดต่อ</td>
                                    <td colspan="3">
                                        <asp:DropDownList ID="cmbTitle" runat="server" Width="80px" CssClass="zcombobox"></asp:DropDownList> 
                                        <asp:TextBox ID="txtName" runat="server" Width="95px" CssClass="zTextbox"></asp:TextBox>
                                        <asp:TextBox ID="txtLastName" runat="server" Width="127px" CssClass="zTextbox"></asp:TextBox></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px" valign="top">
                                        ที่อยู่</td>
                                    <td colspan="3">
                                        <asp:TextBox ID="txtAddress" runat="server" Height="50px" TextMode="MultiLine" Width="315px" CssClass="zTextbox"></asp:TextBox></td>
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
                                        <asp:TextBox ID="txtFax" runat="server" Width="126px" CssClass="zTextbox"></asp:TextBox> 
                                    </td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 75px">ขนส่งโดย</td>
                                    <td style="width: 135px"><asp:DropDownList ID="cmbDelivery" runat="server" Width="140px" CssClass="zCombobox">
                                        <asp:ListItem Value="ZZ">ไม่ระบุ</asp:ListItem>
                                        <asp:ListItem Value="CU">รับเอง</asp:ListItem>
                                        <asp:ListItem Value="CA">ส่งโดยรถบริษัท</asp:ListItem>
                                        <asp:ListItem Value="TR">ส่งโดยบริษัทรับจ้างขนส่ง</asp:ListItem>
                                        <asp:ListItem Value="MA">ส่งทางไปรษณีย์</asp:ListItem>
                                    </asp:DropDownList> </td>
                                    <td colspan="2">
                                       <asp:TextBox ID="txtOther" runat="server" Width="175px" CssClass="zTextbox"></asp:TextBox> </td>
                                </tr> 
                            </table> 
                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtStatus" runat="server" CssClass="zHidden" Width="30px">WA</asp:TextBox>
                                        <asp:TextBox ID="txtLOID" runat="server" CssClass="zHidden" Width="30px">0</asp:TextBox>
                                        <asp:TextBox ID="txtWareHouse" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtRefLoid" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                            <asp:TextBox ID="txtPopup" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
									 <asp:TextBox ID="txtNewPopup" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
			    <asp:TextBox ID="txtNewBind" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                            <asp:TextBox ID="txtRequisitionType" runat="server" CssClass="zHidden" Width="30px"></asp:TextBox>
                                        <asp:TextBox ID="txtCode" runat="server" CssClass="zHidden" ReadOnly="True"
                                            Width="17px" ></asp:TextBox></td>
                        <td width="5">
                        </td> 
                        <td valign="top" style="border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid" width="260">
                            <table border="0" cellpadding="0" cellspacing="0" width="260">
                                <tr height="5px">
                                    <td style="width: 5px;"></td>
                                    <td style="width: 110px;"></td>
                                    <td style="width: 145px;"></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        เลขที่ออกใบเสร็จ</td>
                                    <td style="width: 145px">
                                        <asp:TextBox ID="txtInvCode" runat="server" CssClass="zTextbox-View"
                                            Width="100px" ReadOnly="True"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        วันที่ออกใบเสร็จ</td>
                                    <td style="width: 145px"><uc2:DatePickerControl ID="ctlReqDate" runat="server" Enabled="true" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width:110px">
                                        กำหนดส่งสินค้า</td>
                                    <td style="width: 145px">
                                        <uc2:DatePickerControl ID="ctlDueDate" runat="server" Enabled="true" />
                                    <asp:Label ID="lblRemark" runat="server" Text="*" CssClass="zRemark"></asp:Label></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        วันที่ส่งสินค้า</td>
                                    <td style="width: 145px">
                                        <uc2:DatePickerControl ID="ctlSendDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        เลขที่อ้างอิง</td>
                                    <td style="width: 145px">
                                        <asp:TextBox ID="txtReference" runat="server" CssClass="zTextbox" Width="126px"></asp:TextBox></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                 <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        วันที่รับคำสั่งซื้อ</td>
                                    <td style="width: 145px">
                                        <uc2:DatePickerControl ID="ctlRecieveDate" runat="server" Enabled="false" /></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        เงื่อนไขการชำระ</td>
                                    <td style="width: 145px"><asp:DropDownList ID="cmbCondition" runat="server" Width="140px" CssClass="zCombobox">
                                        <asp:ListItem Value="ZZ">ไม่ระบุ</asp:ListItem>
                                        <asp:ListItem Value="CA">เงินสด</asp:ListItem>
                                        <asp:ListItem Value="CC">บัตรเครดิต</asp:ListItem>
                                        <asp:ListItem Value="CR">สินเชื่อ</asp:ListItem>
                                    </asp:DropDownList></td>
                                    <td style="width: 5px;"></td>
                                </tr> 
                                 <tr height="25px">
                                    <td style="width: 5px"></td>
                                    <td style="width: 110px">
                                        วันครบกำหนดชำระ</td>
                                    <td style="width: 145px">
                                        <uc2:DatePickerControl ID="ctlCreditDay" runat="server" /></td>
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
                                Width="665px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="ItemDataSource" OnRowCommand="grvItem_RowCommand" OnRowDataBound="grvItem_RowDataBound" OnRowUpdated="grvItem_RowUpdated" OnRowDeleted="grvItem_RowDeleted" OnRowUpdating="grvItem_RowUpdating">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbDelete"
                                                    runat="server" CausesValidation="False" CommandName="Delete" ImageUrl="~/Images/icn_delete.gif"/>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Update"
                                                ImageUrl="~/Images/icn_save.gif" />&nbsp;<asp:ImageButton ID="imbCancel"
                                                    runat="server" CausesValidation="False" CommandName="CancelItem" ImageUrl="~/Images/icn_cancel.gif"/>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </FooterTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField DataField="RANK" SortExpression="RANK" HeaderText="ลำดับที่" InsertVisible="False" ReadOnly="True">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:BoundField> 
                                    
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBarcodeView" runat="server" Text='<%# Bind("BARCODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" Text='<%# Bind("BARCODE") %>' OnTextChanged="txtBarCode_TextChanged"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged1" ></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:Label ID="lblProductView" runat="server" Text='<%# Bind("PRODUCTNAME") %>'></asp:Label>
                                            <asp:Label ID="lblProduct" CssClass="zHidden" runat="server" Text='<%# Bind("PRODUCT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cmbProduct" runat="server" CssClass="zCombobox" Width="195px" AutoPostBack="True" OnSelectedIndexChanged="cmbProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="195px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged1"></asp:DropDownList>
                                        </FooterTemplate>
                                        <HeaderStyle Width="200px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" CssClass="zTextboxR" Width="45px" Text='<%# Convert.ToDouble(Eval("QTY")).ToString(ABB.Data.Constz.IntFormat) %>' OnTextChanged="txtQty_TextChanged" AutoPostBack="True"></asp:TextBox>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="1"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="50px" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUnitView" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblUnit" runat="server" Text='<%# Bind("UNITNAME") %>'></asp:Label>
                                            <asp:TextBox ID="txtUnit" runat="server" CssClass="zHidden" Text='<%# Bind("UNIT") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNewUnit" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zHidden" Text="0"></asp:TextBox>
                                        </FooterTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPriceView" runat="server" Text='<%# Convert.ToDouble(Eval("PRICE")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtPrice" runat="server" Text='<%# Convert.ToDouble(Eval("PRICE")).ToString(ABB.Data.Constz.DblFormat) %>' ReadOnly="true" CssClass="zTextboxR-View" Width="55px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" Text="0" ReadOnly="true" CssClass="zTextboxR-View" Width="55px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมเงิน">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNetPriceView" runat="server" Text='<%# Convert.ToDouble(Eval("NETPRICE")).ToString(ABB.Data.Constz.DblFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtNetPrice" runat="server" Text='<%# Convert.ToDouble(Eval("NETPRICE")).ToString(ABB.Data.Constz.DblFormat) %>' ReadOnly="true" CssClass="zTextboxR-View" Width="55px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" Text="0" ReadOnly="true" CssClass="zTextboxR-View" Width="55px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle Width="60px" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="NORMALDISCOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNormalDiscountView" runat="server" Text='<%# Convert.ToDouble(Eval("NORMALDISCOUNT")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblNormalDiscount" runat="server" Text='<%# Convert.ToDouble(Eval("NORMALDISCOUNT")).ToString(ABB.Data.Constz.IntFormat) %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNewNormalDiscount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField> 

                                    <asp:TemplateField HeaderText="ISVAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblIsVatView" runat="server" Text='<%# Bind("ISVAT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lblIsVat" runat="server" Text='<%# Eval("ISVAT") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblNewIsVat" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField>

                                    <asp:BoundField DataField="LOID">
                                        <ControlStyle CssClass="zHidden" />
                                        <ItemStyle CssClass="zHidden" />
                                        <HeaderStyle CssClass="zHidden" />
                                        <FooterStyle CssClass="zHidden" />
                                    </asp:BoundField>

                                </Columns>
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView>
<asp:ObjectDataSource ID="ItemDataSource" runat="server" DeleteMethod="DeleteRequisitionItem" SelectMethod="GetRequisitionItem" 
                                TypeName="InvoiceItem" UpdateMethod="UpdateRequisitionItem" OldValuesParameterFormatString="{0}">
                                <DeleteParameters>
                                    <asp:Parameter Name="LOID" Type="Double" />
                                </DeleteParameters>
                                <UpdateParameters>
                                    <asp:Parameter Name="LOID" Type="Decimal"/>
                                    <asp:Parameter Name="PRODUCT" Type="Decimal" />
                                    <asp:Parameter Name="QTY" Type="Decimal" />
                                    <asp:Parameter Name="UNIT" Type="Decimal" />
                                    <asp:Parameter Name="PRICE" Type="Decimal" />
                                    <asp:Parameter Name="NORMALDISCOUNT" Type="Decimal" />
                                    <asp:Parameter Name="NETPRICE" Type="Decimal" />
                                    <asp:Parameter Name="BARCODE" Type="String" /> 
                                    <asp:Parameter Name="ISVAT" Type="String" />  
                                    <asp:Parameter Name="RANK" Type="Decimal" />
                                    <asp:Parameter Name="PRODUCTNAME" Type="String" />  
                                    <asp:Parameter Name="UNITNAME" Type="String" />  
                                </UpdateParameters>
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="txtLOID" Name="requisition" PropertyName="Text"
                                        Type="Double" />
                                    <asp:ControlParameter ControlID="txtStatus" Name="status" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txtNewPopup" Name="Popup" PropertyName="Text" Type="String" />
				                    <asp:ControlParameter ControlID="txtNewBind" Name="NewBind" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txtWareHouse" Name="warehouse" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txtCustomer" Name="customer" PropertyName="Text" Type="String" />
                                    <asp:ControlParameter ControlID="txtRequisitionType" Name="requisitiontype" PropertyName="Text" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            
                            <asp:GridView ID="grvItemNew" runat="server" AutoGenerateColumns="False" CssClass="t_tablestyle" ShowFooter="false"
                                Width="665px" DataKeyNames="LOID" EmptyDataText="<center>***ไม่พบข้อมูล***</center>" DataSourceID="NewItemDataSource" OnRowCommand="grvItemNew_RowCommand" OnRowDataBound="grvItemNew_RowDataBound">
                                <PagerSettings Visible="False" />
                                <Columns>
                                    <asp:TemplateField ShowHeader="False">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imbSave" runat="server" CausesValidation="True" CommandName="Insert" AlternateText="เพิ่มรายการใหม่"
                                                ImageUrl="~/Images/icn_save.gif" />
                                        </ItemTemplate>
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:BoundField HeaderText="ลำดับที่" InsertVisible="False">
                                        <ItemStyle Width="50px" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" />
                                        <FooterStyle Width="50px" HorizontalAlign="Center" />
                                    </asp:BoundField> 
                                     
                                    <asp:TemplateField HeaderText="บาร์โค้ด">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewBarCode" runat="server" AutoPostBack="true" CssClass="zTextbox" Width="95px" MaxLength="20" OnTextChanged="txtNewBarCode_TextChanged"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="100px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="สินค้า">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="cmbNewProduct" runat="server" CssClass="zCombobox" Width="195px" AutoPostBack="True" OnSelectedIndexChanged="cmbNewProduct_SelectedIndexChanged"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle Width="200px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="จำนวน">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewQty" runat="server" CssClass="zTextboxR" Width="45px" Text="1"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="หน่วย" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNewUnit" runat="server"></asp:Label>
                                            <asp:TextBox ID="txtNewUnit" runat="server" CssClass="zHidden" Text="0"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="ราคาต่อหน่วย" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewPrice" runat="server" Text="0" ReadOnly="true" CssClass="zTextboxR-View" Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="รวมเงิน" InsertVisible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtNewNetPrice" runat="server" Text="0" ReadOnly="true" CssClass="zTextboxR-View" Width="55px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="NORMALDISCOUNT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNewNormalDiscount" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="ISVAT">
                                        <ItemTemplate>
                                            <asp:Label ID="lblNewIsVat" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ControlStyle CssClass="zHidden"/>
                                        <ItemStyle CssClass="zHidden"/>
                                        <HeaderStyle CssClass="zHidden"/>
                                        <FooterStyle CssClass="zHidden"/>
                                    </asp:TemplateField>
                                     
                                </Columns>
                                <EmptyDataRowStyle BorderWidth="0px" Width="600px"  />
                                <HeaderStyle CssClass="t_headtext" />
                                <AlternatingRowStyle CssClass="t_alt_bg" />
                            </asp:GridView> 
                            <asp:ObjectDataSource ID="NewItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetRequisitionItemBlank" TypeName="InvoiceItem"></asp:ObjectDataSource>
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
                                    <td>
                                        <table border="0" cellspacing="0" cellpadding="0" width="420">
                                            <tr>
                                                <td colspan="5">ชำระเงินโดย</td>
                                            </tr>
                                            <tr>
                                                <td style="width:10px">
                                                    <asp:RadioButton ID="rbtPayment1" runat="server" AutoPostBack="True"
                                                        OnCheckedChanged="rbtPayment1_CheckedChanged" Checked="True" GroupName="PaymentGroup" /></td>
                                                <td colspan="4">เงินสด</td>
                           
                                            </tr>
                                            <tr>
                                                <td style="width:10px">
                                                    <asp:RadioButton ID="rbtPayment2" runat="server" AutoPostBack="True" OnCheckedChanged="rbtPayment2_CheckedChanged" GroupName="PaymentGroup" /></td>
                                                <td style="width:60px">บัตรเครดิต</td>
                                                <td style="width:140px">
                                                    <asp:DropDownList ID="cmbCreditType" runat="server" Enabled="False" Width="130px" CssClass="zComboBox">
                                                    </asp:DropDownList></td>
                                                <td style="width:70px">เลขที่</td>
                                                <td style="width:140px">
                                                    <asp:TextBox ID="txtCreditID" runat="server" CssClass="zTextbox" Enabled="False"
                                                        Width="130px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width:10px">
                                                    <asp:RadioButton ID="rbtPayment3" runat="server" AutoPostBack="True" OnCheckedChanged="rbtPayment3_CheckedChanged" GroupName="PaymentGroup" /></td>
                                                <td style="width:60px">เช็คเลขที่</td>
                                                <td style="width:140px">
                                                    <asp:TextBox ID="txtCheque" runat="server" CssClass="zTextbox" Enabled="False" Width="130px"></asp:TextBox></td>
                                                <td style="width:70px">ลงวันที่</td>
                                                <td style="width:140px"><uc2:DatePickerControl ID="ctlChequeDate" runat="server" Enabled="false" /></td>
                                            </tr>
                                            <tr>
                                                <td style="width:10px">&nbsp;</td>
                                                <td style="width:60px">ธนาคาร</td>
                                                <td style="width:140px"><asp:DropDownList ID="cmbBank" runat="server" Enabled="False" Width="130px" CssClass="zComboBox">
                                                </asp:DropDownList></td>
                                                <td style="width:70px">สาขา</td>
                                                <td style="width:140px">
                                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="zTextbox" Enabled="False" Width="130px"></asp:TextBox></td>
                                            </tr>
                                            <tr>
                                                <td style="width:10px">
                                                    <asp:RadioButton ID="rbtPayment4" runat="server" AutoPostBack="True" OnCheckedChanged="rbtPayment4_CheckedChanged" GroupName="PaymentGroup" /></td>
                                                <td style="width:60px">อื่นๆ</td>
                                                <td colspan="3">
                                                    <asp:TextBox ID="txtReason" runat="server" CssClass="zTextbox" Enabled="False" Width="285px"></asp:TextBox></td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    <table border="0" cellspacing="0" cellpadding="0" width="180">
                                    <tr>
                                    <td valign="top">
                                        &nbsp;หมายเหตุ<br />
                                        <asp:TextBox ID="txtRemark" runat="server" Height="100px" TextMode="MultiLine" Width="170px" CssClass="zTextbox"></asp:TextBox></td> 
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
                <table border="0" cellpadding="0" cellspacing="0" width="160">
                    <tr height="3px">
                        <td colspan="3"></td>
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 75px">
                            พนักงานขาย</td>
                        <td style="width: 75px">
                            <asp:TextBox ID="txtCreateBy" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td width="5px"></td>
                        <td style="width: 75px">
                            สถานะ</td>
                        <td style="width: 75px">
                            <asp:TextBox ID="txtStatusName" runat="server" CssClass="zTextbox-View" ReadOnly="True"
                                Width="80px"></asp:TextBox></td> 
                    </tr> 
                </table>
                <hr/> 
                <table border="0" cellpadding="0" cellspacing="0" width="160">
                    <tr height="25px">
                        <td style="width: 5px"></td>
                        <td colspan="2">
                            &nbsp;จำนวนเงิน</td>
                        <td style="width: 70px">
                            <asp:TextBox ID="txtTotal" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="70px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25px">
                        <td style="width: 5px"></td>
                        <td style="width: 45px">
                            &nbsp;ส่วนลด</td>
                        <td style="width: 40px">
                            <asp:TextBox ID="txtDiscount" runat="server" CssClass="zTextboxR-View" Width="25px" MaxLength="2" AutoPostBack="True" OnTextChanged="txtDiscount_TextChanged" ReadOnly="True"></asp:TextBox>%</td>
                        <td style="width: 70px">
                            <asp:TextBox ID="txtTotalDiscount" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="70px"></asp:TextBox></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 5px"></td>
                        <td style="width: 45px">
                            &nbsp;VAT</td>
                        <td style="width: 40px">
                            <asp:TextBox ID="txtVat" runat="server" CssClass="zTextboxR-View" Width="25px" MaxLength="2" AutoPostBack="True" OnTextChanged="txtVat_TextChanged" ReadOnly="True"></asp:TextBox>%</td>
                        <td style="width: 70px">
                            <asp:TextBox ID="txtTotalVat" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="70px"></asp:TextBox></td> 
                    </tr>
                    <tr height="25px">
                        <td style="width: 5px"></td>
                        <td colspan="2">
                            &nbsp;รวม</td>
                        <td style="width: 70px">
                            <asp:TextBox ID="txtNet" runat="server" CssClass="zTextboxR-View" ReadOnly="True"
                                Width="70px"></asp:TextBox></td> 
                    </tr> 
                    <tr height="25">
                        <td style="width: 5px; height: 25px">
                        </td>
                        <td colspan="2" style="height: 25px">
                            <asp:TextBox ID="txtLowerPrice" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="30px">0</asp:TextBox>
                            <asp:TextBox ID="txtMemberDiscount" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="28px">0</asp:TextBox></td>
                        <td style="width: 70px; height: 25px">
                            <asp:TextBox ID="txtGrandTotal" runat="server" CssClass="zHidden" ReadOnly="True"
                                Width="70px"></asp:TextBox></td>
                    </tr>
                </table>
            </td> 
        </tr>
    </table>
</asp:Content>
